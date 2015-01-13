using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alcoholimetros
{
    public class ControlAlcoholimetro
    {
        private string _dbUser;
        private string _dbUserPwd;
        private string _dbTNS;
        private string _error;


        public ControlAlcoholimetro(string _dbUser, string _dbUserPwd, string _dbTNS)
        {
            this._dbUser = _dbUser;
            this._dbUserPwd = _dbUserPwd;
            this._dbTNS = _dbTNS;
        }


        public bool Registrar(string codPrueba, string codAlcohol, string codPersona, string licencia, string fechaPrueba,
            string aprobado, string resultado, string observacion, string estado,
            out string mensaje, out string hayError
            )
        {
            bool retValue = false;
            mensaje = string.Empty;
            hayError = string.Empty;


            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbUserPwd, _dbTNS, "Alcoholimetros.ControlAlcoholimetro.cs Registrar()");
            oDatos.Paquete("Clk_alcoholimetros.clp_inserta_prueba");
            oDatos.Parametro("pn_cod_prueba", codPrueba.Trim());
            oDatos.Parametro("pn_cod_alcoholimetro", codAlcohol.Trim());
            oDatos.Parametro("pn_cod_persona", codPersona.Trim());
            oDatos.Parametro("pv_licencia", licencia.Trim());            
            oDatos.Parametro("pv_fecha_prueba", fechaPrueba.Trim());
            oDatos.Parametro("pv_aprobado", aprobado.Trim().ToUpper());
            oDatos.Parametro("pv_resultado_prueba", resultado.Trim().ToUpper());
            oDatos.Parametro("pv_observacion", observacion.Trim().ToUpper());
            oDatos.Parametro("pv_estado", estado.Trim().ToUpper());
            oDatos.Parametro("pv_mensaje_error", "V", 2000, "O");
            oDatos.Parametro("pv_error", "V", 10, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    mensaje = oDatos.RetornarParametro("pv_mensaje_error").ToString();
                    hayError = oDatos.RetornarParametro("pv_error").ToString();                    

                    if (oDatos.RetornarParametro("pv_error").ToString() == "N")
                    {
                        retValue = true;
                    }
                    else
                    {
                        _error = oDatos.RetornarParametro("pv_mensaje_error").ToString();
                        retValue = true;
                    }
                }
                else
                {
                    _error = oDatos.Mensaje;
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }
            return retValue;
        }


        public bool Consulta(string licencia, out string codPersona, out string nombres, out string bloqueo, out string tipoLicencia,
                             out string descBloqueo, out string fechaInicio, out string fechaCaducidad, out string tipoIdentificacion,
                             out string estado, out string puntos, out string mensaje, out string hayError)
        {
            bool retValue = false;
            codPersona = string.Empty;
            nombres = string.Empty;
            bloqueo = string.Empty;
            tipoLicencia = string.Empty;
            descBloqueo = string.Empty;
            fechaInicio = string.Empty;
            fechaCaducidad = string.Empty;
            tipoIdentificacion = string.Empty;
            estado = string.Empty;
            puntos = string.Empty;
            mensaje = string.Empty;
            hayError = string.Empty;


            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbUserPwd, _dbTNS, "Alcoholimetros.ControlAlcoholimetro.cs Consulta()");
            oDatos.Paquete("Clk_alcoholimetros.clp_consulta");
            oDatos.Parametro("pv_licencia", licencia.Trim());
            oDatos.Parametro("pn_cod_persona", "N", 20, "O");
            oDatos.Parametro("pv_nombres", "V", 100, "O");
            oDatos.Parametro("pv_bloqueo", "V", 10, "O");
            oDatos.Parametro("pv_tipo_licencia", "V", 10, "O");
            oDatos.Parametro("pv_descripcion_bloqueo", "V", 100, "O");
            oDatos.Parametro("pv_fecha_inicio", "V", 30, "O");
            oDatos.Parametro("pv_fecha_caducidad", "V", 30, "O");
            oDatos.Parametro("pv_tipo_identificacion", "V", 20, "O");
            oDatos.Parametro("pv_estado", "V", 10, "O");
            oDatos.Parametro("pn_puntos", "N", 10, "O");
            
            oDatos.Parametro("pv_mensaje_error", "V", 2000, "O");
            oDatos.Parametro("pv_error", "V", 10, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    codPersona = oDatos.RetornarParametro("pn_cod_persona").ToString();
                    nombres = oDatos.RetornarParametro("pv_nombres").ToString();
                    bloqueo = oDatos.RetornarParametro("pv_bloqueo").ToString();
                    tipoLicencia = oDatos.RetornarParametro("pv_tipo_licencia").ToString();
                    descBloqueo = oDatos.RetornarParametro("pv_descripcion_bloqueo").ToString();
                    fechaInicio = oDatos.RetornarParametro("pv_fecha_inicio").ToString();
                    fechaCaducidad = oDatos.RetornarParametro("pv_fecha_caducidad").ToString();
                    tipoIdentificacion = oDatos.RetornarParametro("pv_tipo_identificacion").ToString();
                    estado = oDatos.RetornarParametro("pv_estado").ToString();
                    puntos = oDatos.RetornarParametro("pn_puntos").ToString();
                    mensaje = oDatos.RetornarParametro("pv_mensaje_error").ToString();
                    hayError = oDatos.RetornarParametro("pv_error").ToString();

                    if (oDatos.RetornarParametro("pv_error").ToString() == "N")
                    {
                        retValue = true;
                    }
                    else
                    {
                        _error = oDatos.RetornarParametro("pv_mensaje_error").ToString();
                        retValue = true;
                    }
                }
                else
                {
                    _error = oDatos.Mensaje;
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }
            return retValue;
        }



        /*public bool Consulta(string licencia, out string codPersona, out string nombres, out string bloqueo, out string tipoLicencia,
                             out string descBloqueo, out string fechaInicio, out string fechaCaducidad, out string tipoIdentificacion,
                             out string estado, out string mensaje, out string hayError)
        {
            bool retValue = false;
            _error = string.Empty;
            codPersona = string.Empty;
            nombres = string.Empty;
            bloqueo = string.Empty;
            tipoLicencia = string.Empty;
            descBloqueo = string.Empty;
            fechaInicio = string.Empty;
            fechaCaducidad = string.Empty;
            tipoIdentificacion = string.Empty;
            estado = string.Empty;
            mensaje = string.Empty;
            hayError = string.Empty;


            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbUserPwd, _dbTNS, "MatriculacionCNTTTSV.VehiculoCNTTTSV.cs Consulta()");
            oDatos.Paquete("Clk_alcoholimetros2.clp_consulta");
            oDatos.Parametro("pv_licencia", "V", 30, "A", licencia.Trim().ToUpper());

            oDatos.Parametro("pn_cod_persona", "V", 30, "O");
            oDatos.Parametro("pv_nombres", "V", 100, "O");
            oDatos.Parametro("pv_bloqueo", "V", 10, "O");
            oDatos.Parametro("pv_tipo_licencia", "V", 50, "O");
            oDatos.Parametro("pv_descripcion_bloqueo", "V", 100, "O");
            oDatos.Parametro("pv_fecha_inicio", "V", 20, "O");
            oDatos.Parametro("pv_fecha_caducidad", "V", 20, "O");
            oDatos.Parametro("pv_tipo_identificacion", "V", 10, "O");
            oDatos.Parametro("pv_estado", "V", 20, "O");

            oDatos.Parametro("pv_mensaje_error", "V", 2000, "O");
            oDatos.Parametro("pv_error", "V", 50, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "0")
                    {
                      
                        codPersona = oDatos.RetornarParametro("pn_cod_persona").ToString();
                        nombres = oDatos.RetornarParametro("pv_nombres").ToString();
                        bloqueo = oDatos.RetornarParametro("pv_bloqueo").ToString();
                        tipoLicencia = oDatos.RetornarParametro("pv_tipo_licencia").ToString();
                        bloqueo = oDatos.RetornarParametro("pv_descripcion_bloqueo").ToString();
                        fechaInicio = oDatos.RetornarParametro("pv_fecha_inicio").ToString();
                        fechaCaducidad = oDatos.RetornarParametro("pv_fecha_caducidad").ToString();
                        tipoIdentificacion = oDatos.RetornarParametro("pv_tipo_identificacion").ToString();
                        estado = oDatos.RetornarParametro("pv_estado").ToString();
                        mensaje = oDatos.RetornarParametro("pv_mensaje_error").ToString();
                        hayError = oDatos.RetornarParametro("pv_error").ToString();
                        retValue = true;
                    }
                    else
                        _error = oDatos.RetornarParametro("pv_mensaje_error").ToString();

                }
                else
                    _error = oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return retValue;
        }*/
       
        public string Error
        {
            get
            {
                return _error;
            }
        }
    }

  

}
