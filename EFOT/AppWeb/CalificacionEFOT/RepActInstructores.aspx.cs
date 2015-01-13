using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RepActInstructores : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadTipoActividades();
            LoadActividades(ddlTipoActividad.SelectedValue);
            LoadInstructoresUsers();

            if (Session[EFOTclass.Parametros.Session.Roles.InstructorCalificador] != null)
            {
                try
                {
                    if (Convert.ToBoolean(Session[EFOTclass.Parametros.Session.Roles.InstructorCalificador]))
                        ShowForm();
                    else
                        HideForm();
                }
                catch (Exception) { HideForm(); }
            }
            else
                HideForm();

        }
    }

    protected void HideForm()
    {
        pnlForm.Visible = false;
        lblError.Text = "Usted no tiene los privilegios necesarios para acceder a esta página.";
    }

    protected void ShowForm()
    {
        pnlForm.Visible = true;
        lblError.Text = string.Empty;
    }

    protected void LoadTipoActividades()
    {
        ListItem liReg = new ListItem("Registro", EFOTclass.Parametros.Actividades.Tipos.Registro);
        ListItem liRev = new ListItem("Revisión", EFOTclass.Parametros.Actividades.Tipos.Revisión);
        ddlTipoActividad.Items.Clear();
        ddlTipoActividad.Items.Add(liReg);
        ddlTipoActividad.Items.Add(liRev);
    }

    protected void LoadActividades(string tipoActividad)
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

    protected void LoadInstructoresUsers()
    {
        EFOTclass.User oUser = new EFOTclass.User(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dt = oUser.GetAllInstructoresUsers();
        ddlInstructores.DataSource = dt;
        ddlInstructores.DataValueField = dt.Columns[0].ColumnName;
        ddlInstructores.DataTextField = dt.Columns[0].ColumnName;
        ddlInstructores.DataBind();
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        int total;
        gvActividades.DataSource = oEFOT.ConsultaActividadesPorInstructor(ddlInstructores.SelectedValue, ConvertToDateTime(txtFecDesde.Text), ConvertToDateTime(txtFecHasta.Text), 
            ddlTipoActividad.SelectedValue, ddlActividad.SelectedValue, ddlEstado.SelectedValue, out total);
        gvActividades.DataBind();
        lblTotal.Text = "Se encontró un total de " + total.ToString() + " registro(s).";
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
    protected void ddlTipoActividad_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadActividades(ddlTipoActividad.SelectedValue);
    }
}