using FlightBookingBackend.DTOs;

namespace FlightBookingBackend.Interfaces
{
    public interface IFareService
    {
        Task<FareResponse> CalculateFareAsync(string flightNumber);
    }
}