using HymnsWithChords.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Dtos
{
	public class ChordCreateDto
	{		
		public string ChordName { get; set; }

		[Range(1, 3)]
		public ChordDifficulty Difficulty { get; set; }		
		public int? ChordChartId { get; set; }	
		public string? ChordAudioFilePath { get; set; }	

	}
}
