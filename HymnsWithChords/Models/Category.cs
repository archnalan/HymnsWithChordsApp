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

		[ForeignKey(nameof(ParentCategoryId))]
		public virtual Category ParentCategory { get; set; }

		public ICollection<Category> SubCategories { get; set; }
        public virtual ICollection<Hymn> Hymns { get; set; }
        
    }
}
