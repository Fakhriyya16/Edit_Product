namespace Fiorello_MVC.ViewModels.Products
{
    public class ProductDetailVM
    {
        public List<ProductImageVM> Images { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
}
