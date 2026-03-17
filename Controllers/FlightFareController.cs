using FlightBookingBackend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightFareController : ControllerBase
    {
        private readonly IFareService _fareService;

        public FlightFareController(IFareService fareService)
        {
            _fareService = fareService;
        }

        [Authorize]
        [HttpGet("get")]
        public async Task<IActionResult> GetFare([FromQuery] string flightNumber)
        {
            var result = await _fareService.CalculateFareAsync(flightNumber);
            return Ok(result);
        }
    }
}
