using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserControls_ShortcutMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            imgMenuCons.Attributes["onmouseout"] = string.Format("this.src='{0}' ;", ResolveUrl("~/images/icoMenuConsultas.png"));
            imgMenuCons.Attributes["onmouseover"] = string.Format("this.src='{0}' ;", ResolveUrl("~/images/icoMenuConsultasOn.png"));
            imgMenuTram.Attributes["onmouseout"] = string.Format("this.src='{0}' ;", ResolveUrl("~/images/icoMenuTramites.png"));
            imgMenuTram.Attributes["onmouseover"] = string.Format("this.src='{0}' ;", ResolveUrl("~/images/icoMenuTramitesOn.png"));
        }

        pnlMenu.Visible = !string.IsNullOrWhiteSpace(HttpContext.Current.User.Identity.Name);
    }

    public void Hide()
    {
        HyperLink1.Visible = false;
        HyperLink2.Visible = false;
    }

    public void Show()
    {
        HyperLink1.Visible = true;
        HyperLink2.Visible = true;
    }
}