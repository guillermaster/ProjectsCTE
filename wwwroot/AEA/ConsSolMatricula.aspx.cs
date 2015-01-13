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

public partial class ConsSolMatricula : System.Web.UI.Page
{
    private string currentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.currentUser = User.Identity.Name.ToString();
        if (!IsPostBack)
        {            
            this.btnPrint.SetupPrintingElement("divContent",  710, 460);
            ValidarAcceso();
            SetBackButton();
            if (Request.QueryString["id"] != null)
            {
                try
                {
                    LoadDetallesSolicitud(int.Parse(Request.QueryString["id"]));
                    this.hdnCodSolicitud.Value = Request.QueryString["id"];
                    this.lblPageTitle.Text = "Solicitud No. " + Request.QueryString["id"];
                    this.lblPageSubtitle.Text = "Esta solicitud contiene " + this.gvDetallesSolicitud.Rows.Count.ToString() + " automotores";
                }
                catch (Exception ex)
                {
                    ShowFailureMessage(ex.Message);
                }
            }
            else
            {
                ShowFailureMessage("No se ha especificado la solicitud que desea consultar");
            }
        }
    }

    protected void SetBackButton()
    {
        if (Request.QueryString[AEA.Parametros.QueryStringParReturnURL] != null && Request.QueryString[AEA.Parametros.QueryStringParReturnURL] != "")
        {
            this.btnBack.Visible = true;
            this.btnBack.TargetURL = Request.QueryString[AEA.Parametros.QueryStringParReturnURL];
        }
        else
            this.btnBack.Visible = false;
    }

    protected void ValidarAcceso()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoConsSolMatricula))
        {
            Response.Redirect("AccessDenied.aspx");
        }
    }

    protected void LoadDetallesSolicitud(int idSolicitud)
    {
        AEA.SolicitudMatricula oSolMat = new AEA.SolicitudMatricula(idSolicitud.ToString(), currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oSolMat.CargarDescCabecera())
        {
            this.lblComerc.Text = oSolMat.Comercializadora;
            this.lblSucursal.Text = oSolMat.Sucursal;
            this.lblGestor.Text = oSolMat.Gestor;
        }
        DataTable dtDetSolMat = oSolMat.ObtenerDetallesSolicitud();
        this.gvDetallesSolicitud.DataSource = dtDetSolMat;
        this.gvDetallesSolicitud.DataBind();
        this.btnPrint.Visible = true;
    }


    protected void ShowSuccessMessage(string message)
    {
        this.divError.Visible = false;
        this.divWarning.Visible = true;
        this.lblMsgWarning.Text = message;
    }


    protected void ShowFailureMessage(string message)
    {
        this.divWarning.Visible = false;
        this.divError.Visible = true;
        this.lblMsgError.Text = message;
    }

    protected void gvDetallesSolicitudGridView_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (e.Row.DataItem != null)
        {
            HiddenField hdnCodTramite = (HiddenField)e.Row.FindControl("hdnIdTramite");
            HiddenField hdnObservacion = (HiddenField)e.Row.FindControl("hdnObservacion");
            this.gvDetallesSolicitud.HeaderRow.Cells[this.gvDetallesSolicitud.HeaderRow.Cells.Count - 1].Visible = false;
            this.gvDetallesSolicitud.HeaderRow.Cells[this.gvDetallesSolicitud.HeaderRow.Cells.Count - 2].Visible = false;
            if (hdnCodTramite.Value == "" && hdnObservacion.Value=="" )
                e.Row.Cells[e.Row.Cells.Count - 2].Visible = false;//esconder boton de selección

            if (e.Row.Cells[e.Row.Cells.Count - 4].Text == "DETENIDA")
                e.Row.Cells[e.Row.Cells.Count - 1].Visible = false;//esconder boton de eliminar
        }
    }

    protected void gvDetallesSolicitud_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hdnFechaProceso = (HiddenField)this.gvDetallesSolicitud.SelectedRow.FindControl("hdnFechaProceso");
        HiddenField hdnCodTramite = (HiddenField)this.gvDetallesSolicitud.SelectedRow.FindControl("hdnIdTramite");
        HiddenField hdnObservacion = (HiddenField)this.gvDetallesSolicitud.SelectedRow.FindControl("hdnObservacion");
        DataTable dtData = new DataTable();
        dtData.Columns.Add(new DataColumn("Fecha de Proceso: "));
        dtData.Columns.Add(new DataColumn("Código de Trámite: "));
        dtData.Columns.Add(new DataColumn("Observacion: "));
        DataRow dr = dtData.NewRow();
        dr[0] = hdnFechaProceso.Value;
        dr[1] = hdnCodTramite.Value;
        dr[2] = hdnObservacion.Value;
        dtData.Rows.Add(dr);
        this.detviewSolicitud.DataSource = dtData;
        this.detviewSolicitud.DataBind();
        this.Panel1.Visible = true;
    }
    protected void btnCloseDet_Click(object sender, ImageClickEventArgs e)
    {
        this.Panel1.Visible = false;
    }
    protected void gvDetalles_Delete(object sender, EventArgs e)
    {
        ImageButton dlBtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)dlBtn.Parent.Parent;
        HiddenField hdnIdSelDet = (HiddenField)grdRow.FindControl("hdnIdDetalleSol");
        this.hdnSelDet.Value = hdnIdSelDet.Value;
        this.lblRAMV.Text = grdRow.Cells[0].Text;
        this.divDesactiva.Visible = true;
        this.gvDetallesSolicitud.Visible = false;
    }
    protected void btnDesacitvar_Click(object sender, EventArgs e)
    {
        AEA.SolicitudMatricula oSolMat = new AEA.SolicitudMatricula(this.hdnCodSolicitud.Value, currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oSolMat.DetenerDetalleSolicitud(int.Parse(this.hdnSelDet.Value), this.txtObservacion.Text))
        {
            LoadDetallesSolicitud(int.Parse(this.hdnCodSolicitud.Value));
            this.gvDetallesSolicitud.Visible = true;
            this.divDesactiva.Visible = false;
            this.divError.Visible = false;
            AlertJS(oSolMat.TrxMessage);
        }
        else
        {
            ShowFailureMessage(oSolMat.TrxError);
        }
    }
    

    protected void btnDesacCancelar_Click(object sender, EventArgs e)
    {
        this.divDesactiva.Visible = false;
        this.gvDetallesSolicitud.Visible = true;
        this.divError.Visible = false;
        this.divWarning.Visible = false;
    }

    

    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }
    
}

