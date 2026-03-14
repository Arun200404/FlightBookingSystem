namespace FlightBookingBackend.Models
{
    public class Flight
    {
        public int FlightId { get; set; }
        public required string FlightNumber { get; set; }
        public required string Source { get; set; }
        public required string Destination { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal Fare { get; set; }
        public int AvailableSeats { get; set; }
    }
}
