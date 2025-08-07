

using Business.DTOs;
using Business.Entities;
using Business.Interfaces;
using BuzzShopping.Data;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class CategoryService(AppDbContext context) : ICategoryService
    {


        private readonly AppDbContext _context = context;


        public Task CreateCategoryAsync(CategoryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryEntity>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public Task<CategoryDto> GetCategoryByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(int id, CategoryDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
