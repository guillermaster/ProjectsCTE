using System;
using System.Data;
using System.Configuration;
using System.Web.UI;

public partial class Seguridad_RegUsuario : Page
{
    protected string CurrentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.CurrentUser = User.Identity.Name.ToString();
        if (!IsPostBack)
        {
            //this.btnGuardar.Tipo = AEA.Parametros.Guardar.Actualizar;
            try
            {
                AEA.Parametros.TipoEmpresa tipoEmpresa = AEA.Parametros.TipoEmpresa.AEA;

                if (Request.QueryString[AEA.Parametros.QueryStringParams.TipoEmpresa.ToString()] != null && Request.QueryString[AEA.Parametros.QueryStringParams.TipoEmpresa.ToString()] != "")
                {
                    this.hdnTipoEmpresa.Value = Request.QueryString[AEA.Parametros.QueryStringParams.TipoEmpresa.ToString()];
                    tipoEmpresa = (AEA.Parametros.TipoEmpresa)int.Parse(this.hdnTipoEmpresa.Value);
                }

                CargarDatosPorTipoEmpresa(tipoEmpresa);


                if (Request.QueryString[AEA.Parametros.QueryStringParams.ID.ToString()] != null && Request.QueryString[AEA.Parametros.QueryStringParams.ID.ToString()] != "")
                {
                    this.txtUsername.Text = Request.QueryString[AEA.Parametros.QueryStringParams.ID.ToString()];
                    this.txtUsername.ReadOnly = true;

                    if (Request.QueryString[AEA.Parametros.QueryStringParams.action.ToString()] != null)
                    {
                        if (Request.QueryString[AEA.Parametros.QueryStringParams.action.ToString()] == AEA.Parametros.Acciones.CargaDatosEntidadPara.edit.ToString())
                        {
                            CargarUsuario(Request.QueryString[AEA.Parametros.QueryStringParams.ID.ToString()], tipoEmpresa);
                            this.btnGuardar.Tipo = AEA.Parametros.Acciones.Guardar.Actualizar;
                            this.btnGuardar.Visible = true;
                        }
                        else if (Request.QueryString[AEA.Parametros.QueryStringParams.action.ToString()] == AEA.Parametros.Acciones.CargaDatosEntidadPara.view.ToString())
                        {
                            CargarUsuario(Request.QueryString[AEA.Parametros.QueryStringParams.ID.ToString()], tipoEmpresa);
                            SetControlsToReadOnly();
                            this.btnGuardar.Visible = false;
                        }
                    }
                }
                else
                {
                    this.btnGuardar.Tipo = AEA.Parametros.Acciones.Guardar.Nuevo;
                    this.btnGuardar.Visible = true;
                }

                SetBackButton();
            }
            catch (Exception ex)
            {
                this.regForm.Visible = false;
                ShowFailureMessage(ex.Message);
            }
        }
    }

    protected void ValidateAccessCom()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.CurrentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoCreaUsuarioComercializadora))
        {
            Response.Redirect("../AccessDenied.aspx");
        }
    }

    protected void ValidateAccessAEA()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.CurrentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoCreaUsuarioAEA))
        {
            Response.Redirect("../AccessDenied.aspx");
        }
    }

    protected void SetBackButton()
    {
        if (Request.QueryString[AEA.Parametros.QueryStringParams.returl.ToString()] != null && Request.QueryString[AEA.Parametros.QueryStringParams.returl.ToString()] != "")
        {
            this.btnBack.TargetURL = Request.QueryString[AEA.Parametros.QueryStringParams.returl.ToString()]
                + "?" + AEA.Parametros.QueryStringParams.TipoEmpresa + "=" + this.hdnTipoEmpresa.Value;
            this.btnBack.Visible = true;
        }
        else
        {
            this.btnBack.TargetURL = Page.AppRelativeVirtualPath + "?" + 
                AEA.Parametros.QueryStringParams.TipoEmpresa.ToString() + "=" + 
                this.hdnTipoEmpresa.Value;
        }
    }

    protected void SetControlsToReadOnly()
    {
        this.txtEmail.ReadOnly = true;
        this.txtIdentificacion.ReadOnly = true;
        this.txtNombre.ReadOnly = true;
        this.txtUsername.ReadOnly = true;
        this.ddlAEA.Enabled = false;
        this.ddlComercializadora.Enabled = false;
        this.ddlSucursal.Enabled = false;
        this.ddlTipoUsuario.Enabled = false;
    }

    protected void SetDatosAuditoria(string usuarioCrea, DateTime fechaCrea)
    {
        lblUsuarioCreaVal.Text = usuarioCrea;
        try
        {
            lblFechaCreaVal.Text = fechaCrea.ToString();
        }
        catch (Exception)
        {
            lblFechaCreaVal.Text = "";
        }
        lblUsuarioCreaVal.Visible = true;
        lblUsuarioCrea.Visible = true;
        lblFechaCreaVal.Visible = true;
        lblFechaCrea.Visible = true;
    }

    protected void SaveButtonClick(object sender, EventArgs e)
    {
        AEA.Parametros.TipoEmpresa tipoEmpresa;
        int codEmpresa;
        if(ddlSucursal.Visible)
        {
            tipoEmpresa = AEA.Parametros.TipoEmpresa.Comercializadora;
            codEmpresa = int.Parse(ddlSucursal.SelectedValue);
        }
        else
        {
            tipoEmpresa = AEA.Parametros.TipoEmpresa.AEA;
            codEmpresa = int.Parse(ddlAEA.SelectedValue);
        }
        AEA.Usuario oUsuarioAEA = new AEA.Usuario(txtUsername.Text, txtNombre.Text, txtIdentificacion.Text, txtEmail.Text, tipoEmpresa, codEmpresa, ddlTipoUsuario.SelectedValue,
            CurrentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        switch (btnGuardar.Tipo)
        {
            case AEA.Parametros.Acciones.Guardar.Nuevo:
                if (oUsuarioAEA.CrearNuevo(Request.Url.ToString()))
                    ShowSuccessMessage(oUsuarioAEA.TrxMessage);
                else
                    ShowFailureMessage(oUsuarioAEA.TrxError);
                break;
            case AEA.Parametros.Acciones.Guardar.Actualizar:
                if (oUsuarioAEA.Actualizar(txtObservacion.Text))
                    ShowSuccessMessage(oUsuarioAEA.TrxMessage);
                else
                    ShowFailureMessage(oUsuarioAEA.TrxError);
                break;
        }
        regForm.Visible = false;
        btnGuardar.Visible = false;
        btnBack.Visible = true;
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

    protected void CargarComercializadoras()
    {
        AEA.Parametros oComercializadorasParam = new AEA.Parametros(this.CurrentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
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
        AEA.Sucursal oSucursal = new AEA.Sucursal(codComerc, this.CurrentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtSucursales = oSucursal.ObtenerSucursalesPorComercializadora();
        if (dtSucursales.Rows.Count > 0)
        {
            this.ddlSucursal.Enabled = true;
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
        AEA.Parametros oParam = new AEA.Parametros(this.CurrentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtAEAs = oParam.RetornaSucursalesAEA();

        this.ddlAEA.DataSource = dtAEAs;
        this.ddlAEA.DataValueField = dtAEAs.Columns[0].ColumnName;
        this.ddlAEA.DataTextField = dtAEAs.Columns[1].ColumnName;
        this.ddlAEA.DataBind();
    }

    protected void CargarTiposUsuarios(AEA.Parametros.TipoEmpresa tipoEmpresa)
    {
        AEA.Parametros oParam = new AEA.Parametros(this.CurrentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtTiposUsuarios = oParam.RetornaTiposUsuarios(tipoEmpresa);
        this.ddlTipoUsuario.DataSource = dtTiposUsuarios;
        this.ddlTipoUsuario.DataValueField = dtTiposUsuarios.Columns[0].ColumnName;
        this.ddlTipoUsuario.DataTextField = dtTiposUsuarios.Columns[1].ColumnName;
        this.ddlTipoUsuario.DataBind();
    }

    protected void CargarDatosPorTipoEmpresa(AEA.Parametros.TipoEmpresa tipoEmpresa)
    {
        switch(tipoEmpresa)
        {
            case AEA.Parametros.TipoEmpresa.AEA:
                ValidateAccessAEA();
                this.lblAEA.Visible = true;
                this.ddlAEA.Visible = true;
                this.lblComercializadora.Visible = false;
                this.ddlComercializadora.Visible = false;
                this.lblSucursal.Visible = false;
                this.ddlSucursal.Visible = false;
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
                CargarComercializadoras();
                break;
        }
        CargarTiposUsuarios(tipoEmpresa);
    }


    protected void CargarUsuario(string username, AEA.Parametros.TipoEmpresa tipoEmpresa)
    {
        AEA.Usuario oUsuarioAEA = new AEA.Usuario(this.txtUsername.Text, tipoEmpresa, this.CurrentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oUsuarioAEA.CargarDatos())
        {
            this.txtEmail.Text = oUsuarioAEA.Email;
            this.txtIdentificacion.Text = oUsuarioAEA.Identificacion;
            this.txtNombre.Text = oUsuarioAEA.Nombres;
            this.txtUsername.Text = username;
            this.ddlTipoUsuario.Items.FindByText(oUsuarioAEA.CodigoTipoUsuario).Selected = true;
            SetDatosAuditoria(oUsuarioAEA.UsuarioCrea, oUsuarioAEA.FechaCrea);
            if (tipoEmpresa == AEA.Parametros.TipoEmpresa.AEA)
            {
                this.ddlAEA.Items.FindByValue(oUsuarioAEA.CodigoEmpresa.ToString()).Selected = true;
                this.ddlComercializadora.Visible = false;
                this.lblComercializadora.Visible = false;
                this.lblSucursal.Visible = false;
                this.ddlSucursal.Visible = false;
            }
            else
            {                
                this.ddlComercializadora.Items.FindByValue(oUsuarioAEA.CodigoEmpresaPadre.ToString()).Selected = true;
                this.ddlComercializadora.Enabled = false;
                CargarSucursales(oUsuarioAEA.CodigoEmpresaPadre);
                this.ddlSucursal.Items.FindByValue(oUsuarioAEA.CodigoEmpresa.ToString()).Selected = true;
                this.lblAEA.Visible = false;
                this.ddlAEA.Visible = false;
            }
        }
        else
        {
            this.regForm.Visible = false;
            ShowFailureMessage(oUsuarioAEA.TrxMessage);
        }
    }

    protected void btnGuardar_Load(object sender, EventArgs e)
    {
        this.btnGuardar.ButtonClickDemo += new EventHandler(SaveButtonClick);
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
        catch(Exception ex)
        {
            this.ddlSucursal.Items.Clear();
        }
    }

    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }
}
