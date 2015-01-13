using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EFOTclass
{
    public class Parametros
    {
        private string _dbUser;
        private string _dbPassword;
        private string _dbServer;
        private string _error;

        public const string CodTipoUbicacionDomicilio = "DOM";
        
        public Parametros(string dbUser, string dbPassword, string dbServer)
        {
            _dbUser = dbUser;
            _dbPassword = dbPassword;
            _dbServer = dbServer;
            _error = string.Empty;
        }

               

        public DataTable TiposEducacion()
        {
            DataTable dtTiposEducac = new DataTable();
            dtTiposEducac.Columns.Add("id");
            dtTiposEducac.Columns.Add("descripcion");
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.Parametros.cs TiposEducacion()");

            oDatos.Paquete("clk_aspirantes_efot.clp_retorna_tipos_educacion");
            oDatos.Parametro("c_tipos_educacion", "R", 0, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtTiposEducac.NewRow();
                        dr[0] = oDatos.oDataReader[0].ToString();
                        dr[1] = oDatos.oDataReader[1].ToString();
                        dtTiposEducac.Rows.Add(dr);
                    }
                }
                else
                    _error = oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }
            return dtTiposEducac;
        }

        public DataTable TiposReferencia()
        {
            DataTable dtTiposRef = new DataTable();
            dtTiposRef.Columns.Add("id");
            dtTiposRef.Columns.Add("descripcion");
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.Parametros.cs TiposReferencia()");

            oDatos.Paquete("clk_aspirantes_efot.clp_retorna_tipos_referencias");
            oDatos.Parametro("c_tipos_referencias", "R", 0, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtTiposRef.NewRow();
                        dr[0] = oDatos.oDataReader[0].ToString();
                        dr[1] = oDatos.oDataReader[1].ToString();
                        dtTiposRef.Rows.Add(dr);
                    }
                }
                else
                    _error = oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }
            return dtTiposRef;
        }

        public DataTable TiposUbicacion()
        {
            DataTable dtTiposUbicac = new DataTable();
            dtTiposUbicac.Columns.Add("id");
            dtTiposUbicac.Columns.Add("descripcion");
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.Parametros.cs TiposUbicacion()");

            oDatos.Paquete("clk_aspirantes_efot.clp_retorna_tipos_ubicaciones");
            oDatos.Parametro("c_tipos_educacion", "R", 0, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtTiposUbicac.NewRow();
                        dr[0] = oDatos.oDataReader[0].ToString();
                        dr[1] = oDatos.oDataReader[1].ToString();
                        dtTiposUbicac.Rows.Add(dr);
                    }
                }
                else
                    _error = oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }
            return dtTiposUbicac;
        }

        public DataTable CriteriosBusquedaAspirantes()
        {
            DataTable dtCriterios = new DataTable();
            dtCriterios.Columns.Add("id");
            dtCriterios.Columns.Add("descripcion");
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.Parametros.cs CriteriosBusquedaAspirantes()");

            oDatos.Paquete("clk_aspirantes_efot.clp_criterios_busqueda");
            oDatos.Parametro("c_criterios_busqueda", "R", 0, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtCriterios.NewRow();
                        dr[0] = oDatos.oDataReader[0].ToString();
                        dr[1] = oDatos.oDataReader[1].ToString();
                        dtCriterios.Rows.Add(dr);
                    }
                }
                else
                    _error = oDatos.Mensaje;
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }
            return dtCriterios;
        }

        public class Actividades
        {
            private string _dbUser;
            private string _dbPassword;
            private string _dbServer;
            private string _error;
            
            public Actividades(string dbUser, string dbPassword, string dbServer)
            {
                _dbUser = dbUser;
                _dbPassword = dbPassword;
                _dbServer = dbServer;
                _error = string.Empty;
            }

            public DataTable GetActividades(string tipo)
            {
                DataTable dtActividades = new DataTable();
                dtActividades.Columns.Add("id");
                dtActividades.Columns.Add("descripcion");
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.Parametros.cs Actividades()");

                oDatos.Paquete("clk_aspirantes_efot.clp_consulta_actividades");
                oDatos.Parametro("pv_tipo_actividad", tipo);
                oDatos.Parametro("c_actividad", "R", 0, "O");
                oDatos.Parametro("pv_error", "V", 300, "O");

                try
                {
                    if (oDatos.Ejecutar("R"))
                    {
                        string error = oDatos.RetornarParametro("pv_error").ToString();
                        if (string.IsNullOrWhiteSpace(error))
                        {
                            while (oDatos.oDataReader.Read())
                            {
                                DataRow dr = dtActividades.NewRow();
                                dr[0] = oDatos.oDataReader[0].ToString();
                                dr[1] = oDatos.oDataReader[1].ToString();
                                dtActividades.Rows.Add(dr);
                            }
                        }
                        else
                            _error = error;
                    }
                    else
                        _error = oDatos.Mensaje;
                }
                catch (Exception ex)
                {
                    _error = ex.Message;
                }
                finally
                {
                    oDatos.Dispose();
                }
                return dtActividades;
            }

            public class Tipos
            {
                public static string Registro
                {
                    get { return "REG"; }
                }
                public static string Revisión
                {
                    get { return "REV"; }
                }
            }
        }

        public class Session
        {
            public static string CodigoAspirante
            {
                get { return "CODASP"; }
            }

            public class Roles
            {
                public static string Instructor
                {
                    get { return "REFOT"; }
                }

                public static string InstructorCalificador
                {
                    get { return "RAEFO"; }
                }

                public static string ReversaCalificacion
                {
                    get { return "RCEFO"; } 
                }
            }
        }

    }
}
