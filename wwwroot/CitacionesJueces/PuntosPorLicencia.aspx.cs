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

public partial class PuntosPorLicencia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.DefaultButton = this.btnConsultar.ID;
        Page.Form.DefaultFocus = this.txtCedula.ID;
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Infracciones oInfracciones = new Infracciones(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        this.lblPuntosPerdidos.Text = oInfracciones.PuntosPorLicencia(this.txtCedula.Text);
        if (this.lblPuntosPerdidos.Text == "")
        {
            this.lblError.Text = oInfracciones.Error;
            this.divPuntos.Visible = false;
        }
        else
        {
            this.lblTotalPuntos.Text = (oInfracciones.PuntosInicioLicencia() - int.Parse(this.lblPuntosPerdidos.Text)).ToString();
            this.divPuntos.Visible = true;
        }
    }
}
