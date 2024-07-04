using HymnsWithChords.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HymnsWithChords.Models
{
	public class ChordChart
	{
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int? ChordId { get; set; }

        [Range(1,24)]
        public int FretPosition { get; set; }

        [NotMapped]
        [FileExtensionValidation(new string[] {".png, .jpg, .gif"})]
        public IFormFile ChartUpload { get; set; }

		[StringLength(255)]
		public string? ChartAudioFilePath { get; set; }

		[StringLength(100)]
        public string? PositionDescription { get; set; }

        //Navigation prop
        [ForeignKey(nameof(ChordId))]
        public virtual Chord Chord { get; set; }
    }
}
