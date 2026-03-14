namespace FlightBookingBackend.DTOs
{
    public class BookingRequest
    {
        public required string FlightNumber { get; set; }
        public required string PassengerName { get; set; }
        public required string Gender { get; set; }
    }
}