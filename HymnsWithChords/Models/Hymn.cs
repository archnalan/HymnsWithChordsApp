using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HymnsWithChords.Models
{
	public class Hymn
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public int Number { get; set; }

		[Required]
		[StringLength(100)]
		public string Title { get; set; }

		[NotMapped]
		public IFormFile? TextUpload { get; set; }
        public string? WrittenDateRange { get; set; }
        public string? WrittenBy { get; set; }
        public string? History { get; set; }
        public string? AddedBy { get; set; }
		public DateTime AddedDate { get; set; }
        public int CategoryId { get; set; }       

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        public virtual ICollection<LyricSegment> LyricSegments { get; set; }

    }
}
