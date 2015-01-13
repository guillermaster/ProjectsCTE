using System;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls;

public partial class AsignaGestorSolMatricula : System.Web.UI.Page
{
    private string _currentUser;
    private string _sessionVarNameGestores;
    private string _sessionVarNameSolicitudes;
    //private static DataTable _dtSolicitudes;
    //private static DataTable _dtGestores;

    protected void Page_Load(object sender, EventArgs e)
    {
        _currentUser = User.Identity.Name;
        _sessionVarNameGestores = "_dtGestores";
        _sessionVarNameSolicitudes = "_dtSolicitudes";

        if (!IsPostBack)
        {
            ValidateAccess();
            //LoadComercSolicitudesPendientesGestor();
            /*if (this.ddlComercSolPend.Items.Count > 0)
                LoadGestores();*/
            try
            {
                hdnCodComerc.Value = Session[AEA.Parametros.SessionVarCodEmpresa].ToString();
                LoadSolicitudesPendientes(int.Parse(hdnCodComerc.Value));
            }
            catch
            {
                LogOut();
            }
            
            btnSave.Tipo = AEA.Parametros.Acciones.Guardar.Nuevo;
        }
    }

    protected void ValidateAccess()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(_currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (!oUsuario.ValidateAccessToModule(AEA.Parametros.ModuloCodigoTrxSolMatricula))
        {
            Response.Redirect("AccessDenied.aspx");
        }
    }

    #region "Carga de Datos"
    ////  cambio solicitado.. la AEA ya no hace las asignaciones sino las comercializadoras
    /*protected void LoadComercSolicitudesPendientesGestor()
    {
        AEA.SolicitudMatricula oSolMat = new AEA.SolicitudMatricula(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtCom = oSolMat.ComercializadorasSolicitudPendienteGestor();
        dtCom.Rows.Add(null, " -- Seleccione -- ");
        dtCom.DefaultView.Sort = dtCom.Columns[1].ColumnName + " asc";
        this.ddlComercSolPend.DataSource = dtCom;
        this.ddlComercSolPend.DataValueField = dtCom.Columns[0].ColumnName;
        this.ddlComercSolPend.DataTextField = dtCom.Columns[1].ColumnName;
        this.ddlComercSolPend.DataBind();
    }*/


