using Fiorello_MVC.Data;
using Fiorello_MVC.Models;
using Fiorello_MVC.Services.Interfaces;
using Fiorello_MVC.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_MVC.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public Task<bool> ExistAsync(string name)
        {
            return _context.Categories.AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<CategoryProductVM>> GetAllWithProductAndPaginateCountAsync(int page, int take)
        {
            IEnumerable<Category> categories = await _context.Categories.Include(m => m.Products)
                                                                        .Skip((page-1)*take)
                                                                        .Take(take)
                                                                        .OrderByDescending(m => m.Id)
                                                                        .ToListAsync();
            return categories.Select(m => new CategoryProductVM
            {
                Id = m.Id,
                CategoryName = m.Name,
                CreatedDate = m.CreatedDate.ToString("dd.MM.yyyy"),
                ProductCount = m.Products.Count
            }
            );
        }

        public async Task<Category> GetById(int id)
        {
            return await _context.Categories.IgnoreQueryFilters().FirstOrDefaultAsync(m=>m.Id == id);
        }

        public async Task<CategoryDetailVM> GetByIdWithProducts(int id)
        {
            Category category = await _context.Categories.Include(m=>m.Products).ThenInclude(m=>m.ProductImages).FirstOrDefaultAsync(m=>m.Id == id);
            return new CategoryDetailVM
            {
                Id = category.Id,
                Name = category.Name,
                CreatedDate = category.CreatedDate.ToString("dd.MM.yyyy"),
                Products = category.Products.ToList(),
            };
        }

        public async Task<IEnumerable<CategoryArchiveVM>> GetAllArchiveAsync(int page,int take)
        {
            IEnumerable<Category> categories = await _context.Categories
                                                                .IgnoreQueryFilters()
                                                                .Where(m => m.SoftDeleted)
                                                                .Skip((page-1)*take)
                                                                .Take(take)
                                                                .OrderByDescending(m => m.Id)
                                                                .ToListAsync();

            return categories.Select(m => new CategoryArchiveVM
            {
                Id = m.Id,
                Name = m.Name,
                CreatedDate = m.CreatedDate.ToString("dd.MM.yyyy")
            });
        }

        public async Task<int> CountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<int> ArchiveCountAsync()
        {
            return await _context.Categories
                                 .IgnoreQueryFilters()
                                 .Where(m=>m.SoftDeleted)
                                 .CountAsync();     
        }

        public async Task<SelectList> GetAllSelectedList()
        {
            var categories = await _context.Categories.ToListAsync();

            return new SelectList(categories, "Id", "Name");
        }
    }
}
