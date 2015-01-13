using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brevetacion;

public partial class Consultas_Licencias_LicGroupBySangre : Page
{
    private static DataView _dvGridViewData;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDropDownLists();
        }
    }

    protected void LoadDropDownLists()
    {
        Licencia oLicencia = new Licencia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        DataTable dtTiposSangre = oLicencia.ObtenerTiposSangre();
        ddlTipoSangre.DataSource = dtTiposSangre;
        ddlTipoSangre.DataTextField = "tipo_sangre";
        ddlTipoSangre.DataValueField = "codigo";
        ddlTipoSangre.DataBind();

        DataTable dtRangosEdades = oLicencia.ObtenerRangosEdades();
        ddlRangoEdad.DataSource = dtRangosEdades;
        ddlRangoEdad.DataTextField = "rango_edad";
        ddlRangoEdad.DataValueField = "codigo";
        ddlRangoEdad.DataBind();

        if (dtTiposSangre.Rows.Count == 0 || dtRangosEdades.Rows.Count == 0)
            HtmlWriter.Messages.ShowMainContentError(Master, divMain, "Se ha producido un error al cargar esta página, por favor intente nuevamente más tarde.");        
    }

    protected void AZfilter1FilterClicked(object sender, EventArgs e)
    {
        string filtro = "apellidos LIKE '" + AZfilter1.CaracterFiltro + "%'";
        _dvGridViewData.RowFilter = filtro;
        gvResLicencias.DataSource = _dvGridViewData;
        gvResLicencias.DataBind();
    }

    protected void BtnConsultarClick(object sender, EventArgs e)
    {
        Licencia oLicencia = new Licencia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        DataTable dtResLicencias = oLicencia.LicenciasPorTipoSangreEdades(ddlTipoSangre.SelectedValue, ddlRangoEdad.SelectedValue);
        _dvGridViewData = dtResLicencias.DefaultView;
        _dvGridViewData.Sort = string.Format("{0} {1}", "apellidos", "ASC");
        gvResLicencias.DataSource = _dvGridViewData;
        gvResLicencias.DataBind();
        if (gvResLicencias.Rows.Count > 0)
        {
            //this.lblNumRegistros.Text = "Se han encontrado " + dtResLicencias.Rows.Count + " registros.";
            AZfilter1.Visible = true;
        }
        else
        {
            AZfilter1.Visible = false;
            HtmlWriter.Messages.ShowModalFailureMessage((UpdatePanel)Master.FindControl("UpdatePanel1"), GetType(), "Se produjo un error al consultar la información");
        }
        
    }

    protected void ResLicGridViewPageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        gvResLicencias.PageIndex = e.NewPageIndex;
        gvResLicencias.DataSource = _dvGridViewData;
        gvResLicencias.DataBind();
    }
}