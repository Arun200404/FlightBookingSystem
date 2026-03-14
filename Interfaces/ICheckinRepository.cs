using FlightBookingBackend.Models;

namespace FlightBookingBackend.Interfaces
{
    public interface ICheckinRepository
    {
        void AddCheckin(CheckIn checkin);
        int GetCheckinCount();
    }
}