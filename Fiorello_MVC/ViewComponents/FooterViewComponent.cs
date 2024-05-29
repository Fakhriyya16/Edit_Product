using Fiorello_MVC.Services;
using Fiorello_MVC.Services.Interfaces;
using Fiorello_MVC.ViewModels.Footer;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_MVC.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly ISocialService _socialService;
        private readonly ISettingService _settingService;
        public FooterViewComponent(ISocialService socialService, ISettingService settingService)
        {
            _socialService = socialService;
            _settingService = settingService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            SocialSettingVM model = new()
            {
                Socials = await _socialService.GetAllAsync(),
                Phone = await _settingService.GetPhoneAsync(),
            };
           
            return await Task.FromResult(View(model));
        }
    }
}
