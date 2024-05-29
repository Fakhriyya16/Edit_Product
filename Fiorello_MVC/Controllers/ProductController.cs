using Fiorello_MVC.Models;
using Fiorello_MVC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index(int id)
        {
            Product product = await _productService.GetByIdWithAllDatasAsync(id);

			return View(product);
        }
    }
}
