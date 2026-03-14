using System.ComponentModel.DataAnnotations;

namespace FlightBookingBackend.DTO
{
    public class UpdateFlightRequest
    {
        [Required]
        public required string Source { get; set; }

        [Required]
        public required string Destination { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Fare must be greater than zero")]
        public decimal Fare { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Available seats must be greater than zero")]
        public int AvailableSeats { get; set; }
    }
}
