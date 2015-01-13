using System;
using System.Configuration;
using System.Web;
using Citaciones;
using System.Data;

public partial class Consultas_Licencias_PuntosLicencia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ConsultarPuntos(HttpContext.Current.User.Identity.Name);
    }
    
    protected void ConsultarPuntos(string id)
    {
        Infracciones oCitac = new Infracciones(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()], 
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()], 
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        //string puntos_perdidos = oCitac.PuntosPerdidosPorLicencia(id);
        string puntosPerdidos = oCitac.PuntosPorLicencia(id);
        if (!string.IsNullOrWhiteSpace(puntosPerdidos))
        {
            HtmlWriter.Messages.HideMainContentError(Master, divContent);
            int puntosInicio = oCitac.PuntosInicioLicencia();
            dvPuntos.DataSource = ArrangeData(id, puntosPerdidos, puntosInicio == -1 ? "Error" : puntosInicio.ToString());
            dvPuntos.DataBind();
        }
        else
            HtmlWriter.Messages.ShowMainContentError(Master, divContent, "Error al consultar información");
    }

    protected DataTable ArrangeData(string identificacion, string ptosPerdidos, string ptosInicio)
    {
        DataTable dtData = new DataTable();
        dtData.Columns.Add("Identificación");
        //dtData.Columns.Add("Puntos referenciales (a perder)");
        dtData.Columns.Add("Puntos perdidos");
        dtData.Columns.Add("Puntos inicialmente asignados");
        DataRow dr = dtData.NewRow();
        dr[0] = identificacion;
        dr[1] = ptosPerdidos;
        dr[2] = ptosInicio;
        dtData.Rows.Add(dr);
        return dtData;
    }
}