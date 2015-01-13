using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;

public partial class UserControls_UserLoggedInBasic : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnAccount.Attributes["onmouseout"] = string.Format("this.src='{0}' ;", ResolveUrl("~/images/btnAccount.png"));
            btnAccount.Attributes["onmouseover"] = string.Format("this.src='{0}' ;", ResolveUrl("~/images/btnAccountOn.png"));
            btnCtgData.Attributes["onmouseout"] = string.Format("this.src='{0}' ;", ResolveUrl("~/images/btnCtg.png"));
            btnCtgData.Attributes["onmouseover"] = string.Format("this.src='{0}' ;", ResolveUrl("~/images/btnCtgOn.png"));
            btnEndSession.Attributes["onmouseout"] = string.Format("this.src='{0}' ;", ResolveUrl("~/images/btnLogout.png"));
            btnEndSession.Attributes["onmouseover"] = string.Format("this.src='{0}' ;", ResolveUrl("~/images/btnLogoutOn.png"));
        }
        try
        {
            lblUserNombres.Text = Session[Constantes.UsuarioWeb.SessionUserNames].ToString();
            lblUserApellidos.Text = Session[Constantes.UsuarioWeb.SessionUserLastNames].ToString();
            lblUserEmail.Text = Session[Constantes.UsuarioWeb.SessionUserEmail].ToString();
            lblUserIdent.Text = HttpContext.Current.User.Identity.Name;
        }
        catch
        {
            //LogOut();
        }
    }

    protected void btnEndSession_Click(object sender, ImageClickEventArgs e)
    {
        LogOut();
    }

    private void LogOut()
    {
        Session.RemoveAll();
        Session.Abandon();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }
}
