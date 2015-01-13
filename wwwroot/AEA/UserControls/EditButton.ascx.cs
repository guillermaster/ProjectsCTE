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

public partial class UserControls_EditButton : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect(TargetURL);
    }

    public string TargetURL
    {
        get
        {
            return this.hdnTargetURL.Value;
        }
        set
        {
            this.hdnTargetURL.Value = value;
        }
    }
}