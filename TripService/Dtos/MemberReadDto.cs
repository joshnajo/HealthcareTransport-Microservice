namespace TripService.Dtos
{
    public class MemberReadDto
    {
        public int Id { get; set; }
        public string MemberId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}