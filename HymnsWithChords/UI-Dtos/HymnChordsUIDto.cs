using HymnsWithChords.Dtos;

namespace HymnsWithChords.UI_Dtos
{
	public class HymnChordsUIDto
	{
		public string Title { get; set; }
		public int Number { get; set; }
		public List<VerseUIDto> Verses { get; set; }
	}
}
