namespace FlightBookingBackend.DTO
{
    public class SearchFlightRequest
    {
        public required string Source { get; set; }
        public required string Destination { get; set; }
        public required DateTime Date { get; set; }
    }
}