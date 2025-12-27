using System.ComponentModel.DataAnnotations;

namespace MemberService.Dtos
{
    public class MemberCreateDto
    {
        // data annotations for validation
        [Required]
        public string MemberId { get; set; } = string.Empty;

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