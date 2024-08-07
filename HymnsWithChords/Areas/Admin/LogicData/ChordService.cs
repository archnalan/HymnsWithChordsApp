using AutoMapper;
using HymnsWithChords.Areas.Admin.Interfaces;
using HymnsWithChords.Data;
using HymnsWithChords.Dtos;
using HymnsWithChords.Models;
using HymnsWithChords.ServiceHandler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Web.Http.ModelBinding;

namespace HymnsWithChords.Areas.Admin.LogicData
{
	public class ChordService : IChordService
	{
		private readonly HymnDbContext _context;
		private readonly IMapper _mapper;

		public ChordService(HymnDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ServiceResult<List<ChordEditDto>>> GetAllChordsAsync()
		{
			var chords = await _context.Chords
								.OrderBy(c => c.ChordName)
								.ToListAsync();

			//var chordsDto = chords.Select(_mapper.Map<Chord, ChordEditDto>).ToList();

			var chordsDto = _mapper.Map<List<ChordEditDto>>(chords);

			return ServiceResult<List<ChordEditDto>>.Success(chordsDto);
		}

		public async Task<ServiceResult<List<ChordWithChartsDto>>> GetChordsWithChartsAsync()
		{
			var chords = await _context.Chords
								.OrderBy(c => c.ChordName)
								.Include(ch => ch.ChordCharts.OrderBy(cc => cc.FretPosition))
								.ToListAsync();

			var chordsDto = _mapper.Map<List<ChordWithChartsDto>>(chords);

			return ServiceResult<List<ChordWithChartsDto>>.Success(chordsDto);
		}

		public async Task<ServiceResult<ChordEditDto>> GetChordByIdAsync(int id)
		{
			var chord = await _context.Chords.FindAsync(id);

			if (chord == null) return ServiceResult<ChordEditDto>.Failure(new
				NotFoundException($"Chord with ID: {id} does not exist."));

			var chordDto = _mapper.Map<Chord, ChordEditDto>(chord);

			return ServiceResult<ChordEditDto>.Success(chordDto);
		}

		public async Task<ServiceResult<ChordWithChartsDto>> GetChordWithChartsByIdAsync(int id)
		{
			var chord = await _context.Chords
						.Include(ch => ch.ChordCharts.OrderBy(cc => cc.FretPosition))
						.FirstOrDefaultAsync(ch => ch.Id == id);

			if (chord == null) return ServiceResult<ChordWithChartsDto>.Failure(new
				NotFoundException($"Chord with ID: {id} does not exist."));

			var chordDto = _mapper.Map<Chord, ChordWithChartsDto>(chord);

			return ServiceResult<ChordWithChartsDto>.Success(chordDto);
		}

		public async Task<ServiceResult<ChordEditDto>> CreateChordAsync([FromBody] ChordCreateDto chordDto)
		{
			if (chordDto == null) return ServiceResult<ChordEditDto>.Failure(new
				BadRequestException("Chord data is Required"));		

			var chordExists = await _context.Chords
							.AnyAsync(ch => ch.ChordName == chordDto.ChordName);

			if (chordExists) return ServiceResult<ChordEditDto>.Failure(new
				ConflictException($"Chord: {chordDto.ChordName} already exists."));

			var chord = _mapper.Map<ChordCreateDto, Chord>(chordDto);

			try
			{
				await _context.Chords.AddAsync(chord);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return ServiceResult<ChordEditDto>.Failure(new Exception(ex.Message));
			}

			var newChord = _mapper.Map<Chord, ChordEditDto>(chord);

			return ServiceResult<ChordEditDto>.Success(newChord);
		}
	}
}
