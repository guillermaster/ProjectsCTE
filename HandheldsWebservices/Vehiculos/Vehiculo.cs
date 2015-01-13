using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.DataAccess.Types;

namespace Vehiculos
{
    public class Vehiculo
    {
        private string dbUser;
        private string dbPassword;
        private string dbServer;
        private string error;

        public Vehiculo(string sUsuario, string sClave, string sBaseDatos)
        {
            this.dbUser = sUsuario;
            this.dbPassword = sClave;
            this.dbServer = sBaseDatos;
        }

        private DataTable InitDatosAvVehicDataTable()
        {
            DataTable dtVehiculos = new DataTable("Vehiculo");
            dtVehiculos.Columns.Add("Placa");
            dtVehiculos.Columns.Add("RAMV");
            dtVehiculos.Columns.Add("Marca");
            dtVehiculos.Columns.Add("Modelo");
            dtVehiculos.Columns.Add("Chasis");
            dtVehiculos.Columns.Add("Motor");
            dtVehiculos.Columns.Add("Servicio");
            dtVehiculos.Columns.Add("Clase");
            dtVehiculos.Columns.Add("Tipo");
            dtVehiculos.Columns.Add("Color");
            dtVehiculos.Columns.Add("Combustible");
            dtVehiculos.Columns.Add("Carroceria");
            dtVehiculos.Columns.Add("Tonelaje");
            dtVehiculos.Columns.Add("Cilindraje");
            dtVehiculos.Columns.Add("Ejes");
            dtVehiculos.Columns.Add("Anio");
            dtVehiculos.Columns.Add("Pais");
            dtVehiculos.Columns.Add("Mensaje");
            return dtVehiculos;
        }

