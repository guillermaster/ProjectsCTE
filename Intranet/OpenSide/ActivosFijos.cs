using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace OpenSide
{
    public class ActivosFijos
    {
        private string dbUser, dbPassword, dbServer, trxError;
        

        public ActivosFijos(string dbUser, string dbPassword, string dbServer)
        {
            this.dbUser = dbUser;
            this.dbPassword = dbPassword;
            this.dbServer = dbServer;
        }


        public DataTable GetActivosFijosPorEmpleado(string cedula, string codJurisdiccion)
        {
            this.trxError = string.Empty;
            DataTable dtActivos = new DataTable("ActivosFijos");
            dtActivos.Columns.Add("Cod.Area");
            dtActivos.Columns.Add("Codigo");
            dtActivos.Columns.Add("Descripcion");
            dtActivos.Columns.Add("Marca");
            dtActivos.Columns.Add("Modelo");
            dtActivos.Columns.Add("Serie");
            dtActivos.Columns.Add("Color");

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Biometrico Marcaciones.cs LoadHorariosMarcaciones");

            oDatos.Paquete("web_activos.retorna_activos_x_empleado");
            oDatos.Parametro("pv_ced_empleado", cedula);
            oDatos.Parametro("pv_cod_jur", codJurisdiccion);
            oDatos.Parametro("c_activos", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 1, "O");
            oDatos.Parametro("pv_mensaje", "V", 320, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtActivos.NewRow();
                            for (int i = 0; i < dtActivos.Columns.Count; i++)
                                dr[i] = oDatos.oDataReader[i].ToString();
                            dtActivos.Rows.Add(dr);
                        }
                    }
                    else
                        this.trxError = oDatos.RetornarParametro("pv_mensaje").ToString();
                }
                else
                {
                    this.trxError = oDatos.Mensaje;
                }
            }
            catch (Exception ex)
            {
                this.trxError = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtActivos;
        }

        public DataTable GetTiposJurisdicciones()
        {
            this.trxError = string.Empty;
            DataTable dtJur = new DataTable("JusidiccionesActivos");
            dtJur.Columns.Add("Codigo");
            dtJur.Columns.Add("Descripcion");

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Biometrico Marcaciones.cs LoadHorariosMarcaciones");

            oDatos.Paquete("web_activos.tipos_jurisdicion");
            oDatos.Parametro("c_activos", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 1, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtJur.NewRow();
                            for (int i = 0; i < dtJur.Columns.Count; i++)
                                dr[i] = oDatos.oDataReader[i].ToString();
                            dtJur.Rows.Add(dr);
                        }
                    }
                    else
                        this.trxError = "Error al consultar tipos de jurisdicciones";
                }
                else
                    this.trxError = oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                this.trxError = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtJur;
        }

        public string Error
        {
            get { return trxError; }
        }
    }
}
