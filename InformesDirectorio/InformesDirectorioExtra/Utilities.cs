using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InformesDirectorioExtra
{
    public class Utilities
    {
        public static bool SendEmailNotificationForSharedDocument(string emailTo, string emailFrom, string userFrom, string title, string url)
        {
            try
            {
                System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                correo.From = new System.Net.Mail.MailAddress("noresponder@cte.gob.ec");
                correo.To.Add(emailTo);
                correo.Bcc.Add(emailFrom);
                string subject = title.Length > 50 ? title.Substring(0, 50) : title;
                correo.Subject = "CTE: " + subject;
                string body = "El usuario " + userFrom + " acaba de compartir un documento/informe del Directorio de la Comisión de Tránsito del Ecuador. <br /><br />" +
                    "Haga clic en el siguiente enlace para acceder a este documento:" +
                    "<a href=\"" + url + "\">" + title + "</a>";
                correo.Body = body;
                correo.IsBodyHtml = true;
                correo.Priority = System.Net.Mail.MailPriority.High;
                //
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                //---------------------------------------------
                // Estos datos debes rellanarlos correctamente
                //---------------------------------------------
                smtp.Host = Constantes.ParametrosEnvioEmail.smtpHost;
                smtp.Credentials = new System.Net.NetworkCredential(Constantes.ParametrosEnvioEmail.smtpUser, Constantes.ParametrosEnvioEmail.smptPassword);
                //smtp.EnableSsl = false;

                smtp.Send(correo);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
