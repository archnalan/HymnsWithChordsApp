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

		//Navigation prop for verse,chorus and bridge
        public int? VerseId { get; set; }
        public int? ChorusId { get; set; }
        public int? BridgeId { get; set; }


		[ForeignKey(nameof(VerseId))]
        public virtual Verse Verse { get; set; }
		
		[ForeignKey(nameof(ChorusId))]
        public virtual Chorus Chorus { get; set; }
		
		[ForeignKey(nameof(BridgeId))]
		public virtual Bridge Bridge { get; set; }

    }
}
