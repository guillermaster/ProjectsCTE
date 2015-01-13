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

public partial class UserInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtUsername.Text = HttpContext.Current.User.Identity.Name.ToString();
        }
    }
    protected void btnChangePwd_Click(object sender, EventArgs e)
    {
        this.tblChangePwd.Visible = true;
        this.btnChangePwd.Visible = false;
    }
    protected void btnModificar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            lblError.Text = "";
            if (login(txtUsername.Text, txtCurrPwd.Text))
            {
                if (txtNewPwd.Text == txtNewPwdConf.Text)
                {
                    if (txtNewPwd.Text != txtCurrPwd.Text)
                    {
                        UpdatePassword(txtUsername.Text, txtCurrPwd.Text, txtNewPwd.Text);
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

    protected bool login(string username, string password)
    {

        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(username, objCrypto,
            ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.tns.ToString()]);


        if (oUsuario.LogIn(password))
            return true;
        else
            return false;
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
        {
            hidAlert.Value = "La contraseña fue modificada exitósamente.";
            tblChangePwd.Visible = false;
            btnChangePwd.Visible = true;
        }
        else
            hidAlert.Value = oUsuario.Error;
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.tblChangePwd.Visible = false;
        this.btnChangePwd.Visible = false;
    }
}
