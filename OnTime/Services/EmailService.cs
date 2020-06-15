using Microsoft.Extensions.Options;
using OnTime.Configuration;
using OnTime.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace OnTime.Services
{
    public class EmailService : IEmailService
    {
        private SmtpSetting _setting;
        private readonly SmtpClient _client;
        public EmailService(IOptions<SmtpSetting> options)
        {
            _setting = options.Value;
            _client = new SmtpClient(_setting.Server)
            {
                Credentials = new NetworkCredential(_setting.Username, _setting.Password)
            };
        }

        public async Task SendEmail(string email, string subject, string message)
        {
            var mailMessage = new MailMessage("Test - Email", email, subject, message);
            await _client.SendMailAsync(mailMessage);
        }
    }
}
