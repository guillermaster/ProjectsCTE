using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Biometrico
{
    public class Direcciones
    {
        private string dbUser;
        private string dbPassword;
        private string dbServer;
        private int codDireccion;

        private string error;

        public Direcciones(string dbUser, string dbPassword, string dbServer)
        {
            this.dbUser = dbUser;
            this.dbPassword = dbPassword;
            this.dbServer = dbServer;
        }

        public Direcciones(int codDireccion, string dbUser, string dbPassword, string dbServer)
        {
            this.codDireccion = codDireccion;
            this.dbUser = dbUser;
            this.dbPassword = dbPassword;
            this.dbServer = dbServer;
        }

        public DataTable GetAll()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("codigo");
            dtResult.Columns.Add("nombre");

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Citaciones.Infracciones.cs InfraccionPendiente()");

            oDatos.Paquete("rhk_trx_biometricos.RHP_DEPARTAMENTOS_PADRE");
            oDatos.Parametro("c_dpto_padre", "R", 0, "O");
            oDatos.Parametro("pn_error", "N", 1, "O");
            oDatos.Parametro("pv_mensaje", "V", 320, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtResult.NewRow();
                        dr[0] = int.Parse(oDatos.oDataReader.GetValue(0).ToString());
                        dr[1] = oDatos.oDataReader.GetValue(2).ToString();
                        dtResult.Rows.Add(dr);
                    }
                }
                else
                    this.error = oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtResult;
        }


        public DataTable GetDepartamentosHijos()
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("codigo");
            dtResult.Columns.Add("nombre");

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Citaciones.Infracciones.cs InfraccionPendiente()");

            oDatos.Paquete("rhk_trx_biometricos.RHP_DEPARTAMENTOS_HIJO");
            oDatos.Parametro("c_dpto_hijo", "R", 0, "O");
            oDatos.Parametro("pn_departamento", codDireccion);
            oDatos.Parametro("pn_error", "N", 1, "O");
            oDatos.Parametro("pv_mensaje", "V", 320, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtResult.NewRow();
                        dr[0] = int.Parse(oDatos.oDataReader.GetValue(0).ToString());
                        dr[1] = oDatos.oDataReader.GetValue(2).ToString();
                        dtResult.Rows.Add(dr);
                    }
                }
                else
                    this.error = oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtResult;
        }


        public DataTable GetAllEmployees(int codDepartamento)
        {
            DataTable dtResult = new DataTable();
            dtResult.Columns.Add("codigo");
            dtResult.Columns.Add("nombre");

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Citaciones.Infracciones.cs InfraccionPendiente()");

            oDatos.Paquete("rhk_trx_biometricos.RHP_EMPLEADOS_DPTO");
            oDatos.Parametro("c_empleado_dpto", "R", 0, "O");
            oDatos.Parametro("pn_departamento", codDepartamento);
            oDatos.Parametro("pn_error", "N", 1, "O");
            oDatos.Parametro("pv_mensaje", "V", 320, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtResult.NewRow();
                        dr[0] = int.Parse(oDatos.oDataReader.GetValue(0).ToString());
                        dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                        dtResult.Rows.Add(dr);
                    }
                }
                else
                    this.error = oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtResult;
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
