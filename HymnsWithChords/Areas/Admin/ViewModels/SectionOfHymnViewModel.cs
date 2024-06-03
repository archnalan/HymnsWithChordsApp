using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Areas.Admin.ViewModels
{
	public class SectionOfHymnViewModel
	{
		public int HymnId { get; set; }

		//Verse Section
		[Range(0, 12)]
		public int? VerseNumber { get; set; }

		[Display(Name = "Verse")]
		public string DisplayNumber => $"Verse {VerseNumber}";

		[Required]
		[StringLength(200)]
		public string? VerseLyric { get; set; }
		public int? VerseLyricOrder { get; set; }

		//Bridge Section
		[Range(0, 12)]
		public int? BridgeNumber { get; set; }
		[Required]
		[StringLength(200)]
		public string? BridgeLyric { get; set; }
		public int? BridgeLyricOrder { get; set; }

		//Chorus Section
		[Range(0, 12)]
		public int? ChorusNumber { get; set; }
		[Required]
		[StringLength(200)]
		public string? ChorusLyric { get; set; }
		public int? ChorusLyricOrder { get; set; }
	}
}
