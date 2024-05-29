using Fiorello_MVC.Services;
using Fiorello_MVC.Services.Interfaces;
using Fiorello_MVC.ViewModels.Baskets;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Fiorello_MVC.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly ISettingService _settingService;
        private readonly IHttpContextAccessor _contextAccessor;
        public HeaderViewComponent(ISettingService settingService, IHttpContextAccessor contextAccessor)
        {
            _settingService = settingService;
            _contextAccessor = contextAccessor;

        }   
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BasketVM> basketDatas = new();

            if (_contextAccessor.HttpContext.Request.Cookies["basket"] is not null)
            {
                basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_contextAccessor.HttpContext.Request.Cookies["basket"]);
            }

            Dictionary<string,string> settings = await _settingService.GetAllAsync();

            HeaderVM response = new()
            {
                Settings = settings,
                BasketCount = basketDatas.Sum(m => m.Count),
                BasketTotalPrice = basketDatas.Sum(m => m.Price * m.Count),
            };

            return await Task.FromResult(View(response));
        }

        public class HeaderVM
        {
            public int BasketCount { get; set; }
            public decimal BasketTotalPrice { get; set; }
            public Dictionary<string,string> Settings { get; set; }
        }
    }
}
