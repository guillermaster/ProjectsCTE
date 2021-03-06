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
using Citaciones;

public partial class Consultas_Citaciones_InfraccionesPendientes : System.Web.UI.Page
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

    protected void ConsultarInfracciones(string id, string tipoId)
    {
        Infracciones oCitac = new Infracciones(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        object[] oDatos = oCitac.InfraccionesPendientes(id, tipoId);

        Int64 NumElem = oDatos.GetLength(0);

        if (NumElem > 0)
        {
            this.lblMensaje.Text = "";

            if ((((object[])oDatos[0])[0].ToString()) != "Error")
            {

                DataTable oTabInf = new DataTable("Infracciones");
                oTabInf.Columns.Add("num_infraccion");
                oTabInf.Columns.Add("identificacion");
                oTabInf.Columns.Add("fec_infraccion");
                oTabInf.Columns.Add("placa");
                oTabInf.Columns.Add("tipo");
                oTabInf.Columns.Add("cod_contravencion");
                oTabInf.Columns.Add("contravencion");
                oTabInf.Columns.Add("val_contrav");
                oTabInf.Columns.Add("mul_contrav");
                oTabInf.Columns.Add("total");
                DataRow oRegistros;

                decimal total_pagar = 0;
                for (Int64 iIndice = 0; iIndice < NumElem; iIndice++)
                {
                    oRegistros = oTabInf.NewRow();
                    oRegistros[0] = (((object[])oDatos[iIndice])[0].ToString());//cod_citacion
                    oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());//identificacion_persona
                    oRegistros[2] = (((object[])oDatos[iIndice])[4].ToString());//fecha_citacion
                    oRegistros[3] = (((object[])oDatos[iIndice])[3].ToString());//placa
                    oRegistros[4] = (((object[])oDatos[iIndice])[1].ToString());//tipo_licencia
                    oRegistros[5] = (((object[])oDatos[iIndice])[5].ToString());//articulo
                    oRegistros[6] = (((object[])oDatos[iIndice])[6].ToString());//descripcion articulo
                    oRegistros[7] = (((object[])oDatos[iIndice])[7].ToString());//valor_citacion
                    oRegistros[8] = (((object[])oDatos[iIndice])[8].ToString());//valor_multa
                    oRegistros[9] = (((object[])oDatos[iIndice])[9].ToString());//valor_pagar
                    oTabInf.Rows.Add(oRegistros);
                    total_pagar += Convert.ToDecimal(oRegistros[9]);
                }
                this.Label1.Visible = true;
                this.Label2.Visible = true;
                this.lblTotPagar.Visible = true;
                this.lblTotPendientes.Visible = true;
                this.lblTotPendientes.Text = NumElem.ToString();
                this.lblTotPagar.Text = "$ " + total_pagar.ToString();
                GrdInfrac.DataSource = oTabInf;
                GrdInfrac.DataBind();
            }
            else//existio un error en la base
            {
                this.lblMensaje.Text = "ERROR: " + (((object[])oDatos[0])[1].ToString());
                this.Label1.Visible = false;
                this.Label2.Visible = false;
                this.lblTotPagar.Visible = false;
                this.lblTotPendientes.Visible = false;
                GrdInfrac.DataSource = null;
                GrdInfrac.DataBind();
                
            }
        }
        else
        {
            //ClearControls();
            this.lblMensaje.Text = "No registra infracciones pendientes.";
            this.Label1.Visible = false;
            this.Label2.Visible = false;
            this.lblTotPagar.Visible = false;
            this.lblTotPendientes.Visible = false;
            GrdInfrac.DataSource = null;
            GrdInfrac.DataBind();
            
        }

        
    }

    public void ClearControls()
    {
        this.Label1.Visible = false;
        this.Label2.Visible = false;
        this.lblTotPagar.Visible = false;
        this.lblTotPendientes.Visible = false;
        GrdInfrac.DataSource = null;
        GrdInfrac.DataBind();
    }


    protected void GrdInfrac_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable oDetallesCitacion = new DataTable();
        oDetallesCitacion.Columns.Add("Descripci�n:");
        oDetallesCitacion.Columns.Add("Datos del veh�culo:");
        oDetallesCitacion.Columns.Add("Propietario del veh�culo:");
        oDetallesCitacion.Columns.Add("Uniformado:");

        DataRow row = oDetallesCitacion.NewRow();
        row[0] = this.GrdInfrac.SelectedItem.Cells[6].Text.ToString();

        Infracciones oCitac = new Infracciones(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        string[] datosUniformado = oCitac.ObtenerUniformadoSancionador(this.GrdInfrac.SelectedItem.Cells[0].Text.ToString());
        if (datosUniformado[0] != "error")
        {
            row[3] = datosUniformado[0] + " - " + datosUniformado[1];
        }
        else
        {
            row[3] = "Datos no disponibles";
        }

        if (this.GrdInfrac.SelectedItem.Cells[4].Text.ToString() != "&nbsp;")
        {
            string[] datosVehiculo = oCitac.ObtenerDatosBasicosVehiculo(this.GrdInfrac.SelectedItem.Cells[4].Text.ToString());
            if (datosVehiculo[0] != "error")
            {
                row[2] = datosVehiculo[0];
                row[1] = datosVehiculo[1] + " - " + datosVehiculo[2] + " - " + datosVehiculo[3] + " - " + datosVehiculo[4];
            }
            else
            {
                row[2] = "Datos no disponibles";
                row[1] = "Datos no disponibles";
            }
        }
        else
        {
            row[2] = "Datos no disponibles";
            row[1] = "Datos no disponibles";
        }

        oDetallesCitacion.Rows.Add(row);

        this.dvDetalleCitacion.DataSource = oDetallesCitacion;
        this.dvDetalleCitacion.DataBind();
        this.dvDetalleCitacion.Visible = true;
        this.btnCloseDetails.Visible = true;
    }



    protected void btnCloseDetails_Click(object sender, ImageClickEventArgs e)
    {
        this.btnCloseDetails.Visible = false;
        this.dvDetalleCitacion.Visible = false;
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ConsultarInfracciones(this.txtIdentificacion.Text, "I"); //consultar por licencia
    }
}
