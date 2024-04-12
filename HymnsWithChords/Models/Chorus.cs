using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Models
{
	public class Chorus
	{
		[Key]
		public int Id { get; set; }

		public int HymnId { get; set; }

		[ForeignKey(nameof(HymnId))]
		public virtual Hymn Hymn { get; set; }

		public virtual ICollection<LyricSegment> LyricSegments { get; set; }
	}
}
