using System.ComponentModel.DataAnnotations;

namespace MemberService.Models
{
    public class Member
    { 
        // Entity Framework thinks it's a key because of the name Id. PK is mandatory no neeed to mention all these by default 
        [Key]
        [Required] 
        public int Id { get; set; } // Primary Key

        [Required]
        public string MemberId { get; set; } = string.Empty; // Unique Member Identifier

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        [Required]
        public string HealthPlanName { get; set; } = string.Empty;
    }
} 