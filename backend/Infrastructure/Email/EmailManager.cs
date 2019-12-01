using System.Collections.Generic;
using MailKit.Net.Smtp;
using MimeKit;
using System.Linq;
using MimeKit.Text;
using MailKit.Net.Pop3;
using System;

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
            ;  
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress("Email Test", toAddress));
            message.From.Add(new MailboxAddress("Tutoring Services", _emailConfiguration.SmtpUsername));
            
            message.Subject = subject;

            message.Body = new TextPart("plain") 
            {
                Text = "Thanks for creating your account!"
            };
            
            //Be careful that the SmtpClient class is the one from Mailkit not the framework!
            using (var emailClient = new SmtpClient())
            {
                //The last parameter here is to use SSL (Which you should!)
                emailClient.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
                
                //Remove any OAuth functionality as we won't be using it. 
                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");
                
                emailClient.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                
                emailClient.Send(message);
                
                emailClient.Disconnect(true);
            }
            
        }
    }
}