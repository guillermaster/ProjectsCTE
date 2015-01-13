﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Matriculacion;
using Brevetacion;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        ConsultarDatos(txtConsPlaca.Text.ToUpper());
    }

    protected void ConsultarDatos(string placa)
    {
        if (LoadVehicleData(placa))
        {
            pnlWarning.Visible = false;
            MatriculacionVehicular oMatric = new MatriculacionVehicular(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            string noLicencia = oMatric.GetCedulaPropietario(placa);
            ConsultarDatosLicencia(noLicencia);
            ConsultarVehiculosPorPersona(noLicencia);
            TabContainer1.Visible = true;
        }
        else
        {
            TabContainer1.Visible = false;
            pnlWarning.Visible = true;
            lblWarning.Text = "No existe el vehículo con placa " + placa;
        }
    }

    protected bool LoadVehicleData(string placa)
    {
        try
        {
            Matriculacion.MatriculacionVehicular oMatric = new Matriculacion.MatriculacionVehicular(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            string[] datosVehiculo = oMatric.DatosAvanzadosVehiculo(placa);
            #region "Asignar datos textboxes"
            if (datosVehiculo[0] != "error")
            {
                txtPlaca.Text = placa;
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
                #region "Matriculación"
                gvHistMatriculac.DataSource = oMatric.HistorialMatriculacion(placa); ;
                gvHistMatriculac.DataBind();
                if (gvHistMatriculac.Rows.Count == 0)
                {
                    Label lblError = new Label();
                    if (string.IsNullOrEmpty(oMatric.Error))
                        lblError.Text = "Este vehículo no registra ninguna revisión/matriculación";
                    else
                        lblError.Text = "Ha ocurrido un error al cunsultar la información";
                    TabMatriculacion.Controls.Add(lblError);
                }
                #endregion
                #region "Bloqueos"
                gvBloqueos.DataSource = oMatric.BloqueosPorVehiculo(placa);
                gvBloqueos.DataBind();
                if (gvBloqueos.Rows.Count == 0)
                {
                    Label lblError = new Label();
                    if (string.IsNullOrEmpty(oMatric.Error))
                        lblError.Text = "Este vehículo no registra ningún bloqueo";
                    else
                        lblError.Text = "Ha ocurrido un error al cunsultar la información";
                    TabBloqueos.Controls.Add(lblError);
                }
                #endregion
                imgFotoVeh.ImageUrl = "fotoVehiculo.aspx?p=" + placa;
                return true;
            }
            else
            {
                Label lblError = new Label();
                lblError.Text = datosVehiculo[1];
                TabCaracteristicas.Controls.Add(lblError);
                return false;
            }
            #endregion
        }
        catch
        {
            return false;
        }
    }


    protected void ConsultarDatosLicencia(string noLicencia)
    {
        try
        {
            Licencia oLicencia = new Licencia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            object[] oDatos = oLicencia.ConsultarLicencia(noLicencia);

            if (oDatos[0].ToString() == "N")
            {
                Label lblError1 = new Label();
                lblError1.Text = "No existe licencia número " + noLicencia + " o se produjo un error al consultar la información";
                TabPropietario.Controls.Add(lblError1);
            }
            else
            {
                #region Datos Personales
                DataTable oTabDatosPersonales = new DataTable();
                oTabDatosPersonales.Columns.Add("Nombre:");
                oTabDatosPersonales.Columns.Add("Fecha de nacimiento:");
                oTabDatosPersonales.Columns.Add("Lugar de nacimiento:");
                oTabDatosPersonales.Columns.Add("Sexo:");
                oTabDatosPersonales.Columns.Add("Estatura:");
                oTabDatosPersonales.Columns.Add("Estado civil:");
                oTabDatosPersonales.Columns.Add("Profesión:");
                oTabDatosPersonales.Columns.Add("Lugar de residencia:");
                oTabDatosPersonales.Columns.Add("Dirección:");
                oTabDatosPersonales.Columns.Add("Teléfono:");

                DataRow rowDatosPersonales = oTabDatosPersonales.NewRow();
                rowDatosPersonales[0] = oDatos[1].ToString();
                rowDatosPersonales[1] = oDatos[3].ToString();
                rowDatosPersonales[2] = oDatos[4].ToString() + " - " + oDatos[5].ToString() + " - " + oDatos[6].ToString();
                rowDatosPersonales[3] = oDatos[7].ToString();
                rowDatosPersonales[4] = oDatos[12].ToString();
                rowDatosPersonales[5] = oDatos[13].ToString();
                rowDatosPersonales[6] = oDatos[14].ToString();
                rowDatosPersonales[7] = oDatos[17].ToString() + " - " + oDatos[18].ToString() + " - " + oDatos[19].ToString();
                rowDatosPersonales[8] = oDatos[15].ToString();
                rowDatosPersonales[9] = oDatos[16].ToString();
                oTabDatosPersonales.Rows.Add(rowDatosPersonales);
                dvDatosPersonales.DataSource = oTabDatosPersonales;
                dvDatosPersonales.DataBind();
                dvDatosPersonales.Visible = true;
                #endregion

                #region Rasgos
                if (oDatos[8].ToString() == "null")
                {
                    //this.txtTipoSangre.Text = "";
                }
                #endregion

                imgFoto.ImageUrl = "fotoLicencia.aspx?id=" + oDatos[20].ToString();
                imgFoto.DataBind();

                #region Llenar Tablas
                DataRow oRegistros;
                DataTable _oTabLic = new DataTable();
                _oTabLic.Columns.Add("Categoría");
                _oTabLic.Columns.Add("Fecha/Emisión");
                _oTabLic.Columns.Add("Fecha/Caducidad");
                _oTabLic.Columns.Add("Tipo de Sangre");

                DataTable _oTabBloq = new DataTable();
                _oTabBloq.Columns.Add("Fecha");
                _oTabBloq.Columns.Add("Descripción");


                DataTable _oTabRes = new DataTable();
                _oTabRes.Columns.Add("Fecha");
                _oTabRes.Columns.Add("Descripción");

                DataTable _oTabInfr = new DataTable();
                _oTabInfr.Columns.Add("Fecha/Infracción");
                _oTabInfr.Columns.Add("Contravención");

                //////////////////////////////////////////////////////////
                #region Llenar tabla categoria de licencias
                /*DataTable oTabCatLicencias = new DataTable();
                oTabCatLicencias.Columns.Add("Cat.");
                oTabCatLicencias.Columns.Add("Autorización");
                DataRow oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "A";
                oRegCatLicencias[1] = "Para ciclomotores, motocicletas y triciclos motorizados";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "B";
                oRegCatLicencias[1] = "Para automóviles y camionetas con acoplados de hasta 1.75 KG de carga o casas rodantes";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "C";
                oRegCatLicencias[1] = "Para camiones sin acoplados y los comprendidos en la clase B";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "D";
                oRegCatLicencias[1] = "Para los destinados al servicio de transporte de pasajeros y los de la clase B o C según el caso";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "E";
                oRegCatLicencias[1] = "Para camiones articulados o con acoplados, maquinaria especial no agrícola y los de la clase C y D";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "F";
                oRegCatLicencias[1] = "Para automotores especiales adaptados para discapacitados";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "G";
                oRegCatLicencias[1] = "Para tractores y maquinaria especial agrícola";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                grdCatLicencias.DataSource = oTabCatLicencias;
                grdCatLicencias.DataBind();*/
                #endregion
                /////////////////////////////////////////////////////////


                Int64 numElem = oDatos.GetLength(0);


                for (Int64 iIndice = 21; iIndice < numElem; iIndice++)
                {
                    switch (((object[])oDatos[iIndice])[0].ToString())
                    {
                        case "LIC":
                            oRegistros = _oTabLic.NewRow();
                            oRegistros[0] = (((object[])oDatos[iIndice])[1].ToString());
                            oRegistros[1] = Convert.ToDateTime((((object[])oDatos[iIndice])[2].ToString()));
                            oRegistros[2] = Convert.ToDateTime((((object[])oDatos[iIndice])[3].ToString()));
                            oRegistros[3] = oDatos[8];
                            _oTabLic.Rows.Add(oRegistros);
                            break;
                        case "BLO":
                            oRegistros = _oTabBloq.NewRow();
                            oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                            oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                            _oTabBloq.Rows.Add(oRegistros);
                            break;
                        case "RES":
                            oRegistros = _oTabRes.NewRow();
                            oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                            oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                            _oTabRes.Rows.Add(oRegistros);
                            break;
                        case "INF":
                            oRegistros = _oTabInfr.NewRow();
                            oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                            oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                            _oTabInfr.Rows.Add(oRegistros);
                            break;
                    }
                }
                grdLicencias.DataSource = _oTabLic;
                grdLicencias.DataBind();
                if (_oTabLic.Rows.Count == 0)
                    lblMensajeLic.Text = "No se le ha emitido ninguna licencia";

                grdBloqueos.DataSource = _oTabBloq;
                grdBloqueos.DataBind();
                if (_oTabBloq.Rows.Count == 0)
                    lblMensajeBloq.Text = "No registra ningún bloqueo";

                grdRestricciones.DataSource = _oTabRes;
                grdRestricciones.DataBind();
                if (_oTabRes.Rows.Count == 0)
                    lblMensajeRest.Text = "No tiene ninguna restricción";

                grdInfracciones.DataSource = _oTabInfr;
                grdInfracciones.DataBind();
                if (_oTabInfr.Rows.Count == 0)
                    lblMensajeInfracc.Text = "No ha cometido ninguna infracción grave";
                //ShowControls();

                #endregion

            }
        }
        catch
        {
            Label lblError6 = new Label();
            lblError6.Text = "Se produjo un error al consultar la información";
            TabPropietario.Controls.Add(lblError6);
        }


    }


    public void ConsultarVehiculosPorPersona(string identificacion)
    {
        MatriculacionVehicular oMatric = new MatriculacionVehicular(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtVehiculos = new DataTable();

        dtVehiculos = oMatric.VehiculosPorPersonaDatosBloqueos(identificacion);
        /*dtVehiculos.Columns.Add("URL");
        foreach (DataRow dr in dtVehiculos.Rows)
        {
            dr["URL"] = "ConsultaVehiculos.aspx?placa=" + dr["placa"];
        }*/

        grdVehiculos.DataSource = dtVehiculos;

        if (dtVehiculos.Rows.Count == 0)
        {
            if (oMatric.Error == string.Empty)
                this.grdVehiculos.EmptyDataText = "No registra ningún vehículo a su nombre";
            else
                this.grdVehiculos.EmptyDataText = oMatric.Error;
        }

        grdVehiculos.DataBind();
    }


    protected void grdVehiculos_SelectedIndexChanged(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 0;
        string placa = grdVehiculos.SelectedRow.Cells[0].Text;
        txtConsPlaca.Text = placa;
        ConsultarDatos(placa);
    }
}