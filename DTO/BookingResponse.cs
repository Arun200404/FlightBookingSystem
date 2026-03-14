namespace FlightBookingBackend.DTOs
{
    public class BookingResponse
    {
        public string BookingReference { get; set; } = string.Empty;
        public string FlightNumber { get; set; } = string.Empty;
        public string PassengerName { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public string BookingStatus { get; set; } = string.Empty;
        public decimal BaseFare { get; set; }
        public decimal FinalFare { get; set; }
    }
}
