using System;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;

namespace EscuelasDeConduccion
{
    public class Escuela
    {
        string _dbUser, _dbPassword, _dbServer;
        string _codEscuela, _codProvincia;
        string _error;


        public Escuela(string dbUser, string dbPassword, string dbServer, 
                       string codEscuela, string codProvincia)
        {
            _dbUser = dbUser;
            _dbPassword = dbPassword;
            _dbServer = dbServer;
            _codEscuela = codEscuela;
            _codProvincia = codProvincia;
        }

        public bool PublicarArchivo(byte[] fileToUpload)
        {
            bool retValue = false;
            ROracle oDatos = new ROracle(_dbUser, _dbPassword, _dbServer, "EscuelasDeConduccion.Escuela.cs PublicarArchivo()");
            oDatos.Paquete("");//poner aquí NOMBREPAQUETE.NOMBREPROCEDIMIENTO
            oDatos.Parametro("pv_cod_escuela", _codEscuela);
            oDatos.Parametro("pv_cod_provincia", _codProvincia);
            oDatos.Parametro("pb_archivo", fileToUpload);
            oDatos.Parametro("pv_error", "V", 2, "O");
            oDatos.Parametro("pv_mensaje", "V", 200, "O");

            _error = string.Empty;

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    retValue = true;
                }
                else
                    _error = oDatos.Mensaje;
            }
            catch(Exception ex)
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
