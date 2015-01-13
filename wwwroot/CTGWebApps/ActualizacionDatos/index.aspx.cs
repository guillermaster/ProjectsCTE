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
using Usuario;

public partial class ActualizacionDatos_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Constantes.UsuarioWeb.SessionVarNameFullAccess].ToString() == Constantes.UsuarioWeb.SessionLimitedAccess)
        {
            Response.Redirect("~/sinPermiso.aspx");
        }

        if (!Page.IsPostBack)
        {
            this.fieldsChangePassword.Visible = false;
            this.editCalle1.setMaxLength(30);
            this.editCalle2.setMaxLength(30);
            this.editManzana.setMaxLength(5);
            this.editPiso.setMaxLength(5);
            this.editDepartamento.setMaxLength(5);
            this.editTelefono.setMaxLength(12);
            this.editTelefonoMovil.setMaxLength(12);
            CargarPaises();
            ConsultarDatos(User.Identity.Name.ToString());
            ConsultarDatosWeb(User.Identity.Name.ToString());
        }
    }


    protected void ConsultarDatosWeb(string identificacion)
    {
        Seguridad.UsuarioWeb oUsuarioWeb = new Seguridad.UsuarioWeb(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], identificacion);
        string[] datos_usuarioweb = new string[5];
        datos_usuarioweb = oUsuarioWeb.ObtenerDatosUsuarioWeb();

        if (datos_usuarioweb[0] != "error")
        {
            this.txtEmailVis.Text = datos_usuarioweb[2];
            this.hdnPassword.Value = oUsuarioWeb.GetEncPassword();
        }
    }

    protected void ConsultarDatos(string identificacion)
    {
        DatosUsuario oDatosUsuario = new DatosUsuario(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        string[] oDatos = oDatosUsuario.ObtenerDatosPersonales(identificacion);

        Int64 NumElem = oDatos.GetLength(0);

        if (NumElem > 0)
        {
            if (oDatos[0] != "error")
            {
                if (oDatos[25] == "S")
                {
                    this.txtLicencia.Text = oDatos[0];
                    this.txtNombres.Text = oDatos[1];
                    this.txtApellido1.Text = oDatos[2];
                    this.txtApellido2.Text = oDatos[3];
                    this.txtFechaNac.Text = oDatos[4];
                    this.txtCantonNac.Text = oDatos[5];
                    this.txtProvinciaNac.Text = oDatos[6];
                    this.txtPaisNac.Text = oDatos[7];
                    this.txtSexo.Text = oDatos[8];
                    this.txtTipoSangre.Text = oDatos[9];
                    this.txtEstatura.Text = oDatos[10];
                    this.txtEstadoCivil.Text = oDatos[11];
                    this.txtProfesion.Text = oDatos[12];
                    this.txtDireccion.Text = oDatos[20];
                    this.editCalle1.setValue(oDatos[13]);
                    this.editCalle1.setCampo("CALLE1");
                    this.editCalle1.setIdentificacion(identificacion);
                    this.editCalle2.setValue(oDatos[14]);
                    this.editCalle2.setCampo("CALLE2");
                    this.editCalle2.setIdentificacion(identificacion);
                    this.editNumeroVilla.setValue(oDatos[15]);
                    this.editNumeroVilla.setCampo("NUMERO");
                    this.editNumeroVilla.setIdentificacion(identificacion);
                    this.editManzana.setValue(oDatos[16]);
                    this.editManzana.setCampo("MANZANA");
                    this.editManzana.setIdentificacion(identificacion);
                    this.editPiso.setValue(oDatos[17]);
                    this.editPiso.setCampo("PISO");
                    this.editPiso.setIdentificacion(identificacion);
                    this.editDepartamento.setValue(oDatos[18]);
                    this.editDepartamento.setCampo("DEPARTAMENTO");
                    this.editDepartamento.setIdentificacion(identificacion);
                    
                    this.editTelefono.setValue(oDatos[21]);
                    this.editTelefono.setCampo("TELEFONO1");
                    this.editTelefono.setIdentificacion(identificacion);

                    this.editTelefonoMovil.setValue(oDatos[27]);
                    this.editTelefonoMovil.setCampo("TELEX");
                    this.editTelefonoMovil.setIdentificacion(identificacion);

                    this.hdnPreviousProvinciaVal.Value = oDatos[30];
                    this.hdnPreviousCantonVal.Value = oDatos[29];
                    this.hdnPreviousCiudadelaVal.Value = oDatos[28];

                    try
                    {
                        this.ddlPais.ClearSelection();
                        this.ddlPais.Items.FindByText(oDatos[24]).Selected = true;
                        this.hdnPreviousPaisVal.Value = this.ddlPais.SelectedValue;
                    }
                    catch (Exception ex)
                    {
                    }

                    try
                    {
                        CargarProvincias();
                        this.ddlProvincia.Items.FindByText(oDatos[23]).Selected = true;
                    }
                    catch (Exception ex)
                    {
                    }

                    try
                    {
                        CargarCiudades();
                        this.ddlCanton.Items.FindByText(oDatos[22]).Selected = true;
                    }
                    catch (Exception ex)
                    {
                    }

                    try
                    {
                        CargarCiudadelas();
                        this.ddlCiudadela.Items.FindByText(oDatos[19]).Selected = true;
                    }
                    catch (Exception ex)
                    {
                    }
                    
                    
                    this.UpdatePanel1.Visible = true;
                }
                else
                {
                    this.lblMensaje.Text = "Usted no registra contrato alguno con la CTG";
                    this.UpdatePanel1.Visible = false;
                }
            }
            else
            {
                this.lblMensaje.Text = oDatos[1];
                this.UpdatePanel1.Visible = false;
            }
        }
    }

    protected void ClearMessages()
    {
        this.lblMsgCurrPwd.Visible = false;
        this.lblMsgConfNewPwd.Visible = false;
        this.lblErrorEmailChanged.Text = "";
        this.lblErrorPwdChanged.Text = "";
        this.lblMsgEmailChanged.Text = "";
        this.lblMsgPwdChanged.Text = "";
        this.lblMsgPais.Text = "";
        this.lblMsgProvincia.Text = "";
        this.lblMsgCanton.Text = "";
        this.lblMsgCiudadela.Text = "";
        this.lblSuccessPais.Text = "";
        this.lblSuccessProvincia.Text = "";
        this.lblSuccessCanton.Text = "";
        this.lblSuccessCiudadela.Text = "";
        this.editCalle1.ClearMessages();
        this.editCalle2.ClearMessages();
        this.editDepartamento.ClearMessages();
        this.editManzana.ClearMessages();
        this.editNumeroVilla.ClearMessages();
        this.editPiso.ClearMessages();
        this.editTelefono.ClearMessages();
        this.editTelefonoMovil.ClearMessages();
    }

    protected void btnEditarCiudadela_Click(object sender, EventArgs e)
    {
        this.ddlCiudadela.Enabled = true;
        this.btnGuardarCiudadela.Visible = true;
        this.btnCancelarCiudadela.Visible = true;
        this.btnEditarCiudadela.Visible = false;
        ClearMessages();
        //GuardarValoresGeografActualesTemp();
    }


    protected void btnEditarCanton_Click(object sender, EventArgs e)
    {
        this.ddlCanton.Enabled = true;
        this.ddlCiudadela.Enabled = true;
        this.btnEditarCiudadela.Visible = false;
        this.btnEditarCanton.Visible = false;
        this.btnGuardarCiudadela.Visible = false;
        this.btnGuardarCanton.Visible = true;
        this.btnCancelarCiudadela.Visible = false;
        this.btnCancelarCanton.Visible = true;
        ClearMessages();
        //GuardarValoresGeografActualesTemp();
    }


    protected void btnEditarProvincia_Click(object sender, EventArgs e)
    {
        this.ddlProvincia.Enabled = true;
        this.ddlCanton.Enabled = true;
        this.ddlCiudadela.Enabled = true;
        this.btnEditarCiudadela.Visible = false;
        this.btnEditarCanton.Visible = false;
        this.btnEditarProvincia.Visible = false;
        this.btnGuardarCiudadela.Visible = false;
        this.btnGuardarCanton.Visible = false;
        this.btnGuardarProvincia.Visible = true;
        this.btnCancelarCiudadela.Visible = false;
        this.btnCancelarCanton.Visible = false;
        this.btnCancelarProvincia.Visible = true;
        ClearMessages();
        //GuardarValoresGeografActualesTemp();
    }


    protected void btnEditarPais_Click(object sender, EventArgs e)
    {
        this.ddlPais.Enabled = true;
        this.ddlProvincia.Enabled = true;
        this.ddlCanton.Enabled = true;
        this.ddlCiudadela.Enabled = true;
        this.btnEditarCiudadela.Visible = false;
        this.btnEditarCanton.Visible = false;
        this.btnEditarProvincia.Visible = false;
        this.btnEditarPais.Visible = false;
        this.btnGuardarCiudadela.Visible = false;
        this.btnGuardarCanton.Visible = false;
        this.btnGuardarProvincia.Visible = false;
        this.btnGuardarPais.Visible = true;
        this.btnCancelarCiudadela.Visible = false;
        this.btnCancelarCanton.Visible = false;
        this.btnCancelarProvincia.Visible = false;
        this.btnCancelarPais.Visible = true;
        ClearMessages();
        //GuardarValoresGeografActualesTemp();
    }


    protected void btnCancelarCiudadela_Click(object sender, EventArgs e)
    {
        this.ddlCiudadela.Enabled = false;
        this.btnGuardarCiudadela.Visible = false;
        this.btnCancelarCiudadela.Visible = false;
        this.btnEditarCiudadela.Visible = true;
        ClearMessages();
        RestaurarValoresGeografActualesTemp();
    }


    protected void btnCancelarCanton_Click(object sender, EventArgs e)
    {
        this.ddlCanton.Enabled = false;
        this.ddlCiudadela.Enabled = false;
        this.btnEditarCiudadela.Visible = true;
        this.btnEditarCanton.Visible = true;
        this.btnGuardarCanton.Visible = false;
        this.btnCancelarCanton.Visible = false;
        ClearMessages();
        RestaurarValoresGeografActualesTemp();
    }


    protected void btnCancelarProvincia_Click(object sender, EventArgs e)
    {
        this.ddlProvincia.Enabled = false;
        this.ddlCanton.Enabled = false;
        this.ddlCiudadela.Enabled = false;
        this.btnEditarCiudadela.Visible = true;
        this.btnEditarCanton.Visible = true;
        this.btnEditarProvincia.Visible = true;
        this.btnGuardarProvincia.Visible = false;
        this.btnCancelarProvincia.Visible = false;
        ClearMessages();
        RestaurarValoresGeografActualesTemp();
    }


    protected void btnCancelarPais_Click(object sender, EventArgs e)
    {
        this.ddlPais.Enabled = false;
        this.ddlProvincia.Enabled = false;
        this.ddlCanton.Enabled = false;
        this.ddlCiudadela.Enabled = false;
        this.btnEditarCiudadela.Visible = true;
        this.btnEditarCanton.Visible = true;
        this.btnEditarProvincia.Visible = true;
        this.btnEditarPais.Visible = true;
        this.btnGuardarPais.Visible = false;
        this.btnCancelarPais.Visible = false;
        ClearMessages();
        RestaurarValoresGeografActualesTemp();
    }


    protected void ddlCiudadela_SelectedIndexChanged(object sender, EventArgs e)
    {
        ClearMessages();
    }


    protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCiudades();
        CargarCiudadelas();
        ClearMessages();
    }

    protected void ddlCanton_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarCiudadelas();
        ClearMessages();
    }

    protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarProvincias();
        CargarCiudades();
        CargarCiudadelas();
        ClearMessages();
    }

    protected void CargarCiudadelas()
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataView dvCiudadelas = geog.GetCiudadelasConCodigos(this.ddlCanton.SelectedValue).DefaultView;
        dvCiudadelas.Sort = string.Format("{0} {1}", dvCiudadelas.Table.Columns[1].ColumnName, "ASC");

        DataRow nullrow = dvCiudadelas.Table.NewRow();
        nullrow[0] = null;
        nullrow[1] = null;

        dvCiudadelas.Table.Rows.InsertAt(nullrow, 0);
        this.ddlCiudadela.DataSource = dvCiudadelas;
        this.ddlCiudadela.DataValueField = dvCiudadelas.Table.Columns[0].ColumnName;
        this.ddlCiudadela.DataTextField = dvCiudadelas.Table.Columns[1].ColumnName;
        this.ddlCiudadela.DataBind();
    }

    protected void CargarCiudades()
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataView dvCiudades = geog.GetCiudadesConCodigos(this.ddlProvincia.SelectedValue).DefaultView;
        dvCiudades.Sort = string.Format("{0} {1}", dvCiudades.Table.Columns[1].ColumnName, "ASC");

        DataRow nullrow = dvCiudades.Table.NewRow();
        nullrow[0] = null;
        nullrow[1] = null;

        dvCiudades.Table.Rows.InsertAt(nullrow, 0);
        this.ddlCanton.DataSource = dvCiudades;
        this.ddlCanton.DataValueField = dvCiudades.Table.Columns[0].ColumnName;
        this.ddlCanton.DataTextField = dvCiudades.Table.Columns[1].ColumnName;
        this.ddlCanton.DataBind();
    }

    protected void CargarProvincias()
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataView dvProvincias = geog.GetProvinciasConCodigos(this.ddlPais.SelectedValue).DefaultView;
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

    protected void CargarPaises()
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataView dvPaises = geog.GetPaisesConCodigos().DefaultView;
        dvPaises.Sort = string.Format("{0} {1}", dvPaises.Table.Columns[1].ColumnName, "ASC");

        DataRow nullrow = dvPaises.Table.NewRow();
        nullrow[0] = null;
        nullrow[1] = null;

        dvPaises.Table.Rows.InsertAt(nullrow, 0);

        this.ddlPais.DataSource = dvPaises;
        this.ddlPais.DataValueField = dvPaises.Table.Columns[0].ColumnName;
        this.ddlPais.DataTextField = dvPaises.Table.Columns[1].ColumnName;
        this.ddlPais.DataBind();
    }


    protected void btnGuardarPais_Click(object sender, EventArgs e)
    {
        if (ValidaPais())
        {
            if (ValidaProvincia())
            {
                if (ValidaCanton())
                {
                    if (ValidaCiudadela())
                    {
                        /*** Guardar cambios  **/
                        if (GuardarPais())
                        {
                            this.ddlPais.Enabled = false;
                            this.ddlProvincia.Enabled = false;
                            this.ddlCanton.Enabled = false;
                            this.ddlCiudadela.Enabled = false;
                            this.btnGuardarPais.Visible = false;
                            this.btnCancelarPais.Visible = false;
                            this.btnEditarPais.Visible = true;
                            this.btnEditarProvincia.Visible = true;
                            this.btnEditarCanton.Visible = true;
                            this.btnEditarCiudadela.Visible = true;
                            ClearMessages();
                            GuardarValoresGeografActualesTemp();
                            this.lblSuccessPais.Text = Constantes.MensajesUsuarios.actDatosSuccesful;
                        }
                        
                    }
                    else
                    {
                        this.lblMsgPais.Text = "Debe de seleccionar la ciudadela";
                        this.lblMsgCiudadela.Text = "*";
                    }
                }
                else
                {
                    this.lblMsgPais.Text = "Debe de seleccionar el cantón";
                    this.lblMsgCanton.Text = "*";
                }
            }
            else
            {
                this.lblMsgPais.Text = "Debe de seleccionar la provincia";
                this.lblMsgProvincia.Text = "*";
            }
        }
        else
        {
            this.lblMsgPais.Text = "Debe de seleccionar el país";
        }
    }

    protected void btnGuardarProvincia_Click(object sender, EventArgs e)
    {
        if (ValidaProvincia())
        {
            if (ValidaCanton())
            {
                if (ValidaCiudadela())
                {
                    /*** Guardar cambios  **/
                    if (GuardarProvincia())
                    {
                        this.ddlProvincia.Enabled = false;
                        this.ddlCanton.Enabled = false;
                        this.ddlCiudadela.Enabled = false;
                        this.btnGuardarProvincia.Visible = false;
                        this.btnCancelarProvincia.Visible = false;
                        this.btnEditarProvincia.Visible = true;
                        this.btnEditarCanton.Visible = true;
                        this.btnEditarCiudadela.Visible = true;
                        ClearMessages();
                        GuardarValoresGeografActualesTemp();
                        this.lblSuccessProvincia.Text = Constantes.MensajesUsuarios.actDatosSuccesful;
                    }

                }
                else
                {
                    this.lblMsgProvincia.Text = "Debe de seleccionar la ciudadela";
                    this.lblMsgCiudadela.Text = "*";
                }
            }
            else
            {
                this.lblMsgProvincia.Text = "Debe de seleccionar el cantón";
                this.lblMsgCanton.Text = "*";
            }
        }
        else
        {
            if(this.ddlProvincia.Items.Count > 1)
                this.lblMsgProvincia.Text = "Debe de seleccionar la provincia";
        }
    }

    protected void btnGuardarCanton_Click(object sender, EventArgs e)
    {
        if (ValidaCanton())
        {
            if (ValidaCiudadela())
            {
                ClearMessages();
                /*** Guardar cambios  **/
                if (GuardarCanton())
                {
                    this.ddlCanton.Enabled = false;
                    this.ddlCiudadela.Enabled = false;
                    this.btnGuardarCanton.Visible = false;
                    this.btnCancelarCanton.Visible = false;
                    this.btnEditarCanton.Visible = true;
                    this.btnEditarCiudadela.Visible = true;
                    ClearMessages();
                    GuardarValoresGeografActualesTemp();
                    this.lblSuccessCanton.Text = Constantes.MensajesUsuarios.actDatosSuccesful;
                }

            }
            else
            {
                this.lblMsgCanton.Text = "Debe de seleccionar la ciudadela";
                this.lblMsgCiudadela.Text = "*";
            }
        }
        else
        {
            this.lblMsgCanton.Text = "Debe de seleccionar el cantón";
        }
    }


    protected void btnGuardarCiudadela_Click(object sender, EventArgs e)
    {
        if (ValidaCiudadela())
        {
            /*** Guardar cambios  **/
            if (GuardarCiudadela())
            {
                this.ddlCiudadela.Enabled = false;
                this.btnGuardarCiudadela.Visible = false;
                this.btnCancelarCiudadela.Visible = false;
                this.btnEditarCiudadela.Visible = true;
                ClearMessages();
                GuardarValoresGeografActualesTemp();
                this.lblSuccessCiudadela.Text = Constantes.MensajesUsuarios.actDatosSuccesful;
            }
        }
        else
        {
            this.lblMsgCiudadela.Text = "Debe de seleccionar la ciudadela";
        }
    }


    #region "Métodos UPDATES"
    protected bool GuardarPais()
    {
        DatosUsuario oDatosUsuario = new DatosUsuario(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        //bool resultado = oDatosUsuario.ActualizaPais(User.Identity.Name.ToString(), this.ddlPais.SelectedValue, this.ddlProvincia.SelectedValue, this.ddlCanton.SelectedValue, this.ddlCiudadela.SelectedValue, this.hdnPreviousPaisVal.Value, this.hdnPreviousProvinciaVal.Value, this.hdnPreviousCantonVal.Value, this.hdnPreviousCiudadelaVal.Value);
        bool resultado = oDatosUsuario.ActualizaPais(this.txtLicencia.Text, this.ddlPais.SelectedValue, this.ddlProvincia.SelectedValue, this.ddlCanton.SelectedValue, this.ddlCiudadela.SelectedValue, this.hdnPreviousPaisVal.Value, this.hdnPreviousProvinciaVal.Value, this.hdnPreviousCantonVal.Value, this.hdnPreviousCiudadelaVal.Value);
        if (!resultado)
        {
            this.lblMsgPais.Text = oDatosUsuario.Error;
        }
        return resultado;
    }
    protected bool GuardarProvincia()
    {
        DatosUsuario oDatosUsuario = new DatosUsuario(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        //bool resultado = oDatosUsuario.ActualizaProvincia(User.Identity.Name.ToString(), this.ddlProvincia.SelectedValue, this.ddlCanton.SelectedValue, this.ddlCiudadela.SelectedValue, this.hdnPreviousProvinciaVal.Value, this.hdnPreviousCantonVal.Value, this.hdnPreviousCiudadelaVal.Value);
        bool resultado = oDatosUsuario.ActualizaProvincia(this.txtLicencia.Text, this.ddlProvincia.SelectedValue, this.ddlCanton.SelectedValue, this.ddlCiudadela.SelectedValue, this.hdnPreviousProvinciaVal.Value, this.hdnPreviousCantonVal.Value, this.hdnPreviousCiudadelaVal.Value);
        if (!resultado)
        {
            this.lblMsgProvincia.Text = oDatosUsuario.Error;
        }
        return resultado;
    }
    protected bool GuardarCanton()
    {
        DatosUsuario oDatosUsuario = new DatosUsuario(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        //bool resultado = oDatosUsuario.ActualizaCanton(User.Identity.Name.ToString(), this.ddlCanton.SelectedValue, this.ddlCiudadela.SelectedValue, this.hdnPreviousCantonVal, this.hdnPreviousCiudadelaVal);
        bool resultado = oDatosUsuario.ActualizaCanton(this.txtLicencia.Text, this.ddlCanton.SelectedValue, this.ddlCiudadela.SelectedValue, this.hdnPreviousCantonVal.Value, this.hdnPreviousCiudadelaVal.Value);
        if (!resultado)
        {
            this.lblMsgCanton.Text = oDatosUsuario.Error;
        }
        return resultado;
    }
    protected bool GuardarCiudadela()
    {
        DatosUsuario oDatosUsuario = new DatosUsuario(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        //bool resultado = oDatosUsuario.ActualizaCiudadela(User.Identity.Name.ToString(), this.ddlCiudadela.SelectedValue, this.hdnPreviousCiudadelaVal.Value);
        bool resultado = oDatosUsuario.ActualizaCiudadela(this.txtLicencia.Text, this.ddlCiudadela.SelectedValue, this.hdnPreviousCiudadelaVal.Value);
        
        if (!resultado)
        {
            this.lblMsgCiudadela.Text = oDatosUsuario.Error;
        }
        return resultado;
    }
    #endregion


    protected void GuardarValoresGeografActualesTemp()
    {
        this.hdnPreviousPaisVal.Value = this.ddlPais.SelectedValue;
        this.hdnPreviousProvinciaVal.Value = this.ddlProvincia.SelectedValue;
        this.hdnPreviousCantonVal.Value = this.ddlCanton.SelectedValue;
        this.hdnPreviousCiudadelaVal.Value = this.ddlCiudadela.SelectedValue;
    }
    protected void RestaurarValoresGeografActualesTemp()
    {
        this.ddlPais.SelectedValue = this.hdnPreviousPaisVal.Value;
        CargarProvincias();
        this.ddlProvincia.SelectedValue = this.hdnPreviousProvinciaVal.Value;
        CargarCiudades();
        this.ddlCanton.SelectedValue = this.hdnPreviousCantonVal.Value;
        CargarCiudadelas();
        this.ddlCiudadela.SelectedValue = this.hdnPreviousCiudadelaVal.Value;
    }


    protected bool ValidaCiudadela()
    {
        bool res;

        if (this.ddlCiudadela.SelectedValue != "")
        {
            res = true;
        }
        else
        {
            if (this.ddlCiudadela.Items.Count <= 1)
            {
                res = true;
            }
            else
            {
                res = false;
            }
        }
        
        return res;
    }

    protected bool ValidaCanton()
    {
        bool res;

        if (this.ddlCanton.SelectedValue != "")
        {
            res = true;
        }
        else
        {
            if (this.ddlCanton.Items.Count <= 1)
            {
                res = true;
            }
            else
            {
                res = false;
            }
        }

        return res;
    }

    protected bool ValidaProvincia()
    {
        bool res;

        if (this.ddlProvincia.SelectedValue != "")
        {
            res = true;
        }
        else
        {
            if (this.ddlProvincia.Items.Count <= 1)
            {
                res = true;
            }
            else
            {
                res = false;
            }
        }

        return res;
    }

    protected bool ValidaPais()
    {
        bool res;

        if (this.ddlPais.SelectedValue != "")
        {
            res = true;
        }
        else
        {
            if (this.ddlPais.Items.Count <= 1)
            {
                res = true;
            }
            else
            {
                res = false;
            }
        }

        return res;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        ConsultarDatos(this.txtLicencia.Text);
    }
    protected void btnChangePwd_Click(object sender, EventArgs e)
    {
        this.fieldsChangePassword.Visible = true;
        this.btnChangePwd.Visible = false;
        Page.Form.DefaultButton = this.btnSavePwd.ID;
        Page.Form.DefaultFocus = this.txtCurrPwd.ID;
    }

    

    protected void btnSavePwd_Click(object sender, EventArgs e)
    {
        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        string curr_pwd = objCrypto.DescifrarCadena(this.hdnPassword.Value);

        if (this.txtCurrPwd.Text == curr_pwd)
        {
            this.lblMsgCurrPwd.Visible = false;
            if (this.txtNewPwd.Text == this.txtConfNewPwd.Text)
            {
                if (this.txtNewPwd.Text.Length >= 6)
                {
                    //System.Text.RegularExpressions.Regex objAlphaNumericPattern = new System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]");
                    //if (!objAlphaNumericPattern.IsMatch(this.txtNewPwd.Text))
                    {

                        this.lblMsgConfNewPwd.Visible = false;
                        this.lblErrorPwdChanged.Text = "";
                        Seguridad.UsuarioWeb oUsuarioWeb = new Seguridad.UsuarioWeb(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], this.txtLicencia.Text);
                        if (oUsuarioWeb.ActualizaDato(Constantes.UsuarioWeb.DBFieldNameContrasena, objCrypto.CifrarCadena(this.txtNewPwd.Text)))
                        {
                            this.lblMsgPwdChanged.Text = "Se ha modificado la contraseña correctamente";
                            this.hdnPassword.Value = objCrypto.CifrarCadena(this.txtNewPwd.Text);
                            this.fieldsChangePassword.Visible = false;
                            this.btnChangePwd.Visible = true;
                        }
                        else
                        {
                            this.lblErrorPwdChanged.Text = "Error al modificar contraseña";
                        }
                    }
                    //else
                    //{
                    //    this.lblErrorPwdChanged.Text = "La contraseña debe contener al menos un dígito numérico";
                    //}
                }
                else
                {
                    this.lblErrorPwdChanged.Text = "La contraseña debe ser de al menos 6 caracteres de longitud.";
                }
            }
            else
            {
                this.lblMsgConfNewPwd.Visible = true;
                this.lblErrorPwdChanged.Text = "La nueva contraseña y su confirmación no coinciden";
            }
        }
        else
        {
            this.lblMsgCurrPwd.Visible = true;
            this.lblErrorPwdChanged.Text = "La contraseña ingresada es incorrecta";
        }
    }
    protected void btnEditarEmail_Click(object sender, EventArgs e)
    {
        ClearMessages();
        this.txtEmailReal.Text = this.txtEmailVis.Text;
        this.txtEmailVis.Visible = false;
        this.txtEmailReal.Visible = true;
        this.btnEditarEmail.Visible = false;
        this.btnGuardarEmail.Visible = true;
        this.btnCancelarEmail.Visible = true;
    }
    
    
    protected void btnGuardarEmail_Click1(object sender, EventArgs e)
    {
        Seguridad.UsuarioWeb oUsuarioWeb = new Seguridad.UsuarioWeb(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], this.txtLicencia.Text);
        if (oUsuarioWeb.ActualizaDato(Constantes.UsuarioWeb.DBFieldNameEmail, this.txtEmailReal.Text))
        {
            this.txtEmailVis.Text = this.txtEmailReal.Text;
            this.txtEmailReal.Visible = false;
            this.txtEmailVis.Visible = true;
            this.btnEditarEmail.Visible = true;
            this.btnGuardarEmail.Visible = false;
            this.btnCancelarEmail.Visible = false;
            this.lblMsgEmailChanged.Text = "E-Mail actualizado correctamente";
            this.lblErrorEmailChanged.Text = "";
        }
        else
        {
            this.lblMsgEmailChanged.Text = "";
            this.lblErrorEmailChanged.Text = "Error al actualizar E-Mail";
        }
    }
    protected void btnCancelarEmail_Click1(object sender, EventArgs e)
    {
        this.txtEmailVis.Visible = true;
        this.txtEmailReal.Visible = false;
        this.btnEditarEmail.Visible = true;
        this.btnGuardarEmail.Visible = false;
        this.btnCancelarEmail.Visible = false;
    }
    protected void btnCancelPwd_Click(object sender, EventArgs e)
    {
        this.fieldsChangePassword.Visible = false;
        this.btnChangePwd.Visible = true;
        ClearMessages();
    }
}
