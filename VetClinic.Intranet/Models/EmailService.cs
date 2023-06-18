using MailKit.Net.Smtp;
using MimeKit;

namespace VetClinic.Intranet.Models
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("VetClinic", "paulina.golanowska@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_configuration["Email:SmtpServer"], Convert.ToInt32(_configuration["Email:SmtpPort"]), false);
                await client.AuthenticateAsync(_configuration["Email:SmtpUsername"], _configuration["Email:SmtpPassword"]);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
