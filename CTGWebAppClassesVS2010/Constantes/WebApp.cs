using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Constantes
{
    public class WebApp
    {
        public enum BaseDatosKeys
        {
            tns, usuario, clave
        }

        

        public enum DatosSesion
        {
            NombreEmpresa, CodigoSolicitudTramite, ValorTramite, CodigoProceso, ModoEntregaTramite, CostoCorreosDelEcuador, CodigoOficina,
            ImgCitacWidth, ImgCitacHeight
        }

        public enum MasterPageControls
        {
            divMainContentError
        }

        public static void SendErrorAlert(string appWebName, string errorMsg)
        {
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress("webmaster@ctg.gob.ec");
            correo.To.Add("gpincay@ctg.gob.ec");
            //correo.CC.Add("bramos@ctg.gov.ec");
            correo.Subject = "Error en aplicación web";
            string body = "Un usuario  de la CTE ha recibido un error. <br /><br />" +
                "<b>Error:</b>  " + errorMsg +
                "<br /><b>Aplicación Web:</b> " + appWebName;
            correo.Body = body;
            correo.IsBodyHtml = true;
            correo.Priority = System.Net.Mail.MailPriority.High;
            //
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            //---------------------------------------------
            // Estos datos debes rellanarlos correctamente
            //---------------------------------------------
            smtp.Host = "201.218.0.228";
            smtp.Credentials = new System.Net.NetworkCredential("webmaster", "123456");
            //smtp.EnableSsl = false;
            try
            {
                smtp.Send(correo);
            }
            catch
            {
            }
        }

    }
}
