using Microsoft.EntityFrameworkCore;
using FlightBookingBackend.Models;

namespace FlightBookingBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Flight>().Property(f => f.Fare).HasPrecision(18, 2);

            modelBuilder.Entity<Booking>().Property(b => b.BaseFare).HasPrecision(18, 2);

            modelBuilder.Entity<Booking>().Property(b => b.FinalFare).HasPrecision(18, 2);

            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();


            modelBuilder.Entity<Flight>().HasIndex(f => f.FlightNumber).IsUnique();

            modelBuilder.Entity<Admin>().HasIndex(a => a.Username).IsUnique();

            modelBuilder.Entity<Admin>().HasIndex(a => a.Email).IsUnique();
        }
    }
}
