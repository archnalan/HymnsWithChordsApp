using HymnsWithChords.Models;
using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Dtos
{
	public class HymnBookWithCategoriesDto
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Title { get; set; }

		[Required]
		[StringLength(50)]
		public string Slug { get; set; }

		[StringLength(255)]
		public string? SubTitle { get; set; }

		public string Description { get; set; }

		[Required]
		[StringLength(255)]
		public string Publisher { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime? PublicationDate { get; set; }

		[Required]
		[StringLength(13, MinimumLength = 10)]
		public string ISBN { get; set; }

		[StringLength(255)]
		public string Author { get; set; }

		[StringLength(50)]
		public string Edition { get; set; }

		[Required]
		[StringLength(50)]
		public string Language { get; set; }

		public string? AddedBy { get; set; }

		[Required]
		[DataType(DataType.Date)]
		public DateTime AddedTime { get; set; }

		public ICollection<Category> Categories { get; set; }
	}
}
