using AutoMapper;
using HymnsWithChords.Data;
using HymnsWithChords.Dtos;
using HymnsWithChords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HymnsWithChords.Areas.Admin.ApiControllers
{
	[Route("admin/[controller]")]
	[ApiController]
	[Area("Admin")]
	public class Api_HymnBooksController : ControllerBase
	{
		private readonly HymnDbContext _context;
		private readonly IMapper _mapper;

        public Api_HymnBooksController(HymnDbContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }

		public async Task<IActionResult> Index()
		{
			var hymnBooks = await _context.HymnBooks
								.OrderBy(hb=>hb.Title)
								.ToListAsync();
			var hymBookDtos = _mapper.Map<List<HymnBookDto>>(hymnBooks);

			return Ok(hymBookDtos);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetHymnBookById(int id)
		{
			var hymnBook = await _context.HymnBooks.FindAsync(id);

			if (hymnBook == null) return NotFound($"HymnBook with ID: {id} does not exist.");

			var hymnBookDto = _mapper.Map<HymnBookDto>(hymnBook);

			return Ok(hymnBookDto);
		}
		
		[HttpGet("book_categories/{id}")]
		public async Task<IActionResult> GetHymnWithCategories(int id)
		{
			var hymnBook = await _context.HymnBooks
							.Include(hb=>hb.Categories)
							.FirstOrDefaultAsync(hb=>hb.Id == id);

			if (hymnBook == null) return NotFound($"HymnBook with ID: {id} does not exist.");

			var hymnBookDto = _mapper.Map<HymnBookWithCategoriesDto>(hymnBook);

			return Ok(hymnBookDto);
		}

		[HttpGet("by_ids")]
		public async Task<IActionResult> GetHymnBooksByIds(List<int> ids)
		{
			if (ids == null || ids.Count == 0) 
				return BadRequest("A List of Hymn Book Ids is required");

			var hymnBooks = await _context.HymnBooks
							.Where(hb=>ids.Contains(hb.Id))
							.ToListAsync();

			var foundBooksDto = _mapper.Map<List<HymnBookDto>>(hymnBooks);

			var notFoundBooksDto = ids.Except(foundBooksDto.Select(hb=>hb.Id)).ToList();

			if(notFoundBooksDto.Count == ids.Count) return NotFound(notFoundBooksDto);

			if (notFoundBooksDto.Any())
			{
				return Ok(new
				{
					Found = foundBooksDto,
					NotFound = notFoundBooksDto
				});
			}

			return Ok(foundBooksDto);
		}

		[HttpPost("create")]
		public async Task<IActionResult> Create(HymnBookCreateDto bookCreateDto)
		{
			if (bookCreateDto == null) return BadRequest("Hymn Book data is required.");

			if(!ModelState.IsValid) return BadRequest(ModelState);

			var bookExists = await _context.HymnBooks
								.AnyAsync(hb=>hb.Title == bookCreateDto.Title);

			if (bookExists) return Conflict($"Hymn Book: {bookCreateDto.Title} already Exists.");

			bookCreateDto.Slug = bookCreateDto.Title.ToLower().Replace(" ", "-");
			bookCreateDto.AddedTime = DateTime.Now;
			//Get Current User
			bookCreateDto.AddedBy = "admin";

			var createBook = _mapper.Map<HymnBookCreateDto, HymnBook>(bookCreateDto);

			try
			{
				await _context.HymnBooks.AddAsync(createBook);
				await _context.SaveChangesAsync();
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}

			var newBook = _mapper.Map<HymnBookDto>(createBook);

			return CreatedAtAction(nameof(GetHymnBookById), new { id = createBook.Id }, newBook);
		}

		[HttpPut("edit/{id}")]
		public async Task<IActionResult> Edit(int id, HymnBookDto bookDto)
		{

			if (bookDto == null) return BadRequest("Hymn Book data is required");

			if (!ModelState.IsValid) return BadRequest(ModelState);

			if (id != bookDto.Id) 
				return BadRequest($"Invalid Attempt! Hymn Books of Ids {id} and {bookDto.Id} are not the same");

			var bookInDb = await _context.HymnBooks.FindAsync(id);

			if (bookInDb == null) return NotFound($"Hymn Book with ID:{id} does not exist.");

			bookDto.Slug = bookDto.Title.ToLower().Replace(" ", "-");
			bookDto.AddedTime = DateTime.Now;
			bookDto.AddedBy = "admin";

			var bookExists = await _context.HymnBooks
								.Where(cb => cb.Id != id)
								.AnyAsync(cb=>cb.Slug == bookDto.Slug);

			if (bookExists) return Conflict($"Hymn Book: {bookDto.Title} already exists.");

			var editedBook = _mapper.Map(bookDto, bookInDb);

			try
			{
				_context.HymnBooks.Update(editedBook);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			var editedBookDto = _mapper.Map<HymnBookDto>(editedBook);

			return Ok(editedBookDto);

		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var hymnBook = await _context.HymnBooks.FindAsync(id);

			if (hymnBook == null) return NotFound($"Hymn Book with ID:{id} does not exist.");

			try
			{
				_context.HymnBooks.Remove(hymnBook);
				await _context.SaveChangesAsync();
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return NoContent();
		}

		//DELETE admin/api_hymnbooks/by_ids
		[HttpDelete("by_ids")]
		public async Task<IActionResult> DeleteHymnBooks(List<int> ids)
		{
			if (ids == null || ids.Count == 0) return BadRequest("Hymn Book Ids are required.");

			var deletedIds = new List<int>();
			var errors = new List<string>();

			foreach (int id in ids)
			{
				var hymnBook = await _context.HymnBooks.FindAsync(id);

				if (hymnBook == null)
				{
					errors.Add($"Chart with ID: {id} does not exist.");
					continue;
				}
				_context.HymnBooks.Remove(hymnBook);
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
