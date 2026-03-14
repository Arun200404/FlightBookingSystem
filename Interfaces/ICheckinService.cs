using FlightBookingBackend.Models;

namespace FlightBookingBackend.Interfaces
{
    public interface ICheckinService
    {
        string DoCheckin(string bookingReference);
    }
}