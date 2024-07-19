using System.ComponentModel.DataAnnotations;

namespace HangFire.Domain.Entities
{
    public class House : EntityBase
    {
        [Required]
        public int Bed { get; set; }

        [Required]
        public int Bath { get; set; }

        [Required]
        public int SquareFeet { get; set; }

        [Required]
        public int ListingPrice { get; set; }

        public bool IsSold { get; set; }
    }
}
