using System;
using AccesoDatos;
using Constantes;

namespace CorreosDelEcuador
{
    public class Shipping
    {
        private string _dbName;
        private string _dbPassword;
        private string _dbUser;
        private string _error;

        public Shipping(string sUsuario, string sClave, string sBaseDatos)
        {
            _dbUser = sUsuario;
            _dbPassword = sClave;
            _dbName = sBaseDatos;
        }

        public string GetPaymentValue(string codPais, string codProvincia)
        {
            string value = string.Empty;
            var oDatos = new ROracle(_dbUser, _dbPassword, _dbName, "CorreosDelEcuador.Shipping.GetPaymentValue");

            oDatos.Paquete("web_api_transacciones.retorna_valor_cde");
            oDatos.Parametro("pv_provincia", codProvincia);
            oDatos.Parametro("pv_pais", codPais);
            oDatos.Parametro("pv_valor_cde", "V", 7, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");
            _error = string.Empty;
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    _error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(_error))
                    {
                        value = oDatos.RetornarParametro("pv_valor_cde").ToString();
                        value = value.Replace(Tramites.SeparadorDecimalAxis, ',');
                    }
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

            return value;
        }


        public bool SaveAddress(string numSolicitud, string idProceso, string numIdentif, string nombrePersona1,
                                string nombrePersona2,
                                string direccion, string direccionReferencia, string telefono, string celular,
                                string codCiudad, string codProvincia,
                                string codPais, string observacion)
        {
            bool retValue = false;

            var oDatos = new ROracle(_dbUser, _dbPassword, _dbName, "CorreosDelEcuador.Shipping.GetPaymentValue");

            oDatos.Paquete("wfk_trx_cep_rep.wfp_inserta_correo_web");
            oDatos.Parametro("pn_solicitud", numSolicitud);
            oDatos.Parametro("pv_id_proceso", idProceso);
            oDatos.Parametro("pv_identificacion", numIdentif);
            oDatos.Parametro("pv_nombres_completos", nombrePersona1 + " y/o " + nombrePersona2);
            oDatos.Parametro("Pv_DireccionCompleta", direccion);
            oDatos.Parametro("Pv_Telefono1", telefono);
            oDatos.Parametro("Pv_Celular", celular);
            oDatos.Parametro("Pv_IdLocalidad", codCiudad);
            oDatos.Parametro("Pv_IdPais", codPais);
            oDatos.Parametro("Pv_IdProvincia", codProvincia);
            oDatos.Parametro("Pv_Observacion", direccionReferencia);
            oDatos.Parametro("Pv_CodigoError", "V", 1000, "O");

            _error = string.Empty;
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    _error = oDatos.RetornarParametro("Pv_CodigoError").ToString();
                    if (string.IsNullOrWhiteSpace(_error))
                        retValue = true;
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
        }
    }
}