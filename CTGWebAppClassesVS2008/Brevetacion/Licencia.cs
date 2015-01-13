//using Seguridad;
//using General;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;
using AccesoDatos;
//using CifradoCs;


namespace Brevetacion
{
    public class Licencia
    {
        private string user;
        private string password;
        private string database;
        private string error;


        public Licencia(string sUsuario, string sClave, string sBaseDatos)
        {
            this.user = sUsuario;
            this.password = sClave;
            this.database = sBaseDatos;
        }

        public object[] ConsultarLicencia(string sLicencia)
        {
            ArrayList oLicencias = new ArrayList();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Brevetacion.Licencia.cs ConsultarLicencia()");
            oDatos.Paquete(Constantes.StoredProcedures.ConsultaDatosLicencia);
            oDatos.Parametro("pv_identificacion", sLicencia);
            oDatos.Parametro("pv_nombres", "V", 60, "O");
            oDatos.Parametro("pv_Tipo_Identificacion", "V", 3, "O");
            oDatos.Parametro("pv_Fecha_Nacimiento", "D", 20, "O");
            oDatos.Parametro("pv_Localidad", "V", 60, "O");
            oDatos.Parametro("pv_Provincia", "V", 60, "O");
            oDatos.Parametro("pv_Pais", "V", 60, "O");
            oDatos.Parametro("pv_Sexo", "V", 60, "O");
            oDatos.Parametro("pv_Sangre", "V", 60, "O");
            oDatos.Parametro("pv_Cabello", "V", 60, "O");
            oDatos.Parametro("pv_Ojo", "V", 60, "O");
            oDatos.Parametro("pv_Tez", "V", 60, "O");
            oDatos.Parametro("pv_Estatura", "V", 60, "O");
            oDatos.Parametro("pv_Estado_Civil", "V", 60, "O");
            oDatos.Parametro("pv_Profesion", "V", 60, "O");
            oDatos.Parametro("pv_Direccion", "V", 60, "O");
            oDatos.Parametro("pv_Telefono", "V", 60, "O");
            oDatos.Parametro("pv_Canton", "V", 60, "O");
            oDatos.Parametro("pv_prov_ubicacion", "V", 60, "O");
            oDatos.Parametro("pv_pais_ubicacion", "V", 60, "O");
            oDatos.Parametro("pn_persona", "N", 30, "O");
            oDatos.Parametro("c_datos_licencias", "R", 0, "O");
            oDatos.Parametro("c_datos_bloqueo", "R", 0, "O");
            oDatos.Parametro("c_datos_restriccion", "R", 0, "O");
            oDatos.Parametro("c_datos_infracciones", "R", 0, "O");
            oDatos.Parametro("pv_existe", "V", 30, "O");
            oDatos.Parametro("pv_mensaje", "V", 200, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    oLicencias.Add(oDatos.RetornarParametro("pv_existe").ToString());
                    if (oDatos.RetornarParametro("pv_existe").ToString() == "S")
                    {
                        oLicencias.Add(oDatos.RetornarParametro("pv_nombres").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Tipo_Identificacion").ToString().ToUpper());
                        oLicencias.Add(((DateTime)((OracleDate)oDatos.RetornarParametro("pv_Fecha_Nacimiento"))).ToShortDateString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Localidad").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Provincia").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Pais").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Sexo").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Sangre").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Cabello").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Ojo").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Tez").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Estatura").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Estado_Civil").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Profesion").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Direccion").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Telefono").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Canton").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_prov_ubicacion").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_pais_ubicacion").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pn_persona").ToString());//id persona
                        ArrayList oLista = new ArrayList();
                        while (oDatos.oDataReader.Read())
                        {
                            oLista.Add("LIC");
                            oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("categoria")));
                            oLista.Add(oDatos.oDataReader.GetDateTime(oDatos.oDataReader.GetOrdinal("fecha_emision")));
                            oLista.Add(oDatos.oDataReader.GetDateTime(oDatos.oDataReader.GetOrdinal("fecha_caducidad")));
                            oLicencias.Add(oLista.ToArray());
                            oLista.Clear();
                        }
                        oDatos.oDataReader.NextResult();
                        while (oDatos.oDataReader.Read())
                        {
                            oLista.Add("BLO");
                            oLista.Add(oDatos.oDataReader.GetDateTime(oDatos.oDataReader.GetOrdinal("fecha_bloqueo")));
                            oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("descripcion")));
                            oLicencias.Add(oLista.ToArray());
                            oLista.Clear();
                        }
                        oDatos.oDataReader.NextResult();
                        while (oDatos.oDataReader.Read())
                        {
                            oLista.Add("RES");
                            oLista.Add(oDatos.oDataReader.GetDateTime(oDatos.oDataReader.GetOrdinal("fecha")));
                            oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("descripcion")));
                            oLicencias.Add(oLista.ToArray());
                            oLista.Clear();
                        }
                        oDatos.oDataReader.NextResult();
                        while (oDatos.oDataReader.Read())
                        {
                            oLista.Add("INF");
                            oLista.Add(oDatos.oDataReader.GetDateTime(oDatos.oDataReader.GetOrdinal("fec_infraccion")));
                            oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("contravencion")));
                            oLicencias.Add(oLista.ToArray());
                            oLista.Clear();
                        }
                    }
                    else
                    {
                        oLicencias.Add(oDatos.RetornarParametro("pv_mensaje").ToString());
                    }
                }
                else
                {
                    oLicencias.Add("N");
                    oLicencias.Add(oDatos.Mensaje);
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
            return oLicencias.ToArray();
        }

        /* datos parametrizados */
        #region "Datos Parametrizados"
        public DataTable ObtenerTiposSangre()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Brevetacion.Licencia.cs ObtenerTiposSangre()");

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaTiposSangre);
            oDatos.Parametro("C_TIP_SANG", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 2, "O");

            DataTable dt = new DataTable("tiposdesangre");
            dt.Columns.Add("codigo");
            dt.Columns.Add("tipo_sangre");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                            dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        error = oDatos.RetornarParametro("pv_error").ToString();
                    }
                }
                else
                {
                    error = oDatos.Mensaje;
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
            return dt;
        }

        public DataTable ObtenerRangosEdades()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Brevetacion.Licencia.cs ObtenerRangosEdades()");

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaRangosEdades);
            oDatos.Parametro("C_EDAD_SANG", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 500, "O");

            DataTable dt = new DataTable("rangosedades");
            dt.Columns.Add("codigo");
            dt.Columns.Add("rango_edad");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                            dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                    else
                    {
                        error = oDatos.RetornarParametro("pv_error").ToString();
                    }
                }
                else
                {
                    error = oDatos.Mensaje;
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
            return dt;
        }
        #endregion

        /* estadisticas */
        #region "Estadísticas";
        public DataTable LicenciasPorTipoSangreEdades(string tipo_sangre, string rango_edades)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Brevetacion.Licencia.cs LicenciasPorTipoSangreEdades()");

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaLicenciasPorTipoSangre);
            oDatos.Parametro("pv_tipo", tipo_sangre);
            oDatos.Parametro("pv_edad", rango_edades);
            oDatos.Parametro("C_CONS_TIP_SANG", "R", 0, "O");
            //oDatos.Parametro("pv_error", "V", 2, "O");

            DataTable dt = new DataTable("datoslicencias");
            dt.Columns.Add("apellidos");
            dt.Columns.Add("nombres");
            dt.Columns.Add("telefono");
            dt.Columns.Add("edad");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                        dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                        dr[2] = oDatos.oDataReader.GetValue(2).ToString();
                        dr[3] = oDatos.oDataReader.GetValue(3).ToString();
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    error = oDatos.Mensaje;
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
            return dt;
        }
        #endregion

        #region "Datos Avanzados - Certificados Licencia"


        public object[] ConsultarDatosLicencia(string sLicencia)
        {
            ArrayList oLicencias = new ArrayList();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Brevetacion.Licencia.cs ConsultarDatosLicencia()");
            oDatos.Paquete("GCK_API_CONSULTA_WEB.DATOS_LICENCIA");
            oDatos.Parametro("pv_identificacion", sLicencia);
            oDatos.Parametro("pv_nombres", "V", 60, "O");
            oDatos.Parametro("pv_Tipo_Identificacion", "V", 3, "O");
            oDatos.Parametro("pv_Fecha_Nacimiento", "V", 20, "O");
            oDatos.Parametro("pv_Localidad", "V", 60, "O");
            oDatos.Parametro("pv_Provincia", "V", 60, "O");
            oDatos.Parametro("pv_Pais", "V", 60, "O");
            oDatos.Parametro("pv_Sexo", "V", 60, "O");
            oDatos.Parametro("pv_Sangre", "V", 60, "O");
            oDatos.Parametro("pv_Cabello", "V", 60, "O");
            oDatos.Parametro("pv_Ojo", "V", 60, "O");
            oDatos.Parametro("pv_Tez", "V", 60, "O");
            oDatos.Parametro("pv_Estatura", "V", 60, "O");
            oDatos.Parametro("pv_Estado_Civil", "V", 60, "O");
            oDatos.Parametro("pv_Profesion", "V", 60, "O");
            oDatos.Parametro("pv_Direccion", "V", 60, "O");
            oDatos.Parametro("pv_Telefono", "V", 60, "O");
            oDatos.Parametro("pv_Canton", "V", 60, "O");
            oDatos.Parametro("pv_prov_ubicacion", "V", 60, "O");
            oDatos.Parametro("pv_pais_ubicacion", "V", 60, "O");
            oDatos.Parametro("pn_persona", "N", 30, "O");
            oDatos.Parametro("c_datos_licencias", "R", 0, "O");
            oDatos.Parametro("c_datos_bloqueo", "R", 0, "O");
            oDatos.Parametro("c_datos_restriccion", "R", 0, "O");
            oDatos.Parametro("c_datos_infracciones", "R", 0, "O");
            oDatos.Parametro("pv_existe", "V", 30, "O");
            oDatos.Parametro("pv_mensaje", "V", 200, "O");

            try
            {

                if (oDatos.Ejecutar("R"))
                {
                    oLicencias.Add(oDatos.RetornarParametro("pv_existe").ToString());
                    if (oDatos.RetornarParametro("pv_existe").ToString() == "S")
                    {
                        oLicencias.Add(oDatos.RetornarParametro("pv_nombres").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Tipo_Identificacion").ToString().ToUpper());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Fecha_Nacimiento").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Localidad").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Provincia").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Pais").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Sexo").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Sangre").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Cabello").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Ojo").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Tez").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Estatura").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Estado_Civil").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Profesion").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Direccion").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Telefono").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_Canton").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_prov_ubicacion").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pv_pais_ubicacion").ToString());
                        oLicencias.Add(oDatos.RetornarParametro("pn_persona").ToString());
                        ArrayList oLista = new ArrayList();
                        while (oDatos.oDataReader.Read())
                        {
                            oLista.Add("LIC");
                            oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("categoria")).ToString());
                            oLista.Add(oDatos.oDataReader.GetDateTime(oDatos.oDataReader.GetOrdinal("fecha_emision")).ToString());
                            oLista.Add(oDatos.oDataReader.GetDateTime(oDatos.oDataReader.GetOrdinal("fecha_caducidad")).ToString());
                            oLicencias.Add(oLista.ToArray());
                            oLista.Clear();
                        }
                        oDatos.oDataReader.NextResult();
                        int counter = 0;
                        while (oDatos.oDataReader.Read())
                        {
                            oLista.Add("BLO");
                            oLista.Add(oDatos.oDataReader.GetDateTime(oDatos.oDataReader.GetOrdinal("fecha_bloqueo")).ToString());
                            oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("descripcion")).ToString());
                            oLicencias.Add(oLista.ToArray());
                            oLista.Clear();
                            counter++;
                        }
                        if (counter == 0)//si no existe bloqueo
                        {
                            oLista.Add("BLO");
                            oLista.Add(null);//fecha
                            oLista.Add("A LA FECHA NO REGISTRA BLOQUEOS");//descripción
                            oLicencias.Add(oLista.ToArray());
                            oLista.Clear();
                        }

                        counter = 0;
                        oDatos.oDataReader.NextResult();
                        while (oDatos.oDataReader.Read())
                        {
                            oLista.Add("RES");
                            oLista.Add(oDatos.oDataReader.GetDateTime(oDatos.oDataReader.GetOrdinal("fecha")).ToString());
                            oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("descripcion")).ToString());
                            oLicencias.Add(oLista.ToArray());
                            oLista.Clear();
                            counter++;
                        }
                        if (counter == 0)//si no existen restricciones
                        {
                            oLista.Add("RES");
                            oLista.Add(null);//fecha
                            oLista.Add("A LA FECHA NO REGISTRA RESTRICCIONES");//descripción
                            oLicencias.Add(oLista.ToArray());
                            oLista.Clear();
                        }

                        counter = 0;
                        oDatos.oDataReader.NextResult();
                        while (oDatos.oDataReader.Read())
                        {
                            oLista.Add("INF");
                            oLista.Add(oDatos.oDataReader.GetDateTime(oDatos.oDataReader.GetOrdinal("fec_infraccion")).ToString());
                            oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("contravencion")).ToString());
                            oLicencias.Add(oLista.ToArray());
                            oLista.Clear();
                            counter++;
                        }
                        if (counter == 0)//si no existen infracciones graves
                        {
                            oLista.Add("INF");
                            oLista.Add(null);//fecha
                            oLista.Add("A LA FECHA NO REGISTRA INFRACCIONES");//descripción
                            oLicencias.Add(oLista.ToArray());
                            oLista.Clear();
                        }
                    }
                    else
                    {
                        oLicencias.Add(oDatos.RetornarParametro("pv_mensaje").ToString());
                    }

                    // CONSULTAR DATOS ESPECIFICOS DE LICENCIA

                    //CONSULTAR RENOVACION Y/O CANJE DE CATEGORIA



                }
                else
                {
                    oLicencias.Add(oDatos.Mensaje);
                }
            }
            catch (Exception ex)
            {
                oLicencias.Add(ex.Message);
            }
            finally
            {
                oDatos.Dispose();
            }
            return oLicencias.ToArray();
        }
        
        public object[] ConsultarDatosAdicionalesLicencia(string sLicencia, string sUsuarioWeb)
        {
            ArrayList oLicencias = new ArrayList();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Brevetacion.Licencia.cs ConsultarDatosAdicionalesLicencia()");
            oDatos.Paquete("GCK_API_CONSULTA_WEB.GCP_RETORNA_DATOS_LICENCIA");
            oDatos.Parametro("pv_usuario", sUsuarioWeb);
            oDatos.Parametro("pv_identificacion", sLicencia);
            oDatos.Parametro("pv_fecha_obtencion", "V", 60, "O");
            oDatos.Parametro("pv_prov_obtencion", "V", 60, "O");
            oDatos.Parametro("pv_clase_obtencion", "V", 60, "O");
            oDatos.Parametro("pv_documento", "V", 60, "O");
            oDatos.Parametro("pv_numero", "V", 60, "O");
            oDatos.Parametro("pv_licencia_datos", "V", 60, "O");
            oDatos.Parametro("pv_desc_lic_datos", "V", 60, "O");
            oDatos.Parametro("pv_prov_origen", "V", 60, "O");
            oDatos.Parametro("pv_fecha_origen", "V", 60, "O");
            oDatos.Parametro("pv_fecha_emision", "V", 60, "O");
            oDatos.Parametro("pv_fecha_caducidad", "V", 60, "O");
            oDatos.Parametro("c_renovaciones", "R", 0, "O");
            oDatos.Parametro("c_duplicados", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    oLicencias.Add(oDatos.RetornarParametro("pv_fecha_obtencion").ToString());

                    oLicencias.Add(oDatos.RetornarParametro("pv_prov_obtencion").ToString());
                    oLicencias.Add(oDatos.RetornarParametro("pv_clase_obtencion").ToString().ToUpper());
                    oLicencias.Add(oDatos.RetornarParametro("pv_documento").ToString().ToUpper());
                    oLicencias.Add(oDatos.RetornarParametro("pv_numero").ToString().ToUpper());
                    oLicencias.Add(oDatos.RetornarParametro("pv_licencia_datos").ToString().ToUpper());
                    oLicencias.Add(oDatos.RetornarParametro("pv_desc_lic_datos").ToString().ToUpper());
                    oLicencias.Add(oDatos.RetornarParametro("pv_prov_origen").ToString().ToUpper());
                    try
                    {
                        oLicencias.Add(oDatos.RetornarParametro("pv_fecha_origen").ToString());
                    }
                    catch (Exception e)
                    {
                        oLicencias.Add("---");
                    }
                    try
                    {
                        oLicencias.Add(oDatos.RetornarParametro("pv_fecha_emision").ToString());
                    }
                    catch (Exception e)
                    {
                        oLicencias.Add("---");
                    }
                    try
                    {
                        oLicencias.Add(oDatos.RetornarParametro("pv_fecha_caducidad").ToString());
                    }
                    catch (Exception e)
                    {
                        oLicencias.Add("---");
                    }

                    ArrayList oLista = new ArrayList();
                    int counter = 0;
                    while (oDatos.oDataReader.Read())
                    {
                        oLista.Add("REN");
                        oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("Fecha_Inicio")).ToString());
                        oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("Fecha_Fin")).ToString());
                        oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("Tipo")).ToString());
                        oLicencias.Add(oLista.ToArray());
                        oLista.Clear();
                        counter++;
                    }
                    if (counter == 0)//si no existe renovaciones
                    {
                        oLista.Add("REN");
                        oLista.Add("NO REGISTRA");
                        oLista.Add(null);
                        oLista.Add(null);
                        oLicencias.Add(oLista.ToArray());
                        oLista.Clear();
                    }


                    oDatos.oDataReader.NextResult();
                    counter = 0;
                    while (oDatos.oDataReader.Read())
                    {
                        oLista.Add("DUP");
                        oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("Dup_Fecha_Inicio")).ToString());
                        oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("Dup_Fecha_Fin")).ToString());
                        oLista.Add(oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("Dup_Clase")).ToString());
                        oLicencias.Add(oLista.ToArray());
                        oLista.Clear();
                        counter++;
                    }
                    if (counter == 0)//si no existe duplicados
                    {
                        oLista.Add("DUP");
                        oLista.Add("NO REGISTRA DUPLICADOS");
                        oLista.Add(null);
                        oLista.Add(null);
                        oLicencias.Add(oLista.ToArray());
                        oLista.Clear();
                    }

                }

                else
                {
                    //oLicencias.Add(objCrypto.CifrarCadena(oDatos.RetornarParametro("pv_error").ToString()));
                    oLicencias.Add(oDatos.Mensaje);
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
            return oLicencias.ToArray();
        }


        public DataSet ConsultarDatosAdicionalesLicencia(string sLicencia)
        {
            DataSet dsDatosLicencia = new DataSet("DatosLicencia");
            ArrayList oLicencias = new ArrayList();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Brevetacion.Licencia.cs ConsultarDatosAdicionalesLicencia()");
            oDatos.Paquete("GCK_API_CONSULTA_WEB.GCP_RETORNA_DATOS_LICENCIA");
            #region Add SP parameters
            oDatos.Parametro("pv_usuario", string.Empty);
            oDatos.Parametro("pv_identificacion", sLicencia);
            oDatos.Parametro("pv_fecha_obtencion", "V", 60, "O");
            oDatos.Parametro("pv_prov_obtencion", "V", 60, "O");
            oDatos.Parametro("pv_clase_obtencion", "V", 60, "O");
            oDatos.Parametro("pv_documento", "V", 60, "O");
            oDatos.Parametro("pv_numero", "V", 60, "O");
            oDatos.Parametro("pv_licencia_datos", "V", 60, "O");
            oDatos.Parametro("pv_desc_lic_datos", "V", 60, "O");
            oDatos.Parametro("pv_prov_origen", "V", 60, "O");
            oDatos.Parametro("pv_fecha_origen", "V", 60, "O");
            oDatos.Parametro("pv_fecha_emision", "V", 60, "O");
            oDatos.Parametro("pv_fecha_caducidad", "V", 60, "O");
            oDatos.Parametro("c_renovaciones", "R", 0, "O");
            oDatos.Parametro("c_duplicados", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");
            #endregion
            

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    #region DataTable datos básicos
                    DataTable dtDatosBasicos = new DataTable("DatosBasicos");
                    dtDatosBasicos.Columns.Add("identificacion");
                    dtDatosBasicos.Columns.Add("fecha_obtencion");
                    dtDatosBasicos.Columns.Add("provincia_obtencion");
                    dtDatosBasicos.Columns.Add("clase");
                    dtDatosBasicos.Columns.Add("documento");
                    dtDatosBasicos.Columns.Add("numero");
                    dtDatosBasicos.Columns.Add("licencia_datos");
                    dtDatosBasicos.Columns.Add("des_licencia_datos");
                    dtDatosBasicos.Columns.Add("provincia_origen");
                    dtDatosBasicos.Columns.Add("fecha_origen");
                    dtDatosBasicos.Columns.Add("fecha_emision");
                    dtDatosBasicos.Columns.Add("fecha_caducidad");
                    #endregion
                    #region Datos Basicos
                    DataRow drDatosBasicos = dtDatosBasicos.NewRow();
                    drDatosBasicos[0] = sLicencia;
                    drDatosBasicos[1] = oDatos.RetornarParametro("pv_fecha_obtencion").ToString();
                    drDatosBasicos[2] = oDatos.RetornarParametro("pv_prov_obtencion").ToString();
                    drDatosBasicos[3] = oDatos.RetornarParametro("pv_clase_obtencion").ToString().ToUpper();
                    drDatosBasicos[4] = oDatos.RetornarParametro("pv_documento").ToString().ToUpper();
                    drDatosBasicos[5] = oDatos.RetornarParametro("pv_numero").ToString().ToUpper();
                    drDatosBasicos[6] = oDatos.RetornarParametro("pv_licencia_datos").ToString().ToUpper();
                    drDatosBasicos[7] = oDatos.RetornarParametro("pv_desc_lic_datos").ToString().ToUpper();
                    drDatosBasicos[8] = oDatos.RetornarParametro("pv_prov_origen").ToString().ToUpper();
                    try
                    {
                        drDatosBasicos[9] = oDatos.RetornarParametro("pv_fecha_origen").ToString();
                    }
                    catch
                    {
                        drDatosBasicos[9] = string.Empty;
                    }
                    try
                    {
                        drDatosBasicos[10] = oDatos.RetornarParametro("pv_fecha_emision").ToString();
                    }
                    catch (Exception e)
                    {
                        drDatosBasicos[10] = string.Empty;
                    }
                    try
                    {
                        drDatosBasicos[11] = oDatos.RetornarParametro("pv_fecha_caducidad").ToString();
                    }
                    catch (Exception e)
                    {
                        drDatosBasicos[11] = string.Empty;
                    }
                    dtDatosBasicos.Rows.Add(drDatosBasicos);
                    dsDatosLicencia.Tables.Add(dtDatosBasicos);
                    #endregion

                    #region DataTable renovaciones
                    DataTable dtRenovaciones = new DataTable("Renovaciones");
                    dtRenovaciones.Columns.Add("fecha_inicio");
                    dtRenovaciones.Columns.Add("fecha_fin");
                    dtRenovaciones.Columns.Add("tipo");
                    #endregion
                    #region Renovaciones
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow drRenovaciones = dtRenovaciones.NewRow();
                        foreach (DataColumn dc in dtRenovaciones.Columns)
                            drRenovaciones[dc.ColumnName] = oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal(dc.ColumnName));
                        dtRenovaciones.Rows.Add(drRenovaciones);
                    }
                    dsDatosLicencia.Tables.Add(dtRenovaciones);
                    #endregion

                    #region DataTable duplicados
                    DataTable dtDuplicados = new DataTable("Duplicados");
                    dtDuplicados.Columns.Add("dup_fecha_inicio");
                    dtDuplicados.Columns.Add("dup_fecha_fin");
                    dtDuplicados.Columns.Add("dup_clase");
                    #endregion
                    #region Duplicados
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow drDuplicados = dtDuplicados.NewRow();
                        foreach (DataColumn dc in dtRenovaciones.Columns)
                            drDuplicados[dc.ColumnName] = oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal(dc.ColumnName));
                        dtDuplicados.Rows.Add(drDuplicados);
                    }
                    dsDatosLicencia.Tables.Add(dtDuplicados);
                    #endregion
                }

                /*else
                {
                    //oLicencias.Add(objCrypto.CifrarCadena(oDatos.RetornarParametro("pv_error").ToString()));
                    oLicencias.Add(oDatos.Mensaje);
                }*/
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }
            return dsDatosLicencia;
        }


        public object[] GenerarCodigoCertificadoLicencia(string sLicencia, string sUsuarioWeb, string sIP, string sDNS, int idConsulado)
        {
            ArrayList oLicencias = new ArrayList();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Brevetacion.Licencia.cs GenerarCodigoCertificadoLicencia()");
            oDatos.Paquete("GCK_API_CONSULTA_WEB.GCP_EMITE_CERTIFICADO");
            oDatos.Parametro("pv_identificacion", sLicencia);
            oDatos.Parametro("pv_id_usuario", sUsuarioWeb);
            oDatos.Parametro("pv_ip", sIP);
            oDatos.Parametro("pv_dns", sDNS);
            if(idConsulado < 0)
                oDatos.Parametro("pn_consulado", 0);
            else
                oDatos.Parametro("pn_consulado", idConsulado);
            oDatos.Parametro("pv_cod_certificado", "V", 60, "O");
            oDatos.Parametro("pv_desc_usuario", "V", 60, "O");
            oDatos.Parametro("pv_cargo_usuario", "V", 60, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");

            try
            {
                if (oDatos.Ejecutar("N"))
                {
                    oLicencias.Add(oDatos.RetornarParametro("pv_cod_certificado").ToString());
                    oLicencias.Add(oDatos.RetornarParametro("pv_desc_usuario").ToString().ToUpper());
                    oLicencias.Add(oDatos.RetornarParametro("pv_cargo_usuario").ToString().ToUpper());
                }

                else
                {
                    //oLicencias.Add(objCrypto.CifrarCadena(oDatos.RetornarParametro("pv_error").ToString()));
                    throw new IndexOutOfRangeException();
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
            return oLicencias.ToArray();
        }

        public byte[] RetornarFoto(long idPersona)
        {
            byte[] foto = null;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Brevetacion.Licencia.cs RetornarFoto()");
            try
            {
                string sSql = "select imagen from CL_IMAGENES where id_persona = " + idPersona.ToString() + " and id_imagen = 'FOT'";
                oDatos.Consultar_Imagen(sSql);
                if (oDatos.oDataReader.Read())
                {
                    //foto = (byte[])oDatos.oDataReader.get_Item("imagen");
                    foto = (byte[])oDatos.oDataReader["imagen"];
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
            return foto;
        }



        #endregion

        public DataTable GetConsuladosList(string usuario)
        {
            DataTable dtConsulados = new DataTable("consulados");
            dtConsulados.Columns.Add("pais");
            dtConsulados.Columns.Add("id_consulado");

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database);
            oDatos.Paquete("gck_api_consulta_web.gcp_retorna_consulados");
            oDatos.Parametro("pv_usuario", usuario);
            oDatos.Parametro("c_consulados", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtConsulados.NewRow();
                        dr[0] = oDatos.oDataReader.GetString(oDatos.oDataReader.GetOrdinal("pais"));
                        dr[1] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("id_consulado")).ToString();
                        dtConsulados.Rows.Add(dr);
                    }
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

            return dtConsulados;
        }

        public string GetConsuladoMail(string usuario, int idConsulado)
        {
            string mail = "";
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database);
            oDatos.Paquete("gck_api_consulta_web.gcp_retorna_mail_consulado");
            oDatos.Parametro("pv_usuario", usuario);
            oDatos.Parametro("pn_consulado", idConsulado);
            oDatos.Parametro("pv_mail", "V", 120, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (error == "S")
                        mail = oDatos.RetornarParametro("pv_mail").ToString();
                    else
                        this.error = error;
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

            return mail;
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
