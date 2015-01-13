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

public partial class ConsModeloAutomotorLista : System.Web.UI.Page
{
    private string currentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.currentUser = User.Identity.Name.ToString();
        if (!IsPostBack)
        {
            ValidateAccess();
            this.btnPrint.SetupPrintingElement("divContent", 600, 400);
            SetNewButton();
            this.btnAprobar.Attributes.Add("onclick", "return confirm('¿Está seguro que desea aprobar los automotores seleccionados?');");
            this.btnReprobar.Attributes.Add("onclick", "return confirm('¿Está seguro que desea reprobar los automotores seleccionados?');");
        }
    }

    protected void ValidateAccess()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoConsAutomotor))
        {
            Response.Redirect("AccessDenied.aspx");
        }
        else if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoApruebaAutomotor))
        {
            gvModelosAutomotores.Columns[gvModelosAutomotores.Columns.Count - 2].Visible = false;
            gvModelosAutomotores.Columns[gvModelosAutomotores.Columns.Count - 1].Visible = false;
            btnAprobar.Visible = false;
            btnReprobar.Visible = false;
        }
    }

    protected void SetNewButton()
    {
        string returnURL = AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath;
        this.btnNew.TargetURL = "ModeloAutomotor.aspx?" + returnURL;
    }

    protected void LoadEstados()
    {
        AEA.Parametros oAutomParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtEstados = oAutomParam.EstadosModelosAutomotores();
        dtEstados.Rows.Add(null, " -- Seleccione -- ");
        dtEstados.DefaultView.Sort = dtEstados.Columns[1].ColumnName + " asc";
        this.ddlEstado.Enabled = true;
        this.ddlEstado.DataSource = dtEstados;
        this.ddlEstado.DataValueField = dtEstados.Columns[0].ColumnName;
        this.ddlEstado.DataTextField = dtEstados.Columns[1].ColumnName;
        this.ddlEstado.DataBind();
    }

    protected void LoadMarcas()
    {
        AEA.Parametros oAutomParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            DataTable dtMarcas = oAutomParam.RetornaMarcasAEA(int.Parse(this.txtAnio.Text));
            dtMarcas.Rows.Add(null, " -- Seleccione -- ");
            dtMarcas.DefaultView.Sort = dtMarcas.Columns[1].ColumnName + " asc";
            this.ddlMarca.Enabled = true;
            this.ddlMarca.DataSource = dtMarcas;
            this.ddlMarca.DataValueField = dtMarcas.Columns[0].ColumnName;
            this.ddlMarca.DataTextField = dtMarcas.Columns[1].ColumnName;
            this.ddlMarca.DataBind();
        }
        catch (Exception ex)
        {
        }
        
    }

    protected void LoadModelos()
    {
        AEA.Parametros oAutomParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            DataTable dtModelos = oAutomParam.RetornaModelosAEA(int.Parse(this.txtAnio.Text), int.Parse(this.ddlMarca.SelectedValue));
            dtModelos.Rows.Add(null, " -- Seleccione -- ");
            dtModelos.DefaultView.Sort = dtModelos.Columns[1].ColumnName + " asc";
            this.ddlModelo.Enabled = true;
            this.ddlModelo.DataSource = dtModelos;
            this.ddlModelo.DataValueField = dtModelos.Columns[0].ColumnName;
            this.ddlModelo.DataTextField = dtModelos.Columns[1].ColumnName;
            this.ddlModelo.DataBind();
        }
        catch (Exception ex)
        {
        }        
    }


    protected void LoadColores()
    {
        AEA.Parametros oAutomParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtColores = oAutomParam.RetornaColoresAEA();
        this.ddlColor.DataSource = dtColores;
        this.ddlColor.DataValueField = dtColores.Columns[0].ColumnName;
        this.ddlColor.DataTextField = dtColores.Columns[1].ColumnName;
        this.ddlColor.DataBind();
    }

    protected void HideAdditionalCriteriaControls()
    {
        this.ddlColor.Visible = false;
        this.reqValColor.Visible = false;
        this.ddlEstado.Visible = false;
        this.reqValEstado.Visible = false;
        this.ddlMarca.Visible = false;
        this.reqValMarca.Visible = false;
        this.ddlModelo.Visible = false;
        this.reqValModelo.Visible = false;
        this.txtAnio.Visible = false;
        this.regExpValAnio.Visible = false;
        this.lblAnio.Visible = false;
        this.lblColor.Visible = false;
        this.lblEstado.Visible = false;
        this.lblMarca.Visible = false;
        this.lblModelo.Visible = false;
    }

    protected void ddlCriterio_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideAdditionalCriteriaControls();
        switch (this.ddlCriterio.SelectedValue)
        {
            case "ESTADO":
                this.lblEstado.Visible = true;
                this.ddlEstado.Visible = true;
                this.reqValEstado.Visible = true;
                LoadEstados();
                break;
            case "COLOR":
                this.lblColor.Visible = true;
                this.ddlColor.Visible = true;
                this.reqValColor.Visible = true;
                LoadColores();
                break;
            case "MARCA":
                this.lblAnio.Visible = true;
                this.txtAnio.Visible = true;
                this.lblMarca.Visible = true;
                this.ddlMarca.Visible = true;
                this.reqValMarca.Visible = true;
                this.ddlMarca.AutoPostBack = false;
                break;
            case "MODELO":
                this.lblAnio.Visible = true;
                this.txtAnio.Visible = true;
                this.lblMarca.Visible = true;
                this.ddlMarca.Visible = true;
                this.ddlMarca.AutoPostBack = true;
                this.lblModelo.Visible = true;
                this.ddlModelo.Visible = true;
                this.reqValModelo.Visible = true;
                break;
        }
    }

    public string ValorBusqueda()
    {
        string value;
        switch (this.ddlCriterio.SelectedValue)
        {
            case "ESTADO":
                value = this.ddlEstado.SelectedValue;
                break;
            case "COLOR":
                value = this.ddlColor.SelectedValue;
                break;
            case "MARCA":
                value = this.ddlMarca.SelectedValue;
                break;
            case "MODELO":
                value = this.ddlModelo.SelectedValue;
                break;
            default:
                value = null;
                break;
        }
        return value;
    }

    protected void txtAnio_TextChanged(object sender, EventArgs e)
    {
        LoadMarcas();
    }
    protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadModelos();
    }
    protected void btnConsultar_Click1(object sender, EventArgs e)
    {
        AEA.Automotor oAutomotor = new AEA.Automotor(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        gvModelosAutomotores.DataSource = oAutomotor.ObtenerAutomotores(this.ddlCriterio.SelectedValue, ValorBusqueda());
        gvModelosAutomotores.DataBind();
        gvModelosAutomotores.Visible = true;
        gvErrores.Visible = false;
        divWarning.Visible = false;
        divError.Visible = false;
    }
    protected void gvModelosAutomotores_SelectedIndexChanged(object sender, EventArgs e)
    {
        string idAutomotor = this.gvModelosAutomotores.SelectedRow.Cells[0].Text;
        string returnURL = AEA.Parametros.QueryStringParams.returl + "=" + Page.AppRelativeVirtualPath;
        Response.Redirect("ConsModeloAutomotor.aspx?id=" + idAutomotor+"&"+returnURL);
    }

    protected void btnVerFoto_Click(object sender, ImageClickEventArgs e)
    {
        //imgFotoModeloAutomotor.ImageUrl = "FotoModeloAutomotor.aspx?";
    }

    protected string GetFotoUrl(string codAutomotor)
    {
        return "FotoModeloAutomotor.aspx?codAutomotor=" + codAutomotor;
    }

    protected bool CheckboxVisibility(string estadoAutomotor)
    {
        return (estadoAutomotor=="PROPUESTO") ? true : false;
    }

    protected void btnAprobar_Click(object sender, ImageClickEventArgs e)
    {
        gvModelosAutomotores.Visible = false;
        int numAprobados = 0;
        DataTable dtErrores = new DataTable("Errores");
        dtErrores.Columns.Add("Código de automotor");
        dtErrores.Columns.Add("Descripción del error");
        foreach(GridViewRow gvRow in gvModelosAutomotores.Rows)
        {
            //obtener el segundo control de la última columna del grid (debe ser el checkbox)
            CheckBox chkAp = gvRow.Cells[gvRow.Cells.Count - 2].Controls[1] as CheckBox;
            if (chkAp.Checked)
            {
                int codAutomotor = Convert.ToInt32(gvRow.Cells[0].Text);
                AEA.Automotor oAutomotor = new AEA.Automotor(codAutomotor, User.Identity.Name.ToString(), ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
                if (oAutomotor.AprobarAutomotor())
                    numAprobados++;
                else
                {
                    DataRow drError = dtErrores.NewRow();
                    drError[0] = codAutomotor;
                    drError[1] = oAutomotor.TrxError;
                    dtErrores.Rows.Add(drError);
                }
            }
        }

        if (dtErrores.Rows.Count == 0)
            ShowSuccessMessage("Se aprobaron exitósamente " + numAprobados.ToString() + "  automotores.");
        else
        {
            gvErrores.DataSource = dtErrores;
            gvErrores.DataBind();
            if (numAprobados > 0)
                ShowFailureMessage("Sólo " + numAprobados.ToString() + " automotores pudieron ser aprobados, ocurrieron errores.");
            else
                ShowFailureMessage("Se han presentado errores al aprobar, no se pudo aprobar ningún automotor");
        }
    }
    protected void btnReprobar_Click(object sender, ImageClickEventArgs e)
    {
        gvModelosAutomotores.Visible = false;
        int numReprobados = 0;
        DataTable dtErrores = new DataTable("Errores");
        dtErrores.Columns.Add("Código de automotor");
        dtErrores.Columns.Add("Descripción del error");
        foreach (GridViewRow gvRow in gvModelosAutomotores.Rows)
        {
            //obtener el segundo control de la última columna del grid (debe ser el checkbox)
            CheckBox chkAp = gvRow.Cells[gvRow.Cells.Count - 2].Controls[1] as CheckBox;
            
            if (chkAp.Checked)
            {
                TextBox txtObs = gvRow.Cells[gvRow.Cells.Count - 1].Controls[1] as TextBox;
                int codAutomotor = Convert.ToInt32(gvRow.Cells[0].Text);
                AEA.Automotor oAutomotor = new AEA.Automotor(codAutomotor, User.Identity.Name.ToString(), ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
                if (oAutomotor.ReprobarAutomotor(AEA.Parametros.EstadoAutomotor.Rechazar, txtObs.Text))
                    numReprobados++;
                else
                {
                    DataRow drError = dtErrores.NewRow();
                    drError[0] = codAutomotor;
                    drError[1] = oAutomotor.TrxError;
                    dtErrores.Rows.Add(drError);
                }
            }
        }

        if (dtErrores.Rows.Count == 0)
            ShowSuccessMessage("Se reprobaron exitósamente " + numReprobados.ToString() + "  automotores.");
        else
        {
            gvErrores.DataSource = dtErrores;
            gvErrores.DataBind();
            if (numReprobados > 0)
                ShowFailureMessage("Sólo " + numReprobados.ToString() + " automotores pudieron ser reprobados, ocurrieron errores.");
            else
                ShowFailureMessage("Se han presentado errores al reprobar, no se pudo aprobar ningún automotor");
        }
    }
    protected void ShowSuccessMessage(string message)
    {
        divError.Visible = false;
        divWarning.Visible = true;
        lblMsgWarning.Text = message;
    }


    protected void ShowFailureMessage(string message)
    {
        divWarning.Visible = false;
        divError.Visible = true;
        lblMsgError.Text = message;
    }

    

    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }

    protected void btnVerErrores_Click(object sender, EventArgs e)
    {
        gvErrores.Visible = true;
        btnVerErrores.Visible = false;
        btnHideErrores.Visible = true;
    }
    protected void btnHideErrores_Click(object sender, EventArgs e)
    {
        gvErrores.Visible = false;
        btnHideErrores.Visible = false;
        btnVerErrores.Visible = true;
    }
}
