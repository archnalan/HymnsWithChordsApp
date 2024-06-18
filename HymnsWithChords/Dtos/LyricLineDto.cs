using HymnsWithChords.Models;

namespace HymnsWithChords.Dtos
{
	public class LyricLineDto
	{
		public int Id { get; set; }
		public int LyricLineOrder { get; set; }
		public int? VerseId { get; set; }
		public int? ChorusId { get; set; }
		public int? BridgeId { get; set; }
		public ICollection<LyricSegment>? LyricSegments { get; set; }
	}
}
