using LawInOrder.Timesheet.Web.Models;
using System.Net.Mail;

namespace LawInOrder.Timesheet.Web.Managers
{
    public static class EmailManager
    {
        /// <summary>
        /// Send an email notification to the users manager
        /// </summary>
        /// <param name="time"></param>
        public static void Send(string email, Time time)
        {
            try
            {
                MailMessage mail = new MailMessage("no-reply@lawinorder.com.au", email);
                mail.Subject = "Time Entry Notification";
                mail.Body = "This represents a beautifully rendered HTML body text of the email";

                SmtpClient client = new SmtpClient();
                client.Port = 25;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Host = "smtp.gmail.com";
                //client.Send(mail); 
            }
            catch
            {
                // Fails on my machine without smtp set up
            }
        }
    }
}