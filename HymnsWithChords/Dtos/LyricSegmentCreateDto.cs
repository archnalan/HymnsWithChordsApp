namespace HymnsWithChords.Dtos
{
	public class LyricSegmentCreateDto
	{
		public string Lyric { get; set; }
		public int LyricOrder { get; set; }
		public string? LyricFilePath { get; set; }
		public int LyricLineId { get; set; }
	}
}
