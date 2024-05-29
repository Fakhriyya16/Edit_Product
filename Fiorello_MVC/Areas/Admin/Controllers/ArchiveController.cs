using Fiorello_MVC.Data;
using Fiorello_MVC.Helpers;
using Fiorello_MVC.Services.Interfaces;
using Fiorello_MVC.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArchiveController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _appDbContext;
        public ArchiveController(ICategoryService categoryService, AppDbContext appDbContext)
        {
            _categoryService = categoryService;
            _appDbContext = appDbContext;
        }
        public async Task<IActionResult> CategoryArchive(int page = 1)
        {
            int take = 3;
            var archivedData = await _categoryService.GetAllArchiveAsync(page,take);
            int dataCount = await _categoryService.ArchiveCountAsync();
            int totalPage = (int)Math.Ceiling((decimal)dataCount / take);
            Paginate<CategoryArchiveVM> model = new(archivedData, totalPage, page);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Archive(int? id)
        {
            if (id == null) return BadRequest();

            var category = await _categoryService.GetById((int)id);

            if (category == null) return NotFound();

            category.SoftDeleted = !category.SoftDeleted;

            await _appDbContext.SaveChangesAsync();
            return Ok(category);
        }
    }
}
