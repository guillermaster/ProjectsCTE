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

public partial class MasterPage : System.Web.UI.MasterPage
{
    private static ArrayList rolesUsuario;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.lblUsuario.Text = HttpContext.Current.User.Identity.Name.ToString();
            SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.lblUsuario.Text, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            oUsuario.LoadRolesByUser();
            rolesUsuario = oUsuario.Roles;
            try
            {
                this.lblEmpresa.Text = Session[AEA.Parametros.SessionVarNombreEmpresa].ToString();
            }
            catch
            {
                LogOut();
            }
        }
        SetPageTitle();
    }

    public void SetPageTitle()
    {
        string pageFileName = System.IO.Path.GetFileName(this.ContentPlaceHolder1.Page.AppRelativeVirtualPath);
        switch (pageFileName)
        {
            case "Comercializadora.aspx":
                this.lblTitle.Text = "Creación de Nueva Comercializadora";
                break;
            case "Sucursal.aspx":
                this.lblTitle.Text = "Creación de Nueva Sucursal de Comercializadora";
                break;
            case "ConsComercializadoras.aspx":
                this.lblTitle.Text = "Consulta de Comercializadoras";
                break;
            case "ConsDeudaCompradoresAuto.aspx":
                this.lblTitle.Text = "Consulta de Deudas en CTG por Infracciones";
                break;
            case "ConsGestores.aspx":
                this.lblTitle.Text = "Consulta de Gestores";
                break;
            case "ConsMatriculasEmitidas.aspx":
                this.lblTitle.Text = "Consulta de Matrículas Emitidas";
                break;
            case "ConsModeloAutomotor.aspx":
                this.lblTitle.Text = "Consulta de Automotores";
                break;
            case "ConsModeloAutomotorLista.aspx":
                this.lblTitle.Text = "Consulta de Automotores";
                break;
            case "ConsSolMatricula.aspx":
                this.lblTitle.Text = "Solicitud de Matrícula";
                break;
            case "ConsSolMatriculas.aspx":
                this.lblTitle.Text = "Consulta de Solicitudes de Matrícula";
                break;
            case "ConsSolMatriculasCTG.aspx":
                this.lblTitle.Text = "Consulta de Solicitudes de Matrículas Recibidas en CTG";
                break;
            //case "Default.aspx":
            //    this.lblTitle.Text = "";
            //    break;
            case "ModeloAutomotor.aspx":
                this.lblTitle.Text = "Creación de Nuevo Automotor";
                break;
            case "RegGestores.aspx":
                this.lblTitle.Text = "Creación de Nuevo Gestor";
                break;
            case "RegSolMatricula.aspx":
                this.lblTitle.Text = "Creación de Solicitud de Matrícula";
                break;
            case "AsignaAutomotorComerc.aspx":
                this.lblTitle.Text = "Asignación de Automotores a Sucursales";
                break;
            case "AsignaGestorSolMatricula.aspx":
                this.lblTitle.Text = "Asignación de Gestores a Solicitudes de Matrícula";
                break;
            case "ConsSucursales.aspx":
                this.lblTitle.Text = "Consulta de Sucursales";
                break;
            case "RegUsuario.aspx":
                this.lblTitle.Text = "Registro de Usuario";
                break;
            case "ConsUsuarios.aspx":
                this.lblTitle.Text = "Consulta de Usuarios";
                break;
        }
    }

    protected void OnMenuItemDataBound(object sender, MenuEventArgs e)
    {
        MenuItem menuItem = e.Item;
        if (menuItem.Text != "AEA")
        {
            string[] rolesPermitidos = menuItem.Value.Split(',');
            if (!isMenuItemAccessAllowed(rolesPermitidos))
                DeleteItemFromMenu(menuItem);
        }
    }

    protected bool isMenuItemAccessAllowed(string[] rolesPermitidosEnItem)
    {
        for (int i = 0; i < rolesUsuario.Count; i++)
        {
            for (int j = 0; j < rolesPermitidosEnItem.Length; j++)
            {
                if (rolesUsuario[i].ToString().Trim() == rolesPermitidosEnItem[j].Trim())
                    return true;
            }
        }
        return false;
    }

    protected void DeleteItemFromMenu(MenuItem menuItem)
    {
        if (menuItem.Parent == null)

            Menu1.Items.Remove(menuItem);

        else

            menuItem.Parent.ChildItems.Remove(menuItem);
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        LogOut();
    }

    public void LogOut()
    {
        Session.RemoveAll();
        Session.Abandon();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }

}
