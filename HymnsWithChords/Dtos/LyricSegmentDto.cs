using HymnsWithChords.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Dtos
{
	public class LyricSegmentDto
	{
		public string Lyric { get; set; }

		public int LyricOrder { get; set; }			
		public string? LyricFilePath { get; set; }

		//Navigation prop for verse,chorus and bridge and chord
		public int? VerseId { get; set; }
		public int? ChorusId { get; set; }
		public int? BridgeId { get; set; }
		public int? ChordId { get; set; }
	}
}
