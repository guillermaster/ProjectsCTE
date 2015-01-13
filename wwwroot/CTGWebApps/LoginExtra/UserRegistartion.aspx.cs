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

public partial class LoginExtra_UserRegistartion : System.Web.UI.Page
{
    private Random rand = new Random();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["CaptchaImageText"] = GenerateRandomCode();
        }
    }

    protected string GenerateRandomCode()
    {
        string s = "";
        for(int i=0; i<6; i++)
        {
            s = s + this.rand.Next(10).ToString();
        }
        return s;
    }
    
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        if (this.txtContrasena.Text != string.Empty && this.txtContrasena.Text.Length > 0)
        {
            if (Session["CaptchaImageText"].ToString() == this.txtCaptchaCode.Text)
            {
                Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                UsuarioWeb newuser = new UsuarioWeb(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], this.txtCedula.Text, objCrypto.CifrarCadena(this.txtContrasena.Text.Trim()));
                if (newuser.Register(this.txtNombres.Text, this.txtApellidos.Text, this.txtEmail.Text, Request.UserHostAddress, System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")))
                {
                    ShowSuccessMessage(this.txtEmail.Text);
                    this.divForm.Visible = false;
                }
                else
                {
                    ShowFailureMessage(newuser.Error);
                }
            }
            else
                ShowFailureMessage("ERROR: El código ingresado de verificación ingresado no coincide con el mostrado en la imagen");
        }
        else
            ShowFailureMessage("ERROR: Debe ingresar la contraseña de acceso deseada.");

    }


    protected void ShowSuccessMessage(string email)
    {
        this.divError.Visible = false;
        this.divConfirmation.Visible = true;
        this.lblEmailSent.Text = email;
    }


    protected void ShowFailureMessage(string message)
    {
        this.divConfirmation.Visible = false;
        this.divError.Visible = true;
        this.lblMsgError.Text = message;
    }

    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        if (Session["redirectURL"] != null)
            Response.Redirect("../.." + Session["redirectURL"].ToString());
        else
            Response.Redirect("../DefaultConsultas.aspx");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (Session["redirectURL"] != null)
            Response.Redirect("../.." + Session["redirectURL"].ToString());
        else
            Response.Redirect("../DefaultConsultas.aspx");
    }
}
