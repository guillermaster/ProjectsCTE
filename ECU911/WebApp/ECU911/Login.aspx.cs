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
using SeguridadWebAppInt;
using CifradoCs;

public partial class Logon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadNewsFeed();
        }
    }

    protected void LoadNewsFeed()
    {
        /*CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;*/
        Utilities.RSS oRSS = new Utilities.RSS();
        System.Xml.XmlDocument xmlFeed = oRSS.GetRssFeed("http://www.cte.gob.ec/category/noticia/feed/");/*,
            objCrypto.DescifrarCadena(ConfigurationManager.AppSettings["proxyusr"]),
            objCrypto.DescifrarCadena(ConfigurationManager.AppSettings["proxypwd"]));*/
        if (xmlFeed.FirstChild != null)
        {
            gvNoticias.DataSource = oRSS.GetCteNewsMostRecentLinks(xmlFeed);
            gvNoticias.DataBind();
        }
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

            /*if (oUsuario.LoadRolesByUser())
            {*/
            /*for (int i = 0; i < oUsuario.Roles.Count; i++)
            {
                if (oUsuario.Roles[i].ToString() == rolAdministrador)
                {
                    isUser = true;
                    Session[InformesDirectorioExtra.Parametros.Session.IsAdmin] = true;
                }
                if (oUsuario.Roles[i].ToString() == rolUsuario)
                    isUser = true;
            }*/
            //DESCOMENTAR LO ANTERIOR Y SETEAR EL ROL
            /*if (isUser)
            {*/
            FormsAuthentication.RedirectFromLoginPage(Login1.UserName, false);
            /*}
            else
                Login1.FailureText = "No tiene privilegio para acceder a esta página.";*/
            /*}
            else
                Login1.FailureText = oUsuario.Error;*/
        }
        else
        {
            Login1.FailureText = oUsuario.Error;
            
        }
        /*
        if (db_user.LogInSinRoles())//si no existe error
        {
            Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
            objCrypto.Key = Constantes.ParametrosCifradoCs.key;
            objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
            if (password == objCrypto.DescifrarCadena(db_user.Password))//verificar contraseña
            {
                //SaveUserIPDNSAgent();
                //Session["Loggedin"] = "Yes";
                //Session["Username"] = username;
                FormsAuthentication.RedirectFromLoginPage(username.ToString(), false);
            }
            else//contraseña incorrecta
            {
                this.Login1.FailureText = "Contraseña incorrecta";
            }
        }
        else//existe error
        {
            switch (db_user.Error)
            {
                case "1":
                    this.Login1.FailureText = "Usuario no puede ser nulo";
                    break;
                case "2":
                    this.Login1.FailureText = "Usuario no existe";
                    break;
                case "3":
                    //desencriptar
                    Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                    objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                    objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                    string password_on_db = objCrypto.DescifrarCadena(db_user.Password);
                    if (password_on_db == password)
                    {
                        FormsAuthentication.RedirectFromLoginPage(username.ToString(), false);
                    }
                    else
                    {
                        this.Login1.FailureText = "Contraseña incorrecta";
                    }
                    break;
                default:
                    this.Login1.FailureText = "Error desconocido - " + db_user.Error;
                    break;
            }
        }*/
    }
}
