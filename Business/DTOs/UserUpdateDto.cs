using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class UserUpdateDto
    {
        [Required]
        public int UserId { get; set; } // ✅ Necesario para identificar al usuario al editar

        [Required(ErrorMessage = "The First Name field is required.")]
        [StringLength(50, ErrorMessage = "The First Name must be at most 50 characters long.")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "The Last Name field is required.")]
        [StringLength(50, ErrorMessage = "The Last Name must be at most 50 characters long.")]
        public string LastName { get; set; } = null!;

        [StringLength(50, ErrorMessage = "The Phone Number must be at most 50 characters long.")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(255, ErrorMessage = "The Email must be at most 255 characters long.")]
        public string Email { get; set; } = null!;

        [StringLength(255, MinimumLength = 8, ErrorMessage = "The Password must be at least 8 characters long.")]
        public string? Password { get; set; } // Password no siempre se actualiza, por eso puede ser opcional

        [Required(ErrorMessage = "The Role field is required.")]
        public int RoleId { get; set; }

        public List<AddressDto> Addresses { get; set; } = new();
    }
}
