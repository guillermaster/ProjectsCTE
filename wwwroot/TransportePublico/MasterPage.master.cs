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
            SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.UserLoggedIn1.Username, ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.usuario.ToString()], ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.clave.ToString()], ConfigurationManager.AppSettings[TransPublico.Parametros.BaseDatosKeys.tns.ToString()]);
            oUsuario.LoadRolesByUser();
            rolesUsuario = oUsuario.Roles;
            if (Session[TransPublico.Parametros.DatosSesion.NombreEmpresa.ToString()] != null)
                this.UserLoggedIn1.Empresa = Session[TransPublico.Parametros.DatosSesion.NombreEmpresa.ToString()].ToString();
        }
    }



    protected void OnMenuItemDataBound(object sender, MenuEventArgs e)
    {
        MenuItem menuItem = e.Item;
        if (menuItem.Value.Trim().Length > 0)
        {
            string[] rolesPermitidos = menuItem.Value.Split(',');
            if (!isMenuItemAccessAllowed(rolesPermitidos))
                DeleteItemFromMenu(menuItem);
        }
    }

    protected bool isMenuItemAccessAllowed(string[] rolesPermitidosEnItem)
    {
        if (rolesPermitidosEnItem.Length == 0)
            return true;
        else
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
        FormsAuthentication.SignOut();
        Response.Redirect("~/Default.aspx");
    }

}
