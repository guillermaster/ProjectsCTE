using System;
using System.Configuration;
using Citaciones;


public partial class Consultas_Citaciones_CitacionPorCodigo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.DefaultFocus = txtCodCitacion.ID;
            //Page.Form.DefaultButton = btnConsultar.ID;
        }
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Infracciones oCitac = new Infracciones(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()], 
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()], 
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        detViewDetCitacion.DataSource = oCitac.CitacionesPorCodigoCitac(txtCodCitacion.Text.Trim());
        detViewDetCitacion.DataBind();
        if (detViewDetCitacion.Rows.Count == 0)
        {
            if (string.IsNullOrWhiteSpace(oCitac.Error))
                HtmlWriter.Messages.ShowModalAlertMessage(Master.MasterUpdatePanel, Master.GetType(), "No existe la citación con código " + txtCodCitacion.Text);
            else
                HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), oCitac.Error);
        }
    }
}