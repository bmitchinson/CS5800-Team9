using System.Collections.Generic;
using MailKit.Net.Smtp;
using MimeKit;
using System.Linq;
using MimeKit.Text;
using MailKit.Net.Pop3;
using System;

// created using guide from 
// https://dotnetcoretutorials.com/2017/11/02/using-mailkit-send-receive-email-asp-net-core/
namespace backend.Infrastructure.EmailManager{
    public interface IEmailManager
    {
        void Send(string toAddress, string subject, string body);

    }
    
    public class EmailManager : IEmailManager
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailManager(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        
        public void Send(string toAddress, string subject, string body)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress("Email Test", toAddress));
            message.From.Add(new MailboxAddress("Tutoring Services", _emailConfiguration.SmtpUsername));
            
            message.Subject = subject;

            message.Body = new TextPart("plain") 
            {
                Text = body
            };

            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                emailClient.Send(message);
                emailClient.Disconnect(true);
            }
            
        }
    }
}
