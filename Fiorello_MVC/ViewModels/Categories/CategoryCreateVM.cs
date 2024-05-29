using System.ComponentModel.DataAnnotations;

namespace Fiorello_MVC.ViewModels.Categories
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage ="input cannot be empty")]
        [StringLength(20)]
        public string Name { get; set; }
    }
}
