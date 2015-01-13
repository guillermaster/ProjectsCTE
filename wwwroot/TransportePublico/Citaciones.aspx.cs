using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Citaciones : System.Web.UI.Page
{ 

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Utilities.Utils.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Default.aspx");
            }

            if (Request.QueryString.Count >= 2)
            {
                SetTitle(Request.QueryString[1]);
                ConsultarCitaciones(Request.QueryString[0], Request.QueryString[1]);
            }
            else
                Response.Redirect("~/Default.aspx");
        }
    }


    protected void SetTitle(string pagadas)
    {
        Label lblMasterPageTitle = (Label)Master.FindControl("lblTitle");

        if (pagadas == TransPublico.Parametros.CitacionPagada.S.ToString())
            lblMasterPageTitle.Text = "Consulta de Citaciones Pendientes de Pago";
        else
            lblMasterPageTitle.Text = "Consulta de Citaciones Pagadas";
    }


    protected void ConsultarCitaciones(string tipoOrgTransPub, string pagadas)
    {
    }
}
