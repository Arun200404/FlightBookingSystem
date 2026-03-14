using FlightBookingBackend.DTO;
using FlightBookingBackend.Models;

namespace FlightBookingBackend.Interfaces
{
    public interface IFlightService
    {
        string AddFlight(FlightRequest request);
        List<Flight> GetAllFlights();
        List<Flight> SearchFlights(string source, string destination, DateTime date);
        string UpdateFlight(string flightNumber, UpdateFlightRequest request);
        string DeleteFlight(string flightNumber);
    }
}
