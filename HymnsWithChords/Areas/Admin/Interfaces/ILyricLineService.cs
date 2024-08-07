using HymnsWithChords.Dtos;
using HymnsWithChords.Dtos.CompositeDtos;
using HymnsWithChords.ServiceHandler;

namespace HymnsWithChords.Areas.Admin.Interfaces
{
	public interface ILyricLineService
	{
		Task<ServiceResult<List<LyricLineDto>>> GetAllLyricLinesAsync();

		Task<ServiceResult<LyricLineDto>> GetLyricLineByIdAsync(int id);

		Task<ServiceResult<LyricLineDto>> CreateVerseLineAsync(LineVerseCreateDto verselineDto);

		Task<ServiceResult<LyricLineDto>> EditVerseLineAsync(int id, LyricLineDto verseLineDto);

		Task<ServiceResult<bool>> DeleteLyricLineAsync(int id);
	}
}
