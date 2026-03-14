using FlightBookingBackend.Data;
using FlightBookingBackend.Interfaces;
using FlightBookingBackend.Models;

namespace FlightBookingBackend.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly ApplicationDbContext _context;

        public FlightRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();
        }

        public Flight? GetFlightByNumber(string flightNumber)
        {
            return _context.Flights.FirstOrDefault(f => f.FlightNumber == flightNumber);
        }

        public List<Flight> GetAllFlights()
        {
            return _context.Flights.ToList();
        }

        public List<Flight> SearchFlights(string source, string destination, DateTime date)
        {
            return _context.Flights
                .Where(f => f.Source == source && f.Destination == destination && f.DepartureTime.Date == date.Date)
                .ToList();
        }

        public void UpdateFlight(Flight flight)
        {
            _context.Flights.Update(flight);
            _context.SaveChanges();
        }

        public void DeleteFlight(Flight flight)
        {
            _context.Flights.Remove(flight);
            _context.SaveChanges();
        }
    }
}
