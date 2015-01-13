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
        if (!SeguridadWebAppInt.Seguridad.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }

        if (!IsPostBack)
        {
            this.UserLoggedIn1.Username = HttpContext.Current.User.Identity.Name.ToString();
            //SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.UserLoggedIn1.Username, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            //oUsuario.LoadRolesByUser();
            //rolesUsuario = oUsuario.Roles;
            if (Session[TransPublico.Parametros.DatosSesion.NombreEmpresa.ToString()] != null)
                this.UserLoggedIn1.Empresa = Session[TransPublico.Parametros.DatosSesion.NombreEmpresa.ToString()].ToString();
        }
        SetPageTitle();
    }

    public void SetPageTitle()
    {
        //string pageFileName = System.IO.Path.GetFileName(this.ContentPlaceHolder1.Page.AppRelativeVirtualPath);
        //switch (pageFileName)
        //{
        //    case "Comercializadora.aspx":
        //        this.lblTitle.Text = "Creaci�n de Nueva Comercializadora";
        //        break;
        //    case "Sucursal.aspx":
        //        this.lblTitle.Text = "Creaci�n de Nueva Sucursal de Comercializadora";
        //        break;
        //    case "ConsComercializadoras.aspx":
        //        this.lblTitle.Text = "Consulta de Comercializadoras";
        //        break;
        //    case "ConsDeudaCompradoresAuto.aspx":
        //        this.lblTitle.Text = "Consulta de Deudas en CTG por Infracciones";
        //        break;
        //    case "ConsGestores.aspx":
        //        this.lblTitle.Text = "Consulta de Gestores";
        //        break;
        //    case "ConsMatriculasEmitidas.aspx":
        //        this.lblTitle.Text = "Consulta de Matr�culas Emitidas";
        //        break;
        //    case "ConsModeloAutomotor.aspx":
        //        this.lblTitle.Text = "Consulta de Automotores";
        //        break;
        //    case "ConsModeloAutomotorLista.aspx":
        //        this.lblTitle.Text = "Consulta de Automotores";
        //        break;
        //    case "ConsSolMatricula.aspx":
        //        this.lblTitle.Text = "Solicitud de Matr�cula";
        //        break;
        //    case "ConsSolMatriculas.aspx":
        //        this.lblTitle.Text = "Consulta de Solicitudes de Matr�cula";
        //        break;
        //    case "ConsSolMatriculasCTG.aspx":
        //        this.lblTitle.Text = "Consulta de Solicitudes de Matr�culas Recibidas en CTG";
        //        break;
        //    //case "Default.aspx":
        //    //    this.lblTitle.Text = "";
        //    //    break;
        //    case "ModeloAutomotor.aspx":
        //        this.lblTitle.Text = "Creaci�n de Nuevo Automotor";
        //        break;
        //    case "RegGestores.aspx":
        //        this.lblTitle.Text = "Creaci�n de Nuevo Gestor";
        //        break;
        //    case "RegSolMatricula.aspx":
        //        this.lblTitle.Text = "Creaci�n de Solicitud de Matr�cula";
        //        break;
        //    case "AsignaAutomotorComerc.aspx":
        //        this.lblTitle.Text = "Asignaci�n de Automotores a Sucursales";
        //        break;
        //    case "AsignaGestorSolMatricula.aspx":
        //        this.lblTitle.Text = "Asignaci�n de Gestores a Solicitudes de Matr�cula";
        //        break;
        //    case "ConsSucursales.aspx":
        //        this.lblTitle.Text = "Consulta de Sucursales";
        //        break;
        //    case "RegUsuario.aspx":
        //        this.lblTitle.Text = "Registro de Usuario";
        //        break;
        //    case "ConsUsuarios.aspx":
        //        this.lblTitle.Text = "Consulta de Usuarios";
        //        break;
        //}
    }

    //protected void OnMenuItemDataBound(object sender, MenuEventArgs e)
    //{
    //    MenuItem menuItem = e.Item;
    //    if (menuItem.Text != "AEA")
    //    {
    //        string[] rolesPermitidos = menuItem.Value.Split(',');
    //        if (!isMenuItemAccessAllowed(rolesPermitidos))
    //            DeleteItemFromMenu(menuItem);
    //    }
    //}

    //protected bool isMenuItemAccessAllowed(string[] rolesPermitidosEnItem)
    //{
    //    for (int i = 0; i < rolesUsuario.Count; i++)
    //    {
    //        for (int j = 0; j < rolesPermitidosEnItem.Length; j++)
    //        {
    //            if (rolesUsuario[i].ToString().Trim() == rolesPermitidosEnItem[j].Trim())
    //                return true;
    //        }
    //    }
    //    return false;
    //}

    //protected void DeleteItemFromMenu(MenuItem menuItem)
    //{
    //    if (menuItem.Parent == null)

    //        Menu1.Items.Remove(menuItem);

    //    else

    //        menuItem.Parent.ChildItems.Remove(menuItem);
    //}

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        FormsAuthentication.SignOut();
        Response.Redirect("~/Default.aspx");
    }

}
