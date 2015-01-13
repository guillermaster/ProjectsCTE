using System;
using System.Collections;


namespace Seguridad
{
    public class UsuarioCertificadoLicencia
    {
        private string _username;
        private string _password;
        private string _description;
        private string _error;
        private string dbUser;
        private string dbPassword;
        private string dbServer;
        private ArrayList rolesUsuario;
        public static readonly string CodigoRolUserGeneraCert = "CER";//españa
        public static readonly string CodigoRolUserEnviaCert = "CEB";//brevetación
        public static readonly string CodigoRolUserAuditoriaCert = "AUD";//brevetación

        public UsuarioCertificadoLicencia(string sUsuario, string sClave, string sBaseDatos, string username)
        {
            dbUser = sUsuario;
            dbPassword = sClave;
            dbServer = sBaseDatos;
            _username = username;
            rolesUsuario = new ArrayList();
        }


        public bool LogIn()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(dbUser, dbPassword, dbServer, "Seguridad.UsuarioCertificadoLicencia.cs");

            oDatos.Paquete("GCK_API_CONSULTA_WEB.GCP_CONSULTA_USER");
            oDatos.Parametro("pv_user", _username);
            oDatos.Parametro("pv_clave", "V", 100, "O");
            oDatos.Parametro("pv_descripcion", "V", 80, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");
            string error;

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    _password = oDatos.RetornarParametro("pv_clave").ToString();
                    _description = oDatos.RetornarParametro("pv_descripcion").ToString();
                    error = oDatos.RetornarParametro("pv_error").ToString();
                    //ERROR 1 ->  El usuario no puede ser nulo
                    //ERROR 2 -> El usuario no existe o no está registrado
                    //ERROR 3 -> Debe modificar su clave, es la primera vez que entra al sistema
                    //ERROR 4 -> el password no puede ser nulo
                    //ERROR S -> NO HAY ERROR
                }
                else
                {
                    _password = null;
                    _description = null;
                    error = "Error: No se pudo conectar a la Base de Datos";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            if (error == "S")
            {
                if (LoadRolesByUser())
                {
                    _error = error;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                _error = error;
                return false;
            }

        }

        public bool LogInSinRoles()
        {
            bool returnValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(dbUser, dbPassword, dbServer, "Seguridad.UsuarioCertificadoLicencia.cs");

            oDatos.Paquete("GCK_API_CONSULTA_WEB.GCP_CONSULTA_USER");
            oDatos.Parametro("pv_user", _username);
            oDatos.Parametro("pv_clave", "V", 100, "O");
            oDatos.Parametro("pv_descripcion", "V", 80, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");
            string error;

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    _password = oDatos.RetornarParametro("pv_clave").ToString();
                    _description = oDatos.RetornarParametro("pv_descripcion").ToString();
                    error = oDatos.RetornarParametro("pv_error").ToString();
                    //ERROR 1 ->  El usuario no puede ser nulo
                    //ERROR 2 -> El usuario no existe o no está registrado
                    //ERROR 3 -> Debe modificar su clave, es la primera vez que entra al sistema
                    //ERROR 4 -> el password no puede ser nulo
                    //ERROR S -> NO HAY ERROR
                    if (error == "S" || error == "")
                        returnValue = true;
                    _error = error;
                }
                else
                {
                    _password = null;
                    _description = null;
                    _error = "Error: No se pudo conectar a la Base de Datos";
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }
            return returnValue;
           
        }

        public bool LoadRolesByUser()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(dbUser, dbPassword, dbServer, "Seguridad.UsuarioCertificadoLicencia.cs");

            oDatos.Paquete("gck_api_consulta_web.gcp_valida_modulo");
            oDatos.Parametro("pv_usuario", _username);
            oDatos.Parametro("c_modulos_user", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");
            string error;


            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    error = oDatos.RetornarParametro("pv_error").ToString();
                    while (oDatos.oDataReader.Read())
                    {
                        //if (oDatos.oDataReader.GetString(0) == CodigoRolUserEnviaCert || oDatos.oDataReader.GetString(0) == CodigoRolUserGeneraCert || oDatos.oDataReader.GetString(0) == CodigoRolUserAuditoriaCert)
                        //    rol = oDatos.oDataReader.GetString(0);
                        rolesUsuario.Add(oDatos.oDataReader.GetString(0));
                    }
                }
                else
                {
                    _password = null;
                    _description = null;
                    error = "Error: No se pudo conectar a la Base de Datos";
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }
            _error = error;
            

            if (rolesUsuario.Count>0)
            {                
                return true;
            }
            else
            {
                _error = "Error: No tiene nigún rol asignado";
                return false;
            }
        }

        //actualizar password
        public string ChangePassword(string currentPassword, string newPassword)
        {
            string errorMessage="";
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(dbUser, dbPassword, dbServer, "Seguridad.UsuarioCertificadoLicencia.cs");

            oDatos.Paquete("GCK_API_CONSULTA_WEB.GCP_ACTUALIZA_PASSWORD");
            oDatos.Parametro("pv_user", _username);
            oDatos.Parametro("pv_password_old", currentPassword);
            oDatos.Parametro("pv_password", newPassword);
            oDatos.Parametro("pv_error", "V", 1000, "O");

            if (oDatos.Ejecutar("N"))//si no se retorno un error ORA
            {
                string sError = oDatos.RetornarParametro("pv_Error").ToString();
                if (sError == "S")
                {
                    //oDatos.Transaccion.Commit();
                    errorMessage = null;
                }
                else
                {
                    //oDatos.Transaccion.Rollback();
                    switch (sError)
                    {
                        case "1":
                            errorMessage = "El usuario no puede ser nulo";
                            break;
                        case "2":
                            errorMessage = "El password actual no puede ser nulo";
                            break;
                        case "3":
                            errorMessage = "El nuevo password no puede ser nulo";
                            break;
                        case "4":
                            errorMessage = "El usuario no existe o no está registrado";
                            break;
                        case "5":
                            errorMessage = "El password actual es incorrecto";
                            break;
                        case "6":
                            errorMessage = "El password nuevo es igual al actual";
                            break;
                        default:
                            errorMessage = "Error desconocido";
                            break;
                    }
                    //error 1 -> el usuario no puede ser nulo
                    //error 2 -> el password anterior no puede ser nulo
                    //error 3 -> el password nuevo no puede ser nulo
                    //error 4 -> el usuario no existe o no está registrado
                    //error 5 -> el password anterior no coincide con el registrado
                    //error 6 -> password nuevo es igual al anterior
                }
            }
            else// se retorno un error ORA
            {
                //oDatos.Transaccion.Rollback();
                //errorMessage = oDatos.RetornarMensajeString();
                errorMessage = "error!";
            }

            oDatos.Dispose();

            return errorMessage;
        }


        #region "Propiedades"
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        public string Error
        {
            get
            {
                return _error;
            }
        }
        public object[] RolesUsuario
        {
            get
            {
                return rolesUsuario.ToArray();
            }
        }
        public bool TieneRolesUsuario
        {
            get { return rolesUsuario.Count > 0; }
        }
        #endregion

        
    }
}
