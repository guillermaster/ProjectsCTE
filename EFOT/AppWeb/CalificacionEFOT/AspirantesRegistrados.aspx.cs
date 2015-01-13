using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class AspirantesRegistrados : System.Web.UI.Page
{
    private static DataTable _dtAspirantesRegistrados;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadCriteriosBusqueda();
            LoadRequisitosCriterio(ddlCriterio.SelectedValue);
            mpeRegAct.Hide();
            LoadActividades(EFOTclass.Parametros.Actividades.Tipos.Registro, ddlTipoAct);
            LoadActividades(EFOTclass.Parametros.Actividades.Tipos.Registro, ddlTipoActBulk);
            LoadActividades(EFOTclass.Parametros.Actividades.Tipos.Revisión, ddlTipoRevision);
            if (Session[EFOTclass.Parametros.Session.Roles.InstructorCalificador] != null)
            {
                try
                {
                    if (Convert.ToBoolean(Session[EFOTclass.Parametros.Session.Roles.InstructorCalificador]))
                        ShowActividades();
                    else
                        HideActividades();
                }
                catch (Exception) { HideActividades(); }
            }
            else
                HideActividades();
        }
    }

    protected void HideActividades()
    {
        btnRegistrarAct.Visible = false;
        btnRevisarAct.Visible = false;
    }

    protected void ShowActividades()
    {
        btnRegistrarAct.Visible = true;
        btnRevisarAct.Visible = true;
    }

    protected void LoadCriteriosBusqueda()
    {
        EFOTclass.Parametros oPar = new EFOTclass.Parametros(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtParametros = oPar.CriteriosBusquedaAspirantes();
        ddlCriterio.DataSource = dtParametros;
        ddlCriterio.DataValueField = dtParametros.Columns[0].ColumnName;
        ddlCriterio.DataTextField = dtParametros.Columns[1].ColumnName;
        ddlCriterio.DataBind();
    }

    protected void LoadActividades(string tipoActividad, DropDownList ddlActividad)
    {
        EFOTclass.Parametros.Actividades oAct = new EFOTclass.Parametros.Actividades(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtAct = oAct.GetActividades(tipoActividad);
        ddlActividad.DataSource = dtAct;
        ddlActividad.DataValueField = dtAct.Columns[0].ColumnName;
        ddlActividad.DataTextField = dtAct.Columns[1].ColumnName;
        ddlActividad.DataBind();
    }

    
    protected void LoadRequisitosCriterio(string codCriterio)
    {
        lblRequisitoCriterio.Visible = true;
        btnNextExam.Visible = false;
        
        switch (codCriterio)
        {
            case "T":
                HideActividadControls();
                lblRequisitoCriterio.Visible = false;
                break;
            case "C":
                HideActividadControls();
                txtRequisitoCriterio.Visible = true;
                ddlRequisitoCriterio.Visible = false;
                break;
            break;
            case "N":
                LoadNotaAcademica(ddlRequisitoCriterio);
                HideActividadControls();
                break;
            case "P":
                LoadProvincias(ddlRequisitoCriterio);
                HideActividadControls();
                break;
            case "E":
                LoadTipoEducacion(ddlRequisitoCriterio);
                HideActividadControls();
                break;
            case "A"://consulta por actividad
                ddlTipoActividad.Visible = true;
                txtRequisitoCriterio.Visible = false;
                lblRequisitoCriterio.Visible = false;
                ddlRequisitoCriterio.Visible = false;
                break;
        }
    }

    protected void HideActividadControls()
    {
        ddlTipoActividad.Visible = false;
        txtRequisitoCriterio.Visible = false;
        lblEstadoAct.Visible = false;
        ddlEstadoAct.Visible = false;
    }

    protected void ddlCriterio_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRequisitosCriterio(ddlCriterio.SelectedValue);
    }

    protected void AspRegGridViewPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvAspirantesReg.PageIndex = e.NewPageIndex;
        gvAspirantesReg.DataSource = _dtAspirantesRegistrados;
        gvAspirantesReg.DataBind();
    }


    protected void LoadProvincias(DropDownList ddl)
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView dvProvincias = geog.GetProvinciasConCodigos("ECU").DefaultView;
        dvProvincias.Sort = string.Format("{0} {1}", dvProvincias.Table.Columns[1].ColumnName, "ASC");
        ddl.DataSource = dvProvincias;
        ddl.DataValueField = dvProvincias.Table.Columns[0].ColumnName;
        ddl.DataTextField = dvProvincias.Table.Columns[1].ColumnName;
        ddl.DataBind();
        ddl.Visible = true;
    }

    protected void LoadTipoEducacion(DropDownList ddl)
    {
        EFOTclass.Parametros oParEFOT = new EFOTclass.Parametros(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtParam = oParEFOT.TiposEducacion();
        ddl.DataSource = dtParam;
        ddl.DataValueField = dtParam.Columns[0].ColumnName;
        ddl.DataTextField = dtParam.Columns[1].ColumnName;
        ddl.DataBind();
        ddl.Visible = true;
    }


    protected void LoadNotaAcademica(DropDownList ddl)
    {
        ddl.Items.Clear();
        for (int i = 14; i <= 20; i++)
        {
            ListItem li = new ListItem(i.ToString());
            ddl.Items.Add(li);
        }
        ddl.Visible = true;
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        int total;
        string requisito;
        if (ddlRequisitoCriterio.Visible)
            requisito = ddlRequisitoCriterio.SelectedValue;
        else
            requisito = txtRequisitoCriterio.Text;

        if (ddlCriterio.SelectedValue != "A")
        {
            _dtAspirantesRegistrados = oEFOT.AspirantesRegistrados(ddlCriterio.SelectedValue, requisito, out total);
            gvAspirantesReg.DataSource = _dtAspirantesRegistrados;
            gvAspirantesReg.DataBind();
            lblTotal.Text = "Se han registrado un total de " + total.ToString() + " aspirantes";
            btnNextExam.Visible = false;
        }
        else
        {
            if (ddlTipoActividad.SelectedValue == "REG")
                _dtAspirantesRegistrados = oEFOT.AspirantesPorRegistroActividad(ddlRequisitoCriterio.SelectedValue);
            else
                _dtAspirantesRegistrados = oEFOT.AspirantesPorActividad(ddlRequisitoCriterio.SelectedValue, ddlEstadoAct.SelectedValue);
            gvAspirantesReg.DataSource = _dtAspirantesRegistrados;
            gvAspirantesReg.DataBind();
            lblTotal.Text = "Se han registrado un total de " + _dtAspirantesRegistrados.Rows.Count.ToString() + " aspirantes";
            if (_dtAspirantesRegistrados.Rows.Count > 0 && ddlTipoActividad.SelectedValue != "REG")
                btnNextExam.Visible = true;
            else
                btnNextExam.Visible = false;
        }
        
    }


    protected void gvAspirantesReg_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadDatosBasicos(gvAspirantesReg.SelectedRow.Cells[3].Text);
        pnlForm.Visible = false;
        pnlMoreData.Visible = true;
    }

    protected void LoadDatosBasicos(string codAspirante)
    {
        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        if (oEFOT.LoadDatosAspirante(codAspirante))
        {
            txtNombres.Text = oEFOT.DatosAspirante.Nombres;
            txtApellidos.Text = oEFOT.DatosAspirante.Apellidos;
            txtCargFam.Text = oEFOT.DatosAspirante.CargasFamiliares;
            lblValCedula.Text = oEFOT.DatosAspirante.Identificacion;
            lblValCiudadNac.Text = oEFOT.DatosAspirante.CiudadNac;
            txtEmail.Text = oEFOT.DatosAspirante.Email;
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
            txtIdeDactilar.Text = oEFOT.DatosAspirante.IdeDactilar;
        }
        LoadDirecciones(codAspirante);
        LoadEstudios(codAspirante);
        LoadReferencias(codAspirante);
    }

    protected void LoadDirecciones(string codAspirante)
    {
        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtDirecciones;
        if (oEFOT.LoadDireccionesAspirante(codAspirante, out dtDirecciones))
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

    protected void LoadEstudios(string codAspirantes)
    {
        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtEstudios;
        if (oEFOT.LoadEstudiosAspirante(codAspirantes, out dtEstudios))
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

    protected void LoadReferencias(string codAspirante)
    {
        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtReferencias;
        if (oEFOT.LoadReferenciasAspirante(codAspirante, out dtReferencias))
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
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        pnlMoreData.Visible = false;
        pnlForm.Visible = true;
        txtApellidos.ReadOnly = true;
        txtApellidos.BorderStyle = BorderStyle.None;
        txtNombres.ReadOnly = true;
        txtNombres.BorderStyle = BorderStyle.None;
        txtCargFam.ReadOnly = true;
        txtCargFam.BorderStyle = BorderStyle.None;
        txtEmail.ReadOnly = true;
        txtEmail.BorderStyle = BorderStyle.None;
        txtIdeDactilar.ReadOnly = true;
        txtIdeDactilar.BorderStyle = BorderStyle.None;
        txtRequisitoCriterio.ReadOnly = true;
        txtRequisitoCriterio.BorderStyle = BorderStyle.None;
        btnSaveChanges.Visible = false;
    }
    protected void btnSaveChanges_Click(object sender, ImageClickEventArgs e)
    {
        string nombres = string.Empty;
        string apellidos = string.Empty;
        string email = string.Empty;
        string cargas = string.Empty;
        string dactilar = string.Empty;
        if (!txtNombres.ReadOnly)
            nombres = txtNombres.Text;
        if (!txtApellidos.ReadOnly)
            apellidos = txtApellidos.Text;
        if (!txtEmail.ReadOnly)
            email = txtEmail.Text;
        if (!txtCargFam.ReadOnly)
            cargas = txtCargFam.Text;
        if (!txtIdeDactilar.ReadOnly)
            dactilar = txtIdeDactilar.Text;

        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        if(oEFOT.UpdateDataAspirante(lblValCedula.Text, nombres, apellidos, email, cargas, dactilar))
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('¡Los datos fueron actualizados exitosamente!');", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('" + oEFOT.Error + "');", true);
    }
    protected void btnEditNombres_Click(object sender, ImageClickEventArgs e)
    {
        txtNombres.ReadOnly = false;
        txtNombres.BorderStyle = BorderStyle.Solid;
        btnSaveChanges.Visible = true;
    }
    protected void btnEditApellidos_Click(object sender, ImageClickEventArgs e)
    {
        txtApellidos.ReadOnly = false;
        txtApellidos.BorderStyle = BorderStyle.Solid;
        btnSaveChanges.Visible = true;
    }
    protected void btnEditEmail_Click(object sender, ImageClickEventArgs e)
    {
        txtEmail.ReadOnly = false;
        txtEmail.BorderStyle = BorderStyle.Solid;
        btnSaveChanges.Visible = true;
    }
    protected void btnEditCargFam_Click(object sender, ImageClickEventArgs e)
    {
        txtCargFam.ReadOnly = false;
        txtCargFam.BorderStyle = BorderStyle.Solid;
        btnSaveChanges.Visible = true;
    }
    protected void btnEditIdeDactilar_Click(object sender, ImageClickEventArgs e)
    {
        txtIdeDactilar.BorderStyle = BorderStyle.Solid;
        txtIdeDactilar.ReadOnly = false;
        btnSaveChanges.Visible = true;
    }
    protected void btnRegistrarAct_Click(object sender, EventArgs e)
    {
        mpeRegAct.Show();
    }
    protected void btnGuardarRegAct_Click(object sender, EventArgs e)
    {
        EFOTclass.EFOT.Aspirante oAspirante = new EFOTclass.EFOT.Aspirante(int.Parse(gvAspirantesReg.SelectedRow.Cells[3].Text), HttpContext.Current.User.Identity.Name,
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        try
        {
            bool emailSent;
            if (oAspirante.InsertActividadOnDB(ddlTipoAct.SelectedValue, ConvertToDateTime(txtFecAct.Text), txtHoraAct.Text, txtObservacAct.Text,
                ddlTipoAct.SelectedItem.Text, "Escuela de Formación de Oficiales y Tropa", gvAspirantesReg.SelectedRow.Cells[5].Text, gvAspirantesReg.SelectedRow.Cells[6].Text, out emailSent))
            {
                if(emailSent)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('La actividad ha sido registrada');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('La actividad ha sido registrada, pero no se pudo enviar la notificación por email al aspirante, se recomienda que usted se contacte con el usuario para que le notifique personalmente.');", true);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(oAspirante.Error))
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('No se pudo registrar la actividad');", true);
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('No se pudo registrar la actividad. " + oAspirante.Error + "');", true);
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('HORROR');", true);
        }
    }

    protected DateTime ConvertToDateTime(string mmddyyyy)
    {
        int d, m, y, idxFirst, idxLast;
        idxFirst = mmddyyyy.IndexOf('/');
        idxLast = mmddyyyy.LastIndexOf('/');
        m = int.Parse(mmddyyyy.Substring(0, idxFirst));
        d = int.Parse(mmddyyyy.Substring(idxFirst + 1, (idxLast - 1) - idxFirst));
        y = int.Parse(mmddyyyy.Substring(idxLast + 1, mmddyyyy.Length - (idxLast + 1)));
        DateTime dt = new DateTime(y, m, d);
        return dt;
    }
    protected void btnRevisarAct_Click(object sender, EventArgs e)
    {
        mpeRevision.Show();
    }
    protected void btnGuardarRevision_Click(object sender, EventArgs e)
    {
        EFOTclass.EFOT.Aspirante oAspirante = new EFOTclass.EFOT.Aspirante(int.Parse(gvAspirantesReg.SelectedRow.Cells[3].Text), HttpContext.Current.User.Identity.Name,
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        bool emailSent;
        if (oAspirante.InsertRevisionActividadOnDB(ddlTipoRevision.SelectedValue, ddlEstadoRevision.SelectedValue, txtObservacRevision.Text,
            ddlTipoRevision.SelectedItem.Text, gvAspirantesReg.SelectedRow.Cells[5].Text, gvAspirantesReg.SelectedRow.Cells[6].Text, out emailSent))
        {
            if(emailSent)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('La revisión ha sido registrada');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('La revisión ha sido registrada, pero no se pudo enviar la notificación por email al aspirante, se recomienda que usted se contacte con el usuario para que le notifique personalmente.');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('No se pudo registrar la revisión. " + oAspirante.Error + "');", true);
    }

    protected void btnNextExam_Click(object sender, EventArgs e)
    {
        mpeRegActBulk.Show();
    }

    protected void btnGuardarRegActBulk_Click(object sender, EventArgs e)
    {
        int numTotalExito = 0;
        int numTotalSelec = 0;
        int numTotalFalloMail = 0;
        int numTotalFallo = 0;

        string txtFallidosEmail = string.Empty;
        string txtFallidosReg = string.Empty;

        foreach (GridViewRow gvrow in gvAspirantesReg.Rows)
        {
            CheckBox chk = gvrow.Cells[1].Controls[1] as CheckBox;
            if (chk.Checked)
            {
                numTotalSelec++;
                EFOTclass.EFOT.Aspirante oAspirante = new EFOTclass.EFOT.Aspirante(int.Parse(gvrow.Cells[3].Text), HttpContext.Current.User.Identity.Name,
                    ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                    ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                    ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
                try
                {
                    bool emailSent;
                    if (oAspirante.InsertActividadOnDB(ddlTipoActBulk.SelectedValue, ConvertToDateTime(txtFecActBulk.Text), txtHoraActBulk.Text, txtObservacActBulk.Text,
                        ddlTipoActBulk.SelectedItem.Text, "Escuela de Formación de Oficiales y Tropa", gvrow.Cells[5].Text, gvrow.Cells[6].Text, out emailSent))
                    {
                        numTotalExito++;
                        if (!emailSent)
                        {
                            numTotalFalloMail++;
                            txtFallidosEmail += gvrow.Cells[4].Text + " ";
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('La actividad ha sido registrada, pero no se pudo enviar la notificación por email al aspirante, se recomienda que usted se contacte con el usuario para que le notifique personalmente.');", true);
                        }
                    }
                    else
                    {
                        numTotalFallo++;
                        txtFallidosReg += gvrow.Cells[4].Text + " ";
                        /*if (string.IsNullOrWhiteSpace(oAspirante.Error))
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('No se pudo registrar la actividad');", true);
                        else
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('No se pudo registrar la actividad. " + oAspirante.Error + "');", true);*/
                    }
                }
                catch (Exception ex)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('HORROR');", true);
                    numTotalFallo++;
                    txtFallidosReg += gvrow.Cells[4].Text + " ";
                }
            }
        }

        string alertMsg = "alert('Se han registrado " + numTotalExito.ToString() +"/" + numTotalSelec.ToString() + " actividades.";
        if (numTotalFalloMail > 0)
        {
            alertMsg += " No se pudo enviar en email de notificación a " + numTotalFalloMail.ToString() + " aspirantes.";
            alertMsg += " No se enviaron notificaciones a los aspirantes con los siguientes número de cédulas: " + txtFallidosEmail + ".  ";
        }
        if (numTotalFallo > 0)
        {
            alertMsg += " No se registró la actividad en un total de " + numTotalFallo.ToString() + " aspirantes.";
            alertMsg += " Fallaron los registros de los aspirantes con los siguientes número de cédulas: " + txtFallidosReg + ". ";
        }
        alertMsg += "');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", alertMsg, true);
    }

    protected void btnVerCalificaciones_Click(object sender, EventArgs e)
    {
        ImageButton imgBtn = sender as ImageButton;
        GridViewRow gvRow = imgBtn.Parent.Parent as GridViewRow;
        LoadPruebasAdmision(gvRow.Cells[3].Text, gvRow.Cells[4].Text, gvRow.Cells[5].Text);
    }

    protected void LoadPruebasAdmision(string codAspiramte, string numCedula, string nombre)
    {
        lblValNombreCalif.Text = nombre;
        lblValNumCedulaCalif.Text = numCedula;

        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtPruebas;
        if (oEFOT.LoadPruebasAdmision(codAspiramte, out dtPruebas))
        {
            gvCalificaciones.DataSource = dtPruebas;
            gvCalificaciones.DataBind();
            if (Session[EFOTclass.Parametros.Session.Roles.ReversaCalificacion] != null)
            {
                if (Convert.ToBoolean(Session[EFOTclass.Parametros.Session.Roles.ReversaCalificacion]))
                    imgBtnRevertirCalificacion.Visible = true;
            }
        }
        else
        {
            gvCalificaciones.DataSource = new DataTable();
            gvCalificaciones.DataBind();
        }
        mpeCalificaciones.Show();
    }
    protected void ddlTipoActividad_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(ddlTipoActividad.SelectedValue))
        {
            
            lblRequisitoCriterio.Visible = true;
            ddlRequisitoCriterio.Visible = true;            
            if (ddlTipoActividad.SelectedValue == "REG")
            {
                LoadActividades(EFOTclass.Parametros.Actividades.Tipos.Registro, ddlRequisitoCriterio);
                lblEstadoAct.Visible = false;
                ddlEstadoAct.Visible = false;
            }
            else
            {
                LoadActividades(EFOTclass.Parametros.Actividades.Tipos.Revisión, ddlRequisitoCriterio);
                lblEstadoAct.Visible = true;
                ddlEstadoAct.Visible = true;
            }
        }
        else
        {
            ddlRequisitoCriterio.Visible = false;
            lblEstadoAct.Visible = false;
            ddlEstadoAct.Visible = false;
            lblRequisitoCriterio.Visible = false;
        }
    }


    protected void imgBtnRevertirCalificacion_Click(object sender, ImageClickEventArgs e)
    {
        EFOTclass.EFOT.Aspirante oAspirante = new EFOTclass.EFOT.Aspirante(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        if(oAspirante.ReversarCalificacion(lblValNumCedulaCalif.Text))
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Se ha eliminado la calificación no idónea.')", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('" + oAspirante.Error + "')", true);
    }
}