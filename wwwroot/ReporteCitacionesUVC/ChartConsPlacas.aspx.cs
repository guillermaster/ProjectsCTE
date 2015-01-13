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

public partial class charts_ChartConsPlacas : System.Web.UI.Page
{
    private static string[] colors = { "0DA068", "194E9C", "ED9C13", "ED5713", "057249", "5F91DC", "F88E5D", "000E5D", "F88E00", "F00E50", "088E5D", "0054ff",
                                       "7d8152", "1b9015", "9000ff", "450477", "2a8c9b", "6c1c1c", "7d6d12", "d4f5ff", "60b2ff", "c0e087", "fca98c", "c426b7", "807372", "7b7b7b"};

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGridViewData();
        }
    }

    protected void LoadGridViewData()
    {
        DataSet dsCharts = Session["dsCharts"] as DataSet;
        hdnTotRegistros.Text = dsCharts.Tables[0].Rows.Count.ToString();
        gvChartData.DataSource = dsCharts.Tables[0];
        gvChartData.DataBind();
    }

    public int TotalCitaciones
    {
        get
        {
            DataTable dtDatos = gvChartData.DataSource as DataTable;
            int totCitac = 0;
            foreach (DataRow dr in dtDatos.Rows)
            {
                totCitac += int.Parse(dr[1].ToString());
            }
            return totCitac;
        }
    }

    public int TotalUVC
    {
        get
        {
            DataTable dtDatos = gvChartData.DataSource as DataTable;
            int totUVC = 0;
            foreach (DataRow dr in dtDatos.Rows)
            {
                totUVC += int.Parse(dr[3].ToString());
            }
            return totUVC;
        }
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
}
