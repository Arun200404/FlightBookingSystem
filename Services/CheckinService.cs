using FlightBookingBackend.Exceptions;
using FlightBookingBackend.Interfaces;
using FlightBookingBackend.Models;
namespace FlightBookingBackend.Services
{
    public class CheckinService : ICheckinService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICheckinRepository _checkinRepository;
        private readonly IEmailService _emailService;

        public CheckinService(
            IBookingRepository bookingRepository,
            ICheckinRepository checkinRepository,
            IEmailService emailService)
        {
            _bookingRepository = bookingRepository;
            _checkinRepository = checkinRepository;
            _emailService = emailService;
        }

        public async Task<string> DoCheckinAsync(string bookingReference)
        {
            var booking = await _bookingRepository.GetBookingByReferenceAsync(bookingReference)
                ?? throw new NotFoundException("Booking not found");

            if (booking.BookingStatus == "CheckedIn") 
                throw new BadRequestException("Already checked in for this booking");

            var count = await _checkinRepository.GetCheckinCountAsync() + 1;
            var seatNumber = "A" + count;

            var checkin = new CheckIn
            {
                PassengerName = booking.PassengerName,
                BookingReference = bookingReference,
                SeatNumber = seatNumber,
                CheckInReference = Guid.NewGuid().ToString()[..8].ToUpper(),
                CheckInStatus = "Completed"
            };

            booking.BookingStatus = "CheckedIn";
            await _bookingRepository.SaveAsync();

            await _checkinRepository.AddCheckinAsync(checkin);

            try
            {
                await _emailService.SendEmailAsync(
                    booking.Email,
                    "Check-In Confirmed - Flight Booking System",
                    $"Dear {checkin.PassengerName},\n\n" +
                    $"Your check-in is confirmed!\n\n" +
                    $"Check-In Reference : {checkin.CheckInReference}\n" +
                    $"Flight Number      : {booking.FlightNumber}\n" +
                    $"Seat Number        : {seatNumber}\n" +
                    $"Booking Reference  : {bookingReference}\n\n" +
                    $"Have a good Journy!"
                );
            }
            catch
            {
                return $"Check-in successful. Check-In Reference: {checkin.CheckInReference}, Seat: {seatNumber}\nEmail Service Faied";
            }

            return $"Check-in successful. Check-In Reference: {checkin.CheckInReference}, Seat: {seatNumber}";
        }
    }
}
