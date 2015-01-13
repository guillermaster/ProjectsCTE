using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Citaciones;

public partial class ConsCitaciones : System.Web.UI.Page
{
    decimal _priceTotal;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.DefaultFocus = txtNumLicencia.ClientID;
            Page.Form.DefaultButton = btnConsultar.UniqueID;
        }
    }


    protected void ConsultarInfracciones(string id, string tipoId)
    {
        Infracciones oCitac = new Infracciones(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        pnlWarning.Visible = false;
        gvCitacPend.Visible = false;
        gvCitacPag.Visible = false;
        gvCitacPend.DataSource = oCitac.InfraccionesPendientes(id, tipoId);
        gvCitacPend.DataBind();

        if (gvCitacPend.Rows.Count == 0)
        {
            if (string.IsNullOrWhiteSpace(oCitac.Error))
                SetError("El usuario con identificación " + id + " no registra ninguna citación pendiente de pago");
            else
                SetError(oCitac.Error);
        }
        else
        {
            gvCitacPend.Visible = true;
        }

    }


    protected void ConsultarInfraccionesPagadas(string id)
    {
        Infracciones oCitac = new Infracciones(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        pnlWarning.Visible = false;
        gvCitacPend.Visible = false;
        gvCitacPag.Visible = false;
        gvCitacPag.DataSource = oCitac.InfraccionesPagadas(id);
        gvCitacPag.DataBind();

        if (gvCitacPag.Rows.Count == 0)
        {
            if (string.IsNullOrWhiteSpace(oCitac.Error))
                SetError("El usuario con identificación " + id + " no registra ninguna citación pendiente de pago");
            else
                SetError(oCitac.Error);
        }
        else
        {
            gvCitacPag.Visible = true;
        }

    }


    private void SetError(string errorMsg)
    {
        lblWarning.Text = errorMsg;
        pnlWarning.Visible = true;
    }

    
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

    protected void gvCitacPag_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            _priceTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "val_contrav"));
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            //e.Row.Cells[0].Text = "Totals:";
            // for the Footer, display the running totals
            //_priceTotal = 0;
            e.Row.Cells[7].Text = _priceTotal.ToString("c");
            e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
        }
    }

    protected void gvCitacPend_SelectedIndexChanged(object sender, EventArgs e)
    {

        CargarDatosAdicionalesCitacion(gvCitacPend.SelectedIndex);
        dvCitacPend.Visible = true;
        imgCitacion.Visible = false;
        btnVerCitacion.Visible = true;
        btnVolverDetCitac.Visible = false;
        MPE.Show();
    }

    protected void gvCitacPag_SelectedIndexChanged(object sender, EventArgs e)
    {

        CargarDatosAdicionalesCitacion(gvCitacPag.SelectedIndex);

        dvCitacPend.Visible = true;
        imgCitacion.Visible = false;
        btnVerCitacion.Visible = true;
        btnVolverDetCitac.Visible = false;
        MPE.Show();
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
                if (gvCitacPend.Visible)
                {
                    row[0] = ((HiddenField)gvCitacPend.SelectedRow.FindControl("hdnNumCitacion")).Value;
                    row[1] = ((HiddenField)gvCitacPend.SelectedRow.FindControl("hdnFechaCitacion")).Value;
                    row[2] = ((HiddenField)gvCitacPend.SelectedRow.FindControl("hdnArticulo")).Value;
                    row[3] = ((HiddenField)gvCitacPend.SelectedRow.FindControl("hdnContravencion")).Value;
                    row[4] = ((HiddenField)gvCitacPend.SelectedRow.FindControl("hdnPuntos")).Value;
                }
                else if (gvCitacPag.Visible)
                {
                    row[0] = ((HiddenField)gvCitacPag.SelectedRow.FindControl("hdnNumCitacion")).Value;
                    row[1] = ((HiddenField)gvCitacPag.SelectedRow.FindControl("hdnFechaCitacion")).Value;
                    row[2] = ((HiddenField)gvCitacPag.SelectedRow.FindControl("hdnArticulo")).Value;
                    row[3] = ((HiddenField)gvCitacPag.SelectedRow.FindControl("hdnContravencion")).Value;
                    row[4] = ((HiddenField)gvCitacPag.SelectedRow.FindControl("hdnPuntos")).Value;
                }
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
        //imgCitacion.ImageUrl = "imgCitaciones.aspx?id=" + ((HiddenField)gvCitacPend.SelectedRow.FindControl("hdnNumCitacion")).Value;
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
        _priceTotal = 0;
        if (rblEstado.SelectedValue == "N")
            ConsultarInfracciones(txtNumLicencia.Text, "I");
        else
            ConsultarInfraccionesPagadas(txtNumLicencia.Text);
    }
}