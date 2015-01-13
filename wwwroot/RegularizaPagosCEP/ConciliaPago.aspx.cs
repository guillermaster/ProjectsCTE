using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ConciliaPago : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ComprobanteElectronicoPago.RegularizacionCEP oRegCEP = new ComprobanteElectronicoPago.RegularizacionCEP(
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        if(oRegCEP.ConciliaPago(this.txtCEP.Text, this.txtFecha.Text))
            HtmlWriter.Messages.ShowModalInfoMessage(Master.MasterUpdatePanel, Master.GetType(), "La transacción se ejecutó correctamente");
        else
            HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), oRegCEP.Error);
    }
}