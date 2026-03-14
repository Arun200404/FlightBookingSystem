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
        public IActionResult GetAllFlights()
        {
            var result = _flightService.GetAllFlights();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("search")]
        public IActionResult SearchFlights([FromQuery] SearchFlightRequest request)
        {
            var result = _flightService.SearchFlights(request.Source, request.Destination, request.Date);

            if (result.Count == 0)
                return NotFound("No flights found for the given search criteria");

            return Ok(result);
        }
    }
}
