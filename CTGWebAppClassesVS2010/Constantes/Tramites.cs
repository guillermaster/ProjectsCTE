using System.Collections.Generic;
using System.Data;
using System;

namespace Constantes
{
    public class Tramites
    {
        private string db_user;
        private string db_password;
        private string database;
        private string _error;

        public static readonly char SeparadorDecimalAxis = '.';
        public static readonly int DecimalLong = 2;

        public static readonly string MsgLocationAtencionUsuario = "Cuando el trámite que solicite se encuentre listo para ser retirado, debe acercarse al departamento de Atención al Usuario.";
        public static readonly string MsgLocationMatriculacion = "Cuando el trámite que solicite se encuentre listo para ser retirado, debe acercarse al departamento de Atención al Usuario.";
        public static readonly string MsgLocationCitaciones = "Cuando el trámite que solicite se encuentre listo para ser retirado, debe acercarse al departamento de Atención al Usuario.";
        public static readonly string MsgLocationBrevetacion = "Luego de haber realizado su pago, puede acercarse al departamento de Brevetación (Norte o Centro).";

        
        public static readonly string CEPestadoActivo = "A";
        public static readonly string CEPestadoInactivo = "I";
        public static readonly string CEPestadoPagado = "P";
        public static readonly string CEPestadoReversado = "R";

        public static readonly string CodForPasaporte = "PAS";
        public static readonly string CodForCedula = "CED";
        public static readonly string CodForRuc = "RUC";
        public static readonly string LabelForPasaporte = "Pasaporte";
        public static readonly string LabelForCedula = "Cédula";
        public static readonly string LabelForRuc = "RUC";

        public static readonly string FieldNameGenericCode = "id";
        public static readonly string FieldNameGenericName = "nombre";
        public static readonly string FieldNameGenericDescription = "descripcion";

        public static readonly string ColNameForCEP = "cep";
        public static readonly string ColNameForValorCEP = "valor";
        public static readonly string ColNameForTipoCEP = "tipo";
        public static readonly string ColNameForFechaIngresoCEP = "fecha_ingreso";
        public static readonly string ColNameForFechaPagoReversoCEP = "fecha_pago_reverso";
        public static readonly string ColNameForEstadoCEP = "pagada";
        public static readonly string ColNameForCanalPagoCEP = "canal";
        public static readonly string ColNameForIdTramiteCEP = "id_tramite";
        public static readonly string ColNameForFechaEjecTramiteCEP = "fecha_ejecucion";
        public static readonly string ColNameForEstadoTramiteCEP = "terminada";
        public static readonly string ColNameForEntregaTramiteCEP = "entrega";

        public static readonly string ColNameForTramiteNombre = "nombre_tramite";
        public static readonly string ColNameForTramiteDescripcion = "descripcion";
        public static readonly string ColNameForTramiteEstado = "estado";
        public static readonly string ColNameForTramiteFechaInicio = "fecha_inicio";
        public static readonly string ColNameForTramiteUsuario = "id_usuario";
        public static readonly string ColNameForTramiteLugar = "lugar";

        public static readonly string DBentryNameForUserId = "PV_ID_IDEN_SOLICITA";

        //public static readonly string[] codesForTiposLicencias = { "A", "B", "C", "D", "E", "F", "G" };
        //public static readonly string[] labelsForTiposLicencias = { "Lic. Tipo A", "Lic. Tipo B", "Lic. Tipo C", "Lic. Tipo D", "Lic. Tipo E", "Lic. Tipo F", "Lic. Tipo G" };
        
        public Tramites(string sUsuario, string sClave, string sBaseDatos)
        {
            db_user = sUsuario;
            db_password = sClave;
            database = sBaseDatos;
        }

        //public static DataTable TiposIdentificacionesNoRUC
        //{
        //    get
        //    {
        //        DataTable dtTipoIdent = new DataTable("TipoIdentificaciones");
        //        dtTipoIdent.Columns.Add(fieldNameGenericCode);
        //        dtTipoIdent.Columns.Add(fieldNameGenericName);
        //        DataRow drCed = dtTipoIdent.NewRow();
        //        drCed[0] = codForCedula;
        //        drCed[1] = labelForCedula;
        //        dtTipoIdent.Rows.Add(drCed);
        //        DataRow drPas = dtTipoIdent.NewRow();
        //        drPas[0] = codForPasaporte;
        //        drPas[1] = labelForPasaporte;
        //        dtTipoIdent.Rows.Add(drPas);
        //        return dtTipoIdent;
        //    }
        //}

