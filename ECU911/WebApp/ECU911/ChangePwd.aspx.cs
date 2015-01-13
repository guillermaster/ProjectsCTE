using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangePwd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ModificaPassword()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuarioWeb = new SeguridadWebAppInt.UsuarioWebAppInt(HttpContext.Current.User.Identity.Name, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oUsuarioWeb.ChangePassword(this.txtCurrentPwd.Text, this.txtNewPwd.Text))
        {
            divForm.Visible = false;
            btnChangePwd.Visible = false;
            ShowSuccessMessage(oUsuarioWeb.Mensaje);
        }
        else
        {
            ShowFailureMessage(oUsuarioWeb.Error);
        }
    }

    protected void ShowSuccessMessage(string mensaje)
    {
        lblMsg.Text = mensaje;
        pnlMsg.Visible = true;
        pnlWarning.Visible = false;
    }

    protected void ShowFailureMessage(string mensaje)
    {
        lblWarning.Text = mensaje;
        pnlMsg.Visible = false;
        pnlWarning.Visible = true;
    }
    protected void btnChangePwd_Click1(object sender, EventArgs e)
    {
        ModificaPassword();
    }
}