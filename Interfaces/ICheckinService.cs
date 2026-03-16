namespace FlightBookingBackend.Interfaces
{
    public interface ICheckinService
    {
        Task<string> DoCheckinAsync(string bookingReference);
    }
}