    protected void LoadSolicitudesPendientes(int codSucursal)
    {
        AEA.SolicitudMatricula oSolMat = new AEA.SolicitudMatricula(_currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtSolicitudes;
        dtSolicitudes = oSolMat.SolicitudesPendientesPorSucursal(codSucursal);
        dtSolicitudes.Columns.Add("gestor");
        dtSolicitudes.Columns.Add("id_gestor");
        Session[_sessionVarNameSolicitudes] = dtSolicitudes;
        gvSolPendientes.DataSource = dtSolicitudes;
        gvSolPendientes.DataBind();
        if (gvSolPendientes != null)
            if (gvSolPendientes.Rows.Count > 0)
            {
                btnSave.Visible = true;
                LoadGestores();
            }
    }

    protected void LoadGestores()
    {
        AEA.Gestor oGestor = new AEA.Gestor(_currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtGestores;
        dtGestores = oGestor.ObtenerGestores();
        int nCols = dtGestores.Columns.Count;
        for (int i = nCols - 1; i > -1; i--)
        {
            if (i != 0 && i != 2)
                dtGestores.Columns.RemoveAt(i);
        }
        dtGestores.Rows.Add(null, " -- Seleccione -- ");
        dtGestores.DefaultView.Sort = dtGestores.Columns[1].ColumnName + " asc";
        Session[_sessionVarNameGestores] = dtGestores;
    }
    #endregion

    protected void ShowSuccessMessage(string message)
    {
        divError.Visible = false;
        divWarning.Visible = true;
        lblMsgWarning.Text = message;
    }


    protected void ShowFailureMessage(string message)
    {
        divWarning.Visible = false;
        divError.Visible = true;
        lblMsgError.Text = message;
    }

    protected bool RealizarAsignaciones()
    {
        DataTable dtErrores = new DataTable();
        dtErrores.Columns.Add("Código de Solicitud");
        dtErrores.Columns.Add("Descripción del Error generado");

        foreach (GridViewRow row in gvSolPendientes.Rows)
        {
            try
            {
                if (row.Visible)
                {
                    int idSol = int.Parse(row.Cells[0].Text);
                    HiddenField hdnCodGestor = (HiddenField) row.Cells[3].FindControl("hdnCodGestor");
                    if (hdnCodGestor.Value != "")
                    {
                        int idGestor = int.Parse(hdnCodGestor.Value);
                        int idComSuc = int.Parse(hdnCodComerc.Value);
                        AEA.SolicitudMatricula oSolMat = new AEA.SolicitudMatricula(idSol, idGestor, idComSuc,
                                                                                    _currentUser,
                                                                                    ConfigurationManager.AppSettings["usuario"],
                                                                                    ConfigurationManager.AppSettings["clave"],
                                                                                    ConfigurationManager.AppSettings["base"]);
                        if (oSolMat.AsignaGestor())
                        {
                            //_dtSolicitudes.Rows.RemoveAt(row.RowIndex);
                            gvSolPendientes.Rows[row.RowIndex].Visible = false;
                        }
                        else
                        {
                            DataRow dr = dtErrores.NewRow();
                            dr[0] = idSol;
                            dr[1] = oSolMat.TrxError;
                            dtErrores.Rows.Add(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DataRow dr = dtErrores.NewRow();
                dr[0] = row.Cells[0].Text;
                dr[1] = ex.Message;
                dtErrores.Rows.Add(dr);
            }
        }

        gvErrores.DataSource = dtErrores;
        gvErrores.DataBind();
        GridViewDataBind();

        return dtErrores.Rows.Count == 0;
    }

    protected void RegistrarAsignaciones()
    {
        int numRowsBefore = gvSolPendientes.Rows.Count;
        if (RealizarAsignaciones())
        {
            gvSolPendientes.Visible = false;
            divBusqueda.Visible = false;
            btnSave.Visible = false;
            ShowSuccessMessage("Se asignaron todos los gestores correctamente");
        }
        else
        {
            btnVerErrores.Visible = true;
            if (gvSolPendientes.Rows.Count == numRowsBefore)
                ShowFailureMessage("No se pudo realizar ninguna asignación de gestor");
            else
                ShowFailureMessage("No se pudo asignar el gestor en algunas solicitudes");
        }
    }

    protected void GridViewDataBind()
    {
        gvSolPendientes.DataSource = Session[_sessionVarNameSolicitudes] as DataTable;
        gvSolPendientes.DataBind();
    }

    protected void ddlComercSolPend_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            LoadSolicitudesPendientes(int.Parse(ddlComercSolPend.SelectedValue));
        }
        catch (Exception)
        {
            gvSolPendientes.Visible = false;
            btnSave.Visible = false;
        }
    }

    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvSolPendientes.EditIndex = -1;
        GridViewDataBind();
    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList ddlGestores = gvSolPendientes.Rows[e.RowIndex].Cells[3].FindControl("ddlGestor") as DropDownList;
        if (ddlGestores != null)
        {
            DataTable dtSolicitudes = Session[_sessionVarNameSolicitudes] as DataTable;
            dtSolicitudes.Rows[e.RowIndex]["id_gestor"] = ddlGestores.SelectedValue;
            if (ddlGestores.SelectedValue != "")
                dtSolicitudes.Rows[e.RowIndex]["gestor"] = ddlGestores.SelectedItem.Text;
            else
                dtSolicitudes.Rows[e.RowIndex]["gestor"] = "";
            gvSolPendientes.EditIndex = -1;
            GridViewDataBind();
        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvSolPendientes.EditIndex = e.NewEditIndex;
        HiddenField hdnCodGestor = (HiddenField)gvSolPendientes.Rows[e.NewEditIndex].Cells[3].FindControl("hdnCodGestor");
        GridViewDataBind();
        DropDownList ddlGestores = (DropDownList)gvSolPendientes.Rows[e.NewEditIndex].Cells[3].FindControl("ddlGestor");
        if (ddlGestores.Items.Count == 0)
        {
            DataTable dtGestores = Session[_sessionVarNameGestores] as DataTable;
            ddlGestores.DataSource = dtGestores;
            ddlGestores.DataValueField = dtGestores.Columns[0].ColumnName;
            ddlGestores.DataTextField = dtGestores.Columns[1].ColumnName;
            ddlGestores.DataBind();
            ddlGestores.Items.FindByValue(hdnCodGestor.Value).Selected = true;
        }
    }


    protected void ButtonSaveClick(object sender, EventArgs e)
    {
        RegistrarAsignaciones();
    }
    protected void btnVerErrores_Click(object sender, EventArgs e)
    {
        gvErrores.Visible = true;
        btnHideErrores.Visible = true;
        btnVerErrores.Visible = false;
    }
    protected void btnHideErrores_Click(object sender, EventArgs e)
    {
        gvErrores.Visible = false;
        btnHideErrores.Visible = false;
        btnVerErrores.Visible = true;
    }
    protected void btnSave_Load(object sender, EventArgs e)
    {
        btnSave.ButtonClickDemo += ButtonSaveClick;
    }

    public void LogOut()
    {
        Session.RemoveAll();
        Session.Abandon();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }
}
