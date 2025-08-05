using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Entities
{
    public class UserEntity
    {
        public UserEntity()
        {
            Orders = [];
            Addresses = [];
        }

        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(50, ErrorMessage = "The Name must be at most 50 characters long.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "The Email field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [StringLength(255, ErrorMessage = "The Email must be at most 255 characters long.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "The Phone Number field is required.")]
        [StringLength(15, ErrorMessage = "The Phone Number must be at most 15 characters long.")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "The Password field is required.")]
        [StringLength(255, ErrorMessage = "The Password must be at most 255 characters long.")]
        public string Password { get; set; } = null!;

        // Campos de administración y auditoría
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? LastLoginDate { get; set; }

        // Relación con el rol
        [Required(ErrorMessage = "The Role field is required.")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public RoleEntity Role { get; set; } = null!;

        // Navegación a colecciones relacionadas
        public ICollection<OrderEntity> Orders { get; set; }

        [InverseProperty("User")]
        public ICollection<AddressEntity> Addresses { get; set; }
    }
}