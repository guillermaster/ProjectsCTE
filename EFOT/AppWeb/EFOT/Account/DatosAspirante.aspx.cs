using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class DatosAspirante : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Accordion1.SelectedIndex = 0;
            LoadCodigoAspirante();
            LoadDatosBasicos();
            LoadDirecciones();
            LoadDdlTiposEstudios();
            LoadPaises(ddlPaisEducac);
            LoadEstudios();
            LoadDdlTiposReferencias();
            LoadPaises(ddlPaisReferencia);
            LoadReferencias();
            LoadDdlTiposUbicaciones();
            LoadPaises(ddlPaisDireccion);
            LoadPruebasAdmision();
        }
    }

    protected void LoadCodigoAspirante()
    {
        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        if (oEFOT.LoadCodigoAspirante(HttpContext.Current.User.Identity.Name))
        {
            Session[EFOTclass.Parametros.Session.CodigoAspirante] = oEFOT.DatosAspirante.CodigoAspirante;
        }
    }

    protected void LoadDatosBasicos()
    {
        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        if(oEFOT.LoadDatosAspirante(Session[EFOTclass.Parametros.Session.CodigoAspirante].ToString()))
        {
            lblValNombres.Text = oEFOT.DatosAspirante.Nombres;
            lblValApellidos.Text = oEFOT.DatosAspirante.Apellidos;
            lblValCargFam.Text = oEFOT.DatosAspirante.CargasFamiliares;
            lblValCedula.Text = oEFOT.DatosAspirante.Identificacion;
            lblValCiudadNac.Text = oEFOT.DatosAspirante.CiudadNac;
            lblValEmail.Text = oEFOT.DatosAspirante.Email;
            lblValEstadoCivil.Text = oEFOT.DatosAspirante.EstadoCivil;
            lblValEstatura.Text = oEFOT.DatosAspirante.Estatura;
            lblValFechaNac.Text = oEFOT.DatosAspirante.FechaNac;
            lblValPaisNac.Text = oEFOT.DatosAspirante.PaisNac;
            lblValPeso.Text = oEFOT.DatosAspirante.Peso;
            lblValProvNac.Text = oEFOT.DatosAspirante.ProvinciaNac;
            lblValSexo.Text = oEFOT.DatosAspirante.Sexo;
            lblValTallaCalzado.Text = oEFOT.DatosAspirante.TallaCalzado;
            lblValTallaCamisa.Text = oEFOT.DatosAspirante.TallaCamisa;
            lblValTallaGorra.Text = oEFOT.DatosAspirante.TallaGorra;
            lblValTallaPantalon.Text = oEFOT.DatosAspirante.TallaPantalon;
            lblValTipoSangre.Text = oEFOT.DatosAspirante.TipoSangre;
            lblIdeDactilar.Text = oEFOT.DatosAspirante.IdeDactilar;
        }
    }

    protected void LoadDirecciones()
    {
        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtDirecciones;
        if (oEFOT.LoadDireccionesAspirante(Session[EFOTclass.Parametros.Session.CodigoAspirante].ToString(), out dtDirecciones))
        {
            gvDirecciones.DataSource = dtDirecciones;
            gvDirecciones.DataBind();
        }
        else
        {
            gvDirecciones.DataSource = new DataTable();
            gvDirecciones.DataBind();
        }
    }

    protected void LoadEstudios()
    {
        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtEstudios;
        if (oEFOT.LoadEstudiosAspirante(Session[EFOTclass.Parametros.Session.CodigoAspirante].ToString(), out dtEstudios))
        {
            gvEstudios.DataSource = dtEstudios;
            gvEstudios.DataBind();
        }
        else
        {
            gvEstudios.DataSource = new DataTable();
            gvEstudios.DataBind();
        }
    }

    protected void LoadReferencias()
    {
        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtReferencias;
        if (oEFOT.LoadReferenciasAspirante(Session[EFOTclass.Parametros.Session.CodigoAspirante].ToString(), out dtReferencias))
        {
            gvReferencias.DataSource = dtReferencias;
            gvReferencias.DataBind();
        }
        else
        {
            gvReferencias.DataSource = new DataTable();
            gvReferencias.DataBind();
        }
    }

    protected void LoadPruebasAdmision()
    {
        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtPruebas;
        if (oEFOT.LoadPruebasAdmision(Session[EFOTclass.Parametros.Session.CodigoAspirante].ToString(), out dtPruebas))
        {
            gvPruebas.DataSource = dtPruebas;
            gvPruebas.DataBind();
        }
        else
        {
            gvPruebas.DataSource = new DataTable();
            gvPruebas.DataBind();
        }
    }


    protected void LoadDdlTiposEstudios()
    {
        EFOTclass.Parametros oParEFOT = new EFOTclass.Parametros(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtParam = oParEFOT.TiposEducacion();
        ddlTipoEducacion.DataSource = dtParam;
        ddlTipoEducacion.DataValueField = dtParam.Columns[0].ColumnName;
        ddlTipoEducacion.DataTextField = dtParam.Columns[1].ColumnName;
        ddlTipoEducacion.DataBind();
    }

    protected void LoadDdlTiposReferencias()
    {
        EFOTclass.Parametros oParEFOT = new EFOTclass.Parametros(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtParam = oParEFOT.TiposReferencia();
        ddlTipoReferencia.DataSource = dtParam;
        ddlTipoReferencia.DataValueField = dtParam.Columns[0].ColumnName;
        ddlTipoReferencia.DataTextField = dtParam.Columns[1].ColumnName;
        ddlTipoReferencia.DataBind();
    }

    protected void LoadDdlTiposUbicaciones()
    {
        EFOTclass.Parametros oParEFOT = new EFOTclass.Parametros(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtParam = oParEFOT.TiposUbicacion();
        ddlTipoDireccion.DataSource = dtParam;
        ddlTipoDireccion.DataValueField = dtParam.Columns[0].ColumnName;
        ddlTipoDireccion.DataTextField = dtParam.Columns[1].ColumnName;
        ddlTipoDireccion.DataBind();
    }

    protected void LoadPaises(DropDownList ddlPaises)
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView dvPaises = geog.GetPaisesConCodigosConfContinente().DefaultView;
        dvPaises.Sort = string.Format("{0} {1}", dvPaises.Table.Columns[1].ColumnName, "ASC");
        ddlPaises.DataSource = dvPaises;
        ddlPaises.DataValueField = dvPaises.Table.Columns[0].ColumnName;
        ddlPaises.DataTextField = dvPaises.Table.Columns[1].ColumnName;
        ddlPaises.DataBind();
    }
    protected void LoadProvincias(DropDownList ddlProvincias, string codPais)
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView dvProvincias = geog.GetProvinciasConCodigos(codPais).DefaultView;
        dvProvincias.Sort = string.Format("{0} {1}", dvProvincias.Table.Columns[1].ColumnName, "ASC");
        ddlProvincias.DataSource = dvProvincias;
        ddlProvincias.DataValueField = dvProvincias.Table.Columns[0].ColumnName;
        ddlProvincias.DataTextField = dvProvincias.Table.Columns[1].ColumnName;
        ddlProvincias.DataBind();
    }
    protected void LoadCiudades(DropDownList ddlCiudades, string codProvincia)
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView dvCiudades = geog.GetCiudadesConCodigos(codProvincia).DefaultView;
        dvCiudades.Sort = string.Format("{0} {1}", dvCiudades.Table.Columns[1].ColumnName, "ASC");
        ddlCiudades.DataSource = dvCiudades;
        ddlCiudades.DataValueField = dvCiudades.Table.Columns[0].ColumnName;
        ddlCiudades.DataTextField = dvCiudades.Table.Columns[1].ColumnName;
        ddlCiudades.DataBind();
    }


    protected void btnAddEstudio_Click(object sender, EventArgs e)
    {
        gvEstudios.Visible = false;
        pnlEstudioRealizado.Visible = true;
        btnAddEstudio.Visible = false;
    }

    protected void btnAddReferencia_Click(object sender, EventArgs e)
    {
        gvReferencias.Visible = false;
        pnlReferencia.Visible = true;
        btnAddReferencia.Visible = false;
    }

    protected void btnAddDireccion_Click(object sender, EventArgs e)
    {
        gvDirecciones.Visible = false;
        pnlNvaDirecc.Visible = true;
        btnAddDireccion.Visible = false;
    }

    protected void HideNuevoEstudioCtrls()
    {
        pnlEstudioRealizado.Visible = false;
        btnAddEstudio.Visible = true;
        gvEstudios.Visible = true;
    }

    protected void HideNuevaReferenciaCtrls()
    {
        pnlReferencia.Visible = false;
        btnAddReferencia.Visible = true;
        gvReferencias.Visible = true;
    }

    protected void HideNuevaDireccionCtrls()
    {
        pnlNvaDirecc.Visible = false;
        btnAddDireccion.Visible = true;
        gvDirecciones.Visible = true;
    }

    protected void btnGuardarEstudio_Click(object sender, EventArgs e)
    {
        EFOTclass.EFOT.Aspirante oAspirante = new EFOTclass.EFOT.Aspirante(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        try
        {
            if (oAspirante.InsertEstudioOnDB(int.Parse(Session[EFOTclass.Parametros.Session.CodigoAspirante].ToString()), ddlTipoEducacion.SelectedValue, ddlPaisEducac.SelectedValue,
                ddlProvEducac.SelectedValue, ddlCiudEducac.SelectedValue, txtInstitucion.Text, txtTitulo.Text, float.Parse(txtNotaGrado.Text), int.Parse(txtAnioGrado.Text), txtObservacEducac.Text))
            {
                LoadEstudios();
            }
        }
        catch { }
        HideNuevoEstudioCtrls();
        ClearNewStudyFields();
    }

    protected void btnGuardarReferencia_Click(object sender, EventArgs e)
    {
        EFOTclass.EFOT.Aspirante oAspirante = new EFOTclass.EFOT.Aspirante(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        try
        {
            if (oAspirante.InsertReferenciaOnDB(int.Parse(Session[EFOTclass.Parametros.Session.CodigoAspirante].ToString()), ddlTipoReferencia.SelectedValue, txtIdentReferencia.Text, txtNombresReferencia.Text, txtApellidReferencia.Text,
                ddlProvincReferencia.SelectedValue, ddlCiudadReferencia.SelectedValue, txtDireccReferencia.Text, txtTelefReferencia.Text, txtObservacReferencia.Text))
            {
                LoadReferencias();
            }
        }
        catch { }
        HideNuevaReferenciaCtrls();
        ClearNewReferenceFields();
    }

    protected void btnGuardarDireccion_Click(object sender, EventArgs e)
    {
        EFOTclass.EFOT.Aspirante oAspirante = new EFOTclass.EFOT.Aspirante(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        try
        {
            if (oAspirante.InsertDireccionOnDB(int.Parse(Session[EFOTclass.Parametros.Session.CodigoAspirante].ToString()), ddlTipoDireccion.SelectedValue, 
                ddlProvinciaDireccion.SelectedValue, ddlCiudadDireccion.SelectedValue, txtDireccion.Text, txtTelefConvDireccion.Text, txtTelefMovDireccion.Text, txtReferenciaDireccion.Text))
            {
                LoadDirecciones();
            }
        }
        catch { }
        HideNuevaDireccionCtrls();
        ClearNewAddressFields();
    }

    protected void btnCancelarEstudio_Click(object sender, EventArgs e)
    {
        HideNuevoEstudioCtrls();
    }

    protected void btnCancelarDireccion_Click(object sender, EventArgs e)
    {
        HideNuevaDireccionCtrls();
    }

    protected void btnCancelarReferencia_Click(object sender, EventArgs e)
    {
        HideNuevaReferenciaCtrls();
    }

    protected void ddlPaisEducac_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProvincias(ddlProvEducac, ddlPaisEducac.SelectedValue);
    }
    protected void ddlProvEducac_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCiudades(ddlCiudEducac, ddlProvEducac.SelectedValue);
    }
    protected void ddlPaisReferencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProvincias(ddlProvincReferencia, ddlPaisReferencia.SelectedValue);
    }
    protected void ddlProvReferencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCiudades(ddlCiudadReferencia, ddlProvincReferencia.SelectedValue);
    }
    protected void ddlPaisDireccion_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProvincias(ddlProvinciaDireccion, ddlPaisDireccion.SelectedValue);
    }
    protected void ddlProvDireccion_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCiudades(ddlCiudadDireccion, ddlProvinciaDireccion.SelectedValue);
    }


    protected void btnLogout_Click(object sender, ImageClickEventArgs e)
    {
        Session.RemoveAll();
        Session.Abandon();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }

    protected void ClearNewAddressFields()
    {
        txtDireccion.Text = string.Empty;
        txtTelefConvDireccion.Text = string.Empty;
        txtTelefMovDireccion.Text = string.Empty;
        txtReferenciaDireccion.Text = string.Empty;
    }

    protected void ClearNewStudyFields()
    {
        txtInstitucion.Text = string.Empty;
        txtTitulo.Text = string.Empty;
        txtNotaGrado.Text = string.Empty;
        txtAnioGrado.Text = string.Empty;
        txtObservacEducac.Text = string.Empty;
    }

    protected void ClearNewReferenceFields()
    {
        txtIdentReferencia.Text = string.Empty;
        txtNombresReferencia.Text = string.Empty;
        txtApellidReferencia.Text = string.Empty;
        txtDireccReferencia.Text = string.Empty;
        txtTelefReferencia.Text = string.Empty;
        txtObservacReferencia.Text = string.Empty;
    }
}