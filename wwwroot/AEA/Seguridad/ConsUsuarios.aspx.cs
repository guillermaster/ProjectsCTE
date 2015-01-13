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

public partial class Seguridad_ConsUsuario : System.Web.UI.Page
{
    private string currentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.currentUser = User.Identity.Name.ToString();    
         
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString[AEA.Parametros.QueryStringParams.TipoEmpresa.ToString()] != null && Request.QueryString[AEA.Parametros.QueryStringParams.TipoEmpresa.ToString()] != "")
                {
                    CargarDatosPorTipoEmpresa((AEA.Parametros.TipoEmpresa)int.Parse(Request.QueryString[AEA.Parametros.QueryStringParams.TipoEmpresa.ToString()]));
                }
                else
                {
                    CargarDatosPorTipoEmpresa(AEA.Parametros.TipoEmpresa.AEA);
                }
            }
            catch (Exception ex)
            {
                ShowFailureMessage(ex.Message);
            }
        }
    }

    protected void ValidateAccessCom()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoCreaUsuarioComercializadora))
        {
            Response.Redirect("../AccessDenied.aspx");
        }
    }

    protected void ValidateAccessAEA()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoCreaUsuarioAEA))
        {
            Response.Redirect("../AccessDenied.aspx");
        }
    }


    protected void CargarComercializadoras()
    {
        AEA.Parametros oComercializadorasParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtComercializadora = oComercializadorasParam.RetornaComercializadoras("M");
        DataRow nullrow = dtComercializadora.NewRow();
        nullrow[0] = null;
        nullrow[1] = " -- Seleccione --";
        dtComercializadora.Rows.InsertAt(nullrow, 0);
        this.ddlComercializadora.DataSource = dtComercializadora;
        this.ddlComercializadora.DataValueField = dtComercializadora.Columns[0].ColumnName;
        this.ddlComercializadora.DataTextField = dtComercializadora.Columns[1].ColumnName;
        this.ddlComercializadora.DataBind();
    }

    protected void CargarSucursales(int codComerc)
    {
        AEA.Sucursal oSucursal = new AEA.Sucursal(codComerc, this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtSucursales = oSucursal.ObtenerSucursalesPorComercializadora();
        if (dtSucursales.Rows.Count > 0)
        {
            this.ddlSucursal.Enabled = true;
            DataRow nullrow = dtSucursales.NewRow();
            nullrow[0] = null;
            nullrow[1] = " -- Seleccione --";
            dtSucursales.Rows.InsertAt(nullrow, 0);
        }
        else
            AlertJS("No existen sucursales para la comercializadora seleccionada");
        this.ddlSucursal.DataSource = dtSucursales;
        this.ddlSucursal.DataValueField = dtSucursales.Columns[0].ColumnName;
        this.ddlSucursal.DataTextField = dtSucursales.Columns[1].ColumnName;
        this.ddlSucursal.DataBind();
    }

    protected void CargarSucursalesAEA()
    {
        AEA.Parametros oParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtAEAs = oParam.RetornaSucursalesAEA();
        DataRow nullrow = dtAEAs.NewRow();
        nullrow[0] = null;
        nullrow[1] = " -- Seleccione --";
        dtAEAs.Rows.InsertAt(nullrow, 0);
        this.ddlAEA.DataSource = dtAEAs;
        this.ddlAEA.DataValueField = dtAEAs.Columns[0].ColumnName;
        this.ddlAEA.DataTextField = dtAEAs.Columns[1].ColumnName;
        this.ddlAEA.DataBind();
    }

    protected void LoadGridInfo(AEA.Parametros.TipoEmpresa tipoEmpresa)
    {
        AEA.Usuario oUsuario = new AEA.Usuario(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        int codEmpresa;
        if (tipoEmpresa==AEA.Parametros.TipoEmpresa.AEA)
            codEmpresa = int.Parse(this.ddlAEA.SelectedValue);
        else
            codEmpresa = int.Parse(this.ddlSucursal.SelectedValue);
        DataTable dtUsuarios = oUsuario.ObtenerUsuarios(tipoEmpresa, codEmpresa);
        this.gvUsuarios.DataSource = dtUsuarios;
        this.gvUsuarios.DataBind();
    }


    protected void CargarDatosPorTipoEmpresa(AEA.Parametros.TipoEmpresa tipoEmpresa)
    {
        this.hdnTipoEmpresa.Value = ((int)tipoEmpresa).ToString();
        switch (tipoEmpresa)
        {
            case AEA.Parametros.TipoEmpresa.AEA:
                ValidateAccessAEA();
                this.lblAEA.Visible = true;
                this.ddlAEA.Visible = true;
                this.lblComercializadora.Visible = false;
                this.ddlComercializadora.Visible = false;
                this.lblSucursal.Visible = false;
                this.ddlSucursal.Visible = false;
                this.reqValComercializadora.Visible = false;
                this.reqValSucursal.Visible = false;
                CargarSucursalesAEA();
                break;
            case AEA.Parametros.TipoEmpresa.Comercializadora:
                ValidateAccessCom();
                this.lblAEA.Visible = false;
                this.ddlAEA.Visible = false;
                this.lblComercializadora.Visible = true;
                this.ddlComercializadora.Visible = true;
                this.lblSucursal.Visible = true;
                this.ddlSucursal.Visible = true;
                this.reqValAEA.Visible = false;
                CargarComercializadoras();
                break;
        }
        //CargarTiposUsuarios(tipoEmpresa);
    }

    protected void DesactivaUsuario()
    {
        AEA.Parametros.TipoEmpresa tipoEmpresa = (AEA.Parametros.TipoEmpresa)int.Parse(this.hdnTipoEmpresa.Value);
        int codEmpresa;
        if (tipoEmpresa == AEA.Parametros.TipoEmpresa.AEA)
            codEmpresa = int.Parse(this.ddlAEA.SelectedValue);
        else
            codEmpresa = int.Parse(this.ddlSucursal.SelectedValue);

        AEA.Usuario oUsuarioAEA = new AEA.Usuario(this.txtDesactivaUsuario.Text, this.hdnDesacNombre.Value, this.hdnDesacIdentificacion.Value, this.hdnDesacEmail.Value,
            tipoEmpresa, codEmpresa, this.hdnDesacTipoUsuario.Value,
            this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        if (oUsuarioAEA.Desactivar(this.txtDesactivaObs.Text))
            ShowSuccessMessage(oUsuarioAEA.TrxMessage);
        else
            ShowFailureMessage(oUsuarioAEA.TrxError);

        this.btnBack.TargetURL = Page.AppRelativeVirtualPath;
        this.btnBack.Visible = true;
        this.divDesactiva.Visible = false;
    }


    protected void gvUsuarios_Delete(object sender, EventArgs e)
    {
        ImageButton dlBtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)dlBtn.Parent.Parent;
        this.txtDesactivaUsuario.Text = grdRow.Cells[0].Text;

        AEA.Parametros.TipoEmpresa tipoEmpresa = (AEA.Parametros.TipoEmpresa)int.Parse(this.hdnTipoEmpresa.Value);
        if (tipoEmpresa == AEA.Parametros.TipoEmpresa.AEA)
        {
            this.lblDesactivaEmpresa.Text = "AEA";
            this.txtDesactivaEmpresa.Text = this.ddlAEA.SelectedItem.Text;
        }
        else
        {
            this.lblDesactivaEmpresa.Text = "Comercializadora";
            this.txtDesactivaEmpresa.Text = this.ddlSucursal.SelectedItem.Text;
        }
        
        this.gvUsuarios.Visible = false;
        this.divDesactiva.Visible = true;
        this.divConsulta.Visible = false;
        this.btnNew.Visible = false;
    }

    protected void gvUsuarios_Edit(object sender, EventArgs e)
    {
        ImageButton dlBtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)dlBtn.Parent.Parent;
        string idUsuario = grdRow.Cells[0].Text;
        Response.Redirect("RegUsuario.aspx?" + AEA.Parametros.QueryStringParams.ID + "=" + idUsuario
            + "&" + AEA.Parametros.QueryStringParams.action + "=" + AEA.Parametros.Acciones.CargaDatosEntidadPara.edit.ToString()
            + "&" + AEA.Parametros.QueryStringParams.TipoEmpresa + "=" + this.hdnTipoEmpresa.Value
            + "&" + AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath);
    }

    protected void gvUsuarios_SendPwd(object sender, EventArgs e)
    {
        ImageButton dlBtn = (ImageButton)sender;
        GridViewRow grdRow = (GridViewRow)dlBtn.Parent.Parent;

        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(grdRow.Cells[0].Text, grdRow.Cells[3].Text, objCrypto, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oUsuario.ResetPasswordAndSendByEmail())
            AlertJS("La contraseña ha sido enviada correctamente.");
        else
            AlertJS("Error al enviar contraseña");

    }

    protected void btnDesacitvar_Click(object sender, EventArgs e)
    {
        DesactivaUsuario();
    }

    protected void btnDesacCancelar_Click(object sender, EventArgs e)
    {
        this.divDesactiva.Visible = false;
        this.gvUsuarios.Visible = true;
        this.divConsulta.Visible = true;
        this.btnNew.Visible = true;
    }

    protected void ddlComercializadora_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (this.ddlComercializadora.SelectedValue != null)
                CargarSucursales(int.Parse(this.ddlComercializadora.SelectedValue));
            else
                this.ddlSucursal.Items.Clear();
        }
        catch (Exception ex)
        {
            this.ddlSucursal.Items.Clear();
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        LoadGridInfo((AEA.Parametros.TipoEmpresa)int.Parse(this.hdnTipoEmpresa.Value));
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

    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }
    protected void gvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
    {
        string idUsuario = this.gvUsuarios.SelectedRow.Cells[0].Text;
        Response.Redirect("RegUsuario.aspx?"+AEA.Parametros.QueryStringParams.ID + "=" + idUsuario
            + "&" + AEA.Parametros.QueryStringParams.action + "=" + AEA.Parametros.Acciones.CargaDatosEntidadPara.view.ToString()
            + "&" + AEA.Parametros.QueryStringParams.TipoEmpresa + "=" + this.hdnTipoEmpresa.Value
            + "&" + AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath);
    }
}
