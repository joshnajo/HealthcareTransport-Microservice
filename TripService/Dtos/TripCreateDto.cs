using System.ComponentModel.DataAnnotations;

namespace TripService.Dtos
{
    public class TripCreateDto
    {
        [Required]
        public DateTime TripDate { get; set; }
        [Required]
        public string PickUp { get; set; } = string.Empty;
        [Required]
        public string Destination { get; set; } = string.Empty;
        [Required]
        public decimal Cost { get; set; }
        [Required]
        public string Vehicle { get; set; } = string.Empty;
    }
}