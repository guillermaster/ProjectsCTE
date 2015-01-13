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
using System.Web.SessionState;
using Canchon;

public partial class VehiculosDetenidos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Seguridad.UsuarioWeb.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/DefaultConsultas.aspx");
            }
            this.Form.DefaultButton = this.btnConsultar.ID;
            this.Form.DefaultFocus = this.txtIdentificacion.ID;
        }
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        CanchonClass canchon = new CanchonClass(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable vehiculosRetenidos;


        vehiculosRetenidos = canchon.ConsultarVehiculo(this.rbTipoIdentificacion.SelectedValue, this.txtIdentificacion.Text);
        if (vehiculosRetenidos.Rows.Count == 0)
        {
            if (this.rbTipoIdentificacion.SelectedValue == "licencia")
                this.lblEstado.Text = "No registra ningún vehículo retenido.";
            else
                this.lblEstado.Text = "No se encuentra en el canchón.";
            this.gvVehicRet.Visible = false;
            this.hypInfoCanchon1.Visible = false;
        }
        else
        {
            this.lblEstado.Text = "";
            this.gvVehicRet.DataSource = vehiculosRetenidos;
            this.DataBind();
            this.gvVehicRet.Visible = true;
            this.hypInfoCanchon1.Visible = true;
        }

    }
   
}
