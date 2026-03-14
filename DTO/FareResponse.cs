namespace FlightBookingBackend.DTOs
{
    public class FareResponse
    {
        public decimal BaseFare { get; set; }
        public decimal GST { get; set; }
        public decimal FinalFare { get; set; }
    }
}