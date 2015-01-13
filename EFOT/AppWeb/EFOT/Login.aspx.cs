using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
    }


    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

        EFOTclass.User oUsuarioEFOT = new EFOTclass.User(System.Configuration.ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            System.Configuration.ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            System.Configuration.ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        string encPwd = oUsuarioEFOT.GetEncPwdFromDB(Login1.UserName);

        if (!string.IsNullOrWhiteSpace(encPwd))
        {
            if (objCrypto.DescifrarCadena(encPwd) == Login1.Password)
                FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);
            else
                Login1.FailureText = "La contraseña ingresada es incorrecta";
        }
        else
            Login1.FailureText = "La contraseña ingresada es incorrecta.";
    }
}
