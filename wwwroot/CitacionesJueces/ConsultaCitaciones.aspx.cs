using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.Shared;

public partial class _Default : System.Web.UI.Page 
{
    private static DataView dvGridViewData;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Auxiliar.Helper.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Default.aspx");
            }

            Page.Form.DefaultButton = this.btnConsultar.ID;

            switch (Request.QueryString["critcons"])
            {
                case "fech":
                    this.divConsPorFecha.Visible = true;
                    Page.Form.DefaultFocus = this.txtFechaDesde.ID;
                    break;
                case "ced":
                    this.divConsPorCedula.Visible = true;
                    Page.Form.DefaultFocus = this.txtCedula.ID;
                    break;
                case "plac":
                    this.divConsPorPlaca.Visible = true;
                    Page.Form.DefaultFocus = this.txtPlaca.ID;
                    break;
                case "citac":
                    this.divConsPorCodCitac.Visible = true;
                    Page.Form.DefaultFocus = this.txtNumCitacion.ID;
                    break;
                default:
                    this.divConsPorCedula.Visible = true;
                    Page.Form.DefaultFocus = this.txtCedula.ID;
                    break;
            }
        }
    }

        
    #region "Métodos de Consulta"

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ConsultarCitaciones();
    }

    protected void ConsultarCitaciones()
    {
        SetTitleOnActionGridColumn(this.ddlAccion.SelectedValue);
        Auxiliar.CitacionesJueces oCitaciones = new Auxiliar.CitacionesJueces(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        switch (Request.QueryString["critcons"])
        {
            case "fech":
                CargarCitaciones(oCitaciones.CitacionesPorFechas(this.txtFechaDesde.Text, this.txtFechaHasta.Text, ReturnPaidCitacOption(), this.ddlAccion.SelectedValue));
                break;
            case "ced":
                CargarCitaciones(oCitaciones.CitacionesPorIdentificacion(this.txtCedula.Text, ReturnPaidCitacOption(), this.ddlAccion.SelectedValue));
                break;
            case "plac":
                CargarCitaciones(oCitaciones.CitacionesPorPlaca(this.txtPlaca.Text.ToUpper(), ReturnPaidCitacOption(), this.ddlAccion.SelectedValue));
                break;
            case "citac":
                CargarCitaciones(oCitaciones.CitacionesPorCodigoCitac(this.txtNumCitacion.Text, ReturnPaidCitacOption(), this.ddlAccion.SelectedValue));
                break;
            default:
                CargarCitaciones(oCitaciones.CitacionesPorIdentificacion(this.txtCedula.Text, ReturnPaidCitacOption(), this.ddlAccion.SelectedValue));
                break;
        }
        this.lblError.Text = oCitaciones.Error;
    }

    #endregion

    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }

    #region "Métodos Gridview"

    private void CargarCitaciones(DataTable dtCitaciones)
    {
        dtCitaciones.DefaultView.Sort = "fec_infraccion DESC";
        dvGridViewData = dtCitaciones.DefaultView;
        this.dgCitaciones.DataSource = dvGridViewData;
        this.dgCitaciones.DataBind();
        //this.dgCitaciones.Columns[1].Visible = false;
    }

    protected void dgCitaciones_SelectedIndexChanged(object sender, EventArgs e)
    {
        Auxiliar.CitacionesJueces oCitaciones = new Auxiliar.CitacionesJueces(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        //if (oCitaciones.esJuezDeTurno(this.dgCitaciones.SelectedRow.Cells[1].Text, Session["Username"].ToString()))
        //{
            string codCitacion = this.dgCitaciones.SelectedRow.Cells[2].Text;
            this.imgBtnCitacion.ImageUrl = "imgCitacion.aspx?codCitacion=" + codCitacion;
            this.imgBtnCitacion.DataBind();
        
            this.HideControlsButImgCitacion();
            this.lblError.Text = "HAGA CLIC EN LA IMAGEN PARA CERRARLA";
        //}
        //else
        //{
        //    AlertJS(oCitaciones.Error);
        //}
    }
           
    protected void dgCitaciones_EditIndexChanged(object sender, EventArgs e)
    {
        this.txtCodCitacionModif.Text = this.dgCitaciones.Rows[((System.Web.UI.WebControls.GridViewEditEventArgs)e).NewEditIndex].Cells[2].Text;
        this.hdnFactura.Value = this.dgCitaciones.Rows[((System.Web.UI.WebControls.GridViewEditEventArgs)e).NewEditIndex].Cells[1].Text;
        HideControlsButCitacionAction();
    }

    protected void imgBtnCitacion_Click(object sender, ImageClickEventArgs e)
    {
        this.ShowControlsButImgCitacion();
        this.lblError.Text = "";
    }

    protected void CitacGridView_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        this.dgCitaciones.PageIndex = e.NewPageIndex;
        this.dgCitaciones.DataSource = dvGridViewData;
        this.dgCitaciones.DataBind();
    }
    
    #endregion


    #region "Acción de Botones en Area de Impugnación"
    protected void btnImpugnar_Click(object sender, EventArgs e)
    {              
        Auxiliar.CitacionesJueces oCitaciones = new Auxiliar.CitacionesJueces(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        if (oCitaciones.ActualizarCitacion(this.hdnFactura.Value, this.txtObservacion.Text, Session["Username"].ToString(), this.ddlAccion.SelectedValue))
            this.lblError.Text = "Se ha realizado la " + this.dgCitaciones.Columns[16].HeaderText + " correctamente";
        else
            this.lblError.Text = "Error: No se pudo realizar la " + this.dgCitaciones.Columns[16].HeaderText + ".  " + oCitaciones.Error;

        ShowControlsButCitacionAction();
        this.txtObservacion.Text = "";
        this.dgCitaciones.DataSource = null;
        this.dgCitaciones.DataBind();
    }


    protected void btnCancelarImpugnar_Click(object sender, EventArgs e)
    {
        ShowControlsButCitacionAction();
    }
    #endregion

    protected string ReturnPaidCitacOption()
    {
        if (this.chkIncludePaid.Checked) return "S";
        else return "N";
    }
    
    protected void SetTitleOnActionGridColumn(string opcAccion)
    {
        int posColumnBtnImpugnar = this.dgCitaciones.Columns.Count - 1;
        switch (opcAccion)
        {
            case "I":
                this.dgCitaciones.Columns[posColumnBtnImpugnar].Visible = true;
                this.dgCitaciones.Columns[posColumnBtnImpugnar].HeaderText = "Impugnación";
                this.btnImpugnar.Text = "Impugnar";
                break;
            case "V":
                this.dgCitaciones.Columns[posColumnBtnImpugnar].Visible = true;
                this.dgCitaciones.Columns[posColumnBtnImpugnar].HeaderText = "Verificación";
                this.btnImpugnar.Text = "Solicitar Verificación";
                break;
            case "C":
                this.dgCitaciones.Columns[posColumnBtnImpugnar].Visible = true;
                this.dgCitaciones.Columns[posColumnBtnImpugnar].HeaderText = "Condenación";
                this.btnImpugnar.Text = "Condenar";
                break;
            case "A":
                this.dgCitaciones.Columns[posColumnBtnImpugnar].Visible = true;
                this.dgCitaciones.Columns[posColumnBtnImpugnar].HeaderText = "Absolución";
                this.btnImpugnar.Text = "Absolver";
                break;
            default:
                this.dgCitaciones.Columns[posColumnBtnImpugnar].Visible = false;
                break;
        }
    }
    
    protected void HideControlsButImgCitacion()
    {
        this.divHeader.Visible = false;
        this.divConsPorCedula.Visible = false;
        this.divConsPorCodCitac.Visible = false;
        this.divConsPorFecha.Visible = false;
        this.divConsPorPlaca.Visible = false;
        this.btnConsultar.Visible = false;
        this.dgCitaciones.Visible = false;
        this.chkIncludePaid.Visible = false;
        this.ddlAccion.Visible = false;
        this.lblAccion.Visible = false;
        this.imgBtnCitacion.Visible = true;
        this.btnImprimir.Visible = true;
    }

    protected void ShowControlsButImgCitacion()
    {
        switch (Request.QueryString["critcons"])
        {
            case "fech":
                this.divConsPorFecha.Visible = true;
                Page.Form.DefaultFocus = this.txtFechaDesde.ID;
                break;
            case "ced":
                this.divConsPorCedula.Visible = true;
                Page.Form.DefaultFocus = this.txtCedula.ID;
                break;
            case "plac":
                this.divConsPorPlaca.Visible = true;
                Page.Form.DefaultFocus = this.txtPlaca.ID;
                break;
            case "citac":
                this.divConsPorCodCitac.Visible = true;
                Page.Form.DefaultFocus = this.txtNumCitacion.ID;
                break;
            default:
                this.divConsPorCedula.Visible = true;
                Page.Form.DefaultFocus = this.txtCedula.ID;
                break;
        }
        this.btnConsultar.Visible = true;
        this.divHeader.Visible = true;
        this.dgCitaciones.Visible = true;
        if(this.ddlAccion.SelectedValue=="") this.chkIncludePaid.Visible = true;
        this.ddlAccion.Visible = true;
        this.lblAccion.Visible = true;
        this.imgBtnCitacion.Visible = false;
        this.btnImprimir.Visible = false;
    }

    protected void HideControlsButCitacionAction()
    {
        this.divConsPorCedula.Visible = false;
        this.divConsPorCodCitac.Visible = false;
        this.divConsPorFecha.Visible = false;
        this.divConsPorPlaca.Visible = false;
        this.btnConsultar.Visible = false;
        this.dgCitaciones.Visible = false;
        this.chkIncludePaid.Visible = false;
        this.imgBtnCitacion.Visible = false;
        this.btnImprimir.Visible = false;
        this.lblAccion.Visible = false;
        this.ddlAccion.Visible = false;
        this.divImpugnacion.Visible = true;
    }

    protected void ShowControlsButCitacionAction()
    {
        switch (Request.QueryString["critcons"])
        {
            case "fech":
                this.divConsPorFecha.Visible = true;
                Page.Form.DefaultFocus = this.txtFechaDesde.ID;
                break;
            case "ced":
                this.divConsPorCedula.Visible = true;
                Page.Form.DefaultFocus = this.txtCedula.ID;
                break;
            case "plac":
                this.divConsPorPlaca.Visible = true;
                Page.Form.DefaultFocus = this.txtPlaca.ID;
                break;
            case "citac":
                this.divConsPorCodCitac.Visible = true;
                Page.Form.DefaultFocus = this.txtNumCitacion.ID;
                break;
            default:
                this.divConsPorCedula.Visible = true;
                Page.Form.DefaultFocus = this.txtCedula.ID;
                break;
        }
        this.btnConsultar.Visible = true;
        this.dgCitaciones.Visible = true;
        if (this.ddlAccion.SelectedValue == "") this.chkIncludePaid.Visible = true;
        this.lblAccion.Visible = true;
        this.ddlAccion.Visible = true;
        this.imgBtnCitacion.Visible = false;
        this.btnImprimir.Visible = false;
        this.divImpugnacion.Visible = false;
    }
    
    protected void ddlAccion_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlAccion.SelectedValue == "")
            this.chkIncludePaid.Visible = true;
        else
            this.chkIncludePaid.Visible = false;
    }
    /*
    protected DataSet ReturnActaNotifData(string codCitac)
    {
        Auxiliar.ActasJueces oActasJueces = new Auxiliar.ActasJueces(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        return oActasJueces.GetDataForActaNotificacion(codCitac);
    }

    protected DataSet ReturnActaJuzgamData(string codCitac)
    {
        Auxiliar.ActasJueces oActasJueces = new Auxiliar.ActasJueces(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        return oActasJueces.GetDataForActaJuzgamiento(codCitac);
    }

    protected void GenerarReporte(string codCitac)
    {
        Reporte.ActaNotificacion repActaNotif = new Reporte.ActaNotificacion();

        try
        {
            repActaNotif.SetDataSource(ReturnActaNotifData(codCitac));
        }
        catch (Exception ex)
        {
            if (ex.Message == "System.IndexOutOfRangeException: Index was outside the bounds of the array.")
            {
                this.lblError.Text = "Error al generar código del reporte";
            }
            else
            {
                this.lblError.Text = "Error al consultar datos para reporte";
            }
        }

        try
        {
            System.IO.MemoryStream rptStream = new System.IO.MemoryStream();
            repActaNotif.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "ActaNotificacion");
            repActaNotif.Dispose();
            repActaNotif.Close();
        }
        catch (Exception ex)
        {
            this.lblError.Text = "ERROR AL GENERAR EL ARCHIVO PDF DEL CERTIFICADO<BR />" + ex.Message;
        }
    }


    protected void GenerarReporteActaJuzgamiento(string codCitac)
    {
        Reporte.ActaJuzgamiento repActaJuzgam = new Reporte.ActaJuzgamiento();

        try
        {
            repActaJuzgam.SetDataSource(ReturnActaJuzgamData("2863396"));
        }
        catch (Exception ex)
        {
            if (ex.Message == "System.IndexOutOfRangeException: Index was outside the bounds of the array.")
            {
                this.lblError.Text = "Error al generar código del reporte";
            }
            else
            {
                this.lblError.Text = "Error al consultar datos para reporte";
            }
        }

        try
        {
            repActaJuzgam.ExportToHttpResponse(ExportFormatType.PortableDocFormat, Response, true, "ActaJuzgamiento");
            repActaJuzgam.Dispose();
            repActaJuzgam.Close();
        }
        catch (Exception ex)
        {
            this.lblError.Text = "ERROR AL GENERAR EL ARCHIVO PDF DEL CERTIFICADO<BR />" + ex.Message;
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.dgCitaciones.Rows.Count; i++)
        {
            if (((CheckBox)(this.dgCitaciones.Rows[i].Cells[0].Controls[1])).Checked)
            {
                GenerarReporte(this.dgCitaciones.Rows[i].Cells[2].Text);
            }
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.dgCitaciones.Rows.Count; i++)
        {
            if (((CheckBox)(this.dgCitaciones.Rows[i].Cells[0].Controls[1])).Checked)
            {
                GenerarReporteActaJuzgamiento(this.dgCitaciones.Rows[i].Cells[2].Text);
            }
        }
    }
    */
}
