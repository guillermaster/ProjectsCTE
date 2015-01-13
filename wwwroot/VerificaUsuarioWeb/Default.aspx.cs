using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class _Default : System.Web.UI.Page 
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Seguridad.UsuarioWeb.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }

        if (!Page.IsPostBack)
        {
            if (Session["firstTime"].ToString() == "1")
            {
                this.txtIdentificacion.Enabled = false;
                this.lblMensaje.Text = Constantes.UsuarioWeb.ErrorMsgUserMustChangePassword;
            }
            clear();
        }
        Page.Form.DefaultButton = this.btnConsultar.ID;
        Page.Form.DefaultFocus = this.txtIdentificacion.ID;
    }


    protected void ConsultarDatos(string identificacion)
    {
        Seguridad.UsuarioWeb oUsuarioWeb = new Seguridad.UsuarioWeb(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], identificacion);
        string[] datos_usuarioweb = new string[5];
        datos_usuarioweb = oUsuarioWeb.ObtenerDatosUsuarioWeb();

        this.hdnNewPwd.Value = "";
        this.btnRetrySendPwd.Visible = false;

        if (datos_usuarioweb[0] != "error")
        {
            this.resultados.Visible = true;
            this.editNombres.Value = datos_usuarioweb[0];
            this.editApellidos.Value = datos_usuarioweb[1];
            this.editEmail.Value = datos_usuarioweb[2];
            this.lblEstado.Text = datos_usuarioweb[3];
            this.lblIdentificacion.Text = txtIdentificacion.Text;

            InicializarEditFields();

            if (datos_usuarioweb[3] == Constantes.UsuarioWeb.EstadoNoVerificado)
            {
                this.btnChangeStateToVerified.Visible = true;
            }
            else
            {
                this.btnChangeStateToVerified.Visible = false;
            }

            this.lblMensaje.Text = oUsuarioWeb.PersonaExisteEnAxis(identificacion);
        }
        else
        {
            this.btnChangeStateToVerified.Visible = false;
            if (datos_usuarioweb[1] == Constantes.UsuarioWeb.CodErrorUsuarioNoExiste)
            {
                this.lblMensaje.Text = "Usuario con identificación " + identificacion + " no existe.";
                this.lnkRegistrar.Visible = true;
            }
            if (datos_usuarioweb[1] == Constantes.UsuarioWeb.CodErrorUsuarioNoExisteEnCTG)
            {
                this.lblMensaje.Text = "Usuario con identificación " + identificacion + " no registra ningún tipo de contrato en la CTG.";
                this.lnkRegistrar.Visible = true;////borrar esto despues de autoshow
            }
            if (datos_usuarioweb[1] == Constantes.UsuarioWeb.CodErrorUsuarioInactivoEnCTG)
            {
                this.lblMensaje.Text = "Usuario con identificación " + identificacion + " si registra contrato en la CTG, pero está inactivo.";
                this.lnkRegistrar.Visible = true;////borrar esto despues de autoshow
            }
            else
            {
                this.lblMensaje.Text = datos_usuarioweb[1];
            }
        }
    }


    protected void clear()
    {
        this.lblMensaje.Text = "";
        this.lblMensajeCambioEstado.Text = "";
        this.editNombres.Init();
        this.editApellidos.Init();
        this.editEmail.Init();
        this.lnkRegistrar.Visible = false;
        this.btnRetrySendPwd.Visible = false;
        this.resultados.Visible = false;
        this.lblInfoMessage.Visible = false;
    }

    protected void InicializarEditFields()
    {
        this.editNombres.setCampo(Constantes.UsuarioWeb.DBFieldNameNombres);
        this.editNombres.setIdentificacion(this.txtIdentificacion.Text);
        this.editNombres.setMaxLength(100);
        this.editApellidos.setCampo(Constantes.UsuarioWeb.DBFieldNameApellidos);
        this.editApellidos.setIdentificacion(this.txtIdentificacion.Text);
        this.editApellidos.setMaxLength(100);
        this.editEmail.setCampo(Constantes.UsuarioWeb.DBFieldNameEmail);
        this.editEmail.setIdentificacion(this.txtIdentificacion.Text);
        this.editApellidos.setMaxLength(100);
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        clear();
        if (this.txtIdentificacion.Text.Trim().Length > 0)
            ConsultarDatos(this.txtIdentificacion.Text.Trim());
        Page.Form.DefaultFocus = this.txtIdentificacion.ID;
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        clear();
        Page.Form.DefaultFocus = this.txtIdentificacion.ID;
    }

    protected void btnChangeStateToVerified_Click(object sender, ImageClickEventArgs e)
    {
        Seguridad.UsuarioWeb oUsuarioWeb = new Seguridad.UsuarioWeb(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], this.txtIdentificacion.Text);
        //if (oUsuarioWeb.ActualizaEstadoVerificado())
        oUsuarioWeb.ActualizaDato(Constantes.UsuarioWeb.DBFieldNameEstadoRegistro, Constantes.UsuarioWeb.EstadoActivo);
        if(oUsuarioWeb.ActualizaDato(Constantes.UsuarioWeb.DBFieldNameEstado, Constantes.UsuarioWeb.EstadoVerificado))
        {
            this.lblEstado.Text = Constantes.UsuarioWeb.EstadoVerificado;
            this.btnChangeStateToVerified.Visible = false;
            this.lblMensajeCambioEstado.Text = "Se ha modificado el estado correctamente";
            this.lblInfoMessage.Visible = true;
            ModificaContrasena(oUsuarioWeb);
        }
        else
        {
            this.lblMensaje.Text = "No se pudo modificar el estado.  " + oUsuarioWeb.Error;
            this.lnkRegistrar.Visible = true;
        }
    }

    protected void ModificaContrasena(Seguridad.UsuarioWeb oUsuarioWeb)
    {
        //actualizar password
        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        string newuser_password = Utilities.Utils.CreateRandomPassword(8);
        string enc_newuser_password = objCrypto.CifrarCadena(newuser_password);
        if (oUsuarioWeb.ActualizaDato(Constantes.UsuarioWeb.DBFieldNameContrasena, enc_newuser_password))
        {
            this.hdnNewPwd.Value = enc_newuser_password;
            this.lblMensaje.Text = "La nueva contraseña es " + newuser_password + "<br /><br />";
            if (EnviaContrasena(newuser_password))
            {
                this.lblMensaje.Text += "La nueva contraseña se envió por email a " + this.editEmail.Value + " exitósamente";
                this.btnRetrySendPwd.Visible = false;
            }
            else
            {
                this.lblMensaje.Text += "No se pudo enviar la nueva contraseña por email a " + this.editEmail.Value;
                this.btnRetrySendPwd.Visible = true;
            }
        }
    }

    protected bool EnviaContrasena(string pwd)
    {
        //
        try
        {
        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
        correo.From = new System.Net.Mail.MailAddress("Comisión de Tránsito del Ecuador <" + Constantes.ParametrosEnvioEmail.mailAddress + ">");
        correo.To.Add(this.editEmail.Value);
        correo.Bcc.Add("jjaimec@cte.gob.ec");
        correo.Bcc.Add("dvaldano@cte.gob.ec");
        correo.Bcc.Add("gmontalvan@cte.gob.ec");
        correo.Subject = "Contraseña de acceso en www.cte.gob.ec";
        correo.Body = "Saludos " + this.editNombres.Value + ",\n\nEste mensaje fue generado automáticamente por www.cte.gob.ec al recibir su solicitud de verificación de identidad en las oficinas de la CTE.\n\nSu contraseña de acceso es: " + pwd + "\n";
        correo.IsBodyHtml = false;
        correo.Priority = System.Net.Mail.MailPriority.High;
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
            this.lblMensaje.Text = "Error al enviar contraseña. " + ex.Message;
            return false;
        }
        
    }

    protected void btnRetrySendPwd_Click(object sender, EventArgs e)
    {
        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        if (EnviaContrasena(objCrypto.DescifrarCadena(this.hdnNewPwd.Value)))
            this.btnRetrySendPwd.Visible = false;
    }
}
