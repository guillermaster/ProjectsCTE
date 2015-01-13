using System;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls;
using SeguridadWebAppInt;

public partial class Logon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        String username = Login1.UserName;
        String password = Login1.Password;

        //UsuarioBD db_cnx = new UsuarioBD(sUsuario, sClave, sServidor);
        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        UsuarioWebAppInt oUsuario = new UsuarioWebAppInt(username, objCrypto,ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);


        if (oUsuario.LogIn(password))
        {
            //Session["Username"] = username;
            //if (oUsuario.UsuarioActivo)
            //{
                AEA.Parametros oPar = new AEA.Parametros(username, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
                string codEmpresa;
                Session[AEA.Parametros.SessionVarNombreEmpresa] = oPar.RetornaEmpresaUsuario(out codEmpresa);
                Session[AEA.Parametros.SessionVarCodEmpresa] = codEmpresa;
                FormsAuthentication.RedirectFromLoginPage(username, false);
            //}
        }
        else
        {
            Login1.FailureText = oUsuario.Error;
        }
    }
}
