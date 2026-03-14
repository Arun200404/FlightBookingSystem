using FlightBookingBackend.DTO;

namespace FlightBookingBackend.Interfaces
{
    public interface IAuthService
    {
        string Register(RegisterRequest request);
        string Login(LoginRequest request);
        string AdminLogin(AdminLoginRequest request);
    }
}
