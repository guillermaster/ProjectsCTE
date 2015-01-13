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
            ValidateAccess();
            CargarGestores();
            CargarProvincias();
            CargarUsuariosCTG();
            this.btnPrint.SetupPrintingElement("divContent", 600, 400);
        }
    }

    protected void ValidateAccess()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoConsSolMatricula))
        {
            Response.Redirect("AccessDenied.aspx");
        }
    }


    protected void LoadGridInfo()
    {
        AEA.SolicitudMatricula oSolMatricula = new AEA.SolicitudMatricula(User.Identity.Name.ToString(), ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        string campoFiltro = "FECHAS";
        string valorFiltro;
        if (this.rbGestor.Checked)
            campoFiltro = "GES";
        else if (this.rbProv.Checked)
            campoFiltro = "PROV";
        else if (this.rbUsuario.Checked)
            campoFiltro = "CAJERO";

        switch(campoFiltro)
        {
            case "GES":
                valorFiltro = this.ddlGestor.SelectedValue;
                break;
            case "PROV":
                valorFiltro = this.ddlProv.SelectedValue;
                break;
            case "CAJERO":
                valorFiltro = this.ddlUsuario.SelectedValue;
                break;
            default:
                valorFiltro = null;
                break;
        }

        this.gvSolMatriculas.DataSource = oSolMatricula.ObtenerSolicitudes(DateTime.Parse(this.txtFechaIni.Text, new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false)), DateTime.Parse(this.txtFechaFin.Text, new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false)),
            campoFiltro, valorFiltro, true);
        this.gvSolMatriculas.DataBind();
    }


    



    protected void CargarProvincias()
    {
        AEA.Parametros oParametros = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
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
        AEA.Gestor oGestor = new AEA.Gestor(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataView dvGestores = oGestor.ObtenerGestores().DefaultView;
        dvGestores.Sort = dvGestores.Table.Columns[2].ColumnName;
        this.ddlGestor.DataSource = dvGestores;
        this.ddlGestor.DataTextField = dvGestores.Table.Columns[2].ColumnName;
        this.ddlGestor.DataValueField = dvGestores.Table.Columns[0].ColumnName;
        this.ddlGestor.DataBind();
    }


    protected void CargarUsuariosCTG()
    {
        AEA.Parametros oAEAParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtUsuarios = oAEAParam.RetornaUsuariosAXIS();
        this.ddlUsuario.DataSource = dtUsuarios;
        this.ddlUsuario.DataTextField = dtUsuarios.Columns[0].ColumnName;
        this.ddlUsuario.DataValueField = dtUsuarios.Columns[0].ColumnName;
        this.ddlUsuario.DataBind();
    }

    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadGridInfo();
    }
    protected void gvSolMatriculas_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("ConsSolMatricula.aspx?id=" + this.gvSolMatriculas.SelectedRow.Cells[0].Text);
    }
}
