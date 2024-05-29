using Fiorello_MVC.Models;

namespace Fiorello_MVC.ViewModels.Footer
{
    public class SocialSettingVM
    {
        public IEnumerable<Social> Socials { get; set; }
        public string Phone { get; set; }
    }
}
