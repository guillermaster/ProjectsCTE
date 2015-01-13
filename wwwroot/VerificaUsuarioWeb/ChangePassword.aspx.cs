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

public partial class LoginExtra_UserRegistartion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Seguridad.UsuarioWeb.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }

        if (!IsPostBack)
        {
            //ChangePassword();
            Page.Form.DefaultButton = this.btnChangePwd.ID;
            Page.Form.DefaultFocus = this.txtContrasena.ID;
        }        
    }

   
    protected void btnChangePwd_Click(object sender, EventArgs e)
    {
        ChangePassword();
    }

    protected void ChangePassword()
    {
        UsuarioVerificador oUsuario = new UsuarioVerificador(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], User.Identity.Name.ToString());

        if (oUsuario.ChangePassword(this.txtContrasena.Text))
        {
            this.lblMensaje.Text = "Se modificó la contraseña correctamente.";
            this.tableDiv.Visible = false;
            this.LinkButton1.Visible = true;
            Session["firstTime"] = "0";
        }
        else
        {
            this.lblMensaje.Text = oUsuario.Error;
        }
    }
}
