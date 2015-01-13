using System;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using ComprobanteElectronicoPago;


public partial class Consultas_Tramites_Finalizados : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ConsultarCEPsTramites(HttpContext.Current.User.Identity.Name);
        }
    }

    protected void ConsultarCEPsTramites(string identificacion)
    {
        CEP oCEP = new CEP(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        try
        {
            DataTable dtCEPs = oCEP.ConsultaCEPTramitexUsuario(identificacion, true);

            dtCEPs.DefaultView.Sort = Constantes.Tramites.ColNameForFechaIngresoCEP + " DESC";
            gvTrackTramites.DataSource = dtCEPs.DefaultView;
            gvTrackTramites.DataBind();

            if(gvTrackTramites.Rows.Count==0)
            {
                if(string.IsNullOrWhiteSpace(oCEP.Error))
                    pnlContent.Controls.Add(HtmlWriter.Messages.PrintInfoMessage("Usted no tiene ningún trámite finalizado."));
                else
                    HtmlWriter.Messages.ShowMainContentError(Master, divMain, oCEP.Error);
            }
        }
        catch
        {
            pnlContent.Controls.Add(HtmlWriter.Messages.PrintErrorMessage("Error al consultar Comprobantes Electrónicos de Pago por usuario."));
        }

    }



    protected void gvTrackTramites_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDatosAdicionalesTramite();
        MPE.Show();
    }


    protected void GridViewPageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        gvTrackTramites.PageIndex = e.NewPageIndex;
        ConsultarCEPsTramites(HttpContext.Current.User.Identity.Name);
    }


    protected void CargarDatosAdicionalesTramite()
    {
        DataTable dtDetallesCitacion = new DataTable();
        #region add columns to datatable
        dtDetallesCitacion.Columns.Add("Código de Pago (CEP)");//0
        dtDetallesCitacion.Columns.Add("Valor CEP ($)");//1
        dtDetallesCitacion.Columns.Add("Estado CEP");//2
        dtDetallesCitacion.Columns.Add("Fecha de solicitud");//3
        dtDetallesCitacion.Columns.Add("Fecha de pago/reverso");//4
        dtDetallesCitacion.Columns.Add("Canal de Pago");//5
        dtDetallesCitacion.Columns.Add("Trámite");//6
        dtDetallesCitacion.Columns.Add("Código de trámite");//7        
        dtDetallesCitacion.Columns.Add("Fecha de ejecución");//8
        dtDetallesCitacion.Columns.Add("Estado de trámite");//9
        dtDetallesCitacion.Columns.Add("Entrega");//10
        #endregion
        DataRow row = dtDetallesCitacion.NewRow();

        #region copy some gridview data
        row[0] = gvTrackTramites.SelectedRow.Cells[2].Text;
        row[1] = ((HiddenField)gvTrackTramites.SelectedRow.FindControl("hdnValor")).Value;
        row[2] = ((HiddenField)gvTrackTramites.SelectedRow.FindControl("hdnEstadoCEP")).Value;
        row[3] = ((HiddenField)gvTrackTramites.SelectedRow.FindControl("hdnFechaSolicitud")).Value;
        row[4] = ((HiddenField)gvTrackTramites.SelectedRow.FindControl("hdnFechaPagoReverso")).Value;
        row[5] = ((HiddenField)gvTrackTramites.SelectedRow.FindControl("hdnCanalPago")).Value;
        row[6] = gvTrackTramites.SelectedRow.Cells[3].Text;
        row[7] = ((HiddenField)gvTrackTramites.SelectedRow.FindControl("hdnIdTramite")).Value;
        row[8] = ((HiddenField)gvTrackTramites.SelectedRow.FindControl("hdnFechaEjecTramite")).Value;
        row[9] = ((HiddenField)gvTrackTramites.SelectedRow.FindControl("hdnEstadoTramite")).Value;
        row[10] = ((HiddenField)gvTrackTramites.SelectedRow.FindControl("hdnEntregaTramite")).Value;
        #endregion

        dtDetallesCitacion.Rows.Add(row);
        dvDetTramite.DataSource = dtDetallesCitacion;
        dvDetTramite.DataBind();
    }
}