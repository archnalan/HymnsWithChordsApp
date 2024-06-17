using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Dtos
{
	public class VerseCreateDto
	{
		[Range(0, 12)]
		public int Number { get; set; }

		public int HymnId { get; set; }
	}
}
