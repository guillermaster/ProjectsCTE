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

public partial class _Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["redirectURL"] =  FormsAuthentication.GetRedirectUrl("test", false);
        Page.Form.DefaultFocus = this.Login1.ID;
    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        UsuarioVerificador oUsuario = new UsuarioVerificador(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], this.Login1.UserName, this.Login1.Password);
        oUsuario.LogIn();

        if (oUsuario.isLoggedIn)
        {
            SaveUserIPDNSAgent();
            if(oUsuario.Error==Constantes.UsuarioWeb.ErrorMsgUserMustChangePassword)
                Session["firstTime"] = "1";
            else
                Session["firstTime"] = "0";
            FormsAuthentication.RedirectFromLoginPage(oUsuario.Username, false);
        }
        else
        {
            Login1.FailureText = oUsuario.Error;
        }
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
