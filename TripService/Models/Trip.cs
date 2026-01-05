using System.ComponentModel.DataAnnotations;

namespace TripService.Models
{
    public class Trip
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime TripDate { get; set; }

        [Required]
        public string PickUp { get; set; } = string.Empty;

        [Required]
        public string Destination { get; set; } = string.Empty;

        [Required]
        public decimal Cost { get; set; }

        //lyft uber etc
        [Required]
        public string Vehicle { get; set; } = string.Empty;

        // Foreign key to Member
        [Required]
        public string MemberId { get; set; } = string.Empty;

        // Navigation property
        public Member Member { get; set; } = null!;
    }
}