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
    private static DataView dvGridViewData;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.currentUser = User.Identity.Name.ToString();
        if (!IsPostBack)
        {
            ValidateAccess();
            this.btnPrint.SetupPrintingElement("divContent", 600, 400);
            LoadCriteriosBusquedaList();
            CargarProvincias();
            SetNewButton();
        }
    }

    protected void ValidateAccess()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoConsComercializadora))
        {//esconder columna con el boton de desactivar
            Response.Redirect("AccessDenied.aspx");
        }
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoDesComercializadora))
        {//esconder columna con el boton de desactivar
            this.gvSucursales.Columns[this.gvSucursales.Columns.Count - 1].Visible = false;
        }
    }

    protected void SetNewButton()
    {
        this.btnNew.TargetURL = "Sucursal.aspx?" + AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath;
    }

    protected void LoadGridInfo()
    {
        AEA.Sucursal oSucursal = new AEA.Sucursal(currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        dvGridViewData = oSucursal.ObtenerSucursales(this.ddlBusqueda.SelectedValue.ToUpper(), ValorBusqueda()).DefaultView;
        dvGridViewData.Sort = string.Format("{0}, {1}", dvGridViewData.Table.Columns[1].ColumnName, dvGridViewData.Table.Columns[2].ColumnName);
        this.gvSucursales.DataSource = dvGridViewData;
        this.gvSucursales.DataBind();
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
        HiddenField hdnCodSuc = (HiddenField)grdRow.FindControl("hdnCodSuc");
        //this.hdnCodSucursal.Value = grdRow.Cells[0].Text;
        this.hdnCodSucursal.Value = hdnCodSuc.Value;
        this.txtDesactivaRazonSocial.Text = grdRow.Cells[1].Text;
        this.txtDesactivaRUC.Text = grdRow.Cells[2].Text;
        this.divDesactiva.Visible = true;
        this.gvSucursales.Visible = false;   
    }

    protected void DesactivaSucursal()
    {
        AEA.Sucursal oSucursal = new AEA.Sucursal(currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], int.Parse(this.hdnCodSucursal.Value));
        if (oSucursal.DesactivaSucursal(this.txtDesactivaObs.Text))
        {
            LoadGridInfo();
            this.gvSucursales.Visible = true;
            this.divDesactiva.Visible = false;
            AlertJS("Sucursal desactivada exitosamente");
        }
        else
        {
            AlertJS(oSucursal.TrxMessage + "\n" + oSucursal.TrxError);
        }
    }

    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }

    protected void btnDesacitvar_Click(object sender, EventArgs e)
    {
        DesactivaSucursal();
    }
    protected void btnDesacCancelar_Click(object sender, EventArgs e)
    {
        this.divDesactiva.Visible = false;
        this.gvSucursales.Visible = true;
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
        HiddenField hdnCodSuc = (HiddenField)this.gvSucursales.SelectedRow.FindControl("hdnCodSuc");
        //string idSucursal = this.gvSucursales.SelectedRow.Cells[0].Text;
        Response.Redirect("Sucursal.aspx?id=" + hdnCodSuc.Value + "&action=view&" + AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath);
    }

    protected void gvComercializadoras_Edit(object sender, EventArgs e)
    {
        ImageButton dlBtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)dlBtn.Parent.Parent;
        HiddenField hdnCodSuc = (HiddenField)grdRow.FindControl("hdnCodSuc");
        //string idSucursal = grdRow.Cells[0].Text;
        Response.Redirect("Sucursal.aspx?id=" + hdnCodSuc.Value + "&action=edit&" + AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath);
    }
}
