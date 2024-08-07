using HymnsWithChords.Dtos;
using HymnsWithChords.ServiceHandler;

namespace HymnsWithChords.Areas.Admin.Interfaces
{
	public interface IChordService
	{
		Task<ServiceResult<List<ChordEditDto>>> GetAllChordsAsync();
		Task<ServiceResult<List<ChordWithChartsDto>>> GetChordsWithChartsAsync();
		Task<ServiceResult<ChordEditDto>> GetChordByIdAsync(int id);
		Task<ServiceResult<ChordWithChartsDto>> GetChordWithChartsByIdAsync(int id);
		Task<ServiceResult<ChordEditDto>> CreateChordAsync(ChordCreateDto chordDto);
	}
}
