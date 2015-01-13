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

public partial class MasterPage : System.Web.UI.MasterPage
{
    private static ArrayList rolesUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.lblUsuario.Text = HttpContext.Current.User.Identity.Name.ToString();
            SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.lblUsuario.Text, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            oUsuario.LoadRolesByUser();
            rolesUsuario = oUsuario.Roles;
            try
            {
                this.lblEmpresa.Text = Session[AEA.Parametros.SessionVarNombreEmpresa].ToString();
            }
            catch
            {
                LogOut();
            }
        }
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        LogOut();
    }

    public void LogOut()
    {
        Session.RemoveAll();
        Session.Abandon();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }

}
