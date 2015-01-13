using System;
using System.Data;

namespace ComprobanteElectronicoPago
{
    public class RegularizacionCEP
    {
        private string db_user;
        private string db_password;
        private string db_name;
        private string _error;
        public static string TipoTramite = "TRA";
        public static string TipoCitacion = "CIT";

        public RegularizacionCEP(string sUsuario, string sClave, string sBaseDatos)
        {
            db_user = sUsuario;
            db_password = sClave;
            db_name = sBaseDatos;
        }

        public bool ConciliaPago(string cep, string fecha)
        {
            bool res = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name,"ComprobanteElectronicoPago.RegularizacionCEP.ConciliaPago");

            oDatos.Paquete("web_regulariza_pagos.wfp_consulta_cep");
            oDatos.Parametro("pv_cep", (!string.IsNullOrWhiteSpace(cep)) ? cep : null);
            if(!string.IsNullOrWhiteSpace(fecha))
                oDatos.Parametro("pd_fecha",  Convert.ToDateTime(fecha));
            else
                oDatos.Parametro("pd_fecha",  null);
            oDatos.Parametro("pv_error", "V", 1000, "O");
            _error = string.Empty;
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (error == "EXITO")
                        res = true;
                    else
                        _error = error;
                }
                else
                    _error = oDatos.Mensaje;
            }
            catch(Exception ex)
            {
                _error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return res;
        }


        public bool ConciliaFechaPagoTramite(string cep, string fecha)
        {
            bool res = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name, "ComprobanteElectronicoPago.RegularizacionCEP.ConciliaPago");

            oDatos.Paquete("web_regulariza_pagos.wfp_concilia_reverso_tramites");
            oDatos.Parametro("pv_cep", cep);
            oDatos.Parametro("pd_fecha", Convert.ToDateTime(fecha));
            oDatos.Parametro("pv_error", "V", 1000, "O");
            _error = string.Empty;
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(error))
                        res = true;
                    else
                        _error = error;
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

            return res;
        }


        public bool ConciliaPagoTramite(string cep)
        {
            bool res = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name, "ComprobanteElectronicoPago.RegularizacionCEP.ConciliaPago");

            oDatos.Paquete("web_regulariza_pagos.wfp_concilia_pago_tramites");
            oDatos.Parametro("pv_cep", (!string.IsNullOrWhiteSpace(cep)) ? cep : null);
            _error = string.Empty;
            try
            {
                if (oDatos.Ejecutar("R"))
                    res = true;
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

            return res;
        }


        public bool ReversaPago(string cep)
        {
            bool res = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name, "ComprobanteElectronicoPago.RegularizacionCEP.ReversaPago");

            oDatos.Paquete("web_regulariza_pagos.wfp_reverso_cep");
            oDatos.Parametro("pv_cep", (!string.IsNullOrWhiteSpace(cep)) ? cep : null);
            _error = string.Empty;
            try
            {
                if (oDatos.Ejecutar("R"))
                    res = true;
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

            return res;
        }


        public bool ReversaPagoCNTTTSV(string cep)
        {
            bool res = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name, "ComprobanteElectronicoPago.RegularizacionCEP.ReversaPagoCNTTTSV");

            oDatos.Paquete("web_regulariza_pagos. wfp_reverso_cep_cntttsv");
            oDatos.Parametro("pv_cep", (!string.IsNullOrWhiteSpace(cep)) ? cep : null);
            _error = string.Empty;
            try
            {
                if (oDatos.Ejecutar("R"))
                    res = true;
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

            return res;
        }


        public bool ConciliaReversosAXIS()
        {
            bool res = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name, "ComprobanteElectronicoPago.RegularizacionCEP.ConciliaReversosAXIS");

            oDatos.Paquete("web_regulariza_pagos.wfp_concilia_reversos_cep");
            _error = string.Empty;
            try
            {
                if (oDatos.Ejecutar("R"))
                    res = true;
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

            return res;
        }


        public DataTable PagosEncolados(string tipo)
        {
            DataTable dtPagosEncolados = new DataTable("PagosEncolados");
            dtPagosEncolados.Columns.Add("CEP");
            dtPagosEncolados.Columns.Add("Tipo");
            dtPagosEncolados.Columns.Add("Fecha contable");
            dtPagosEncolados.Columns.Add("Cod. Banco");
            dtPagosEncolados.Columns.Add("Valor");
            dtPagosEncolados.Columns.Add("Id Proceso");
            dtPagosEncolados.Columns.Add("Usuario");
            dtPagosEncolados.Columns.Add("Modo de entrega");
            dtPagosEncolados.Columns.Add("Valor CDE");
            dtPagosEncolados.Columns.Add("Fecha de procesamiento");

            if (tipo == TipoTramite || tipo == TipoCitacion)
            {
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name, "ComprobanteElectronicoPago.RegularizacionCEP.PagosEncolados()");
                oDatos.Paquete("wfk_trx_cep_rep.wfp_consulta_pagos_encolados");
                oDatos.Parametro("pv_tipo", tipo);
                oDatos.Parametro("c_consulta_pagos", "R", 0, "O");
                oDatos.Parametro("pv_error", "V", 1000, "O");

                try
                {
                    _error = string.Empty;
                    if (oDatos.Ejecutar("R"))
                    {
                        _error = oDatos.RetornarParametro("pv_error").ToString();
                        if (string.IsNullOrWhiteSpace(_error))
                        {
                            while (oDatos.oDataReader.Read())
                            {
                                DataRow dr = dtPagosEncolados.NewRow();
                                for (int i = 0; i < dtPagosEncolados.Columns.Count; i++)
                                    dr[i] = oDatos.oDataReader[i].ToString();
                                dtPagosEncolados.Rows.Add(dr);
                            }
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
            }
            else
                _error = "Tipo de transacción no válida.";

            return dtPagosEncolados;
        }


        public DataTable ConsultaCitacionesEnCEP(string cep)
        {
            DataTable dtCEPs = new DataTable("CEPs");
            dtCEPs.Columns.Add("CEP");
            dtCEPs.Columns.Add("Código de citación");
            dtCEPs.Columns.Add("Id Factura");
            dtCEPs.Columns.Add("Valor de citación");
            dtCEPs.Columns.Add("Valor de multa Multa");
            dtCEPs.Columns.Add("Total");

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name);

            oDatos.Paquete("Wfk_trx_cep_rep.wfp_consulta_cit_cep");
            oDatos.Parametro("pv_cep", cep);
            oDatos.Parametro("c_consulta_citaciones", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");

            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {   
                    _error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(_error))
                    {
                        while (oDatos.oDataReader.Read())//recorrer cursor
                        {
                            DataRow dr = dtCEPs.NewRow();
                            dr[0] = cep;
                            for (int i = 1; i < dtCEPs.Columns.Count; i++)
                                dr[i] = oDatos.oDataReader.GetValue(i-1).ToString();
                            dtCEPs.Rows.Add(dr);
                        }
                    }
                }
                else
                {
                    _error = oDatos.Mensaje;
                }
            }
            catch(Exception ex)
            {
                _error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtCEPs;
        }


        public string Error
        {
            get
            {
                return _error;
            }
        }
    }
}
