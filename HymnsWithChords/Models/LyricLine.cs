using System.ComponentModel.DataAnnotations.Schema;

namespace HymnsWithChords.Models
{
	public class LyricLine
	{
        public int Id { get; set; }
        public int LyricLineOrder { get; set; }

        //Navigation prop for verse,chorus and bridge and chord
        public int? VerseId { get; set; }
		public int? ChorusId { get; set; }
		public int? BridgeId { get; set; }

		[ForeignKey(nameof(VerseId))]
		public virtual Verse Verse { get; set; }

		[ForeignKey(nameof(ChorusId))]
		public virtual Chorus Chorus { get; set; }

		[ForeignKey(nameof(BridgeId))]
		public virtual Bridge Bridge { get; set; }

		public ICollection<LyricSegment> LyricSegments { get; set; }
	}
}
