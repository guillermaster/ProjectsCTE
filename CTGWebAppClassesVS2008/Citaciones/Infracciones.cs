using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using AccesoDatos;
using System.Data;

namespace Citaciones
{
    public class Infracciones
    {
        private string user;
        private string password;
        private string database;
        private string error;

        public Infracciones(string sUsuario, string sClave, string sBaseDatos)
        {
            this.user = sUsuario;
            this.password = sClave;
            this.database = sBaseDatos;
        }

        public int PuntosInicioLicencia()
        {
            int puntos = -1;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Citaciones.Infracciones.cs PuntosIniciosLicencia()");
            try
            {
                oDatos.EjecutarQuery("select web_inter_infracciones_1.retorna_puntos_lic() puntos from dual");
                if (oDatos.oDataReader.Read())
                {
                    puntos = int.Parse(oDatos.oDataReader["puntos"].ToString());
                }
                oDatos.Dispose();
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
                oDatos.Dispose();
            }
            return puntos;
        }

        //public string PuntosPerdidosPorLicencia(string id)
        //{
        //    string puntos = "";
        //    AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Citaciones.Infracciones.cs ConsultarLicencia()");
        //    try
        //    {
        //        oDatos.EjecutarQuery("select web_inter_infracciones_1.retorna_puntos('" + id + "') puntos from dual");
        //        if (oDatos.oDataReader.Read())
        //        {
        //            puntos = oDatos.oDataReader["puntos"].ToString();
        //        }
        //        oDatos.Dispose();
        //    }
        //    catch (Exception ex)
        //    {
        //        this.error = ex.Message;
        //        oDatos.Dispose();
        //    }
        //    return puntos;
        //}

        public string PuntosPorLicencia(string id)
        {
            string puntos = string.Empty;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Citaciones.Infracciones.cs PuntosPorLicencia()");

            oDatos.Paquete("consulta_puntos_licencia");
            oDatos.Parametro("pv_identificacion", id);
            oDatos.Parametro("pn_puntos", "V", 3, "O");
            oDatos.Parametro("pv_mensaje", "V", 320, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_mensaje").ToString() == "")
                        puntos = oDatos.RetornarParametro("pn_puntos").ToString();
                    else
                        this.error = oDatos.RetornarParametro("pv_mensaje").ToString();
                }
                else
                    this.error = oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return puntos;
        }

        public object[] InfraccionPendiente(string cod_citacion)
        {
            ArrayList oInfracciones = new ArrayList();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Citaciones.Infracciones.cs InfraccionPendiente()");

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaCitacionPendiente);
            oDatos.Parametro("pv_citacion", cod_citacion);
            oDatos.Parametro("C_INFRAC", "R", 0, "O");

            if (oDatos.Ejecutar("R"))
            {
                ArrayList oLista = new ArrayList();
                while (oDatos.oDataReader.Read())
                {
                    try { oLista.Add(oDatos.oDataReader.GetString(0)); }
                    catch (Exception ex) { oLista.Add(""); }
                    try { oLista.Add(oDatos.oDataReader.GetString(1)); }
                    catch (Exception ex) { oLista.Add(""); }
                    try { oLista.Add(oDatos.oDataReader.GetString(2)); }
                    catch (Exception ex) { oLista.Add(""); }
                    try { oLista.Add(oDatos.oDataReader.GetString(3)); }
                    catch (Exception ex) { oLista.Add(""); }
                    try { oLista.Add(oDatos.oDataReader.GetString(4)); }
                    catch (Exception ex) { oLista.Add(""); }
                    try { oLista.Add(oDatos.oDataReader.GetString(5)); }// articulo literal 
                    catch (Exception ex) { oLista.Add(""); }
                    try { oLista.Add(oDatos.oDataReader.GetString(6)); }//descripcion contravencion
                    catch (Exception ex) { oLista.Add(""); }
                    try { oLista.Add(oDatos.oDataReader.GetDecimal(7)); }
                    catch (Exception ex) { oLista.Add(""); }
                    try { oLista.Add(oDatos.oDataReader.GetDecimal(8)); }
                    catch (Exception ex) { oLista.Add(""); }
                    try { oLista.Add(oDatos.oDataReader.GetDecimal(9)); }
                    catch (Exception ex) { oLista.Add(""); }
                    oInfracciones.Add(oLista.ToArray());
                    oLista.Clear();
                }
            }
            else
            {
                ArrayList oLista = new ArrayList();
                oLista.Add("Error");
                oLista.Add(oDatos.Mensaje);
                oInfracciones.Add(oLista.ToArray());
            }

            oDatos.Dispose();

            return oInfracciones.ToArray();
        }


        public object[] InfraccionesPendientes(string id, string tipoId)
        {
            ArrayList oInfracciones = new ArrayList();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Citaciones.Infracciones.cs InfraccionesPendientes()");

