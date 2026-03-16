using FlightBookingBackend.Models;

namespace FlightBookingBackend.Interfaces
{
    public interface IBookingRepository
    {
        Task AddBookingAsync(Booking booking);
        Task<Booking?> GetBookingByReferenceAsync(string bookingReference);
        Task SaveAsync();
    }
}