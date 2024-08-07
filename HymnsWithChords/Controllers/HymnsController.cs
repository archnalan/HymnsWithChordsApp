using HymnsWithChords.Data;
using HymnsWithChords.UI_Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HymnsWithChords.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HymnsController : ControllerBase
	{
		private readonly HymnDbContext _context;

        public HymnsController(HymnDbContext context)
        {
            _context = context;
        }

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var hymns = await _context.Hymns
						.Include(h => h.Verses)
						.ThenInclude(v => v.LyricLines)
						.ThenInclude(ll => ll.LyricSegments)
						.ToListAsync();

			var sortedHymns = hymns
						.OrderBy(h => h.Number)
						.Select(h => new
						{
							Hymn = h,
							Verses = h.Verses.OrderBy(v => v.Number).Select(v => new
							{
								Verse = v,
								LyricLines = v.LyricLines.OrderBy(ll => ll.LyricLineOrder).Select(ll => new
								{
									LyricLine = ll,
									LyricSegments = ll.LyricSegments.OrderBy(ls => ls.LyricOrder).ToList()
								}).ToList()
							}).ToList()
						}).ToList();

			return Ok(sortedHymns);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetHymnById(int id)
		{
			var hymn = await _context.Hymns
						.Include(h => h.Verses)
						.ThenInclude(v => v.LyricLines)
						.ThenInclude(ll => ll.LyricSegments)
						.FirstOrDefaultAsync(h => h.Id == id);

			//hymnDto
			return Ok(hymn);
		}

		[HttpGet("chords")]
		public async Task<IActionResult> GetHymnsWithChords()
		{
			var hymns = await _context.Hymns
						.Include(h => h.Verses)
						.ThenInclude(v => v.LyricLines)
						.ThenInclude(ll => ll.LyricSegments)
						.ThenInclude(ls => ls.Chord)
						.ThenInclude(c => c.ChordCharts)
						.ToListAsync();

			var hymnWithChords = hymns
						.OrderBy(h => h.Number)
						.Select(h => new HymnChordsUIDto
						{
							Title = h.Title,
							Number = h.Number,
							Verses = h.Verses.OrderBy(v => v.Number).Select(v => new VerseUIDto
							{
								Number = v.Number,
								LyricLines = v.LyricLines.OrderBy(ll => ll.LyricLineOrder).Select(ll => new LyricLineUIDto
								{
									LyricLineOrder = ll.LyricLineOrder,
									LyricSegments = ll.LyricSegments.OrderBy(ls => ls.LyricOrder).Select(ls => new LyricSegmentUIDto
									{
										LyricOrder = ls.LyricOrder,
										Chord = ls.Chord == null ? null : new ChordUIDto
										{
											Id = ls.Chord.Id,
											ChordCharts = ls.Chord.ChordCharts.OrderBy(cc => cc.FretPosition).Select(cc => new ChordChartUIDto
											{
												Id = cc.Id,
												FretPosition = cc.FretPosition
											}).ToList()
										}
									}).ToList()
								}).ToList()
							}).ToList()
						}).ToList();

			return Ok(hymnWithChords);
		}


	}
}
