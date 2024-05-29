
using Fiorello_MVC.Services.Interfaces;
using Fiorello_MVC.ViewModels;
using Fiorello_MVC.ViewModels.Baskets;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Fiorello_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IBlogService _blogService;
        private readonly IExpertService _expertService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(ISliderService sliderService,
                                IBlogService blogService,
                                IExpertService expertService,
                                ICategoryService categoryService,
                                IProductService productService,
                                IHttpContextAccessor httpContextAccessor)
        {
            _sliderService = sliderService;
            _blogService = blogService;
            _expertService = expertService;
            _categoryService = categoryService;
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Blogs = await _blogService.GetAllAsync(3),
                Experts = await _expertService.GetAllAsync(),
                Categories = await _categoryService.GetAllAsync(),
                Products = await _productService.GetAllWithImagesAsync(),
            };
            return View(model); 
        }

        [HttpPost]
        public async Task<IActionResult> AddProductToBasket(int? id)
        {
            if (id is null) return BadRequest();

            var product = await _productService.GetByIdAsync(id);

            if (product is null) return NotFound();

            List<BasketVM> basketDatas = new();

            if(_httpContextAccessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_httpContextAccessor.HttpContext.Request.Cookies["basket"]);
            }

            var existBasket = basketDatas.FirstOrDefault(m => m.Id == id);

            if(existBasket is not null)
            {
                existBasket.Count++;
            }
            else
            {
                basketDatas.Add(new BasketVM
                {
                    Id = (int)id,
                    Price = product.Price,
                    Count = 1
                });
            }

            _httpContextAccessor.HttpContext.Response.Cookies.Append("basket",JsonConvert.SerializeObject(basketDatas));

            int count = basketDatas.Sum(m => m.Count);
            decimal total = basketDatas.Sum(m => m.Count * m.Price);

            return Ok(new{count,total});
        }
    }
}
