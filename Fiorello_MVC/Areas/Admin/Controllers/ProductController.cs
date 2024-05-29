using Fiorello_MVC.Data;
using Fiorello_MVC.Helpers;
using Fiorello_MVC.Models;
using Fiorello_MVC.Services.Interfaces;
using Fiorello_MVC.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;


namespace Fiorello_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly AppDbContext _appDbContext;
        public ProductController(IProductService productService, 
                                 ICategoryService categoryService, 
                                 IWebHostEnvironment webHostEnvironment,
                                 AppDbContext appDbContext)
        {
            _productService = productService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
            _appDbContext = appDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var products = await _productService.GetAllPaginateAsync(page, 4);

            var mappedData = _productService.GetMappedDatas(products);

            int totalPage = await GetPageCountAsync(4);

            Paginate<ProductVM> paginatedData = new(mappedData, totalPage, page);

            return View(paginatedData);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _productService.GetCountAsync();

            return (int)Math.Ceiling((decimal)productCount / take);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var existProduct = await _productService.GetByIdWithAllDatasAsync((int)id);

            if (existProduct is null) return NotFound();

            List<ProductImageVM> images = new List<ProductImageVM>();

            foreach(var item in existProduct.ProductImages)
            {
                images.Add(new ProductImageVM
                {
                    Image = item.Name,
                    isMain = item.isMain
                });
            }

            ProductDetailVM model = new()
            {
                Images = images,
                Name = existProduct.Name,
                Description = existProduct.Description,
                Price = existProduct.Price,
                Category = existProduct.Category.Name
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _categoryService.GetAllSelectedList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM product)
        {
            ViewBag.Categories = await _categoryService.GetAllSelectedList();

            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach(var item in product.Images)
            {
                if (!(item.Length / 1024 < 500))
                {
                    ModelState.AddModelError("Image", "Image max size is 200KB");
                    return View();
                }
            }

            List<ProductImage> images = new();

            foreach (var item in product.Images)
            {
                string fileName = Guid.NewGuid().ToString() + item.FileName;

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                using (FileStream stream = new(path, FileMode.Create))
                {
                    await item.CopyToAsync(stream);
                }

                bool existingProduct = await _productService.ExistAsync(item.Name);
                if (existingProduct)
                {
                    ModelState.AddModelError("Name", "Product with this title already exists");
                    return View();
                }

                images.Add(new ProductImage { Name = fileName });
                
            }

            images.FirstOrDefault().isMain = true;

            Product newProduct = new()
            {
                Name = product.Name,
                Description = product.Description,
                Price = decimal.Parse(product.Price.Replace(".",",")),
                CategoryId = product.CategoryId,
                ProductImages = images
            };
            await _productService.CreateAsync(newProduct);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existProduct = await _productService.GetByIdWithAllDatasAsync((int)id);

            if (existProduct is null) return NotFound();

            foreach(var item in existProduct.ProductImages)
            {

                string path = Path.Combine(_webHostEnvironment.WebRootPath, "img", item.Name);

                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }

            await _productService.DeleteAsync(existProduct);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Categories = await _categoryService.GetAllSelectedList();

            if (id is null) return BadRequest();

            var existProduct = await _productService.GetByIdWithAllDatasAsync((int)id);

            if (existProduct is null) return NotFound();

            ProductEditVM model = new()
            {
                Name = existProduct.Name,
                Description = existProduct.Description,
                Price = existProduct.Price.ToString(),
                CategoryId = existProduct.CategoryId,
                AvailableImages = existProduct.ProductImages.ToList(),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id,ProductEditVM product)
        {
            ViewBag.Categories = await _categoryService.GetAllSelectedList();

            if (id is null) return BadRequest();

            var existProduct = await _productService.GetByIdWithAllDatasAsync((int)id);

            if (existProduct is null) return NotFound();

            if(product.Images is not null)
            {
                foreach (var item in product.Images)
                {
                    string fileName = Guid.NewGuid().ToString() + item.FileName;
                    string newPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", fileName);

                    using FileStream stream = new(newPath, FileMode.Create);
                    await item.CopyToAsync(stream);

                    existProduct.ProductImages.Add(new ProductImage
                    {
                        Name = fileName,
                        ProductId = existProduct.Id
                    });
                }
            }
            product.Price ??= existProduct.Price.ToString();
    

            existProduct.Name = product.Name;
            existProduct.Description = product.Description;
            existProduct.Price = decimal.Parse(product.Price);
            existProduct.CategoryId = product.CategoryId;

            await _appDbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(int? id)
        {
            if (id is null) return BadRequest();

            var image = await _productService.GetImageByIdAsync((int)id);

            if (image is null) return NotFound();

            string oldPath = Path.Combine(_webHostEnvironment.WebRootPath, "img", image.Name);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }

            _appDbContext.ProductImages.Remove(image);
            await _appDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> MakeMain(int? id)
        {
            if (id is null) return BadRequest();

            var image = await _productService.GetImageByIdAsync((int)id);

            var product = await _productService.GetByIdAsync(image.ProductId);

            if (image is null) return NotFound();

            if (product.ProductImages.Count == 0) return NotFound();

            foreach (var item in product.ProductImages)
            {
                item.isMain = false;
                _appDbContext.ProductImages.Update(item);
            }

            image.isMain = true;
            _appDbContext.ProductImages.Update(image);

            await _appDbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
