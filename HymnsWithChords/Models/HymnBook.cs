using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Models
{
	public class HymnBook
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Title { get; set; }
		
		public string Description { get; set; }

		[StringLength (255)]
        public string Author { get; set; }
       
        public ICollection<Category> Categories { get; set; }
    }
}
