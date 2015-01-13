using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

public partial class ConsultaPagosEncolados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString.Count > 0)
        {
            CargarPagosEncolados(Request.QueryString[0]);
        }
    }


    protected void CargarPagosEncolados(string tipo)
    {
        ComprobanteElectronicoPago.RegularizacionCEP oRegCEP = new ComprobanteElectronicoPago.RegularizacionCEP(
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        DataTable dtPagosEncolados = oRegCEP.PagosEncolados(tipo);
        gvPagosEncolados.DataSource = dtPagosEncolados;
        gvPagosEncolados.DataBind();

        if (tipo == ComprobanteElectronicoPago.RegularizacionCEP.TipoCitacion)
            gvPagosEncolados.Columns[0].Visible = true;
        else
            gvPagosEncolados.Columns[0].Visible = false;

        if (gvPagosEncolados.Rows.Count == 0)
        {
            if (string.IsNullOrWhiteSpace(oRegCEP.Error))
                pnlContent.Controls.Add(HtmlWriter.Messages.PrintInfoMessage(oRegCEP.Error));
            else
                HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), oRegCEP.Error);
        }
    }


    protected void CargarCitacionesPagadasEnCEP(string CEP)
    {
        ComprobanteElectronicoPago.RegularizacionCEP oRegCEP = new ComprobanteElectronicoPago.RegularizacionCEP(
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        gvCitacPagadas.DataSource = oRegCEP.ConsultaCitacionesEnCEP(CEP);
        gvCitacPagadas.DataBind();

    }


    protected void gvPagosEncolados_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCitacionesPagadasEnCEP(gvPagosEncolados.SelectedRow.Cells[1].Text);
        MPE.Show();
    }


    decimal _priceTotal;
    protected void gvCitacPagadas_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            _priceTotal += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            //e.Row.Cells[0].Text = "Totals:";
            // for the Footer, display the running totals
            //_priceTotal = 0;
            e.Row.Cells[5].Text = _priceTotal.ToString("c");
            e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Font.Bold = true;
        }
    }
}