using Microsoft.AspNetCore.Mvc;
using FlightBookingBackend.DTOs;
using FlightBookingBackend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace FlightBookingBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [Authorize(Roles = "User")]
        [HttpPost("book")]
        public IActionResult BookFlight([FromBody] BookingRequest request)
        {
            var userId = int.Parse(User.FindFirst("UserId")!.Value);
            var result = _bookingService.CreateBooking(request, userId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("search/{flightNumber}")]
        public IActionResult SearchBooking([FromQuery] string bookingReference)
        {
            var result = _bookingService.SearchBooking(bookingReference);
            if (result is null)
                return NotFound("Booking not found");

            return Ok(result);
        }
    }
}
