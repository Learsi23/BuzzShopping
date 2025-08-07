

using Business.DTOs;
using Business.Entities;
using Business.ViewModels;

namespace Business.Interfaces
{
    public  interface IProductService
    {
        Task<ProductDto> GetProductAsync(int id);

        Task<List<ProductDto>> GetFeaturedProductsAsync();

        Task<ProductPageViewModel> GetProductPageAsync(int? catgoryId, string? busqueda, int page, int productsByPages);

    }
}
