using Fiorello_MVC.Models;

namespace Fiorello_MVC.ViewModels.Products
{
    public class ProductEditVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<ProductImage> AvailableImages { get; set; }
    }
}
