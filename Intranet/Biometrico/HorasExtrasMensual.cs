using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Oracle.DataAccess.Types;
using Oracle.DataAccess.Client;

namespace Biometrico
{
    public class HorasExtrasMensual
    {
        #region Database variables
        private string dbUser;
        private string dbPassword;
        private string dbServer;
        #endregion
        private int codEmpleado;
        #region Arreglos de campos de horas extras
        ArrayList listIdHoraExtra, listFecha;//, listFechaIngreso, listFechaSalida, 
        ArrayList listHoraIngreso, listHoraSalida;
        ArrayList listHorasExtras, listHorasAprob, listTipoHoraExtra;
        ArrayList listIdJefe, listFechaAprobJefe, listTituloTarea;
        #endregion
        private string error;

        public HorasExtrasMensual(int codEmpleado, string dbUser, string dbPassword, string dbServer)
        {
            this.codEmpleado = codEmpleado;
            this.dbUser = dbUser;
            this.dbPassword = dbPassword;
            this.dbServer = dbServer;
        }

        private void InitArrays()
        {
            listIdHoraExtra = new ArrayList();
            listFecha = new ArrayList();
            listHoraIngreso = new ArrayList();
            listHoraSalida = new ArrayList();
            listHorasExtras = new ArrayList();
            listHorasAprob = new ArrayList();
            listTipoHoraExtra = new ArrayList();
            listIdJefe = new ArrayList();
            listFechaAprobJefe = new ArrayList();
            listTituloTarea = new ArrayList();
        }

        public bool LoadHorasExtras(int mes, int anio)
        {
            bool retValue = false;
            string nMes;
            if (mes < 10)
                nMes = "0" + mes.ToString();
            else
                nMes = mes.ToString();
            InitArrays();

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Citaciones.Infracciones.cs InfraccionPendiente()");

            oDatos.Paquete("rhk_trx_biometricos.rhp_aprobadas_jefe");
            oDatos.Parametro("pv_mes", nMes);
            oDatos.Parametro("pv_anio", anio.ToString());
            oDatos.Parametro("pn_empleado", this.codEmpleado);
            oDatos.Parametro("c_aprobadas", "R", 0, "O");
            oDatos.Parametro("pn_error", "N", 1, "O");
            oDatos.Parametro("pv_mensaje", "V", 320, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    int i = 0;
                    while (oDatos.oDataReader.Read())
                    {
                        listIdHoraExtra.Add(int.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("id_horaextra")).ToString()));
                        listFecha.Add(Convert.ToDateTime(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("fecha")).ToString()).ToString("dd/MM/yyyy"));
                        listHoraIngreso.Add(Convert.ToDateTime(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("horario_ingreso")).ToString()).ToString("HH:mm:ss"));
                        listHoraSalida.Add(Convert.ToDateTime(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("horario_salida")).ToString()).ToString("HH:mm:ss"));
                        listHorasExtras.Add(double.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("horas_extra")).ToString()));
                        listHorasAprob.Add(double.Parse(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("horas_aprob")).ToString()));
                        listTipoHoraExtra.Add(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("tipo_horaextra")).ToString());
                        listIdJefe.Add(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("id_jefe")).ToString());
                        listFechaAprobJefe.Add(Convert.ToDateTime(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("fecha_aprob_jefe")).ToString()).ToString("dd/MM/yyyy"));
                        listTituloTarea.Add(oDatos.oDataReader.GetValue(oDatos.oDataReader.GetOrdinal("tarea")).ToString());
                        i++;
                    }
                    if (i > 0)
                        retValue = true;
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

            return retValue;
        }


        public int[] ListaIdHoraExtra
        {
            get
            {
                return listIdHoraExtra.ToArray(typeof(int)) as int[];
            }
        }

        public string[] ListaFecha
        {
            get
            {
                return listFecha.ToArray(typeof(string)) as string[];
            }
        }

        public string[] ListaHoraIngreso
        {
            get
            {
                return listHoraIngreso.ToArray(typeof(string)) as string[];
            }
        }

        public string[] ListaHoraSalida
        {
            get
            {
                return listHoraSalida.ToArray(typeof(string)) as string[];
            }
        }

        public double[] ListaHorasExtras
        {
            get
            {
                return listHorasExtras.ToArray(typeof(double)) as double[];
            }
        }

        public double[] ListaHorasAprob
        {
            get
            {
                return listHorasAprob.ToArray(typeof(double)) as double[];
            }
        }

        public string[] ListaTipoHoraExtra
        {
            get
            {
                return listTipoHoraExtra.ToArray(typeof(string)) as string[];
            }
        }

        public string[] ListaIdJefe
        {
            get
            {
                return listIdJefe.ToArray(typeof(string)) as string[];
            }
        }

        public string[] ListaFechaAprobJefe
        {
            get
            {
                return listFechaAprobJefe.ToArray(typeof(string)) as string[];
            }
        }

        public string[] ListaTituloTarea
        {
            get
            {
                return listTituloTarea.ToArray(typeof(string)) as string[];
            }
        }
    }
}
