using System.ComponentModel.DataAnnotations;

namespace Business.Entities
{
    public class RoleEntity
    {
        [Key]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "The field Name is required")]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        // Campo para una descripción del rol
        [StringLength(255)]
        public string? Description { get; set; }
    }
}