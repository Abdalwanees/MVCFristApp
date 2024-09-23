using Microsoft.Extensions.Options;
using MimeKit;
using MVCFristApp.DAL.Models;
using MVCFristApp.PL.AppSettingConfig;
using System.Net;
using System.Net.Mail;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace MVCFristApp.PL.Healpers
{
    public class EmailSettings
    {
        private readonly MailSettings _options;

        public EmailSettings(IOptions<MailSettings> options)
        {
            _options = options.Value;
        }
        public /*static*/ void SendEmail(Email email)
        {
            //var clint = new SmtpClient("smtp.gmail.com", 587);//Old method to sent Email
            //clint.EnableSsl = false;
            //clint.Credentials = new NetworkCredential("a****ayed@gmail.com", "********************");
            //clint.Send("a****ayed@gmail.com", email.Reciepints,email.Subject,email.Body);
            //clint.Dispose();
            var mail = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.Email),
                Subject = email.Subject
            };

            mail.To.Add(MailboxAddress.Parse(email.Reciepints));
            mail.From.Add(new MailboxAddress(_options.DisplayName, _options.Email));

            var builder = new BodyBuilder
            {
                HtmlBody = email.Body
            };

            mail.Body = builder.ToMessageBody();

            // Sending the email using MailKit's SmtpClient
            using var smtp = new SmtpClient();
            smtp.Connect(_options.Host, _options.Port, MailKit.Security.SecureSocketOptions.StartTls);

            // Authenticate with the SMTP server
            smtp.Authenticate(_options.Email, _options.Password);

            // Send the email
            smtp.Send(mail);

            // Disconnect from the SMTP server
            smtp.Disconnect(true);

        }
    }
}
