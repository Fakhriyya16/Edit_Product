using Fiorello_MVC.Data;
using Fiorello_MVC.Models;
using Fiorello_MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_MVC.Services
{
    public class SocialService : ISocialService
    {
        private readonly AppDbContext _appDbContext;
        public SocialService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<IEnumerable<Social>> GetAllAsync()
        {
            return await _appDbContext.Socials.ToListAsync();
        }
    }
}
