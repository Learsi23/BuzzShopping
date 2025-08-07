using Business.DTOs;
using Business.Entities;

namespace Business.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToDto(CategoryEntity entity)
        {
            return new CategoryDto
            {
                Name = entity.Name,
                Description = entity.Description,
                Slug = entity.Slug,
                IsActive = entity.IsActive,
                // ImageFile is not mapped because it's only used for upload (form), not for reading
            };
        }

        public static CategoryEntity ToEntity(CategoryDto dto)
        {
            return new CategoryEntity
            {
                Name = dto.Name,
                Description = dto.Description,
                Slug = dto.Slug,
                IsActive = dto.IsActive,
                // Image must be set manually afterward if an image is uploaded
            };
        }

        public static void UpdateEntity(CategoryEntity entity, CategoryDto dto)
        {
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.Slug = dto.Slug;
            entity.IsActive = dto.IsActive;
            // If you want to update the image, handle it in the service method (e.g., using ImageFile)
        }
    }
}
