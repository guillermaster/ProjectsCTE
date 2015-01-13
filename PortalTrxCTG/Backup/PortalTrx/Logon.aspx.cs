using System;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using Seguridad;

public partial class Logon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ValidateUserBrowser();
    }


    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        UsuarioWeb usrweb = new UsuarioWeb(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()], Login1.UserName, Login1.Password);
        usrweb.LogIn();

        if (usrweb.isLoggedIn)
        {
            //SaveUserIPDNSAgent();
            SaveBasicUserDataOnSessionVars(usrweb, usrweb.FechaUltimoAcceso);

            if (usrweb.isVerified)
                Session[Constantes.UsuarioWeb.SessionVarNameFullAccess] = Constantes.UsuarioWeb.SessionFullAccess;
            else
                Session[Constantes.UsuarioWeb.SessionVarNameFullAccess] = Constantes.UsuarioWeb.SessionLimitedAccess;
            FormsAuthentication.RedirectFromLoginPage(usrweb.Username, false);
        }
        else
        {
            Login1.FailureText = usrweb.Error;
        }
    }


    protected void SaveBasicUserDataOnSessionVars(UsuarioWeb oUserWeb, string fechaUltimoAcceso)
    {
        string[] userData = oUserWeb.ObtenerDatosUsuarioWeb();
        Session[Constantes.UsuarioWeb.SessionUserNames] = userData[0];
        Session[Constantes.UsuarioWeb.SessionUserLastNames] = userData[1];
        Session[Constantes.UsuarioWeb.SessionUserEmail] = userData[2];
        Session[Constantes.UsuarioWeb.SessionUserLastAccess] = fechaUltimoAcceso;
    }

    protected void ValidateUserBrowser()
    {
        HttpBrowserCapabilities browser = Request.Browser;
        BrowserNotSupported1.Visible = !Utilities.Utils.BrowserIsSupported(browser.Browser, Convert.ToDouble(browser.Version).ToString());
    }
}
