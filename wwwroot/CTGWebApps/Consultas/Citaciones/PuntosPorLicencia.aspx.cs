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
using Citaciones;

public partial class Consultas_Citaciones_PuntosPorLicencia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Seguridad.UsuarioWeb.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/DefaultConsultas.aspx");
        }
        ConsultarPuntos(User.Identity.Name.ToString());
    }

    protected void ConsultarPuntos(string id)
    {
        this.lblLicencia.Text = id;
        Infracciones oCitac = new Infracciones(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        //string puntos_perdidos = oCitac.PuntosPerdidosPorLicencia(id);
        string puntos_perdidos = oCitac.PuntosPorLicencia(id);
        if (puntos_perdidos != null)
        {
            this.lblPuntosPerdidos.Text = puntos_perdidos;
            int puntos_inicio = oCitac.PuntosInicioLicencia();
            if (puntos_inicio != -1)
            {
                //float puntos = puntos_inicio - float.Parse(puntos_perdidos);
                //this.lblTotalPuntos.Text = puntos.ToString();
                this.lblTotalPuntos.Text = puntos_inicio.ToString();
            }
        }
        
    }
}
