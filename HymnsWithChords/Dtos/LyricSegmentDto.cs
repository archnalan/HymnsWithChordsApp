using HymnsWithChords.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Dtos
{
	public class LyricSegmentDto
	{
		public int Id { get; set; }	
		public string Lyric { get; set; }
		public int LyricOrder { get; set; }			
		public string? LyricFilePath { get; set; }
		public int LyricLineId { get; set; }
	}
}
