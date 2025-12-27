namespace MemberService.Dtos
{
    public class MemberReadDto
    {
        // we don't need data annotations here as this is only for reading data
        public int Id { get; set; }
        public string MemberId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string HealthPlanName { get; set; } = string.Empty;
    }
}