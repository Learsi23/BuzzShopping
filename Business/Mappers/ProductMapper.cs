using Business.DTOs;
using Business.Entities;

namespace Business.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToDto(ProductEntity entity)
        {
            return new ProductDto
            {
                Code = entity.Code,
                Name = entity.Name,
                Model = entity.Model,
                Description = entity.Description,
                Price = entity.Price,
                SpecialOfferPrice = entity.SpecialOfferPrice,
                DiscountPercentage = entity.DiscountPercentage,
                HasSpecialOffer = entity.HasSpecialOffer,
                ProductUrl = entity.ProductUrl,
                CategoryId = entity.CategoryId,
                Stock = entity.Stock,
                Active = entity.Active,
                IsFeatured = entity.IsFeatured,
                Brand = entity.Brand
                
            };
        }
    }
}
