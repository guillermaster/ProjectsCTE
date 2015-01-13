using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

public partial class DataUserCTG : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session[Constantes.UsuarioWeb.SessionVarNameFullAccess].ToString() == Constantes.UsuarioWeb.SessionLimitedAccess)
            {
                //Response.Redirect("~/sinPermiso.aspx");
                HtmlWriter.Messages.ShowMainContentError(this.Master, this.divMain, "Usted no tiene permiso para acceder a esta página. Para poder acceder a este servicio debe validar su usuario, debe acercarse a las oficinas del departamento de <b>Atención al Usuario</b>.");
            }
        }
        catch
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        if (!IsPostBack)
        {
            CargarPaises();
            SetFieldsValues(HttpContext.Current.User.Identity.Name);
        }
    }


    protected void SetFieldsValues(string identificacion)
    {
        Usuario.DatosUsuario oDatosUsuarios = new Usuario.DatosUsuario(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        string[] oDatos = oDatosUsuarios.ObtenerDatosPersonales(identificacion);

        if (oDatos.GetLength(0) > 0)
        {
            if (oDatos[0] != "error")
            {
                if (oDatos[25] == "S")
                {
                    txtIdentificacion.Text = oDatos[0];
                    txtNombres.Text = oDatos[1];
                    txtApellidos.Text = oDatos[2] + " " + oDatos[3];
                    txtFechaNac.Text = oDatos[4];
                    txtCantonNac.Text = oDatos[5];
                    txtProvinciaNac.Text = oDatos[6];
                    txtPaisNac.Text = oDatos[7];
                    txtSexo.Text = oDatos[8];
                    txtTipoSangre.Text = oDatos[9];
                    txtEstatura.Text = oDatos[10];
                    txtEstadoCivil.Text = oDatos[11];
                    txtProfesion.Text = oDatos[12];
                    txtDireccion.Text = oDatos[20];
                    txtTelefono.Text = oDatos[21];
                    txtCelular.Text = oDatos[27];
                    txtCalle1.Text = oDatos[13];
                    hdnCalle1.Value = txtCalle1.Text;
                    txtCalle2.Text = oDatos[14];
                    hdnCalle2.Value = txtCalle2.Text;
                    txtNumVilla.Text = oDatos[15];
                    hdnNumVilla.Value = txtNumVilla.Text;
                    txtManzana.Text = oDatos[16];
                    hdnNumVilla.Value = txtNumVilla.Text;
                    txtPiso.Text = oDatos[17];
                    hdnPiso.Value = txtPiso.Text;
                    txtDpto.Text = oDatos[18];
                    hdnDpto.Value = txtDpto.Text;

                    try
                    {
                        ddlPais.ClearSelection();
                        ddlPais.Items.FindByText(oDatos[24]).Selected = true;
                        txtPaisRes.Text = ddlPais.SelectedItem.Text;
                    }
                    catch { }

                    try
                    {
                        CargarProvincias(ddlPais.Items.FindByText(oDatos[24]).Value);
                        ddlProvincia.Items.FindByText(oDatos[23]).Selected = true;
                        txtProvRes.Text = ddlProvincia.SelectedItem.Text;
                    }
                    catch { }

                    try
                    {
                        CargarCiudades(ddlProvincia.Items.FindByText(oDatos[23]).Value);
                        ddlCanton.Items.FindByText(oDatos[22]).Selected = true;
                        txtCantonRes.Text = ddlCanton.SelectedItem.Text;
                    }
                    catch { }

                    try
                    {
                        CargarCiudadelas(ddlCanton.Items.FindByText(oDatos[22]).Value);
                        ddlCiudadela.Items.FindByText(oDatos[19]).Selected = true;
                        hdnCiudadela.Value = oDatos[19];
                    }
                    catch { }
                }
            }
            else
                HtmlWriter.Messages.ShowMainContentError(Master, divMain, "Usted no registra contrato en la CTE");
        }
        else
            HtmlWriter.Messages.ShowMainContentError(Master, divMain, oDatos[1]);
    }

    protected void CargarPaises()
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView dvPaises = geog.GetPaisesConCodigos().DefaultView;
        dvPaises.Sort = string.Format("{0} {1}", dvPaises.Table.Columns[1].ColumnName, "ASC");

        DataRow nullrow = dvPaises.Table.NewRow();
        nullrow[0] = null;
        nullrow[1] = null;

        dvPaises.Table.Rows.InsertAt(nullrow, 0);

        ddlPais.DataSource = dvPaises;
        ddlPais.DataValueField = dvPaises.Table.Columns[0].ColumnName;
        ddlPais.DataTextField = dvPaises.Table.Columns[1].ColumnName;
        ddlPais.DataBind();
    }

    protected void CargarProvincias(string codPais)
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView dvProvincias = geog.GetProvinciasConCodigos(codPais).DefaultView;
        dvProvincias.Sort = string.Format("{0} {1}", dvProvincias.Table.Columns[1].ColumnName, "ASC");

        DataRow nullrow = dvProvincias.Table.NewRow();
        nullrow[0] = null;
        nullrow[1] = null;

        dvProvincias.Table.Rows.InsertAt(nullrow, 0);

        ddlProvincia.DataSource = dvProvincias;
        ddlProvincia.DataValueField = dvProvincias.Table.Columns[0].ColumnName;
        ddlProvincia.DataTextField = dvProvincias.Table.Columns[1].ColumnName;
        ddlProvincia.DataBind();
    }

    protected void CargarCiudades(string codProvincia)
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView dvCiudades = geog.GetCiudadesConCodigos(codProvincia).DefaultView;
        dvCiudades.Sort = string.Format("{0} {1}", dvCiudades.Table.Columns[1].ColumnName, "ASC");

        DataRow nullrow = dvCiudades.Table.NewRow();
        nullrow[0] = null;
        nullrow[1] = null;

        dvCiudades.Table.Rows.InsertAt(nullrow, 0);
        ddlCanton.DataSource = dvCiudades;
        ddlCanton.DataValueField = dvCiudades.Table.Columns[0].ColumnName;
        ddlCanton.DataTextField = dvCiudades.Table.Columns[1].ColumnName;
        ddlCanton.DataBind();
    }

    protected void CargarCiudadelas(string codCanton)
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView dvCiudadelas = geog.GetCiudadelasConCodigos(codCanton).DefaultView;
        dvCiudadelas.Sort = string.Format("{0} {1}", dvCiudadelas.Table.Columns[1].ColumnName, "ASC");

        DataRow nullrow = dvCiudadelas.Table.NewRow();
        nullrow[0] = null;
        nullrow[1] = null;

        dvCiudadelas.Table.Rows.InsertAt(nullrow, 0);
        ddlCiudadela.DataSource = dvCiudadelas;
        ddlCiudadela.DataValueField = dvCiudadelas.Table.Columns[0].ColumnName;
        ddlCiudadela.DataTextField = dvCiudadelas.Table.Columns[1].ColumnName;
        ddlCiudadela.DataBind();
    }


    protected void btnEditDireccion_Click(object sender, ImageClickEventArgs e)
    {
        MPE.Show();        
    }
    protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
    {
        MPE.Show();
        CargarProvincias(ddlPais.SelectedValue);
        CargarCiudades(ddlProvincia.SelectedValue);
        CargarCiudadelas(ddlCanton.SelectedValue);
    }
    protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        MPE.Show();
        CargarCiudades(ddlProvincia.SelectedValue);
        CargarCiudadelas(ddlCanton.SelectedValue);
    }
    protected void ddlCanton_SelectedIndexChanged(object sender, EventArgs e)
    {
        MPE.Show();
        CargarCiudadelas(ddlCanton.SelectedValue);
    }


    protected void btnCancelChangAddress_Click(object sender, ImageClickEventArgs e)
    {
        txtCalle1.Text = hdnCalle1.Value;
        txtCalle2.Text = hdnCalle2.Value;
        txtNumVilla.Text = hdnNumVilla.Value;
        txtManzana.Text = hdnManzana.Value;
        txtPiso.Text = hdnPiso.Value;
        txtDpto.Text = hdnDpto.Value;

        //this.ddlPais.Items.FindByText(this.txtPaisRes.Text).Selected = true;
        
        CargarProvincias(ddlPais.Items.FindByText(txtPaisRes.Text).Value);
        ddlProvincia.Items.FindByText(txtProvRes.Text).Selected = true;

        CargarCiudades(ddlProvincia.Items.FindByText(txtProvRes.Text).Value);
        ddlCanton.Items.FindByText(txtCantonRes.Text).Selected = true;

        CargarCiudadelas(ddlCanton.Items.FindByText(txtCantonRes.Text).Value);
        ddlCiudadela.Items.FindByText(hdnCiudadela.Value).Selected = true;
    }


    protected void btnEditTelMov_Click1(object sender, ImageClickEventArgs e)
    {
        btnEditTelMov.Visible = false;
        btnSaveTelMov.Visible = true;
        btnCancelTelmov.Visible = true;
        txtCelular.ReadOnly = false;
        hdnTelefonoMovil.Value = txtCelular.Text;
    }
    protected void btnEditTelefono_Click(object sender, ImageClickEventArgs e)
    {
        btnEditTelefono.Visible = false;
        btnSaveTelefono.Visible = true;
        btnCancelTelefono.Visible = true;
        txtTelefono.ReadOnly = false;
        hdnTelefono.Value = txtTelefono.Text;
    }
    protected void btnCancelTelmov_Click(object sender, ImageClickEventArgs e)
    {
        HideSaveMobPhoneButtons();
        txtCelular.Text = hdnTelefonoMovil.Value;
    }
    protected void btnSaveTelMov_Click1(object sender, ImageClickEventArgs e)
    {
        HideSaveMobPhoneButtons();
        string message;
        if (SaveMobilePhone(out message))
            HtmlWriter.Messages.ShowModalInfoMessage(Master.MasterUpdatePanel, Master.GetType(), "Se ha actualizado correctamente el teléfono móvil. Los cambios efectuados se verán reflejados aproximádamente en 5 minutos.");
        else
        {
            txtCelular.Text = hdnTelefonoMovil.Value;
            HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), "Ha ocurrido un error al actualizar el teléfono móvil. " + message);
        }
    }
    protected void btnCancelTelefono_Click(object sender, ImageClickEventArgs e)
    {
        HideSavePhoneButtons();
        txtTelefono.Text = hdnTelefono.Value;
    }
    protected void btnSaveTelefono_Click(object sender, ImageClickEventArgs e)
    {
        HideSavePhoneButtons();
        string message;
        if (SavePhone(out message))
            HtmlWriter.Messages.ShowModalInfoMessage(Master.MasterUpdatePanel, Master.GetType(), "Se ha actualizado correctamente el teléfono. Los cambios efectuados se verán reflejados aproximádamente en 5 minutos.");
        else
        {
            txtTelefono.Text = hdnTelefono.Value;
            HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), "Ha ocurrido un error al actualizar el teléfono. " + message);
        }
    }

    protected void HideSaveMobPhoneButtons()
    {
        btnEditTelMov.Visible = true;
        btnSaveTelMov.Visible = false;
        btnCancelTelmov.Visible = false;
        txtCelular.ReadOnly = true;
    }

    protected void HideSavePhoneButtons()
    {
        btnEditTelefono.Visible = true;
        btnSaveTelefono.Visible = false;
        btnCancelTelefono.Visible = false;
        txtTelefono.ReadOnly = true;
    }


    protected void btnSaveChangeAddress_Click(object sender, ImageClickEventArgs e)
    {
        string msg1;
        if (SaveGeographicData(out msg1))
            if (SaveAddress(out msg1))
                HtmlWriter.Messages.ShowModalInfoMessage(Master.MasterUpdatePanel, Master.GetType(), "Los datos de residencia han sido guardados. Los cambios efectuados se verán reflejados aproximádamente en 5 minutos.");
            else
                HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), msg1);
        else
            HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), msg1);
    }


    #region Metodos de transacciones con base de datos
    protected bool SaveGeographicData(out string msg)
    {
        msg = string.Empty;
        Usuario.DatosUsuario oDatosUsuario = new Usuario.DatosUsuario(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        
        bool resultado = oDatosUsuario.ActualizaPais(txtIdentificacion.Text, ddlPais.SelectedValue, ddlProvincia.SelectedValue, ddlCanton.SelectedValue, ddlCiudadela.SelectedValue, 
            ddlPais.Items.FindByText(txtPaisRes.Text).Value, 
            ddlProvincia.Items.FindByText(txtProvRes.Text).Value,
            ddlCanton.Items.FindByText(txtCantonRes.Text).Value,
            ddlCiudadela.Items.FindByText(hdnCiudadela.Value).Value);
        if (resultado)
            return true;
        else
        {
            msg = oDatosUsuario.Error;
            return false;
        }
    }

    protected bool SaveAddress(out string msg)
    {
        if (UpdateUserData("CALLE1", txtCalle1.Text, hdnCalle1.Value, out msg))
            if (UpdateUserData("CALLE2", txtCalle2.Text, hdnCalle2.Value, out msg))
                if (UpdateUserData("NUMERO", txtNumVilla.Text, hdnNumVilla.Value, out msg))
                    if (UpdateUserData("MANZANA", txtManzana.Text, hdnManzana.Value, out msg))
                        if (UpdateUserData("PISO", txtPiso.Text, hdnPiso.Value, out msg))
                            if (UpdateUserData("DEPARTAMENTO", txtDpto.Text, hdnDpto.Value, out msg))
                                return true;
                            else
                            {
                                msg = "No se pudo guardar el dato correspondiente a departamento";
                                return false;
                            }
                        else
                        {
                            msg = "No se pudieron guardar los datos correspondientes a piso y departamento";
                            return false;
                        }
                    else
                    {
                        msg = "No se pudieron guardar los datos correspondientes a manzana, piso y departamento";
                        return false;
                    }
                else
                {
                    msg = "No se pudieron guardar los datos correspondientes a número o villa, manzana, piso y departamento";
                    return false;
                }
            else
            {
                msg = "No se pudieron guardar los datos correspondientes a calle 2, número o villa, manzana, piso y departamento";
                return false;
            }
        else
        {
            msg = "No se pudieron guardar los datos correspondientes a la dirección de residencia";
            return false;
        }
    }


    protected bool SaveMobilePhone(out string msg)
    {
        return UpdateUserData("TELEX", txtCelular.Text, hdnTelefonoMovil.Value, out msg);
    }

    protected bool SavePhone(out string msg)
    {
        return UpdateUserData("TELEFONO1", txtTelefono.Text, hdnTelefono.Value, out msg);
    }


    protected bool UpdateUserData(string nombreCampo, string newValue, string previousValue, out string msg)
    {
        Usuario.DatosUsuario oDatosUsuario = new Usuario.DatosUsuario(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        if (oDatosUsuario.ActualizaDato(txtIdentificacion.Text, nombreCampo, newValue, previousValue))
        {
            msg = Constantes.MensajesUsuarios.actDatosSuccesful;
            return true;
        }
        else
        {
            msg = oDatosUsuario.Error;
            return false;
        }
    }

    #endregion


    #region Validaciones
    protected bool DropdownHasValueSelected(DropDownList dropDownList)
    {
        if (string.IsNullOrWhiteSpace(dropDownList.SelectedValue))
        {
            if (dropDownList.Items.Count == 0)
                return true;
            else
                return false;
        }
        else
            return true;
    }
    #endregion




    
}