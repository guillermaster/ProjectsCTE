using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using SeguridadWebAppInt;

public partial class Logon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

        UsuarioWebAppInt oUsuario = new UsuarioWebAppInt(Login1.UserName, objCrypto,
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        if (oUsuario.LogIn(Login1.Password))
        {
            bool isUser = false;
            Session[InformesDirectorioExtra.Parametros.Session.IsAdmin] = false;
            string rolUsuario = string.Empty;
            string rolAdministrador = string.Empty;
            switch (ConfigurationManager.AppSettings[InformesDirectorioExtra.Parametros.InstitucionConfigPar])
            {
                case InformesDirectorioExtra.Parametros.InstitucionesPermitidas.ComisionTransitoEcuador.Codigo:
                    rolUsuario = InformesDirectorioExtra.Parametros.InstitucionesPermitidas.ComisionTransitoEcuador.RolUsuarioAppWeb;
                    rolAdministrador = InformesDirectorioExtra.Parametros.InstitucionesPermitidas.ComisionTransitoEcuador.RolUsuarioAdminAppWeb;
                    break;
                case InformesDirectorioExtra.Parametros.InstitucionesPermitidas.AgenciaNacionalTransito.Codigo:
                    rolUsuario = InformesDirectorioExtra.Parametros.InstitucionesPermitidas.AgenciaNacionalTransito.RolUsuarioAppWeb;
                    rolAdministrador = InformesDirectorioExtra.Parametros.InstitucionesPermitidas.AgenciaNacionalTransito.RolUsuarioAdminAppWeb;
                    break;
            }
            
            if (oUsuario.LoadRolesByUser())
            {
                for (int i = 0; i < oUsuario.Roles.Count; i++)
                {
                    if (oUsuario.Roles[i].ToString() == rolAdministrador)
                    {
                        isUser = true;
                        Session[InformesDirectorioExtra.Parametros.Session.IsAdmin] = true;
                    }
                    if (oUsuario.Roles[i].ToString() == rolUsuario)
                        isUser = true;
                }
                if (isUser)
                {
                    Session[InformesDirectorioExtra.Parametros.Session.FolderId] = 0;
                    FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);
                }
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