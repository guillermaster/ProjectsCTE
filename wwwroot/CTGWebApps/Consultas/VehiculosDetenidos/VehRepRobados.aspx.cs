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
using AjaxControlToolkit;

public partial class Consultas_VehiculosDetenidos_VehRepRobados : System.Web.UI.Page
{
    private static DataTable dtVehRobados;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadGridViewData();
        }
    }
    protected void btnBuscarPorPlaca_Click(object sender, EventArgs e)
    {
        string fechaDenuncia;
        Matriculacion.VehiculosRobados oVehRob = new Matriculacion.VehiculosRobados(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        this.divError2.Visible = false;
        this.divWarning.Visible = false;

        if (oVehRob.VehiculoEstaRobado(this.txtPlaca.Text.ToUpper(), out fechaDenuncia))
        {
            DataTable dtRobado = new DataTable("VehiculoRobado");
            dtRobado.Columns.Add("placa");
            dtRobado.Columns.Add("fecha");
            DataRow dr = dtRobado.NewRow();
            dr[0] = this.txtPlaca.Text;
            dr[1] = fechaDenuncia;
            dtRobado.Rows.Add(dr);
            this.gvPlacaVehRob.DataSource = dtRobado;
            this.gvPlacaVehRob.DataBind();
            this.gvPlacaVehRob.Visible = true;
        }
        else
        {
            this.gvPlacaVehRob.Visible = false;
            if (oVehRob.Error == string.Empty)
            {
                this.lblMsgWarning.Text = "El vehículo con placa " + this.txtPlaca.Text.ToUpper() + " no está reportado como robado.";
                this.divWarning.Visible = true;
            }
            else
            {                
                this.lblMsgError2.Text = oVehRob.Error;
                this.divError2.Visible = true;
            }
        }
    }

    protected void LoadGridViewData()
    {
        Matriculacion.VehiculosRobados oVehRobados = new Matriculacion.VehiculosRobados(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        if (oVehRobados.LoadVehiculosRobadosRecientes(DateTime.Today.AddDays(-30).ToString("dd/MM/yyyy"), DateTime.Today.ToString("dd/MM/yyyy")))
        {
            dtVehRobados = oVehRobados.VehiculosRobadosRecientemente;
            this.gvRecientes.DataSource = dtVehRobados;
            this.gvRecientes.DataBind();
            this.gvRecientes.Visible = true;
        }
        else
        {
            this.gvRecientes.Visible = false;
            this.lblMsgError1.Text = oVehRobados.Error;
            this.divError1.Visible = true;
        }
    }


    protected void gvRepRobadosRecientes_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.MPE.X = 10;
        //this.MPE.Y = 80;
        //this.MPE.Show();
        GridViewRow row = ((GridView)sender).SelectedRow;

        if (row == null) return;

        ModalPopupExtender extender = row.FindControl("extProject") as ModalPopupExtender;

        if (extender != null) extender.Show(); 
    }


    protected void btnImgPhoto_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton imgBtn = (ImageButton)sender;
            
            GridViewRow row = (GridViewRow)imgBtn.Parent.Parent;
            if(row == null) return;

            string placa = row.Cells[0].Text;
            this.imgFotoVehiculo.ImageUrl = "fotoVehiculo.aspx?p=" + placa;
            this.MPE.X = 10;
            this.MPE.Y = 60;
            this.MPE.Show();
        }
        catch (Exception ex)
        {
        }
    }


    protected void imgFotoVehiculo_Click(object sender, ImageClickEventArgs e)
    {
        this.MPE.Hide();
    }

    protected void VehRobGridView_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        this.gvRecientes.PageIndex = e.NewPageIndex;
        this.gvRecientes.DataSource = dtVehRobados;
        this.gvRecientes.DataBind();
    }
}
