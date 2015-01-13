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

public partial class RegistroComercializadora : System.Web.UI.Page
{
    private string currentUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.currentUser = User.Identity.Name.ToString();
        if (!IsPostBack)
        {
            SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoInsActComercializadora))
            {
                Response.Redirect("AccessDenied.aspx");
            }
            SetBackButton();
            CargarProvincias();

            if (Request.QueryString["id"] != null)
            {
                this.hdnCodComercializadora.Value = Request.QueryString["id"];
                if (Request.QueryString["action"] != null)
                {
                    switch (Request.QueryString["action"])
                    {
                        case "edit":
                            LoadComercializadora(int.Parse(Request.QueryString["id"]));
                            this.btnSave.Tipo = AEA.Parametros.Acciones.Guardar.Actualizar;
                            this.btnSave.Visible = true;
                            break;
                        case "view":
                            LoadComercializadora(int.Parse(Request.QueryString["id"]));
                            SetControlsToReadOnly();
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    this.btnSave.Tipo = AEA.Parametros.Acciones.Guardar.Nuevo;
                    this.btnSave.Visible = true;
                }
            }
            else
            {
                this.btnSave.Tipo = AEA.Parametros.Acciones.Guardar.Nuevo;
                this.btnSave.Visible = true;
            }
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

    

    protected void LoadComercializadora(int idComercializadora)
    {
        AEA.Comercializadora oComercializ = new AEA.Comercializadora(idComercializadora, this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        
        if (oComercializ.CargarComercializadora())
        {
            this.txtEmail.Text = oComercializ.Email;
            this.txtEmailContacto.Text = oComercializ.EmailContacto1;
            this.txtEmailContacto2.Text = oComercializ.EmailContacto2;
            this.txtNombreComercial.Text = oComercializ.NombreComercial;
            this.txtNombreContacto.Text = oComercializ.NombreContacto1;
            this.txtNombreContacto2.Text = oComercializ.NombreContacto2;
            this.txtRazonSocial.Text = oComercializ.RazonSocial;
            this.txtRepLegal.Text = oComercializ.RepresentanteLegal;
            this.txtRUC.Text = oComercializ.RUC;
            this.txtTelefonoConv.Text = oComercializ.TelefonoConvencional;
            this.txtTelefonoMovil.Text = oComercializ.TelefonoMovil;
            this.ddlProvincia.Items.FindByValue(oComercializ.CodProvincia).Selected = true;
            CargarCantones(oComercializ.CodProvincia);
            ListItem itemCanton = this.ddlCanton.Items.FindByValue(oComercializ.CodCanton);
            if (itemCanton != null)
                itemCanton.Selected = true;
        }
        else
        {
            ShowFailureMessage("No se pudieron cargar los datos de la comercializadora");
        }
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

    //gviteri: presenta información de los cantones.
    protected void CargarCantones(string cnProvincia)
    {
        AEA.Parametros oCantonParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtCantones = oCantonParam.RetornaCantones(cnProvincia);

        this.ddlCanton.DataSource = dtCantones;
        this.ddlCanton.DataValueField = dtCantones.Columns[0].ColumnName;
        this.ddlCanton.DataTextField = dtCantones.Columns[1].ColumnName;
        this.ddlCanton.DataBind();
    }

    protected void RegistrarComercializadora()
    {
        AEA.Comercializadora oComercializ = new AEA.Comercializadora(this.txtRazonSocial.Text, this.txtRUC.Text, this.txtRepLegal.Text, this.ddlProvincia.SelectedValue, this.ddlCanton.SelectedValue, this.txtNombreComercial.Text,
            this.txtTelefonoConv.Text, this.txtTelefonoMovil.Text, this.txtNombreContacto.Text, this.txtEmail.Text, this.txtEmailContacto.Text, this.txtNombreContacto2.Text, this.txtEmailContacto2.Text,
            this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        this.divForms.Visible = false;
        this.btnSave.Visible = false;

        if (oComercializ.RegistrarNueva())
            ShowSuccessMessage(oComercializ.TrxMessage);
        else
            ShowFailureMessage(oComercializ.TrxError);
    }

    protected void ModificarComercializadora()
    {
        AEA.Comercializadora oComercializ = new AEA.Comercializadora(int.Parse(this.hdnCodComercializadora.Value), this.txtRazonSocial.Text, this.txtRUC.Text, this.txtRepLegal.Text, this.ddlProvincia.SelectedValue, this.ddlCanton.SelectedValue, this.txtNombreComercial.Text,
            this.txtTelefonoConv.Text, this.txtTelefonoMovil.Text, this.txtNombreContacto.Text, this.txtEmail.Text, this.txtEmailContacto.Text, this.txtNombreContacto2.Text, this.txtEmailContacto2.Text,
            this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        this.divForms.Visible = false;
        this.btnSave.Visible = false;

        if (oComercializ.Actualizar())
            ShowSuccessMessage(oComercializ.TrxMessage);
        else
            ShowFailureMessage(oComercializ.TrxError);
    }
    

    protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCantones(this.ddlProvincia.SelectedValue);
    }

    protected void ddlCanton_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    protected void SaveButtonClick(object sender, EventArgs e)
    {
        switch (this.btnSave.Tipo)
        {
            case AEA.Parametros.Acciones.Guardar.Actualizar:
                ModificarComercializadora();
                break;
            case AEA.Parametros.Acciones.Guardar.Nuevo:
                RegistrarComercializadora();
                break;
        }
    }

    protected void btnGuardar_Load(object sender, EventArgs e)
    {
        this.btnSave.ButtonClickDemo += new EventHandler(SaveButtonClick);
    }
}
