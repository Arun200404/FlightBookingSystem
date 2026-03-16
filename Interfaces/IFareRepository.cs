namespace FlightBookingBackend.Interfaces
{
    public interface IFareRepository
    {
        Task<decimal> GetBaseFareAsync(string flightNumber);
    }
}