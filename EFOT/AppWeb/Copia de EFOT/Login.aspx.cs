using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CifradoCs;
using EFOTclass;
using Constantes;
using System.Web.Security;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        Crypto crypto = new Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        crypto.Key = ParametrosCifradoCs.key;
        crypto.IV = ParametrosCifradoCs.iv;
        User user = new User(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        string encPwdFromDB = user.GetEncPwdFromDB(this.Login1.UserName);
        if (string.IsNullOrWhiteSpace(encPwdFromDB) || crypto.DescifrarCadena(encPwdFromDB) == this.Login1.Password)
        {
            FormsAuthentication.RedirectFromLoginPage(this.Login1.UserName, false);
        }
        else
            this.Login1.FailureText = "La contraseña ingresada es incorrecta";
    }
}