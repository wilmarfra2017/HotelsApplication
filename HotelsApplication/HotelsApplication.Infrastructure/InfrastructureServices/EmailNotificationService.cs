using HotelsApplication.Domain.Ports;
using System.Net.Mail;

namespace HotelsApplication.Infrastructure.InfrastructureServices
{
    public class EmailNotificationService : INotificationService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _fromEmail;
        private readonly string _smtpUsername;
        private readonly string _smtpPassword;

        public EmailNotificationService(string smtpServer, int smtpPort, string fromEmail, string smtpUsername, string smtpPassword)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _fromEmail = fromEmail;
            _smtpUsername = smtpUsername;
            _smtpPassword = smtpPassword;
        }

        public async Task SendAsync(string recipient, string subject, string message)
        {
            var mailMessage = new MailMessage(_fromEmail, recipient, subject, message)
            {
                IsBodyHtml = true
            };

            using (var smtpClient = new SmtpClient(_smtpServer, _smtpPort))
            {
                smtpClient.Credentials = new System.Net.NetworkCredential(_smtpUsername, _smtpPassword);
                smtpClient.EnableSsl = true;

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {                    
                    Console.WriteLine(ex.Message);
                }

            }
        }
    }
}
