using System;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using Seguridad;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.HideShortcutMenu();
        SetEstadoUsuario();
        SetLastLoggedInDate();
        ValidateUserBrowser();
        if (HttpContext.Current.User == null || !HttpContext.Current.User.Identity.IsAuthenticated)
        {
            pnlUserData.Visible = false;
            pnlLogin.Visible = true;
        }
        else
            pnlUserData.Visible = true;
    }

    protected void SetEstadoUsuario()
    {
        try
        {
            if(Session[Constantes.UsuarioWeb.SessionVarNameFullAccess].ToString() == Constantes.UsuarioWeb.SessionFullAccess)
            {
                acrEstado.Attributes["title"] = string.Empty;
                lblEstado.Text = "Activada y verificada";
                imgIcoEstado.ImageUrl = "images/icoStatusFull.png";
            }
            else if(Session[Constantes.UsuarioWeb.SessionVarNameFullAccess].ToString() == Constantes.UsuarioWeb.SessionLimitedAccess)
            {
                acrEstado.Attributes["title"] = "Su cuenta se encuentra activada, pero usted no se ha acercado a Atención al Usuario de la CTE para verificar su autenticidad.";
                lblEstado.Text = "No verificada";
                imgIcoEstado.ImageUrl = "images/icoStatusNotVerified.png";
            }
            else
            {
                acrEstado.Attributes["title"] = "Debe activar su cuenta, lea las instrucciones del correo electrónico de activación que se le envió luego de completar el registro de su cuenta.";
                lblEstado.Text = "No verificada";
                imgIcoEstado.ImageUrl = "images/icoStatusNone.png";
            }
            
        }
        catch
        {
            acrEstado.Attributes["title"] = "Ocurrió un error al verificar el estado de su cuenta.";
            lblEstado.Text = "Error";
            imgIcoEstado.ImageUrl = "images/icoStatusNone.png";
        }
    }


    protected void SetLastLoggedInDate()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(Session[Constantes.UsuarioWeb.SessionUserLastAccess].ToString()))
            {
                lblLastLoggedInDate.Text = Convert.ToDateTime(Session[Constantes.UsuarioWeb.SessionUserLastAccess].ToString()).ToString("dd/MMMM/yyyy", System.Globalization.CultureInfo.GetCultureInfo("es-ES"));
            }
            else
            {
                lblLastLoggedInDate.Text = "Es su primer acceso";
            }
        }
        catch
        {
            lblLastLoggedInDate.Text = DateTime.Now.ToString();
        }
    }

    protected void ValidateUserBrowser()
    {
        HttpBrowserCapabilities browser = Request.Browser;
        BrowserNotSupported1.Visible = !Utilities.Utils.BrowserIsSupported(browser.Browser, browser.Version);
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

}
