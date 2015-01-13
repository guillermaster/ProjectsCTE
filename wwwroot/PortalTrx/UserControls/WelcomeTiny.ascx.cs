using System;
using System.Web.Security;

public partial class UserControls_WelcomeTiny : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            lblName.Text = "Bienvenido " + Session[Constantes.UsuarioWeb.SessionUserNames].ToString();
        }
        catch
        {
            //LogOut();
            wlcMainPanel.Visible = false;
        }
    }
    protected void lnkDataCTG_Click(object sender, EventArgs e)
    {

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
}