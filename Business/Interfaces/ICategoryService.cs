using Business.DTOs;
using Business.Entities;

namespace Business.Interfaces
{
    public interface ICategoryService
    {

        Task<List<CategoryEntity>> GetCategoriesAsync();
        Task<CategoryDto> GetCategoryByIdAsync(int id);
        Task CreateCategoryAsync(CategoryDto dto);
        Task UpdateCategoryAsync(int id, CategoryDto dto);
        Task DeleteCategoryAsync(int id);
    }
}
