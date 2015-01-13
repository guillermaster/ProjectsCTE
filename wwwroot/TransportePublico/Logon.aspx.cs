using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Logon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        string username = this.Login1.UserName.ToString();
        string password = this.Login1.Password.ToString();

        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(username, objCrypto, 
            ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.tns.ToString()]);


        if (oUsuario.LogIn(password))
        {
            if (oUsuario.UsuarioActivo)
                InitSession(Login1.UserName);
            else
            {
                this.hdnUsername.Value = Login1.UserName;
                this.hdnCurrPassword.Value = Login1.Password;
                this.Login1.Visible = false;
                this.tblChangePwd.Visible = true;
            }
        }
        else
        {
            this.Login1.FailureText = oUsuario.Error;
        }
    }

    protected void btnModificar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError.Text = "";
            if (txtCurrPwd.Text == hdnCurrPassword.Value)
            {
                if (txtNewPwd.Text == txtNewPwdConf.Text)
                {
                    if (txtNewPwd.Text != txtCurrPwd.Text)
                    {
                        UpdatePassword(hdnUsername.Value, txtCurrPwd.Text, txtNewPwd.Text);
                    }
                    else
                        lblError.Text = "La nueva contraseña debe ser distinta a la actual";
                }
                else
                    lblError.Text = "La confirmación de la nueva contraseña no coincide";
            }
            else
                lblError.Text = "La contraseña actual es incorrecta.";
        }
    }

    protected void InitSession(string username)
    {
        TransPublico.Parametros oPar = new TransPublico.Parametros(ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.tns.ToString()],
                    ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.clave.ToString()],
                    ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.usuario.ToString()]);
        Session[TransPublico.Parametros.DatosSesion.NombreEmpresa.ToString()] = oPar.RetornaEmpresaUsuario(username);
        SaveUserIPDNSAgent();
        FormsAuthentication.RedirectFromLoginPage(username, false);
    }

    protected void UpdatePassword(string user, string currentPwd, string newPwd)
    {
        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(user, objCrypto, 
            ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.tns.ToString()]);

        if (oUsuario.ChangePassword(currentPwd, newPwd))
            InitSession(user);
        else
            this.lblError.Text = oUsuario.Error;
    }

    protected void SaveUserIPDNSAgent()
    {
        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        Session[Constantes.UsuarioWeb.SessionIP] = objCrypto.CifrarCadena(Request.UserHostAddress);
        Session[Constantes.UsuarioWeb.SessionAgent] = objCrypto.CifrarCadena(Request.UserAgent);
        Session[Constantes.UsuarioWeb.SessionDNS] = objCrypto.CifrarCadena(Request.UserHostName);
    }
}
