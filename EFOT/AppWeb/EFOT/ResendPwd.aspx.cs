using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ResendPwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSendPwd_Click(object sender, EventArgs e)
    {
        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

        EFOTclass.User oUsuarioEFOT = new EFOTclass.User(System.Configuration.ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            System.Configuration.ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            System.Configuration.ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        string encPwd;
        string email;

        if (oUsuarioEFOT.GetEncPwdAndEmailFromDB(txtCedula.Text, out encPwd, out email))
        {
            string password = objCrypto.DescifrarCadena(encPwd);
            if (SendEmail(email, txtCedula.Text, password))
                ShowSuccessMessage("Su contraseña ha sido enviada a " + email);
            else
                ShowErrorMessage("Su contraseña no pudo ser enviada a " + email);
        }
        else
            ShowErrorMessage(oUsuarioEFOT.Error);

    }

    protected bool SendEmail(string emailTo, string numCedula, string password)
    {
        try
        {
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress(Constantes.ParametrosEnvioEmail.mailAddress);
            correo.To.Add(emailTo);
            correo.Subject = "Contraseña de aspirante a la EFOT";
            string body = "Saludos,<br /><br />\n\nEste mensaje fue generado automáticamente para recordarle su contraseña de acceso como aspirante a ingresar a la Escuela de Formación de Oficiales y Tropa de la Comisión de Tránsito del Ecuador.";
            body += "<br /><br />\n\nPara terminar el proceso de registro debe ingresar a www.cte.gob.ec/EFOT, ingresar sus datos académicos, referencias personales y agregue direcciones de contacto en caso de ser necesario. <br /><br />\n\n";
            body += "Debe ingresar su usuario: " + numCedula + " y contraseña: " + password;
            correo.Body = body;
            correo.IsBodyHtml = true;
            correo.Priority = System.Net.Mail.MailPriority.High;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = Constantes.ParametrosEnvioEmail.smtpHost;
            smtp.Credentials = new System.Net.NetworkCredential(Constantes.ParametrosEnvioEmail.smtpUser, Constantes.ParametrosEnvioEmail.smptPassword);

            smtp.Send(correo);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void ShowErrorMessage(string message)
    {
        lblError.Text = message;
        divError.Style.Value = "visibility: visible; background-color: #fbbcc8";
        divSuccess.Style.Value = "visibility: hidden";
        pnlForm.Visible = false;
        ScriptManager.RegisterStartupScript(UpdatePanel2, UpdatePanel2.GetType(), "click", "Rounded('div#divError', '#FFFFFF', '#fbbcc8');", true);
    }

    protected void ShowSuccessMessage(string message)
    {
        lblSuccess.Text = message;
        divError.Style.Value = "visibility: hidden";
        divSuccess.Style.Value = "visibility: visible; background-color: #cfe7f2";
        pnlForm.Visible = false;
        ScriptManager.RegisterStartupScript(UpdatePanel2, UpdatePanel2.GetType(), "click", "Rounded('div#divSuccess', '#FFFFFF', '#cfe7f2');", true);
    }
    protected void btnTryAgain_Click(object sender, EventArgs e)
    {
        pnlForm.Visible = true;
        divError.Style.Value = "visibility: hidden";
        divSuccess.Style.Value = "visibility: hidden";
    }
}