using Fiorello_MVC.Models;
using Fiorello_MVC.Services.Interfaces;
using Fiorello_MVC.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC.Controllers
{
    public class BlogDetailController : Controller
    {
        private readonly IBlogService _blogService;
        public BlogDetailController(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IActionResult> Index(int id)
        {
            Blog model = await _blogService.GetByIdAsync(id);
            return View(model);
        }
    }
}
