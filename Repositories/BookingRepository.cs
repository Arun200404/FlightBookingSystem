using FlightBookingBackend.Data;
using FlightBookingBackend.Interfaces;
using FlightBookingBackend.Models;

namespace FlightBookingBackend.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _context;
        

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public Booking? GetBookingByReference(string bookingReference)
        {
            return _context.Bookings.FirstOrDefault(x => x.BookingReference == bookingReference);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}