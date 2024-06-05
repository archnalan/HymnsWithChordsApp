using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Dtos
{
	public class CategoryDto
	{
		[Key]
		public int Id { get; set; }
		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public int? ParentCategoryId { get; set; }//Nullable Main Category

		public int? Sorting { get; set; } //CategoryOrder

		[StringLength(255)]
		public string? CategorySlug { get; set; }
	}
}