            switch (tipoId)
            {
                case "P":
                    oDatos.Paquete(Constantes.StoredProcedures.ConsultaCitacionesPendPorPlaca);
                    oDatos.Parametro("pv_placa", id.ToUpper());
                    break;
                case "I":
                    oDatos.Paquete(Constantes.StoredProcedures.ConsultaCitacionesPendPorIdent);
                    oDatos.Parametro("pv_identificacion", id);
                    break;
            }

            oDatos.Parametro("C_INFRAC", "R", 0, "O");

            if (oDatos.Ejecutar("R"))
            {
                ArrayList oLista = new ArrayList();
                //bool tieneCitacionesPendientes = false;
                while (oDatos.oDataReader.Read())
                {
                    //tieneCitacionesPendientes = true;
                    #region NUMERO DE CITACION
                    try { oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("NUM_INFRACCION"))); }
                    catch (Exception ex) { oLista.Add(""); }
                    #endregion
                    #region TIPO de contravención
                    try { oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("TIPO_INFRACCION"))); }
                    catch (Exception ex) { oLista.Add(""); }
                    #endregion
                    #region IDENTIFICACION INFRACTOR
                    try { oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("IDENTIFICACION"))); }
                    catch (Exception ex) { oLista.Add(""); }
                    #endregion
                    #region PLACA VEHICULO CONTRAVENTOR
                    try { oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("PLACA"))); }
                    catch (Exception ex) { oLista.Add(""); }
                    #endregion
                    #region FECHA de contravención
                    try { oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("FEC_INFRACCION"))); }
                    catch (Exception ex) { oLista.Add(""); }
                    #endregion
                    #region ARTÍCULO de contravención
                    //try { oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("WEB_INTER_INFRACCIONES_1.MAPEA"))); }
                    //catch (Exception ex) { oLista.Add(""); }
                    if (tipoId == "I")
                    {
                        try { oLista.Add(oDatos.oDataReader.GetString(5)); }// articulo literal 
                        catch (Exception ex) { oLista.Add(""); }
                    }
                    else
                        oLista.Add("");
                    #endregion
                    
                    #region puntos
                    try { oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("PUNTOS"))); }
                    catch (Exception ex) { oLista.Add("--"); }
                    #endregion
                    #region descripción de contravención
                    try { oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("CONTRAVENCION"))); }
                    catch (Exception ex) { oLista.Add(""); }
                    #endregion
                    #region valor de contravención
                    try { oLista.Add(oDatos.oDataReader.GetDecimal(oDatos.oDataReader.GetOrdinal("VAL_CONTRAV"))); }
                    catch (Exception ex) { oLista.Add(""); }
                    #endregion
                    #region valor de MULTA
                    try { oLista.Add(oDatos.oDataReader.GetDecimal(oDatos.oDataReader.GetOrdinal("MUL_CONTRAV"))); }
                    catch (Exception ex) { oLista.Add(""); }
                    #endregion
                    #region valor TOTAL
                    try { oLista.Add(oDatos.oDataReader.GetDecimal(oDatos.oDataReader.GetOrdinal("TOTAL"))); }
                    catch (Exception ex) { oLista.Add(""); }
                    #endregion
                    oInfracciones.Add(oLista.ToArray());
                    oLista.Clear();
                }
                //if (!tieneCitacionesPendiente)
                //{

                //}
            }
            else
            {
                ArrayList oLista = new ArrayList();
                oLista.Add("Error");
                oLista.Add(oDatos.Mensaje);
                oInfracciones.Add(oLista.ToArray());
            }

            oDatos.Dispose();

            return oInfracciones.ToArray();
        }

        public string[] ObtenerUniformadoSancionador(string numCitacion)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Citaciones.Infracciones.cs ObtenerUniformadoSancionador()");
            string[] uniformado = new string[2];

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaUniformadoSancCitacion);
            oDatos.Parametro("pv_sec_libretin", numCitacion);
            oDatos.Parametro("pn_persona_uni", "N", 120, "O");
            oDatos.Parametro("pv_dec_persona_uni", "V", 120, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");

            if (oDatos.Ejecutar("R"))
            {
                string error = oDatos.RetornarParametro("pv_error").ToString();
                //ERROR 1 ->  El usuario no puede ser nulo
                //ERROR 2 -> El usuario no existe o no está registrado
                //ERROR 3 -> Debe modificar su clave, es la primera vez que entra al sistema
                //ERROR 4 -> el password no puede ser nulo
                //ERROR S -> NO HAY ERROR
                if (error == "")
                {
                    uniformado[0] = oDatos.RetornarParametro("pv_dec_persona_uni").ToString();
                    uniformado[1] = oDatos.RetornarParametro("pn_persona_uni").ToString();
                }
                else
                {
                    uniformado[0] = "error";
                    uniformado[1] = error;
                }
            }
            else
            {
                uniformado[0] = "error";
                uniformado[1] = "Error: No se pudo conectar a la Base de Datos";
            }

            oDatos.Dispose();

            return uniformado;
        }

        public string[] ObtenerDatosBasicosVehiculo(string placa)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Citaciones.Infracciones.cs ObtenerDatosBasicosVehiculo()");
            string[] datos_vehiculo = new string[5];

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaDatosBasicosVehiculo);
            oDatos.Parametro("pv_placa", placa);
            oDatos.Parametro("pv_marca", "V", 120, "O");
            oDatos.Parametro("pv_modelo", "V", 120, "O");
            oDatos.Parametro("pv_clase", "V", 120, "O");
            oDatos.Parametro("pv_color", "V", 120, "O");
            oDatos.Parametro("pv_propietario", "V", 120, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");

            if (oDatos.Ejecutar("R"))
            {
                string error = oDatos.RetornarParametro("pv_error").ToString();
                //ERROR 1 ->  El usuario no puede ser nulo
                //ERROR 2 -> El usuario no existe o no está registrado
                //ERROR 3 -> Debe modificar su clave, es la primera vez que entra al sistema
                //ERROR 4 -> el password no puede ser nulo
                //ERROR S -> NO HAY ERROR
                if (error == "")
                {
                    datos_vehiculo[0] = oDatos.RetornarParametro("pv_propietario").ToString();
                    datos_vehiculo[1] = oDatos.RetornarParametro("pv_clase").ToString();
                    datos_vehiculo[2] = oDatos.RetornarParametro("pv_marca").ToString();
                    datos_vehiculo[3] = oDatos.RetornarParametro("pv_modelo").ToString();
                    datos_vehiculo[4] = oDatos.RetornarParametro("pv_color").ToString();
                }
                else
                {
                    datos_vehiculo[0] = "error";
                    datos_vehiculo[1] = error;
                }
            }
            else
            {
                datos_vehiculo[0] = "error";
                datos_vehiculo[1] = "Error: No se pudo conectar a la Base de Datos";
            }

            oDatos.Dispose();

            return datos_vehiculo;
        }

        public DataTable CitacionesPorCodigoCitac(string codCitac)
        {
            string[] dtColumnsValues = { "fec_infraccion", "contravencion", "articulo", "nombres", "identificacion", "placa", "uniformado", "id_persona_vig", "val_contrav", "pagada", "puntos", "reincidencias" };
            string[] dtColumnsHeader = { "Fecha de Infracción", "Contravención", "Artículo", "Nombre del Infractor", "Identificación del Infractor", "Placa del vehículo infractor", "Uniformado", "Código del Uniformado", "Valor a Pagar", "Pagada (S/N)", "Puntos a Reducir", "Cantidad de Reincidencias" };
            DataTable dtResCitaciones = new DataTable("dtCitaciones");
            for (int i = 0; i < dtColumnsHeader.Length; i++)
            {
                if (i == 0)
                    dtResCitaciones.Columns.Add(dtColumnsHeader[0], typeof(DateTime));
                else
                    dtResCitaciones.Columns.Add(dtColumnsHeader[i]);
            }
            this.error = "";
            int n = 0;

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Citaciones.Infracciones.cs CitacionesPorCodigoCitac");
            try
            {
                oDatos.Paquete("web_inter_infracciones_1.Infracciones_x_cit_jueces");
                oDatos.Parametro("pv_citac", codCitac);
                oDatos.Parametro("pv_pagada", "");
                oDatos.Parametro("C_INFRAC", "R", 0, "O");

                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtResCitaciones.NewRow();
                        for (int i = 0; i < dtColumnsHeader.Length; i++)
                        {
                            if(i==0)
                                dr[dtColumnsHeader[0]] = DateTime.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtColumnsValues[0])).ToString(), new System.Globalization.CultureInfo("es-ES", false));
                            else if(i==(dtColumnsHeader.Length - 1))
                                dr[dtColumnsHeader[i]] = int.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtColumnsValues[i])).ToString()) - 1;
                            else 
                                dr[dtColumnsHeader[i]] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtColumnsValues[i])).ToString();

                            if (i == 8)//valor a pagar
                                dr[dtColumnsHeader[i]] = "$ " + dr[dtColumnsHeader[i]];
                        }
                        dtResCitaciones.Rows.Add(dr);
                        n++;
                    }
                    if (n == 0)
                    {
                        this.error = "No existe citación con código " + codCitac;
                    }
                }
                else
                {
                    this.error = "Error al consultar citacion, intente nuevamente por favor ";// + oDatos.Mensaje;
                }
            }
            catch (Exception ex)
            {
                this.error = "Error al consultar citacion, intente nuevamente por favor ";
            }
            finally
            {
                oDatos.Dispose();
            }           

            return dtResCitaciones;
        }


        public double TotalPagarCitacionesPendientes(string numLicencia)
        {
            double ValTotCitac = 0;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Citaciones.Infracciones.cs TotalPagarCitacionesPendientes()");

            oDatos.Paquete("web_trx_pago_citaciones.consulta_valor_deudas_ctg");
            oDatos.Parametro("pv_identificacion", numLicencia);
            oDatos.Parametro("pn_total", "F", 12, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string strVal = oDatos.RetornarParametro("pn_total").ToString();
                    ValTotCitac = Convert.ToDouble(strVal);
                }
                else
                    this.error = oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return ValTotCitac;
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
