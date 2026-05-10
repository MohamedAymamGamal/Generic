using Ecom.Core.DTO;
using Ecom.Core.Service;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ecom.infrastructure.Reposities.Service
{
   
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;
        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task SendEmail(EmailDto emailDto)
        {
             MimeMessage message = new MimeMessage();


            message.From.Add(new MailboxAddress("My Ecom", configuration["EmailSetting:From"]));
            message.Subject = emailDto.Subject;
            message.To.Add(new MailboxAddress(emailDto.To, emailDto.To));
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = emailDto.Contant   
            };
            return Task.CompletedTask;
        }
    }
}
