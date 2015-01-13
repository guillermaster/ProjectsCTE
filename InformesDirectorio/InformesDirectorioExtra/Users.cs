using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace InformesDirectorioExtra
{
    public class Users
    {
        private string _dbUser;
        private string _dbPassword;
        private string _dbServer;
        private string _error;
        private Aspirante _oAspirante;

        public Users(string dbUser, string dbPassword, string dbServer)
        {
            _dbUser = dbUser;
            _dbPassword = dbPassword;
            _dbServer = dbServer;
        }

        public DataTable AppUsers(string codInstitucion)
        {
            DataTable dtUsers = new DataTable("Users");
            dtUsers.Columns.Add("Usuario");
            dtUsers.Columns.Add("Email");
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "InformesDirectorioExtra.Users.cs AppUsers");

            switch (codInstitucion)
            {
                case InformesDirectorioExtra.Parametros.InstitucionesPermitidas.ComisionTransitoEcuador.Codigo:
                    oDatos.Paquete("clk_documentos_directorio.clp_retorna_usuarios");
                    break;
                case InformesDirectorioExtra.Parametros.InstitucionesPermitidas.AgenciaNacionalTransito.Codigo:
                    oDatos.Paquete("clk_documentos_directorio.clp_retorna_usuarios_ant");
                    break;
            }

            oDatos.Parametro("c_usuarios", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");

            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(error))
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            DataRow drUser = dtUsers.NewRow();
                            drUser[0] = oDatos.oDataReader[0];
                            drUser[1] = oDatos.oDataReader[1];
                            dtUsers.Rows.Add(drUser);
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
            /*DataRow dr = dtUsers.NewRow();
            dr[0] = "VRUALES";
            dr[1] = "vruales@cte.gob.ec";
            dtUsers.Rows.Add(dr);*/
            return dtUsers;
        }

        

        public class Aspirante
        {
            private string _nombres, _apellidos, _identificacion, _email, _fechaNac, _sexo, _idoneo, _estadocivil, _cargasfamiliares,
                _codPaisNac, _codProvinciaNac, _codCiudadNac, _fechaRegistro, _estatura, _tiposangre, _peso, _tallacalzado,
                _tallapantalon, _tallacamisa, _tallagorra;

            public Aspirante()
            {
            }
        }

    }
}
