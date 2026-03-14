using FlightBookingBackend.Data;
using FlightBookingBackend.Interfaces;
using FlightBookingBackend.Models;

namespace FlightBookingBackend.Repositories
{
    public class CheckinRepository : ICheckinRepository
    {
        private readonly ApplicationDbContext _context;

        public CheckinRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddCheckin(CheckIn checkin)
        {
            _context.CheckIns.Add(checkin);
            _context.SaveChanges();
        }

        public int GetCheckinCount()
        {
            return _context.CheckIns.Count();
        }
    }
}