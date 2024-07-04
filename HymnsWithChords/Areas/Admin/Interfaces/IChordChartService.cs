using HymnsWithChords.Dtos.WithUploads;
using HymnsWithChords.Models;

namespace HymnsWithChords.Areas.Admin.Interfaces
{
	public interface IChordChartService	
	{
		Task<List<ChartEditDto>> GetAllChordChartsAsync();
		Task<(ChartWithUploadsDto, string)> GetChordChartByIdAsync(int id);
		//Task<(ChartWithParentChordDto, string)> GetChordChartWithChordByIdAsync(int id);
		Task<(ChartEditDto, string)> CreateChordChartAsync(ChartCreateDto chordChartDto);
		Task<(ChartEditDto, string)> EditChordChartAsync(ChartEditDto chordChartDto);
		//Task<(bool, string)> DeleteChordChartByIdAsync(int id);
	}
}
