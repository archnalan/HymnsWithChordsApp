using AutoMapper;
using DocumentFormat.OpenXml.Office.CoverPageProps;
using HymnsWithChords.Data;
using HymnsWithChords.Dtos;
using HymnsWithChords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HymnsWithChords.Areas.Admin.ApiControllers
{
	[Route("admin/[controller]")]
	[ApiController]
	[Area("admin")]
	public class Api_HymnsController : ControllerBase
	{
		private readonly HymnDbContext _context;
		private readonly IMapper _mapper;

        public Api_HymnsController(HymnDbContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;
		}

		public async Task<IActionResult> Index()
		{
			var hymns = await _context.Hymns
						.OrderBy(h => h.Number)
						.ToListAsync();		

			var hymnDtos = _mapper.Map<List<HymnDto>>(hymns);

			return Ok(hymnDtos);
		}

		[HttpGet("categories")]
		public async Task<IActionResult> GetHymnsWithCategories()
		{
			var hymns = await _context.Hymns
						.OrderBy(h => h.Number)
						.Include(h=>h.Category)
						.ToListAsync();		

			var hymnDtos = _mapper.Map<List<HymnDto>>(hymns);

			return Ok(hymnDtos);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetHymnById(int id)
		{
			var hymn = await _context.Hymns.FindAsync(id);

			if(hymn == null) return NotFound($"Hymn of ID:{id} does not exist.");

			var hymnDto = _mapper.Map<HymnDto>(hymn);

			return Ok(hymnDto);
		}
		[HttpGet("category/{id}")]
		public async Task<IActionResult> GetHymnWithCategoryById(int id)
		{
			var hymn = await _context.Hymns
							.Include(h=>h.Category)
							.FirstOrDefaultAsync(h=>h.Id == id);

			if(hymn == null) return NotFound($"Hymn of ID:{id} does not exist.");

			var hymnDto = _mapper.Map<HymnDto>(hymn);

			return Ok(hymnDto);
		}

		[HttpGet("by_ids")]
		public async Task<IActionResult> GetHymnsByIds(List<int> ids)
		{
			if (ids == null || ids.Count == 0) return BadRequest("Hymn Ids are required.");
			
			var hymns = await _context.Hymns
						.Where(h=>ids.Contains(h.Id))
						.ToListAsync();

			var foundHymnsDtos = _mapper.Map<List<HymnDto>>(hymns);

			var notFoundHymnsDtos = ids.Except(foundHymnsDtos.Select(h => h.Id)).ToList();

			if(notFoundHymnsDtos.Count == ids.Count) return NotFound(notFoundHymnsDtos);

			if (notFoundHymnsDtos.Any())
			{
				return Ok(new
				{
					Found = foundHymnsDtos,
					NotFound = notFoundHymnsDtos
				});
			}

			return Ok(foundHymnsDtos);
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create(HymnCreateDto createDto)
		{
			if (createDto == null) return BadRequest("Hymn data is required.");

			if (!ModelState.IsValid) return BadRequest(ModelState);

			createDto.Slug = createDto.Title.ToLower().Replace(" ", "-");
			createDto.AddedDate = DateTime.Now;
			createDto.AddedBy = "admin";

			var hymnExists = await _context.Hymns.AnyAsync(hE=>hE.Slug ==  createDto.Slug);

			if (hymnExists) return Conflict($"Hymn: {createDto.Title} already exists.");

			var categoryExists = await _context.Categories									
									.FirstOrDefaultAsync(hC=>hC.Id==createDto.CategoryId);

			if (categoryExists == null)
				return BadRequest($"Hymn Category of ID: {createDto.CategoryId} does not exist.");

			var hymn = _mapper.Map<Hymn>(createDto);

			try
			{				
				await _context.Hymns.AddAsync(hymn);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			var createdHymnDto = _mapper.Map<HymnDto>(hymn);

			return CreatedAtAction(nameof(GetHymnById), new {id = hymn.Id}, createdHymnDto);
		}

		[HttpPut("edit/{id}")]
		public async Task<IActionResult> Edit(int id, HymnDto hymnDto)
		{
			if (hymnDto == null) return BadRequest("Hymn Data is required");

			if (!ModelState.IsValid) return BadRequest(ModelState);

			if (id != hymnDto.Id) 
				return BadRequest($"Invalid Attempt! Hymns with Ids {id} and {hymnDto.Id} are not the same.");

			var hymnInDb = await _context.Hymns.FindAsync(id);

			if (hymnInDb == null) return NotFound($"Hymn with ID: {id} does not exist.");

			hymnDto.Slug = hymnDto.Title.ToLower().Replace(" ", "-");
			hymnDto.AddedDate = DateTime.Now;
			hymnDto.AddedBy = "admin";

			var hymnExists = await _context.Hymns
								.Where(hE=>hE.Id != id)
								.AnyAsync(hE => hE.Slug == hymnDto.Slug);

			if (hymnExists) return Conflict($"Hymn: {hymnDto.Title} already exists.");

			var categoryExists = await _context.Categories
									.FirstOrDefaultAsync(hC => hC.Id == hymnDto.CategoryId);

			if (categoryExists == null)
				return BadRequest($"Hymn Category of ID: {hymnDto.CategoryId} does not exist.");

			var editedHymn = _mapper.Map(hymnDto, hymnInDb);

			try
			{
				_context.Hymns.Update(editedHymn);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message );
			}

			var editedHymnDto = _mapper.Map<HymnDto>(editedHymn);

			return Ok(editedHymnDto);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var hymn = await _context.Hymns.FindAsync(id);

			if (hymn == null) return NotFound($"Hymn with ID:{id} does not exist.");

			try
			{
				_context.Hymns.Remove(hymn);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return NoContent();
		}

		//DELETE admin/api_hymns/by_ids
		[HttpDelete("by_ids")]
		public async Task<IActionResult> DeleteHymns(List<int> ids)
		{
			if (ids == null || ids.Count == 0) return BadRequest("Hymn Ids are required.");

			var deletedIds = new List<int>();
			var errors = new List<string>();

			foreach (int id in ids)
			{
				var hymn = await _context.Hymns.FindAsync(id);

				if (hymn == null)
				{
					errors.Add($"Hymn with ID: {id} does not exist.");
					continue;
				}
				_context.Hymns.Remove(hymn);
				deletedIds.Add(id);
			}
			try
			{
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				errors.Add(ex.Message);
			}

			if (errors.Count == ids.Count) return NotFound(errors);

			if (errors.Any())
			{
				return Ok(new
				{
					Deleted = deletedIds,
					NotDeleted = errors
				});
			}

			return NoContent();
		}
	}
}