        //public static DataTable TiposIdentificacionesRUC
        //{
        //    get
        //    {
        //        DataTable dtTipoIdent = new DataTable("TipoIdentificaciones");
        //        dtTipoIdent.Columns.Add(fieldNameGenericCode);
        //        dtTipoIdent.Columns.Add(fieldNameGenericName);
        //        DataRow drCed = dtTipoIdent.NewRow();
        //        drCed[0] = codForCedula;
        //        drCed[1] = labelForCedula;
        //        dtTipoIdent.Rows.Add(drCed);
        //        DataRow drPas = dtTipoIdent.NewRow();
        //        drPas[0] = codForPasaporte;
        //        drPas[1] = labelForPasaporte;
        //        dtTipoIdent.Rows.Add(drPas);
        //        DataRow drRUC = dtTipoIdent.NewRow();
        //        drRUC[0] = codForRUC;
        //        drRUC[1] = labelForRUC;
        //        dtTipoIdent.Rows.Add(drRUC);
        //        return dtTipoIdent;
        //    }
        //}

        public DataTable TiposLicencias
        {
            get
            {
                DataTable dtTipoLic = new DataTable("TipoLicencias");
                dtTipoLic.Columns.Add(FieldNameGenericCode);
                dtTipoLic.Columns.Add(FieldNameGenericName);

                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, database, "Constantes.Tramites.cs TipoLicencias()");

                oDatos.Paquete(StoredProcedures.ConsultaTipoLicencias);
                oDatos.Parametro("C_LICENCIAS", "R", 0, "O");
                oDatos.Parametro("pv_error", "V", 500, "O");

                try
                {
                    if (oDatos.Ejecutar("R"))
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtTipoLic.NewRow();
                            dr[0] = oDatos.oDataReader.GetString(0);
                            dr[1] = oDatos.oDataReader.GetString(1);
                            dtTipoLic.Rows.Add(dr);
                        }
                    }
                }
                catch
                {
                    _error = "Error al consultar listado de tipos de licencias.";
                }
                finally
                {
                    oDatos.Dispose();
                }

