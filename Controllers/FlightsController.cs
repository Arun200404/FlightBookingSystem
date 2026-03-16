using Microsoft.AspNetCore.Mvc;
using FlightBookingBackend.DTO;
using FlightBookingBackend.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FlightBookingBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [Authorize]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllFlights()
        {
            var result = await _flightService.GetAllFlightsAsync();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("search")]
        public async Task<IActionResult> SearchFlights([FromQuery] SearchFlightRequest request)
        {
            var result = await _flightService.SearchFlightsAsync(request.Source, request.Destination, request.Date);

            if (result.Count == 0)
                return NotFound("No flights found for the given search criteria");

            return Ok(result);
        }
    }
}
