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

            // Flight precision
            modelBuilder.Entity<Flight>()
                .Property(f => f.Fare)
                .HasPrecision(18, 2);

            // Booking precision
            modelBuilder.Entity<Booking>()
                .Property(b => b.BaseFare)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Booking>()
                .Property(b => b.FinalFare)
                .HasPrecision(18, 2);

            // Unique indexes - User
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Unique indexes - Flight
            modelBuilder.Entity<Flight>()
                .HasIndex(f => f.FlightNumber)
                .IsUnique();

            // Unique indexes - Admin
            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Username)
                .IsUnique();

            modelBuilder.Entity<Admin>()
                .HasIndex(a => a.Email)
                .IsUnique();
        }
    }
}
