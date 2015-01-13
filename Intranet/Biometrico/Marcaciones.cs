using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
//[assembly: AllowPartiallyTrustedCallers]


namespace Biometrico
{
    public class Marcaciones
    {
        private string codEmpleado;
        private string appUser, dbUser, dbPassword, dbServer;
        private string trxError;
        private string trxMessage;
        private string horaEntradaMarcada;
        private string horaEntradaHorario;
        private string horaSalidaMarcada;
        private string horaSalidaHorario;
        private int tipoEmpleadoCod;
        private string tipoEmpleadoDesc;
        private string tipoHora;
        private int maxHorasExtras;
        private int dGracia;
        private double numHorasExtras;

        #region "Constructores"
        public Marcaciones(string codEmpleado, string dbUser, string dbPassword, string dbServer)
        {
            this.codEmpleado = codEmpleado;
            this.dbUser = dbUser;
            this.dbPassword = dbPassword;
            this.dbServer = dbServer;
        }
        #endregion

        public string GetHoraEntrada(DateTime fecha)
        {
            string hora = null;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Biometrico Marcaciones.cs GetHoraEntrada");

            oDatos.Paquete("web_biometrico.retorna_marcacion_entrada");
            oDatos.Parametro("pv_cod_empleado", this.codEmpleado);
            //oDatos.Parametro("pd_fecha", fecha.ToString("dd/MM/yyyy"));
            oDatos.Parametro("pd_fecha", fecha);
            oDatos.Parametro("pv_hora", "V", 15, "O");
            oDatos.Parametro("pv_error", "V", 220, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "")
                        hora = oDatos.RetornarParametro("pv_hora").ToString();
                    else
                        this.trxError = oDatos.RetornarParametro("pv_error").ToString();
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

            return hora;
        }

        public bool LoadHorariosMarcaciones(DateTime fecha)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Biometrico Marcaciones.cs LoadHorariosMarcaciones");

            oDatos.Paquete("rhk_trx_biometricos.rhp_consulta_horarios");
            oDatos.Parametro("pn_codigo_emp", int.Parse(this.codEmpleado));
            oDatos.Parametro("pv_Fecha", fecha.ToString("dd/MM/yyyy"));
            oDatos.Parametro("pv_MEntrada", "V", 20, "O");
            oDatos.Parametro("pv_MSalida", "V", 20, "O");
            oDatos.Parametro("pv_HEntrada", "V", 20, "O");
            oDatos.Parametro("pv_HSalida", "V", 20, "O");
            oDatos.Parametro("pn_DGracia", "N", 4, "O");
            oDatos.Parametro("pn_EmpleadoTipo_cod", "N", 6, "O");
            oDatos.Parametro("pv_EmpleadoTipo_des", "V", 120, "O");
            oDatos.Parametro("pv_tipo_hora", "V", 120, "O");
            oDatos.Parametro("pn_max_hextras", "N", 2, "O");
            oDatos.Parametro("pn_hextras", "F", 6, "O");
            oDatos.Parametro("pn_error", "N", 1, "O");
            oDatos.Parametro("pv_msg_error", "V", 220, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pn_error").ToString() == "1")
                    {
                        this.horaEntradaMarcada = oDatos.RetornarParametro("pv_MEntrada").ToString();
                        this.horaEntradaHorario = oDatos.RetornarParametro("pv_HEntrada").ToString();
                        this.horaSalidaMarcada = oDatos.RetornarParametro("pv_MSalida").ToString();
                        this.horaSalidaHorario = oDatos.RetornarParametro("pv_HSalida").ToString();
                        this.dGracia = int.Parse(oDatos.RetornarParametro("pn_DGracia").ToString());
                        this.tipoEmpleadoCod = int.Parse(oDatos.RetornarParametro("pn_EmpleadoTipo_cod").ToString());
                        this.tipoEmpleadoDesc = oDatos.RetornarParametro("pv_EmpleadoTipo_des").ToString();
                        this.tipoHora = oDatos.RetornarParametro("pv_tipo_hora").ToString();
                        this.maxHorasExtras = int.Parse(oDatos.RetornarParametro("pn_max_hextras").ToString());
                        this.numHorasExtras = double.Parse(oDatos.RetornarParametro("pn_hextras").ToString());
                        retValue = true;
                    }
                    else
                        this.trxError = oDatos.RetornarParametro("pv_msg_error").ToString();
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

            return retValue;
        }


        public bool LoadHorariosEspeciales(DateTime fecha)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Biometrico Marcaciones.cs LoadHorariosMarcaciones");

