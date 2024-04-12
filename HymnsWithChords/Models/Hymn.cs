using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HymnsWithChords.Models
{
	public class Hymn
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[Display(Name ="SDAH-")]
		public int Number { get; set; }

		[Required]
		[StringLength(100)]
		public string Title { get; set; }

		[NotMapped]
		public IFormFile? TextUpload { get; set; }

		[StringLength(255)]
        public string? TextFilePath { get; set; }

        [StringLength(100)]
        public string? WrittenDateRange { get; set; }

		[StringLength(100)]
		public string? WrittenBy { get; set; }

		[StringLength(255)]
		public string? History { get; set; }

		[StringLength(200)]
        public string AddedBy { get; set; }

		public DateTime AddedDate { get; set; }
        public int CategoryId { get; set; }       

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        public virtual ICollection<Verse> Verses { get; set; }
        public virtual ICollection<Bridge> Bridges { get; set; }
        public virtual ICollection<Chorus> Choruses { get; set; }

    }
}
