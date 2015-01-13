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

public partial class AsignaAutomotorComerc : System.Web.UI.Page
{
    private string currentUser;
    private static DataTable dtModelos;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.currentUser = User.Identity.Name.ToString();
        if (!IsPostBack)
        {
            ValidateAccess();
            LoadMatricesComercializadoras();
            this.btnSave.Tipo = AEA.Parametros.Acciones.Guardar.Nuevo;
        }
    }

    protected void ValidateAccess()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoTrxAutomotor))
        {
            Response.Redirect("AccessDenied.aspx");
        }
    }

    #region "Carga de Datos"
    protected void LoadMatricesComercializadoras()
    {
        AEA.Comercializadora oComerc = new AEA.Comercializadora(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtMatCom = oComerc.ObtenerComercializadoras(true);
        DataRow nullrow = dtMatCom.NewRow();
        dtMatCom.Rows.Add(null, " -- Seleccione -- ");
        dtMatCom.DefaultView.Sort = dtMatCom.Columns[1].ColumnName + " asc";
        this.ddlMatriz.DataSource = dtMatCom;
        this.ddlMatriz.DataValueField = dtMatCom.Columns[0].ColumnName;
        this.ddlMatriz.DataTextField = dtMatCom.Columns[1].ColumnName;
        this.ddlMatriz.DataBind();
    }

    protected void LoadSucursalesComercializadoras(int codComerc)
    {
        AEA.Sucursal oSucursal = new AEA.Sucursal(codComerc, this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtSucursales = oSucursal.ObtenerSucursalesPorComercializadora();
        if (dtSucursales.Rows.Count > 0)
        {
            this.ddlSucursales.Enabled = true;
            this.txtAnioProd.Enabled = true;
        }
        else
            AlertJS("No existen sucursales para la comercializadora matriz seleccionada");
        this.ddlSucursales.DataSource = dtSucursales;
        this.ddlSucursales.DataValueField = dtSucursales.Columns[0].ColumnName;
        this.ddlSucursales.DataTextField = dtSucursales.Columns[1].ColumnName;
        this.ddlSucursales.DataBind();

    }

    protected void LoadMarcas(int anio)
    {
        AEA.Parametros oAutomParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtMarcas = oAutomParam.RetornaMarcasAEA(int.Parse(this.txtAnioProd.Text));
        if (dtMarcas.Rows.Count > 0)
        {
            dtMarcas.Rows.Add(null, " -- Seleccione -- ");
            dtMarcas.DefaultView.Sort = dtMarcas.Columns[1].ColumnName + " asc";
            this.ddlMarca.Enabled = true;
        }
        else
        {
            this.ddlMarca.Enabled = false;
            AlertJS("No existen automotores registrados para el año "+this.txtAnioProd.Text);
        }
        this.ddlMarca.DataSource = dtMarcas;
        this.ddlMarca.DataValueField = dtMarcas.Columns[0].ColumnName;
        this.ddlMarca.DataTextField = dtMarcas.Columns[1].ColumnName;
        this.ddlMarca.DataBind();
    }

    protected void LoadModelos(int anio, int codMarca)
    {
        AEA.Parametros oAutomParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        dtModelos = oAutomParam.RetornaModelosRegistradosAeaNoEnComerc(int.Parse(this.txtAnioProd.Text), int.Parse(this.ddlMarca.SelectedValue), int.Parse(this.ddlSucursales.SelectedValue));
        this.gvModelos.DataSource = dtModelos;
        this.gvModelos.DataBind();
        this.gvModelos.Visible = true;
        if (this.gvModelos.Rows.Count > 0)
            this.btnSave.Visible = true;
        else
            this.btnSave.Visible = false;
    }

    #endregion

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

    protected int CodigoAutomotor(int anio, int codMarca, int codModelo, int codColor, int codComerc, out string error)
    {
        AEA.Automotor oAutomotor = new AEA.Automotor(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        int codAutomotor = oAutomotor.ObtenerCodigoAutomotor(anio, codMarca, codModelo, codColor, codComerc);
        error = oAutomotor.TrxError;
        return codAutomotor;
    }

    protected bool AsignaAutomotor(int codModelo, int codColor, string descModelo, out string error)
    {
        int codAutomotor = CodigoAutomotor(int.Parse(this.txtAnioProd.Text), int.Parse(this.ddlMarca.SelectedValue), codModelo, codColor, int.Parse(this.ddlMatriz.SelectedValue), out error);
        if (codAutomotor != -1)
        {
            AEA.Sucursal oSucursal = new AEA.Sucursal(int.Parse(this.ddlSucursales.SelectedValue), int.Parse(this.ddlMatriz.SelectedValue), this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            if (oSucursal.AsignaAutomotor(codAutomotor, descModelo))
            {
                error = oSucursal.TrxError;
                return true;
            }
            else
            {
                error = oSucursal.TrxError;
                return false;
            }
        }
        else
            return false;
        
    }

    protected bool AsignarAutomotoresEnComercializadora()
    {
        string error;
        DataTable dtErrores = new DataTable();
        dtErrores.Columns.Add("Automotor");
        dtErrores.Columns.Add("Descripción del Error");
        foreach(GridViewRow row in this.gvModelos.Rows)
        {
            CheckBox chkSeleccion = (CheckBox) row.Cells[0].FindControl("chkSeleccion");
            HiddenField hdnCodModelo = (HiddenField) row.Cells[0].FindControl("hdnCodModelo");
            HiddenField hdnCodColor = (HiddenField) row.Cells[0].FindControl("hdnCodColor");
            if (chkSeleccion.Checked)
            {
                error = "";
                if (!AsignaAutomotor(int.Parse(hdnCodModelo.Value), int.Parse(hdnCodColor.Value), row.Cells[1].Text, out error))
                {
                    DataRow dr = dtErrores.NewRow();
                    dr[0] = row.Cells[1].Text + row.Cells[2].Text;
                    dr[1] = error;
                    dtErrores.Rows.Add(dr);
                }
            }
        }

        //LoadModelos(int.Parse(this.txtAnioProd.Text), int.Parse(this.ddlMarca.SelectedValue));

        this.gvErrores.DataSource = dtErrores;
        this.gvErrores.DataBind();

        if (dtErrores.Rows.Count==0)
        {
            this.gvModelos.Visible = false;
            return true;
        }
        else
        {
            this.btnVerErrores.Visible = true;            
            return false;
        }
    }

    protected void HideTrxControls()
    {
        this.gvModelos.Visible = false;
        this.gvErrores.Visible = false;
        this.btnSave.Visible = false;
        this.btnHideErrores.Visible = false;
        this.btnVerErrores.Visible = false;
        this.divError.Visible = false;
        this.divWarning.Visible = false;
    }

    

    #region "Métodos Eventos"
    protected void ddlMatriz_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlMatriz.SelectedValue != null && this.ddlMatriz.SelectedValue != "")
            LoadSucursalesComercializadoras(int.Parse(this.ddlMatriz.SelectedValue));
        else
        {
            this.ddlSucursales.Items.Clear();
        }
        HideTrxControls();
    }    
    protected void txtAnioProd_TextChanged(object sender, EventArgs e)
    {
        LoadMarcas(int.Parse(this.txtAnioProd.Text));
        HideTrxControls();
    }
    protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.ddlMarca.SelectedValue.Length > 0)
            LoadModelos(int.Parse(this.txtAnioProd.Text), int.Parse(this.ddlMarca.SelectedValue));
        else
            HideTrxControls();
    }
    protected void ddlSucursales_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(this.txtAnioProd.Text.Length>0 && this.ddlMarca.Items.Count>0)
            LoadModelos(int.Parse(this.txtAnioProd.Text), int.Parse(this.ddlMarca.SelectedValue));
    }
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        bool atLeastOneRowChecked = false;
        foreach (GridViewRow row in this.gvModelos.Rows)
        {
            CheckBox chkSeleccion = (CheckBox)row.Cells[0].FindControl("chkSeleccion");
            if (chkSeleccion.Checked)
            {
                atLeastOneRowChecked = true;
                break;
            }
        }

        if (atLeastOneRowChecked)
        {
            int nRowsBefore = this.gvModelos.Rows.Count;
            this.divBusqueda.Visible = false;
            if (AsignarAutomotoresEnComercializadora())
            {
                ShowSuccessMessage("Los automotores fueron asignados con exito a " + this.ddlSucursales.SelectedItem.Text);
                this.btnSave.Visible = false;
            }
            else
            {
                int nRowsAfter = this.gvModelos.Rows.Count;
                if (nRowsAfter == nRowsBefore)
                    ShowFailureMessage("No se pudieron asignar los automotores a " + this.ddlSucursales.SelectedItem.Text);
                else
                    ShowFailureMessage("Algunos automotores no pudieron ser asignados a " + this.ddlSucursales.SelectedItem.Text);
            }
        }
        else
            AlertJS("No ha seleccionado ningún modelo");
    }
    protected void btnVerErrores_Click(object sender, EventArgs e)
    {
        this.gvErrores.Visible = true;
        this.btnHideErrores.Visible = true;
        this.btnVerErrores.Visible = false;
    }
    protected void btnHideErrores_Click(object sender, EventArgs e)
    {
        this.gvErrores.Visible = false;
        this.btnHideErrores.Visible = false;
        this.btnVerErrores.Visible = true;
    }
    #endregion

    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }

    protected void btnSave_Load(object sender, EventArgs e)
    {
        this.btnSave.ButtonClickDemo += new EventHandler(btnRegistrar_Click);
    }
}
