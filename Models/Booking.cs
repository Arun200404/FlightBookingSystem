namespace FlightBookingBackend.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public required string BookingReference { get; set; }
        public int UserId { get; set; }
        public required string Email {get;set;}
        public required string FlightNumber { get; set; }
        public required string PassengerName { get; set; }
        public required string Gender { get; set; }
        public DateTime BookingDate { get; set; }
        public required string BookingStatus { get; set; }
        public decimal BaseFare {get;set;}
        public decimal FinalFare {get;set;}
    }
}