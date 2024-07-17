using MailKit.Net.Smtp;
using MimeKit;

namespace CarWashAPI.Repository
{
    public class EmailRepository
    {
        private readonly IConfiguration _configuration;
        public EmailRepository(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        private async Task SendEmailAsync(string to, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration["SmtpSettings:SenderName"], _configuration["SmtpSettings:SenderEmail"]));
            message.To.Add(new MailboxAddress(to, to));
            message.Subject = subject;
            message.Body = new TextPart("html")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_configuration["SmtpSettings:Server"], int.Parse(_configuration["SmtpSettings:Port"]), false);
                client.Authenticate(_configuration["SmtpSettings:Username"], _configuration["SmtpSettings:Password"]);
                await client.SendAsync(message);
                client.Disconnect(true);
            }
        }
    }
}
