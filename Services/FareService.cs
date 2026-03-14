using FlightBookingBackend.DTOs;
using FlightBookingBackend.Interfaces;

namespace FlightBookingBackend.Services
{
    public class FareService : IFareService
    {
        private readonly IFareRepository _fareRepository;

        public FareService(IFareRepository fareRepository)
        {
            _fareRepository = fareRepository;
        }

        public FareResponse CalculateFare(string flightNumber)
        {
            var baseFare = _fareRepository.GetBaseFare(flightNumber);

            var gst = baseFare * 0.05m;
            var finalFare = baseFare + gst;

            return new FareResponse
            {
                BaseFare = baseFare,
                GST = gst,
                FinalFare = Math.Round(finalFare,2)
            };
        }
    }
}