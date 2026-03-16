using FlightBookingBackend.DTO;

namespace FlightBookingBackend.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterRequest request);
        Task<string> LoginAsync(LoginRequest request);
        Task<string> AdminLoginAsync(AdminLoginRequest request);
    }
}
