
using Business.DTOs;
using Business.Entities;
using Business.Interfaces;
using Business.Mappers;
using Business.ViewModels;
using BuzzShopping.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Business.Services;

public class ProductService(AppDbContext context) : IProductService
{

    private readonly AppDbContext _context = context;

    public async Task<List<ProductDto>> GetFeaturedProductsAsync()
    {
        var products = await _context.Products
            .Where(p => p.Active && p.IsFeatured)
            .OrderByDescending(p => p.CreatedDate)
            .Take(12)
            .ToListAsync();

        return products.Select(ProductMapper.ToDto).ToList();
    }



    public async Task<ProductDto?> GetProductAsync(int id)
    {
        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.ProductId == id);

        return product != null ? ProductMapper.ToDto(product) : null;
    }


    public async Task<ProductPageViewModel> GetProductPageAsync(int? categoryId, string? search, int page, int productsByPages)
    {
        IQueryable<ProductEntity> query = _context.Products;

        query = query.Where(p => p.Active && p.IsFeatured);

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId);

        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));

        int totalProducts = await query.CountAsync();

        int totalPages = (int)Math.Ceiling((double)totalProducts / productsByPages);

        if (page < 1)
            page = 1;
        else if (page > totalPages && totalPages > 0)
            page = totalPages;

        List<ProductDto> productDtos = new();

        if (totalProducts > 0)
        {
            var products = await query
                .OrderBy(p => p.Name)
                .Skip((page - 1) * productsByPages)
                .Take(productsByPages)
                .ToListAsync();

            productDtos = products.Select(ProductMapper.ToDto).ToList();
        }     

        return new ProductPageViewModel
        {
            Products = productDtos,
            ActualPage = page,
            TotalPage = totalPages,
            CategoryIdSelected = categoryId,
            Search = search,
            ShowNoResultsMessage = totalProducts == 0,
            CategoryNameSelected = null 
        };
    }

}
