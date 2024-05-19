using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HymnsWithChords.Models
{
	public class UserFeedback
	{
		[Key]
        public int FeedbackId { get; set; }

        [Required]
        public int HymnId { get; set; }

        [Required]
        [StringLength(255)]
        public string UserComment { get; set; }

        [Required]
		[StringLength(50)]
		public string UserId { get; set; }

        public DateTime DateSubmitted { get; set; }

        //Current status of the user suggestions
        [Required]
        [EnumDataType(typeof(FeedbackStatus))]
        public FeedbackStatus Status { get; set; }

        [ForeignKey(nameof(HymnId))]
        public Hymn Hymn { get; set; }
    }

    public enum FeedbackStatus 
    {
        Pending,
        UnderReview,
        Addressed
    }
}
