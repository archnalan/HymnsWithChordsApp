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
	public class VerseService : IVerseService
	{
		private readonly HymnDbContext _context;
		private readonly IMapper _mapper;

		public VerseService(HymnDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<ServiceResult<List<VerseDto>>> GetAllVersesAsync()
		{
			var verses = await _context.Verses
							.OrderBy(v => v.Number)
							.ToListAsync();

			var verseDtos = _mapper.Map<List<VerseDto>>(verses).ToList();

			return ServiceResult<List<VerseDto>>.Success(verseDtos);
		}

		public async Task<ServiceResult<VerseDto>> GetVerseByIdAsync(int id)
		{
			var verse = await _context.Verses.FindAsync(id);

			if (verse == null) return ServiceResult<VerseDto>.Failure(
				new NotFoundException($"Verse with ID:{id} does not exist."));

			var verseDto = _mapper.Map<VerseDto>(verse);

			return ServiceResult<VerseDto>.Success(verseDto);
		}		

		public async Task<ServiceResult<VerseDto>> CreateVerseAsync(VerseCreateDto verseDto)
		{
			if (verseDto == null) return ServiceResult<VerseDto>.Failure( new
				BadRequestException("Verse data is required."));		

			var hymnInDb = await _context.Hymns.FindAsync(verseDto.HymnId);

			if (hymnInDb == null)
				return ServiceResult<VerseDto>.Failure(new
				BadRequestException($"Parent hymn with ID: {verseDto.HymnId} does not exist"));

			var verseExists = await _context.Verses
								.Where(v => v.HymnId == verseDto.HymnId)
								.AnyAsync(v => v.Number == verseDto.Number);

			if (verseExists)
				return ServiceResult<VerseDto>.Failure(new
				ConflictException($"Verse Number {verseDto.Number} already exists for this hymn"));

			var verse = _mapper.Map<Verse>(verseDto);
			try
			{
				await _context.Verses.AddAsync(verse);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return ServiceResult<VerseDto>.Failure(new
				Exception(ex.Message));
			}

			var newVerseDto = _mapper.Map<VerseDto>(verse);

			return ServiceResult<VerseDto>.Success(newVerseDto);
		}

		public async Task<ServiceResult<VerseDto>> EditVerseAsync(int id, VerseDto verseEdit)
		{
			if (verseEdit == null) return ServiceResult<VerseDto>.Failure(new
				BadRequestException("Verse data is required."));
			
			if (id != verseEdit.Id)
				return ServiceResult<VerseDto>.Failure(new
				BadRequestException($"Invalid Attempt! Verses of IDs:{id} and {verseEdit.Id} are not the same"));

			var verseExists = await _context.Verses
								.Where(v => v.Id != id)
								.AnyAsync(v => v.HymnId == verseEdit.HymnId && v.Number == verseEdit.Number);

			if (verseExists)
			{
				return ServiceResult<VerseDto>.Failure(new
				ConflictException($"Verse Number {verseEdit.Number} already exists for this hymn"));
			}

			var verseInDb = await _context.Verses.FindAsync(id);

			if (verseInDb == null)
				return ServiceResult<VerseDto>.Failure(new
				BadRequestException($"Verse with ID: {id} does not exist"));

			var HymnInDb = await _context.Hymns.FindAsync(verseEdit.HymnId);

			if (HymnInDb == null)
				return ServiceResult<VerseDto>.Failure(new
				BadRequestException($"Parent hymn with ID: {verseEdit.HymnId} does not exist"));

			var verse = _mapper.Map(verseEdit, verseInDb);

			try
			{
				_context.Verses.Update(verse);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return ServiceResult<VerseDto>.Failure(new
				Exception (ex.Message));
			}

			var newVerseDto = _mapper.Map<VerseDto>(verse);

			return ServiceResult<VerseDto>.Success(newVerseDto);
		}

		public async Task<ServiceResult<bool>> DeleteVerseAsync(int id)
		{
			var verse = await _context.Verses.FindAsync(id);

			if (verse == null) return ServiceResult<bool>.Failure(new
				NotFoundException($"Verse with ID:{id} does not exist."));

			try
			{
				_context.Verses.Remove(verse);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				return ServiceResult<bool>.Failure(new
				Exception(ex.Message));
			}

			return ServiceResult<bool>.Success(true);
		}

	}
}
