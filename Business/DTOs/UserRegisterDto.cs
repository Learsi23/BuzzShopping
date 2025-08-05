using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "The First Name field is required.")]
        [StringLength(50, ErrorMessage = "The First Name must be at most 50 characters long.")]
        public string FirstName { get; set; } = null!;

        [StringLength(50, ErrorMessage = "The Last Name must be at most 50 characters long.")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(255, ErrorMessage = "The Email must be at most 255 characters long.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "The Password field is required.")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "The Password must be between 8 and 255 characters long.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "The Role field is required.")]
        public int RoleId { get; set; }
    }
}