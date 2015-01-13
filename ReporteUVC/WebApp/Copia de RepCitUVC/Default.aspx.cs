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
    private static string[] colors = { "0DA068", "194E9C", "ED9C13", "ED5713", "057249", "5F91DC", "F88E5D", "000E5D", "F88E00", "F00E50", "088E5D", "0054ff",
                                       "eaff00", "0cff00", "9000ff", "e3c5fa", "7acbc8", "6c1c1c", "7d6d12", "d4f5ff", "60b2ff", "c0e087", "fca98c", "c426b7", "807372", "7b7b7b"};

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetDefaultDates();
            LoadGridViewData();
        }
    }

    protected void SetDefaultDates()
    {
        txtDesde.Text = DateTime.Today.AddDays(-7).ToString("dd/MM/yyyy");
        txtHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");
    }

    protected void LoadGridViewData()
    {
        //DataTable dt = new DataTable();
        //dt.Columns.Add("Delegación");
        //dt.Columns.Add("Citaciones");
        //DataRow dr1 = dt.NewRow();
        //dr1[0] = "Delegación 1";
        //dr1[1] = "50";
        //dt.Rows.Add(dr1);
        //DataRow dr2 = dt.NewRow();
        //dr2[0] = "Delegación 2";
        //dr2[1] = "25";
        //dt.Rows.Add(dr2);
        //DataRow dr3 = dt.NewRow();
        //dr3[0] = "Delegación 3";
        //dr3[1] = "15";
        //dt.Rows.Add(dr3);
        //DataRow dr4 = dt.NewRow();
        //dr4[0] = "Delegación 4";
        //dr4[1] = "35";
        //dt.Rows.Add(dr4);
        //gvChartData.DataSource = dt;
        //gvChartData.DataBind();
        DatosUVC.DatosRepUVC oRepUVC = new DatosUVC.DatosRepUVC(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        gvChartData.DataSource = oRepUVC.DatosReporteCitacionesUVC(txtDesde.Text, txtHasta.Text);
        gvChartData.DataBind();
    }

    protected void GridView_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        string hexColour = GetNewColor(e.Row.RowIndex);
        e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml(hexColour);
    }

    public string GetNewColor(int idx)
    {
        string color;
        if (idx >= 0)
        {
            if (idx > colors.Length - 1)
                color = colors[0];
            else
                color = colors[idx];
        }
        else
            color = "000000";
        return "#" + color;
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        LoadGridViewData();
    }
}
