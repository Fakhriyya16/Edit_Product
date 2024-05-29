using System.ComponentModel.DataAnnotations;

namespace Fiorello_MVC.ViewModels.Blog
{
    public class BlogEditVM
    {
        [Required(ErrorMessage = "input cannot be empty")]
        public string Title { get; set; }
        [Required(ErrorMessage = "input cannot be empty")]
        public string Description { get; set; }
        public string Image {  get; set; }
        public IFormFile NewImage { get; set; }
    }
}
