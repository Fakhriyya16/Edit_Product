using Fiorello_MVC.Data;
using Fiorello_MVC.Models;
using Fiorello_MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_MVC.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        public SliderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<SliderInfo> GetSliderInfoAsync()
        {
            return await _context.SliderInfos.FirstOrDefaultAsync();
        }
    }
}
