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
        public async Task<IActionResult> AddFlight([FromBody] FlightRequest request)
        {
            var result = await _flightService.AddFlightAsync(request);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("flights/update/{flightNumber}")]
        public async Task<IActionResult> UpdateFlight(string flightNumber, [FromBody] UpdateFlightRequest request)
        {
            var result = await _flightService.UpdateFlightAsync(flightNumber, request);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("flights/delete/{flightNumber}")]
        public async Task<IActionResult> DeleteFlight(string flightNumber)
        {
            var result = await _flightService.DeleteFlightAsync(flightNumber);
            return Ok(result);
        }

        [Authorize(Roles="Admin")]
        [HttpGet("flights/Running")]
        public async Task<IActionResult> GetAllFlights()
        {
            var result = await _flightService.GetAllFlightsAsync();
            return Ok(result);
        }
    }
}