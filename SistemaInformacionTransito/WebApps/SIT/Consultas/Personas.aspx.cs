using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Consultas_DatosLicencia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Master.Title = "Datos de Persona";
        }
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        /*pnlDatosLicencia.Visible = false;
        gvPersonas.Visible = false;

        if (radLic.Checked)
        {
            if (ConsultarDatosLicencia(txtNumLicencia.Text))
                ConsultarVehiculosPorPersona(txtNumLicencia.Text);
        }
        else
            ConsultarPersonas(txtNombre.Text);*/
    }

    protected void grdVehiculos_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("DatosVehiculo.aspx?placa=" + grdVehiculos.SelectedRow.Cells[0].Text);
    }

    protected void gvPersonas_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPersonas.Visible = false;
        /*ConsultarDatosLicencia(gvPersonas.SelectedRow.Cells[0].Text);
        ConsultarVehiculosPorPersona(gvPersonas.SelectedRow.Cells[0].Text);*/
    }

}