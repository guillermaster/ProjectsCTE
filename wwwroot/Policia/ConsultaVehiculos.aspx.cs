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


public partial class ConsultaVehiculos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Auxiliar.Helper.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Default.aspx");
            }
            Page.Form.DefaultButton = this.btnConsultar.ID;
            Page.Form.DefaultFocus = this.txtConsPlaca.ID;
            if (Request.QueryString.Count > 0)
            {
                this.txtConsPlaca.Text = Request.QueryString[0];
                Consultar();
            }
        }
    }

    public void Consultar()
    {
        changeTab(0);
        MatriculacionVehicular oMatric = new MatriculacionVehicular(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        string[] datosVehiculo = oMatric.DatosAvanzadosVehiculo(this.txtConsPlaca.Text.ToUpper());
        if (datosVehiculo[0] != "error")
        {
            this.lblError.Text = "";
            this.divMenu.Visible = true;
            this.btnVerPropietario.Visible = true;
            this.btnImgPhoto.Visible = true;
            #region "Asignar datos"
            this.txtPlaca.Text = txtConsPlaca.Text.ToUpper();
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
        else
        {
            this.divMenu.Visible = false;
            this.btnVerPropietario.Visible = false;
            this.btnImgPhoto.Visible = false;
            this.lblError.Text = "No existe el veh�culo con placa " + this.txtConsPlaca.Text;
        }
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Consultar();
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
                Menu1.Items[i].ImageUrl = "./img/tab" + i.ToString() + "_on.gif";
            }
            else
            {
                Menu1.Items[i].ImageUrl = "./img/tab" + i.ToString() + "_off.gif";
            }
        }

        MatriculacionVehicular oMatric;

        switch (nTab)
        {
            case 2:
                oMatric = new MatriculacionVehicular(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
                DataTable dtHistMatric = oMatric.HistorialMatriculacion(this.txtConsPlaca.Text.ToUpper());
                this.gvHistMatricula.DataSource = dtHistMatric;
                this.gvHistMatricula.DataBind();
                if (dtHistMatric.Rows.Count == 0)
                    this.lblMensajeMatriculas.Text = "Su veh�culo con placa " + this.txtPlaca.Text.ToString() + " no registra historial de matriculaci�n";
                else
                    this.lblMensajeMatriculas.Text = "";
                break;
            case 3:
                oMatric = new MatriculacionVehicular(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
                DataTable dtBloqueos = oMatric.BloqueosPorVehiculo(this.txtConsPlaca.Text.ToUpper());
                this.gvBloqueos.DataSource = dtBloqueos;
                this.gvBloqueos.DataBind();
                if (dtBloqueos.Rows.Count == 0)
                    this.lblMensajeBloqueos.Text = "Su veh�culo con placa " + this.txtPlaca.Text.ToString() + " no registra bloqueos";
                else
                    this.lblMensajeBloqueos.Text = "";
                break;
        }
    }
    protected void btnVerPropietario_Click(object sender, EventArgs e)
    {
        
    }
    protected void btnVerPropietario_Click1(object sender, EventArgs e)
    {
        MatriculacionVehicular oMatric = new MatriculacionVehicular(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        Response.Redirect("ConsultaLicencias.aspx?Lic=" + oMatric.GetCedulaPropietario(this.txtPlaca.Text));
    }

    protected void btnImgPhoto_Click(object sender, EventArgs e)
    {
        try
        {
            this.imgFotoVehiculo.ImageUrl = "fotoVehiculo.aspx?p=" + txtPlaca.Text;
            this.MPE.X = 10;
            this.MPE.Y = 60;
            this.MPE.Show();
        }
        catch (Exception ex)
        {
        }
    }
}
