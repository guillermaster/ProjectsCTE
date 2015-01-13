using System;
using System.Configuration;
using System.Web.UI;
using Seguridad;

public partial class LoginExtra_LostPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSolicitar_Click(object sender, EventArgs e)
    {
        UsuarioWeb newuser = new UsuarioWeb(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()], txtCedula.Text);
        if (newuser.SendLostPassword())
            ShowSuccessMessage("Su contraseña ha sido enviada a <b>" + newuser.UserEmail + "</b>");
        else
            ShowFailureMessage(newuser.Error);
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void ShowFailureMessage(string message)
    {
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "click", "csscody.error('<b>Error</b><br /><br />" + message + "')", true);
    }

    protected void ShowSuccessMessage(string message)
    {
        ScriptManager.RegisterStartupScript(UpdatePanel1, GetType(), "click", "csscody.info('" + message + "')", true);
    }
}
