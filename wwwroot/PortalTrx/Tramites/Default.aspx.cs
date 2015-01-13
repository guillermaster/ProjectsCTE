using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

public partial class _Default : Page
{
    private string _validationGroupPrefix;

    protected void Page_Load(object sender, EventArgs e)
    {
        _validationGroupPrefix = "valGroup";
        if (!IsPostBack)
        {
            PopulateAllDropDownLists();
        }
    }


    protected void PopulateAllDropDownLists()
    {
        PopulateTramiteDropDownList(Constantes.Tramites.CodigosAreas.AtencionUsuario, ddlAccPane1.ID);
        PopulateTramiteDropDownList(Constantes.Tramites.CodigosAreas.Brevetacion, ddlAccPane2.ID);
        PopulateTramiteDropDownList(Constantes.Tramites.CodigosAreas.CitacionesPartes, ddlAccPane3.ID);
        PopulateTramiteDropDownList(Constantes.Tramites.CodigosAreas.Matriculacion, ddlAccPane4.ID);
    }


    protected void PopulateTramiteDropDownList(string codArea, string dropDownListId)
    {
        Constantes.Tramites oTramite = new Constantes.Tramites(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        //DropDownList ddlTramite = FindControlRecursive(Form, dropDownListID) as DropDownList;
        //ddlTramite.AutoPostBack = true;
        OptionDropDownList.OptionGroupSelect ddlTramite = FindControlRecursive(Form, dropDownListId) as OptionDropDownList.OptionGroupSelect;

        DataTable dtTramites = oTramite.GetTramitesFromDB(codArea);

        foreach (DataRow dr in dtTramites.Rows)
        {
            /*ListItem item = new ListItem(dr[Constantes.Tramites.fieldNameGenericName].ToString(), dr[Constantes.Tramites.fieldNameGenericCode].ToString());
            item.Attributes["OptionGroup"] = dr[Constantes.Tramites.fieldNameGenericDescription].ToString();
            ddlTramite.Items.Add(item);*/
            ddlTramite.Items.Add(new OptionDropDownList.OptionGroupItem(dr[Constantes.Tramites.FieldNameGenericCode].ToString(),
                dr[Constantes.Tramites.FieldNameGenericName].ToString(), dr[Constantes.Tramites.FieldNameGenericDescription].ToString()));
        }
        /*
        ddlTramite.DataTextField = Constantes.Tramites.fieldNameGenericName;
        ddlTramite.DataValueField = Constantes.Tramites.fieldNameGenericCode;
        DataView dvTramites = oTramite.GetTramitesFromDB(codArea).DefaultView;
        dvTramites.Sort = Constantes.Tramites.fieldNameGenericName + " ASC";
        ddlTramite.DataSource = dvTramites;
        ddlTramite.DataBind();*/
    }


    private Control FindControlRecursive(Control rootControl, string controlId)
    {
        if (rootControl.ID == controlId) return rootControl;

        foreach (Control controlToSearch in rootControl.Controls)
        {
            Control controlToReturn =
                FindControlRecursive(controlToSearch, controlId);
            if (controlToReturn != null) return controlToReturn;
        }
        return null;
    }


    protected void ddlTramite_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideConfirmYesNoButtons();
        OptionDropDownList.OptionGroupSelect ddlSelected = sender as OptionDropDownList.OptionGroupSelect;
        lblSelectedDropDownListText.Text = ddlSelected.SelectedItem.Text;
        foreach (Control ctrl in ddlSelected.Parent.Controls)
        {
            if (ctrl is Panel)
            {
                HtmlTable table = TableRequisitosTramites(ddlSelected.SelectedValue, Accordion1.SelectedIndex.ToString());
                if (table.Rows.Count > 0)
                    ctrl.Controls.Add(table);
                else
                    break;
            }
            if (ctrl is Button)
                ctrl.Visible = true;
        }
    }


