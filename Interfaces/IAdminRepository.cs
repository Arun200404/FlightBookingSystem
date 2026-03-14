using FlightBookingBackend.Models;

namespace FlightBookingBackend.Interfaces
{
    public interface IAdminRepository
    {
        Admin? GetAdminByUsername(string username);
        Admin? GetAdminByEmail(string email);
        void AddAdmin(Admin admin);
    }
}
