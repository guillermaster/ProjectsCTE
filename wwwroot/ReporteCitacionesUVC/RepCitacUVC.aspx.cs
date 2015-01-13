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

public partial class RepCitacUVC : System.Web.UI.Page
{
    private static string[] colors = { "0DA068", "194E9C", "ED9C13", "ED5713", "057249", "5F91DC", "F88E5D", "000E5D", "F88E00", "F00E50", "088E5D", "0054ff",
                                       "7d8152", "1b9015", "9000ff", "450477", "2a8c9b", "6c1c1c", "7d6d12", "d4f5ff", "60b2ff", "c0e087", "fca98c", "c426b7", "807372", "7b7b7b"};
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString.Count >= 2)
            {
                txtFechaDesde.Text = Request.QueryString[0];
                txtFechaHasta.Text = Request.QueryString[1];
            }
            else
            {
                txtFechaDesde.Text = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
                txtFechaHasta.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
            LoadGridViewData();
            PrintReportData();
        }
    }

    protected void PrintReportData()
    {
        DataSet ds = BuildTables(txtFechaDesde.Text, txtFechaHasta.Text);
        int i = 0;

        HtmlGenericControl list = new HtmlGenericControl("ul");
        ArrayList divs = new ArrayList();
        
        foreach (DataTable dt in ds.Tables)
        {
            Panel pnl = new Panel();
            pnl.CssClass = "twoColumnsBlock";

            GridView gv = new GridView();
            gv.ID = "gvCitac" + i.ToString();
            gv.Attributes.Add("style", "display:none");
            gv.SkinID = "styledGv";
            gv.DataSource = dt;
            gv.DataBind();

            Label lblTitle = new Label();
            lblTitle.ID = "lblTitle" + i.ToString();
            lblTitle.Text = dt.TableName;
            lblTitle.Attributes.Add("onclick", "javascript:ToggleGrid('" + gv.ID + "', '" + lblTitle.ID + "')");
            lblTitle.CssClass = "gridTitle";
            lblTitle.Width = 199;
            lblTitle.Height = 23;
            
            /*pnl.Controls.Add(lblTitle);
            pnl.Controls.Add(gv);
            pnlContent.Controls.Add(pnl);*/
            pnlTabs.Controls.Add(lblTitle);
            pnlTabsContent.Controls.Add(gv);

            //////////

            

            i++;

            /*HtmlGenericControl listItem = new HtmlGenericControl("li");
            HtmlAnchor ahref = new HtmlAnchor();
            ahref.HRef = "#tabs-" + i.ToString();
            ahref.InnerText = dt.TableName;
            listItem.Controls.Add(ahref);
            list.Controls.Add(listItem);

            HtmlGenericControl div = new HtmlGenericControl("div");
            div.ID = "tabs-" + i.ToString();
            div.Controls.Add(gv);
            divs.Add(div);*/
        }

        /*tabs.Controls.Add(list);
        for (int j = 0; j < divs.Count; j++)
        {
            HtmlGenericControl div = divs[j] as HtmlGenericControl;
            tabs.Controls.Add(div);
        }*/
    }

    private DataSet BuildTables(string fechaInicio, string fechaFin)
    {
        DatosUVC.DatosRepUVC oRepUVC = new DatosUVC.DatosRepUVC(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        return oRepUVC.DatosReporteCitaciones(fechaInicio, fechaFin);
    }


    protected void LoadGridViewData()
    {
        DatosUVC.DatosRepUVC oRepUVC = new DatosUVC.DatosRepUVC(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        
        DataTable datosTotales = oRepUVC.DatosTotalesReporteCitacionesUVC(txtFechaDesde.Text, txtFechaHasta.Text);
        hdnTotRegistros.Text = datosTotales.Rows.Count.ToString();
        gvChartData.DataSource = datosTotales;
        gvChartData.DataBind();
        
        //Page.RegisterStartupScript("anykey", "<script> pieChart();</script>");
    }

    protected void GridView_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        string hexColour = GetNewColor(e.Row.RowIndex);
        e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml(hexColour);
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
        PrintReportData();
    }
}
