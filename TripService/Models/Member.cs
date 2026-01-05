using System.ComponentModel.DataAnnotations;

namespace TripService.Models
{
    public class Member
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ExternalRefId { get; set; }

        [Required]
        public string MemberId { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        // Navigation property for related Trips
        // one member can have many trips
        public ICollection<Trip> Trips { get; set; } = new List<Trip>();
    }
}