namespace TripService.Dtos
{
    public class TripReadDto
    {
        public int Id { get; set; }
        public DateTime TripDate { get; set; }
        public string PickUp { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public string Vehicle { get; set; } = string.Empty;
        public string MemberId { get; set; } = string.Empty;
    }
}