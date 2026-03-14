using System.ComponentModel.DataAnnotations;

namespace FlightBookingBackend.DTOs
{
    public class BookingRequest
    {
        [Required]
        public required string FlightNumber { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Passenger name must be at least 2 characters")]
        [MaxLength(100)]
        public required string PassengerName { get; set; }

        [Required]
        [RegularExpression("^(Male|Female|Other)$", ErrorMessage = "Gender must be Male, Female, or Other")]
        public required string Gender { get; set; }
    }
}
