using System.ComponentModel.DataAnnotations;

namespace Fiorello_MVC.ViewModels.Blog
{
    public class BlogCreateVM
    {
        [Required(ErrorMessage = "input cannot be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "input cannot be empty")]
        public string Description { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
