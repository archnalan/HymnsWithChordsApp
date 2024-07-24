using HymnsWithChords.Areas.Admin.Interfaces;
using HymnsWithChords.Areas.Admin.LogicData;
using HymnsWithChords.Data;
using HymnsWithChords.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Transactions;

namespace HymnsWithChords.Areas.Admin.ApiControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VersesController : ControllerBase
	{
		private readonly IVerseService _verseService;
		private readonly ILyricLineService _lineService;
		private readonly ILyricSegment _segmentService;
		private readonly HymnDbContext _context;
        public VersesController(IVerseService verseService, ILyricLineService lineService, 
						ILyricSegment segmentService, 	HymnDbContext context)
        {
			_verseService = verseService;
			_lineService = lineService;
			_segmentService = segmentService;
			_context = context;
        }

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var verseResult = await _verseService.GetAllVersesAsync();

			if (verseResult.IsSuccess == false || verseResult.Data == null) 
			{ 
				return NotFound(); 
			}

			return Ok(verseResult.Data);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetVerseById(int id)
		{
			var verseResult = await _verseService.GetVerseByIdAsync(id);

			if(verseResult.IsSuccess == false) 
				return StatusCode(verseResult.StatusCode, new {message = verseResult.Error.Message});

			return Ok(verseResult.Data);
		}

		[HttpGet("by_ids")]
		public async Task<IActionResult> GetVersesByIds(List<int> ids)
		{
			if(ids.Count == 0 || !ids.Any())
			{
				return BadRequest("Verse Ids are required");
			}

			var foundVerses = new List<VerseDto>();
			var responseErrors = new List<(int id, string errorMessage)>();
			
			foreach(int id in ids)
			{
				var verseResult = await _verseService.GetVerseByIdAsync(id);

				if (verseResult.IsSuccess == false)
					responseErrors.Add((id, verseResult.Error.Message));

				foundVerses.Add(verseResult.Data);
			}

			if(responseErrors.Count == ids.Count)
			{
				return StatusCode(StatusCodes.Status404NotFound, new {Errors = responseErrors});
			}

			if (responseErrors.Any())
			{
				return Ok(new
				{
					Found = foundVerses,
					NotFound = responseErrors
				});
			}

			return Ok(foundVerses);
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateVerse(VerseCreateDto verseCreate, 
										List<LyricLineCreateDto> lineCreate,
										List<LyricSegmentCreateDto> segmentCreate)
		{
			if (!ModelState.IsValid) return BadRequest(ModelState);

			using(var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{

					var verseResult = await _verseService.CreateVerseAsync(verseCreate);

					if (verseResult.IsSuccess == false || verseResult.Data == null)
						return StatusCode(verseResult.StatusCode, new { message = verseResult.Error.Message });

					var createdVerse = verseResult.Data;

					int verseId = createdVerse.Id;

					var lines = new List<LyricLineDto>();
					var segments = new List<LyricSegmentDto>();
					var errorLines = new List<(int orderNumber, string errorMessage)>();
					var errorSegments = new List<(int orderNumber, string errorMessage)>();

					foreach (var line in lineCreate)
					{
						line.VerseId = verseId;
						var lineResult = await _lineService.CreateVerseLineAsync(line);

						if (lineResult.IsSuccess == false)
						{
							errorLines.Add((line.LyricLineOrder, lineResult.Error.Message));
							continue;
						}

						var createdLine = lineResult.Data;
						lines.Add(createdLine);

						var lineId = createdLine.Id;

						foreach (var segment in segmentCreate)
						{
							segment.LyricLineId = lineId;
							var segmentResult = await _segmentService.CreateSegmentAsync(segment);

							if (segmentResult.IsSuccess == false)
							{
								errorSegments.Add((segment.LyricOrder, segmentResult.Error.Message));
								continue;
							}

							var createdSegment = segmentResult.Data;
							segments.Add(createdSegment);
						}

					}

					if (errorLines.Any() || errorSegments.Any())
					{
						await transaction.RollbackAsync();
						return BadRequest(new { errorLines, errorSegments });
					}

					await transaction.CommitAsync();

					return Ok(new { createdVerse, lines, segments });
				}
				catch(Exception ex)
				{
					await transaction.RollbackAsync();
					return StatusCode(500, new { message = $"Error Creating Verse: {ex.Message}" });
				}
			}

		}
	}

	
}
