using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Sistema_Negocio_Ropa.Recursos
{
    public class ServicioCorreo
{
        private readonly string smtpServer = "smtp.gmail.com";
        private readonly int smtpPort = 587;
        private readonly string emailFrom = ConfigurationManager.AppSettings["Email"];
        private readonly string emailPassword = ConfigurationManager.AppSettings["Contraseña"];

        
        // Servicio de Correo SMTP
        public bool EnviarCorreo(string nombreUsuario, string emailTo, string claveGenerada)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort);
                StringBuilder htmlBuilder = new StringBuilder();
                htmlBuilder.AppendLine("<h2 style='color:#4d8bd0'>Servicio de gestión de usuarios</h2>");
                htmlBuilder.AppendLine("<h1>Confirmación de correo electrónico</h1>");
                htmlBuilder.AppendLine($"<p><b>Tu nombre de usuario es: </b>{nombreUsuario}</p>");
                htmlBuilder.AppendLine($"<p><b>Tu clave generada es: </b>{claveGenerada}</p>");
                htmlBuilder.AppendLine("<p>Si no reconoces esta actividad, por favor ignórala.</p>");
                htmlBuilder.AppendLine("Gracias.");

                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = "Recuperar contraseña - Servicio de gestión de usuarios.";
                mail.Body = htmlBuilder.ToString();
                mail.IsBodyHtml = true;

                smtpClient.Credentials = new NetworkCredential(emailFrom, emailPassword);
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }


}
