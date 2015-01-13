using System;
using System.Configuration;
using CifradoCs;
using Seguridad;

public partial class LoginExtra_UserRegistartion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Session["CaptchaImageText"] = GenerateRandomCode();
        }
    }

    protected string GenerateRandomCode()
    {
        Random rand = new Random();
        string s = string.Empty;
        for (int i = 0; i < 6; i++)
        {
            s = s + rand.Next(10).ToString();
        }
        return s;
    }


    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        if (txtContrasena.Text != string.Empty && txtContrasena.Text.Length > 5)
        {
            if (Session["CaptchaImageText"].ToString() == txtCaptchaCode.Text)
            {
                Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                UsuarioWeb newuser = new UsuarioWeb(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()], 
                    ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                    ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()], txtCedula.Text.Trim().ToUpper(), objCrypto.CifrarCadena(txtContrasena.Text.Trim()));
                if (newuser.Register(txtNombres.Text.Trim().ToUpper(), txtApellidos.Text.Trim().ToUpper(), txtEmail.Text.Trim(), Request.UserHostAddress, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")))
                {
                    ShowSuccessMessage(txtEmail.Text);
                    divForm.Visible = false;
                }
                else
                {
                    ShowFailureMessage(newuser.Error);
                }
            }
            else
                ShowFailureMessage("El código de verificación ingresado no coincide con el mostrado en la imagen");
        }
        else
            ShowFailureMessage("Debe ingresar la contraseña de acceso deseada y debe ser de al menos 6 caracteres de longitud.");
    }


    protected void btnLnkCountinuar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void ShowFailureMessage(string message)
    {
        HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), "<b>Error al registrar cuenta</b><br /><br />" + message);
    }

    protected void ShowSuccessMessage(string email)
    {
        divError.Visible = false;
        divConfirmation.Visible = true;
        lblEmailSent.Text = email;
        //System.Web.UI.ScriptManager.RegisterStartupScript(UpdatePanel1, this.GetType(), "click", "csscody.info('<b>Registro de cuenta</b><br /><br />Su cuenta fue creada exitósamente, " + 
        //    "se le envió un correo electrónico a " + email + ". <br /><br />Por favor lea este correo, en el se especifican los pasos a seguir para finalizar el proceso de registro.')", true);
    }

    
}
