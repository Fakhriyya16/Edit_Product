using Fiorello_MVC.Models;

namespace Fiorello_MVC.Services.Interfaces
{
    public interface ISocialService
    {
        Task<IEnumerable<Social>> GetAllAsync();
    }
}
