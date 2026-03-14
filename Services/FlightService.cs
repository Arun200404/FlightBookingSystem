using FlightBookingBackend.DTO;
using FlightBookingBackend.Interfaces;
using FlightBookingBackend.Models;
using FlightBookingBackend.Exceptions;
using System.Text.RegularExpressions;

namespace FlightBookingBackend.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public string AddFlight(FlightRequest request)
        {
            if (!Regex.IsMatch(request.FlightNumber, @"^[A-Z]{2}\d{3}$"))
                throw new BadRequestException("Flight number must be 2 uppercase letters followed by 3 digits (e.g. AK101)");

            if (_flightRepository.GetFlightByNumber(request.FlightNumber) != null)
                throw new BadRequestException("Flight number already exists");

            if (request.Source.Trim().ToLower() == request.Destination.Trim().ToLower())
                throw new BadRequestException("Source and Destination must not be the same");

            if (request.DepartureTime <= DateTime.Now)
                throw new BadRequestException("Departure time must be a future date and time");

            if (request.ArrivalTime <= request.DepartureTime)
                throw new BadRequestException("Arrival time must be after departure time");

            if (request.Fare <= 0)
                throw new BadRequestException("Fare must be greater than zero");

            if (request.AvailableSeats <= 0)
                throw new BadRequestException("Available seats must be greater than zero");

            var flight = new Flight
            {
                FlightNumber = request.FlightNumber,
                Source = request.Source,
                Destination = request.Destination,
                DepartureTime = request.DepartureTime,
                ArrivalTime = request.ArrivalTime,
                Fare = request.Fare,
                AvailableSeats = request.AvailableSeats
            };

            _flightRepository.AddFlight(flight);
            return "Flight added successfully";
        }

        public string UpdateFlight(string flightNumber, UpdateFlightRequest request)
        {
            var flight = _flightRepository.GetFlightByNumber(flightNumber)
                ?? throw new NotFoundException($"Flight '{flightNumber}' not found");

            if (request.Source.Trim().ToLower() == request.Destination.Trim().ToLower())
                throw new BadRequestException("Source and Destination must not be the same");

            if (request.DepartureTime <= DateTime.Now)
                throw new BadRequestException("Departure time must be a future date and time");

            if (request.ArrivalTime <= request.DepartureTime)
                throw new BadRequestException("Arrival time must be after departure time");

            if (request.Fare <= 0)
                throw new BadRequestException("Fare must be greater than zero");

            if (request.AvailableSeats <= 0)
                throw new BadRequestException("Available seats must be greater than zero");

            flight.Source = request.Source;
            flight.Destination = request.Destination;
            flight.DepartureTime = request.DepartureTime;
            flight.ArrivalTime = request.ArrivalTime;
            flight.Fare = request.Fare;
            flight.AvailableSeats = request.AvailableSeats;

            _flightRepository.UpdateFlight(flight);
            return $"Flight '{flightNumber}' updated successfully";
        }

        public string DeleteFlight(string flightNumber)
        {
            var flight = _flightRepository.GetFlightByNumber(flightNumber)
                ?? throw new NotFoundException($"Flight '{flightNumber}' not found");

            _flightRepository.DeleteFlight(flight);
            return $"Flight '{flightNumber}' deleted successfully";
        }

        public List<Flight> GetAllFlights()
        {
            return _flightRepository.GetAllFlights();
        }

        public List<Flight> SearchFlights(string source, string destination, DateTime date)
        {
            return _flightRepository.SearchFlights(source, destination, date);
        }
    }
}
