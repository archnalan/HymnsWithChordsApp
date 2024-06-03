using HymnsWithChords.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Models
{
	public class Chord
	{
		public int Id { get; set; }

		[StringLength(15)]
		[RegularExpression(@"^([A-G])(#|b|##|bb)?(\d+|m|maj|min|sus|aug|dim|add)?(/A-G?)?$",
			ErrorMessage = "Invalid Chord Format!")]
		public string ChordName { get; set; }
		public ChordDifficulty Difficulty { get; set; } = ChordDifficulty.Easy;

		[NotMapped]
		public IFormFile ChordChart { get; set; }

		[StringLength(255)]
		public string? ChordChartFilePath { get; set; }

		public virtual ICollection<LyricSegment> LyricSegments { get; set; }

	}
}
