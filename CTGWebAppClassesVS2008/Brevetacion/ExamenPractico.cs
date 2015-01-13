using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Brevetacion
{
    public class ExamenPractico
    {
        private string dbUser;
        private string dbPassword;
        private string dbServer;
        private string error;

        public ExamenPractico(string sUsuario, string sClave, string sBaseDatos)
        {
            this.dbUser = sUsuario;
            this.dbPassword = sClave;
            this.dbServer = sBaseDatos;
        }

        public DataTable GetTurnosExamenByIdUser(string identificacion)
        {
            this.error = "";
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Brevetacion.ExamenPractico.cs GetTurnosExamenByIdUser()");

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaTurnosExamenPorLicencia);
            oDatos.Parametro("pv_identificacion", identificacion);
            oDatos.Parametro("c_exa_practico", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 2, "O");//s-exito, n-error

            DataTable dt = new DataTable("turnosexamen");
            dt.Columns.Add("nombre_escuela");
            dt.Columns.Add("nombre_persona");
            dt.Columns.Add("identificacion");
            dt.Columns.Add("fecha_examen");
            dt.Columns.Add("numero_examen");

            if (oDatos.Ejecutar("R"))
            {
                if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = oDatos.oDataReader.GetValue(0).ToString();
                        dr[1] = oDatos.oDataReader.GetValue(1).ToString();
                        dr[2] = oDatos.oDataReader.GetValue(2).ToString();
                        dr[3] = oDatos.oDataReader.GetValue(3).ToString();
                        dr[4] = oDatos.oDataReader.GetValue(4).ToString();
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    this.error = "Error al consultar turnos para examen práctico";
                }
            }
            else
            {
                this.error = "Error al consultar turnos para examen práctico";
            }

            oDatos.Dispose();
            return dt;
        }


        public DataTable GetCalificacionesExamenByIdUser(string identificacion)
        {
            this.error = "";
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Brevetacion.ExamenPractico.cs GetCalificacionesExamenByIdUser()");

            oDatos.Paquete(Constantes.StoredProcedures.ConsultaCalificacionesExamenPorLicencia);
            oDatos.Parametro("pv_identificacion", identificacion);
            oDatos.Parametro("c_cal_exa_practico", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 2, "O");//S-exito, N-error

            DataTable dt = new DataTable("calificacionesExamen");
            dt.Columns.Add("cod_examen");
            dt.Columns.Add("fec_examen");
            dt.Columns.Add("calificacion");
            dt.Columns.Add("aprobo");

            if (oDatos.Ejecutar("R"))
            {
                if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i <= 3; i++)
                        {
                            dr[i] = oDatos.oDataReader.GetValue(i).ToString();
                        }
                        dt.Rows.Add(dr);
                    }
                }
                else
                {
                    this.error = "Error al consultar calificaciones de examen práctico";
                }
            }
            else
            {
                this.error = "Error al consultar calificaciones de examen práctico";
            }

            oDatos.Dispose();
            return dt;
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
