using FlightBookingBackend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FareController : ControllerBase
    {
        private readonly IFareService _fareService;

        public FareController(IFareService fareService)
        {
            _fareService = fareService;
        }

        [Authorize]
        [HttpGet("get")]
        public IActionResult GetFare([FromQuery] string flightNumber)
        {
            var result = _fareService.CalculateFare(flightNumber);
            return Ok(result);
        }
    }
}
