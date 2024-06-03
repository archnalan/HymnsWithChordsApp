using HymnsWithChords.Data;
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
        public string Lyric { get; set; }		
		
		public int LyricOrder { get; set; }

		[NotMapped]
		[TextFileValidation(".txt", ".pdf")]
		public IFormFile? LyricUpload { get; set; }

		[StringLength(255)]
		public string? LyricFilePath { get; set; }

		//Navigation prop for verse,chorus and bridge and chord
		public int? VerseId { get; set; }
        public int? ChorusId { get; set; }
        public int? BridgeId { get; set; }
		public int? ChordId { get; set; }


		[ForeignKey(nameof(VerseId))]
        public virtual Verse Verse { get; set; }
		
		[ForeignKey(nameof(ChorusId))]
        public virtual Chorus Chorus { get; set; }
		
		[ForeignKey(nameof(BridgeId))]
		public virtual Bridge Bridge { get; set; }

		[ForeignKey(nameof(ChordId))]
		public virtual Chord Chord { get; set; }

	}
}
