using FlightBookingBackend.Models;

namespace FlightBookingBackend.Interfaces
{
    public interface IFlightRepository
    {
        Task AddFlightAsync(Flight flight);
        Task<List<Flight>> GetAllFlightsAsync();
        Task<Flight?> GetFlightByNumberAsync(string flightNumber);
        Task<List<Flight>> SearchFlightsAsync(string source, string destination, DateTime date);
        Task UpdateFlightAsync(Flight flight);
        Task DeleteFlightAsync(Flight flight);
    }
}
