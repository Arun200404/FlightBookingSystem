namespace FlightBookingBackend.Interfaces
{
    public interface IFareRepository
    {
        decimal GetBaseFare(string flightNumber);
    }
}