            oDatos.Paquete("rhk_trx_biometricos.rhp_consulta_horarios2");
            oDatos.Parametro("pn_codigo_emp", int.Parse(this.codEmpleado));
            oDatos.Parametro("pv_Fecha", fecha.ToString("dd/MM/yyyy"));
            oDatos.Parametro("pv_HEntrada", "V", 20, "O");
            oDatos.Parametro("pv_HSalida", "V", 20, "O");
            oDatos.Parametro("pn_DGracia", "N", 4, "O");
            oDatos.Parametro("pn_EmpleadoTipo_cod", "N", 6, "O");
            oDatos.Parametro("pv_EmpleadoTipo_des", "V", 120, "O");
            oDatos.Parametro("pv_tipo_hora", "V", 120, "O");
            oDatos.Parametro("pn_max_hextras", "N", 2, "O");
            oDatos.Parametro("pn_error", "N", 1, "O");
            oDatos.Parametro("pv_msg_error", "V", 220, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pn_error").ToString() == "1")
                    {
                        this.horaEntradaHorario = oDatos.RetornarParametro("pv_HEntrada").ToString();
                        this.horaSalidaHorario = oDatos.RetornarParametro("pv_HSalida").ToString();
                        this.dGracia = int.Parse(oDatos.RetornarParametro("pn_DGracia").ToString());
                        this.tipoEmpleadoCod = int.Parse(oDatos.RetornarParametro("pn_EmpleadoTipo_cod").ToString());
                        this.tipoEmpleadoDesc = oDatos.RetornarParametro("pv_EmpleadoTipo_des").ToString();
                        this.tipoHora = oDatos.RetornarParametro("pv_tipo_hora").ToString();
                        this.maxHorasExtras = int.Parse(oDatos.RetornarParametro("pn_max_hextras").ToString());
                        retValue = true;
                    }
                    else
                        this.trxError = oDatos.RetornarParametro("pv_msg_error").ToString();
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

            return retValue;
        }


        public DataTable GetMarcaciones(string fechainicio, string fechaFin)
        {
            this.trxError = string.Empty;
            DataTable dtMarcaciones = new DataTable("marcaciones");
            dtMarcaciones.Columns.Add("Fecha");
            dtMarcaciones.Columns.Add("Hora");
            dtMarcaciones.Columns.Add("Origen");
            dtMarcaciones.Columns.Add("Tipo");
            dtMarcaciones.Columns.Add("Respuesta");

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Biometrico Marcaciones.cs LoadHorariosMarcaciones");

            oDatos.Paquete("rhk_trx_biometricos.RHP_MARCACION_MENSUAL");
            oDatos.Parametro("pn_empleado", int.Parse(this.codEmpleado));
            oDatos.Parametro("pv_fecha_ing", fechainicio);
            oDatos.Parametro("pv_anio", fechaFin);
            oDatos.Parametro("c_marcaciones", "R", 0, "O");
            oDatos.Parametro("pn_error", "N", 1, "O");
            oDatos.Parametro("pv_mensaje", "V", 220, "O");
            
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pn_error").ToString() == "1")
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow dr = dtMarcaciones.NewRow();
                            for (int i = 0; i < dtMarcaciones.Columns.Count; i++)
                                dr[i] = oDatos.oDataReader[i].ToString();
                            dtMarcaciones.Rows.Add(dr);
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

            return dtMarcaciones;
        }

        public double HorasExtras
        {
            get
            {
                return this.numHorasExtras;
            }
        }

        public string Error
        {
            get
            {
                return this.trxError;
            }
        }

        public string HoraEntrada
        {
            get
            {
                return this.horaEntradaMarcada;
            }
        }

        public string HoraSalida
        {
            get
            {
                return this.horaSalidaMarcada;
            }
        }

        public string HorarioEntrada
        {
            get
            {
                return this.horaEntradaHorario;
            }
        }

        public string HorarioSalida
        {
            get
            {
                return this.horaSalidaHorario;
            }
        }

        public int DiasGracia
        {
            get
            {
                return this.dGracia;
            }
        }

        public int TipoEmpleadoCod
        {
            get
            {
                return this.tipoEmpleadoCod;
            }
        }

        public string TipoEmpleadoDesc
        {
            get
            {
                return this.tipoEmpleadoDesc;
            }
        }

        public string TipoHoraExtra
        {
            get
            {
                return this.tipoHora;
            }
        }

        public int MaximoHorasExtras
        {
            get
            {
                return this.maxHorasExtras;
            }
        }

    }
}
