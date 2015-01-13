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

public partial class ConsultaAudiencias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Auxiliar.Helper.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Default.aspx");
            }
        }
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Auxiliar.CitacionesJueces oCitacJueces = new Auxiliar.CitacionesJueces(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtGridData = oCitacJueces.GetAudiencias(oCitacJueces.GetJuzgado(User.Identity.Name.ToString()), this.txtFechaDesde.Text);
        this.GridView1.DataSource = dtGridData;
        this.GridView1.DataBind();
    }
}
