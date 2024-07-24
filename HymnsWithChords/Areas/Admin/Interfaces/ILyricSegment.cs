using HymnsWithChords.Dtos;
using HymnsWithChords.ServiceHandler;

namespace HymnsWithChords.Areas.Admin.Interfaces
{
	public interface ILyricSegment
	{
		Task<ServiceResult<List<LyricSegmentDto>>> GetAllSegmentsAsync();

		Task<ServiceResult<LyricSegmentDto>> GetSegmentByIdAsync(int id);

		Task<ServiceResult<LyricSegmentDto>> CreateSegmentAsync(LyricSegmentCreateDto segmentDto);

		Task<ServiceResult<LyricSegmentDto>> EditSegmentAsyc(int id, LyricSegmentDto segmentDto);

		Task<ServiceResult<bool>> DeleteSegmentAsync(int id);
	}
}
