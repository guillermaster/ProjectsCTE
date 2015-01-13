using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EFOTclass
{
    public class Tallas
    {
        private string _dbUser;
        private string _dbPassword;
        private string _dbServer;
        private string _appUser;
        private string _error;

        public Tallas(string dbUser, string dbPassword, string dbServer)
        {
            _dbUser = dbUser;
            _dbPassword = dbPassword;
            _dbServer = dbServer;
            _error = string.Empty;
        }

        
        public DataTable TallasCalzado()
        {
            DataTable dtTallas = new DataTable();
            dtTallas.Columns.Add("Talla");
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.Tallas.cs TallasCalzado()");

            oDatos.Paquete("clk_aspirantes_efot.clp_talla_calzado");
            oDatos.Parametro("c_tallas", "R", 0, "O");

            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtTallas.NewRow();
                        dr[0] = oDatos.oDataReader[0];
                        dtTallas.Rows.Add(dr);
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
            return dtTallas;
        }


        public DataTable TallasCamisa()
        {
            DataTable dtTallas = new DataTable();
            dtTallas.Columns.Add("Talla");
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.Tallas.cs TallasCamisa()");

            oDatos.Paquete("clk_aspirantes_efot.clp_talla_camisa");
            oDatos.Parametro("c_tallas", "R", 0, "O");

            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtTallas.NewRow();
                        dr[0] = oDatos.oDataReader[0];
                        dtTallas.Rows.Add(dr);
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
            return dtTallas;
        }

        public DataTable TallasPantalon()
        {
            DataTable dtTallas = new DataTable();
            dtTallas.Columns.Add("Talla");
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.Tallas.cs TallasCalzado()");

            oDatos.Paquete("clk_aspirantes_efot.clp_talla_pantalon");
            oDatos.Parametro("c_tallas", "R", 0, "O");

            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtTallas.NewRow();
                        dr[0] = oDatos.oDataReader[0];
                        dtTallas.Rows.Add(dr);
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
            return dtTallas;
        }


        public DataTable TallasGorra()
        {
            DataTable dtTallas = new DataTable();
            dtTallas.Columns.Add("Talla");
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.Tallas.cs TallasCalzado()");

            oDatos.Paquete("clk_aspirantes_efot.clp_talla_gorra");
            oDatos.Parametro("c_tallas", "R", 0, "O");

            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtTallas.NewRow();
                        dr[0] = oDatos.oDataReader[0];
                        dtTallas.Rows.Add(dr);
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
            return dtTallas;
        }
    }
}
