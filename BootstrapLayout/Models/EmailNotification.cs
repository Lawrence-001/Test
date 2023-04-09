using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace BootstrapLayout.Models
{
    public class EmailNotification
    {
        private readonly SmtpClient _smtpClient;
        private const string SmtpBucketHost = "smtpbucket.com";
        private const int SmtpBucketPort = 8025;

        public EmailNotification()
        {
            _smtpClient = new SmtpClient(SmtpBucketHost, SmtpBucketPort);
        }

        [HttpGet]
        public void SendProfileCreatedNotification(string recipientName, string recipientEmail)
        {
            string subject = "Profile Notification #Created";
            string body = $"Greeting {recipientName}, we are glad to inform you that your staff profile has been created.";

            SendEmailNotification(recipientEmail, subject, body);
        }

        public void SendProfileEditedNotification(string recipientName, string recipientEmail)
        {
            string subject = "Profile Notification #Edited";
            string body = $"Greeting {recipientName}, we are glad to inform you that your staff profile has been edited.";

            SendEmailNotification(recipientEmail, subject, body);
        }

        public void SendProfileDeletedNotification(string recipientName, string recipientEmail)
        {
            string subject = "Profile Notification #Deleted";
            string body = $"Greeting {recipientName}, we are sad to inform you that your staff profile has been deleted.";

            SendEmailNotification(recipientEmail, subject, body);
        }

        private void SendEmailNotification(string recipientEmail, string subject, string body)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress("sender@example.com"),
                Subject = subject,
                Body = body
            };
            message.To.Add(recipientEmail);

            _smtpClient.Send(message);
        }
    }

}
