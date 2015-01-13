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

public partial class Seguridad_ChangePassword : System.Web.UI.Page
{
    private string currentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.currentUser = User.Identity.Name.ToString();
    }

    protected void ModificaPassword(object sender, EventArgs e)
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuarioWeb = new SeguridadWebAppInt.UsuarioWebAppInt(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oUsuarioWeb.ChangePassword(this.txtCurrentPwd.Text, this.txtNewPwd.Text))
        {
            this.divForm.Visible = false;
            this.btnGuardar.Visible = false;
            ShowSuccessMessage(oUsuarioWeb.Mensaje);
        }
        else
        {
            ShowFailureMessage(oUsuarioWeb.Error);
        }
    }

    protected void ShowSuccessMessage(string message)
    {
        this.divError.Visible = false;
        this.divWarning.Visible = true;
        this.lblMsgWarning.Text = message;
    }
    
    protected void ShowFailureMessage(string message)
    {
        this.divWarning.Visible = false;
        this.divError.Visible = true;
        this.lblMsgError.Text = message;
    }

    protected void btnGuardar_Load(object sender, EventArgs e)
    {
        this.btnGuardar.ButtonClickDemo += new EventHandler(ModificaPassword);
    }
}
