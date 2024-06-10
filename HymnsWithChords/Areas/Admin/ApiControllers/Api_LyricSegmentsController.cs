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
	public class Api_LyricSegmentsController : ControllerBase
	{
		private readonly HymnDbContext _context;
		private readonly IMapper _mapper;

        public Api_LyricSegmentsController(HymnDbContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }

		//GET admin/api_lyricsegments
		public async Task<IActionResult> Index()
		{
			var lyricSegments = await _context.LyricSegments
										.Include(ls=>ls.ChordId)
										.ToListAsync();
			var lyricSegmentsDto = lyricSegments.Select(_mapper.Map<LyricSegment, LyricSegmentDto>)
												.ToList();
			return Ok(lyricSegmentsDto);
		}

		//GET admin/api_lyricSegments/5
		[HttpGet("{id}")]
		public async Task<IActionResult> GetLyricSegmentByID(int id)
		{
			var lyricSegment = await _context.LyricSegments.FindAsync(id);

			if(lyricSegment == null) return NotFound($"Lyrics with ID: {id} do not exist.");

			var lyricSegmentDto = _mapper.Map<LyricSegment, LyricSegmentDto>(lyricSegment);

			return Ok(lyricSegmentDto);
		}
    }
}
