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

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session["redirectURL"] = GetOriginURL();
            SetDefaultUser();
        }
    }

    protected string GetOriginURL()
    {
        return Request.QueryString["ReturnUrl"];
        //string url = Request.QueryString["ReturnUrl"];
        ///CTGWebApps/Pagos/Requisitos.aspx?codCatTramite=ATU
        //return url.Substring(12);
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        UsuarioWeb usrweb = new UsuarioWeb(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], this.Login1.UserName, this.Login1.Password);
        usrweb.LogIn();

        if (usrweb.isLoggedIn)
        {
            SaveCookieUsername(usrweb.Username);
            SaveUserIPDNSAgent();
 
            if(usrweb.isVerified)
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

    protected void SaveCookieUsername(string username)
    {
        HttpCookie addCookie = new HttpCookie("webtransctg");
        addCookie.Expires = DateTime.Today.AddDays(1).AddSeconds(-1);
        addCookie.Value = username;
        Response.Cookies.Add(addCookie);
    }

    protected void SetDefaultUser()
    {
        HttpCookie cogeCookie = Request.Cookies.Get("webtransctg");
        if (cogeCookie != null)
            this.Login1.UserName = cogeCookie.Value;
    }

    protected void SaveUserIPDNSAgent()
    {
        Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        Session[Constantes.UsuarioWeb.SessionIP] = objCrypto.CifrarCadena(Request.UserHostAddress);
        Session[Constantes.UsuarioWeb.SessionAgent] = objCrypto.CifrarCadena(Request.UserAgent);
        Session[Constantes.UsuarioWeb.SessionDNS] = objCrypto.CifrarCadena(Request.UserHostName);
    }

    
}
