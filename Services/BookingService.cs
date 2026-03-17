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
        private readonly IPaymentService _paymentService;

        public BookingService(
            IBookingRepository bookingRepository,
            IFlightRepository flightRepository,
            IFareService fareService,
            IEmailService emailService,
            IAuthRepository authRepository,
            IPaymentService paymentService)
        {
            _bookingRepository = bookingRepository;
            _flightRepository = flightRepository;
            _fareService = fareService;
            _emailService = emailService;
            _authRepository = authRepository;
            _paymentService = paymentService;
        }

        public async Task<string> CreateBookingAsync(BookingRequest request, int userId)
        {
            var flight = await _flightRepository.GetFlightByNumberAsync(request.FlightNumber)
                ?? throw new NotFoundException("Flight not found");

            if (flight.DepartureTime.AddHours(-1) <= DateTime.Now)
                throw new BadRequestException("Booking Time Closed");

            if (flight.AvailableSeats <= 0)
                throw new BadRequestException("No seats available on this flight");

            var user = await _authRepository.GetUserByIdAsync(userId)
                ?? throw new NotFoundException("User not found");

            if (string.IsNullOrWhiteSpace(request.PassengerName) || request.PassengerName.Length < 2)
                throw new BadRequestException("Passenger name must be at least 2 characters");

            if (request.Gender != "Male" && request.Gender != "Female" && request.Gender != "Other")
                throw new BadRequestException("Gender must be Male, Female, or Other");

            flight.AvailableSeats -= 1;

            var fareDetails = await _fareService.CalculateFareAsync(request.FlightNumber);

            var paySuccess = await _paymentService.ProcessPaymentAsync(fareDetails.FinalFare);
            if (!paySuccess)
                throw new BadRequestException("Payment failed. Booking not confirmed.");


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

            await _bookingRepository.AddBookingAsync(booking);

            try
            {
                await _emailService.SendEmailAsync(
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
                return $"Booking successful. Your Booking Reference: {booking.BookingReference}\nEmail service failed";
            }

            return $"Booking successful. Your Booking Reference: {booking.BookingReference}";
        }

        public async Task<BookingResponse?> SearchBookingAsync(string bookingReference)
        {
            var booking = await _bookingRepository.GetBookingByReferenceAsync(bookingReference);
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
