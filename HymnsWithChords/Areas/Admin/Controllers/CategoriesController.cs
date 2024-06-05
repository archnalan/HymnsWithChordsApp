using HymnsWithChords.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HymnsWithChords.Areas.Admin.Controllers
{
	[Route("admin/[controller]")]
	[Area("Admin")]
	public class CategoriesController : Controller
	{
		private readonly HymnDbContext _context;

        public CategoriesController(HymnDbContext context)
        {
			_context = context;  
        }

		//GET /admin/categories
        public async Task<IActionResult> Index()
		{
			return View(await _context.Categories.OrderBy(c=>c.Sorting).ToListAsync());
		}

	}
}
