using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;


namespace Seguridad
{
    public class UsuarioCertificadoLicencia
    {
        private string username;
        private string password;
        private string description;
        private string error;
        private string dbUser;
        private string dbPassword;
        private string dbServer;
        private ArrayList rolesUsuario;
        public static readonly string CodigoRolUserGeneraCert = "CER";//españa
        public static readonly string CodigoRolUserEnviaCert = "CEB";//brevetación
        public static readonly string CodigoRolUserAuditoriaCert = "AUD";//brevetación

        public UsuarioCertificadoLicencia(string sUsuario, string sClave, string sBaseDatos, string _username)
        {
            this.dbUser = sUsuario;
            this.dbPassword = sClave;
            this.dbServer = sBaseDatos;
            this.username = _username;
            rolesUsuario = new ArrayList();
        }


        public bool LogIn()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Seguridad.UsuarioCertificadoLicencia.cs");

            oDatos.Paquete("GCK_API_CONSULTA_WEB.GCP_CONSULTA_USER");
            oDatos.Parametro("pv_user", username);
            oDatos.Parametro("pv_clave", "V", 100, "O");
            oDatos.Parametro("pv_descripcion", "V", 80, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");
            string error;

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    password = oDatos.RetornarParametro("pv_clave").ToString();
                    description = oDatos.RetornarParametro("pv_descripcion").ToString();
                    error = oDatos.RetornarParametro("pv_error").ToString();
                    //ERROR 1 ->  El usuario no puede ser nulo
                    //ERROR 2 -> El usuario no existe o no está registrado
                    //ERROR 3 -> Debe modificar su clave, es la primera vez que entra al sistema
                    //ERROR 4 -> el password no puede ser nulo
                    //ERROR S -> NO HAY ERROR
                }
                else
                {
                    password = null;
                    description = null;
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
                    this.error = error;
                    return true;
                }
                else
                    return false;
            }
            else
            {
                this.error = error;
                return false;
            }

        }

        public bool LogInSinRoles()
        {
            bool returnValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Seguridad.UsuarioCertificadoLicencia.cs");

            oDatos.Paquete("GCK_API_CONSULTA_WEB.GCP_CONSULTA_USER");
            oDatos.Parametro("pv_user", username);
            oDatos.Parametro("pv_clave", "V", 100, "O");
            oDatos.Parametro("pv_descripcion", "V", 80, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");
            string error;

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    password = oDatos.RetornarParametro("pv_clave").ToString();
                    description = oDatos.RetornarParametro("pv_descripcion").ToString();
                    error = oDatos.RetornarParametro("pv_error").ToString();
                    //ERROR 1 ->  El usuario no puede ser nulo
                    //ERROR 2 -> El usuario no existe o no está registrado
                    //ERROR 3 -> Debe modificar su clave, es la primera vez que entra al sistema
                    //ERROR 4 -> el password no puede ser nulo
                    //ERROR S -> NO HAY ERROR
                    if (error == "S" || error == "")
                        returnValue = true;
                    this.error = error;
                }
                else
                {
                    password = null;
                    description = null;
                    this.error = "Error: No se pudo conectar a la Base de Datos";
                }
            }
            catch (Exception ex)
            {
                this.error = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }
            return returnValue;
           
        }

        public bool LoadRolesByUser()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Seguridad.UsuarioCertificadoLicencia.cs");

            oDatos.Paquete("gck_api_consulta_web.gcp_valida_modulo");
            oDatos.Parametro("pv_usuario", username);
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
                        this.rolesUsuario.Add(oDatos.oDataReader.GetString(0));
                    }
                }
                else
                {
                    password = null;
                    description = null;
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
            this.error = error;
            

            if (this.rolesUsuario.Count>0)
            {                
                return true;
            }
            else
            {
                this.error = "Error: No tiene nigún rol asignado";
                return false;
            }
        }

        //actualizar password
        public string ChangePassword(string current_password, string new_password)
        {
            string errorMessage="";
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Seguridad.UsuarioCertificadoLicencia.cs");

            oDatos.Paquete("GCK_API_CONSULTA_WEB.GCP_ACTUALIZA_PASSWORD");
            oDatos.Parametro("pv_user", username);
            oDatos.Parametro("pv_password_old", current_password);
            oDatos.Parametro("pv_password", new_password);
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
                return username;
            }
            set
            {
                username = value;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        public string Error
        {
            get
            {
                return error;
            }
        }
        public object[] RolesUsuario
        {
            get
            {
                return this.rolesUsuario.ToArray();
            }
        }
        public bool TieneRolesUsuario
        {
            get
            {
                if (this.rolesUsuario.Count > 0)
                    return true;
                else
                    return false;
            }
        }
        #endregion

        
    }
}
