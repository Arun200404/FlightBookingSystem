using FlightBookingBackend.DTO;
using FlightBookingBackend.Interfaces;
using FlightBookingBackend.Models;
using FlightBookingBackend.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlightBookingBackend.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public AuthService(
            IAuthRepository authRepository,
            IAdminRepository adminRepository,
            IConfiguration configuration,
            IEmailService emailService)
        {
            _authRepository = authRepository;
            _adminRepository = adminRepository;
            _configuration = configuration;
            _emailService = emailService;
        }

        public string Register(RegisterRequest request)
        {
            if (_authRepository.GetUserByUsername(request.Username) != null)
                throw new BadRequestException("Username already exists");

            if (_authRepository.GetUserByEmail(request.Email) != null)
                throw new BadRequestException("Email already exists");

            if (string.IsNullOrWhiteSpace(request.Username) || request.Username.Length < 3)
                throw new BadRequestException("Username must be at least 3 characters");

            if (string.IsNullOrWhiteSpace(request.Email) || !request.Email.Contains("@"))
                throw new BadRequestException("Invalid email format");

            if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 6)
                throw new BadRequestException("Password must be at least 6 characters");


            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _authRepository.AddUser(user);

            try
            {
                _emailService.SendEmail(
                    request.Email,
                    "Welcome to Flight Booking System",
                    $"Hello {request.Username},\n\nYour account has been created successfully.\n\nThank you for registering with Flight Booking System."
                );
            }
            catch
            {
            }

            return "User registered successfully";
        }

        public string Login(LoginRequest request)
        {
            var user = _authRepository.GetUserByUsername(request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new UnauthorizedException("Invalid username or password");

            return GenerateToken(user.UserId.ToString(), user.Username, "User");
        }


        public string AdminLogin(AdminLoginRequest request)
        {
            var admin = _adminRepository.GetAdminByUsername(request.Username);

            if (admin == null)
                throw new UnauthorizedException("Invalid admin credentials");

            if (request.Password != admin.Password)
                throw new UnauthorizedException("Invalid admin credentials");

            return GenerateToken(admin.Id.ToString(), admin.Username, "Admin");
        }



        private string GenerateToken(string id, string username, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim("UserId", id),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
