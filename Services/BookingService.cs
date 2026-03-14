using FlightBookingBackend.DTOs;

using FlightBookingBackend.Exceptions;
using FlightBookingBackend.Interfaces;
using FlightBookingBackend.Models;

namespace FlightBookingBackend.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IFareService _fareService;
        private readonly IEmailService _emailService;
        private readonly IAuthRepository _authRepository;

        public BookingService(
            IBookingRepository bookingRepository,
            IFlightRepository flightRepository,
            IFareService fareService,
            IEmailService emailService,
            IAuthRepository authRepository)
        {
            _bookingRepository = bookingRepository;
            _flightRepository = flightRepository;
            _fareService = fareService;
            _emailService = emailService;
            _authRepository = authRepository;
        }

        public string CreateBooking(BookingRequest request, int userId)
        {
            var flight = _flightRepository.GetFlightByNumber(request.FlightNumber)
                ?? throw new NotFoundException("Flight not found");

            if (flight.AvailableSeats <= 0)
                throw new BadRequestException("No seats available on this flight");

            var user = _authRepository.GetUserById(userId)
                ?? throw new NotFoundException("User not found");

            flight.AvailableSeats -= 1;

            var fareDetails = _fareService.CalculateFare(request.FlightNumber);

            var booking = new Booking
            {
                UserId = userId,
                Email = user.Email,
                FlightNumber = flight.FlightNumber,
                PassengerName = request.PassengerName,
                Gender = request.Gender,
                BaseFare = flight.Fare,
                FinalFare = fareDetails.FinalFare,
                BookingReference = Guid.NewGuid().ToString()[..8].ToUpper(),
                BookingDate = DateTime.UtcNow,
                BookingStatus = "Confirmed"
            };

            _bookingRepository.AddBooking(booking);

            try
            {
                _emailService.SendEmail(
                    user.Email,
                    "Booking Confirmed - Flight Booking System",
                    $"Dear {booking.PassengerName},\n\n" +
                    $"Your booking has been confirmed!\n\n" +
                    $"Booking Reference : {booking.BookingReference}\n" +
                    $"Passenger Name   : {booking.PassengerName}\n" +
                    $"Gender           : {booking.Gender}\n" +
                    $"Flight Number    : {booking.FlightNumber}\n" +
                    $"Journey          : {flight.Source} → {flight.Destination}\n" +
                    $"Departure Time   : {flight.DepartureTime:dd MMM yyyy hh:mm tt}\n" +
                    $"Arrival Time     : {flight.ArrivalTime:dd MMM yyyy hh:mm tt}\n" +
                    $"Base Fare        : ₹{booking.BaseFare}\n" +
                    $"GST (5%)         : ₹{fareDetails.GST}\n" +
                    $"Total Fare       : ₹{booking.FinalFare}\n\n" +
                    $"Thank you for booking with us!"
                );
            }
            catch
            {
                // Email failure must not fail the booking
            }

            return $"Booking successful. Your Booking Reference: {booking.BookingReference}";
        }

        public BookingResponse? SearchBooking(string bookingReference)
        {
            var booking = _bookingRepository.GetBookingByReference(bookingReference);
            if (booking == null) return null;

            return new BookingResponse
            {
                BookingReference = booking.BookingReference,
                FlightNumber = booking.FlightNumber,
                PassengerName = booking.PassengerName,
                Gender = booking.Gender,
                BookingDate = booking.BookingDate,
                BookingStatus = booking.BookingStatus,
                BaseFare = booking.BaseFare,
                FinalFare = booking.FinalFare
            };
        }
    }
}
