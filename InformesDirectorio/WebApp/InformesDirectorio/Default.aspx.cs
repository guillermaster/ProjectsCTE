using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDocumentsFolders(HttpContext.Current.User.Identity.Name,
                int.Parse(Session[InformesDirectorioExtra.Parametros.Session.FolderId].ToString()));
            if (Session[InformesDirectorioExtra.Parametros.Session.IsAdmin] != null)
            {
                if (Convert.ToBoolean(Session[InformesDirectorioExtra.Parametros.Session.IsAdmin]))
                    LoadAppUsers();
                else
                    HideCreateControls();
            }
            LoadAppUsers();
        }
    }


    protected void HideCreateControls()
    {
        btnNewDocument.Visible = false;
        btnNewFolder.Visible = false;
    }


    protected void LoadDocumentsFolders(string user, int idFolder)
    {
        InformesDirectorioExtra.Document objDoc = new InformesDirectorioExtra.Document(user,
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataSet dsFolderNDocs = objDoc.LoadDocumentsAndFolders(idFolder, ConfigurationManager.AppSettings[InformesDirectorioExtra.Parametros.InstitucionConfigPar]);
        if (dsFolderNDocs != null && dsFolderNDocs.Tables.Count > 1)
        {
            gvFolderList.DataSource = dsFolderNDocs.Tables[0];
            gvFolderList.DataBind();
            gvDocsList.DataSource = dsFolderNDocs.Tables[1];
            gvDocsList.DataBind();
        }
    }


    protected void btnNewFolder_Click(object sender, ImageClickEventArgs e)
    {
        HideSuccess();
        HideError();
        mpeNewFolder.Show();
    }


    protected void btnSaveNewFolder_Click(object sender, EventArgs e)
    {
        InformesDirectorioExtra.Folder objFolder = new InformesDirectorioExtra.Folder(HttpContext.Current.User.Identity.Name,
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        if (!string.IsNullOrWhiteSpace(txtFolderName.Text))
        {
            if (objFolder.CreateNewFolder(txtFolderName.Text, int.Parse(Session[InformesDirectorioExtra.Parametros.Session.FolderId].ToString()), ConfigurationManager.AppSettings[InformesDirectorioExtra.Parametros.InstitucionConfigPar]))
            {
                mpeNewFolder.Hide();
                LoadDocumentsFolders(HttpContext.Current.User.Identity.Name,
                    int.Parse(Session[InformesDirectorioExtra.Parametros.Session.FolderId].ToString()));
            }
            else
                SetError(objFolder.Error);
        }
        else
            SetError("Debe ingresar un nombre para la nueva carpeta");
    }


    protected void btnCancelNewFolder_Click(object sender, EventArgs e)
    {
        mpeNewFolder.Hide();
    }


    protected void btnNewDocument_Click(object sender, ImageClickEventArgs e)
    {
        HideSuccess();
        HideError();
        mpeNewDocument.Show();
    }


    protected void btnSaveNewDocument_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            byte[] fileBytes = FileUpload1.FileBytes;
            InformesDirectorioExtra.Document objDoc = new InformesDirectorioExtra.Document(HttpContext.Current.User.Identity.Name,
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
            if (objDoc.CreateNewDocument(FileUpload1.FileName, txtNewDocTitle.Text, fileBytes, int.Parse(Session[InformesDirectorioExtra.Parametros.Session.FolderId].ToString()), ConfigurationManager.AppSettings[InformesDirectorioExtra.Parametros.InstitucionConfigPar]))
            {
                mpeNewFolder.Hide();
                LoadDocumentsFolders(HttpContext.Current.User.Identity.Name,
                    int.Parse(Session[InformesDirectorioExtra.Parametros.Session.FolderId].ToString()));
            }
            else
                SetError(objDoc.Error);

        }
        else
            SetError("Debe seleccionar un archivo.");
    }

    protected void btnShare_Click(object sender, EventArgs e)
    {
        InformesDirectorioExtra.Document objDoc = new InformesDirectorioExtra.Document(HttpContext.Current.User.Identity.Name,
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        string sharingUserEmail = string.Empty;
        DataTable dtUsers = new DataTable();
        dtUsers.Columns.Add("user");
        dtUsers.Columns.Add("email");
        foreach (ListItem item in chkListUsersToShare.Items)
        {
            if (item.Selected)
            {
                if (item.Text != HttpContext.Current.User.Identity.Name)
                {
                    DataRow drUser = dtUsers.NewRow();
                    drUser[0] = item.Text;
                    drUser[1] = item.Value;
                    dtUsers.Rows.Add(drUser);
                }
            }
            if (item.Text.ToUpper() == HttpContext.Current.User.Identity.Name.ToUpper())
                sharingUserEmail = item.Value;
        }
        
        #region "Get item URL"
        string absUri = Request.Url.AbsoluteUri;
        absUri = absUri.Substring(0, absUri.LastIndexOf('/'));
        string url = absUri + "/" + ((HyperLink)gvDocsList.SelectedRow.Cells[2].Controls[0]).NavigateUrl;
        #endregion
        #region "Get item ID and Title"
        string id = gvDocsList.SelectedRow.Cells[1].Text;
        string title = gvDocsList.SelectedRow.Cells[3].Text;
        #endregion

        if (objDoc.Share(dtUsers, sharingUserEmail, Convert.ToInt32(id), title, url))
        {
            if (string.IsNullOrWhiteSpace(objDoc.Error))
                SetSuccess("El documento fue compartido exitósamente.");
            else
                SetError("Han ocurrido errores al compartir el documento: <br />" + objDoc.Error);
        }
        else
        {
            SetError("No se pudo compartir el documento.<br />" + objDoc.Error);
        }
    }

    public void SetError(string errorMsg)
    {
        lblError.Text = errorMsg;
        pnlError.Visible = true;
    }

    public void HideError()
    {
        pnlError.Visible = false;
    }

    public void SetSuccess(string msg)
    {
        lblSuccess.Text = msg;
        pnlSuccess.Visible = true;
    }

    public void HideSuccess()
    {
        pnlSuccess.Visible = false;
    }


    protected void gvDocsList_SelectedIndexChanged(object sender, EventArgs e)
    {
        HideSuccess();
        HideError();
        mpeSharing.Show();
        if (chkListUsersToShare.Items.Count == 0)
            LoadAppUsers();
    }

    protected void LoadAppUsers()
    {
        InformesDirectorioExtra.Users oUsers = new InformesDirectorioExtra.Users(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        
        DataTable dtUsers = oUsers.AppUsers(ConfigurationManager.AppSettings[InformesDirectorioExtra.Parametros.InstitucionConfigPar]);
        chkListUsersToShare.DataSource = dtUsers;
        chkListUsersToShare.DataTextField = dtUsers.Columns[0].ColumnName;
        chkListUsersToShare.DataValueField = dtUsers.Columns[1].ColumnName;
        chkListUsersToShare.DataBind();
    }


    protected void gvDocumentsRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        e.Row.Cells[1].Visible = false;
        int lastColumnIndex = e.Row.Cells.Count - 1;
        e.Row.Cells[lastColumnIndex].Visible = false;
        if (e.Row.Cells[lastColumnIndex].Text == InformesDirectorioExtra.Parametros.Sharing.Read)
            e.Row.Cells[lastColumnIndex - 2].Visible = false;
    }

    protected bool CanShareDocument(string codPriv)
    {
        if (codPriv == InformesDirectorioExtra.Parametros.Sharing.Read)
            return false;
        else
            return true;
    }

}