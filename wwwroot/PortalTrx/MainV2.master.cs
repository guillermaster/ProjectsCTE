using System;
using System.Web;
using System.Web.UI;

public partial class _Main : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ucWelcomeTiny1.Visible = HttpContext.Current.User.Identity.IsAuthenticated;
    }

    public UpdatePanel MasterUpdatePanel
    {
        get { return UpdatePanel1; }
    }

    public UpdatePanel RightColumnUpdatePanel
    {
        get { return UpdatePanel2; }
    }

    public void HideShortcutMenu()
    {
        ShortcutMenu1.Hide();
    }
}
