using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Citaciones;

public partial class Consultas_Citaciones_CitacionesPendientes : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (HttpContext.Current.User == null || !HttpContext.Current.User.Identity.IsAuthenticated)
            {
                SetPageForAnonymousUser();
            }
            else
            {
                ConsultarInfracciones(HttpContext.Current.User.Identity.Name, "I");
            }
        }
    }



    protected void ConsultarInfracciones(string id, string tipoId)
    {
        Infracciones oCitac = new Infracciones(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        gvCitacPend.DataSource = oCitac.InfraccionesPendientes(id, tipoId);
        gvCitacPend.DataBind();

        if (gvCitacPend.Rows.Count == 0)
        {
            if (string.IsNullOrWhiteSpace(oCitac.Error))
                pnlContent.Controls.Add(HtmlWriter.Messages.PrintInfoMessage("Usted no registra ninguna citación pendiente de pago"));
            else
                HtmlWriter.Messages.ShowMainContentError(Master, divMain, oCitac.Error);
        }

    }

    private void SetPageForAnonymousUser()
    {
        //gvCitacPend.Columns[0].Visible = false;
        pnlNumCedula.Visible = true;
    }

    decimal _priceTotal;
    protected void gvCitacPend_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            _priceTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "total_pagar"));
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            //e.Row.Cells[0].Text = "Totals:";
            // for the Footer, display the running totals
            //_priceTotal = 0;
            e.Row.Cells[9].Text = _priceTotal.ToString("c");
            e.Row.Cells[9].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
        }
    }

    protected void gvCitacPend_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (HttpContext.Current.User != null && HttpContext.Current.User.Identity.IsAuthenticated)
        {
            CargarDatosAdicionalesCitacion(gvCitacPend.SelectedIndex);
            dvCitacPend.Visible = true;
            imgCitacion.Visible = false;
            btnVerCitacion.Visible = true;
            btnVolverDetCitac.Visible = false;
            MPE.Show();
        }
        else
        {
            HtmlWriter.Messages.ShowModalAlertMessage(Master.MasterUpdatePanel, Master.GetType(), "Para ver los detalles completos, debe ingresar con su usuario y contraseña.");
        }
    }

    protected void CargarDatosAdicionalesCitacion(int selIndex)
    {
        DataTable dtDetallesCitacion = new DataTable();
        #region add columns to datatable
        dtDetallesCitacion.Columns.Add("Código de citación");
        dtDetallesCitacion.Columns.Add("Fecha de citación");
        dtDetallesCitacion.Columns.Add("Artículo");
        dtDetallesCitacion.Columns.Add("Contravención");
        dtDetallesCitacion.Columns.Add("Puntos a deducir");
        dtDetallesCitacion.Columns.Add("Datos del vehículo");
        dtDetallesCitacion.Columns.Add("Propietario del vehículo");
        dtDetallesCitacion.Columns.Add("Uniformado");
        #endregion
        DataRow row = dtDetallesCitacion.NewRow();

        #region copy some gridview data
        foreach (DataControlField column in gvCitacPend.Columns)
        {
            if (string.IsNullOrWhiteSpace(column.HeaderText))
            {
                row[0] = ((HiddenField)gvCitacPend.SelectedRow.FindControl("hdnNumCitacion")).Value;
                row[1] = ((HiddenField)gvCitacPend.SelectedRow.FindControl("hdnFechaCitacion")).Value;
                row[2] = ((HiddenField)gvCitacPend.SelectedRow.FindControl("hdnArticulo")).Value;
                row[3] = ((HiddenField)gvCitacPend.SelectedRow.FindControl("hdnContravencion")).Value;
                row[4] = ((HiddenField)gvCitacPend.SelectedRow.FindControl("hdnPuntos")).Value;
            }
        }
        #endregion

        Infracciones oCitac = new Infracciones(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        #region obtener datos de vigilante
        string[] datosUniformado = oCitac.ObtenerUniformadoSancionador(gvCitacPend.SelectedRow.Cells[2].Text.Trim());

        if (datosUniformado != null)
            row[7] = datosUniformado[0] + " - " + datosUniformado[1];
        else
            row[7] = oCitac.Error;
        #endregion
        #region obtener datos de vehículo y propietario
        if (!string.IsNullOrWhiteSpace(gvCitacPend.SelectedRow.Cells[5].Text))
        {
            string[] datosVehiculo = oCitac.ObtenerDatosBasicosVehiculo(gvCitacPend.SelectedRow.Cells[5].Text.Trim());
            if (datosVehiculo != null)
            {
                row[5] = datosVehiculo[1] + " " + datosVehiculo[2] + " " + datosVehiculo[3] + " " + datosVehiculo[4];
                row[6] = datosVehiculo[0];
            }
            else
            {
                row[5] = oCitac.Error;
                row[6] = oCitac.Error;
            }
        }
        #endregion

        dtDetallesCitacion.Rows.Add(row);
        dvCitacPend.DataSource = dtDetallesCitacion;
        dvCitacPend.DataBind();
    }
    protected void btnVerCitacion_Click(object sender, ImageClickEventArgs e)
    {
        imgCitacion.ImageUrl = "imgCitaciones.aspx?id=" + ((HiddenField)gvCitacPend.SelectedRow.FindControl("hdnNumCitacion")).Value;
        imgCitacion.DataBind();
        imgCitacion.Visible = true;
        //imgCitacion.Width = Unit.Parse(Session[Constantes.WebApp.DatosSesion.ImgCitacWidth.ToString()].ToString());
        //imgCitacion.Height = Unit.Parse(Session[Constantes.WebApp.DatosSesion.ImgCitacHeight.ToString()].ToString());
        dvCitacPend.Visible = false;
        btnVerCitacion.Visible = false;
        btnVolverDetCitac.Visible = true;
        MPE.Show();
    }

    protected void btnVolverDetCitacion_Click(object sender, ImageClickEventArgs e)
    {
        dvCitacPend.Visible = true;
        imgCitacion.Visible = false;
        btnVerCitacion.Visible = true;
        btnVolverDetCitac.Visible = false;
        MPE.Show();
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ConsultarInfracciones(txtNumLicencia.Text, "I");
    }
}