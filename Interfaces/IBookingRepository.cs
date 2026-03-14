using FlightBookingBackend.Models;

namespace FlightBookingBackend.Interfaces
{
    public interface IBookingRepository
    {
        void AddBooking(Booking booking);
        Booking? GetBookingByReference(string bookingReference);
        void Save();
    }
}