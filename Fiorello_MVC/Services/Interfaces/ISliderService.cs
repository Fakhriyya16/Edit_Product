using Fiorello_MVC.Models;

namespace Fiorello_MVC.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<Slider>> GetAllAsync();
        Task<SliderInfo> GetSliderInfoAsync();
    }
}
