using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RESHOTEL_APP.Tools
{
    class Mail
    {
        private MailMessage message;
        private SmtpClient mailClient;

        public Mail()
        {
            message = new MailMessage();
            message.From = new MailAddress("dimafoot@hotmail.fr", "RESHOTEL");
            mailClient = new SmtpClient("smtp.live.com", 587);
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.UseDefaultCredentials = false;
            mailClient.EnableSsl = true;
            mailClient.Credentials = new NetworkCredential("dimafoot@hotmail.fr", "***************");
        }

        public void nbRoomAlert(DateTime _checkIn)
        {
            message.To.Add("abdelfattah.ratnane@viacesi.fr");
            message.IsBodyHtml = true;
            message.Subject = "Plus que 5 chambres";
            message.Body = "Alerte il ne reste plus que 5 chambre disponible pour cette date : "+Convert.ToDateTime(_checkIn).ToString("dd/MM/yyyy");
            ThreadPool.QueueUserWorkItem(_ =>
            {
                mailClient.Send(message);
            });
        }


        public void errorMail(string _message, string _details)
        {
            message.To.Add("abdelfattah.ratnane@viacesi.fr");
            message.IsBodyHtml = true;
            message.Subject = "Une erreur est survenue sur l'application RESHOTEL";
            message.Body = "<b>Identité de l'utilisateur :</b> " + Model.Login.firstname + " " + Model.Login.lastname + "<br><br>";
            message.Body += "<b>Date et heure de l'erreur :</b> " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "<br><br>";
            message.Body += "<b>Message d'erreur :</b> " + _message + "<br><br>";
            message.Body += "<b>Détails sur l'erreur :</b> " + _details.Replace("à", "<br>");
            ThreadPool.QueueUserWorkItem(_ =>
            {
                mailClient.Send(message);
            });
        }

    }
}
