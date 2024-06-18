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
		
		public int? ChordId { get; set; }

		public int? LyricLineId{ get;  set; }	

		[ForeignKey(nameof(ChordId))]
		public virtual Chord Chord { get; set; }

		[ForeignKey(nameof(LyricLineId))]
		public virtual LyricLine LyricLine { get; set; }
	}
}
