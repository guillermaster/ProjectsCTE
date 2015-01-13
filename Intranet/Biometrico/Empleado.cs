using System;
using System.Collections.Generic;
using System.Text;

namespace Biometrico
{
    public class Empleado
    {
        private string dbUser;
        private string dbPassword;
        private string dbServer;

        private int codEmpleado;
        private string nombreEmpleado, descDirArea, descDpto, nombreJefe, nombreDirector;
        private string userSpJefe, userSpDirector;
        private int codDirArea, codDpto, codJefe, codDirector;

        //private int tipoEmpleadoCod;//
        //private string tipoEmpleadoDesc;//
        //private string tipoHora;//
        //private int maxHorasExtras;//
        
        private string trxError;

        public Empleado(int codEmpleado, string dbUser, string dbPassword, string dbServer)
        {
            this.codEmpleado = codEmpleado;
            this.dbUser = dbUser;
            this.dbPassword = dbPassword;
            this.dbServer = dbServer;
        }

        public bool LoadEmpleadoInfo(DateTime fecha)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Biometrico Marcaciones.cs LoadHorariosMarcaciones");

            oDatos.Paquete("rhk_trx_biometricos.RHP_DATOS_EMPLEADOS");
            oDatos.Parametro("pn_codigo_emp", this.codEmpleado);
            oDatos.Parametro("pv_Fecha", fecha.ToString("dd/MM/yyyy"));
            oDatos.Parametro("pv_nombres", "V", 180, "O");
            oDatos.Parametro("pn_PadreDpto_cod", "N", 5, "O");
            oDatos.Parametro("pv_PadreDpto_des", "V", 120, "O");
            oDatos.Parametro("pn_HijoDpto_cod", "N", 5, "O");
            oDatos.Parametro("pn_HijoDpto_des", "V", 120, "O");
            oDatos.Parametro("pn_JefeCodigo", "N", 5, "O");
            oDatos.Parametro("pv_JefeDes", "V", 180, "O");
            oDatos.Parametro("pv_JefeWeb", "V", 180, "O");
            oDatos.Parametro("pn_DirectorCodigo", "N", 5, "O");
            oDatos.Parametro("pv_DirectorDes", "V", 180, "O");
            oDatos.Parametro("pv_DirectorWeb", "V", 180, "O");
            //oDatos.Parametro("pn_EmpleadoTipo_cod", "N", 6, "O");//
            //oDatos.Parametro("pv_EmpleadoTipo_des", "V", 120, "O");//
            //oDatos.Parametro("pv_tipo_hora", "V", 120, "O");//
            //oDatos.Parametro("pn_max_hextras", "N", 2, "O");//
            oDatos.Parametro("pn_error", "N", 1, "O");
            oDatos.Parametro("pv_msg_error", "V", 220, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    if (oDatos.RetornarParametro("pn_error").ToString() == "1")
                    {
                        this.nombreEmpleado = oDatos.RetornarParametro("pv_nombres").ToString();
                        this.codDpto = int.Parse(oDatos.RetornarParametro("pn_HijoDpto_cod").ToString());
                        this.descDpto = oDatos.RetornarParametro("pn_HijoDpto_des").ToString();
                        this.codDirArea = int.Parse(oDatos.RetornarParametro("pn_PadreDpto_cod").ToString());
                        this.descDirArea = oDatos.RetornarParametro("pv_PadreDpto_des").ToString();
                        this.codJefe = int.Parse(oDatos.RetornarParametro("pn_JefeCodigo").ToString());
                        this.nombreJefe = oDatos.RetornarParametro("pv_JefeDes").ToString();
                        this.userSpJefe = oDatos.RetornarParametro("pv_JefeWeb").ToString();
                        this.codDirector = int.Parse(oDatos.RetornarParametro("pn_DirectorCodigo").ToString());
                        this.nombreDirector = oDatos.RetornarParametro("pv_DirectorDes").ToString();
                        this.userSpDirector = oDatos.RetornarParametro("pv_DirectorWeb").ToString();


                        //this.tipoEmpleadoCod = int.Parse(oDatos.RetornarParametro("pn_EmpleadoTipo_cod").ToString());
                        //this.tipoEmpleadoDesc = oDatos.RetornarParametro("pn_EmpleadoTipo_des").ToString();
                        //this.tipoHora = oDatos.RetornarParametro("pv_tipo_hora").ToString();
                        //this.maxHorasExtras = int.Parse(oDatos.RetornarParametro("pn_max_hextras").ToString());

                        
                        this.trxError = "";
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

        public string Nombre
        {
            get
            {
                return this.nombreEmpleado;
            }
        }

        public string Direcccion
        {
            get
            {
                return this.descDirArea;
            }
        }

        public string Departamento
        {
            get
            {
                return this.descDpto;
            }
        }

        public string JefeInmediato
        {
            get
            {
                return this.nombreJefe;
            }
        }

        public string JefeDirector
        {
            get
            {
                return this.nombreDirector;
            }
        }

        
        public int CodDireccion
        {
            get
            {
                return this.codDirArea;
            }
        }

        public int CodDepartamento
        {
            get
            {
                return this.codDpto;
            }
        }

        public int CodJefeInmediato
        {
            get
            {
                return this.codJefe;
            }
        }

        public int CodJefeDirector
        {
            get
            {
                return this.codDirector;
            }
        }

        public string UserSharepointJefe
        {
            get
            {
                return this.userSpJefe;
            }
        }

        public string UserSharepointDirector
        {
            get
            {
                return this.userSpDirector;
            }
        }


        //public int TipoEmpleadoCod
        //{
        //    get
        //    {
        //        return this.tipoEmpleadoCod;
        //    }
        //}

        //public string TipoEmpleadoDesc
        //{
        //    get
        //    {
        //        return this.tipoEmpleadoDesc;
        //    }
        //}

        //public string TipoHoraExtra
        //{
        //    get
        //    {
        //        return this.tipoHora;
        //    }
        //}

        //public int MaximoHorasExtras
        //{
        //    get
        //    {
        //        return this.maxHorasExtras;
        //    }
        //}
    }
}
