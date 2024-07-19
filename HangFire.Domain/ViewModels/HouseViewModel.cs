using AutoMapper;
using HangFire.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HangFire.Domain.ViewModels
{
    [AutoMap(typeof(House), ReverseMap=true)]
    public class HouseViewModel : ViewModelBase
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
