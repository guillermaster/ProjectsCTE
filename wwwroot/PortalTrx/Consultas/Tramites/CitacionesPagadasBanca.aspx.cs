using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ComprobanteElectronicoPago;

public partial class Consultas_Tramites_CitacionesPagadasBanca : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ConsultarCEPsCitaciones(HttpContext.Current.User.Identity.Name);
        }
    }

    protected void ConsultarCEPsCitaciones(string identificacion)
    {
        CEP oCEP = new CEP(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        try
        {
            DataTable dtCEPs = oCEP.ConsultaCEPCitacionesPagadasxUsuario(identificacion);

            dtCEPs.DefaultView.Sort = dtCEPs.Columns[1].ColumnName + " DESC";
            gvCitacionesPagadas.DataSource = dtCEPs.DefaultView;
            gvCitacionesPagadas.DataBind();

            if (gvCitacionesPagadas.Rows.Count == 0)
            {
                if (string.IsNullOrWhiteSpace(oCEP.Error))
                    pnlContent.Controls.Add(HtmlWriter.Messages.PrintInfoMessage("Usted ha realizado ningún pago de citaciones a través de la banca."));
                else
                    HtmlWriter.Messages.ShowMainContentError(Master, divMain, oCEP.Error);
            }
        }
        catch
        {
            pnlContent.Controls.Add(HtmlWriter.Messages.PrintErrorMessage("Error al consultar Comprobantes Electrónicos de Pago por usuario."));
        }

    }

    protected void GvCitacionesPagadasSelectedIndexChanged(object sender, EventArgs e)
    {
        CargarDatosAdicionalesCitacion(gvCitacionesPagadas.SelectedRow.Cells[1].Text);
        MPE.Show();
    }

    protected void CargarDatosAdicionalesCitacion(string cep)
    {
        CEP oCEP = new CEP(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        DataTable dtDetallesCitacion = oCEP.ConsultaCitacionesEnCEP(cep);

        gvDetCitaciones.DataSource = dtDetallesCitacion;
        gvDetCitaciones.DataBind();
    }


}