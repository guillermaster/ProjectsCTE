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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtFechaDesde.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
            txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtConsFechaDesde.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
            txtConsFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }
        Master.HideMenu();
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Response.Redirect("RepCitacUVC.aspx?desde=" + txtFechaDesde.Text + "&" + "hasta=" + txtFechaHasta.Text);
    }

    protected void btnConsultarCons_Click(object sender, EventArgs e)
    {
        Response.Redirect("RepConsUVC.aspx?desde=" + txtConsFechaDesde.Text + "&" + "hasta=" + txtConsFechaHasta.Text);
    }
}
