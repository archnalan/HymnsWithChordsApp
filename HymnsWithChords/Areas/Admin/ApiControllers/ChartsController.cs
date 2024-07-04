using HymnsWithChords.Areas.Admin.Interfaces;
using HymnsWithChords.Dtos;
using HymnsWithChords.Dtos.WithUploads;
using HymnsWithChords.UI_Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HymnsWithChords.Areas.Admin.ApiControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChartsController : ControllerBase
	{
		private readonly IChordChartService _chartService;

        public ChartsController(IChordChartService chartService)
        {
			_chartService = chartService;            
        }

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var charts = await _chartService.GetAllChordChartsAsync();

			if (charts == null) return BadRequest("No Charts Added yet!");

			return Ok(charts);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetChordChartById(int id)
		{
			var (chartDto, errorResult )= await _chartService.GetChordChartByIdAsync(id);

			if (errorResult != null) return BadRequest(errorResult);

			return Ok(chartDto);
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateChordChart([FromForm] ChartCreateDto chartCreateDto)
		{
			if (chartCreateDto == null) return BadRequest("Chord Chart data is required.");			

			if (!ModelState.IsValid) return BadRequest(ModelState);			

			 var ( newChartDto, errorResults) = await _chartService.CreateChordChartAsync(chartCreateDto);

			if (errorResults != null) return BadRequest(errorResults);

			return CreatedAtAction(nameof(GetChordChartById), new { id = newChartDto.Id}, newChartDto);
		}

		[HttpPut("edit")]
		public async Task<IActionResult> EditChordChart([FromForm]ChartEditDto chartEditDto)
		{
			if (chartEditDto == null) return BadRequest("Chord Chart data is required.");

			if (!ModelState.IsValid) return BadRequest(ModelState);

			var (editedChartDto, errorResult) = await _chartService.EditChordChartAsync(chartEditDto);

			if (errorResult != null) return BadRequest(errorResult);

			return Ok(editedChartDto);
		}
	}
}
