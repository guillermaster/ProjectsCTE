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
    private static DataView _dvGridViewData;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.currentUser = User.Identity.Name.ToString();
        if (!IsPostBack)
        {
            SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoConsComercializadora))
            {//esconder columna con el boton de desactivar
                Response.Redirect("AccessDenied.aspx");
            }
            if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoDesComercializadora))
            {//esconder columna con el boton de desactivar
                this.gvComercializadoras.Columns[this.gvComercializadoras.Columns.Count - 1].Visible = false;
            }
            this.btnPrint.SetupPrintingElement("divContent", 600, 400);
            SetNewButton();
            LoadCriteriosBusquedaList();
            CargarProvincias();
        }
    }

    protected void SetNewButton()
    {
        this.btnNew.TargetURL = "Comercializadora.aspx?" + AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath;
    }

    protected void LoadGridInfo()
    {
        AEA.Comercializadora oComercializ = new AEA.Comercializadora(User.Identity.Name.ToString(), ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        _dvGridViewData = oComercializ.ObtenerComercializadoras(this.ddlBusqueda.SelectedValue.ToUpper(), ValorBusqueda()).DefaultView;
        _dvGridViewData.Sort = string.Format("{0}, {1}", _dvGridViewData.Table.Columns[1].ColumnName, _dvGridViewData.Table.Columns[2].ColumnName);
        this.gvComercializadoras.DataSource = _dvGridViewData;
        this.gvComercializadoras.DataBind();
    }

    protected void LoadCriteriosBusquedaList()
    {
        //DataTable dtCriteriosBus = new DataTable("criterios");
        //dtCriteriosBus.Columns.Add();
        //dtCriteriosBus.Columns.Add();
        string[] criterios = { "Todos", "Provincia", "Fecha", "Usuario" };
        this.ddlBusqueda.DataSource = criterios;
        this.ddlBusqueda.DataBind();
    }

    protected void CargarProvincias()
    {
        AEA.Parametros oParametros = new AEA.Parametros(User.Identity.Name.ToString(), ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataView dvProvincias = oParametros.ProvinciasEcuador();
        //DataRow nullrow = dvProvincias.Table.NewRow();
        //nullrow[0] = null;
        //nullrow[1] = null;
        //dvProvincias.Table.Rows.InsertAt(nullrow, 0);
        this.ddlBusqProvincia.DataSource = dvProvincias;
        this.ddlBusqProvincia.DataValueField = dvProvincias.Table.Columns[0].ColumnName;
        this.ddlBusqProvincia.DataTextField = dvProvincias.Table.Columns[1].ColumnName;
        this.ddlBusqProvincia.DataBind();
    }


    protected void gvComercializadoras_Delete(object sender, EventArgs e)
    {
        ImageButton dlBtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)dlBtn.Parent.Parent;
        HiddenField hdnCodComerc = (HiddenField)grdRow.FindControl("hdnCodComerc");
        //this.hdnCodComercializadora.Value = grdRow.Cells[0].Text;
        this.hdnCodComercializadora.Value = hdnCodComerc.Value;
        this.txtDesactivaRazonSocial.Text = grdRow.Cells[1].Text;
        this.txtDesactivaRUC.Text = grdRow.Cells[2].Text;
        this.divDesactiva.Visible = true;
        this.gvComercializadoras.Visible = false;   
    }

    protected void DesactivaComercializadora()
    {
        AEA.Comercializadora oComercializ = new AEA.Comercializadora(int.Parse(this.hdnCodComercializadora.Value), User.Identity.Name.ToString(), ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oComercializ.DesactivaComercializadora(this.txtDesactivaObs.Text))
        {
            LoadGridInfo();
            this.gvComercializadoras.Visible = true;
            this.divDesactiva.Visible = false;
            AlertJS(oComercializ.TrxMessage);
        }
        else
        {
            AlertJS(oComercializ.TrxError);
        }
    }

    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }

    protected void btnDesacitvar_Click(object sender, EventArgs e)
    {
        DesactivaComercializadora();
    }
    protected void btnDesacCancelar_Click(object sender, EventArgs e)
    {
        this.divDesactiva.Visible = false;
        this.gvComercializadoras.Visible = true;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadGridInfo();
    }
    protected void ddlBusqueda_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (this.ddlBusqueda.SelectedValue)
        {
            case "Todos":
                this.ddlBusqProvincia.Visible = false;
                this.txtBusqFecha.Visible = false;
                this.txtBusqUsuario.Visible = false;
                break;
            case "Provincia":
                this.ddlBusqProvincia.Visible = true;
                this.txtBusqFecha.Visible = false;
                this.txtBusqUsuario.Visible = false;
                break;
            case "Fecha":
                this.ddlBusqProvincia.Visible = false;
                this.txtBusqFecha.Visible = true;
                this.txtBusqUsuario.Visible = false;
                break;
            case "Usuario":
                this.ddlBusqProvincia.Visible = false;
                this.txtBusqFecha.Visible = false;
                this.txtBusqUsuario.Visible = true;
                break;
        }
    }

    protected string ValorBusqueda()
    {
        string value;
        switch (this.ddlBusqueda.SelectedValue)
        {
            case "Todos":
                value = null;
                break;
            case "Provincia":
                value = this.ddlBusqProvincia.SelectedValue;
                break;
            case "Fecha":
                value = this.txtBusqFecha.Text;
                break;
            case "Usuario":
                value = this.txtBusqUsuario.Text;
                break;
            default:
                value = null;
                break;
        }
        return value;
    }
    protected void gvComercializadoras_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField hdnCodComerc = (HiddenField)this.gvComercializadoras.SelectedRow.FindControl("hdnCodComerc");
        //string idComercializadora = this.gvComercializadoras.SelectedRow.Cells[0].Text;
        Response.Redirect("Comercializadora.aspx?id=" + hdnCodComerc.Value + "&action=view&" + AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath);
    }

    protected void gvComercializadoras_Edit(object sender, EventArgs e)
    {
        ImageButton dlBtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)dlBtn.Parent.Parent;
        HiddenField hdnCodComerc = (HiddenField)grdRow.FindControl("hdnCodComerc");
        //string idComercializadora = grdRow.Cells[0].Text;
        Response.Redirect("Comercializadora.aspx?id=" + hdnCodComerc.Value + "&action=edit&" + AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath);
    }
}
