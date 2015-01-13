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

public partial class ConsDeudaCompradoresAuto : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.btnPrint.SetupPrintingElement("divContent", 600, 400);
        }
    }

    protected void ConsultarInfracciones(string id, string tipoId)
    {
        Infracciones oCitac = new Infracciones(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        object[] oDatos = oCitac.InfraccionesPendientes(id, tipoId);

        Int64 NumElem = oDatos.GetLength(0);

        if (NumElem > 0)
        {
            this.divWarning.Visible = false;
            this.divError.Visible = false;

            if ((((object[])oDatos[0])[0].ToString()) != "Error")
            {

                DataTable oTabInf = new DataTable("Infracciones");
                oTabInf.Columns.Add("num_infraccion");
                oTabInf.Columns.Add("identificacion");
                oTabInf.Columns.Add("fec_infraccion");
                oTabInf.Columns.Add("placa");
                oTabInf.Columns.Add("tipo");
                oTabInf.Columns.Add("cod_contravencion");
                oTabInf.Columns.Add("puntos");
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
                    oRegistros[6] = (((object[])oDatos[iIndice])[6].ToString());//puntos
                    oRegistros[7] = (((object[])oDatos[iIndice])[7].ToString());//descripcion articulo
                    oRegistros[8] = (((object[])oDatos[iIndice])[8].ToString());//valor_citacion
                    oRegistros[9] = (((object[])oDatos[iIndice])[9].ToString());//valor_multa
                    oRegistros[10] = (((object[])oDatos[iIndice])[10].ToString());//valor_pagar
                    oTabInf.Rows.Add(oRegistros);
                    total_pagar += Convert.ToDecimal(oRegistros[10]);
                }
                this.Label1.Visible = true;
                this.Label2.Visible = true;
                this.lblTotPagar.Visible = true;
                this.lblTotPendientes.Visible = true;
                this.lblTotPendientes.Text = NumElem.ToString();
                this.lblTotPagar.Text = "$ " + total_pagar.ToString();
                this.gvDeudas.DataSource = oTabInf;
                this.gvDeudas.DataBind();
            }
            else//existio un error en la base
            {
                ShowFailureMessage("ERROR: " + (((object[])oDatos[0])[1].ToString()));
            }
        }
        else
        {
            //ClearControls();
            ShowFailureMessage("Usuario con identificación " + this.txtCedula.Text + " no registra infracciones pendientes de pago.");
        }


    }


    protected void ShowSuccessMessage(string message)
    {
        this.divError.Visible = false;
        this.divWarning.Visible = true;
        this.lblMsgWarning.Text = message;
    }


    protected void ShowFailureMessage(string message)
    {
        this.divWarning.Visible = false;
        this.divError.Visible = true;
        this.lblMsgError.Text = message;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ConsultarInfracciones(this.txtCedula.Text, "I");
    }
}
