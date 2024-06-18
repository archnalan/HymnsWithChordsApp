using HymnsWithChords.Dtos;

namespace HymnsWithChords.UI_Dtos
{
	public class LyricLineUIDto
	{
		public int LyricLineOrder { get; set; }
		public List<LyricSegmentUIDto> LyricSegments { get; set; }
	}
}
