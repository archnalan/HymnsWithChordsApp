using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Models
{
	public class Bridge
	{
		[Key]
		public int Id { get; set; }		
		public int HymnId { get; set; }

		public string Title { get; set; }

		[ForeignKey(nameof(HymnId))]
		public virtual Hymn Hymn { get; set; }

		public virtual ICollection<LyricLine> LyricLines { get; set; }
	}
}
