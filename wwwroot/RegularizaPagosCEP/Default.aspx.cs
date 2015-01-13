using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            linkPagosEncCitac.NavigateUrl = "ConsultaPagosEncolados.aspx?tipo=" + ComprobanteElectronicoPago.RegularizacionCEP.TipoCitacion;
            linkPagosEncTram.NavigateUrl = "ConsultaPagosEncolados.aspx?tipo=" + ComprobanteElectronicoPago.RegularizacionCEP.TipoTramite;
        }
    }

    protected void btnReversoAXIS_Click(object sender, EventArgs e)
    {
        ComprobanteElectronicoPago.RegularizacionCEP oRegCEP = new ComprobanteElectronicoPago.RegularizacionCEP(
           ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
           ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
           ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        if (oRegCEP.ConciliaReversosAXIS())
            HtmlWriter.Messages.ShowModalInfoMessage(Master.MasterUpdatePanel, Master.GetType(), "La transacción se ejecutó correctamente");
        else
            HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), oRegCEP.Error.Substring(0, 20));
    }
}
