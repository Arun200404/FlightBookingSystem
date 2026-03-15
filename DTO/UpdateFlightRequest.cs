namespace FlightBookingBackend.DTO
{
    public class UpdateFlightRequest
    {
        public required string Source { get; set; }

        public required string Destination { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public decimal Fare { get; set; }

        public int AvailableSeats { get; set; }
    }
}
