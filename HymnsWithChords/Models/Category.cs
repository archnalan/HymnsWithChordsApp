using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HymnsWithChords.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		public int? ParentCategoryId { get; set; }//Nullable Main Category

		public int? Sorting {  get; set; } //CategoryOrder

		[StringLength(255)]
		public string? CategorySlug { get; set; }

		public int? HymnBookId { get; set; }

		[ForeignKey(nameof(ParentCategoryId))]
		public virtual Category ParentCategory { get; set; }

		[ForeignKey(nameof(HymnBookId))]
		public virtual HymnBook HymnBook { get; set; }

		public ICollection<Category> SubCategories { get; set; }
        public virtual ICollection<Hymn> Hymns { get; set; }
        
    }
}
