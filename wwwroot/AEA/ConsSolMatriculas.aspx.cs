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

public partial class ConsComercializadoras : System.Web.UI.Page
{
    private string currentUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.currentUser = User.Identity.Name.ToString();
        if (!IsPostBack)
        {
            ValidarAcceso();
            CargarGestores();
            CargarProvincias();
            SetNewButton();
            this.btnPrint.SetupPrintingElement("divContent", 600, 400);
        }
    }

    protected void SetNewButton()
    {
        this.btnNew.TargetURL = "RegSolMatricula.aspx?" + AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath;
    }

    protected void ValidarAcceso()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoConsSolMatricula))
        {
            Response.Redirect("AccessDenied.aspx");
        }
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoDesSolMatricula))
        {//esconder columna con el boton de desactivar
            this.gvSolMatriculas.Columns[this.gvSolMatriculas.Columns.Count - 1].Visible = false;
        }
    }

    protected void LoadGridInfo()
    {
        AEA.SolicitudMatricula oSolMatricula = new AEA.SolicitudMatricula(User.Identity.Name.ToString(), ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        string campoFiltro = "FECHAS";
        string valorFiltro;
        if(this.rbGestor.Checked)
            campoFiltro = "GES";
        else if(this.rbProv.Checked)
            campoFiltro = "PROV";

        switch(campoFiltro)
        {
            case "GES":
                valorFiltro = this.ddlGestor.SelectedValue;
                break;
            case "PROV":
                valorFiltro = this.ddlProv.SelectedValue;
                break;
            default:
                valorFiltro = null;
                break;
        }

        this.gvSolMatriculas.DataSource = oSolMatricula.ObtenerSolicitudes(DateTime.Parse(this.txtFechaIni.Text, new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false)), DateTime.Parse(this.txtFechaFin.Text, new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false)),
            campoFiltro, valorFiltro, false);
        this.gvSolMatriculas.DataBind();
    }


    protected void gvSolMatricula_Delete(object sender, EventArgs e)
    {
        ImageButton dlBtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)dlBtn.Parent.Parent;
        this.hdnCodComercializadora.Value = grdRow.Cells[0].Text;
        this.divDesactiva.Visible = true;
        this.gvSolMatriculas.Visible = false;
        this.divBusqueda.Visible = false;
    }

    protected void DetenerSolicitud()
    {
        AEA.SolicitudMatricula oSolicitud = new AEA.SolicitudMatricula(this.hdnCodComercializadora.Value, User.Identity.Name.ToString(), ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oSolicitud.DetenerSolicitud(this.txtDesactivaObs.Text))
        {
            LoadGridInfo();
            this.gvSolMatriculas.Visible = true;
            this.divDesactiva.Visible = false;
            this.divBusqueda.Visible = true;
            AlertJS(oSolicitud.TrxMessage);
        }
        else
        {
            AlertJS(oSolicitud.TrxError);
        }
    }


    protected void CargarProvincias()
    {
        AEA.Parametros oParametros = new AEA.Parametros(User.Identity.Name.ToString(), ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataView dvProvincias = oParametros.ProvinciasEcuador();
        //DataRow nullrow = dvProvincias.Table.NewRow();
        //nullrow[0] = null;
        //nullrow[1] = null;
        //dvProvincias.Table.Rows.InsertAt(nullrow, 0);
        this.ddlProv.DataSource = dvProvincias;
        this.ddlProv.DataValueField = dvProvincias.Table.Columns[0].ColumnName;
        this.ddlProv.DataTextField = dvProvincias.Table.Columns[1].ColumnName;
        this.ddlProv.DataBind();
    }

    protected void CargarGestores()
    {
        AEA.Gestor oGestor = new AEA.Gestor(User.Identity.Name.ToString(), ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataView dvGestores = oGestor.ObtenerGestores().DefaultView;
        dvGestores.Sort = dvGestores.Table.Columns[2].ColumnName;
        this.ddlGestor.DataSource = dvGestores;
        this.ddlGestor.DataTextField = dvGestores.Table.Columns[2].ColumnName;
        this.ddlGestor.DataValueField = dvGestores.Table.Columns[0].ColumnName;
        this.ddlGestor.DataBind();
    }



    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }

    protected void btnDesacitvar_Click(object sender, EventArgs e)
    {        
        DetenerSolicitud();
    }
    protected void btnDesacCancelar_Click(object sender, EventArgs e)
    {
        this.divDesactiva.Visible = false;
        this.gvSolMatriculas.Visible = true;
        this.divBusqueda.Visible = true;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadGridInfo();
    }
    protected void gvSolMatriculas_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("ConsSolMatricula.aspx?id=" + this.gvSolMatriculas.SelectedRow.Cells[0].Text
            + "&" + AEA.Parametros.QueryStringParams.returl + "=" + Page.AppRelativeVirtualPath);
    }
}
