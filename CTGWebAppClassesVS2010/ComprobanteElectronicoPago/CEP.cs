using System;
using AccesoDatos;
using System.Data;


namespace ComprobanteElectronicoPago
{
    public class CEP
    {
        private string db_user;
        private string db_password;
        private string db_name;
        private string _error;
        private string _idSolicitud;
        private string estado;

        private string _valor;

        private string _cep;
        private string _nombreTramite;
        private string _nombreUsuario;
        private string _identUsuario;

        private string _mensaje;

        public string Mensaje
        {
            get { return _mensaje; }
            set { _mensaje = value; }
        }

        public CEP(string sUsuario, string sClave, string sBaseDatos)
        {
            db_user = sUsuario;
            estado = string.Empty;
            db_password = sClave;
            db_name = sBaseDatos;
        }

        public DataTable ModosEntregaDisponibles(string codTramiteProceso)
        {
            DataTable dtModosEntrega = new DataTable();
            dtModosEntrega.Columns.Add("codigo");
            dtModosEntrega.Columns.Add("descripcion");

            ROracle oDatos = new ROracle(db_user, db_password, db_name);
            oDatos.Paquete("wfk_trx_cep_rep.wfp_consulta_modo_entrega");
            oDatos.Parametro("pv_proceso", codTramiteProceso);
            oDatos.Parametro("c_modos", "R", 0, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())//recorrer cursor
                    {
                        DataRow dr = dtModosEntrega.NewRow();
                        for (int i = 0; i < dtModosEntrega.Columns.Count; i++)
                            dr[i] = oDatos.oDataReader.GetValue(i).ToString();
                        dtModosEntrega.Rows.Add(dr);
                    }
                }
                else
                    _error = oDatos.Mensaje;
            }
            catch
            {
                _error = "Error al cargar modos de entrega";
            }
            finally
            {
                oDatos.Dispose();
            }
            return dtModosEntrega;
        }

        public bool SolicitaCEP(string proceso, string usuario, string tramaRequisitos)
        {
            bool res = false;
            ROracle oDatos = new ROracle(db_user, db_password, db_name);

            oDatos.Paquete(Constantes.StoredProcedures.SolicitaCEP);
            oDatos.Parametro("PV_PROCESO", proceso);
            oDatos.Parametro("PV_USUARIO", usuario);
            oDatos.Parametro("PV_TRAMA", tramaRequisitos);
            oDatos.Parametro("PN_SOLICITUD_PORTAL", "N", 20, "O");
            oDatos.Parametro("PN_VALOR", "F", 12, "O");
            oDatos.Parametro("PN_ERROR", "N", 10, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 1000, "O");
            oDatos.Parametro("PV_MENSAJE_USR", "V", 1000, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    //ERROR 1 ->  El usuario no puede ser nulo
                    //ERROR 2 -> El usuario no existe o no está registrado
                    //ERROR 3 -> error oracle
                    //ERROR S -> NO HAY ERROR
                    if (error == "1")
                    {
                        _idSolicitud = oDatos.RetornarParametro("PN_SOLICITUD_PORTAL").ToString();
                        _valor = Utilities.Utils.FormatStringNumber(oDatos.RetornarParametro("PN_VALOR").ToString().Replace(Constantes.Tramites.SeparadorDecimalAxis, ','), ',', Constantes.Tramites.DecimalLong);
                        res = true;
                    }
                    else
                        _error = oDatos.RetornarParametro("PV_MENSAJE_USR").ToString();
                }
                else
                    _error = oDatos.Mensaje;
            }
            catch
            {
                _error = "Error al procesar solicitud de trámites.";
            }
            finally
            {
                oDatos.Dispose();
            }

            return res;
        }

        public bool GeneraCEP(string numSolicitud, string username, string codModoEntrega, string valorPagarCDE, string codOficinaCTG)
        {
            bool returnValue = false;
            ROracle oDatos = new ROracle(db_user, db_password, db_name);

            oDatos.Paquete(Constantes.StoredProcedures.GeneraCEP);
            oDatos.Parametro("pn_solicitud_portal", int.Parse(numSolicitud));
            oDatos.Parametro("pv_usuario", username);
            oDatos.Parametro("pv_modo_entrega", codModoEntrega);
            try
            {
                oDatos.Parametro("pn_valor_cde", float.Parse(valorPagarCDE.Replace(',', '.')));//servidor web de producción utiliza . como separador decimal
            }
            catch
            {
                oDatos.Parametro("pn_valor_cde", 0);
            }
            oDatos.Parametro("pv_oficina", codOficinaCTG);
            oDatos.Parametro("pv_cep", "V", 12, "O");
            oDatos.Parametro("PV_DESC_PROCESO", "V", 90, "O");
            oDatos.Parametro("PV_NOMBRE_USUARIO", "V", 90, "O");
            oDatos.Parametro("PV_IDENTIFICACION", "V", 18, "O");
            oDatos.Parametro("pn_error", "N", 10, "O");
            oDatos.Parametro("pv_mensaje", "V", 280, "O");
            oDatos.Parametro("pv_mensaje_usr", "V", 180, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    //ERROR 1 -> NO HAY ERROR
                    if (error == "1")
                    {
                        _cep = oDatos.RetornarParametro("pv_cep").ToString();
                        _nombreTramite = oDatos.RetornarParametro("PV_DESC_PROCESO").ToString();
                        _nombreUsuario = oDatos.RetornarParametro("PV_NOMBRE_USUARIO").ToString();
                        _identUsuario = oDatos.RetornarParametro("PV_IDENTIFICACION").ToString();
                        _mensaje = oDatos.RetornarParametro("pv_mensaje_usr").ToString();
                        returnValue = true;
                    }
                    else
                    {
                        _cep = null;
                        _error = oDatos.RetornarParametro("pv_mensaje_usr").ToString();
                    }
                }
                else
                {
                    _cep = null;
                    _error = oDatos.Mensaje;
                }
            }
            catch
            {
                _error = "Error al generar el código de pago, intente nuevamente";
            }
            finally
            {
                oDatos.Dispose();
            }

            return returnValue;
        }


        public bool ConsultaCEP(string numSolicitud, string username)
        {
            bool returnValue = false;
            ROracle oDatos = new ROracle(db_user, db_password, db_name);

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaNumCEP);
            oDatos.Parametro("pn_solicitud_portal", int.Parse(numSolicitud));
            oDatos.Parametro("pv_usuario", username);
            oDatos.Parametro("pv_cep", "V", 12, "O");
            oDatos.Parametro("PV_DESC_PROCESO", "V", 90, "O");
            oDatos.Parametro("PV_NOMBRE_USUARIO", "V", 90, "O");
            oDatos.Parametro("PV_IDENTIFICACION", "V", 18, "O");
            oDatos.Parametro("pn_error", "N", 10, "O");
            oDatos.Parametro("pv_mensaje", "V", 280, "O");
            oDatos.Parametro("pv_mensaje_usr", "V", 180, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    //ERROR 1 -> NO HAY ERROR
                    if (error == "1")
                    {
                        _cep = oDatos.RetornarParametro("pv_cep").ToString();
                        _nombreTramite = oDatos.RetornarParametro("PV_DESC_PROCESO").ToString();
                        _nombreUsuario = oDatos.RetornarParametro("PV_NOMBRE_USUARIO").ToString();
                        _identUsuario = oDatos.RetornarParametro("PV_IDENTIFICACION").ToString();
                        _mensaje = oDatos.RetornarParametro("pv_mensaje_usr").ToString();
                        returnValue = true;
                    }
                    else
                    {
                        _cep = null;
                        _error = oDatos.RetornarParametro("pv_mensaje_usr").ToString();
                    }
                }
                else
                {
                    _cep = null;
                    _error = oDatos.Mensaje;
                }
            }
            catch
            {
                _error = "Error al generar el código de pago, intente nuevamente";
            }
            finally
            {
                oDatos.Dispose();
            }

            return returnValue;
        }


        public DataTable ConsultaCEPxUsuario(string identificacion)
        {
            DataTable dtCEPs = new DataTable("CEPs");
            ROracle oDatos = new ROracle(db_user, db_password, db_name);

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaCEPxUsuario);
            oDatos.Parametro("PV_USUARIO", identificacion);
            oDatos.Parametro("PT_CEP_POR_USUARIO", "R", 0, "O");
            oDatos.Parametro("PN_ERROR", "N", 3, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 280, "O");

            try
            {

                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    //ERROR 1 -> NO HAY ERROR
                    if (error == "1")
                    {
                        _error = "";
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForValorCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForTipoCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForFechaIngresoCEP, typeof(DateTime));
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForFechaPagoReversoCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForEstadoCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForCanalPagoCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForIdTramiteCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForFechaEjecTramiteCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForEstadoTramiteCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForEntregaTramiteCEP);
                        dtCEPs.Columns.Add("ver_tramite");

                        while (oDatos.oDataReader.Read())//recorrer cursor
                        {
                            DataRow dr = dtCEPs.NewRow();

                            dr[Constantes.Tramites.ColNameForCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForCEP)).ToString();
                            dr[Constantes.Tramites.ColNameForValorCEP] = Utilities.Utils.FormatStringNumber(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForValorCEP)).ToString(), ',', 2);
                            dr[Constantes.Tramites.ColNameForTipoCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForTipoCEP)).ToString();
                            dr[Constantes.Tramites.ColNameForFechaIngresoCEP] = DateTime.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForFechaIngresoCEP)).ToString());
                            dr[Constantes.Tramites.ColNameForFechaPagoReversoCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForFechaPagoReversoCEP)).ToString();
                            dr[Constantes.Tramites.ColNameForEstadoCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForEstadoCEP)).ToString();
                            dr[Constantes.Tramites.ColNameForCanalPagoCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForCanalPagoCEP)).ToString();
                            dr[Constantes.Tramites.ColNameForIdTramiteCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForIdTramiteCEP)).ToString();
                            dr[Constantes.Tramites.ColNameForFechaEjecTramiteCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForFechaEjecTramiteCEP)).ToString();
                            dr[Constantes.Tramites.ColNameForEstadoTramiteCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForEstadoTramiteCEP)).ToString();
                            dr[Constantes.Tramites.ColNameForEntregaTramiteCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForEntregaTramiteCEP)).ToString();
                            if (dr[Constantes.Tramites.ColNameForIdTramiteCEP].ToString() == "")
                                dr["ver_tramite"] = "";
                            else
                                dr["ver_tramite"] = "Ver trámite";
                            dtCEPs.Rows.Add(dr);
                        }

                    }
                    else
                    {
                        _error = oDatos.RetornarParametro("pv_mensaje").ToString();
                    }
                }
                else
                {
                    _error = "Error al consultar Comprobantes Electrónicos de Pago por usuario.";
                }
            }
            catch
            {
                _error = "Error al consultar Comprobantes Electrónicos de Pago por usuario";
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtCEPs;
        }


        public DataTable ConsultaCEPTramitexUsuario(string identificacion, bool terminado)
        {
            DataTable dtCEPs = new DataTable("CEPs");
            ROracle oDatos = new ROracle(db_user, db_password, db_name);

            oDatos.Paquete("WFK_WORKFLOW_PORTAL.WFP_CEP_POR_USUARIO_TRA");
            //oDatos.Paquete("WFK_WORKFLOW_PORTAL.WFP_CEP_POR_USUARIO");
            oDatos.Parametro("PV_USUARIO", identificacion);
            oDatos.Parametro("PV_TERMINADO", terminado ? "S" : "N");
            oDatos.Parametro("PT_CEP_POR_USUARIO", "R", 0, "O");
            oDatos.Parametro("PN_ERROR", "N", 3, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 280, "O");

            try
            {

                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    //ERROR 1 -> NO HAY ERROR
                    if (error == "1")
                    {
                        _error = "";
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForValorCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForTipoCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForFechaIngresoCEP, typeof(DateTime));
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForFechaPagoReversoCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForEstadoCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForCanalPagoCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForIdTramiteCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForFechaEjecTramiteCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForEstadoTramiteCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForEntregaTramiteCEP);
                        dtCEPs.Columns.Add("nombre_tramite");

                        while (oDatos.oDataReader.Read())//recorrer cursor
                        {
                            DataRow dr = dtCEPs.NewRow();
                            for (int i = 0; i < dtCEPs.Columns.Count; i++)
                                dr[dtCEPs.Columns[i].ColumnName] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtCEPs.Columns[i].ColumnName)).ToString();
                            dtCEPs.Rows.Add(dr);
                        }

                    }
                    else
                    {
                        _error = oDatos.RetornarParametro("pv_mensaje").ToString();
                    }
                }
                else
                {
                    _error = "Error al consultar Comprobantes Electrónicos de Pago por usuario.";
                }
            }
            catch(Exception ex)
            {
                _error = "Error al consultar Comprobantes Electrónicos de Pago por usuario";
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtCEPs;
        }


        public DataTable ConsultaCEPCitacionesPagadasxUsuario(string identificacion)
        {
            DataTable dtCEPs = new DataTable("CEPs");
            ROracle oDatos = new ROracle(db_user, db_password, db_name);

            try
            {
                string query = "select w.cep, fecha_pago_reverso, g.descripcion, w.valor "
                                + "from   wf_cep w, ge_financieras g "
                                + "where  w.cod_banco = g.id_financiera "
                                + "and    w.tipo = 'CIT' "
                                + "and    w.usuario = '" + identificacion + "' "
                                + "and    w.estado_pagado = 'P'";

                _error = string.Empty;
                if (oDatos.EjecutarQuery(query))
                {
                    dtCEPs.Columns.Add("CEP");
                    dtCEPs.Columns.Add("Fecha de Pago");
                    dtCEPs.Columns.Add("Entidad financiera");
                    dtCEPs.Columns.Add("Valor ($)");

                    while (oDatos.oDataReader.Read())//recorrer cursor
                    {
                        DataRow dr = dtCEPs.NewRow();
                        for (int i = 0; i < dtCEPs.Columns.Count; i++)
                            dr[i] = oDatos.oDataReader.GetValue(i).ToString();
                        dtCEPs.Rows.Add(dr);
                    }
                }
                else
                    _error = "Error al consultar Comprobantes Electrónicos de Pago de citaciones por usuario.";
            }
            catch
            {
                _error = "Error al consultar Comprobantes Electrónicos de Pago de citaciones por usuario";
            }
            finally
            {
                oDatos.Dispose();
            }
            /*oDatos.Paquete("wfk_trx_cep_rep.wfp_consulta_pagos_usuario");
            oDatos.Parametro("pv_identificacion", identificacion);
            oDatos.Parametro("c_consulta_pagos", "R", 0, "O");

            try
            {

                if (oDatos.Ejecutar("R"))
                {
                    _error = string.Empty;
                    dtCEPs.Columns.Add("CEP");
                    dtCEPs.Columns.Add("Fecha de Pago");
                    dtCEPs.Columns.Add("Entidad financiera");
                    dtCEPs.Columns.Add("Valor Pagado");

                    while (oDatos.oDataReader.Read())//recorrer cursor
                    {
                        DataRow dr = dtCEPs.NewRow();
                        for (int i = 0; i < dtCEPs.Columns.Count; i++)
                            dr[i] = oDatos.oDataReader.GetValue(i).ToString();
                        dtCEPs.Rows.Add(dr);
                    }
                }
                else
                {
                    _error = "Error al consultar Comprobantes Electrónicos de Pago de citaciones por usuario.";
                }
            }
            catch
            {
                _error = "Error al consultar Comprobantes Electrónicos de Pago de citaciones por usuario";
            }
            finally
            {
                oDatos.Dispose();
            }*/

            return dtCEPs;
        }


        public DataTable ConsultaCitacionesEnCEP(string cep)
        {
            DataTable dtCEPs = new DataTable("CEPs");
            ROracle oDatos = new ROracle(db_user, db_password, db_name);

            oDatos.Paquete("wfk_trx_cep_rep.wfp_consulta_citaciones_cep");
            oDatos.Parametro("pv_cep", cep);
            oDatos.Parametro("c_consulta_citaciones", "R", 0, "O");

            try
            {

                if (oDatos.Ejecutar("R"))
                {
                    _error = string.Empty;
                    dtCEPs.Columns.Add("Código de citación");
                    dtCEPs.Columns.Add("Tipo");
                    dtCEPs.Columns.Add("Licencia del infractor");
                    dtCEPs.Columns.Add("Placa de vehículo");
                    dtCEPs.Columns.Add("Fecha de citación");
                    dtCEPs.Columns.Add("Valor de citación ($)");
                    dtCEPs.Columns.Add("Multa ($)");
                    dtCEPs.Columns.Add("Total a pagar ($)");

                    while (oDatos.oDataReader.Read())//recorrer cursor
                    {
                        DataRow dr = dtCEPs.NewRow();
                        for (int i = 0; i < dtCEPs.Columns.Count; i++)
                            dr[i] = oDatos.oDataReader.GetValue(i).ToString();
                        dtCEPs.Rows.Add(dr);
                    }
                }
                else
                {
                    _error = "Error al consultar citaciones.";
                }
            }
            catch
            {
                _error = "Error al consultar citaciones";
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtCEPs;
        }


        public DataTable ConsultaCEPNoPagados(string identificacion)
        {
            DataTable dtCEPs = new DataTable("CEPs");
            ROracle oDatos = new ROracle(db_user, db_password, db_name);

            try
            {
                string query = "select w.cep, w.usuario, w.fecha_ingreso, upper(p.descripcion) proceso "
                                + "from   transito.wf_cep w, transito.wf_procesos_axis  p "
                                + "where  w.usuario = '" + identificacion + "' "
                                + "and    w.id_proceso = p.id_proceso "
                                + "and    w.tipo = 'TRA' "
                                + "and    w.estado = 'A' "
                                + "and    w.estado_pagado is null";
                _error = "";
                if (oDatos.EjecutarQuery(query))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    //ERROR 1 -> NO HAY ERROR
                    if (string.IsNullOrWhiteSpace(error))
                    {                        
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForCEP);
                        dtCEPs.Columns.Add("usuario");
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForFechaIngresoCEP, typeof(DateTime));
                        dtCEPs.Columns.Add("proceso");

                        while (oDatos.oDataReader.Read())//recorrer cursor
                        {
                            DataRow dr = dtCEPs.NewRow();
                            for (int i = 0; i < dtCEPs.Columns.Count; i++)
                                dr[dtCEPs.Columns[i].ColumnName] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtCEPs.Columns[i].ColumnName)).ToString();
                            dtCEPs.Rows.Add(dr);
                        }

                    }
                    else
                    {
                        _error = oDatos.RetornarParametro("pv_mensaje").ToString();
                    }
                }
                else
                {
                    _error = "Error al consultar Comprobantes Electrónicos de Pago por usuario.";
                }
            }
            catch
            {
                _error = "Error al consultar Comprobantes Electrónicos de Pago por usuario";
            }
            finally
            {
                oDatos.Dispose();
            }

            /*oDatos.Paquete("wfk_trx_cep_rep.wfp_consulta_proceso_usuario");
            oDatos.Parametro("identificacion", identificacion);
            oDatos.Parametro("c_consulta_procesos", "R", 0, "O");

            try
            {

                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    //ERROR 1 -> NO HAY ERROR
                    if (string.IsNullOrWhiteSpace(error))
                    {
                        _error = "";
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForCEP);
                        dtCEPs.Columns.Add("usuario");
                        dtCEPs.Columns.Add(Constantes.Tramites.ColNameForFechaIngresoCEP, typeof(DateTime));
                        dtCEPs.Columns.Add("proceso");

                        while (oDatos.oDataReader.Read())//recorrer cursor
                        {
                            DataRow dr = dtCEPs.NewRow();
                            for (int i = 0; i < dtCEPs.Columns.Count; i++)
                                dr[dtCEPs.Columns[i].ColumnName] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(dtCEPs.Columns[i].ColumnName)).ToString();
                            dtCEPs.Rows.Add(dr);
                        }

                    }
                    else
                    {
                        _error = oDatos.RetornarParametro("pv_mensaje").ToString();
                    }
                }
                else
                {
                    _error = "Error al consultar Comprobantes Electrónicos de Pago por usuario.";
                }
            }
            catch
            {
                _error = "Error al consultar Comprobantes Electrónicos de Pago por usuario";
            }
            finally
            {
                oDatos.Dispose();
            }*/

            return dtCEPs;
        }


        public DataTable ConsultaDatosTramite(string idTramite)
        {
            DataTable dtTramite = new DataTable("Tramite");
            ROracle oDatos = new ROracle(db_user, db_password, db_name);

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaTramite);
            oDatos.Parametro("PN_TRAMITE", idTramite);
            oDatos.Parametro("PT_REF_DET_TRAMITE", "R", 0, "O");
            oDatos.Parametro("PN_ERROR", "N", 3, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 280, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    //ERROR 1 -> NO HAY ERROR
                    if (error == "1")
                    {
                        _error = "";
                        dtTramite.Columns.Add("paso");
                        dtTramite.Columns.Add(Constantes.Tramites.ColNameForTramiteNombre);
                        dtTramite.Columns.Add(Constantes.Tramites.ColNameForTramiteDescripcion);
                        dtTramite.Columns.Add(Constantes.Tramites.ColNameForTramiteEstado);
                        dtTramite.Columns.Add(Constantes.Tramites.ColNameForTramiteFechaInicio);
                        dtTramite.Columns.Add(Constantes.Tramites.ColNameForTramiteUsuario);
                        dtTramite.Columns.Add(Constantes.Tramites.ColNameForTramiteLugar);
                        int i = 1;
                        while (oDatos.oDataReader.Read())//recorrer cursor
                        {
                            DataRow dr = dtTramite.NewRow();
                            dr["paso"] = i;
                            dr[Constantes.Tramites.ColNameForTramiteNombre] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForTramiteNombre)).ToString();
                            dr[Constantes.Tramites.ColNameForTramiteDescripcion] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForTramiteDescripcion)).ToString();
                            dr[Constantes.Tramites.ColNameForTramiteEstado] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForTramiteEstado)).ToString();
                            dr[Constantes.Tramites.ColNameForTramiteFechaInicio] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForTramiteFechaInicio)).ToString();
                            dr[Constantes.Tramites.ColNameForTramiteUsuario] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForTramiteUsuario)).ToString();
                            dr[Constantes.Tramites.ColNameForTramiteLugar] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.ColNameForTramiteLugar)).ToString();
                            dtTramite.Rows.Add(dr);
                            i++;
                        }

                    }
                    else
                    {
                        _error = "Error al consultar estado de trámite.";
                    }
                }
            }
            catch
            {
                _error = "Error al consultar estado de trámite";
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtTramite;
        }


        public DataTable ConsultaTramitesPorUsuario(string identificacion, string tipoIdentificacion)
        {
            DataTable dtTramite = new DataTable("Tramites");
            ROracle oDatos = new ROracle(db_user, db_password, db_name);

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaTramitesPorUsuario);
            oDatos.Parametro("pn_tramite", string.Empty);

            if(tipoIdentificacion == Constantes.TipoIdentificacion.Vehiculo.CEDRUCPAS.ToString())
                oDatos.Parametro("pv_identificacion", identificacion);
            else
                oDatos.Parametro("pv_identificacion", string.Empty);

            if (tipoIdentificacion == Constantes.TipoIdentificacion.Vehiculo.PLACA.ToString())
                oDatos.Parametro("pv_placa", identificacion);
            else
                oDatos.Parametro("pv_placa", string.Empty);

            oDatos.Parametro("pt_ref_cons_tramite", "R", 0, "O");
            oDatos.Parametro("pn_error", "N", 3, "O");
            oDatos.Parametro("pv_mensaje", "V", 280, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pn_error").ToString();
                    //ERROR 1 -> NO HAY ERROR
                    if (error == "1")
                    {
                        _error = "";
                        dtTramite.Columns.Add("No.Trámite");
                        dtTramite.Columns.Add("Trámite");
                        dtTramite.Columns.Add("Id.Solicitante");
                        dtTramite.Columns.Add("Fecha de solicitud");
                        while (oDatos.oDataReader.Read())//recorrer cursor
                        {
                            DataRow dr = dtTramite.NewRow();
                            for (int i = 0; i < dtTramite.Columns.Count; i++)
                                dr[i] = oDatos.oDataReader.GetValue(i).ToString();
                            dtTramite.Rows.Add(dr);
                        }

                    }
                    else
                    {
                        _error = "Error al consultar trámites por usuario.";
                    }
                }
            }
            catch
            {
                _error = "Error al consultar trámites por usuario";
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtTramite;
        }


        public string Error
        {
            get
            {
                return _error;
            }
        }

        public string CodSolicitud
        {
            get
            {
                return _idSolicitud;
            }
        }

        public string Estado
        {
            get
            {
                return estado;
            }
        }

        public string codigoCEP
        {
            get
            {
                return _cep;
            }
        }

        public string NombreUsuario
        {
            get
            {
                return _nombreUsuario;
            }
        }

        public string IdentificacionUsuario
        {
            get
            {
                return _identUsuario;
            }
        }

        public string Tramite
        {
            get
            {
                return _nombreTramite;
            }
        }

        public string ValorPago
        {
            get
            {
                return _valor;
            }
        }

        public enum ModosEntrega
        {
            WEB, CTG, CDE
        }

    }


}
