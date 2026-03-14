using FlightBookingBackend.Models;

namespace FlightBookingBackend.Interfaces
{
    public interface IFlightRepository
    {
        void AddFlight(Flight flight);
        List<Flight> GetAllFlights();
        Flight? GetFlightByNumber(string flightNumber);
        List<Flight> SearchFlights(string source, string destination, DateTime date);
        void UpdateFlight(Flight flight);
        void DeleteFlight(Flight flight);
    }
}
