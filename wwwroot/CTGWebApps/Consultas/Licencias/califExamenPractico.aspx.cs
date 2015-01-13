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

public partial class Consultas_Licencias_turnoExamenPractico : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Seguridad.UsuarioWeb.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/DefaultConsultas.aspx");
        }
        HideMessages();
        ConsultarCalificaciones(User.Identity.Name.ToString());
    }

    protected void ConsultarCalificaciones(string identificacion)
    {
        Brevetacion.ExamenPractico oExamenPractico = new Brevetacion.ExamenPractico(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtCalificaciones = oExamenPractico.GetCalificacionesExamenByIdUser(identificacion);
        if (dtCalificaciones.Rows.Count > 0)
        {
            this.gvCalifExamen.DataSource = dtCalificaciones;
        }
        else
        {
            this.gvCalifExamen.DataSource = null;
            if (oExamenPractico.Error != "")
            {
                this.divError.Visible = true;
                this.lblMsgError.Text = oExamenPractico.Error;
            }
            else
            {
                this.divWarning.Visible = true;
                this.lblMsgWarning.Text = "Usted no registra ningún examen práctico";
            }
        }
        this.gvCalifExamen.DataBind();
    }

    protected void HideMessages()
    {
        this.divError.Visible = false;
        this.divWarning.Visible = false;
    }
}
