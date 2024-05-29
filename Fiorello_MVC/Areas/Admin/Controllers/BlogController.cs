using Fiorello_MVC.Data;
using Fiorello_MVC.Models;
using Fiorello_MVC.Services.Interfaces;
using Fiorello_MVC.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Fiorello_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _context;
        public BlogController(IBlogService blogService, IWebHostEnvironment webHostEnvironment, AppDbContext appDbContext)
        {
            _blogService = blogService;
            _webHostEnvironment = webHostEnvironment;
            _context = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Blog> blog = await _blogService.GetAllAsync();
            IEnumerable<BlogTableVM> model = blog.Select(m => new BlogTableVM
            {
                Id = m.Id,
                Title = m.Title,
                CreatedDate = m.CreatedDate.ToString("dd.MM.yyyy"),
                Description = m.Description,
            });
            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM blog)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }

            foreach (var item in blog.Images)
            {
                if (!(item.Length / 1024 < 200))
                {
                    ModelState.AddModelError("Image", "Image max size is 200KB");
                    return View();
                }
            }

            foreach (var item in blog.Images)
            {
                string fileName = Guid.NewGuid().ToString() + item.FileName;

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }

                bool existingBlog = await _blogService.ExistBlogAsync(blog.Title);
                if (existingBlog)
                {
                    ModelState.AddModelError("Title", "Blog with this title already exists");
                    return View();
                }
                await _blogService.Create(new Blog { Title = blog.Title, Description = blog.Description, Image = fileName });
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if(id == null) { return BadRequest(); }
            var blog = await _blogService.GetByIdAsync((int)id);
            if(blog == null) { return NotFound(); }

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", blog.Image);

            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            await _blogService.DeleteAsync(blog);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            Blog blog = await _blogService.GetByIdAsync((int)id);
            BlogDetailVM model = new() { Title =  blog.Title, 
                                         Image = blog.Image,
                                         Description = blog.Description, 
                                         CreatedDate = blog.CreatedDate.ToString("dd.MM.yyyy")
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) { return BadRequest(); }
            var blog = await _blogService.GetByIdAsync((int)id);
            if (blog == null) { return NotFound(); }

            return View(new BlogEditVM { Title = blog.Title, Description = blog.Description, Image = blog.Image});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BlogEditVM model)
        {
            if (id == null) { return BadRequest(); }
            var blog = await _blogService.GetByIdAsync((int)id);
            if (blog == null) { return NotFound(); }
            if (model.NewImage is null) return RedirectToAction(nameof(Index));

            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", blog.Image);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + model.NewImage.FileName;

            string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await model.NewImage.CopyToAsync(stream);
            }

            blog.Image = fileName;
            blog.Description = model.Description;
            blog.Title = model.Title;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
