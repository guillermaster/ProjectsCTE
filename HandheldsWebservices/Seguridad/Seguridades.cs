using System;
using System.Collections.Generic;
using System.Text;
using Oracle.DataAccess.Types;

namespace Seguridad
{
    public class Seguridades
    {
        private string dbUser;
        private string dbPassword;
        private string dbServer;
        private string error;

        public Seguridades(string sUsuario, string sClave, string sBaseDatos)
        {
            this.dbUser = sUsuario;
            this.dbPassword = sClave;
            this.dbServer = sBaseDatos;
        }

        public string PermisosPorUsuario(string xmlUserLogin)
        {
            string xml = null;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer);
            oDatos.Paquete("stk_trx_revision_hh_axis.STP_PERMISOS_USUARIO");
            oDatos.Parametro("PL_LOGIN", xmlUserLogin);
            oDatos.Parametro("PL_XML", "V", 8000, "O");
            oDatos.Parametro("PN_ERROR", "N", 10, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 2000, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("PN_ERROR").ToString();
                    if (error == "1")
                    {
                        xml = oDatos.RetornarParametro("PL_XML").ToString();
                    }
                    else
                    {
                        this.error = oDatos.RetornarParametro("PV_MENSAJE").ToString();
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

            return xml;
        }


        public string CambiaClave(string xmlCambioClave)
        {
            string xml = null;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer);
            oDatos.Paquete("stk_trx_revision_hh_axis.STP_CAMBIA_CLAVE");
            oDatos.Parametro("PC_CAMBIO_CALVE", xmlCambioClave);
            oDatos.Parametro("PC_XML", "CLOB", "O");
            oDatos.Parametro("PN_ERROR", "N", 10, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 2000, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("PN_ERROR").ToString();
                    if (error == "1")
                    {
                        xml = ((OracleClob)oDatos.RetornarParametro("PC_XML")).Value;
                    }
                    else
                    {
                        this.error = oDatos.RetornarParametro("PV_MENSAJE").ToString();
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
            

            return xml;
        }


        public string ActualizarBase(string emei)
        {
            string xml = null;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer);
            oDatos.Paquete("stk_trx_revision_hh_axis.STP_ACTUALIZAR_BASE");
            oDatos.Parametro("PV_IMEI", emei);
            oDatos.Parametro("PV_XML", "CLOB",  "O");
            oDatos.Parametro("PN_ERROR", "N", 10, "O");
            oDatos.Parametro("PV_MENSAJE", "V", 2000, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("PN_ERROR").ToString();
                    if (error == "1")
                    {
                        xml = ((OracleClob)oDatos.RetornarParametro("PV_XML")).Value;
                    }
                    else
                    {
                        this.error = oDatos.RetornarParametro("PV_MENSAJE").ToString();
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

            return xml;
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
