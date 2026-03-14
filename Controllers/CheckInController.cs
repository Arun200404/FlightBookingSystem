using FlightBookingBackend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckinController : ControllerBase
    {
        private readonly ICheckinService _checkinService;

        public CheckinController(ICheckinService checkinService)
        {
            _checkinService = checkinService;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult Checkin([FromQuery] string bookingReference)
        {
            var result = _checkinService.DoCheckin(bookingReference);
            return Ok(result);
        }
    }
}
