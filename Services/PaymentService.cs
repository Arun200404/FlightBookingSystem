using FlightBookingBackend.Interfaces;
namespace FlightBookingBackend.Services
{
    public class PaymentService : IPaymentService
{
    public Task<bool> ProcessPaymentAsync(decimal amount)
    {
        return Task.FromResult(true);
    }
}
}