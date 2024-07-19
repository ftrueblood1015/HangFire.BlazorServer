using System.ComponentModel.DataAnnotations;

namespace HangFire.Domain.Entities
{
    public class EntityBase
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
