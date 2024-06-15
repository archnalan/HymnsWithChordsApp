using HymnsWithChords.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Dtos
{
	public class HymnCreateDto
	{

		[Required]
		[Display(Name = "SDAH-")]
		public int Number { get; set; }

		[Required]
		[StringLength(100)]
		public string Title { get; set; }

		public string? Slug { get; set; }

		[StringLength(255)]
		public string? TextFilePath { get; set; }

		[StringLength(100)]
		public string? WrittenDateRange { get; set; }

		[StringLength(100)]
		public string? WrittenBy { get; set; }

		[StringLength(255)]
		public string? History { get; set; }

		[StringLength(200)]
		public string? AddedBy { get; set; }

		public DateTime AddedDate { get; set; }
		public int CategoryId { get; set; }
	}
}
