using FlightBookingBackend.Data;
using FlightBookingBackend.Interfaces;
using FlightBookingBackend.Models;

namespace FlightBookingBackend.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Admin? GetAdminByUsername(string username)
        {
            return _context.Admins.FirstOrDefault(a => a.Username == username);
        }

        public Admin? GetAdminByEmail(string email)
        {
            return _context.Admins.FirstOrDefault(a => a.Email == email);
        }

        public void AddAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }
    }
}
