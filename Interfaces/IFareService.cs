using FlightBookingBackend.DTOs;

namespace FlightBookingBackend.Interfaces
{
    public interface IFareService
    {
        FareResponse CalculateFare(string flightNumber);
    }
}