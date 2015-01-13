using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Collections;

public partial class _Default : System.Web.UI.Page
{
    private static ArrayList rolesUsuario;
    private string currentUser;

    protected void Page_Load(object sender, EventArgs e)
    {
        //this.hypRegistrarUsuario.NavigateUrl = "Seguridad/RegUsuario.aspx?" + AEA.Parametros.QueryStringParams.TipoEmpresa + "=" 
        //    + ((int)AEA.Parametros.TipoEmpresa.Comercializadora).ToString() +
        //    "&" + AEA.Parametros.QueryStringParams.returl + "=" + Page.AppRelativeVirtualPath;
        this.currentUser = User.Identity.Name.ToString();
        LoadUserRoles();
        CreateControls();
    }

    protected void LoadUserRoles()
    {
        SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(this.currentUser, ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        oUsuario.LoadRolesByUser();
        rolesUsuario = oUsuario.Roles;
    }

    protected void CreateControls()
    {

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(Server.MapPath("~/menuContent.xml"));
        foreach (XmlNode root in xmlDoc.ChildNodes)
        {
            if (root.Name == "AEA")
            {
                foreach (XmlNode entidad in root.ChildNodes)
                {
                    if (isMenuItemAccessAllowed(entidad.Attributes.GetNamedItem("Roles").Value.Split(',')))
                    {
                        Panel pn = new Panel();
                        LiteralControl divMain = new LiteralControl("<div style=\"float:left; width:41%; margin: 50px 10px 20px 20px; height: 98px;\">");
                        pn.Controls.Add(divMain);
                        LiteralControl divSub1 = new LiteralControl("<div style=\"background-color:#729cd6\">" + entidad.Attributes.GetNamedItem("Name").Value + "</div>");
                        pn.Controls.Add(divSub1);

                        LiteralControl listMain = new LiteralControl("<ul style=\"list-style-image: url('img/ico_bullet.gif'); padding-left:20px;\">");
                        pn.Controls.Add(listMain);

                        Panel pnLevel2 = new Panel();
                        foreach (XmlNode hijoEntidad in entidad.ChildNodes)
                        {
                            if (isMenuItemAccessAllowed(hijoEntidad.Attributes.GetNamedItem("Roles").Value.Split(',')))
                            {
                                if (hijoEntidad.HasChildNodes)//subentidad (unica que tiene que tener hijos)
                                {
                                    LiteralControl divSubEntidadTitle = new LiteralControl("<div style=\"background-color:#c6daf6; margin-left:20px;\">" + hijoEntidad.Attributes.GetNamedItem("Name").Value + "</div>");
                                    pnLevel2.Controls.Add(divSubEntidadTitle);
                                    LiteralControl listSub = new LiteralControl("<ul style=\"list-style-image: url('img/ico_bullet.gif'); padding-left:40px;\">");
                                    pnLevel2.Controls.Add(listSub);
                                    foreach (XmlNode hijoSubentidad in hijoEntidad.ChildNodes)
                                    {//añadir link
                                        if (isMenuItemAccessAllowed(hijoSubentidad.Attributes.GetNamedItem("Roles").Value.Split(',')))
                                        {
                                            pnLevel2.Controls.Add(GenerateListItem(getHrefElement(
                                                hijoSubentidad.Attributes.GetNamedItem("Name").Value,
                                                hijoSubentidad.Attributes.GetNamedItem("URL").Value)));
                                        }
                                    }
                                    LiteralControl listSubClose = new LiteralControl("</ul>");
                                    pnLevel2.Controls.Add(listSubClose);
                                }
                                else//añadir link
                                {
                                    pn.Controls.Add(GenerateListItem(getHrefElement(hijoEntidad.Attributes.GetNamedItem("Name").Value,
                                        hijoEntidad.Attributes.GetNamedItem("URL").Value)));
                                }
                            }
                        }

                        LiteralControl listMainClose = new LiteralControl("</ul>");
                        pn.Controls.Add(listMainClose);
                        pn.Controls.Add(pnLevel2);
                        LiteralControl divMainClose = new LiteralControl("</div>");
                        this.divContent.Controls.Add(divMainClose);
                        this.divContent.Controls.Add(pn);
                        //Response.Write(entidad.Name + "<br />");
                    }
                }
            }
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

    protected string getHrefElement(string text, string url)
    {
        return "<a href=\"" + url + "\">" + text + "</a>";
    }

    protected LiteralControl GenerateListItem(string itemText)
    {
        LiteralControl listItem = new LiteralControl("<li>" + itemText + "</li>");
        return listItem;
    }
}