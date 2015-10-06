using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace Pizza.Controllers.Extensions
{
    public class EmailService : IIdentityMessageService
    {
        public static string MAIL_SERVER = ConfigurationManager.AppSettings.Get("MAIL_SERVER");
        public static int MAIL_SERVER_PORT = int.Parse(ConfigurationManager.AppSettings.Get("MAIL_SERVER_PORT"));
        public static bool USE_SSL = bool.Parse(ConfigurationManager.AppSettings.Get("USE_SSL"));
        public static string SENDER_EMAIL = ConfigurationManager.AppSettings.Get("SENDER_EMAIL");
        public static string SENDER_PASSWORD = ConfigurationManager.AppSettings.Get("SENDER_PASSWORD");

        public Task SendAsync(IdentityMessage message)
        {
            MailMessage mail = new MailMessage(SENDER_EMAIL, message.Destination);
            mail.Subject = message.Subject;
            mail.Body = message.Body;
            mail.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Host = MAIL_SERVER;
            client.Port = MAIL_SERVER_PORT;
            client.EnableSsl = USE_SSL;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(SENDER_EMAIL, SENDER_PASSWORD);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.SendCompleted += client_SendCompleted;
            client.SendAsync(mail, new object());

            return Task.FromResult(0);
        }

        public static Task SendAsync(string subject, string destination, string body)
        {
            try
            {
                MailMessage mail = new MailMessage(SENDER_EMAIL, destination);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.Host = MAIL_SERVER;
                client.Port = MAIL_SERVER_PORT;
                client.EnableSsl = USE_SSL;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(SENDER_EMAIL, SENDER_PASSWORD);

                client.Send(mail);

                //client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.SendCompleted += client_SendCompleted;
                //client.SendAsync(mail, new object());

                return Task.FromResult(0);
            }
            catch
            {
                throw new Exception("Email not sent");
            }
        }

        public static void client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            var args = e;
            //Console.WriteLine(e.ToString());
        }
    }
}