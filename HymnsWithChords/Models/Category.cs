using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Models
{
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(100)]
		public string Name { get; set; }
        public virtual ICollection<Hymn> Hymns { get; set; }
        
    }
}
