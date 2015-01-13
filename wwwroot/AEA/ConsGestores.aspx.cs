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
            ValidateAccess();
            this.btnPrint.SetupPrintingElement("divContent", 600, 400);
            SetNewButton();
            LoadGridInfo();
        }
    }

    protected void ValidateAccess()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoConsGestor))
        {
            Response.Redirect("AccessDenied.aspx");
        }
    }

    protected void SetNewButton()
    {
        this.btnNew.TargetURL = "RegGestores.aspx?" + AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath;
    }


    protected void LoadGridInfo()
    {
        AEA.Gestor oGestor = new AEA.Gestor(User.Identity.Name.ToString(), ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        _dvGridViewData = oGestor.ObtenerGestores().DefaultView;
        _dvGridViewData.Sort = _dvGridViewData.Table.Columns[2].ColumnName;
        this.gvGestores.DataSource = _dvGridViewData;
        this.gvGestores.DataBind();
    }


    protected void gvGestores_Delete(object sender, EventArgs e)
    {
        ImageButton dlBtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)dlBtn.Parent.Parent;
        //this.hdnIdGestor.Value = grdRow.Cells[0].Text;
        HiddenField hdnCodGestor = (HiddenField)grdRow.FindControl("hdnCodGestor");
        this.hdnIdGestor.Value = hdnCodGestor.Value;
        this.txtDesactivaCedula.Text = grdRow.Cells[1].Text;
        this.txtDesactivaNombre.Text = Server.HtmlDecode(grdRow.Cells[2].Text).ToString();
        this.divDesactiva.Visible = true;
        this.gvGestores.Visible = false;   
    }

    protected void DesactivaGestor()
    {
        AEA.Gestor oGestor = new AEA.Gestor(int.Parse(this.hdnIdGestor.Value), User.Identity.Name.ToString(), ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oGestor.DesactivaGestor(this.txtDesactivaObs.Text))
        {
            LoadGridInfo();
            this.gvGestores.Visible = true;
            this.divDesactiva.Visible = false;
            AlertJS(oGestor.TrxMessage);
        }
        else
        {
            AlertJS(oGestor.TrxError);
        }
    }

    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }

    
    protected void btnDesactivar_Click(object sender, EventArgs e)
    {
        DesactivaGestor();
    }
    protected void btnDesacCancelar_Click1(object sender, EventArgs e)
    {
        this.divDesactiva.Visible = false;
        this.gvGestores.Visible = true;
    }
    protected void gvGestores_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string idGestor = this.gvGestores.SelectedRow.Cells[0].Text;
        HiddenField hdnCodGestor = (HiddenField)this.gvGestores.SelectedRow.FindControl("hdnCodGestor");
        Response.Redirect("RegGestores.aspx?id=" + hdnCodGestor.Value + "&action=view&" + AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath);
    }
}
