using FlightBookingBackend.DTO;
using FlightBookingBackend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FindFlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FindFlightsController(IFlightService flightService)
        {
            _flightService = flightService;
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
