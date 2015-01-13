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

public partial class RegistroSucursal : System.Web.UI.Page
{
    private string currentUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.currentUser = User.Identity.Name.ToString();
        if (!IsPostBack)
        {
            ValidateAccess();
            SetBackButton();
            CargarProvincias();
            CargarComercializadoras();

            if (Request.QueryString["id"] != null)
            {
                this.hdnCodSucursal.Value = Request.QueryString["id"];
                if (Request.QueryString["action"] != null)
                {
                    switch (Request.QueryString["action"])
                    {
                        case "edit":
                            LoadSucursal(int.Parse(Request.QueryString["id"]));
                            this.ddl_comercializadora.Enabled = false;
                            this.btnSave.Tipo = AEA.Parametros.Acciones.Guardar.Actualizar;
                            this.btnSave.Visible = true;
                            break;
                        case "view":
                            LoadSucursal(int.Parse(Request.QueryString["id"]));
                            SetControlsToReadOnly();
                            this.ddl_comercializadora.Enabled = false;
                            this.btnSave.Visible = false;
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                this.btnSave.Tipo = AEA.Parametros.Acciones.Guardar.Nuevo;
                this.btnSave.Visible = true;
            }
        }
    }

    protected void ValidateAccess()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoInsActComercializadora))
        {
            Response.Redirect("AccessDenied.aspx");
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

    protected void SetControlsToReadOnly()
    {        
        this.txtEmail.ReadOnly = true;
        this.txtEmailContacto.ReadOnly = true;
        this.txtEmailContacto2.ReadOnly = true;
        this.txtNombreComercial.ReadOnly = true;
        this.txtNombreContacto.ReadOnly = true;
        this.txtNombreContacto2.ReadOnly = true;
        this.txtRazonSocial.ReadOnly = true;
        this.txtRepLegal.ReadOnly = true;
        this.txtRUC.ReadOnly = true;
        this.txtTelefonoConv.ReadOnly = true;
        this.txtTelefonoMovil.ReadOnly = true;
        this.ddlCanton.Enabled = false;
        this.ddlProvincia.Enabled = false;
    }


    protected void LoadSucursal(int idSucursal)
    {
        AEA.Sucursal oSucursal = new AEA.Sucursal(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], idSucursal);

        if (oSucursal.CargarSucursal())
        {
            this.ddl_comercializadora.Items.FindByValue(oSucursal.ComercializadoraMatriz.CodComercializadora.ToString()).Selected = true;
            this.txtEmail.Text = oSucursal.Email;
            this.txtEmailContacto.Text = oSucursal.EmailContacto1;
            this.txtEmailContacto2.Text = oSucursal.EmailContacto2;
            this.txtNombreComercial.Text = oSucursal.NombreComercial;
            this.txtNombreContacto.Text = oSucursal.NombreContacto1;
            this.txtNombreContacto2.Text = oSucursal.NombreContacto2;
            this.txtRazonSocial.Text = oSucursal.RazonSocial;
            this.txtRepLegal.Text = oSucursal.RepresentanteLegal;
            this.txtRUC.Text = oSucursal.RUC;
            this.txtTelefonoConv.Text = oSucursal.TelefonoConvencional;
            this.txtTelefonoMovil.Text = oSucursal.TelefonoMovil;
            this.ddlProvincia.Items.FindByValue(oSucursal.CodProvincia).Selected = true;
            CargarCantones(oSucursal.CodProvincia);
            ListItem itemCanton = this.ddlCanton.Items.FindByValue(oSucursal.CodCanton);
            if (itemCanton != null)
                itemCanton.Selected = true;
        }
        else
        {
            ShowFailureMessage("No se pudieron cargar los datos de la comercializadora");
        }
    }

    protected void RegistrarSucursal()
    {
        AEA.Sucursal oSucursal = new AEA.Sucursal(int.Parse(this.ddl_comercializadora.SelectedValue),
            this.txtRazonSocial.Text, this.txtRUC.Text, this.txtRepLegal.Text, this.ddlProvincia.SelectedValue,
            this.ddlCanton.SelectedValue, this.txtNombreComercial.Text, this.txtTelefonoConv.Text, this.txtTelefonoMovil.Text,
            this.txtNombreContacto.Text, this.txtEmail.Text, this.txtEmailContacto.Text, this.currentUser,
            ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        this.divForms.Visible = false;
        this.btnSave.Visible = false;
        if (oSucursal.RegistrarNuevaSucursal())
            ShowSuccessMessage(oSucursal.TrxMessage);
        else
            ShowFailureMessage(oSucursal.TrxError);
    }

    protected void ModificarSucursal()
    {
        AEA.Sucursal oSucursal = new AEA.Sucursal(int.Parse(this.hdnCodSucursal.Value), int.Parse(this.ddl_comercializadora.SelectedValue), this.txtRazonSocial.Text, this.txtRUC.Text, this.txtRepLegal.Text, this.ddlProvincia.SelectedValue, this.ddlCanton.SelectedValue, this.txtNombreComercial.Text,
            this.txtTelefonoConv.Text, this.txtTelefonoMovil.Text, this.txtNombreContacto.Text, this.txtEmail.Text, this.txtEmailContacto.Text,
            this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        this.divForms.Visible = false;
        this.btnSave.Visible = false;

        if (oSucursal.Actualizar())
            ShowSuccessMessage(oSucursal.TrxMessage);
        else
            ShowFailureMessage(oSucursal.TrxError);
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


    protected void CargarProvincias()
    {
        AEA.Parametros oParametros = new AEA.Parametros(User.Identity.Name.ToString(), ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataView dvProvincias = oParametros.ProvinciasEcuador();
        DataRow nullrow = dvProvincias.Table.NewRow();
        nullrow[0] = null;
        nullrow[1] = null;

        dvProvincias.Table.Rows.InsertAt(nullrow, 0);

        this.ddlProvincia.DataSource = dvProvincias;
        this.ddlProvincia.DataValueField = dvProvincias.Table.Columns[0].ColumnName;
        this.ddlProvincia.DataTextField = dvProvincias.Table.Columns[1].ColumnName;
        this.ddlProvincia.DataBind();
    }

    protected void CargarCantones(string cnProvincia)
    {
        AEA.Parametros oCantonParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtCantones = oCantonParam.RetornaCantones(cnProvincia);

        this.ddlCanton.DataSource = dtCantones;
        this.ddlCanton.DataValueField = dtCantones.Columns[0].ColumnName;
        this.ddlCanton.DataTextField = dtCantones.Columns[1].ColumnName;
        this.ddlCanton.DataBind();
    }


    protected void CargarComercializadoras()
    {
        AEA.Parametros oComercializadorasParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtComercializadora = oComercializadorasParam.RetornaComercializadoras("M");

        this.ddl_comercializadora.DataSource = dtComercializadora;
        this.ddl_comercializadora.DataValueField = dtComercializadora.Columns[0].ColumnName;
        this.ddl_comercializadora.DataTextField = dtComercializadora.Columns[1].ColumnName;
        this.ddl_comercializadora.DataBind();
    }


    protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCantones(this.ddlProvincia.SelectedValue);
    }

    
    

    protected void SaveButtonClick(object sender, EventArgs e)
    {
        switch (this.btnSave.Tipo)
        {
            case AEA.Parametros.Acciones.Guardar.Actualizar:
                ModificarSucursal();
                break;
            case AEA.Parametros.Acciones.Guardar.Nuevo:
                RegistrarSucursal();
                break;
        }
    }

    protected void btnGuardar_Load(object sender, EventArgs e)
    {
        this.btnSave.ButtonClickDemo += new EventHandler(SaveButtonClick);
    }
}
