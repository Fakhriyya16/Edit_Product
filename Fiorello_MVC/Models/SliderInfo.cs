using System.ComponentModel.DataAnnotations;

namespace Fiorello_MVC.Models
{
    public class SliderInfo : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
