using FlightBookingBackend.Models;

namespace FlightBookingBackend.Interfaces
{
    public interface ICheckinRepository
    {
        Task AddCheckinAsync(CheckIn checkin);
        Task<int> GetCheckinCountAsync();
    }
}