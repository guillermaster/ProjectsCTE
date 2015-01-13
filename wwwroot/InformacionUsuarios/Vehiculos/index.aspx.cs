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
using CifradoCs;

public partial class Consultas_Matriculas_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Seguridad.UsuarioWeb.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }

        if (!Page.IsPostBack)
        {
            Page.Form.DefaultFocus = this.txtIdentificacion.ID;
            Page.Form.DefaultButton = this.btnConsultar.ID;
        }
    }


    protected void ConsultarVehiculos(string id)
    {
        MatriculacionVehicular oMatric = new MatriculacionVehicular(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtVehiculos = new DataTable();
        
        dtVehiculos = oMatric.VehiculosPorPersona(id);

        if (dtVehiculos.Rows.Count > 0)
        {
            this.lblMensaje.Text = "";
            this.gvVehiculos.DataSource = dtVehiculos;
            this.gvVehiculos.DataBind();
        }
        else
        {
            this.gvVehiculos.DataSource = null;
            this.gvVehiculos.DataBind();
            this.lblMensaje.Text = "No registra ningún vehículo a su nombre";
        }
    }


    protected void gvVehiculos_SelectedIndexChanged(object sender, EventArgs e)
    {
        string placa = this.gvVehiculos.SelectedRow.Cells[0].Text.ToString();
        Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        placa = objCrypto.CifrarCadena(placa);
        Session["placaCurrDetails"] = placa;
        Response.Redirect("Details.aspx");
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ConsultarVehiculos(this.txtIdentificacion.Text); //consultar por licencia
    }
}
