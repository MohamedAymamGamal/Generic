using Ecom.Core.DTO;
using Ecom.Core.Service;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Ecom.infrastructure.Reposities.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(EmailDto emailDto)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("My Ecom", _configuration["EmailSetting:From"]));
            message.To.Add(new MailboxAddress(emailDto.To, emailDto.To));
            message.Subject = emailDto.Subject;
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailDto.Contant
            };

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _configuration["EmailSetting:Smtp"],
                int.Parse(_configuration["EmailSetting:Port"]!),
                SecureSocketOptions.SslOnConnect
            );
            await client.AuthenticateAsync(
                _configuration["EmailSetting:Username"],
                _configuration["EmailSetting:Password"]
            );
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}