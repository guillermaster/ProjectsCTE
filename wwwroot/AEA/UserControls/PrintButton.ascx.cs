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

public partial class UserControls_PrintButton : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetupPrintingElement(string id, int width, int height)
    {
        this.btnPrint.OnClientClick = "javascript: CallPrint('" + id + "', "+width.ToString()+","+height.ToString()+");";
    }

    //public string PrintingElementId
    //{
    //    set
    //    {
    //        this.btnPrint.OnClientClick = "javascript: CallPrint('" + value + "');";
    //    }
    //}
        
}
