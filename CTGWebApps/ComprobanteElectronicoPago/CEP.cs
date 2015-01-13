using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using System.Data;


namespace ComprobanteElectronicoPago
{
    public class CEP
    {
        private string db_user;
        private string db_password;
        private string db_name;
        private string error;
        private string id_solicitud;
        private string estado;

        private string valor;

        private string cep;
        private string nombre_tramite;
        private string nombre_usuario;
        private string ident_usuario;

        public CEP(string sUsuario, string sClave, string sBaseDatos)
        {
            this.db_user = sUsuario;
            this.db_password = sClave;
            this.db_name = sBaseDatos;
        }

        public bool SolicitaCEP(string proceso, string usuario, string trama_requisitos)
        {
            bool res;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name);

            oDatos.Paquete(Constantes.StoredProcedures.SolicitaCEP);
            oDatos.Parametro("PV_PROCESO", proceso);
            oDatos.Parametro("PV_USUARIO", usuario);
            oDatos.Parametro("PV_TRAMA", trama_requisitos);
            oDatos.Parametro("PN_SOLICITUD_PORTAL", "N", 20, "O");
            oDatos.Parametro("PN_VALOR", "F", 12, "O");
            oDatos.Parametro("PN_ERROR", "N", 10, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 1000, "O");
            oDatos.Parametro("PV_MENSAJE_USR", "V", 1000, "O");

            if (oDatos.Ejecutar("R"))
            {
                string error = oDatos.RetornarParametro("pn_error").ToString();
                //ERROR 1 ->  El usuario no puede ser nulo
                //ERROR 2 -> El usuario no existe o no está registrado
                //ERROR 3 -> error oracle
                //ERROR S -> NO HAY ERROR
                if (error == "1")
                {
                    res = true;
                    this.id_solicitud = oDatos.RetornarParametro("PN_SOLICITUD_PORTAL").ToString();
                    this.valor = Utilities.Utils.FormatStringNumber(oDatos.RetornarParametro("PN_VALOR").ToString(), Constantes.Tramites.SeparadorDecimal, Constantes.Tramites.DecimalLong);
                }
                else
                {
                    res = false;
                    this.error = oDatos.RetornarParametro("PV_MENSAJE_USR").ToString();
                }
            }
            else
            {
                res = false;
                this.error = oDatos.Mensaje;
            }

            oDatos.Dispose();

            return res;
        }

        public bool GeneraCEP(string NumSolicitud, string Username)
        {
            bool return_value;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name);

            oDatos.Paquete(Constantes.StoredProcedures.GeneraCEP);
            oDatos.Parametro("pn_solicitud_portal", int.Parse(NumSolicitud));
            oDatos.Parametro("pv_usuario", Username);
            oDatos.Parametro("pv_cep", "V", 12, "O");
            oDatos.Parametro("PV_DESC_PROCESO", "V", 90, "O");
            oDatos.Parametro("PV_NOMBRE_USUARIO", "V", 90, "O");
            oDatos.Parametro("PV_IDENTIFICACION", "V", 18, "O");
            oDatos.Parametro("pn_error", "N", 10, "O");
            oDatos.Parametro("pv_mensaje", "V", 280, "O");
            oDatos.Parametro("pv_mensaje_usr", "V", 180, "O");

            if (oDatos.Ejecutar("R"))
            {
                string error = oDatos.RetornarParametro("pn_error").ToString();
                //ERROR 1 -> NO HAY ERROR
                if (error == "1")
                {
                    this.cep = oDatos.RetornarParametro("pv_cep").ToString();
                    this.nombre_tramite = oDatos.RetornarParametro("PV_DESC_PROCESO").ToString();
                    this.nombre_usuario = oDatos.RetornarParametro("PV_NOMBRE_USUARIO").ToString();
                    this.ident_usuario = oDatos.RetornarParametro("PV_IDENTIFICACION").ToString();
                    return_value = true;
                }
                else
                {
                    this.cep = null;
                    this.error = oDatos.RetornarParametro("pv_mensaje_usr").ToString();
                    return_value = false;
                }
            }
            else
            {
                this.cep = null;
                this.error = "Error al generar el código de pago, intente nuevamente.";
                return_value = false;
            }

            oDatos.Dispose();

