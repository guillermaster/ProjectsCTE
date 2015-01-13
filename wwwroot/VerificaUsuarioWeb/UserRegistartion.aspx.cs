using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Seguridad;
using CifradoCs;

public partial class LoginExtra_UserRegistartion : System.Web.UI.Page
{
    private Random rand = new Random();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Seguridad.UsuarioWeb.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }

        if (!Page.IsPostBack)
        {
            Page.Form.DefaultFocus = this.txtCedula.ID;
            Page.Form.DefaultButton = this.btnRegistrar.ID;
        }
    }

    

    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        string newuser_password = Utilities.Utils.CreateRandomPassword(8);
        UsuarioWeb newuser = new UsuarioWeb(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], this.txtCedula.Text, objCrypto.CifrarCadena(newuser_password));
        if (newuser.Register(this.txtNombres.Text, this.txtApellidos.Text, this.txtEmail.Text, Request.UserHostAddress, System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")))
        {
            this.lblMensaje.Text = "El usuario " + this.txtCedula.Text + " ha sido creado. <br /><br /> La contraseña del nuevo usuario es " + newuser_password;
            this.lnkContinuar.Visible = true;
            this.divForm.Visible = false;

            #region "Envía contraseña por email"
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress(Constantes.ParametrosEnvioEmail.mailAddress);
            correo.To.Add(this.txtEmail.Text);
            correo.Subject = "Contraseña de acceso en www.ctg.gov.ec";
            correo.Body = "Saludos " + this.txtNombres.Text + ",\n\nEste mensaje fue generado automáticamente por www.ctg.gov.ec al recibir su solicitud de registro de usuario desde las oficinas de la CTG.\n\nSu contraseña de acceso es: " + newuser_password + "\n";
            correo.IsBodyHtml = false;
            correo.Priority = System.Net.Mail.MailPriority.High;
            //
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            //---------------------------------------------
            // Estos datos debes rellanarlos correctamente
            //---------------------------------------------
            smtp.Host = Constantes.ParametrosEnvioEmail.smtpHost;
            smtp.Credentials = new System.Net.NetworkCredential(Constantes.ParametrosEnvioEmail.smtpUser, Constantes.ParametrosEnvioEmail.smptPassword);
            //smtp.EnableSsl = false;
            try
            {
                newuser.ActualizaDato(Constantes.UsuarioWeb.DBFieldNameEstado, Constantes.UsuarioWeb.EstadoVerificado);
                newuser.ActualizaDato(Constantes.UsuarioWeb.DBFieldNameEstadoRegistro, Constantes.UsuarioWeb.EstadoActivo);
                smtp.Send(correo);
                this.lblMensaje.Text += "<br /><br />Se envió otro email a " + this.txtEmail.Text + " en el cual se le dice al usuario cual es su contraseña.";
            }
            catch (Exception ex)
            {
                this.lblMensaje.Text = ex.Message;
            }
            #endregion

        }
        else
        {
            this.lblMensaje.Text = newuser.Error;
        }
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }
    
}
