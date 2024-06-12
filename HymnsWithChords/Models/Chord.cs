using HymnsWithChords.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Models
{
	public class Chord
	{
		public int Id { get; set; }

		[StringLength(15)]
		[RegularExpression(@"^([A-G])(#|b|##|bb)?(\d+|m|maj|min|sus|aug|dim|add)?(/A-G?)?$",
			ErrorMessage = "Invalid Chord Format!")]
		public string ChordName { get; set; }

		[Range(1, 3)]
		public ChordDifficulty Difficulty { get; set; } 			

		//Will be assigned path of guitar chord position 1
		[StringLength(255)]
		public string? ChordAudioFilePath { get; set; }

		
		public virtual ICollection<ChordChart> ChordCharts { get; set; }

		public virtual ICollection<LyricSegment> LyricSegments { get; set; }

        
        public Chord()
        {
            ChordCharts = new HashSet<ChordChart>();
			LyricSegments = new HashSet<LyricSegment>();
        }
    }
	
}
