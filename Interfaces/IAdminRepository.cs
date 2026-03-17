using FlightBookingBackend.Models;

namespace FlightBookingBackend.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin?> GetAdminByUsernameAsync(string username);
    }
}
