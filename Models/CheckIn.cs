namespace FlightBookingBackend.Models
{
    public class CheckIn
    {
        public int CheckInId { get; set; }
        public required string PassengerName {get;set;}
        public required string BookingReference { get; set; }
        public required string SeatNumber { get; set; }
        public required string CheckInReference { get; set; }
        public required string CheckInStatus { get; set; }
    }
}