using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class RoleDto
    {
        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(50, ErrorMessage = "The Name must be at most 50 characters long.")]
        public string Name { get; set; } = null!;

        [StringLength(255, ErrorMessage = "The Description must be at most 255 characters long.")]
        public string? Description { get; set; }
    }
}