                return dtTipoLic;
            }
        }


        public DataTable Colores
        {
            get
            {
                DataTable dtColores = new DataTable("Colores");
                dtColores.Columns.Add(FieldNameGenericCode);
                dtColores.Columns.Add(FieldNameGenericName);

                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, database, "Constantes.Tramites.cs TipoLicencias()");

                oDatos.Paquete(StoredProcedures.ConsultaTipoColores);
                oDatos.Parametro("C_COLOR", "R", 0, "O");
                oDatos.Parametro("pv_error", "V", 500, "O");

                try
                {
                    if (oDatos.Ejecutar("R"))
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtColores.NewRow();
                            dr[0] = oDatos.oDataReader.GetString(0);
                            dr[1] = oDatos.oDataReader.GetString(1);
                            dtColores.Rows.Add(dr);
                        }
                    }
                }
                catch
                {
                    _error = "Error al consultar listado de colores";
                }
                finally
                {
                    oDatos.Dispose();
                }

                return dtColores;
            }
        }

        // Retorna el código del área (departamento) que realiza él trámite
        public string GetTipoTramite(string codTramite)
        {
            string codTipoTramite = string.Empty;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, database, "Constantes.Tramites.cs GetTipoTramite()");
            oDatos.Paquete("web_api_transacciones.retorna_tipo_proceso");
            oDatos.Parametro("pv_proceso", codTramite);
            oDatos.Parametro("pv_tipo_proceso", "V", 6, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                        codTipoTramite = oDatos.RetornarParametro("pv_tipo_proceso").ToString();
                    else
                        _error = oDatos.RetornarParametro("pv_error").ToString();
                }
                else
                    _error = "Error durante consulta.";
            }
            catch
            {
                _error = "Error al consultar tipod de trámite";
            }
            finally
            {
                oDatos.Dispose();
            }

            return codTipoTramite;
        }

        public DataTable GetTramitesFromDB(string codCatTramite)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, database, "Constantes.Tramites.cs GetTramitesFromDB()");
            oDatos.Paquete(StoredProcedures.ConsultaTramitesPorCategoria);
            oDatos.Parametro("PV_TIPO_PROCESO", codCatTramite);
            oDatos.Parametro("PT_PROCESOS_PORTAL", "R", 0, "O");
            oDatos.Parametro("PN_ERROR", "N", 13, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 200, "O");

            DataTable dtTramites = new DataTable("tramites");
            dtTramites.Columns.Add(FieldNameGenericCode);
            dtTramites.Columns.Add(FieldNameGenericName);
            dtTramites.Columns.Add(FieldNameGenericDescription);

            DataRow nullrow = dtTramites.NewRow();
            nullrow[0] = null;
            nullrow[1] = " -- Seleccione el trámite deseado --";
            dtTramites.Rows.Add(nullrow);

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())//recorrer cursor
                    {
                        DataRow dr = dtTramites.NewRow();
                        dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                        dr[1] = oDatos.oDataReader.GetValue(1).ToString().Substring(0, 1).ToUpper() + oDatos.oDataReader.GetValue(1).ToString().Substring(1).ToLower();
                        dr[2] = oDatos.oDataReader.GetValue(2).ToString();
                        dtTramites.Rows.Add(dr);
                    }
                }
            }
            catch
            {
                _error = "Error al consultar trámites";
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtTramites;
        }

        public List<string> GetDocumentacionPresentarPorTramite(string codTramite)
        {
            List<string> docsList = new List<string>();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, database, "Constantes.Tramites.cs GetTramitesFromDB()");
            oDatos.Paquete("wfk_trx_portal_servicios.wfp_documentos_requeridos");
            oDatos.Parametro("pv_proceso", codTramite);
            oDatos.Parametro("C_DOC_REQUERIDOS", "R", 0, "O");
            
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())//recorrer cursor
                        docsList.Add(oDatos.oDataReader.GetValue(0).ToString());
                    _error = string.Empty;
                }
                else
                    _error = "Error al consultar requisitos para el trámite seleccionado.";
            }
            catch
            {
                _error = "Error al consultar requisitos para el trámite seleccionado.";
            }
            finally
            {
                oDatos.Dispose();
            }
            return docsList;
        }

        public DataRow[] GetRequisitosPorTramiteFromDB(string codTramite)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, database, "Constantes.Tramites.cs GetRequisitosPorTramiteFromDB()");

            oDatos.Paquete(StoredProcedures.ConsultaRequisitosPorTramite);
            oDatos.Parametro("PV_PROCESO", codTramite);
            oDatos.Parametro("PT_DATOS_REQ", "R", 0, "O");
            oDatos.Parametro("PN_ERROR", "N", 13, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 200, "O");

            DataTable dtTramites = new DataTable("requisitos");
            dtTramites.Columns.Add("id_etapa_proceso");//0
            dtTramites.Columns.Add("item");//1
            dtTramites.Columns.Add("requerido");//2
            dtTramites.Columns.Add("etiqueta");//3
            dtTramites.Columns.Add("lista");//4
            dtTramites.Columns.Add("valor_defecto");//5
            dtTramites.Columns.Add("sentencia");//6
            dtTramites.Columns.Add("longitud");//7
            dtTramites.Columns.Add("ancho");//8
            dtTramites.Columns.Add("alto");//9
            dtTramites.Columns.Add("tipo_dato");//10
            dtTramites.Columns.Add("entrada");//11

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())//recorrer cursor
                    {
                        if (oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("HABILITAR")).ToString() == "S")
                        {
                            DataRow dr = dtTramites.NewRow();

                            dr["id_etapa_proceso"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("ID_ETAPA_PROCESO")).ToString();
                            dr["item"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("ITEM")).ToString();
                            dr["requerido"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("REQUERIDO")).ToString();
                            dr["etiqueta"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("ETIQUETA")).ToString();
                            dr["lista"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("LISTA")).ToString();
                            dr["valor_defecto"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("VALOR_DEFECTO")).ToString();
                            dr["sentencia"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("SENTENCIA")).ToString();
                            dr["longitud"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("LONGITUD")).ToString();
                            dr["ancho"] = (int.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("ANCHO")).ToString()) * 9);
                            dr["alto"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("ALTO")).ToString();
                            dr["tipo_dato"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("TIPO_DATO")).ToString();
                            dr["entrada"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("ENTRADA")).ToString();
                            dtTramites.Rows.Add(dr);
                        }
                    }
                }
            }
            catch
            {
                _error = "Error al consultar requisitos del trámite.";
            }
            finally
            {
                oDatos.Dispose();
            }
            return dtTramites.Select();
        }


        public DataTable GetRequisitoValuesList(string listype, string proceso, 
            string etapaProceso, string item, string query)
        {
            bool execute;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, database, "Constantes.Tramites.cs GetRequisitoValueList()");

            switch(listype)
            {
                case "S":
                    oDatos.Paquete(StoredProcedures.ValoresRequisitoTramite);
                    oDatos.Parametro("PV_SENTENCIA", query);
                    oDatos.Parametro("PT_VALORES_SENTENCIA", "R", 0, "O");
                    oDatos.Parametro("PN_ERROR", "N", 13, "O");
                    oDatos.Parametro("PV_MENSAJE", "V", 200, "O");
                    execute = true;
                    break;
                case "E":
                    oDatos.Paquete(StoredProcedures.ValoresDominioTramite);
                    oDatos.Parametro("PV_PROCESO", proceso);
                    oDatos.Parametro("PV_ETAPA_PROCESO", etapaProceso);
                    oDatos.Parametro("PV_ITEM", item);
                    oDatos.Parametro("PT_VALORES_DOMINIOS", "R", 0, "O");
                    oDatos.Parametro("PN_ERROR", "N", 13, "O");
                    oDatos.Parametro("PV_MENSAJE", "V", 200, "O");
                    execute = true;
                    break;
                default:
                    execute = false;
                    break;
            }

            DataTable dtValues = new DataTable("valueslist");
            dtValues.Columns.Add(FieldNameGenericCode);
            dtValues.Columns.Add(FieldNameGenericName);

            try
            {
                if (execute)
                {
                    if (oDatos.Ejecutar("R"))
                    {
                        while (oDatos.oDataReader.Read())//recorrer cursor
                        {
                            DataRow dr = dtValues.NewRow();
                            dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                            dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                            dtValues.Rows.Add(dr);
                        }
                    }
                }
            }
            catch
            {
                _error = "Error al leer lista de trámites";
            }
            finally
            {
                oDatos.Dispose();
            }
            return dtValues;
        }


        public bool TramiteRequiereEntregaDocumentos(string codTramite)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, database, "Constantes.Tramites.cs TramiteRequiereEntregaDocumentos()");
            oDatos.Paquete("web_api_transacciones.proceso_requiere_documentos");
            oDatos.Parametro("pv_proceso", codTramite);
            oDatos.Parametro("pv_requiere", "V", 1, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                    retValue = oDatos.RetornarParametro("pv_requiere").ToString() == "S";
                else
                    _error = "Error durante consulta de proceso requiere documentos";
            }
            catch
            {
                _error = "Error durante consulta de proceso requiere documentos.";
            }
            finally
            {
                oDatos.Dispose();
            }

            return retValue;
        }


        //public DataTable GetOficinasCTG(string codAreaCTG)
        public DataTable GetOficinasCTG(string codProceso, string codProvincia)
        {
            DataTable dtOficinas = new DataTable("Oficinas");
            dtOficinas.Columns.Add("id_oficina");
            dtOficinas.Columns.Add("descripcion");

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, database, "Constantes.Tramites.cs GetOficinasCTG()");
            //oDatos.Paquete("web_api_transacciones.retorna_oficinas_atu");
            oDatos.Paquete("web_api_transacciones.retorna_oficinas_web");
            oDatos.Parametro("pv_provincia", codProvincia);
            oDatos.Parametro("pv_proceso", codProceso);
            oDatos.Parametro("c_oficinas", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtOficinas.NewRow();
                            foreach (DataColumn dc in dtOficinas.Columns)
                                dr[dc.ColumnName] = oDatos.oDataReader[dc.ColumnName].ToString();
                            dtOficinas.Rows.Add(dr);
                        }
                    }
                    else
                        _error = oDatos.RetornarParametro("pv_error").ToString();
                }
                else
                    _error = oDatos.Mensaje;
                    //_error = "Error durante consulta de oficinas. Intente nuevamente refrescando la página.";
            }
            catch(Exception ex)
            {
                _error = ex.Message;
                //_error = "Error al consultar oficinas. Intente nuevamente refrescando la página.";
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtOficinas;
        }


        public string Error
        {
            get { return _error; }
        }

        public class CodigosAreas
        {
            public static readonly string AtencionUsuario = "ATW";
            //public static readonly string AtencionUsuario = "ATU";
            public static readonly string Brevetacion = "BRP";
            public static readonly string Matriculacion = "MAT";
            public static readonly string CitacionesPartes = "JPG";
        }

        public class CodigosRequisitos
        {
            public static readonly string Placa = "INF_PLACA";
        }
        
    }
}
