using System;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.UI;
using ComprobanteElectronicoPago;
using System.Web.UI.WebControls;

public partial class Consultas_Tramites_SolTramitesNoPagadas : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
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
            DataTable dtCEPs = oCEP.ConsultaCEPNoPagados(identificacion);
            dtCEPs.DefaultView.Sort = Constantes.Tramites.ColNameForFechaIngresoCEP + " DESC";
            gvCepNoPagados.DataSource = dtCEPs.DefaultView;
            gvCepNoPagados.DataBind();

            if (gvCepNoPagados.Rows.Count == 0)
            {
                if (string.IsNullOrWhiteSpace(oCEP.Error))
                    pnlContent.Controls.Add(HtmlWriter.Messages.PrintInfoMessage("Usted no tiene ninguna solicitud de trámites pendiente de pago."));
                else
                    HtmlWriter.Messages.ShowMainContentError(Master, divMain, oCEP.Error);
            }

        }
        catch
        {
            pnlContent.Controls.Add(HtmlWriter.Messages.PrintErrorMessage("Error al consultar Comprobantes Electrónicos de Pago por usuario."));
        }

    }

    protected void GridViewPageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        gvCepNoPagados.PageIndex = e.NewPageIndex;
        ConsultarCEPsTramites(HttpContext.Current.User.Identity.Name);
    }
}