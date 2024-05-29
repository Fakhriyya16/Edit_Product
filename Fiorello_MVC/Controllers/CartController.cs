using Fiorello_MVC.Data;
using Fiorello_MVC.Models;
using Fiorello_MVC.ViewModels;
using Fiorello_MVC.ViewModels.Baskets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Fiorello_MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public CartController(AppDbContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;

        }
        public async Task<IActionResult> Index()
        {
            List<Product> test = new();

            var productsInBasket = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]).ToList();
            foreach(var item in productsInBasket)
            {
                int id = item.Id;
                var product = await _context.Products.Include(m=>m.Category).Include(m=>m.ProductImages).FirstOrDefaultAsync(m=>m.Id == id);
                test.Add(product);
            }
            List<CartVM> items = test.Select(product => new CartVM
            {
                Id = product.Id,
                Image = product.ProductImages.FirstOrDefault(m=>m.isMain).Name,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category.Name,
                Count = productsInBasket.Find(m=>m.Id == product.Id).Count,
                Price = product.Price,
            }).ToList();

            TotalBasketVM model = new()
            {
                Products = items,
                TotalPrice = productsInBasket.Sum(m => m.Price * m.Count),
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null) return BadRequest();

            List<BasketVM> basketDatas = new();

            if (_contextAccessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
            }

            basketDatas = basketDatas.Where(m=>m.Id != id).ToList();

            _contextAccessor.HttpContext.Response.Cookies.Append("basket",JsonConvert.SerializeObject(basketDatas));

            int totalCount = basketDatas.Sum(m => m.Count);
            decimal totalPrice = basketDatas.Sum(m => m.Count * m.Price);
            int basketCount = basketDatas.Count;

            return Ok(new {basketCount ,totalCount, totalPrice });
        }

        [HttpPost]
        public IActionResult DecreaseAmount(int? id)
        {
            if (id == null) return BadRequest();

            List<BasketVM> basketDatas = new();

            if (_contextAccessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
            }

            var item = basketDatas.FirstOrDefault(m=>m.Id == id);

            if (item == null) return BadRequest();
            item.Count--;
            int totalCount = basketDatas.Sum(m => m.Count);
            decimal totalPrice = basketDatas.Sum(m => m.Count * m.Price);
            int itemCount = item.Count;

            _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketDatas));
            return Ok(new { itemCount, totalCount, totalPrice });
        }


        [HttpPost]
        public IActionResult IncreaseAmount(int? id)
        {
            if (id == null) return BadRequest();

            List<BasketVM> basketDatas = new();

            if (_contextAccessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
            }

            var item = basketDatas.FirstOrDefault(m => m.Id == id);

            if (item == null) return BadRequest();
            item.Count++;
            int totalCount = basketDatas.Sum(m => m.Count);
            decimal totalPrice = basketDatas.Sum(m => m.Count * m.Price);
            int itemCount = item.Count;

            _contextAccessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketDatas));
            return Ok(new { itemCount, totalCount, totalPrice });
        }
    }
}
