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


public partial class Automotor : System.Web.UI.Page
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
            if (Request.QueryString["id"] != null)
            {
                this.txtCodAutomotor.Text = Request.QueryString["id"];
                try
                {
                    ConsultarAutomotor(int.Parse(Request.QueryString["id"]));
                    SetAdvanceButtons();
                    SetNewButton();
                }
                catch (Exception ex)
                {
                }
            }
            if (Request.QueryString[AEA.Parametros.QueryStringParReturnURL] != null)
            {
                this.btnBack.TargetURL = Request.QueryString[AEA.Parametros.QueryStringParReturnURL];
                this.btnBack.Visible = true;
            }
        }
    }

    protected void ValidateAccess()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoConsAutomotor))
        {
            Response.Redirect("AccessDenied.aspx");
        }
    }

    protected void SetNewButton()
    {
        string returnURL = AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath + "?id="+this.txtCodAutomotor.Text;
        this.btnNew.TargetURL = "ModeloAutomotor.aspx?" + returnURL;
    }

    protected void SetAdvanceButtons()
    {
        string returnURL = AEA.Parametros.QueryStringParReturnURL + "=" + Page.AppRelativeVirtualPath+"?id="+this.txtCodAutomotor.Text;
        this.btnNewFrom.TargetURL = "ModeloAutomotor.aspx?action=newfrom&anio=" + this.hdnAnio.Value +
            "&codMarca=" + this.hdnCodMarca.Value +
            "&codModelo=" + this.hdnCodModelo.Value +
            "&codColor=" + this.hdnCodColor.Value + "&" + returnURL;
        this.btnModify.TargetURL = "ModeloAutomotor.aspx?action=edit&anio=" + this.hdnAnio.Value +
            "&codMarca=" + this.hdnCodMarca.Value +
            "&codModelo=" + this.hdnCodModelo.Value +
            "&codColor=" + this.hdnCodColor.Value +
            "&id=" + this.txtCodAutomotor.Text + "&" + returnURL;
    }

    protected void ConsultarAutomotor(int codAutomotor)
    {        
        this.detViewAutomotor.Visible = true;
        this.divError.Visible = false;
        this.divWarning.Visible = false;

        AEA.Automotor oAutomotor = new AEA.Automotor(codAutomotor, this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oAutomotor.CargarDatosAutomotor())
        {
            this.detViewAutomotor.DataSource = oAutomotor.AutomotorDataTable();
            this.detViewAutomotor.DataBind();
            this.imgFotoModeloAutomotor.ImageUrl = "FotoModeloAutomotor.aspx?codAutomotor=" + codAutomotor.ToString();
            this.imgFotoModeloAutomotor.DataBind();
            this.btnVerFoto.Visible = true;
            this.btnNewFrom.Visible = true;
            this.btnModify.Visible = true;
            this.btnDesactivar.Visible = true;
            this.hdnAnio.Value = oAutomotor.anio_produccion;
            this.hdnCodMarca.Value = oAutomotor.CodigoMarca.ToString();
            this.hdnCodModelo.Value = oAutomotor.CodigoModelo.ToString();
            this.hdnCodColor.Value = oAutomotor.CodigoColor.ToString();
        }
        else
        {
            this.btnNewFrom.Visible = false;
            this.btnModify.Visible = false;
            this.btnDesactivar.Visible = false;
            this.detViewAutomotor.DataSource = null;
            this.detViewAutomotor.DataBind();
            this.btnVerFoto.Visible = false;
            this.hdnAnio.Value = null;
            this.hdnCodMarca.Value = null;
            this.hdnCodModelo.Value = null;
            this.hdnCodColor.Value = null;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ConsultarAutomotor(int.Parse(this.txtCodAutomotor.Text));
        SetAdvanceButtons();
        SetNewButton();
    }

    
    protected void btnVerFoto_Click(object sender, ImageClickEventArgs e)
    {
        
    }
    
    protected void btnModificar_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("ModeloAutomotor.aspx?action=edit&anio=" + this.hdnAnio.Value +
            "&codMarca=" + this.hdnCodMarca.Value +
            "&codModelo=" + this.hdnCodModelo.Value +
            "&codColor=" + this.hdnCodColor.Value + 
            "&id=" + this.txtCodAutomotor.Text);
    }
    protected void btnDesactivar_Click(object sender, ImageClickEventArgs e)
    {
        this.detViewAutomotor.Visible = false;
        this.divDesactiva.Visible = true;
        this.btnSearch.Visible = false;
        this.txtCodAutomotor.ReadOnly = true;
        this.btnNew.Visible = false;
        this.btnNewFrom.Visible = false;
        this.btnModify.Visible = false;
        this.btnDesactivar.Visible = false;
        this.btnVerFoto.Visible = false;
    }

    protected void DesactivarAutomotor()
    {
        AEA.Automotor oAutomotor = new AEA.Automotor(int.Parse(this.txtCodAutomotor.Text), this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oAutomotor.ReprobarAutomotor(AEA.Parametros.EstadoAutomotor.Desactivar, this.txtDesactivaObs.Text))
        {
            ShowSuccessMessage(oAutomotor.TrxMessage);
        }
        else
        {
            ShowFailureMessage(oAutomotor.TrxError + "<br />" + oAutomotor.TrxMessage);
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

    protected void btnDesacitvar_Click(object sender, EventArgs e)
    {
        DesactivarAutomotor();
        this.txtCodAutomotor.ReadOnly = false;
        this.btnSearch.Visible = true;
        this.divDesactiva.Visible = false;
    }
    protected void btnDesacCancelar_Click(object sender, EventArgs e)
    {
        this.divDesactiva.Visible = false;
        this.txtCodAutomotor.ReadOnly = false;
        this.btnSearch.Visible = true;
    }
    
}
