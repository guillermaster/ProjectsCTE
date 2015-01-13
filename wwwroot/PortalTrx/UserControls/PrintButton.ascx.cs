using System;
using System.Web.UI;

public partial class UserControls_PrintButton : System.Web.UI.UserControl
{
    public UpdatePanel Panel;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ImageButton1.Attributes.Add("onmouseover", "this.src='../images/buttonDownPrintPreview.gif'");
            ImageButton1.Attributes.Add("onmouseout", "this.src='../images/buttonUpPrintPreview.gif'");
        }
    }

    public void SetPopUp(int width, int height, string innerHtml)
    {
        ImageButton1.OnClientClick = "popupAndWriteHtml('" + width.ToString() + "', '" + height.ToString() + "', '1', 'popupCTG', '" + innerHtml + "')";
    }
    
}