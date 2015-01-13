using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Constantes
{
    public class Tramites
    {
        private string db_user;
        private string db_password;
        private string database;

        //public static readonly string TramitesXML = "C:\\Inetpub\\wwwroot\\CTGWebApps\\Tramites.xml";
        //public static readonly string[] TramitesXMLcolumns = { "CodigoArea", "CodigoTramite", "Nombre" };

        //public static readonly string RequisitosPorTramiteXML = "C:\\Inetpub\\wwwroot\\CTGWebApps\\RequisitosPorTramite.xml";
        //public static readonly string[] RequisitosPorTramiteXMLcolumns = { "CodigoTramite", "Nombre", "Label", "Field", "Hidden" };

        //public static readonly string ProcedimientosValidaTramiteXML = "C:\\Inetpub\\wwwroot\\CTGWebApps\\ProcValidaPorTramite.xml";
        //public static readonly string[] ProcedimientosValidaTramiteXMLcolumns = { "CodigoTramite", "Nombre", "Label", "Field" };

        public static readonly char SeparadorDecimal = ',';
        public static readonly int DecimalLong = 2;

        public static readonly string msgLocationAtencionUsuario = "Cuando el trámite que solicite se encuentre listo para ser retirado, debe acercarse al departamento de Atención al Usuario.";
        public static readonly string msgLocationMatriculacion = "Cuando el trámite que solicite se encuentre listo para ser retirado, debe acercarse al departamento de Atención al Usuario.";
        public static readonly string msgLocationCitaciones = "Cuando el trámite que solicite se encuentre listo para ser retirado, debe acercarse al departamento de Atención al Usuario.";
        public static readonly string msgLocationBrevetacion = "Luego de haber realizado su pago, puede acercarse al departamento de Brevetación (Norte o Centro).";

        public static readonly string msgExpiracionCEP = "Con el código de pago (CEP) puede pagar el valor del trámite que acaba de solicitar <i><b>(Debe imprimir este documento si va a realizar el pago a través de las ventanilla de los bancos)</b></i>. Este documento tiene una vigencia de 72 horas.";

        public static readonly string CEPestadoActivo = "A";
        public static readonly string CEPestadoInactivo = "I";
        public static readonly string CEPestadoPagado = "P";
        public static readonly string CEPestadoReversado = "R";

        public static readonly string codForPasaporte = "PAS";
        public static readonly string codForCedula = "CED";
        public static readonly string codForRUC = "RUC";
        public static readonly string labelForPasaporte = "Pasaporte";
        public static readonly string labelForCedula = "Cédula";
        public static readonly string labelForRUC = "RUC";

        public static readonly string fieldNameGenericCode = "id";
        public static readonly string fieldNameGenericName = "nombre";

        public static readonly string colNameForCEP = "cep";
        public static readonly string colNameForValorCEP = "valor";
        public static readonly string colNameForTipoCEP = "tipo";
        public static readonly string colNameForFechaIngresoCEP = "fecha_ingreso";
        public static readonly string colNameForFechaPagoReversoCEP = "fecha_pago_reverso";
        public static readonly string colNameForEstadoCEP = "pagada";
        public static readonly string colNameForCanalPagoCEP = "canal";
        public static readonly string colNameForIdTramiteCEP = "id_tramite";
        public static readonly string colNameForFechaEjecTramiteCEP = "fecha_ejecucion";
        public static readonly string colNameForEstadoTramiteCEP = "terminada";
        public static readonly string colNameForEntregaTramiteCEP = "entrega";

        public static readonly string colNameForTramiteDescripcion = "descripcion";
        public static readonly string colNameForTramiteEstado = "estado";
        public static readonly string colNameForTramiteFechaInicio = "fecha_inicio";

        public static readonly string DBentryNameForUserID = "PV_ID_IDEN_SOLICITA";

        //public static readonly string[] codesForTiposLicencias = { "A", "B", "C", "D", "E", "F", "G" };
        //public static readonly string[] labelsForTiposLicencias = { "Lic. Tipo A", "Lic. Tipo B", "Lic. Tipo C", "Lic. Tipo D", "Lic. Tipo E", "Lic. Tipo F", "Lic. Tipo G" };

        public static readonly string RequiredDocsForCertGravamen = "<br />&raquo;&nbsp;Copia de cédula de solicitante<br />&raquo;&nbsp;Copia de matrícula<br />&raquo;&nbsp;Copia a colores del SOAT";
        public static readonly string RequiredDocsForPermCirculacion = "<br />&raquo;&nbsp;Copia de factura donde debe constar el RAMV o CPN<br />&raquo;&nbsp;Copia de carta de venta<br />&raquo;&nbsp;Copia a colores de identificación(cédula, pasaporte o RUC)<br />&raquo;&nbsp;Copia a colores de certificado de votación del propietario";
        public static readonly string RequiredDocsForDupMatricula = "<br />&raquo;&nbsp;Denuncia en Fiscalía (cuando es por robo de la matrícula)<br />Denuncia en la Comisaria (cuando es por pérdida de la matrícula)<br />Copia a colores de identificación (cédula, pasaporte o RUC)<br />Copia a colores de certificado de votación del propietario<br />&raquo;&nbsp;Presentación física del vehículo al departamento de Revisión<br />&raquo;&nbsp;Planilla de servicios básicos<br />&raquo;&nbsp;Copia a colores del SOAT";
        public static readonly string RequiredDocsForCambColor = "<br />&raquo;&nbsp;Factura de talles donde realizó el cambio de color (debe estar autorizado por SRI)<br />&raquo;&nbsp;Matrícula original<br />&raquo;&nbsp;Copia a colores de identificación (cédula, pasaporte o RUC)<br />&raquo;&nbsp;Presentación física del vehículo al departamento de Revisión";
        public static readonly string RequiredDocsForDupLicencia = "<br />&raquo;&nbsp;Denuncia en Fiscalía<br />&raquo;&nbsp;Denuncia en Brevetación<br />&raquo;&nbsp;Copia de identificación(cédula, pasaporte o RUC)<br />&raquo;&nbsp;Copia de certificado de votación del propietario";
        public static readonly string RequiredDocsForRenovLicencia = "<br />&raquo;&nbsp;Licencia original<br />&raquo;&nbsp;Copia de identificación(cédula, pasaporte o RUC)<br />&raquo;&nbsp;Copia de certificado de votación del propietario<br />&raquo;&nbsp;Aprobar el exame psicotécnico";
        public static readonly string RequiredDocsForCertPosVehiculos = "<br />&raquo;&nbsp;Copia de identificación de solicitante(cédula, pasaporte o RUC)";
        public static readonly string RequiredDocsForDupCitacion = "<br />&raquo;&nbsp;Copia de identificación de solicitante(cédula, pasaporte o RUC)";


        public Tramites(string sUsuario, string sClave, string sBaseDatos)
        {
            this.db_user = sUsuario;
            this.db_password = sClave;
            this.database = sBaseDatos;
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
                dtTipoLic.Columns.Add(fieldNameGenericCode);
                dtTipoLic.Columns.Add(fieldNameGenericName);

                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.db_user, this.db_password, this.database, "Constantes.Tramites.cs TipoLicencias()");

                oDatos.Paquete(StoredProcedures.ConsultaTipoLicencias);
                oDatos.Parametro("C_LICENCIAS", "R", 0, "O");
                oDatos.Parametro("pv_error", "V", 500, "O");

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

                return dtTipoLic;
            }
        }


        public DataTable Colores
        {
            get
            {
                DataTable dtColores = new DataTable("Colores");
                dtColores.Columns.Add(fieldNameGenericCode);
                dtColores.Columns.Add(fieldNameGenericName);

                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.db_user, this.db_password, this.database, "Constantes.Tramites.cs TipoLicencias()");

                oDatos.Paquete(StoredProcedures.ConsultaTipoColores);
                oDatos.Parametro("C_COLOR", "R", 0, "O");
                oDatos.Parametro("pv_error", "V", 500, "O");

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

                return dtColores;
            }
        }


        //public static DataTable GetTramites(string codCatTramite)
        //{
        //    DataSet dsTramites = new DataSet("Tramites");
        //    dsTramites.ReadXmlSchema(TramitesXML);
        //    dsTramites.ReadXml(TramitesXML);
            
        //    DataRow[] drRows = dsTramites.Tables[0].Select(TramitesXMLcolumns[0] + "='" + codCatTramite +"'");

        //    DataTable dtTramites = new DataTable("Tramites");
        //    dtTramites.Columns.Add(fieldNameGenericCode);//id tramite
        //    dtTramites.Columns.Add(fieldNameGenericName);//nombre tramite

        //    DataRow nullrow = dtTramites.NewRow();
        //    nullrow[0] = null;
        //    nullrow[1] = null;
        //    dtTramites.Rows.Add(nullrow);

        //    for (int i = 0; i < drRows.Length; i++)
        //    {
        //        DataRow dr = dtTramites.NewRow();
        //        dr[0] = drRows[i][Constantes.Tramites.TramitesXMLcolumns[1]];
        //        dr[1] = drRows[i][Constantes.Tramites.TramitesXMLcolumns[2]];
        //        dtTramites.Rows.Add(dr);
        //    }


        //    return dtTramites;
        //}

        public DataTable GetTramitesFromDB(string codCatTramite)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.db_user, this.db_password, this.database, "Constantes.Tramites.cs GetTramitesFromDB()");
            oDatos.Paquete(StoredProcedures.ConsultaTramitesPorCategoria);
            oDatos.Parametro("PV_TIPO_PROCESO", codCatTramite);
            oDatos.Parametro("PT_PROCESOS_PORTAL", "R", 0, "O");
            oDatos.Parametro("PN_ERROR", "N", 13, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 200, "O");

            DataTable dtTramites = new DataTable("tramites");
            dtTramites.Columns.Add(Constantes.Tramites.fieldNameGenericCode);
            dtTramites.Columns.Add(Constantes.Tramites.fieldNameGenericName);

            DataRow nullrow = dtTramites.NewRow();
            nullrow[0] = null;
            nullrow[1] = null;
            dtTramites.Rows.Add(nullrow);

            if (oDatos.Ejecutar("R"))
            {
                while (oDatos.oDataReader.Read())//recorrer cursor
                {
                    DataRow dr = dtTramites.NewRow();
                    dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                    dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                    dtTramites.Rows.Add(dr);
                }
            }

            oDatos.Dispose();

            return dtTramites;
        }
        
        //public static DataRow[] GetRequisitosPorTramite(string codTramite)
        //{
        //    DataSet dsTramites = new DataSet("Requisitos");
        //    dsTramites.ReadXmlSchema(RequisitosPorTramiteXML);
        //    dsTramites.ReadXml(RequisitosPorTramiteXML);

        //    return dsTramites.Tables[0].Select(RequisitosPorTramiteXMLcolumns[0] + "='" + codTramite + "'");
        //}

        public DataRow[] GetRequisitosPorTramiteFromDB(string codTramite)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.db_user, this.db_password, this.database, "Constantes.Tramites.cs GetRequisitosPorTramiteFromDB()");

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
                        dr["ancho"] = (int.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("ANCHO")).ToString())*9);
                        dr["alto"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("ALTO")).ToString();
                        dr["tipo_dato"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("TIPO_DATO")).ToString();
                        dr["entrada"] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("ENTRADA")).ToString();
                        dtTramites.Rows.Add(dr);
                    }
                }
            }

            oDatos.Dispose();

            return dtTramites.Select();
        }


        public DataTable GetRequisitoValuesList(string listype, string proceso, 
            string etapa_proceso, string item, string query)
        {
            bool execute;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.db_user, this.db_password, this.database, "Constantes.Tramites.cs GetRequisitoValueList()");

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
                    oDatos.Parametro("PV_ETAPA_PROCESO", etapa_proceso);
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
            dtValues.Columns.Add(Constantes.Tramites.fieldNameGenericCode);
            dtValues.Columns.Add(Constantes.Tramites.fieldNameGenericName);

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

            oDatos.Dispose();

            return dtValues;
        }
    }
}
