using HymnsWithChords.Interfaces;

namespace HymnsWithChords.Dtos
{
	public class ChordDto
	{
		public int Id { get; set; }		
		public string ChordName { get; set; }
		public ChordDifficulty Difficulty { get; set; }		
		public string? ChordChartFilePath { get; set; }	

	}
}
