using FlightBookingBackend.DTOs;

namespace FlightBookingBackend.Interfaces
{
    public interface IBookingService
    {
        string CreateBooking(BookingRequest request, int userId);
        BookingResponse? SearchBooking(string bookingReference);
    }
}
