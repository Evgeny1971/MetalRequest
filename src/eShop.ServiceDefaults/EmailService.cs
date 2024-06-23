using System;
using System.Net;
using System.Net.Mail;

using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

//using Serilog;

namespace eShop.ServiceDefaults;

   public class EmailRequestDTO
    {
        public string From { get; set; } ="gen.vinnikov@gmail.com";
        public string[] To { get; set; } = new string[0];
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string SmtpServer { get; set; }="smtp.gmail.com";
        public int Port { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; } 
    }	
	public class EmailService()
	{
		
		public void SendEmail(EmailRequestDTO emailRequestDTO)
		{
			MailMessage mail = new MailMessage();

			SmtpClient smtpClient = new SmtpClient();//(smtpServerAdress,25);
			smtpClient.Credentials = new System.Net.NetworkCredential(emailRequestDTO.UserName, emailRequestDTO.Password);
			smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
			mail.From = new MailAddress(emailRequestDTO.From);
			foreach(string itemToAdress in emailRequestDTO.To){mail.To.Add(itemToAdress);}
			
			mail.Subject = emailRequestDTO.Subject;
			mail.Body = emailRequestDTO.Body;
		
			smtpClient.Host = emailRequestDTO.SmtpServer;
			smtpClient.Port = emailRequestDTO.Port;
			smtpClient.EnableSsl = true;

			smtpClient.Send(mail);
			
		}
    }
    
