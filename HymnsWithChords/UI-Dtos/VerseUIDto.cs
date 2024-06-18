using HymnsWithChords.Dtos;

namespace HymnsWithChords.UI_Dtos
{
	public class VerseUIDto
	{
		public int Number { get; set; }
		public List<LyricLineUIDto> LyricLines { get; set; }
	}
}
