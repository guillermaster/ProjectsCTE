using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EFOTclass
{
    public class User
    {
        private string _dbUser;
        private string _dbPassword;
        private string _dbServer;
        private string _error;

        public User(string dbUser, string dbPassword, string dbServer)
        {
            _dbUser = dbUser;
            _dbPassword = dbPassword;
            _dbServer = dbServer;
        }


        public string GetEncPwdFromDB(string numCedula)
        {
            string encPwd = string.Empty;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.User.GetEncPwdFromDB");

            oDatos.Paquete("clk_aspirantes_efot.clp_consulta_clave");
            oDatos.Parametro("pv_cedula", numCedula);
            oDatos.Parametro("pv_clave", "V", 100, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");
            
            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(error))
                        encPwd = oDatos.RetornarParametro("pv_clave").ToString();
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
            return encPwd;
        }

        public bool GetEncPwdAndEmailFromDB(string numCedula, out string encPwd, out string email)
        {
            bool retValue = false;
            encPwd = string.Empty;
            email = string.Empty;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.User.GetEncPwdAndEmailFromDB");

            oDatos.Paquete("clk_aspirantes_efot.clp_consulta_email");
            oDatos.Parametro("pv_cedula", numCedula);
            oDatos.Parametro("pv_clave", "V", 100, "O");
            oDatos.Parametro("pv_email", "V", 120, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");

            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(error))
                    {
                        encPwd = oDatos.RetornarParametro("pv_clave").ToString();
                        email = oDatos.RetornarParametro("pv_email").ToString();
                        retValue = true;
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
            return retValue;
        }

        public DataTable GetAllInstructoresUsers()
        {
            DataTable dtUsers = new DataTable();
            dtUsers.Columns.Add("Usuario");
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "EFOTclass.Tallas.cs TallasCalzado()");

            oDatos.Paquete("clk_aspirantes_efot.clp_retorna_usuarios");
            oDatos.Parametro("c_usuarios", "R", 0, "O");

            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        DataRow dr = dtUsers.NewRow();
                        dr[0] = oDatos.oDataReader[0];
                        dtUsers.Rows.Add(dr);
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
            return dtUsers;
        }

        public string Error
        {
            get { return _error; }
        }
    }
}
