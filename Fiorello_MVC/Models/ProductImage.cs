namespace Fiorello_MVC.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isMain { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
