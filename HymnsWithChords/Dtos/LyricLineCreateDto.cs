using HymnsWithChords.Models;
using System.ComponentModel.DataAnnotations;


namespace HymnsWithChords.Dtos
{
	public class LyricLineCreateDto:IValidatableObject
	{
		public int LyricLineOrder { get; set; }
		public int? VerseId { get; set; }
		public int? ChorusId { get; set; }
		public int? BridgeId { get; set; }
		public ICollection<LyricSegment>? LyricSegments { get; set; }

		
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			int noIdCount = 0;

			if (VerseId == null) noIdCount++;

			if (BridgeId == null)  noIdCount++; 

			if (ChorusId == null) noIdCount++;

			if(noIdCount == 3)
			{
				 yield return new ValidationResult(
					"One of VerseId, ChorusId, or BridgeId must be provided",
					new[] { nameof(VerseId), nameof(BridgeId), nameof(ChorusId) });
			}
			if (noIdCount > 2)
			{
				yield return new ValidationResult(
				   "Only one of VerseId, ChorusId, or BridgeId must be provided",
				   new[] { nameof(VerseId), nameof(BridgeId), nameof(ChorusId) });
			}
		}
	}
}
