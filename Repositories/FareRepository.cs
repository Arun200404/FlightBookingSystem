using FlightBookingBackend.Data;
using FlightBookingBackend.Exceptions;
using FlightBookingBackend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingBackend.Repositories
{
    public class FareRepository : IFareRepository
    {
        private readonly ApplicationDbContext _context;

        public FareRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetBaseFareAsync(string flightNumber)
        {
            var flight = await _context.Flights
                .FirstOrDefaultAsync(f => f.FlightNumber == flightNumber)
                ?? throw new NotFoundException($"Flight '{flightNumber}' not found");

            return flight.Fare;
        }
    }
}
