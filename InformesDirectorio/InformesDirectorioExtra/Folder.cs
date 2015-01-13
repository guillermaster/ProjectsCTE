using System;
using System.Collections.Generic;
using System.Text;

namespace InformesDirectorioExtra
{
    public class Folder
    {
        private string _dbUser;
        private string _dbPassword;
        private string _dbServer;
        private string _appUser;
        private string _error;

        public Folder(string appUser, string dbUser, string dbPassword, string dbServer)
        {
            _dbUser = dbUser;
            _dbPassword = dbPassword;
            _dbServer = dbServer;
            _appUser = appUser;
        }

        public bool CreateNewFolder(string nombre, int idPadre, string codInstitucion)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "InformesDirectorioExtra.Folder.cs CreateNewFolder");

            oDatos.Paquete("clk_documentos_directorio.clp_crea_carpeta");
            oDatos.Parametro("pv_nombre_carpeta", nombre);
            oDatos.Parametro("pv_usuario", _appUser);
            oDatos.Parametro("pn_carpeta_padre", idPadre);
            oDatos.Parametro("pv_institucion", codInstitucion);
            oDatos.Parametro("pv_error", "V", 300, "O");

            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (string.IsNullOrWhiteSpace(error))
                        retValue = true;
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

        public string Error
        {
            get { return _error; }
        }
    }
}
