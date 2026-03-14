using Microsoft.AspNetCore.Mvc;
using FlightBookingBackend.DTO;
using FlightBookingBackend.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FlightBookingBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public AdminController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("flights/add")]
        public IActionResult AddFlight([FromBody] FlightRequest request)
        {
            var result = _flightService.AddFlight(request);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("flights/update/{flightNumber}")]
        public IActionResult UpdateFlight(string flightNumber, [FromBody] UpdateFlightRequest request)
        {
            var result = _flightService.UpdateFlight(flightNumber, request);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("flights/delete/{flightNumber}")]
        public IActionResult DeleteFlight(string flightNumber)
        {
            var result = _flightService.DeleteFlight(flightNumber);
            return Ok(result);
        }
    }
}