using Fiorello_MVC.Models;

namespace Fiorello_MVC.ViewModels.Categories
{
    public class CategoryDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
