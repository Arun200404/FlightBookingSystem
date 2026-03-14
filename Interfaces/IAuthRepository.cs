using FlightBookingBackend.Models;

namespace FlightBookingBackend.Interfaces
{
    public interface IAuthRepository
    {
        User? GetUserByEmail(string email);
        void AddUser(User user);
        User? GetUserByUsername(string username);
        User? GetUserById(int id);
    }
}