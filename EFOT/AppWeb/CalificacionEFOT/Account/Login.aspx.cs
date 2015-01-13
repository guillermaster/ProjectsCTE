using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(Login1.UserName, objCrypto,
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        if (oUsuario.LogIn(Login1.Password))
        {
            if (oUsuario.LoadRolesByUser())
            {
                bool hasPrivileges = false;
                Session[EFOTclass.Parametros.Session.Roles.ReversaCalificacion] = false;
                for (int i = 0; i < oUsuario.Roles.Count; i++)
                {
                    if (oUsuario.Roles[i].ToString() == EFOTclass.Parametros.Session.Roles.Instructor ||
                        oUsuario.Roles[i].ToString() == EFOTclass.Parametros.Session.Roles.InstructorCalificador)
                    {
                        hasPrivileges = true;
                        if(oUsuario.Roles[i].ToString() == EFOTclass.Parametros.Session.Roles.InstructorCalificador)
                            Session[EFOTclass.Parametros.Session.Roles.InstructorCalificador] = true;
                    }
                    if (oUsuario.Roles[i].ToString() == EFOTclass.Parametros.Session.Roles.ReversaCalificacion)
                        Session[EFOTclass.Parametros.Session.Roles.ReversaCalificacion] = true;
                }
                if (hasPrivileges)
                    FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);
                else
                    Login1.FailureText = "No tiene privilegio para acceder a esta página.";
            }
            else
                Login1.FailureText = oUsuario.Error;
        }
        else
            Login1.FailureText = oUsuario.Error;
    }
}
