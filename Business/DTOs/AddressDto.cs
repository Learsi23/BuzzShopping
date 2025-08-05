using System.ComponentModel.DataAnnotations;

namespace Business.DTOs
{
    public class AddressDto
    {
        [Required(ErrorMessage = "El campo de la calle es requerido.")]
        [StringLength(100, ErrorMessage = "El campo de la calle no puede tener más de 100 caracteres.")]
        public string StreetAddress { get; set; } = null!;

        [StringLength(100, ErrorMessage = "La segunda línea de dirección no puede tener más de 100 caracteres.")]
        public string? StreetAddressLine2 { get; set; }

        [Required(ErrorMessage = "El campo de la ciudad es requerido.")]
        [StringLength(50, ErrorMessage = "El campo de la ciudad no puede tener más de 50 caracteres.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "El código postal es requerido.")]
        [StringLength(10, ErrorMessage = "El código postal no puede tener más de 10 caracteres.")]
        public string PostalCode { get; set; } = null!;

        [Required(ErrorMessage = "La región o estado es requerido.")]
        [StringLength(50, ErrorMessage = "La región o estado no puede tener más de 50 caracteres.")]
        public string Region { get; set; } = null!;

        [Required(ErrorMessage = "El país es requerido.")]
        [StringLength(50, ErrorMessage = "El país no puede tener más de 50 caracteres.")]
        public string Country { get; set; } = null!;

        public bool IsPrimary { get; set; }
    }
}