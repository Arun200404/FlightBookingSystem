using FlightBookingBackend.Interfaces;
using System.Net;
using System.Net.Mail;

namespace FlightBookingBackend.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var smtpHost = _configuration["Email:SmtpHost"]!;
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"]!);
            var senderEmail = _configuration["Email:SenderEmail"]!;
            var senderPassword = _configuration["Email:SenderPassword"]!;

            var smtp = new SmtpClient(smtpHost)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true
            };

            var mail = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                Body = body
            };

            mail.To.Add(to);

            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                throw new Exception($"Email sending failed: {ex.Message}");
            }
        }
    }
}
