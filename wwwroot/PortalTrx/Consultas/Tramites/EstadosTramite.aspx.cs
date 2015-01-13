using System;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using ComprobanteElectronicoPago;


public partial class Consultas_Tramites_Tracking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        CargarDatosTramite(txtNumTramite.Text);
    }

    protected void CargarDatosTramite(string numTramite)
    {
        CEP oCEP = new CEP(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        DataTable dtTramite = oCEP.ConsultaDatosTramite(numTramite);
        gvEtapasTramite.DataSource = dtTramite;
        gvEtapasTramite.DataBind();
        if(gvEtapasTramite.Rows.Count==0)
            pnlNumCedula.Controls.Add(HtmlWriter.Messages.PrintInfoMessage("No se encontró información del trámite número " + numTramite));
    }


}