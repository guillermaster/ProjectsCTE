using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Account_ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void CancelPushButton_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    {
        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(HttpContext.Current.User.Identity.Name, objCrypto,
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        if (oUsuario.LogIn(CurrentPassword.Text))
        {
            if (oUsuario.ChangePassword(CurrentPassword.Text, NewPassword.Text))
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('¡La contraseña fue cambiada exitosamente! Ahora debe ingresar con su nueva contraseña');", true);
                LogOut();
            }
            else
                FailureText.Text = oUsuario.Error;
        }
        else
            FailureText.Text = oUsuario.Error;
    }

    private void LogOut()
    {
        Session.RemoveAll();
        Session.Abandon();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }
}
