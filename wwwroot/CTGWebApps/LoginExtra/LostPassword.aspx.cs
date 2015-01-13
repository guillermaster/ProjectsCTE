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

public partial class LoginExtra_LostPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        UsuarioWeb newuser = new UsuarioWeb(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], this.txtCedula.Text);
        if (newuser.SendLostPassword())
        {
            this.lblMensaje.Text = "<br />Su contraseña ha sido enviada a <b>" + newuser.UserEmail + "</b>";
            this.divForm.Visible = false;
            this.LinkButton1.Visible = true;
        }
        else
        {
            this.lblMensaje.Text = "ERROR: " + newuser.Error;
        }
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
