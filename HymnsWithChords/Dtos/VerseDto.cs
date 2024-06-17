using System.ComponentModel.DataAnnotations;

namespace HymnsWithChords.Dtos
{
	public class VerseDto
	{
		public int Id { get; set; }

		[Range(0, 12)]
		public int Number { get; set; }

		public int HymnId { get; set; }
	}
}
