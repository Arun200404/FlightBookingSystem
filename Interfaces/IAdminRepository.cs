using FlightBookingBackend.Models;

namespace FlightBookingBackend.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin?> GetAdminByUsernameAsync(string username);
        Task<Admin?> GetAdminByEmailAsync(string email);
        Task AddAdminAsync(Admin admin);
    }
}
