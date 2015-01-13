using System;
using System.Configuration;
using Canchon;

public partial class Consultas_Vehiculos_VehiculosCanchon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        CanchonClass oCanchon = new CanchonClass(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()], 
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()], 
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        detViewVehCanchon.DataSource = oCanchon.ConsultarVehiculo(ddlTipoIdent.SelectedValue, txtIdentificacion.Text.ToUpper().Trim());
        detViewVehCanchon.DataBind();

        if (detViewVehCanchon.Rows.Count == 0)
        {
            if (string.IsNullOrWhiteSpace(oCanchon.Error))
                HtmlWriter.Messages.ShowModalAlertMessage(Master.MasterUpdatePanel, Master.GetType(), "El vehículo con " + ddlTipoIdent.SelectedItem.Text + " " + txtIdentificacion.Text.ToUpper() + " no se encuentra en ningún Centro de Retención Vehicular de la CTE.");
            else
                HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), oCanchon.Error);
        }
    }
}