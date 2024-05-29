using Fiorello_MVC.Models;

namespace Fiorello_MVC.Services.Interfaces
{
    public interface IExpertService
    {
        Task<IEnumerable<Expert>> GetAllAsync();
    }
}
