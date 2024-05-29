using Fiorello_MVC.Data;
using Fiorello_MVC.Models;
using Fiorello_MVC.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_MVC.Services
{
    public class ExpertService : IExpertService
    {
        private readonly AppDbContext _context;
        public ExpertService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Expert>> GetAllAsync()
        {
            return await _context.Experts.ToListAsync(); 
        }
    }
}
