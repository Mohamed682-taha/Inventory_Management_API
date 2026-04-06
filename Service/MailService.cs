using Domain.Interfaces;
using Domain.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Service
{
    public class MailService : IMailService
    {
        private readonly MailSettings _options;
        public MailService(IOptions<MailSettings> options)
        {
            _options = options.Value;
        }

        public void SendEmail(Email email)
        {
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Email),
                Subject = email.Subject
            };
            mail.To.Add(MailboxAddress.Parse(email.To));
            mail.From.Add(new MailboxAddress(_options.DisplayName,_options.Email));

            var builder = new BodyBuilder();
            builder.TextBody = email.Body;

            mail.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_options.Host,_options.Port,MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_options.Email,_options.Password);
            smtp.Send(mail);
            smtp.Disconnect(true);
        }
    }
}
