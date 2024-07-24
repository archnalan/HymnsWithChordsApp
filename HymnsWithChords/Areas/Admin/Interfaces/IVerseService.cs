using HymnsWithChords.Dtos;
using HymnsWithChords.ServiceHandler;

namespace HymnsWithChords.Areas.Admin.Interfaces
{
	public interface IVerseService
	{
		Task<ServiceResult<List<VerseDto>>> GetAllVersesAsync();

		Task<ServiceResult<VerseDto>> GetVerseByIdAsync(int id);

		Task<ServiceResult<VerseDto>> CreateVerseAsync(VerseCreateDto verseDto);

		Task<ServiceResult<VerseDto>> EditVerseAsync(int id, VerseDto verseEdit);

		Task<ServiceResult<bool>> DeleteVerseAsync(int id);
	}
}
