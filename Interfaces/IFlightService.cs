using FlightBookingBackend.DTO;
using FlightBookingBackend.Models;

namespace FlightBookingBackend.Interfaces
{
    public interface IFlightService
    {
        Task<string> AddFlightAsync(FlightRequest request);
        Task<List<Flight>> GetAllFlightsAsync();
        Task<List<Flight>> SearchFlightsAsync(string source, string destination, DateTime date);
        Task<string> UpdateFlightAsync(string flightNumber, UpdateFlightRequest request);
        Task<string> DeleteFlightAsync(string flightNumber);
    }
}
