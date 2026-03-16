using FlightBookingBackend.Interfaces;
namespace FlightBookingBackend.Services
{
    public class PaymentService : IPaymentService
{
    public Task<bool> ProcessPaymentAsync(decimal amount)
    {
        // Dummy — always returns true
        return Task.FromResult(true);
    }
}
}