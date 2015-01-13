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

public partial class Logon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        String username = this.Login1.UserName.ToString();
        String password = this.Login1.Password.ToString();

        //UsuarioBD db_cnx = new UsuarioBD(sUsuario, sClave, sServidor);
        UsuarioCertificadoLicencia db_user = new UsuarioCertificadoLicencia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], username);


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
                        /*this.Login1.Visible = false;
                        this.hdUsername.Value = username;
                        this.tblChangePwd.Visible = true;*/
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
        }
    }
}
