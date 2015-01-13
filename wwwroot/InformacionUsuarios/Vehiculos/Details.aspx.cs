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
using CifradoCs;
using Matriculacion;


public partial class Consultas_Matriculas_index : System.Web.UI.Page
{
    private MatriculacionVehicular oMatric;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session[Constantes.UsuarioWeb.SessionVarNameFullAccess].ToString() == "NO")
        //{
        //    Response.Redirect("../../sinPermiso.aspx");
        //}

        if (!Page.IsPostBack)
        {
            try
            {
                if (!Seguridad.UsuarioWeb.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
                {
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Default.aspx");
                }
                Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                this.txtPlaca.Text = objCrypto.DescifrarCadena(Session["placaCurrDetails"].ToString());
                this.oMatric = new MatriculacionVehicular(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
                string[] datosVehiculo = oMatric.DatosAvanzadosVehiculo(this.txtPlaca.Text.ToString());
                #region "Asignar datos"
                this.txtChasis.Text = datosVehiculo[0];
                this.txtAno.Text = datosVehiculo[1];
                this.txtTonelaje.Text = datosVehiculo[2];
                this.txtCAVMCPN.Text = datosVehiculo[3];
                this.txtMotor.Text = datosVehiculo[4];
                this.txtPaisOrigen.Text = datosVehiculo[5];
                this.txtTipo.Text = datosVehiculo[6];
                this.txtCilindraje.Text = datosVehiculo[7];
                this.txtServicio.Text = datosVehiculo[8];
                this.txtClase.Text = datosVehiculo[13];
                this.txtMarca.Text = datosVehiculo[14];
                this.txtModelo.Text = datosVehiculo[15];
                this.txtColor.Text = datosVehiculo[16];
                if (datosVehiculo[8] == "PARTICULAR")
                {
                    this.Menu1.Items[1].Enabled = false;
                }
                else
                {
                    this.txtNoPasajeros.Text = datosVehiculo[9];
                    this.txtModalidad.Text = datosVehiculo[10];
                    this.txtCooperativa.Text = datosVehiculo[11];
                    this.txtClaseServicio.Text = datosVehiculo[12];
                }

                this.txtNumSOAT.Text = datosVehiculo[17];
                this.txtEmpSOAT.Text = datosVehiculo[18];
                this.txtFechaIniSOAT.Text = datosVehiculo[19];
                this.txtFechaFinSOAT.Text = datosVehiculo[20];

                #endregion
            }
            catch (Exception ex)
            {
                Response.Redirect("index.aspx");
            }
        }
        Menu1.Items[0].Selected = true;
    }


    protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
    {
        changeTab(Convert.ToInt32(e.Item.Value.ToString()));
    }


    protected void changeTab(int nTab)
    {
        MultiView1.ActiveViewIndex = nTab;
        int i;
        //Make the selected menu item reflect the correct imageurl
        for (i = 0; i < Menu1.Items.Count; i++)
        {
            if (i == nTab)
            {
                Menu1.Items[i].ImageUrl = "../../img/tab" + i.ToString() +"_on.gif";
            }
            else
            {
                Menu1.Items[i].ImageUrl = "../../img/tab" + i.ToString() + "_off.gif";
            }
        }

        switch (nTab)
        {
            case 2:
                this.oMatric = new MatriculacionVehicular(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
                DataTable dtHistMatric = oMatric.HistorialMatriculacion(this.txtPlaca.Text.ToString());
                this.gvHistMatricula.DataSource = dtHistMatric;
                this.gvHistMatricula.DataBind();
                if (dtHistMatric.Rows.Count == 0)
                    this.lblMensajeMatriculas.Text = "Su vehículo con placa " + this.txtPlaca.Text.ToString() + " no registra historial de matriculación";
                else
                    this.lblMensajeMatriculas.Text = "";
                break;
            case 3:
                this.oMatric = new MatriculacionVehicular(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
                DataTable dtBloqueos = oMatric.BloqueosPorVehiculo(this.txtPlaca.Text.ToString());
                this.gvBloqueos.DataSource = dtBloqueos;
                this.gvBloqueos.DataBind();
                if (dtBloqueos.Rows.Count == 0)
                    this.lblMensajeBloqueos.Text = "Su vehículo con placa " + this.txtPlaca.Text.ToString() + " no registra bloqueos";
                else
                    this.lblMensajeBloqueos.Text = "";
                break;
        }
    }
}
