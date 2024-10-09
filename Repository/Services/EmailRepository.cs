using System.Net.Mail;
using practice_dotnet.Repository.Interface;

namespace practice_dotnet.Repository.Services
{
    public class EmailRepository : IEmailRepository
    {
        private readonly SmtpClient _smtpClient;

        public EmailRepository(SmtpClient smtpClient)
        {
            _smtpClient = smtpClient;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("mikco@bpoc.co.jp"),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
