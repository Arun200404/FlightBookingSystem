using System.ComponentModel.DataAnnotations;

namespace FlightBookingBackend.DTO
{
    public class AdminLoginRequest
    {
        [Required]
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }
    }
}
