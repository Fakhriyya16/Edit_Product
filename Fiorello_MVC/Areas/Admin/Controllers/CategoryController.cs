using Fiorello_MVC.Data;
using Fiorello_MVC.Helpers;
using Fiorello_MVC.Models;
using Fiorello_MVC.Services.Interfaces;
using Fiorello_MVC.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _appDbContext;
        public CategoryController(ICategoryService categoryService, AppDbContext appDbContext)
        {
            _categoryService = categoryService;
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            int take = 3;
            var categories = await _categoryService.GetAllWithProductAndPaginateCountAsync(page,take);
            int categoryCount = await _categoryService.CountAsync();
            int totalPage = (int)Math.Ceiling((decimal)categoryCount / take);
            Paginate<CategoryProductVM> model = new(categories, totalPage, page);
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool existCategory = await _categoryService.ExistAsync(category.Name);
            if (existCategory)
            {
                ModelState.AddModelError("Name", "Category with this name already exists");
                return View();
            }
            await _categoryService.CreateAsync(new Category { Name = category.Name });
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var category = await _categoryService.GetById((int)id);

            if (category == null) return NotFound();

            await _categoryService.DeleteAsync(category);

            if (category.SoftDeleted) return RedirectToAction("CategoryArchive", "Archive");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var category = await _categoryService.GetByIdWithProducts((int)id);
            if (category == null) { return NotFound(); }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> SetToArchieve(int? id)
        {
            if (id == null) return BadRequest();

            var category = await _categoryService.GetById((int)id);

            if (category == null)  return NotFound(); 

            category.SoftDeleted = true;

            await _appDbContext.SaveChangesAsync();
            return Ok(category);
        }
    }
}

