using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Models
{
	public class Page
	{
		public int Id { get; set; }

		[Required]
		[StringLength(15)]
		public string Title { get; set; }

		[StringLength(25)]
		public string Slug { get; set; }

		[Required]
		[StringLength(50)]
		public string Content { get; set; }
		public int Sorting { get; set; }
	}
}
