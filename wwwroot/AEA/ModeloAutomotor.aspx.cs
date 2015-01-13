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


public partial class ModeloAutomotor : System.Web.UI.Page
{
    private string currentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.currentUser = User.Identity.Name.ToString();

        if (!IsPostBack)
        {
            ValidateAccess();
            SetBackButton();
            LoadColores();
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    if (Request.QueryString["id"] == null || Request.QueryString["id"] != "")
                    {
                        this.btnSave.Visible = true;
                        this.btnSave.Tipo = AEA.Parametros.Acciones.Guardar.Nuevo;
                    }

                    if (Request.QueryString["action"] == "newfrom")
                    {
                        this.btnSave.Visible = true;
                        this.btnSave.Tipo = AEA.Parametros.Acciones.Guardar.Nuevo;
                        this.btnDeletePhoto.Visible = false;
                        LoadValuesFromCons();
                    }
                    else if (Request.QueryString["action"] == "edit")
                    {
                        this.hdnCodAutomotor.Value = Request.QueryString["id"];
                        this.btnSave.Visible = true;
                        this.btnSave.Tipo = AEA.Parametros.Acciones.Guardar.Actualizar;
                        LoadValuesFromCons();
                        this.btnDeletePhoto.Visible = true;
                        this.fileupFoto.Visible = false;
                        this.imgFotoAutomotor.Visible = true;
                        this.imgFotoAutomotor.ImageUrl = "FotoModeloAutomotor.aspx?codAutomotor=" + this.hdnCodAutomotor.Value;
                        this.imgFotoAutomotor.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    ShowFailureMessage(ex.Message);
                }
            }
            else
            {
                this.btnSave.Visible = true;
                this.btnSave.Tipo = AEA.Parametros.Acciones.Guardar.Nuevo;
            }
        }
    }

    protected void ValidateAccess()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoTrxAutomotor))
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

    protected void LoadValuesFromCons()
    {
        this.txtAnioProd.Text = Request.QueryString["anio"];
        LoadMarcas();
        try { this.ddlMarca.Items.FindByValue(Request.QueryString["codMarca"]).Selected = true; }
        catch (Exception ex) { }
        try { LoadModelos(); }
        catch (Exception ex) { }
        try { this.ddlModelo.Items.FindByValue(Request.QueryString["codModelo"]).Selected = true; }
        catch (Exception ex) { }
        this.ddlColor.Items.FindByValue(Request.QueryString["codColor"]).Selected = true;
        this.ddlMarca.Enabled = true;
        this.ddlModelo.Enabled = true;
    }


    protected void LoadMarcas()
    {
        AEA.Parametros oAutomParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtMarcas = oAutomParam.RetornaMarcasSRI(int.Parse(this.txtAnioProd.Text));
        dtMarcas.Rows.Add(null, " -- Seleccione -- ");
        dtMarcas.DefaultView.Sort = dtMarcas.Columns[1].ColumnName + " asc";
        this.ddlMarca.Enabled = true;
        this.ddlMarca.DataSource = dtMarcas;
        this.ddlMarca.DataValueField = dtMarcas.Columns[0].ColumnName;
        this.ddlMarca.DataTextField = dtMarcas.Columns[1].ColumnName;
        this.ddlMarca.DataBind();
    }


    protected void LoadModelos()
    {
        AEA.Parametros oAutomParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtModelos = oAutomParam.RetornaModelos(int.Parse(this.txtAnioProd.Text), int.Parse(this.ddlMarca.SelectedValue));
        dtModelos.Rows.Add(null, " -- Seleccione -- ");
        dtModelos.DefaultView.Sort = dtModelos.Columns[1].ColumnName + " asc";
        this.ddlModelo.Enabled = true;
        this.ddlModelo.DataSource = dtModelos;
        this.ddlModelo.DataValueField = dtModelos.Columns[0].ColumnName;
        this.ddlModelo.DataTextField = dtModelos.Columns[1].ColumnName;
        this.ddlModelo.DataBind();
    }


    protected void LoadColores()
    {
        AEA.Parametros oAutomParam = new AEA.Parametros(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtColores = oAutomParam.RetornaColores();
        dtColores.Rows.Add(null, " -- Seleccione -- ");
        this.ddlColor.DataSource = dtColores;
        this.ddlColor.DataValueField = dtColores.Columns[0].ColumnName;
        this.ddlColor.DataTextField = dtColores.Columns[1].ColumnName;
        this.ddlColor.DataBind();
    }

    protected void RegistrarAutomotor()
    {        
        if (int.Parse(this.txtAnioProd.Text) <= (DateTime.Now.Year + 3))
        {
            byte[] foto = this.fileupFoto.FileBytes;
            if (CheckPhotoSize(foto))
            {
                AEA.Automotor oAutomotor = new AEA.Automotor(int.Parse(this.txtAnioProd.Text), int.Parse(this.ddlMarca.SelectedValue),
                    int.Parse(this.ddlModelo.SelectedValue), int.Parse(this.ddlColor.SelectedValue), foto,
                    this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
                this.divForms.Visible = false;
                this.btnSave.Visible = false;

                if (oAutomotor.RegistrarNuevoAutomotor())
                    ShowSuccessMessage(oAutomotor.TrxMessage);
                else
                    ShowFailureMessage(oAutomotor.TrxError);
            }
        }
        else
        {
            int year = DateTime.Now.Year + 1;
            ShowFailureMessage("El año no puede ser mayor a " + year.ToString());
        }
    }

    protected void ModificarAutomotor()
    {
        if (int.Parse(this.txtAnioProd.Text) <= (DateTime.Now.Year + 1))
        {
            byte[] foto;
            if (this.imgFotoAutomotor.Visible)
                foto = null;
            else
                foto = this.fileupFoto.FileBytes;
            AEA.Automotor oAutomotor = new AEA.Automotor(int.Parse(this.hdnCodAutomotor.Value), int.Parse(this.txtAnioProd.Text), int.Parse(this.ddlMarca.SelectedValue),
                int.Parse(this.ddlModelo.SelectedValue), int.Parse(this.ddlColor.SelectedValue), foto,
                this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            this.divForms.Visible = false;
            this.btnSave.Visible = false;

            if (oAutomotor.ActualizarAutomotor())
                ShowSuccessMessage(oAutomotor.TrxMessage);
            else
                ShowFailureMessage(oAutomotor.TrxError);
        }
        else
        {
            int year = DateTime.Now.Year + 1;
            ShowFailureMessage("El año no puede ser mayor a " + year.ToString());
        }
    }


    protected bool CheckPhotoSize(byte[] photoBytes)
    {
       /* System.IO.MemoryStream ms;
        System.Drawing.Image oImg = System.Drawing.Image.FromStream(new System.IO.MemoryStream(photoBytes));
        oImg.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
        byte[] bmpImg = ms.ToArray();*/
        return true;
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

    

    protected void txtAnioProd_TextChanged(object sender, EventArgs e)
    {
        LoadMarcas();
    }
    protected void ddlMarca_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (((DropDownList)sender).SelectedValue != null && ((DropDownList)sender).SelectedValue != "")
            LoadModelos();
        else
            this.ddlModelo.Enabled = false;
    }
    
    protected void btnDeletePhoto_Click(object sender, EventArgs e)
    {
        this.imgFotoAutomotor.Visible = false;
        this.fileupFoto.Visible = true;
        ((LinkButton)sender).Visible = false;
    }

    protected void SaveButtonClick(object sender, EventArgs e)
    {
        switch (this.btnSave.Tipo)
        {
            case AEA.Parametros.Acciones.Guardar.Actualizar:
                ModificarAutomotor();
                break;
            case AEA.Parametros.Acciones.Guardar.Nuevo:
                RegistrarAutomotor();
                break;
        }
    }

    protected void SaveButton1_Load(object sender, EventArgs e)
    {
        this.btnSave.ButtonClickDemo += new EventHandler(SaveButtonClick);
    }
}
