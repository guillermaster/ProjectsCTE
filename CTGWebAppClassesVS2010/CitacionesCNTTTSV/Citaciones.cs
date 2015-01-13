using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Data;

namespace CitacionesCNTTTSV
{
    public class Citaciones
    {
        private string userDB;
        private string passwordDB;
        private string database;
        private string error;


        public Citaciones(string sUsuario, string sClave, string sBaseDatos)
        {
            this.userDB = sUsuario;
            this.passwordDB = sClave;
            this.database = sBaseDatos;
        }

        public DataTable CitacionesPorVehiculo(string placa, string chasis, string camv)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.userDB, this.passwordDB, this.database, "CitacionesCNTTTSV.Citaciones.cs CitacionesPendientesPorVehiculo()");
            oDatos.Paquete("web_corpaire_ctg.consulta_infracciones");
            oDatos.Parametro("pv_placa", placa);
            oDatos.Parametro("pv_chasis", chasis);
            oDatos.Parametro("pv_camv", camv);
            oDatos.Parametro("c_infracciones", "R", 0, "O");
            oDatos.Parametro("pn_error", "V", 2, "O");
            oDatos.Parametro("pv_mensaje_error", "V", 2000, "O");

            string[] tInfraColumns = {"CDG", "CODIGO", "TIPO_INFRAC", "LICENCIA", "TIPO_LICENCIA", "PLACA", "DIRECCION", "LOCALIDAD",
                                         "FECHA", "AGENTE", "PUNTOS", "VALOR", "USUARIO_INGRESO", "CANCELADO", "FECHAC", "USUARIOC", "SISTEMA", "BANDERA"};

            DataTable dtCitac = new DataTable("CitacionesPendientes");
            for (int i = 0; i < tInfraColumns.Length; i++)
                dtCitac.Columns.Add(tInfraColumns[i]);

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (int.Parse(oDatos.RetornarParametro("pn_error").ToString()) == 0)
                    {
                        bool hasCitacPend = false;
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtCitac.NewRow();
                            foreach (DataColumn col in dtCitac.Columns)
                                dr[col.ColumnName] = oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal(col.ColumnName)).ToString();
                            dtCitac.Rows.Add(dr);
                            hasCitacPend = true;
                        }
                        if (!hasCitacPend)
                            error = "Vehículo no tiene citaciones";
                    }
                    else
                    {
                        error = oDatos.RetornarParametro("pv_mensaje_error").ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return dtCitac;
        }


        public string Error
        {
            get
            {
                return error;
            }
        }
    }
}