        public DataSet Test()
        {
            DataSet dsTest = new DataSet();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer);
            oDatos.EjecutarSPfillDataSet("{call STK_TRX_REVISION_HAND_HELD.STP_VERIFICACION_PRINCIPAL('?','?','?',{resultset 4, PT_OPCIONES_USUARIO, PT_MENSAJES, PN_ERROR, PV_MENSAJE})}", dsTest);
            return dsTest;
            //AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer);
            //oDatos.Paquete("STK_TRX_REVISION_HAND_HELD.STP_VERIFICACION_PRINCIPAL");
            //oDatos.Parametro("PV_USUARIO", "usr");
            //oDatos.Parametro("PV_PASSWORD", "pwd");
            //oDatos.Parametro("PV_IMEI", "emei");
            //oDatos.Parametro("PT_OPCIONES_USUARIO", "R", 0, "O");
            //oDatos.Parametro("PT_MENSAJES", "R", 0, "O");
            //oDatos.Parametro("PN_ERROR", "N", 10, "O");
            //oDatos.Parametro("PV_MENSAJE", "V", 220, "O");
            //if (oDatos.Ejecutar("R"))
            //{
            //    string error = oDatos.RetornarParametro("PV_MENSAJE").ToString();
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}

        }
        
        public DataTable DatosAvanzadosVehiculo(string placa)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer);
            DataTable dtDatAvVehiculo = InitDatosAvVehicDataTable();

            oDatos.Paquete("WEB_INTER_INFRACCIONES_1.retorna_mas_datos_veh");
            oDatos.Parametro("pv_placa", placa);
            oDatos.Parametro("pv_chasis", "V", 120, "O");
            oDatos.Parametro("pv_anio_prod", "V", 120, "O");
            oDatos.Parametro("pv_tonelaje", "V", 120, "O");
            oDatos.Parametro("pv_camv", "V", 120, "O");//null
            oDatos.Parametro("pv_motor", "V", 120, "O");
            oDatos.Parametro("pv_pais_origen", "V", 120, "O");//null
            oDatos.Parametro("pv_tipo", "V", 120, "O");
            oDatos.Parametro("pv_cilindraje", "V", 120, "O");//null
            oDatos.Parametro("pv_servicio", "V", 120, "O");
            oDatos.Parametro("pv_pasajeros", "V", 120, "O");
            oDatos.Parametro("pv_modalidad", "V", 120, "O");
            oDatos.Parametro("pv_cooperativa", "V", 120, "O");
            oDatos.Parametro("pv_clase_serv", "V", 120, "O");
            oDatos.Parametro("pv_clase", "V", 120, "O");
            oDatos.Parametro("pv_marca", "V", 120, "O");
            oDatos.Parametro("pv_modelo", "V", 120, "O");
            oDatos.Parametro("pv_color", "V", 120, "O");
            //oDatos.Parametro("pv_num_soat", "V", 120, "O");
            //oDatos.Parametro("pv_emp_soat", "V", 120, "O");
            //oDatos.Parametro("pv_fecha_ini_soat", "V", 120, "O");
            //oDatos.Parametro("pv_fecha_fin_soat", "V", 120, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (error == "")
                    {
                        DataRow dtRow = dtDatAvVehiculo.NewRow();
                        dtRow["Placa"] = placa;
                        dtRow["RAMV"] = oDatos.RetornarParametro("pv_camv").ToString();
                        dtRow["Chasis"] = oDatos.RetornarParametro("pv_chasis").ToString();
                        dtRow["Anio"] = oDatos.RetornarParametro("pv_anio_prod").ToString();
                        dtRow["Tonelaje"] = oDatos.RetornarParametro("pv_tonelaje").ToString();
                        dtRow["Motor"] = oDatos.RetornarParametro("pv_motor").ToString();
                        dtRow["Pais"] = oDatos.RetornarParametro("pv_pais_origen").ToString();
                        dtRow["Tipo"] = oDatos.RetornarParametro("pv_tipo").ToString();
                        dtRow["Cilindraje"] = oDatos.RetornarParametro("pv_cilindraje").ToString();
                        dtRow["Servicio"] = oDatos.RetornarParametro("pv_servicio").ToString();
                        dtRow["Clase"] = oDatos.RetornarParametro("pv_clase").ToString();
                        dtRow["Marca"] = oDatos.RetornarParametro("pv_marca").ToString();
                        dtRow["Modelo"] = oDatos.RetornarParametro("pv_modelo").ToString();
                        dtRow["Color"] = oDatos.RetornarParametro("pv_color").ToString();
                        dtDatAvVehiculo.Rows.Add(dtRow);
                    }
                    else
                    {
                        this.error = error;
                    }
                }
                else
                {
                    this.error = oDatos.Mensaje;
                }
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtDatAvVehiculo;
        }


        public string DatosVehiculo(string revision)
        {
            string xml = null;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer);
            oDatos.Paquete("stk_trx_revision_hh_axis.STP_CONSULTA_DATOS_VEHICULO");
            oDatos.Parametro("PV_REVISION", revision);
            oDatos.Parametro("PC_XML", "CLOB", "O");
            oDatos.Parametro("PN_ERROR", "N", 10, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 2000, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("PN_ERROR").ToString();
                    if (error == "1")
                    {
                        xml = ((OracleClob)oDatos.RetornarParametro("PC_XML")).Value;
                    }
                    else
                    {
                        this.error = oDatos.RetornarParametro("PV_MENSAJE").ToString();
                    }
                }
                else
                {
                    this.error = oDatos.Mensaje;
                }
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return xml;
        }


        public string ResultadoRevision(string revision)
        {
            string xml = null;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer);
            oDatos.Paquete("stk_trx_revision_hh_axis.stp_resultado_revision");
            oDatos.Parametro("PC_RESULTADO", "CLOB", "I", revision);
            oDatos.Parametro("PC_XML", "CLOB", "O");
            oDatos.Parametro("PN_ERROR", "N", 10, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 2000, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("PN_ERROR").ToString();
                    if (error == "1")
                    {
                        xml = ((OracleClob)oDatos.RetornarParametro("PC_XML")).Value;
                    }
                    else
                    {
                        this.error = oDatos.RetornarParametro("PV_MENSAJE").ToString();
                    }
                }
                else
                {
                    this.error = oDatos.Mensaje;
                }
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return xml;
        }


        public string Error
        {
            get
            {
                return this.error;
            }
        }

    }
}
