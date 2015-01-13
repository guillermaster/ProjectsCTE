using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AccesoDatos;

namespace Constantes
{
    public class Geografia
    {
        private string _sUsuario = "";
        private string _sClave = "";
        private string _sBaseDatos = "";
        private string error = "";
        
        // Constructor de la Clase
        public Geografia(string sUsuario, string sClave, string sBaseDatos)
        {
            this._sUsuario = sUsuario;
            this._sClave = sClave;
            this._sBaseDatos = sBaseDatos;
        }

        public static readonly string CodigoEcuador = "ECU";

        public DataTable GetPaisesConCodigos()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this._sUsuario, this._sClave, this._sBaseDatos, "Constantes.Geografia.cs GetPaisesConCodigos()");

            DataTable dtPaises = new DataTable("paises");
            dtPaises.Columns.Add("codigo");
            dtPaises.Columns.Add("pais");

            oDatos.Paquete("WEB_API_TRANSACCIONES.retorna_paises");
            oDatos.Parametro("C_PAISES", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 2, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtPaises.NewRow();
                            dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                            dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                            dtPaises.Rows.Add(dr);
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
                error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtPaises;

        }

        public DataTable GetPaisesConCodigosConfContinente()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this._sUsuario, this._sClave, this._sBaseDatos, "Constantes.Geografia.cs GetPaisesConCodigos()");

            DataTable dtPaises = new DataTable("paises");
            dtPaises.Columns.Add("codigo");
            dtPaises.Columns.Add("pais");

            oDatos.Paquete("WEB_API_TRANSACCIONES.retorna_paises_portal");
            oDatos.Parametro("C_PAISES", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 2, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtPaises.NewRow();
                            dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                            dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                            dtPaises.Rows.Add(dr);
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
                error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtPaises;

        }


        public DataTable GetProvinciasConCodigos(string cod_pais)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this._sUsuario, this._sClave, this._sBaseDatos, "Constantes.Geografia.cs GetProvinciasConCodigos()");

            DataTable dtProvincias = new DataTable("provincias");
            dtProvincias.Columns.Add("codigo");
            dtProvincias.Columns.Add("provincia");

            if (cod_pais != null && cod_pais != "")
            {
                oDatos.Paquete("WEB_API_TRANSACCIONES.retorna_provincias");
                oDatos.Parametro("C_PROVINCIAS", "R", 0, "O");
                oDatos.Parametro("pv_pais", cod_pais);
                oDatos.Parametro("pv_error", "V", 2, "O");

                try
                {
                    if (oDatos.Ejecutar("R"))
                    {
                        if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                        {
                            while (oDatos.oDataReader.Read())
                            {
                                DataRow dr = dtProvincias.NewRow();
                                dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                                dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                                dtProvincias.Rows.Add(dr);
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
                    error = ex.Message;
                }
                finally
                {
                    oDatos.Dispose();
                }
            }
                       

            return dtProvincias;

        }


        public DataTable GetCiudadesConCodigos(string cod_provincia)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this._sUsuario, this._sClave, this._sBaseDatos, "Constantes.Geografia.cs GetCiudadesConCodigos()");

            DataTable dtCiudades = new DataTable("ciudades");
            dtCiudades.Columns.Add("codigo");
            dtCiudades.Columns.Add("ciudad");

            if (cod_provincia != null && cod_provincia != "")
            {

                oDatos.Paquete("WEB_API_TRANSACCIONES.retorna_cantones");
                oDatos.Parametro("C_CANTONES", "R", 0, "O");
                oDatos.Parametro("pv_prov", cod_provincia);
                oDatos.Parametro("pv_error", "V", 2, "O");

                try
                {
                    if (oDatos.Ejecutar("R"))
                    {
                        if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                        {
                            while (oDatos.oDataReader.Read())
                            {
                                DataRow dr = dtCiudades.NewRow();
                                dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                                dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                                dtCiudades.Rows.Add(dr);
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
                    error = ex.Message;
                }
                finally
                {
                    oDatos.Dispose();
                }
            }
                       

            return dtCiudades;

        }


        public DataTable GetCiudadelasConCodigos(string cod_ciudad)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this._sUsuario, this._sClave, this._sBaseDatos, "Constantes.Geografia.cs GetCiudadelasConCodigos()");

            DataTable dtCiudadelas = new DataTable("ciudadelas");
            dtCiudadelas.Columns.Add("codigo");
            dtCiudadelas.Columns.Add("ciudadela");

            if (cod_ciudad != null && cod_ciudad != "")
            {

                oDatos.Paquete("WEB_API_TRANSACCIONES.retorna_ciudadelas");
                oDatos.Parametro("C_CIUDADELAS", "R", 0, "O");
                oDatos.Parametro("pv_localidad", cod_ciudad);
                oDatos.Parametro("pv_error", "V", 2, "O");

                try
                {
                    if (oDatos.Ejecutar("R"))
                    {
                        if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                        {
                            while (oDatos.oDataReader.Read())
                            {
                                DataRow dr = dtCiudadelas.NewRow();
                                dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                                dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                                dtCiudadelas.Rows.Add(dr);
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
                    error = ex.Message;
                }
                finally
                {
                    oDatos.Dispose();
                }
            }

            return dtCiudadelas;

        }
    }
}
