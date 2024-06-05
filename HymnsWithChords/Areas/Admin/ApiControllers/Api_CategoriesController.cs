using AutoMapper;
using HymnsWithChords.Data;
using HymnsWithChords.Dtos;
using HymnsWithChords.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HymnsWithChords.Areas.Admin.ApiControllers
{
	[Route("admin/[controller]")]
	[ApiController]
	[Area("Admin")]
	public class Api_CategoriesController : ControllerBase
	{
		private readonly HymnDbContext _context;
		private readonly IMapper _mapper;

        public Api_CategoriesController(HymnDbContext context, IMapper mapper)
        {
            _context = context;
			_mapper = mapper;
        }

		//GET admin/api_categories
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			var categories = await _context.Categories
									.OrderBy(c => c.Sorting)
									.ToListAsync();
			var categoryDto = categories.Select(_mapper.Map<Category, CategoryDto>).ToList();

			return Ok(categoryDto);
		}

		//GET admin/api_categories/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCategoryByID(int id)
		{
			var category = await _context.Categories.FindAsync(id);

			if (category == null) return BadRequest($"Category of ID: {id} does not exist");

			var categoryDto = _mapper.Map<Category, CategoryDto>(category);

			return Ok(categoryDto);
		}

		//POST admin/api_categories/create

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> Create(CategoryDto categoryDto)
		{
			if (categoryDto == null) return BadRequest("Category Data is required");

			if (!ModelState.IsValid) return BadRequest(ModelState);

			var slugExists = await _context.Categories
				.AnyAsync(c => c.CategorySlug == categoryDto.CategorySlug);

			if (slugExists) return Conflict("Category Already Exists.");

			categoryDto.CategorySlug = categoryDto.Name.ToLower().Replace(" ", "-");
			categoryDto.Sorting = 100;

			var category = _mapper.Map<CategoryDto, Category>(categoryDto);

			await _context.Categories.AddAsync(category);
			await _context.SaveChangesAsync();

			var newCategory = _mapper.Map<Category, CategoryDto>(category);

			return CreatedAtAction(nameof(GetCategoryByID), new { id = category.Id}, newCategory);
		}

		//POST admin/api_categories/create-many
		[HttpPost]
		[Route("create-many")]
		public async Task<IActionResult> CreateMany([FromBody]CategoryDto[] categoryDtos)
		{
			if (categoryDtos == null || categoryDtos.Length == 0) 
				return BadRequest("Category Data is required");

			foreach(var categoryDto in categoryDtos)
			{
				if (!ModelState.IsValid) return BadRequest(ModelState);

				var slugExists = await _context.Categories
					.AnyAsync(c => c.CategorySlug == categoryDto.CategorySlug);

				if (slugExists) return Conflict($"Category: {categoryDto.Name} Already Exists.");

				categoryDto.CategorySlug = categoryDto.Name.ToLower().Replace(" ", "-");
				categoryDto.Sorting = 100;

				var category = _mapper.Map<CategoryDto, Category>(categoryDto);

				await _context.Categories.AddAsync(category);
			}

			await _context.SaveChangesAsync();			

			return Ok("Categories created successfully");
		}

		//PUT admin/api_categories/edit
		[HttpPut("{id}")]
		[Route("edit/{id}")]
		public async Task<IActionResult> Edit(int id, CategoryDto categoryDto)
		{
			if (categoryDto == null) return BadRequest("Category Data is required");

			if(!ModelState.IsValid) return BadRequest(ModelState);			

			var categoryInDb = await _context.Categories.FindAsync(id);

			if(categoryInDb == null) 
				return NotFound($"The category with ID: {id} does not exist.");

			categoryDto.CategorySlug = categoryDto.Name.ToLower().Replace(" ", "-");
			categoryDto.Sorting = 100;

			var slugExists = await _context.Categories
				.Where(s => s.Id != id) // because the database already has slug for this model
				.AnyAsync(c => c.CategorySlug == categoryDto.CategorySlug);

			if (slugExists) return Conflict("Category Already Exists.");

			var category =_mapper.Map(categoryDto, categoryInDb);

			_context.Categories.Update(category);

			await _context.SaveChangesAsync();

			var updatedCategoryDto = _mapper.Map<Category, CategoryDto>(categoryInDb);

			return Ok(updatedCategoryDto);
		}

		//PUT admin/api_categories/edit-many
		[HttpPut]
		[Route("edit-many")]
		public async Task<IActionResult> Edit(CategoryDto[] categoryDtos)
		{
			if (categoryDtos == null|| categoryDtos.Length == 0) 
				return BadRequest("Category Data is required");

			var updatedCategories = new List<CategoryDto>();
			var notFoundResult = new List<string>();
			var conflictResult = new List<string>();

			foreach(var categoryDto in categoryDtos)
			{
				if (!ModelState.IsValid) return BadRequest(ModelState);

				var categoryInDb = await _context.Categories.FindAsync(categoryDto.Id);

				if (categoryInDb == null) 
				{
					notFoundResult.Add($"{categoryDto.Name} does not exist");
					continue;				
				} 

				categoryDto.CategorySlug = categoryDto.Name.ToLower().Replace(" ", "-");
				categoryDto.Sorting = 100;

				var slugExists = await _context.Categories
					.Where(s => s.Id != categoryDto.Id) // because the database already has slug for this model
					.AnyAsync(c => c.CategorySlug == categoryDto.CategorySlug);

				if (slugExists) 
				{
					conflictResult.Add($"Category: {categoryDto.Name} Already Exists.");
					continue;
				} 

				var category = _mapper.Map(categoryDto, categoryInDb);

				_context.Categories.Update(category);

				updatedCategories.Add(categoryDto);				

			}			

			await _context.SaveChangesAsync();	
			
			if(notFoundResult.Any() || conflictResult.Any())
			{
				return BadRequest( new { NotFound = notFoundResult, 
										Conflict = conflictResult });
			}

			return Ok(updatedCategories);
		}

		//DELETE admin/api_categories/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var category = await _context.Categories.FindAsync(id);

			if (category == null) 
				return NotFound($"The category with ID: {id} does not exist.");
			try
			{
				_context.Categories.Remove(category);

				await _context.SaveChangesAsync();
			}
			catch(Exception ex)
			{
				return BadRequest($"An Error occured on attempt to delete category: {ex.Message}");
			}
			
			return NoContent();
		}

		//POST admin/api_categories/re-assign
		//method that takes an array of categoryIds and assigns it to a new category Id
		

    }
}
