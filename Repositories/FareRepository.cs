using FlightBookingBackend.Data;
using FlightBookingBackend.Exceptions;
using FlightBookingBackend.Interfaces;

namespace FlightBookingBackend.Repositories
{
    public class FareRepository : IFareRepository
    {
        private readonly ApplicationDbContext _context;

        public FareRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public decimal GetBaseFare(string flightNumber)
        {
            var flight = _context.Flights.FirstOrDefault(f => f.FlightNumber == flightNumber)
                ?? throw new NotFoundException($"Flight '{flightNumber}' not found");

            return flight.Fare;
        }
    }
}
