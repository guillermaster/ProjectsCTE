using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string Title
    {
        set
        {
            lblTitulo.Text = value;
        }
    }

    public string Error
    {
        set
        {
            lblPnlError.Text = value;
            pnlError.Visible = true;
            pnlInfo.Visible = false;
        }
    }

    public string Info
    {
        set
        {
            lblPnlInfo.Text = value;
            pnlError.Visible = false;
            pnlInfo.Visible = true;
        }
    }
}
