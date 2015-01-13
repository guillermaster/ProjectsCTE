using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Brevetacion;
using Citaciones;


public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            this.resultados.Visible = false;
        }
        else
        {
            clear();
            if(this.txtLicencia.Text.Trim().Length > 0)
                ConsultarDatosLicencia(this.txtLicencia.Text.Trim());
        }
    }

    protected void ConsultarDatosLicencia(string noLicencia)
    {
        this.lblMensaje.Text = "";
        try
        {
            Licencia oLicencia = new Licencia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            object[] oDatos = oLicencia.ConsultarLicencia(noLicencia);
            if (oDatos[0].ToString() == "N")
            {
                this.lblMensaje.Text = oDatos[1].ToString().ToUpper();
                this.resultados.Visible = false;
            }
            else
            {                
                this.lblResLicencia.Text = noLicencia;
                this.lblResNombre.Text = oDatos[1].ToString();
                this.resultados.Visible = true;
                               
                #region Llenar Tablas
                DataRow oRegistros;
                DataTable oTabLic = new DataTable();
                oTabLic.Columns.Add("Categoría");
                oTabLic.Columns.Add("Fecha/Emisión");
                oTabLic.Columns.Add("Fecha/Caducidad");
                oTabLic.Columns.Add("Tipo de Sangre");

                DataTable oTabBloq = new DataTable();
                oTabBloq.Columns.Add("Fecha");
                oTabBloq.Columns.Add("Descripción");


                DataTable oTabRes = new DataTable();
                oTabRes.Columns.Add("Fecha");
                oTabRes.Columns.Add("Descripción");

                Int64 NumElem = oDatos.GetLength(0);
                
                for (Int64 iIndice = 21; iIndice < NumElem; iIndice++)
                {
                    switch (((object[])oDatos[iIndice])[0].ToString())
                    {
                        case "LIC":
                            oRegistros = oTabLic.NewRow();
                            oRegistros[0] = (((object[])oDatos[iIndice])[1].ToString());
                            oRegistros[1] = Convert.ToDateTime((((object[])oDatos[iIndice])[2].ToString()));
                            oRegistros[2] = Convert.ToDateTime((((object[])oDatos[iIndice])[3].ToString()));
                            oRegistros[3] = oDatos[8];
                            oTabLic.Rows.Add(oRegistros);
                            break;
                        case "BLO":
                            oRegistros = oTabBloq.NewRow();
                            oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                            oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                            oTabBloq.Rows.Add(oRegistros);
                            break;
                        case "RES":
                            oRegistros = oTabRes.NewRow();
                            oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                            oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                            oTabRes.Rows.Add(oRegistros);
                            break;
                        //case "INF":
                        //    oRegistros = oTabInfr.NewRow();
                        //    oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                        //    oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                        //    oTabInfr.Rows.Add(oRegistros);
                        //    break;
                    }
                }
                this.grdLicencias.DataSource = oTabLic;
                this.grdLicencias.DataBind();
                this.grdBloqueos.DataSource = oTabBloq;
                this.grdBloqueos.DataBind();
                this.grdRestricciones.DataSource = oTabRes;
                this.grdRestricciones.DataBind();
                oDatos = null;

                if (oTabBloq.Rows.Count == 0)
                {
                    this.lblBloqueos.Text = "No registra bloqueos";
                }
                if (oTabRes.Rows.Count == 0)
                {
                    this.lblRestricciones.Text = "No registra restricciones";
                }
               
                #endregion

                ConsultarInfracciones(noLicencia);

            }
            //btnPersonales_Click(null, null);
        }
        catch (Exception ex)
        {
            this.lblMensaje.Text = "No es posible consultar datos de la licencia " + noLicencia;
            //this.lblMensaje.Text = ex.Message;
            //Iniciar();//clean all the web controls
            //this.imgFoto.ImageUrl = "~/images/NoDisponible.jpg";
        }
    }



    protected void ConsultarInfracciones(string id)
    {
        Infracciones oCitac = new Infracciones(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        object[] oDatos = oCitac.InfraccionesPendientes(id, "I");

        Int64 NumElem = oDatos.GetLength(0);
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

        if (NumElem > 0)
        {
            this.lblMensaje.Text = "";

            if ((((object[])oDatos[0])[0].ToString()) != "Error")
            {
                
                DataRow oRegistros;

                decimal total_pagar = 0;
                for (Int64 iIndice = 0; iIndice < NumElem; iIndice++)
                {
                    oRegistros = oTabInf.NewRow();
                    oRegistros[0] = (((object[])oDatos[iIndice])[0].ToString());
                    oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                    oRegistros[2] = (((object[])oDatos[iIndice])[4].ToString());
                    oRegistros[3] = (((object[])oDatos[iIndice])[3].ToString());
                    oRegistros[4] = (((object[])oDatos[iIndice])[1].ToString());
                    oRegistros[5] = (((object[])oDatos[iIndice])[5].ToString());
                    oRegistros[6] = (((object[])oDatos[iIndice])[6].ToString());
                    oRegistros[7] = (((object[])oDatos[iIndice])[7].ToString());
                    oRegistros[8] = (((object[])oDatos[iIndice])[8].ToString());
                    oRegistros[9] = (((object[])oDatos[iIndice])[9].ToString());
                    oTabInf.Rows.Add(oRegistros);
                    total_pagar += Convert.ToDecimal(oRegistros[9]);
                }
                
                
                this.lblTotPagar.Text = "$ " + total_pagar.ToString();
                this.lblTotPagar.Visible = true;
                this.lblEtiqTotPagar.Visible = true;
                this.lblTitInfPend.Visible = true;
                this.GrdInfrac.Visible = true;
                
            }
            else//existio un error en la base
            {
                this.lblMensaje.Text = "ERROR: " + (((object[])oDatos[0])[1].ToString());
            }
            
        }
        else
        {
                this.lblTotPagar.Visible = false;
                this.lblEtiqTotPagar.Visible = false;
                this.lblTitInfPend.Visible = false;
                this.GrdInfrac.Visible = false;
            this.lblInfraccionesPendientes.Text = "No registra infracciones pendientes.";
        }

        GrdInfrac.DataSource = oTabInf;
        GrdInfrac.DataBind();

    }

    protected void clear()
    {
        this.lblMensaje.Text = "";
        this.lblBloqueos.Text = "";
        this.lblTotPagar.Text = "";
        this.lblRestricciones.Text = "";
        this.lblInfraccionesPendientes.Text = "";
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        this.resultados.Visible = false;
    }
    
}
