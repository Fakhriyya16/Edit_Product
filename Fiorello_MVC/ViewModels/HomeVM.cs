using Fiorello_MVC.Models;
using Fiorello_MVC.ViewModels;

namespace Fiorello_MVC.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Models.Blog> Blogs { get; set; }
        public IEnumerable<Expert> Experts { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