    protected HtmlTable TableRequisitosTramites(string codTramite, string paneIndex)
    {
        HtmlTable tblRequisitos;
        try
        {
            Constantes.Tramites oTramite = new Constantes.Tramites(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
            tblRequisitos = CreateControls(oTramite.GetRequisitosPorTramiteFromDB(codTramite), codTramite, paneIndex);
            if (!string.IsNullOrWhiteSpace(oTramite.Error))
                HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), oTramite.Error);
        }
        catch (Exception)
        {
            //tramiteRequisitos = null;
            tblRequisitos = new HtmlTable();
        }
        return tblRequisitos;
    }



    protected HtmlTable CreateControls(DataRow[] tramiteRequisitos, string codTramite, string paneIndex)
    {
        HtmlTable tblDynamic = new HtmlTable();
        foreach (DataRow t in tramiteRequisitos)
        {
            HtmlTableRow row = new HtmlTableRow();

            HtmlTableCell leftCell = new HtmlTableCell();
            Label label = new Label();
            label.ID = "lbl" + t["item"].ToString() + paneIndex;
            label.Text = t["etiqueta"].ToString() + ": ";
            leftCell.Controls.Add(label);
            row.Cells.Add(leftCell);

            HtmlTableCell rightCell = new HtmlTableCell();
            if (string.IsNullOrWhiteSpace(t["sentencia"].ToString()) && (t["lista"].ToString() == "N" || t["lista"].ToString() == "D"))//si es textbox
            {
                TextBox textbox = new TextBox();
                textbox.ID = "txt" + t["item"].ToString() + paneIndex;
                textbox.Text = "";
                if (t["tipo_dato"].ToString() == "F")//tipo fecha
                {
                    rightCell.Controls.Add(textbox);
                    CalendarExtender calendar = new CalendarExtender();
                    calendar.ID = "cal" + codTramite + t["item"].ToString();
                    calendar.TargetControlID = textbox.ID;
                    rightCell.Controls.Add(calendar);
                }
                else //textbox simple
                {
                    textbox.MaxLength = int.Parse(t["longitud"].ToString());
                    textbox.Width = int.Parse(t["ancho"].ToString());
                    if (int.Parse(t["alto"].ToString()) > 1)
                    {
                        textbox.TextMode = TextBoxMode.MultiLine;
                        textbox.Rows = int.Parse(t["alto"].ToString());
                    }

                    if (t["entrada"].ToString() == Constantes.Tramites.DBentryNameForUserId)
                    {
                        textbox.Text = User.Identity.Name;
                        textbox.ReadOnly = true;
                    }

                    rightCell.Controls.Add(textbox);
                }

                #region Required Validator
                if (t["requerido"].ToString() == "S")
                {
                    RequiredFieldValidator reqVal = new RequiredFieldValidator();
                    reqVal.ID = "rv" + t["item"].ToString() + paneIndex;
                    reqVal.ControlToValidate = textbox.ID;
                    reqVal.ErrorMessage = "*";
                    reqVal.ValidationGroup = this._validationGroupPrefix + paneIndex;
                    rightCell.Controls.Add(reqVal);
                }
                #endregion
                #region Custom Validator
                if (t["item"].ToString() == Constantes.Tramites.CodigosRequisitos.Placa)
                {
                    RegularExpressionValidator regExpVal = new RegularExpressionValidator();
                    regExpVal.ID = "regEx" + t["item"].ToString() + paneIndex;
                    regExpVal.ControlToValidate = textbox.ID;
                    regExpVal.ErrorMessage = "La placa ingresada es incorrecta";
                    regExpVal.ValidationGroup = this._validationGroupPrefix + paneIndex;
                    regExpVal.ValidationExpression = "[a-zA-Z]{3,3}[0-9]{4,4}";
                    rightCell.Controls.Add(regExpVal);
                }
                #endregion
            }
            else//no es textbox  - es dropdownlist
            {
                if (t["lista"].ToString() == "D")
                {//esto se realiza si el tipo de campo viene como textbox de la base pero en realidad es dropdownlist xq trae de la base valor en campo sentencia
                    t["lista"] = "S";
                }
                DropDownList list = new DropDownList();
                list.ID = "ddl" + t["item"].ToString() + paneIndex;
                list.Width = int.Parse(t["ancho"].ToString());
                //list.CssClass = "combo3";
                // cargar lista de valores
                #region "Cargar lista de valores"
                Constantes.Tramites oTramite = new Constantes.Tramites(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                                                       ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                                                       ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
                list.DataTextField = Constantes.Tramites.FieldNameGenericName;
                list.DataValueField = Constantes.Tramites.FieldNameGenericCode;
                list.DataSource = oTramite.GetRequisitoValuesList(t["lista"].ToString(), codTramite, t["id_etapa_proceso"].ToString(), t["item"].ToString(), t["sentencia"].ToString());
                list.DataBind();
                #endregion
                //
                rightCell.Controls.Add(list);

                if (t["requerido"].ToString() == "S")
                {
                    RequiredFieldValidator reqVal = new RequiredFieldValidator();
                    reqVal.ID = "rv" + codTramite + t["item"].ToString() + paneIndex;
                    reqVal.ControlToValidate = list.ID;
                    reqVal.ErrorMessage = "*";
                    reqVal.ValidationGroup = _validationGroupPrefix + paneIndex;
                    rightCell.Controls.Add(reqVal);
                }
            }
            row.Cells.Add(rightCell);

            tblDynamic.Rows.Add(row);
        }

        return tblDynamic;
    }



    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Button btn = sender as Button;
        string codTramite = string.Empty;
        #region get codTramite value
        foreach (Control ctrl in btn.Parent.Controls)
        {
            if (ctrl is OptionDropDownList.OptionGroupSelect)
            {
                OptionDropDownList.OptionGroupSelect ddl = ctrl as OptionDropDownList.OptionGroupSelect;
                codTramite = ddl.SelectedValue;
                break;
            }
        }
        #endregion
        Constantes.Tramites oTramite = new Constantes.Tramites(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataRow[] reqTramite = oTramite.GetRequisitosPorTramiteFromDB(codTramite);
        string tramaRequisitos;

        if (VerifyValues(reqTramite, codTramite, Accordion1.SelectedIndex.ToString(), btn.Parent.ClientID, out tramaRequisitos))
        {
            ComprobanteElectronicoPago.CEP oCEP = new ComprobanteElectronicoPago.CEP(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

            if (oCEP.SolicitaCEP(codTramite, User.Identity.Name, tramaRequisitos))
            {
                //pnlSidebarTitle.Visible = true;
                pnlSidebar.Visible = true;
                pnlConfirmar.Controls.Add(PrintDataConfirmation(oCEP.ValorPago, reqTramite, Accordion1.SelectedIndex.ToString(), btn.Parent.ClientID));
                SaveNumValueSolicitudEnc(oCEP.CodSolicitud, oCEP.ValorPago, codTramite);
                btnSi.Visible = true;
                btnNo.Visible = true;
                btn.Visible = false;
            }
            else
                HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), oCEP.Error);
        }
    }


    protected bool VerifyValues(DataRow[] tramiteRequisitos, string codTramite, string tabIndex, string accordionPanelClientId, out string tramaRequisitos)
    {
        tramaRequisitos = string.Empty;
        int emptyFieldCounter = 0;
        foreach (DataRow t in tramiteRequisitos)
        {
            string controlName = string.IsNullOrWhiteSpace(t["sentencia"].ToString()) &&
                                 (t["lista"].ToString() == "N" || t["lista"].ToString() == "D")
                                     ? "txt" + t["item"].ToString() + tabIndex
                                     : "ddl" + t["item"].ToString() + tabIndex;
            string formClientId = GetFormattedClientID(accordionPanelClientId + "$" + controlName);
            
            if (!string.IsNullOrWhiteSpace(Request.Form[formClientId]))
            {
                if (t["item"].ToString() == Constantes.Tramites.CodigosRequisitos.Placa)
                {
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[a-zA-Z]{3,3}[0-9]{4,4}");
                    if(!regex.IsMatch(Request.Form[formClientId]))
                    {
                        HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(),
                                                                "La placa ingresada es incorrecta, debe estar conformada por 3 letras seguido por 4 números (ej.: GAI0211)");
                        return false;
                    }
                }
                tramaRequisitos += codTramite + "!";
                tramaRequisitos += t["id_etapa_proceso"].ToString() + "!";
                tramaRequisitos += t["item"].ToString() + "!";
                tramaRequisitos += Request.Form[formClientId].ToUpper() + "!=";
            }
            else
            {
                if (codTramite != "ATW_IFVR" && codTramite != "ATW_CCDR" && codTramite != "ATW_CNC" && codTramite != "ATW_DDRV")// validar si es trámite Informe final de vehículo robado
                {
                    HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(),
                                                                "Debe ingresar todos los datos requeridos, por favor seleccione el trámite e ingrese sus requisitos nuevamente.");
                    return false;
                }
                else
                {
                    emptyFieldCounter++;
                    if(emptyFieldCounter>=2)//solo puede dejar 1 campo vacío
                    {
                        HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(),
                                                                "Debe ingresar la placa o el CAMV del vehículo, por favor seleccione el trámite e ingrese sus requisitos nuevamente.");
                        return false;
                    }
                    /*else
                    {
                        tramaRequisitos += codTramite + "!";
                        tramaRequisitos += t["id_etapa_proceso"].ToString() + "!";
                        tramaRequisitos += t["item"].ToString() + "!";
                        tramaRequisitos += string.Empty + "!=";
                    }*/
                }
            }
        }


        return true;
    }


    protected HtmlTable PrintDataConfirmation(string valorPagar, DataRow[] tramiteRequisitos, string tabIndex, string accordionPanelClientId)
    {
        HtmlTable tblDynamic = new HtmlTable();
        tblDynamic.CellPadding = 5;
        tblDynamic.CellSpacing = 5;
        for (int i = 0; i < tramiteRequisitos.Length; i++)
        {
            HtmlTableRow row = new HtmlTableRow();
            HtmlTableCell left_cell = new HtmlTableCell();
            left_cell.Attributes["style"] = "font-weight: bold; padding: 2px 20px 2px 0;";
            Label label = new Label();
            label.ID = "lblConf" + tramiteRequisitos[i]["item"].ToString();
            label.Text = tramiteRequisitos[i]["etiqueta"].ToString() + ": ";
            left_cell.Controls.Add(label);
            row.Cells.Add(left_cell);

            HtmlTableCell right_cell = new HtmlTableCell();

            string controlName = string.IsNullOrWhiteSpace(tramiteRequisitos[i]["sentencia"].ToString()) &&
                          (tramiteRequisitos[i]["lista"].ToString() == "N" ||
                           tramiteRequisitos[i]["lista"].ToString() == "D")
                              ? "txt" + tramiteRequisitos[i]["item"].ToString() + tabIndex
                              : "ddl" + tramiteRequisitos[i]["item"].ToString() + tabIndex;
            string formClientId = GetFormattedClientID(accordionPanelClientId + "$" + controlName);
            Label label2 = new Label();
            label2.ID = "lblValConf" + tramiteRequisitos[i]["item"].ToString();
            label2.Text = Request.Form[formClientId].ToUpper();
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

        return tblDynamic;
    }


    private bool SaveNumValueSolicitudEnc(string codSolicitud, string valorPagar, string codTramite)
    {
        try
        {
            CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
            objCrypto.Key = Constantes.ParametrosCifradoCs.key;
            objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
            Session[Constantes.WebApp.DatosSesion.CodigoSolicitudTramite.ToString()] = Utilities.Utils.EncodeToBase64(objCrypto.CifrarCadena(codSolicitud));
            Session[Constantes.WebApp.DatosSesion.ValorTramite.ToString()] = Utilities.Utils.EncodeToBase64(objCrypto.CifrarCadena(valorPagar));
            Session[Constantes.WebApp.DatosSesion.CodigoProceso.ToString()] = Utilities.Utils.EncodeToBase64(objCrypto.CifrarCadena(codTramite));
            return true;
        }
        catch
        {
            return false;
        }
    }


    private string GetFormattedClientID(string clientID)
    {
        string formattedClientID = string.Empty;
        int _counter = 0;

        foreach (char letter in clientID.ToCharArray())
        {
            if (letter.Equals('_'))
            {
                _counter++;
                if (_counter == 1 || _counter == 2)
                    formattedClientID += "$";
                else
                    formattedClientID += letter.ToString();
            }
            else
                formattedClientID += letter.ToString();
        }

        return formattedClientID;
    }

    protected void btnNo_Click(object sender, EventArgs e)
    {
        HideConfirmYesNoButtons();
        PopulateAllDropDownLists();
    }
    protected void btnSi_Click(object sender, EventArgs e)
    {
        Response.Redirect("ModEntrega.aspx", true);
        //Response.Redirect("CEP.aspx", true);
    }
    protected void HideConfirmYesNoButtons()
    {
        btnNo.Visible = false;
        btnSi.Visible = false;
    }
}
