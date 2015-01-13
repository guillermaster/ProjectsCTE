using System;
using System.Web;
using System.Web.UI;

public partial class _OneColumn : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public UpdatePanel MasterUpdatePanel
    {
        get { return UpdatePanel1; }
    }
}
