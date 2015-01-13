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
    private static DataTable dtDetalles;

    protected void Page_Load(object sender, EventArgs e)
    {
        this.currentUser = User.Identity.Name.ToString();
        if (!IsPostBack)
        {
            ValidateAccess();
            SetBackButton();
            InicializarDataTableDetalles();
            DropDownList ddlEmptyInsertAutomotor = GridView1.Controls[0].Controls[0].FindControl("ddlEmptyInsertAutomotor") as DropDownList;
            FillDropDownGravamen(ddlEmptyInsertAutomotor);
            this.btnSave.Tipo = AEA.Parametros.Acciones.Guardar.Nuevo;
        }
    }

    protected void ValidateAccess()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoTrxSolMatricula))
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

    protected void InicializarDataTableDetalles()
    {
        dtDetalles = new DataTable("detalles");
        dtDetalles.Columns.Add("RAMV");
        dtDetalles.Columns.Add("Automotor");
        dtDetalles.Columns.Add("Gravamen");
        dtDetalles.Columns.Add("Anio");
        dtDetalles.Columns.Add("Marca");
        dtDetalles.Columns.Add("Modelo");
        dtDetalles.Columns.Add("CodColor");
        dtDetalles.Columns.Add("DescColor");
        dtDetalles.Columns.Add("Comercializadora");
        this.GridView1.DataSource = dtDetalles;
        this.GridView1.DataBind();
    }

    protected void GridViewDataBind()
    {
        GridView1.DataSource = dtDetalles;
        GridView1.DataBind();
    }


    protected void FillDropDownGravamen(DropDownList webcontrol)
    {
        DataTable dtGravamen = new DataTable("gravamen");
        dtGravamen.Columns.Add("value");
        dtGravamen.Columns.Add("text");
        DataRow drSI = dtGravamen.NewRow();
        drSI[0] = "S";
        drSI[1] = "Si";
        dtGravamen.Rows.Add(drSI);
        DataRow drNO = dtGravamen.NewRow();
        drNO[0] = "N";
        drNO[1] = "No";
        dtGravamen.Rows.Add(drNO);
        webcontrol.DataSource = dtGravamen;
        webcontrol.DataValueField = dtGravamen.Columns[0].ColumnName;
        webcontrol.DataTextField = dtGravamen.Columns[1].ColumnName;
        webcontrol.DataBind();
    }

    protected void RegistrarSolicitud()
    {
        this.lblSubtitle.Visible = false;
        this.gvDetallesErrores.Visible = false;
        this.divForms.Visible = false;
        this.btnSave.Visible = false;
        AEA.SolicitudMatricula oSolicMatric = new AEA.SolicitudMatricula(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        for (int i = 0; i < dtDetalles.Rows.Count; i++)
        {
            if (oSolicMatric.InsertaDetalleSolicitud(dtDetalles.Rows[i][0].ToString(), int.Parse(dtDetalles.Rows[i][8].ToString()), dtDetalles.Rows[i][2].ToString()))
            {
            }
        }
        if (oSolicMatric.GuardarNuevaSolicitud())
        {
            if (oSolicMatric.NumDetallesFallidosAlGrabar == 0)
            {
                ShowSuccessMessage(oSolicMatric.TrxMessage);
                HideInsertControls();
            }
            else
            {
                if (oSolicMatric.NumDetallesFallidosAlGrabar == oSolicMatric.NumDetalles)
                {
                    int i = 0;
                    while (!oSolicMatric.EliminaSolicitud())
                    {
                        if (i == 3)
                            break;
                        i++;
                    }
                    ShowFailureMessage("No se grabó la solicitud porque no se pudo ingresar ningún detalle de la misma");
                }
                else
                {
                    ShowSuccessMessage("Se ha grabado la solicitud, pero existen detalles que no pudieron ser ingresados");
                    HideInsertControls();
                }
                ShowErroresDetalles(oSolicMatric.DetallesFallidosAlGrabar);
            }
        }
        else
        {
            ShowFailureMessage(oSolicMatric.TrxError);
        }

        if (GridView1.Rows.Count == 0)
        {
            DropDownList ddlEmptyInsertAutomotor = GridView1.Controls[0].Controls[0].FindControl("ddlEmptyInsertAutomotor") as DropDownList;
            FillDropDownGravamen(ddlEmptyInsertAutomotor);
        }
    }

    protected void AgregarDetalle(string RAMV, string Automotor, string Gravamen, 
        string Anio, string CodMarca, string CodModelo, string CodColor, string DescColor, int codAutomotor)
    {
        //this.dtDetalles = (DataTable)this.GridView1.DataSource;
        DataRow dr = dtDetalles.NewRow();
        dr[0] = RAMV;
        dr[1] = Automotor;
        dr[2] = Gravamen;
        dr[3] = Anio;
        dr[4] = CodMarca;
        dr[5] = CodModelo;
        dr[6] = CodColor;
        dr[7] = DescColor;
        dr[8] = codAutomotor;
        dtDetalles.Rows.Add(dr);
        this.GridView1.DataSource = dtDetalles;
        this.GridView1.DataBind();
    }

    protected void txtEmptyInsertRAMV_TextChanged(object sender, EventArgs e)
    {
        TextBox textboxSender = (TextBox) sender;
        AEA.SolicitudMatricula oSolicMatric = new AEA.SolicitudMatricula(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oSolicMatric.CargarDatosAutomotorPorRAMV(textboxSender.Text))
        {
            Label lblEmptyDescModelo = (Label)GridView1.Controls[0].Controls[0].FindControl("lblEmptyDescModelo");
            HiddenField hdnEmptyCodMarca = (HiddenField)GridView1.Controls[0].Controls[0].FindControl("hdnEmptyCodMarca");
            HiddenField hdnEmptyCodModelo = (HiddenField)GridView1.Controls[0].Controls[0].FindControl("hdnEmptyCodModelo");
            HiddenField hdnEmptyAnio = (HiddenField)GridView1.Controls[0].Controls[0].FindControl("hdnEmptyAnio");
            HiddenField hdnEmptyCodComerc = (HiddenField)GridView1.Controls[0].Controls[0].FindControl("hdnEmptyCodComerc");
            DropDownList ddlEmptyCodColor = (DropDownList)GridView1.Controls[0].Controls[0].FindControl("ddlEmptyCodColor");
            DropDownList ddlEmptyInsertAutomotor = (DropDownList)GridView1.Controls[0].Controls[0].FindControl("ddlEmptyInsertAutomotor");
            ImageButton btnEmptyInsert = (ImageButton)GridView1.Controls[0].Controls[0].FindControl("btSend");
            ddlEmptyCodColor.DataSource = oSolicMatric.PosiblesColores;
            ddlEmptyCodColor.DataValueField = oSolicMatric.PosiblesColores.Columns[0].ColumnName;
            ddlEmptyCodColor.DataTextField = oSolicMatric.PosiblesColores.Columns[1].ColumnName;
            ddlEmptyCodColor.DataBind();
            Label lblHeaderModelo = (Label)GridView1.Controls[0].Controls[0].FindControl("lblEmptyModelo");
            Label lblHeaderColor = (Label)GridView1.Controls[0].Controls[0].FindControl("lblEmptyColor");
            Label lblHeaderGravamen = (Label)GridView1.Controls[0].Controls[0].FindControl("lblEmptyGravamen");
            lblHeaderColor.Visible = true;
            lblHeaderGravamen.Visible = true;
            lblHeaderModelo.Visible = true;
            lblEmptyDescModelo.Visible = true;
            ddlEmptyInsertAutomotor.Visible = true;
            ddlEmptyCodColor.Visible = true;
            btnEmptyInsert.Visible = true;
            lblEmptyDescModelo.Text = oSolicMatric.DescripcionAutomotor;
            hdnEmptyCodMarca.Value = oSolicMatric.CodMarcaAutomotor.ToString();
            hdnEmptyCodModelo.Value = oSolicMatric.CodModeloAutomotor.ToString();
            hdnEmptyAnio.Value = oSolicMatric.CodAnioAutomotor.ToString();
            hdnEmptyCodComerc.Value = oSolicMatric.CodComercializadora.ToString();
        }
        else
        {
            AlertJS(oSolicMatric.TrxError);
        }
    }


    protected void txtInsertRAMV_TextChanged(object sender, EventArgs e)
    {
        TextBox textboxSender = (TextBox)sender;
        AEA.SolicitudMatricula oSolicMatric = new AEA.SolicitudMatricula(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oSolicMatric.CargarDatosAutomotorPorRAMV(textboxSender.Text))
        {
            ImageButton btnEmptyInsert = (ImageButton)GridView1.FooterRow.Cells[0].FindControl("btInsert");
            btnEmptyInsert.Visible = true;
            Label lblDescModelo = (Label)GridView1.FooterRow.Cells[2].FindControl("lblAutomotorInsert");
            HiddenField hdnCodMarca = (HiddenField)GridView1.FooterRow.Cells[3].FindControl("hdnAutomotorMarcaInsert");
            HiddenField hdnCodModelo = (HiddenField)GridView1.FooterRow.Cells[4].FindControl("hdnAutomotorModeloInsert");
            HiddenField hdnAnio = (HiddenField)GridView1.FooterRow.Cells[5].FindControl("hdnAutomotorAnioInsert");
            DropDownList ddlCodColor = (DropDownList)GridView1.FooterRow.Cells[6].FindControl("ddlAutomotorColorInsert");
            HiddenField hdnCodComerc = (HiddenField)GridView1.FooterRow.Cells[8].FindControl("hdnCodComercInsert");
            ddlCodColor.DataSource = oSolicMatric.PosiblesColores;
            ddlCodColor.DataValueField = oSolicMatric.PosiblesColores.Columns[0].ColumnName;
            ddlCodColor.DataTextField = oSolicMatric.PosiblesColores.Columns[1].ColumnName;
            ddlCodColor.DataBind();
            ddlCodColor.Visible = true;
            lblDescModelo.Text = oSolicMatric.DescripcionAutomotor;
            hdnCodMarca.Value = oSolicMatric.CodMarcaAutomotor.ToString();
            hdnCodModelo.Value = oSolicMatric.CodModeloAutomotor.ToString();
            hdnAnio.Value = oSolicMatric.CodAnioAutomotor.ToString();
            hdnCodComerc.Value = oSolicMatric.CodComercializadora.ToString();
            DropDownList ddlGravamen = (DropDownList)GridView1.FooterRow.FindControl("ddlInsertAutomotor");
            FillDropDownGravamen(ddlGravamen);
            ddlGravamen.Visible = true;
            ddlCodColor.Visible = true;
        }
        else
        {
            AlertJS(oSolicMatric.TrxError);
        }
    }


    protected void txtEditRAMV_TextChanged(object sender, EventArgs e)
    {
        TextBox textboxSender = (TextBox)sender;
        AEA.SolicitudMatricula oSolicMatric = new AEA.SolicitudMatricula(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oSolicMatric.CargarDatosAutomotorPorRAMV(textboxSender.Text))
        {
            Label lblDescModelo = (Label)GridView1.Rows[int.Parse(this.hdnGridViewEditIndex.Value)].Cells[2].FindControl("lblAutomotorEdit");
            HiddenField hdnCodMarca = (HiddenField)GridView1.Rows[int.Parse(this.hdnGridViewEditIndex.Value)].Cells[3].FindControl("hdnAutomotorAnioEdit");
            HiddenField hdnCodModelo = (HiddenField)GridView1.Rows[int.Parse(this.hdnGridViewEditIndex.Value)].Cells[4].FindControl("hdnAutomotorMarcaEdit");
            HiddenField hdnAnio = (HiddenField)GridView1.Rows[int.Parse(this.hdnGridViewEditIndex.Value)].Cells[5].FindControl("hdnAutomotorModeloEdit");
            DropDownList ddlCodColor = (DropDownList)GridView1.Rows[int.Parse(this.hdnGridViewEditIndex.Value)].Cells[6].FindControl("ddlAutomotorColorEdit");
            HiddenField hdnCodComerc = (HiddenField)GridView1.Rows[int.Parse(this.hdnGridViewEditIndex.Value)].Cells[8].FindControl("hdnCodComercEdit");
            ddlCodColor.DataSource = oSolicMatric.PosiblesColores;
            ddlCodColor.DataValueField = oSolicMatric.PosiblesColores.Columns[0].ColumnName;
            ddlCodColor.DataTextField = oSolicMatric.PosiblesColores.Columns[1].ColumnName;
            ddlCodColor.DataBind();
            ddlCodColor.Visible = true;
            lblDescModelo.Text = oSolicMatric.DescripcionAutomotor;
            hdnCodMarca.Value = oSolicMatric.CodMarcaAutomotor.ToString();
            hdnCodModelo.Value = oSolicMatric.CodModeloAutomotor.ToString();
            hdnAnio.Value = oSolicMatric.CodAnioAutomotor.ToString();
            hdnCodComerc.Value = oSolicMatric.CodComercializadora.ToString();
        }
        else
        {
            AlertJS(oSolicMatric.TrxError);
        }
    }

    protected int CodigoAutomotor(int anio, int codMarca, int codModelo, int codColor, int codComerc, out string error)
    {
        AEA.Automotor oAutomotor = new AEA.Automotor(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        int codAutomotor = oAutomotor.ObtenerCodigoAutomotor(anio, codMarca, codModelo, codColor, codComerc);
        error = oAutomotor.TrxError;
        return codAutomotor;
    }

    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EmptyInsert")
        {
            //handle insert here
            TextBox tbEmptyInsertRAMV = (TextBox)GridView1.Controls[0].Controls[0].FindControl("txtEmptyInsertRAMV");
            //TextBox tbEmptyInsertAutomotor = (TextBox)GridView1.Controls[0].Controls[0].FindControl("txtEmptyInsertAutomotor");
            Label lblEmptyDescModelo = (Label)GridView1.Controls[0].Controls[0].FindControl("lblEmptyDescModelo");
            DropDownList ddlEmptyInsertAutomotor = (DropDownList)GridView1.Controls[0].Controls[0].FindControl("ddlEmptyInsertAutomotor");
            HiddenField hdnEmptyCodMarca = (HiddenField)GridView1.Controls[0].Controls[0].FindControl("hdnEmptyCodMarca");
            HiddenField hdnEmptyCodModelo = (HiddenField)GridView1.Controls[0].Controls[0].FindControl("hdnEmptyCodModelo");
            HiddenField hdnEmptyAnio = (HiddenField)GridView1.Controls[0].Controls[0].FindControl("hdnEmptyAnio");
            DropDownList ddlEmptyCodColor = (DropDownList)GridView1.Controls[0].Controls[0].FindControl("ddlEmptyCodColor");
            HiddenField hdnEmptyCodComerc = (HiddenField)GridView1.Controls[0].Controls[0].FindControl("hdnEmptyCodComerc");

            //Label1.Text = string.Format("You would have inserted the name : <b>{0}</b> from the emptydatatemplate", tbEmptyInsertRAMV.Text);
            string error;
            int codAutomotor = CodigoAutomotor(int.Parse(hdnEmptyAnio.Value), int.Parse(hdnEmptyCodMarca.Value), int.Parse(hdnEmptyCodModelo.Value), int.Parse(ddlEmptyCodColor.SelectedValue), int.Parse(hdnEmptyCodComerc.Value), out error);
            if (codAutomotor != -1)
            {
                InicializarDataTableDetalles();
                AgregarDetalle(tbEmptyInsertRAMV.Text, lblEmptyDescModelo.Text, ddlEmptyInsertAutomotor.SelectedValue,
                    hdnEmptyAnio.Value, hdnEmptyCodMarca.Value, hdnEmptyCodModelo.Value, ddlEmptyCodColor.SelectedValue, ddlEmptyCodColor.SelectedItem.Text, codAutomotor);
                this.btnSave.Visible = true;
                //this.btnCancelar.Visible = true;
            }
            else
            {
                AlertJS(error);
            }
        }
        if (e.CommandName == "Insert")
        {
            //handle insert here
            TextBox tbInsertRAMV = (TextBox) GridView1.FooterRow.FindControl("txtInsertRAMV");
            Label lblInsertAutomotor = (Label) GridView1.FooterRow.FindControl("lblAutomotorInsert");
            DropDownList ddlRAMV = (DropDownList)GridView1.FooterRow.FindControl("ddlInsertAutomotor");
            HiddenField hdnInsertCodMarca = (HiddenField)GridView1.FooterRow.FindControl("hdnAutomotorMarcaInsert");
            HiddenField hdnInsertCodModelo = (HiddenField)GridView1.FooterRow.FindControl("hdnAutomotorModeloInsert");
            HiddenField hdnInsertAnio = (HiddenField)GridView1.FooterRow.FindControl("hdnAutomotorAnioInsert");
            DropDownList ddlInsertCodColor = (DropDownList)GridView1.FooterRow.FindControl("ddlAutomotorColorInsert");
            HiddenField hdnCodComerc = (HiddenField)GridView1.FooterRow.FindControl("hdnCodComercInsert");
            //Label1.Text = string.Format("You would have inserted the name :  <b>{0}</b> from the footerrow", tbInsert.Text);
            string error;
            int codAutomotor = CodigoAutomotor(int.Parse(hdnInsertAnio.Value), int.Parse(hdnInsertCodMarca.Value), int.Parse(hdnInsertCodModelo.Value), int.Parse(ddlInsertCodColor.SelectedValue), int.Parse(hdnCodComerc.Value), out error);
            if (codAutomotor != -1)
            {
                AgregarDetalle(tbInsertRAMV.Text, lblInsertAutomotor.Text, ddlRAMV.SelectedValue,
                    hdnInsertAnio.Value, hdnInsertCodMarca.Value, hdnInsertCodModelo.Value, ddlInsertCodColor.SelectedValue, ddlInsertCodColor.SelectedItem.Text, codAutomotor);
                ddlRAMV.Visible = false;
                ddlInsertCodColor.Visible = false;
                ImageButton btnEmptyInsert = (ImageButton)GridView1.FooterRow.FindControl("btInsert");
                btnEmptyInsert.Visible = false;
            }
            else
            {
                AlertJS(error);
            }
        }
        
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        this.hdnGridViewEditIndex.Value = e.NewEditIndex.ToString();
        GridView1.EditIndex = e.NewEditIndex;
        GridViewDataBind();
//        DropDownList ddlGravamen = (DropDownList)(GridView1.Rows[e.NewEditIndex].Cells[3].FindControl("ddlGravamen"));
        DropDownList ddlGravamen = (DropDownList)(GridView1.Rows[e.NewEditIndex].Cells[6].FindControl("ddlGravamen"));
        FillDropDownGravamen(ddlGravamen);
        ddlGravamen.Items.FindByValue(dtDetalles.Rows[e.NewEditIndex][2].ToString()).Selected = true;
        DropDownList ddlColor = (DropDownList)(GridView1.Rows[e.NewEditIndex].Cells[5].FindControl("ddlAutomotorColorEdit"));
        AEA.SolicitudMatricula oSolicMatric = new AEA.SolicitudMatricula(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        TextBox txtRAMVedit = (TextBox)GridView1.Rows[e.NewEditIndex].Cells[1].FindControl("tbRAMV");
        if (oSolicMatric.CargarDatosAutomotorPorRAMV(txtRAMVedit.Text))
        {
            ddlColor.DataSource = oSolicMatric.PosiblesColores;
            ddlColor.DataValueField = oSolicMatric.PosiblesColores.Columns[0].ColumnName;
            ddlColor.DataTextField = oSolicMatric.PosiblesColores.Columns[1].ColumnName;
            ddlColor.DataBind();
        }
        else
        {
            AlertJS(oSolicMatric.TrxError);
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        GridViewDataBind();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtRAMV = ((TextBox)(GridView1.Rows[e.RowIndex].Cells[1].FindControl("tbRAMV")));
        Label lblAutomotor = ((Label)(GridView1.Rows[e.RowIndex].Cells[2].FindControl("lblAutomotorEdit")));
        HiddenField hdnAnio = ((HiddenField)(GridView1.Rows[e.RowIndex].Cells[3].FindControl("hdnAutomotorAnioEdit")));
        HiddenField hdnMarca = ((HiddenField)(GridView1.Rows[e.RowIndex].Cells[4].FindControl("hdnAutomotorMarcaEdit")));
        HiddenField hdnModelo = ((HiddenField)(GridView1.Rows[e.RowIndex].Cells[5].FindControl("hdnAutomotorModeloEdit")));
        DropDownList ddlColor = ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[6].FindControl("ddlAutomotorColorEdit")));
        DropDownList ddlGravamen = ((DropDownList)(GridView1.Rows[e.RowIndex].Cells[7].FindControl("ddlGravamen")));
        HiddenField hdnCodComerc = ((HiddenField)(GridView1.Rows[e.RowIndex].Cells[8].FindControl("hdnCodComercEdit")));
        dtDetalles.Rows[e.RowIndex][0] = txtRAMV.Text;
        dtDetalles.Rows[e.RowIndex][1] = lblAutomotor.Text;
        dtDetalles.Rows[e.RowIndex][2] = ddlGravamen.SelectedValue;
        dtDetalles.Rows[e.RowIndex][3] = hdnAnio.Value;
        dtDetalles.Rows[e.RowIndex][4] = hdnMarca.Value;
        dtDetalles.Rows[e.RowIndex][5] = hdnModelo.Value;
        dtDetalles.Rows[e.RowIndex][6] = ddlColor.SelectedValue;
        dtDetalles.Rows[e.RowIndex][7] = ddlColor.SelectedItem.Text;
        dtDetalles.Rows[e.RowIndex][8] = hdnCodComerc.Value;
        this.GridView1.EditIndex = -1;
        GridViewDataBind();
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        dtDetalles.Rows.RemoveAt(e.RowIndex);
        GridView1.DataSource = dtDetalles;
        GridView1.EditIndex = -1;
        GridViewDataBind();
        if (GridView1.Rows.Count == 0)
        {
            DropDownList ddlEmptyInsertAutomotor = GridView1.Controls[0].Controls[0].FindControl("ddlEmptyInsertAutomotor") as DropDownList;
            FillDropDownGravamen(ddlEmptyInsertAutomotor);
        }
    }

    //protected void CargaListaGestores()
    //{
    //    AEA.Gestor oGestor = new AEA.Gestor(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
    //    DataTable dtGestores = oGestor.ObtenerGestores();
    //    this.ddlGestores.DataSource = dtGestores;
    //    this.ddlGestores.DataTextField = dtGestores.Columns[2].ColumnName;
    //    this.ddlGestores.DataValueField = dtGestores.Columns[0].ColumnName;
    //    this.ddlGestores.DataBind();
    //}

    protected void lbInsert_Click(object sender, EventArgs e)
    {

        //odsIngredient.Insert();

    }

    protected void gvDetalleSolicitud_RowCreated(object sender, EventArgs e)
    {
    }

    protected void CancelInsert_Click(object sender, EventArgs e)
    {

    }

    protected void HideInsertControls()
    {
        this.lblGestores.Visible = false;
        this.ddlGestores.Visible = false;
        this.GridView1.Visible = false;
    }



    protected void ShowErroresDetalles(DataTable detallesFallidos)
    {
        this.gvDetallesErrores.DataSource = detallesFallidos;
        this.gvDetallesErrores.DataBind();
        this.gvDetallesErrores.Visible = true;
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
        RegistrarSolicitud();
    }

    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }

    protected void btnSave_Load(object sender, EventArgs e)
    {
        this.btnSave.ButtonClickDemo += new EventHandler(SaveButtonClick);
    }
}
