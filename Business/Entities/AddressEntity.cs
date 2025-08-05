using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Entities
{
    public class AddressEntity
    {
        [Key]
        public int AddressId { get; set; }

        // Campos de dirección mejorados
        [Required(ErrorMessage = "The Street Address field is required.")]
        [StringLength(100, ErrorMessage = "The Street Address must be at most 100 characters long.")]
        public string StreetAddress { get; set; } = null!;

        [StringLength(100, ErrorMessage = "The Street Address Line 2 must be at most 100 characters long.")]
        public string? StreetAddressLine2 { get; set; } // Campo opcional para un segundo renglón

        [Required(ErrorMessage = "The City field is required.")]
        [StringLength(50, ErrorMessage = "The City must be at most 50 characters long.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "The Postal Code field is required.")]
        [StringLength(10, ErrorMessage = "The Postal Code must be at most 10 characters long.")]
        public string PostalCode { get; set; } = null!;

        [Required(ErrorMessage = "The Region/State field is required.")]
        [StringLength(50, ErrorMessage = "The Region/State must be at most 50 characters long.")]
        public string Region { get; set; } = null!;

        [Required(ErrorMessage = "The Country field is required.")]
        [StringLength(50, ErrorMessage = "The Country must be at most 50 characters long.")]
        public string Country { get; set; } = null!;

        // Campo para identificar si es la dirección principal
        public bool IsPrimary { get; set; }

        // Relación con el usuario
        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public UserEntity User { get; set; } = null!;
    }
}