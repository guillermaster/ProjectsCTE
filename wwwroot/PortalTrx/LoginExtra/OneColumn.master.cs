using System;
using System.Web.UI;

public partial class _OneColumn : MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public UpdatePanel MasterUpdatePanel
    {
        get { return UpdatePanel1; }
    }
}
