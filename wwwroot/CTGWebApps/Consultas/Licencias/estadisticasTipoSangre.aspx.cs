using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Brevetacion;

public partial class Consultas_Licencias_estadisticas : System.Web.UI.Page
{
    //private static DataTable dtResLicencias;
    private static DataView dvGridViewData;
    
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Seguridad.UsuarioWeb.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/DefaultConsultas.aspx");
            }
            CargarFiltros();
        }
    }


    protected void CargarFiltros()
    {
        Licencia oLicencia = new Licencia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        DataTable dtTiposSangre = oLicencia.ObtenerTiposSangre();
        this.ddlTipoSangre.DataSource = dtTiposSangre;
        this.ddlTipoSangre.DataTextField = "tipo_sangre";
        this.ddlTipoSangre.DataValueField = "codigo";
        this.ddlTipoSangre.DataBind();

        DataTable dtRangosEdades = oLicencia.ObtenerRangosEdades();
        this.ddlRangoEdad.DataSource = dtRangosEdades;
        this.ddlRangoEdad.DataTextField = "rango_edad";
        this.ddlRangoEdad.DataValueField = "codigo";
        this.ddlRangoEdad.DataBind();
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Licencia oLicencia = new Licencia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        DataTable dtResLicencias = oLicencia.LicenciasPorTipoSangreEdades(this.ddlTipoSangre.SelectedValue, this.ddlRangoEdad.SelectedValue);
        dvGridViewData = dtResLicencias.DefaultView;
        //dvGridViewData.Sort = string.Format("{0} {1}", "apellidos, nombres, edad", "ASC");
        //dvGridViewData.Sort = string.Format("{0} {1}", "apellidos, nombres", "ASC");
        dvGridViewData.Sort = string.Format("{0} {1}", "apellidos", "ASC");
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
        this.lblNumRegistros.Text = "Se han encontrado " + dtResLicencias.Rows.Count + " registros.";
        ShowAlphaFilters();
    }

    protected void ResLicGridView_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        this.gvResLicencias.PageIndex = e.NewPageIndex;
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }

    protected void HideAlphaFilters()
    {
        this.lnkA.Visible = false;
        this.lnkB.Visible = false;
        this.lnkC.Visible = false;
        this.lnkD.Visible = false;
        this.lnkE.Visible = false;
        this.lnkF.Visible = false;
        this.lnkG.Visible = false;
        this.lnkH.Visible = false;
        this.lnkI.Visible = false;
        this.lnkJ.Visible = false;
        this.lnkK.Visible = false;
        this.lnkL.Visible = false;
        this.lnkM.Visible = false;
        this.lnkN.Visible = false;
        this.lnkO.Visible = false;
        this.lnkP.Visible = false;
        this.lnkQ.Visible = false;
        this.lnkR.Visible = false;
        this.lnkS.Visible = false;
        this.lnkT.Visible = false;
        this.lnkU.Visible = false;
        this.lnkV.Visible = false;
        this.lnkW.Visible = false;
        this.lnkX.Visible = false;
        this.lnkY.Visible = false;
        this.lnkZ.Visible = false;
    }

    protected void ShowAlphaFilters()
    {
        this.lnkA.Visible = true;
        this.lnkB.Visible = true;
        this.lnkC.Visible = true;
        this.lnkD.Visible = true;
        this.lnkE.Visible = true;
        this.lnkF.Visible = true;
        this.lnkG.Visible = true;
        this.lnkH.Visible = true;
        this.lnkI.Visible = true;
        this.lnkJ.Visible = true;
        this.lnkK.Visible = true;
        this.lnkL.Visible = true;
        this.lnkM.Visible = true;
        this.lnkN.Visible = true;
        this.lnkO.Visible = true;
        this.lnkP.Visible = true;
        this.lnkQ.Visible = true;
        this.lnkR.Visible = true;
        this.lnkS.Visible = true;
        this.lnkT.Visible = true;
        this.lnkU.Visible = true;
        this.lnkV.Visible = true;
        this.lnkW.Visible = true;
        this.lnkX.Visible = true;
        this.lnkY.Visible = true;
        this.lnkZ.Visible = true;
    }

    #region "Eventos de Filtro Alfabético"
    protected void lnkA_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkA.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkB_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkB.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkC_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkC.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkD_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkD.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkE_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkE.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkF_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkF.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkG_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkG.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkH_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkH.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkI_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkI.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkJ_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkJ.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkK_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkK.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkL_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkL.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkM_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkM.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkN_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkN.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkO_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkO.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkP_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkP.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkQ_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkQ.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkR_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkR.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkS_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkS.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkT_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkT.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkU_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkU.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkV_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkV.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkW_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkW.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkX_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkX.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkY_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkY.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    protected void lnkZ_Click(object sender, EventArgs e)
    {
        dvGridViewData.RowFilter = "apellidos LIKE '" + this.lnkZ.Text + "%'";
        this.gvResLicencias.DataSource = dvGridViewData;
        this.gvResLicencias.DataBind();
    }
    #endregion
}
