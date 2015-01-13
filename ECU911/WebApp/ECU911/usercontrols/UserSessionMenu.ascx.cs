using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


public partial class usercontrols_UserSessionMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblName.Text = "Bienvenido " + HttpContext.Current.User.Identity.Name;
        }
        catch
        {
            LogOut();
        }
    }

    protected void lnkCloseSession_Click(object sender, EventArgs e)
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

    public bool visible
    {
        get
        {
            return lblName.Visible;
        }
        set
        {
            lblName.Visible = value;
        }
    }
}