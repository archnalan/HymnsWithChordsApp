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
	public class Api_ChordsController : ControllerBase
	{
		private readonly HymnDbContext _context;
		private readonly IMapper _mapper;

        public Api_ChordsController(HymnDbContext context, IMapper mapper)
        {
			_context = context;
			_mapper = mapper;            
        }

		//GET admin/api_chords
		public async Task<IActionResult> Index()
		{
			var chords = await _context.Chords.OrderBy(c=>c.ChordName).ToListAsync();

			var chordsDto = chords.Select(_mapper.Map<Chord, ChordDto>).ToList();

			return Ok(chordsDto);
		}

		//GET admin/api_chrds/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetChordById(int id)
		{
			var chord = await _context.Chords.FindAsync(id);

			if (chord == null) return NotFound($"Chord with ID: {id} does not exist.");

			var chordDto = _mapper.Map<Chord, ChordDto>(chord);

			return Ok(chordDto);
		}

		//GET admin/api_chords/by_ids
		[HttpGet("by_ids")]
		public async Task<IActionResult> GetChordsById(int[] ids)
		{
			var chords = await _context.Chords
								.Where(ch=>ids.Contains(ch.Id))
								.ToListAsync();

			var chordsDto = _mapper.Map<List<ChordDto>>(chords);

			var foundIds = chords.Select(ch=>ch.Id).ToList();
			var notFoundIds = ids.Except(foundIds).ToList();

			if(notFoundIds.Count == ids.Length) return NotFound( new { NotFound = notFoundIds });

			if (notFoundIds.Any())
				return Ok(new {
					Found = chordsDto,
					NotFound = notFoundIds
				});

			return Ok(chordsDto);
		}

		//POST admin/api_chords/create
		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> Create(ChordDto chordDto)
		{
			if (chordDto == null) return BadRequest("Chord data is Required");

			if (!ModelState.IsValid) return BadRequest(ModelState);

			var chordExists = await _context.Chords
							.AnyAsync(ch => ch.ChordName == chordDto.ChordName);

			if (chordExists) return Conflict($"Chord: {chordDto.ChordName} already exists.");

			var chord = _mapper.Map<ChordDto, Chord>(chordDto);

			await _context.Chords.AddAsync(chord);
			await _context.SaveChangesAsync();

			var newChord = _mapper.Map<Chord, ChordDto>(chord);

			return CreatedAtAction(nameof(GetChordsById), new {id = chord.Id}, newChord);
		}

		//POST admin/api_chords/create_many
		[HttpPost]
		[Route("create_many")]
		public async Task<IActionResult> CreateChords(List<ChordDto> chordDtos)
		{
			if (chordDtos == null || chordDtos.Count == 0) return BadRequest("Chord Data is required.");

			var chordsToAdd = new List<Chord>();
			var createdChordsDto = new List<ChordDto>();

			var errors = new List<string>();

			foreach(var  chordDto in chordDtos)
			{
				if (!TryValidateModel(chordDto))
				{
					errors.Add($"Invalid data for Chord: {chordDto.ChordName}");
					continue;
				}

				var chordExists = await _context.Chords
								.AnyAsync(ch=>ch.ChordName == chordDto.ChordName);

				if (chordExists)
				{
					errors.Add($"Chord: {chordDto.ChordName} already exists");
					continue;
				}

				var chord = _mapper.Map<ChordDto, Chord>(chordDto);

				await _context.Chords.AddAsync(chord);

				chordsToAdd.Add(chord);
			}

			try
			{
				await _context.SaveChangesAsync();

				foreach(var chord in chordsToAdd)
				{
					var newChordDto = _mapper.Map<Chord, ChordDto>(chord);
					createdChordsDto.Add(newChordDto);
				}

			}
			catch (Exception ex)
			{
				errors.Add($"{ex.Message}");
			}

			if(errors.Count == chordDtos.Count) return BadRequest(errors);

			if (errors.Any())
			{
				return Ok(new
				{
					Created = createdChordsDto,
					Errors = errors
				});
			}

			return Ok(createdChordsDto);
		}

		//PUT admin/api_chords/edit/5
		[HttpPut]
		[Route("edit/{id}")]
		public async Task<IActionResult> Edit(int id, ChordDto chordDto)
		{
			if (chordDto == null) return BadRequest("Chord Data is required.");

			if (!ModelState.IsValid) return BadRequest(ModelState);

			var chordInDb = await _context.Chords.FindAsync(id);

			if(chordInDb == null) return BadRequest($"Chord with ID: {id} does not exist");

			var chordExits = await _context.Chords.Where(ch=>ch.Id != id)
									.AnyAsync(ch=>ch.ChordName == chordDto.ChordName);

			if (chordExits) return Conflict($"Chord: {chordDto.ChordName} already exists.");

			if (chordInDb.ChordName == chordDto.ChordName &&
			chordInDb.Difficulty == chordDto.Difficulty &&
			chordInDb.ChordChartFilePath == chordDto.ChordChartFilePath)
				return Ok(new 
				{ 
					Message = $"Chord: {chordDto.ChordName} is already up-to-date"
				});

			var chord = _mapper.Map(chordDto, chordInDb);

			_context.Chords.Update(chord);

			await _context.SaveChangesAsync();

			var newchordDto = _mapper.Map<Chord, ChordDto>(chord);

			return Ok(newchordDto);
		}

		//PUT admin/api_chords/edit_many
		[HttpPut]
		[Route("edit_many")]
		public async Task<IActionResult> EditChords(List<ChordDto> chordDtos)
		{
			if (chordDtos == null || chordDtos.Count == 0) return BadRequest("Chord data is required.");

			var chordsToEdit = new List<Chord>();
			var editedChordsDto = new List<ChordDto>();

			var errors = new List<string>();

			foreach(var chordDto in chordDtos)
			{
				if (!TryValidateModel(chordDto))
				{
					errors.Add($"Invalid data for Chord: {chordDto.ChordName}");
					continue;
				}
				var chordInDb = await _context.Chords.FindAsync(chordDto.Id);

				if(chordInDb == null)
				{
					errors.Add($"Chord with ID: {chordDto.Id} does not exist");
					continue;
				}

				var chordExists = await _context.Chords
								.Where(ch => ch.Id != chordDto.Id)
								.AnyAsync(ch => ch.ChordName == chordDto.ChordName);

				if(chordExists) 
				{
					errors.Add($"Chord: {chordDto.ChordName} already exists.");
					continue;
				}

				if (chordInDb.ChordName == chordDto.ChordName &&
					chordInDb.Difficulty == chordDto.Difficulty &&
					chordInDb.ChordChartFilePath == chordDto.ChordChartFilePath)
				{
					errors.Add($"Chord: {chordDto.ChordName} already up-to-date.");
					continue;
				}

				var chord = _mapper.Map(chordDto, chordInDb);

				_context.Chords.Update(chord);

				chordsToEdit.Add(chord);
			}

			try
			{
				await _context.SaveChangesAsync();

				foreach(var chord in chordsToEdit) 
				{
					var newChordDto = _mapper.Map<Chord, ChordDto>(chord);
					editedChordsDto.Add(newChordDto);
				}

			}
			catch (Exception ex)
			{
				errors.Add(ex.Message);
			}

			if(errors.Count == chordDtos.Count) return BadRequest(errors);

			if (errors.Any())
			{
				return Ok(new 
				{
					Edited = editedChordsDto,
					Errors = errors
				});
			}

			return Ok(editedChordsDto);
		}

		//DELETE admin/api_chords
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var chord = await _context.Chords.FindAsync(id);

			if (chord == null) return BadRequest($"Chord with ID: {id} does not exist.");

			try
			{
				_context.Chords.Remove(chord);

				await _context.SaveChangesAsync();
			}
			catch (DbUpdateException ex)
			{
				if (ex.InnerException is
					Microsoft.Data.SqlClient.SqlException sqlEx
					&& sqlEx.Number == 547)
					return BadRequest(sqlEx.Message);

				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

			return NoContent();
		}

		//DELETE admin/api_chords/by_ids
		[HttpDelete]
		[Route("by_ids")]
		public async Task<IActionResult> DeleteChords(List<int> ids)
		{
			if (ids == null || ids.Count == 0) return BadRequest("Chord Ids required.");

			var deletedIds = new List<int>();
			var errors = new List<string>();

			foreach(int id in ids)
			{
				var chord = await _context.Chords.FindAsync(id);

				if (chord == null)
				{
					errors.Add($"Chord with ID: {id} does not exist.");
					continue;
				}
				_context.Chords.Remove(chord);
				deletedIds.Add(id);
			}
			try
			{
				await _context.SaveChangesAsync();
			}
			catch(Exception ex)
			{
				errors.Add(ex.Message);
			}			

			if (errors.Count == ids.Count) return BadRequest(errors);

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
