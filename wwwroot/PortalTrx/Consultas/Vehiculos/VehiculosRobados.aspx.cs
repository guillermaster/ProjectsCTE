using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

public partial class Consultas_Vehiculos_VehiculosRobados : System.Web.UI.Page
{
    private static DataTable _dtVehRobados;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadVehiculosRecientementeRobados();
        }
    }


    protected void LoadVehiculosRecientementeRobados()
    {
        Matriculacion.VehiculosRobados oVehRobados = new Matriculacion.VehiculosRobados(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        if (oVehRobados.LoadVehiculosRobadosRecientes(DateTime.Today.AddDays(-30).ToString("dd/MM/yyyy"), DateTime.Today.ToString("dd/MM/yyyy")))
        {
            _dtVehRobados = oVehRobados.VehiculosRobadosRecientemente;
            gvRecientes.DataSource = _dtVehRobados;
            gvRecientes.DataBind();
            gvRecientes.Visible = true;
            if (gvRecientes.Rows.Count == 0)
                divContent.Controls.Add(HtmlWriter.Messages.PrintInfoMessage("No existe ningún vehículo reportado como robado en los últimos 30 días."));
        }
        else
        {
            gvRecientes.Visible = false;
            divContent.Controls.Add(HtmlWriter.Messages.PrintErrorMessage(oVehRobados.Error));
        }
    }


    protected void btnImgPhoto_Click(object sender, EventArgs e)
    {
        ImageButton imgBtn = (ImageButton)sender;

        GridViewRow row = (GridViewRow)imgBtn.Parent.Parent;
        if (row == null) return;

        string placa = row.Cells[0].Text;
        imgFotoVehiculo.ImageUrl = "FotoVehiculo.aspx?p=" + placa;
        MPE.Show();
    }

    protected void btnImgPhoto2_Click(object sender, EventArgs e)
    {
        ImageButton imgBtn = (ImageButton)sender;
        DetailsView dv = (DetailsView)imgBtn.Parent.Parent.Parent.Parent;

        if (dv == null) return;

        string placa = dv.Rows[0].Cells[1].Text;
        imgFotoVehiculo.ImageUrl = "FotoVehiculo.aspx?p=" + placa;
        MPE.Show();
    }


    protected void VehRobGridView_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        gvRecientes.PageIndex = e.NewPageIndex;
        gvRecientes.DataSource = _dtVehRobados;
        gvRecientes.DataBind();
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        if (rbtnPlaca.Checked)
            BuscarVehiculoRobadoPorPlaca(txtPlaca.Text.Trim().ToUpper());
        else //búsqueda por chasis
            BuscarVehiculoRobadoPorChasis(txtChasis.Text.Trim().ToUpper());
    }



    protected void BuscarVehiculoRobadoPorPlaca(string placa)
    {
        string fechaDenuncia;
        Matriculacion.VehiculosRobados oVehRob = new Matriculacion.VehiculosRobados(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
           ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
           ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        if (oVehRob.VehiculoEstaRobado(placa, out fechaDenuncia))
        {
            DataTable dtRobado = new DataTable("VehiculoRobado");
            dtRobado.Columns.Add("Placa");
            dtRobado.Columns.Add("Fecha de denuncia");
            DataRow dr = dtRobado.NewRow();
            dr[0] = placa;
            dr[1] = fechaDenuncia;
            dtRobado.Rows.Add(dr);
            detViewVehRobado.DataSource = dtRobado;
            detViewVehRobado.DataBind();
            detViewVehRobado.Visible = true;
        }
        else
        {
            detViewVehRobado.Visible = false;
            if (string.IsNullOrWhiteSpace(oVehRob.Error))
                HtmlWriter.Messages.ShowModalAlertMessage(Master.MasterUpdatePanel, Master.GetType(), "El vehículo con placa " + placa + " no se encuentra reportado como robado.");
            else
                HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), oVehRob.Error);
        }
    }


    protected void BuscarVehiculoRobadoPorChasis(string chasis)
    {
        string fechaDenuncia;
        Matriculacion.VehiculosRobados oVehRob = new Matriculacion.VehiculosRobados(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
           ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
           ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        if (oVehRob.VehiculoEstaRobadoChasis(chasis, out fechaDenuncia))
        {
            DataTable dtRobado = new DataTable("VehiculoRobado");
            dtRobado.Columns.Add("Chasis");
            dtRobado.Columns.Add("Fecha de denuncia");
            DataRow dr = dtRobado.NewRow();
            dr[0] = chasis;
            dr[1] = fechaDenuncia;
            dtRobado.Rows.Add(dr);
            detViewVehRobado.DataSource = dtRobado;
            detViewVehRobado.DataBind();
            detViewVehRobado.Visible = true;
        }
        else
        {
            detViewVehRobado.Visible = false;
            if (string.IsNullOrWhiteSpace(oVehRob.Error))
                HtmlWriter.Messages.ShowModalAlertMessage(Master.MasterUpdatePanel, Master.GetType(), "El vehículo con chasis " + chasis + " no se encuentra reportado como robado.");
            else
                HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), oVehRob.Error);
        }
    }
}