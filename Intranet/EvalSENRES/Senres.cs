using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace EvalSENRES
{
    public class Senres
    {
        private string dbUser;
        private string dbPassword;
        private string dbServer;

        private ArrayList listApellidos, listNombres, listCedulas, listCargos, listArea, listCiudad, listCalifJefe, listCalifCiud, listCalifTot, listCalifEsc;

        private string error;

        public Senres(string dbUser, string dbPassword, string dbServer)
        {
            this.dbUser = dbUser;
            this.dbPassword = dbPassword;
            this.dbServer = dbServer;
        }

        private void InitArrays()
        {
            listApellidos = new ArrayList();
            listNombres = new ArrayList();
            listCedulas = new ArrayList();
            listCargos = new ArrayList();
            listArea = new ArrayList();
            listCiudad = new ArrayList();
            listCalifJefe = new ArrayList();
            listCalifCiud = new ArrayList();
            listCalifTot = new ArrayList();
            listCalifEsc = new ArrayList();
        }

        private string GetEscalaCalificacion(double calif_total)
        {
            if (calif_total >= 90.5)
                return "EXCELENTE";
            else if (calif_total >= 80.5)
                return "MUY BUENO";
            else if (calif_total >= 70.5)
                return "SATISFACTORIO";
            else if (calif_total >= 60.5)
                return "DEFICIENTE";
            else
                return "INACEPTABLE";
        }

        private string formatCedula(string cedula)
        {
            if (cedula.Length == 9)
                return "0" + cedula;
            else
                return cedula;
        }

        public bool ConsolidadoEvaluaciones(string fechaIni, string fechaFin)
        {
            bool retValue = false;
            InitArrays();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Citaciones.Infracciones.cs InfraccionPendiente()");

            oDatos.Paquete("clk_kactus.clp_cons_evaluaciones_senres");
            oDatos.Parametro("pv_fec_inic", fechaIni);
            oDatos.Parametro("pv_fec_fina", fechaFin);
            oDatos.Parametro("c_aprobadas", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 280, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        listApellidos.Add(oDatos.oDataReader["apellidos"].ToString());
                        listNombres.Add(oDatos.oDataReader["nombres"].ToString());
                        listCedulas.Add(formatCedula(oDatos.oDataReader["cedula"].ToString()));
                        listCargos.Add(oDatos.oDataReader["cargo"].ToString());
                        listArea.Add(oDatos.oDataReader["unidad"].ToString());
                        listCiudad.Add(oDatos.oDataReader["ciudad"].ToString());
                        double califTotal = double.Parse(oDatos.oDataReader["calif_total"].ToString()) + double.Parse(oDatos.oDataReader["calif_ciud"].ToString());
                        listCalifTot.Add(califTotal);
                        listCalifCiud.Add(double.Parse(oDatos.oDataReader["calif_ciud"].ToString()) * -1);
                        listCalifJefe.Add(double.Parse(oDatos.oDataReader["calif_total"].ToString()));
                        listCalifEsc.Add(GetEscalaCalificacion(califTotal));
                    }
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


        public string[] Nombres
        {
            get
            {
                return listNombres.ToArray(typeof(string)) as string[];
            }
        }

        public string[] Apellidos
        {
            get
            {
                return listApellidos.ToArray(typeof(string)) as string[];
            }
        }

        public string[] Cedulas
        {
            get
            {
                return listCedulas.ToArray(typeof(string)) as string[];
            }
        }

        public string[] Cargos
        {
            get
            {
                return listCargos.ToArray(typeof(string)) as string[];
            }
        }

        public string[] Unidades
        {
            get
            {
                return listArea.ToArray(typeof(string)) as string[];
            }
        }

        public string[] Ciudades
        {
            get
            {
                return listCiudad.ToArray(typeof(string)) as string[];
            }
        }

        public double[] CalificacionesJefe
        {
            get
            {
                return listCalifJefe.ToArray(typeof(double)) as double[];
            }
        }

        public double[] CalificacionesCiudadanos
        {
            get
            {
                return listCalifCiud.ToArray(typeof(double)) as double[];
            }
        }

        public double[] CalificacionesTotales
        {
            get
            {
                return listCalifTot.ToArray(typeof(double)) as double[];
            }
        }

        public string[] CalificacionesEscalas
        {
            get
            {
                return listCalifEsc.ToArray(typeof(string)) as string[];
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
