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

        public async Task<FareResponse> CalculateFareAsync(string flightNumber)
        {
            var baseFare = await _fareRepository.GetBaseFareAsync(flightNumber);

            var gst = baseFare * 0.05m;
            var finalFare = baseFare + gst;

            return new FareResponse
            {
                BaseFare = baseFare,
                GST = gst,
                FinalFare = Math.Round(finalFare, 2)
            };
        }
    }
}