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
using Reporte;


public partial class ConsultaLicencias : System.Web.UI.Page
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

            Page.Form.DefaultButton = this.btnConsultaByCedula.ID;
            txtNombre.Attributes["onclick"] = "this.value=''";

            if(Request.QueryString.Count > 0)
            {
                this.txtNombre.Text = Request.QueryString[0];
                Consultar();
            }            
        }
    }
    protected void btnConsultaByCedula_Click(object sender, EventArgs e)
    {
        Consultar();
    }


    public void Consultar()
    {
        SecretariaGeneral.Contratos oContratos = new SecretariaGeneral.Contratos(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtPersonas = oContratos.ConsultarPersonas(txtNombre.Text, "ConsultaLicencias.aspx");
        GridView1.DataSource = dtPersonas;
        GridView1.DataBind();
    }

}
