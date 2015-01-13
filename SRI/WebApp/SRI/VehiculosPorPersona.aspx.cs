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
using Matriculacion;

public partial class _Default : System.Web.UI.Page
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
            if (Request.QueryString.Count > 0)
            {
                Consultar(Request.QueryString[0]);
            }
        }
    }

    public void Consultar(string identificacion)
    {
        MatriculacionVehicular oMatric = new MatriculacionVehicular(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtVehiculos = new DataTable();

        dtVehiculos = oMatric.VehiculosPorPersona(identificacion);
        dtVehiculos.Columns.Add("URL");
        foreach (DataRow dr in dtVehiculos.Rows)
        {
            dr["URL"] = "ConsultaVehiculos.aspx?placa=" + dr["placa"];
        }

        GridView1.DataSource = dtVehiculos;

        if (dtVehiculos.Rows.Count == 0)
        {
            if (oMatric.Error == string.Empty)
                this.GridView1.EmptyDataText = "No registra ningún vehículo a su nombre";
            else
                this.GridView1.EmptyDataText = oMatric.Error;
        }

        GridView1.DataBind();
    }


    
}
