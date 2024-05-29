using Fiorello_MVC.Models;
using Fiorello_MVC.ViewModels.Products;

namespace Fiorello_MVC.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllWithImagesAsync();
        Task<Product> GetByIdWithAllDatasAsync(int? id);
        Task<Product> GetByIdAsync(int? id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetAllPaginateAsync(int page, int take);
        IEnumerable<ProductVM> GetMappedDatas(IEnumerable<Product> products);
        Task<int> GetCountAsync();
        Task<bool> ExistAsync(string name);
        Task CreateAsync(Product product);
        Task DeleteAsync(Product product);
        Task<ProductImage> GetImageByIdAsync(int id);
        Task<List<ProductImage>> GetAllImagesAsync();
    }
}
