using System;
using System.Collections;
using System.Text;

namespace TTDuran
{
    public class TerminalDuran
    {
        private string _dbUser;
        private string _dbPassword;
        private string _dbServer;
        private string _error;

        public string Error
        {
            get { return _error; }
        }

        public TerminalDuran(string dbUser, string dbPassword, string dbServer)
        {
            _dbUser = dbUser;
            _dbPassword = dbPassword;
            _dbServer = dbServer;
            _error = string.Empty;
        }


        public bool InsertNameProposal(string nombrePropuesto, string motivo, string usuarioCedula, string usuarioNombre, string telefono, string email)
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "TerminalDuran.cs InsertNameProposal()");

            oDatos.Paquete("clk_tt_propuestas.clp_inserta_propuestas");
            oDatos.Parametro("pv_nombre_propuesta", nombrePropuesto);
            oDatos.Parametro("pv_motivo", motivo);
            oDatos.Parametro("pv_identificacion", usuarioCedula);
            oDatos.Parametro("pv_nombre_usuario", usuarioNombre);
            oDatos.Parametro("pv_email", email);
            oDatos.Parametro("pv_telefono", telefono);
            oDatos.Parametro("pv_error", "V", 3, "O");
            oDatos.Parametro("pv_mensaje", "V", 300, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    if (error != "S")
                        retValue = true;
                    else
                        _error = oDatos.RetornarParametro("pv_mensaje").ToString();
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


        public string[] GetNombresPropuestos()
        {
            ArrayList listNombres = new ArrayList();
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(_dbUser, _dbPassword, _dbServer, "TerminalDuran.cs GetNombresPropuestos()");
            oDatos.Paquete("clk_tt_propuestas.retorna_nombres");
            oDatos.Parametro("c_nombres", "R", 0, "O");

            try
            {
                _error = string.Empty;
                if (oDatos.Ejecutar("R"))
                {
                    while (oDatos.oDataReader.Read())
                    {
                        listNombres.Add(oDatos.oDataReader[0]);
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

            return ((string [])listNombres.ToArray(typeof(string)));
        }

    }
}
