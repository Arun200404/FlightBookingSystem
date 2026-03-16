using FlightBookingBackend.DTOs;

namespace FlightBookingBackend.Interfaces
{
    public interface IBookingService
    {
        Task<string> CreateBookingAsync(BookingRequest request, int userId);
        Task<BookingResponse?> SearchBookingAsync(string bookingReference);
    }
}
