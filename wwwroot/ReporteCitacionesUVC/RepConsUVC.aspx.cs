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
        //DataSet ds = BuildTables("01/06/2011", "01/07/2011");
        int i = 0;
        
        foreach (DataTable dt in ds.Tables)
        {
            Panel pnl = new Panel();
            pnl.CssClass = "twoColumnsBlock";

            GridView gv = new GridView();
            gv.ID = "gvCons" + i.ToString();
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

            i++;
        }
    }

    private DataSet BuildTables(string fechaInicio, string fechaFin)
    {
        DatosUVC.DatosRepUVC oRepUVC = new DatosUVC.DatosRepUVC(ConfigurationManager.AppSettings["usuario2"], ConfigurationManager.AppSettings["clave2"], ConfigurationManager.AppSettings["base2"]);
        return oRepUVC.DatosReporteConsultas(fechaInicio, fechaFin);
    }


    protected void LoadGridViewData()
    {
        DatosUVC.DatosRepUVC oRepUVC = new DatosUVC.DatosRepUVC(ConfigurationManager.AppSettings["usuario2"], ConfigurationManager.AppSettings["clave2"], ConfigurationManager.AppSettings["base2"]);
        DataSet dsCharts = oRepUVC.DatosTotalesReporteConsultasUVC(txtFechaDesde.Text, txtFechaHasta.Text);
        //DataSet dsCharts = oRepUVC.DatosTotalesReporteConsultasUVC("01/06/2011", "01/07/2011");
        Session["dsCharts"] = dsCharts;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        LoadGridViewData();
        PrintReportData();
    }
}
