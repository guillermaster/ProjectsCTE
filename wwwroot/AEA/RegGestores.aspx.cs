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

public partial class RegGestores : System.Web.UI.Page
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

            if (Request.QueryString["id"] != null)
            {
                if (Request.QueryString["action"] != null)
                {
                    switch (Request.QueryString["action"])
                    {
                        case "edit":
                            LoadGestor(int.Parse(Request.QueryString["id"]));
                            this.btnSave.Tipo = AEA.Parametros.Acciones.Guardar.Actualizar;
                            this.btnSave.Visible = true;
                            break;
                        case "view":
                            LoadGestor(int.Parse(Request.QueryString["id"]));
                            SetControlsToReadOnly();
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
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoTrxGestor))
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

    protected void RegistrarGestor()
    {
        AEA.Gestor oGestor = new AEA.Gestor(this.txtCedula.Text, this.txtNombre.Text, this.ddlProvincia.SelectedValue,
            this.txtTelefonoConv.Text, this.txtTelefonoMovil.Text, this.txtEmail.Text, this.txtDireccionDomicilio.Text,
            this.txtDireccionLaboral.Text, this.fileupFoto.FileBytes,
            this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oGestor.RegistrarNuevo())
        {
            this.divForms.Visible = false;
            ShowSuccessMessage(oGestor.TrxMessage);
        }
        else
        {
            ShowFailureMessage(oGestor.TrxError);
        }
    }

    protected void ModificarGestor()
    {
    }

    protected void LoadGestor(int idGestor)
    {
        AEA.Gestor oGestor = new AEA.Gestor(idGestor, this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        if (oGestor.CargarGestor())
        {
            this.txtCedula.Text = oGestor.Cedula;
            this.txtDireccionDomicilio.Text = oGestor.DireccionDomicilio;
            this.txtDireccionLaboral.Text = oGestor.DireccionLaboral;
            this.txtEmail.Text = oGestor.EMail;
            this.txtNombre.Text = oGestor.Nombre;
            this.txtTelefonoConv.Text = oGestor.TelefonoConvencional;
            this.txtTelefonoMovil.Text = oGestor.TelefonoMovil;
            
            this.ddlProvincia.Items.FindByValue(oGestor.CodigoProvincia).Selected = true;
            this.btnVerFoto.Visible = true;
            this.imgFotoModeloAutomotor.ImageUrl = "FotoGestor.aspx?codGestor=" + idGestor.ToString();
            this.imgFotoModeloAutomotor.DataBind();
            //CargarCantones(oComercializ.CodProvincia);
            //ListItem itemCanton = this.ddlCanton.Items.FindByValue(oComercializ.CodCanton);
            //if (itemCanton != null)
            //    itemCanton.Selected = true;
        }
        else
        {
            ShowFailureMessage("No se pudieron cargar los datos de la comercializadora");
        }
    }


    protected void SetControlsToReadOnly()
    {
        this.txtCedula.ReadOnly = true;
        this.txtDireccionDomicilio.ReadOnly = true;
        this.txtDireccionLaboral.ReadOnly = true;
        this.txtEmail.ReadOnly = true;
        this.txtNombre.ReadOnly = true;
        this.txtTelefonoConv.ReadOnly = true;
        this.txtTelefonoMovil.ReadOnly = true;
        this.ddlProvincia.Enabled = false;
        this.lblFoto.Visible = false;
        this.fileupFoto.Visible = false;
    }

    protected void CargarProvincias()
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataView dvProvincias = geog.GetProvinciasConCodigos("ECU").DefaultView;
        dvProvincias.Sort = string.Format("{0} {1}", dvProvincias.Table.Columns[1].ColumnName, "ASC");

        DataRow nullrow = dvProvincias.Table.NewRow();
        nullrow[0] = null;
        nullrow[1] = null;

        dvProvincias.Table.Rows.InsertAt(nullrow, 0);

        this.ddlProvincia.DataSource = dvProvincias;
        this.ddlProvincia.DataValueField = dvProvincias.Table.Columns[0].ColumnName;
        this.ddlProvincia.DataTextField = dvProvincias.Table.Columns[1].ColumnName;
        this.ddlProvincia.DataBind();
    }

    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        RegistrarGestor();
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



    protected void SaveButtonClick(object sender, EventArgs e)
    {
        switch (this.btnSave.Tipo)
        {
            case AEA.Parametros.Acciones.Guardar.Actualizar:
                ModificarGestor();
                break;
            case AEA.Parametros.Acciones.Guardar.Nuevo:
                RegistrarGestor();
                break;
        }
    }

    protected void btnSave_Load(object sender, EventArgs e)
    {
        this.btnSave.ButtonClickDemo += new EventHandler(SaveButtonClick);
    }
    
}
