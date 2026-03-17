using FlightBookingBackend.Data;
using FlightBookingBackend.Interfaces;
using FlightBookingBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingBackend.Repositories
{
    public class CheckinRepository : ICheckinRepository
    {
        private readonly ApplicationDbContext _context;

        public CheckinRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCheckinAsync(CheckIn checkin)
        {
            _context.CheckIns.Add(checkin);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetCheckinCountAsync(string flightNumber)
        {
            return await _context.CheckIns.Join(_context.Bookings,
                                    c => c.BookingReference,
                                    b => b.BookingReference,
                                    (c, b) => new { c, b })
                                .CountAsync(x => x.b.FlightNumber == flightNumber);

        }
    }
}