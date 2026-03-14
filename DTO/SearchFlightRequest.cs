using System.ComponentModel.DataAnnotations;

namespace FlightBookingBackend.DTO
{
    public class SearchFlightRequest
    {
        [Required]
        public required string Source { get; set; }

        [Required]
        public required string Destination { get; set; }

        [Required]
        public required DateTime Date { get; set; }
    }
}
