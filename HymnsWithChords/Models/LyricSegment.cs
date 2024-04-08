using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HymnsWithChords.Models
{
	public class LyricSegment
	{
		[Key]
        public int Id { get; set; }

        [Required]
		[StringLength(200)]
        public string Words { get; set; }

		[StringLength(10)]
		[RegularExpression(@"^([A-G])(#|b|##|bb)?(\d+|m|maj|min|sus|aug|dim|add)?(/A-G?)?$", 
			ErrorMessage = "Invalid Chord Format!")]
		public string Chord { get; set; }

		[Required]
        public int HymnId { get; set; }

        [ForeignKey(nameof(HymnId))]
        public virtual Hymn Hymn { get; set; }
    }
}
