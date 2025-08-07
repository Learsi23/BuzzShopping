using Business.DTOs;
using Business.Entities;
using Business.Interfaces;
using Business.Mappers;
using BuzzShopping.Data;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class CategoryService(AppDbContext context) : ICategoryService
    {
        private readonly AppDbContext _context = context;

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return categories.Select(CategoryMapper.ToDto).ToList();
        }

        public Task CreateCategoryAsync(CategoryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoryAsync(int id)
        {
            throw new NotImplementedException();
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
