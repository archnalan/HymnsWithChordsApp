using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HymnsWithChords.Models
{
	public class Verse
	{
		[Key]
		public int Id { get; set; }

		[Range(0, 12)]		
		public int Number { get; set; }

		public int HymnId { get; set; }

		[ForeignKey(nameof(HymnId))]
		public virtual Hymn Hymn { get; set; }

		public virtual ICollection<LyricSegment> LyricSegments { get; set; }
	}
}
