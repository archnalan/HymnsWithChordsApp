using HymnsWithChords.Models;

namespace HymnsWithChords.Dtos.CompositeDtos
{
	public class LineVerseCreateDto
	{
		public int LyricLineOrder { get; set; }
		public int VerseId { get; set; }
		public ICollection<LyricSegment>? LyricSegments { get; set; }
	}
}
