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
            this.tblChangePwd.Visible = false;
        }
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
                SaveUserIPDNSAgent();
                //Session["Loggedin"] = "Yes";
                Session["Username"] = username;
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
                        this.Login1.Visible = false;
                        this.hdUsername.Value = username;
                        this.tblChangePwd.Visible = true;
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


    protected void SaveUserIPDNSAgent()
    {
        Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        Session[Constantes.UsuarioWeb.SessionIP] = objCrypto.CifrarCadena(Request.UserHostAddress);
        Session[Constantes.UsuarioWeb.SessionAgent] = objCrypto.CifrarCadena(Request.UserAgent);
        Session[Constantes.UsuarioWeb.SessionDNS] = objCrypto.CifrarCadena(Request.UserHostName);
        //Session["IP"] = objCrypto.CifrarCadena(Request.UserHostAddress);
        //Session["DNS"] = objCrypto.CifrarCadena(Request.UserHostName);
        //Session["Agent"] = objCrypto.CifrarCadena(Request.UserAgent);
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string current_password = this.txtCurrPwd.Text;
            string new_password = this.txtNewPwd.Text;
            string new_password_confirm = this.txtNewPwdConf.Text;
            string errChangePwd;

            UsuarioCertificadoLicencia db_user = new UsuarioCertificadoLicencia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], this.hdUsername.Value);
            Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
            objCrypto.Key = Constantes.ParametrosCifradoCs.key;
            objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
            errChangePwd = db_user.ChangePassword(objCrypto.CifrarCadena(current_password), objCrypto.CifrarCadena(new_password));

            if (errChangePwd != null)//si existe un error
            {
                this.lblError.Text = errChangePwd;
                this.lblError.Visible = true;
            }
            else
            {
                SaveUserIPDNSAgent();
                //Session["Loggedin"] = "Yes";
                Session["Username"] = this.hdUsername.Value;
                FormsAuthentication.RedirectFromLoginPage(this.hdUsername.Value, false);
            }
        }
    }
}
