using System;
using System.Configuration;
using CifradoCs;

public partial class Consultas_Vehiculos_VehiculoDetInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                    objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                    objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                    string placa = objCrypto.DescifrarCadena(Utilities.Utils.DecodeFrom64(Request.QueryString[0]));
                    hdnPlaca.Value = placa;
                    LoadData();
                }
                catch
                {
                    HtmlWriter.Messages.ShowMainContentError(Master, divMain, "El formato del parámetro de placa es incorrecto");
                }
            }
        }
    }


    protected void LoadData()
    {
        try
        {
            Matriculacion.MatriculacionVehicular oMatric = new Matriculacion.MatriculacionVehicular(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
            string[] datosVehiculo = oMatric.DatosAvanzadosVehiculo(hdnPlaca.Value);
            #region "Asignar datos textboxes"
            if (datosVehiculo.Length > 2)
            {
                txtPlaca.Text = hdnPlaca.Value;
                txtChasis.Text = datosVehiculo[0];
                txtAno.Text = datosVehiculo[1];
                txtTonelaje.Text = datosVehiculo[2];
                txtCAVMCPN.Text = datosVehiculo[3];
                txtMotor.Text = datosVehiculo[4];
                txtPaisOrigen.Text = datosVehiculo[5];
                txtTipo.Text = datosVehiculo[6];
                txtCilindraje.Text = datosVehiculo[7];
                txtServicio.Text = datosVehiculo[8];
                txtClase.Text = datosVehiculo[13];
                txtMarca.Text = datosVehiculo[14];
                txtModelo.Text = datosVehiculo[15];
                txtColor.Text = datosVehiculo[16];
                if (datosVehiculo[8] == "PARTICULAR")
                    TabTransPub.Visible = false;
                else
                {
                    txtNoPasajeros.Text = datosVehiculo[9];
                    txtModalidad.Text = datosVehiculo[10];
                    txtCooperativa.Text = datosVehiculo[11];
                    txtClaseServicio.Text = datosVehiculo[12];
                    TabTransPub.Visible = true;
                }

                txtNumSOAT.Text = datosVehiculo[17];
                txtEmpSOAT.Text = datosVehiculo[18];
                txtFechaIniSOAT.Text = datosVehiculo[19];
                txtFechaFinSOAT.Text = datosVehiculo[20];
            }
            else
                TabCaracteristicas.Controls.Add(HtmlWriter.Messages.PrintErrorMessage(datosVehiculo[1]));
            #endregion
            #region "Matriculación"
            gvHistMatriculac.DataSource = oMatric.HistorialMatriculacion(hdnPlaca.Value);
            gvHistMatriculac.DataBind();
            if (gvHistMatriculac.Rows.Count == 0)
            {
                TabMatriculacion.Controls.Add(string.IsNullOrWhiteSpace(oMatric.Error)
                                                  ? HtmlWriter.Messages.PrintInfoMessage(
                                                      "Este vehículo no registra ninguna revisión/matriculación")
                                                  : HtmlWriter.Messages.PrintErrorMessage(
                                                      "Ha ocurrido un error al cunsultar la información"));
            }
            #endregion
            #region "Bloqueos"
            gvBloqueos.DataSource = oMatric.BloqueosPorVehiculo(hdnPlaca.Value);
            gvBloqueos.DataBind();
            if (gvBloqueos.Rows.Count == 0)
            {
                TabBloqueos.Controls.Add(string.IsNullOrWhiteSpace(oMatric.Error)
                                                  ? HtmlWriter.Messages.PrintInfoMessage(
                                                      "Este vehículo no registra ningún bloqueo")
                                                  : HtmlWriter.Messages.PrintErrorMessage(
                                                      "Ha ocurrido un error al cunsultar la información"));
            }
            #endregion
        }
        catch
        {
        }
    }
}