using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MatriculacionCNTTTSV
{
    public class RevisionCorpaire
    {
        private string userDB;
        private string passwordDB;
        private string database;
        private string error;
        private string codError;

        public RevisionCorpaire(string sUsuario, string sClave, string sBaseDatos)
        {
            this.userDB = sUsuario;
            this.passwordDB = sClave;
            this.database = sBaseDatos;
        }

        public bool IngresarRevision(int codigo_revision, int numero_revision, DateTime fecha_revision, DateTime fecha_vigencia,
        string placa, string chasis, string ramv, string usuario_ingresa, DateTime fecha_ingresa, 
        char estado, int cod_zona, int cod_agencia, int numero_certificado, int numero_adhesiva)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.userDB, this.passwordDB, this.database, "MatriculacionCNTTTSV.VehiculoCNTTTSV.IngresaRevision()");
            oDatos.Paquete("Web_corpaire_ctg.ingresa_revision");
            oDatos.Parametro("pn_cod_revision", codigo_revision);
            oDatos.Parametro("pn_num_revision", numero_revision);
            oDatos.Parametro("pd_fec_revision", fecha_revision);
            oDatos.Parametro("pd_fec_vigencia", fecha_vigencia);
            oDatos.Parametro("pv_placa", placa);
            oDatos.Parametro("pv_chasis", chasis);
            oDatos.Parametro("pv_ramv", ramv);
            oDatos.Parametro("pv_usr_ingresa", usuario_ingresa);
            oDatos.Parametro("pd_fec_ingresa", fecha_ingresa);
            oDatos.Parametro("pv_estado", estado);
            oDatos.Parametro("pn_cod_zona", cod_zona);
            oDatos.Parametro("pn_cod_agencia", cod_agencia);
            oDatos.Parametro("pn_num_certificado", numero_certificado);
            oDatos.Parametro("pn_num_adhesiva", numero_adhesiva);
            oDatos.Parametro("pv_error", "V", 4, "O");

            try
            {
                this.codError = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    this.codError = oDatos.RetornarParametro("pv_error").ToString();
                    if (this.codError == "C000")
                    {
                        retValue = true;
                    }
                }
                else
                {
                    this.error = oDatos.Mensaje;
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

            return retValue;
        }


        public bool ReversarRevision(int codigo_revision, int numero_revision, string usuario_reversa, DateTime fecha_reversa)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.userDB, this.passwordDB, this.database, "MatriculacionCNTTTSV.VehiculoCNTTTSV.IngresaRevision()");
            oDatos.Paquete("Web_corpaire_ctg.reversa_revision");
            oDatos.Parametro("pn_cod_revision", codigo_revision);
            oDatos.Parametro("pn_num_revision", numero_revision);
            oDatos.Parametro("pv_usr_rev", usuario_reversa);
            oDatos.Parametro("pd_fec_rev", fecha_reversa);
            oDatos.Parametro("pv_error", "V", 4, "O");

            try
            {
                this.codError = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    this.codError = oDatos.RetornarParametro("pv_error").ToString();
                    if (this.codError == "C000")
                    {
                        retValue = true;
                    }
                }
                else
                {
                    this.error = oDatos.Mensaje;
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

            return retValue;
        }


        public bool ActualizarRevision(int codigo_revision, int numero_revision, int numero_certificado, 
            int numero_adhesiva, string placa, string dui, string usuario_modifica, DateTime fecha_modifica, 
            int cod_zona, int cod_agencia, DateTime fecha_revision, string estado_revision, string chasis)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.userDB, this.passwordDB, this.database, "MatriculacionCNTTTSV.VehiculoCNTTTSV.IngresaRevision()");
            oDatos.Paquete("Web_corpaire_ctg.actualiza_revision");
            oDatos.Parametro("pn_cod_revision", codigo_revision);
            oDatos.Parametro("pn_num_revision", numero_revision);
            oDatos.Parametro("pn_num_certificado", numero_certificado);
            oDatos.Parametro("pn_num_adhesiva", numero_adhesiva);
            oDatos.Parametro("pv_placa", placa);
            oDatos.Parametro("pv_dui", dui);
            oDatos.Parametro("pv_usr_mod", usuario_modifica);
            oDatos.Parametro("pd_fec_mod", fecha_modifica);
            oDatos.Parametro("pn_zona", cod_zona);
            oDatos.Parametro("pn_agencia", cod_agencia);
            oDatos.Parametro("pd_fec_revision", fecha_revision);
            oDatos.Parametro("pv_est_revision", estado_revision);
            oDatos.Parametro("pv_chasis", chasis);
            oDatos.Parametro("pv_error", "V", 4, "O");

            try
            {
                this.codError = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    this.codError = oDatos.RetornarParametro("pv_error").ToString();
                    if (this.codError == "C000")
                    {
                        retValue = true;
                    }
                }
                else
                {
                    this.error = oDatos.Mensaje;
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

            return retValue;
        }

        private DataTable InitTableRevision()
        {
            DataTable dtRevision = new DataTable("Revision");
            dtRevision.Columns.Add("COD_REVISION");
            dtRevision.Columns.Add("NUM_REVISION");
            dtRevision.Columns.Add("FECHA_REVISION");
            dtRevision.Columns.Add("FECHA_VIGENCIA");
            dtRevision.Columns.Add("PLACA");
            dtRevision.Columns.Add("CHASIS");
            dtRevision.Columns.Add("RAMV");
            dtRevision.Columns.Add("USR_ING");
            dtRevision.Columns.Add("FEC_ING");
            dtRevision.Columns.Add("USR_MOD");
            dtRevision.Columns.Add("FEC_MOD");
            dtRevision.Columns.Add("ESTADO");
            dtRevision.Columns.Add("COD_ZONA");
            dtRevision.Columns.Add("COD_AGENCIA");
            dtRevision.Columns.Add("NUM_CERTIFICADO");
            dtRevision.Columns.Add("NUM_ADHESIVA");
            dtRevision.Columns.Add("USR_REV");
            dtRevision.Columns.Add("FEC_REV");
            return dtRevision;
        }

        public DataTable ConsultaRevision(int codigo_revision, int numero_revision)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.userDB, this.passwordDB, this.database, "MatriculacionCNTTTSV.VehiculoCNTTTSV.cs LoadDatosVehiculo()");
            oDatos.Paquete("WEB_CORPAIRE_CTG.consulta_revision");
            oDatos.Parametro("pn_cod_revision", codigo_revision);
            oDatos.Parametro("pn_num_revision", numero_revision);
            oDatos.Parametro("c_revisiones", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 4, "O");

            DataTable dtRevision = InitTableRevision();

            try
            {
                this.codError = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    this.codError = oDatos.RetornarParametro("pv_error").ToString();
                    if (this.codError == "C000")
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtRevision.NewRow();
                            for (int i = 0; i < dtRevision.Columns.Count; i++)
                                dr[i] = oDatos.oDataReader[dtRevision.Columns[i].ColumnName];
                            dtRevision.Rows.Add(dr);
                        }
                    }
                }
                else
                {
                    this.error = oDatos.Mensaje;
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

            return dtRevision;
        }


        public string CodigoErrorEvento
        {
            get
            {
                return this.codError;
            }
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
