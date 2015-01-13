using System;
using System.Configuration;
using Seguridad;

public partial class LoginExtra_ResendActivationEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        UsuarioWeb newuser = new UsuarioWeb(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()], txtCedula.Text);
        if (newuser.ReSendActivationEmail())
            ShowSuccessMessage("Su e-mail de activación ha sido enviado a <b>" + newuser.UserEmail + "</b>");
        else
            ShowFailureMessage(newuser.Error);
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void ShowFailureMessage(string message)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "click", "csscody.error('<b>Error</b><br /><br />" + message + "')", true);
    }

    protected void ShowSuccessMessage(string message)
    {
        System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "click", "csscody.info('" + message + "')", true);
    }
}
