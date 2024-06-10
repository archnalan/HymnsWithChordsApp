using AutoMapper;
using HymnsWithChords.Models;
using HymnsWithChords.Data;
using HymnsWithChords.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;

namespace HymnsWithChords.Areas.Admin.ApiControllers
{
	[Route("admin/[controller]")]
	[ApiController]
	[Area("Admin")]
	public class Api_ChordChartsController : ControllerBase
	{
		private readonly HymnDbContext _context;
		private readonly IMapper _mapper;

        public Api_ChordChartsController(HymnDbContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }

		//GET admin/api_chordcharts
		public async Task<IActionResult> Index()
		{
			var chordCharts = await _context.ChordsCharts
							.OrderBy(ch=>ch.FretPosition)
							.ToListAsync();
			var chordChartsDto = chordCharts.Select(_mapper.Map<ChordChart, ChordChartEditDto>)
											.ToList();
			return Ok(chordChartsDto);
		}

		//GET admin/api_chordcharts/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetChordChartById(int id)
		{
			var chordChart = await _context.ChordsCharts.FindAsync(id);

			if (chordChart == null) return BadRequest($"Chord with ID: {id} does not exist");

			var chordChartDto = _mapper.Map<ChordChart, ChordChartEditDto>(chordChart);

			return Ok(chordChartDto);
		}

		//GET admin/api_chordchats/by_ids
		[HttpGet]
		[Route("by_ids")]
		public async Task<IActionResult> GetChordChartsById(List<int> ids)
		{
			if (ids == null || ids.Any() == false) 
					return BadRequest("Chord Chart Ids are required.");

			var chords = await _context.ChordsCharts
						.Where(ch => ids.Contains(ch.Id))
						.ToListAsync();

			var foundChartsDto = _mapper.Map<List<ChordChartEditDto>>(chords)
										.ToList();

			var notFoundChartsDto = ids.Except(chords.Select(ch=>ch.Id)).ToList();

			if(notFoundChartsDto.Count == ids.Count) return BadRequest(notFoundChartsDto);

			if (notFoundChartsDto.Any())
			{
				return Ok(new
				{
					Found = foundChartsDto,
					NotFound = notFoundChartsDto
				});
			}

			return Ok(foundChartsDto);

		}

		//POST admin/api_chordcharts/create
		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> Create([FromBody]ChordChartCreateDto chordChartDto)
		{
			if (chordChartDto == null) return BadRequest("Chord Chart data is required.");

			if (!ModelState.IsValid) return BadRequest(ModelState);

			var chordExists = _context.ChordsCharts
							.Exists(ch => ch.FilePath == chordChartDto.FilePath);

			if (chordExists) return Conflict($"Chord with file Path: {chordChartDto.FilePath} already exists.");

			var chordChart = _mapper.Map<ChordChartCreateDto, ChordChart>(chordChartDto);

			try
			{
				_context.ChordsCharts.Add(chordChart);
				await _context.SaveChangesAsync();

			}catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}

			var newChordChartDto = _mapper.Map<ChordChart, ChordChartCreateDto>(chordChart);

			return CreatedAtAction(nameof(GetChordChartById), new { id = chordChart.Id }, newChordChartDto);

		}

		//POST admin/api_chordchart/create_many
		[HttpPost]
		[Route("create_many")]
		public async Task<IActionResult> CreateChordCharts(List<ChordChartCreateDto> chordChartDtos)
		{
			if (chordChartDtos == null || chordChartDtos.Count == 0) 
				return BadRequest("Chord Chart Data is required");

			var chartsToAdd = new List<ChordChart>();
			
			//To show Id in the output
			var createdCharts = new List<ChordChartEditDto>();

			var errors = new List<string>();
			foreach(var chordChartDto in chordChartDtos)
			{
				if (!TryValidateModel(chordChartDto))
				{
					errors.Add($"Invalid data for: {chordChartDto.FilePath}");
					continue;
				}

				var chartExists = await _context.ChordsCharts
								.AnyAsync(ch => ch.FilePath == chordChartDto.FilePath);

				if (chartExists)
				{
					errors.Add($"Chord chart: {chordChartDto.FilePath} already exists.");
				}

				var chart = _mapper.Map<ChordChartCreateDto, ChordChart>(chordChartDto);			

				chartsToAdd.Add(chart);
			}

			if(chartsToAdd.Count> 0)
			{
				try
				{
					await _context.ChordsCharts.AddRangeAsync(chartsToAdd);

					await _context.SaveChangesAsync();

					foreach (var chart in chartsToAdd)
					{
						var newchartDto = _mapper.Map<ChordChart, ChordChartEditDto>(chart);
						createdCharts.Add(newchartDto);
					}

				}
				catch (Exception ex)
				{
					errors.Add(ex.Message);
				}
			}			

			if (errors.Count == chordChartDtos.Count) return BadRequest(errors);

			if (errors.Any())
			{
				return Ok(new
				{
					Created = createdCharts,
					Errors = errors
				});
			}

			return Ok(createdCharts);
		}

    }
}
