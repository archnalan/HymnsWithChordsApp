using HymnsWithChords.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using HymnsWithChords.Interfaces;

namespace HymnsWithChords.Areas.Admin.ViewModels
{
	public class HymnViewModel
	{
		[Required]
		[Display(Name = "SDAH-")]
		public int Number { get; set; }

		[Required]
		[StringLength(100)]
		public string Title { get; set; }		

		[StringLength(100)]
		public string? WrittenDateRange { get; set; }

		[StringLength(100)]
		public string? WrittenBy { get; set; }

		[StringLength(255)]
		public string? History { get; set; }

		[StringLength(200)]
		public string AddedBy { get; set; }

		public DateTime AddedDate { get; set; }
		public int CategoryId { get; set; }

		//Category Dropdown
		public IEnumerable<SelectListItem> Categories { get; set; }		

	}
}
