using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class UserRegisterDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;

        [StringLength(50)]
        public string? PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; } = null!;

        [Required]
        public int RoleId { get; set; }

        public List<AddressDto> Addresses { get; set; } = new();
    }
}