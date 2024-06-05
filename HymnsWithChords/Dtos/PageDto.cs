using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Dtos
{
	public class PageDto
	{
		public int Id { get; set; }

		[Required]
		[StringLength(15, MinimumLength = 3,
			ErrorMessage = "The {0} must be atleast {2} characters")]
		public string Title { get; set; }

		[StringLength(25)]
		public string? Slug { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 5,
			ErrorMessage = "The {0} must be atleast {2} characters")]
		public string Content { get; set; }
		public int Sorting { get; set; }
	}
}
