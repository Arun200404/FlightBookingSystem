using FlightBookingBackend.Data;
using FlightBookingBackend.Interfaces;
using FlightBookingBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightBookingBackend.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBookingAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking?> GetBookingByReferenceAsync(string bookingReference)
        {
            return await _context.Bookings
                .FirstOrDefaultAsync(x => x.BookingReference == bookingReference);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}