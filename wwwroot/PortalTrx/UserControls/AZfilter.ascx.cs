using System;
using System.Web.UI.WebControls;

public partial class UserControls_AZfilter : System.Web.UI.UserControl
{
    private string _filterChar;
    public event EventHandler FilterClicked;

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lnk_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)sender;
        if (btn != null)
            _filterChar = btn.Text;
        else
            _filterChar = string.Empty;
        if (FilterClicked != null)
            FilterClicked(this, EventArgs.Empty);
    }
    public string CaracterFiltro
    {
        get
        {
            return _filterChar;
        }
    }
}