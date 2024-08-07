using HymnsWithChords.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Dtos
{
	public class ChordCreateDto
	{
		[StringLength(15)]
		[RegularExpression(@"^([A-G])(#|b|bb|##)?(m|maj|min|sus|aug|dim|add)?(2|4|5|6|7|9|11|13)?(/([A-G])(#|b|bb|##)?)?$",
		ErrorMessage = "Invalid Chord Format!")]
		public string ChordName { get; set; }

		[Range(1, 3)]
		public ChordDifficulty ChordDifficulty { get; set; }
		public int LineId { get; set; }
		public int SegmentOrderNo { get; set; }		
	}
}
