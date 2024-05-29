using Fiorello_MVC.Data;
using Fiorello_MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_MVC.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _appDbContext;
        public SettingService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<Dictionary<string, string>> GetAllAsync()
        {
            return await _appDbContext.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);
        }
        public async Task<string> GetPhoneAsync()
        {
            return await _appDbContext.Settings
                                      .Where(m => m.Key == "Phone")
                                      .Select(m => m.Value)
                                      .FirstOrDefaultAsync();
        }
    }
}
