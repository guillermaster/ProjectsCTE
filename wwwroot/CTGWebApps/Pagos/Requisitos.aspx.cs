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

public partial class Pagos_Requisitos : System.Web.UI.Page
{
    private static DataRow[] tramiteRequisitos;
    private static string trama_requisitos;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Seguridad.UsuarioWeb.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/DefaultPagos.aspx");
            }

            //if (Session[Constantes.UsuarioWeb.SessionVarNameFullAccess].ToString() == Constantes.UsuarioWeb.SessionLimitedAccess)
            //{
            //    Response.Redirect("~/sinPermiso.aspx");
            //}

            Page.Form.DefaultButton = this.btnGenerarCEP.ID;

            if (Request.QueryString["codCatTramite"] != null)
            {
                CargaTramitesFromDB(Request.QueryString["codCatTramite"]);
                Session["codTramite"] = "";
            }
            else
            {
                Response.Redirect("../DefaultPagos.aspx");
            }
        }
        else
        {
            ClearErrorMessages();
        }
    }

    //setea mensaje que se imprimirá al generar el CEP
    protected void SetLocationMessageByArea(string codArea)
    {
        switch (codArea)
        {
            case "ATU":
                this.lblLocationMsg.Text = "Si la placa de su vehículo es de 6 dígitos, debe agregar el 0 despues de los 3 primeros caracteres de su placa. <i>Ejemplo: Para la placa GJK512, se debe ingresar GJK0512</i><br /><br />";
                this.lblLocationMsg.Text += "Nota:  " + Constantes.Tramites.msgLocationAtencionUsuario;
                break;
            case "BRP":
                this.lblLocationMsg.Text = "Nota:  " + Constantes.Tramites.msgLocationBrevetacion;
                break;
            case "JPG":
                this.lblLocationMsg.Text = "Nota:  " + Constantes.Tramites.msgLocationCitaciones;
                break;
            case "MAT":
                this.lblLocationMsg.Text = "Si la placa de su vehículo es de 6 dígitos, debe agregar el 0 despues de los 3 primeros caracteres de su placa. <i>Ejemplo: Para la placa GJK512, se debe ingresar GJK0512</i><br /><br />";
                this.lblLocationMsg.Text += "Nota:  " + Constantes.Tramites.msgLocationMatriculacion;
                break;
            default:
                this.lblLocationMsg.Text = "";
                break;
        }
    }

    protected void CargaTramitesFromDB(string codAreaTramite)
    {
        Constantes.Tramites oTramite = new Constantes.Tramites(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        this.ddlTramite.DataTextField = Constantes.Tramites.fieldNameGenericName;
        this.ddlTramite.DataValueField = Constantes.Tramites.fieldNameGenericCode;
        this.ddlTramite.DataSource = oTramite.GetTramitesFromDB(codAreaTramite);
        this.ddlTramite.DataBind();
    }

    protected void CargaTramitesFromDB(string codAreaTramite, string codSelectedTramite)
    {
        Constantes.Tramites oTramite = new Constantes.Tramites(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        this.ddlTramite.DataTextField = Constantes.Tramites.fieldNameGenericName;
        this.ddlTramite.DataValueField = Constantes.Tramites.fieldNameGenericCode;
        this.ddlTramite.DataSource = oTramite.GetTramitesFromDB(codAreaTramite);
        this.ddlTramite.DataBind();
        this.ddlTramite.SelectedValue = codSelectedTramite;
    }

    protected void CreateControls()
    {

        for (int i = 0; i < tramiteRequisitos.Length; i++)
        {
            HtmlTableRow row = new HtmlTableRow();

            HtmlTableCell left_cell = new HtmlTableCell();
            Label label = new Label();
            label.ID = "lbl" + tramiteRequisitos[i]["item"].ToString();
            label.Text = tramiteRequisitos[i]["etiqueta"].ToString() + ": ";
            left_cell.Controls.Add(label);
            row.Cells.Add(left_cell);

            HtmlTableCell right_cell = new HtmlTableCell();
            if (tramiteRequisitos[i]["sentencia"].ToString()=="" && (tramiteRequisitos[i]["lista"].ToString() == "N" || tramiteRequisitos[i]["lista"].ToString() == "D"))//si es textbox
            {

                TextBox textbox = new TextBox();
                textbox.ID = "txt" + tramiteRequisitos[i]["item"].ToString();
                textbox.Text = "";
                if (tramiteRequisitos[i]["tipo_dato"].ToString() == "F")//tipo fecha
                {
                    right_cell.Controls.Add(textbox);
                    AjaxControlToolkit.CalendarExtender calendar = new AjaxControlToolkit.CalendarExtender();
                    calendar.ID = "cal" + tramiteRequisitos[i]["item"].ToString();
                    calendar.TargetControlID = textbox.ID;
                    right_cell.Controls.Add(calendar);
                }
                else //textbox simple
                {
                    textbox.CssClass = "uppercase";
                    textbox.MaxLength = int.Parse(tramiteRequisitos[i]["longitud"].ToString());
                    textbox.Width = int.Parse(tramiteRequisitos[i]["ancho"].ToString());
                    if (int.Parse(tramiteRequisitos[i]["alto"].ToString()) > 1)
                    {
                        textbox.TextMode = TextBoxMode.MultiLine;
                        textbox.Rows = int.Parse(tramiteRequisitos[i]["alto"].ToString());
                    }

                    if (tramiteRequisitos[i]["entrada"].ToString() == Constantes.Tramites.DBentryNameForUserID)
                    {
                        textbox.Text = User.Identity.Name.ToString();
                        textbox.ReadOnly = true;
                    }


                    right_cell.Controls.Add(textbox);
                }

                if (tramiteRequisitos[i]["requerido"].ToString() == "S")
                {
                    RequiredFieldValidator reqVal = new RequiredFieldValidator();
                    reqVal.ID = "rv" + tramiteRequisitos[i]["item"].ToString();
                    reqVal.ControlToValidate = textbox.ID;
                    reqVal.ErrorMessage = "*";
                    right_cell.Controls.Add(reqVal);
                }

            }
            else//no es textbox  - es dropdownlist
            {
                if (tramiteRequisitos[i]["lista"].ToString() == "D")
                {//esto se realiza si el tipo de campo viene como textbox de la base pero en realidad es dropdownlist xq trae de la base valor en campo sentencia
                    tramiteRequisitos[i]["lista"] = "S";
                }
                DropDownList list = new DropDownList();
                list.ID = "ddl" + tramiteRequisitos[i]["item"].ToString();
                list.Width = int.Parse(tramiteRequisitos[i]["ancho"].ToString());
                list.CssClass = "combo3";
                // cargar lista de valores
                #region "Cargar lista de valores"
                Constantes.Tramites oTramite = new Constantes.Tramites(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
                list.DataTextField = Constantes.Tramites.fieldNameGenericName;
                list.DataValueField = Constantes.Tramites.fieldNameGenericCode;
                list.DataSource = oTramite.GetRequisitoValuesList(tramiteRequisitos[i]["lista"].ToString(), this.ddlTramite.SelectedValue, tramiteRequisitos[i]["id_etapa_proceso"].ToString(), tramiteRequisitos[i]["item"].ToString(), tramiteRequisitos[i]["sentencia"].ToString());
                list.DataBind();
                #endregion
                //
                right_cell.Controls.Add(list);

                if (tramiteRequisitos[i]["requerido"].ToString() == "S")
                {
                    RequiredFieldValidator reqVal = new RequiredFieldValidator();
                    reqVal.ID = "rv" + tramiteRequisitos[i]["item"].ToString();
                    reqVal.ControlToValidate = list.ID;
                    reqVal.ErrorMessage = "*";
                    right_cell.Controls.Add(reqVal);
                }
            }
            row.Cells.Add(right_cell);

            tblDynamic.Rows.Add(row);
        }
    }

    protected void PrintDataConfirmation(string valorPagar)
    {
        string controlName = "";
        for (int i = 0; i < tramiteRequisitos.Length; i++)
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell left_cell = new HtmlTableCell();
            Label label = new Label();
            label.ID = "lbl" + tramiteRequisitos[i]["item"].ToString();
            label.Text = tramiteRequisitos[i]["etiqueta"].ToString() + ": ";
            left_cell.Controls.Add(label);
            row.Cells.Add(left_cell);

            HtmlTableCell right_cell = new HtmlTableCell();

            if (tramiteRequisitos[i]["lista"].ToString() == "N" || tramiteRequisitos[i]["lista"].ToString() == "D")
            {
                controlName = "txt" + tramiteRequisitos[i]["item"].ToString();
            }
            else
            {
                controlName = "ddl" + tramiteRequisitos[i]["item"].ToString();
            }

            Label label2 = new Label();
            label2.ID = "lblVal" + tramiteRequisitos[i]["item"].ToString();
            label2.Text = Request.Form[controlName].ToUpper();
            right_cell.Controls.Add(label2);
            row.Cells.Add(right_cell);
                        
            tblDynamic.Rows.Add(row);
        }

        HtmlTableRow row2 = new HtmlTableRow();
        HtmlTableCell left_cell2 = new HtmlTableCell();
        Label lblValorPagar = new Label();
        lblValorPagar.ID = "lblValorPagar";
        lblValorPagar.Text = "Valor a Pagar: ";
        left_cell2.Controls.Add(lblValorPagar);
        row2.Cells.Add(left_cell2);
        HtmlTableCell right_cell2 = new HtmlTableCell();
        Label lblValValorPagar = new Label();
        lblValValorPagar.ID = "lblValValorPagar";
        lblValValorPagar.Text = "$ " + valorPagar;
        right_cell2.Controls.Add(lblValValorPagar);
        row2.Cells.Add(right_cell2);
        tblDynamic.Rows.Add(row2);

    }

    protected void ddlTramite_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarRequisitosTramites(this.ddlTramite.SelectedValue);
        Session["codTramite"] = this.ddlTramite.SelectedValue;
        if (this.ddlTramite.SelectedValue != null && this.ddlTramite.SelectedValue != "")
            SetLocationMessageByArea(Request.QueryString["codCatTramite"]);
        else
        {
            SetLocationMessageByArea("");
            this.btnGenerarCEP.Visible = false;
        }
    }

    protected void CargarRequisitosTramites(string codTramite)
    {
        try
        {
            Constantes.Tramites oTramite = new Constantes.Tramites(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            tramiteRequisitos = oTramite.GetRequisitosPorTramiteFromDB(codTramite);
        }
        catch (Exception ex)
        {
            tramiteRequisitos = null;
        }
        CreateControls();
        this.btnGenerarCEP.Visible = true;
    }



    #region "Carga Dropdownlist, radio buttons, etc."
    

    #endregion

    protected void ValidarRequerimientos()
    {
        if (VerifyValues())
        {
            this.divDdlTramite.Visible = false;
            this.divBack.Visible = false;
            this.lblMessage.Text = "";
            this.btnGenerarCEP.Visible = false;

            ComprobanteElectronicoPago.CEP oCEP = new ComprobanteElectronicoPago.CEP(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

            //llamar a stored procedure de validación de datos
            if (oCEP.SolicitaCEP(Session["codTramite"].ToString(), User.Identity.Name.ToString(), trama_requisitos))
            {
                this.lblMessage2.Text = "¿Está seguro de que desea solicitar el trámite?";
                this.lblMessage2.CssClass = "h1";
                this.btnAceptar.Visible = true;
                this.btnCancelar.Visible = true;
                CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
                objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                Session["tramaSolicTramite"] = objCrypto.CifrarCadena(oCEP.CodSolicitud);
                Session["valorCEP"] = objCrypto.CifrarCadena(oCEP.ValorPago);
                PrintDataConfirmation(oCEP.ValorPago);
            }
            else
            {
                //this.lblError.Text = "Datos incorrectos.  <br /><br />";
                this.lblError.Text = oCEP.Error;
                this.btnCorregir.Visible = true;
                this.divError.Visible = true;
            }

        }
        else
        {
            CargarRequisitosTramites(Session["codTramite"].ToString());
        }
    }

    // validar los requerimientos
    protected void btnGenerarCEP_Click(object sender, ImageClickEventArgs e)
    {
        ValidarRequerimientos();
    }

    protected void BackToForm()
    {
        this.divDdlTramite.Visible = true;
        this.divBack.Visible = true;
        CargarRequisitosTramites(Session["codTramite"].ToString());
        this.btnGenerarCEP.Visible = true;
        this.btnAceptar.Visible = false;
        this.btnCancelar.Visible = false;
        this.btnCorregir.Visible = false;
        this.divError.Visible = false;
        this.lblMessage2.Text = "";
    }

    protected void ClearErrorMessages()
    {
        this.lblMessage.Text = "";
    }

    protected bool VerifyValues()
    {
        trama_requisitos = "";
        string controlName = "";
        for (int i = 0; i < tramiteRequisitos.Length; i++)
        {
            if (tramiteRequisitos[i]["lista"].ToString() == "N" || tramiteRequisitos[i]["lista"].ToString() == "D")
            {
                controlName = "txt" + tramiteRequisitos[i]["item"].ToString();
            }
            else
            {
                controlName = "ddl" + tramiteRequisitos[i]["item"].ToString();
            }
            if (Request.Form[controlName] != "")
            {
                trama_requisitos += Session["codTramite"].ToString() + "!";
                trama_requisitos += tramiteRequisitos[i]["id_etapa_proceso"].ToString() + "!";
                trama_requisitos += tramiteRequisitos[i]["item"].ToString() + "!";
                trama_requisitos += Request.Form[controlName].ToUpper() + "!=";
            }
            else
            {
                this.lblMessage.Text = "Error: Debe de ingresar " + tramiteRequisitos[i]["etiqueta"].ToString();
                return false;
            }
        }


        return true;
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Session["tramaSolicTramite"] = null;
        BackToForm();
    }
    protected void btnCorregir_Click(object sender, EventArgs e)
    {
        BackToForm();
    }
    protected void btnAceptar_Click(object sender, EventArgs e)
    {
        Response.Redirect("CEP.aspx");
    }
}