            return return_value;
        }

        public DataTable ConsultaCEPxUsuario(string identificacion)
        {
            DataTable dtCEPs = new DataTable("CEPs");
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name);

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
                        this.error = "";
                        dtCEPs.Columns.Add(Constantes.Tramites.colNameForCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.colNameForValorCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.colNameForTipoCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.colNameForFechaIngresoCEP, typeof(DateTime));
                        dtCEPs.Columns.Add(Constantes.Tramites.colNameForFechaPagoReversoCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.colNameForEstadoCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.colNameForCanalPagoCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.colNameForIdTramiteCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.colNameForFechaEjecTramiteCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.colNameForEstadoTramiteCEP);
                        dtCEPs.Columns.Add(Constantes.Tramites.colNameForEntregaTramiteCEP);
                        dtCEPs.Columns.Add("ver_tramite");

                        while (oDatos.oDataReader.Read())//recorrer cursor
                        {
                            DataRow dr = dtCEPs.NewRow();

                            dr[Constantes.Tramites.colNameForCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForCEP)).ToString();
                            dr[Constantes.Tramites.colNameForValorCEP] = Utilities.Utils.FormatStringNumber(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForValorCEP)).ToString(), ',', 2);
                            dr[Constantes.Tramites.colNameForTipoCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForTipoCEP)).ToString();
                            dr[Constantes.Tramites.colNameForFechaIngresoCEP] = DateTime.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForFechaIngresoCEP)).ToString());
                            dr[Constantes.Tramites.colNameForFechaPagoReversoCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForFechaPagoReversoCEP)).ToString();
                            dr[Constantes.Tramites.colNameForEstadoCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForEstadoCEP)).ToString();
                            dr[Constantes.Tramites.colNameForCanalPagoCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForCanalPagoCEP)).ToString();
                            dr[Constantes.Tramites.colNameForIdTramiteCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForIdTramiteCEP)).ToString();
                            dr[Constantes.Tramites.colNameForFechaEjecTramiteCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForFechaEjecTramiteCEP)).ToString();
                            dr[Constantes.Tramites.colNameForEstadoTramiteCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForEstadoTramiteCEP)).ToString();
                            dr[Constantes.Tramites.colNameForEntregaTramiteCEP] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForEntregaTramiteCEP)).ToString();
                            if (dr[Constantes.Tramites.colNameForIdTramiteCEP].ToString() == "")
                                dr["ver_tramite"] = "";
                            else
                                dr["ver_tramite"] = "Ver trámite";
                            dtCEPs.Rows.Add(dr);
                        }

                    }
                    else
                    {
                        this.error = "Error al consultar Comprobantes Electrónicos de Pago por usuario.";
                    }
                }
                else
                {
                    this.error = "Error al consultar Comprobantes Electrónicos de Pago por usuario.";
                }
            }
            catch (Exception ex)
            {
                this.error = "Error al consultar Comprobantes Electrónicos de Pago por usuario.";
            }

            oDatos.Dispose();

            return dtCEPs;
        }

        public DataTable ConsultaDatosTramite(string id_tramite)
        {
            DataTable dtTramite = new DataTable("Tramite");
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name);

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaTramite);
            oDatos.Parametro("PN_TRAMITE", id_tramite);
            oDatos.Parametro("PT_REF_DET_TRAMITE", "R", 0, "O");
            oDatos.Parametro("PN_ERROR", "N", 3, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 280, "O");

            if (oDatos.Ejecutar("R"))
            {
                string error = oDatos.RetornarParametro("pn_error").ToString();
                //ERROR 1 -> NO HAY ERROR
                if (error == "1")
                {
                    this.error = "";
                    dtTramite.Columns.Add("id_tramite");
                    dtTramite.Columns.Add(Constantes.Tramites.colNameForTramiteDescripcion);
                    dtTramite.Columns.Add(Constantes.Tramites.colNameForTramiteEstado);
                    dtTramite.Columns.Add(Constantes.Tramites.colNameForTramiteFechaInicio);
                    
                    while (oDatos.oDataReader.Read())//recorrer cursor
                    {
                        DataRow dr = dtTramite.NewRow();
                        dr[Constantes.Tramites.colNameForTramiteDescripcion] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForTramiteDescripcion)).ToString();
                        dr[Constantes.Tramites.colNameForTramiteEstado] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForTramiteEstado)).ToString();
                        dr[Constantes.Tramites.colNameForTramiteFechaInicio] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(Constantes.Tramites.colNameForTramiteFechaInicio)).ToString();
                        dtTramite.Rows.Add(dr);
                    }

                }
                else
                {
                    this.error = "Error al consultar estado de trámite.";
                }
            }

            oDatos.Dispose();

            return dtTramite;
        }

        public string Error
        {
            get
            {
                return this.error;
            }
        }

        public string CodSolicitud
        {
            get
            {
                return this.id_solicitud;
            }
        }

        public string Estado
        {
            get
            {
                return this.estado;
            }
        }

        public string codigoCEP
        {
            get
            {
                return this.cep;
            }
        }

        public string NombreUsuario
        {
            get
            {
                return this.nombre_usuario;
            }
        }

        public string IdentificacionUsuario
        {
            get
            {
                return this.ident_usuario;
            }
        }

        public string Tramite
        {
            get
            {
                return this.nombre_tramite;
            }
        }

        public string ValorPago
        {
            get
            {
                return this.valor;
            }
        }

    }


}
