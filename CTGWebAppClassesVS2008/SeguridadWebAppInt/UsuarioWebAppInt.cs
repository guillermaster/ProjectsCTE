using System;
using System.Collections.Generic;
using System.Text;
using CifradoCs;
using System.Collections;

namespace SeguridadWebAppInt
{
    public class UsuarioWebAppInt
    {
        private string appUser;
        //private string appPassword;
        //private string description;
        private string trxMessage;
        private string trxError;
        private string dbUser;
        private string dbPassword;
        private string dbServer;
        private Crypto objCrypto;
        private bool activeUser;
        private ArrayList rolesUsuario;
        private ArrayList modulosUsuario;
        //public static readonly string CodigoRolUserGeneraCert = "CER";//españa
        //public static readonly string CodigoRolUserEnviaCert = "CEB";//brevetación
        //public static readonly string CodigoRolUserAuditoriaCert = "AUD";//brevetación

        public UsuarioWebAppInt(string appUser, string dbUser, string dbPassword, string dbServer)
        {
            this.dbUser = dbUser;
            this.dbPassword = dbPassword;
            this.dbServer = dbServer;
            this.appUser = appUser;
            rolesUsuario = new ArrayList();
        }

        public UsuarioWebAppInt(string appUser, Crypto objCrypto, string dbUser, string dbPassword, string dbServer)
        {
            this.dbUser = dbUser;
            this.dbPassword = dbPassword;
            this.dbServer = dbServer;
            this.appUser = appUser;
            this.objCrypto = objCrypto;
            rolesUsuario = new ArrayList();
        }


        public bool LogIn(string password)
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Seguridad.UsuarioCertificadoLicencia.cs");

            oDatos.Paquete("GCK_API_CONSULTA_WEB.GCP_CONSULTA_USER");
            oDatos.Parametro("pv_user", this.appUser);
            oDatos.Parametro("pv_clave", "V", 100, "O");
            oDatos.Parametro("pv_descripcion", "V", 80, "O");
            oDatos.Parametro("pv_error", "V", 300, "O");
            string error;

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string passwordFromDB = oDatos.RetornarParametro("pv_clave").ToString();
                    //description = oDatos.RetornarParametro("pv_descripcion").ToString();
                    error = oDatos.RetornarParametro("pv_error").ToString();
                    if (error == "S")
                    {                        
                        if (password == this.objCrypto.DescifrarCadena(passwordFromDB))
                        {
                            this.trxError = "";
                            this.trxMessage = "Bienvenido " + this.appUser;
                            this.activeUser = true;
                            return true;
                        }
                    }
                    else
                    {
                        switch (error)
                        {
                            case "1":
                                this.trxError = "El usuario no puede ser nulo";
                                break;
                            case "2":
                                this.trxError = "El usuario no existe";
                                break;
                            case "3":
                                if (password == this.objCrypto.DescifrarCadena(passwordFromDB))
                                {
                                    this.activeUser = false;
                                    return true;
                                }
                                else
                                    this.trxError = "Contraseña incorrecta";
                                break;
                            case "4":
                                this.trxError = "La contraseña no puede ser nula";
                                break;
                            default:
                                this.trxError = "Contraseña incorrecta";
                                break;
                        }
                    }
                }
                else
                {
                    //password = null;
                    //description = null;
                    this.trxError = "Error: " + oDatos.Mensaje;
                }
            }
            catch (Exception ex)
            {
                this.trxError = "Error: " + ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            /*if (error == "S")
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
            }*/
            return false;
        }


        public bool LoadRolesByUser()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Seguridad.UsuarioCertificadoLicencia.cs");

            oDatos.Paquete("gck_api_consulta_web.gcp_retorna_rol");
            oDatos.Parametro("pv_usuario", this.appUser);
            oDatos.Parametro("c_rol_user", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");
            string error;


            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    error = oDatos.RetornarParametro("pv_error").ToString();
                    while (oDatos.oDataReader.Read())
                    {
                        this.rolesUsuario.Add(oDatos.oDataReader.GetString(0));
                    }
                }
                else
                {
                    //password = null;
                    //description = null;
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
            this.trxError = error;


            if (this.rolesUsuario.Count > 0)
            {
                return true;
            }
            else
            {
                this.trxError = "Error: No tiene nigún rol asignado";
                return false;
            }
        }

        public bool LoadModulosByUser()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "Seguridad.UsuarioCertificadoLicencia.cs");
            this.modulosUsuario = new ArrayList();
            oDatos.Paquete("gck_api_consulta_web.gcp_valida_modulo");
            oDatos.Parametro("pv_usuario", this.appUser);
            oDatos.Parametro("c_modulos_user", "R", 0, "O");
            oDatos.Parametro("pv_error", "V", 200, "O");
            string error;


            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    error = oDatos.RetornarParametro("pv_error").ToString();
                    if (error == "S")
                    {
                        while (oDatos.oDataReader.Read())
                        {
                            this.modulosUsuario.Add(oDatos.oDataReader.GetString(0));
                        }
                    }
                }
                else
                {
                    //password = null;
                    //description = null;
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
            this.trxError = error;


            if (this.modulosUsuario.Count > 0)
            {
                return true;
            }
            else
            {
                this.trxError = "Error: No tiene nigún módulo asignado";
                return false;
            }
        }


        public bool ValidateAccessToModule(string modulo)
        {
            if (this.LoadModulosByUser())
            {
                for (int i = 0; i < this.modulosUsuario.Count; i++)
                {
                    if (this.modulosUsuario[i].ToString() == modulo)
                        return true;
                }
            }
            return false;
        }

        //actualizar password
        public bool ChangePassword(string current_password, string new_password)
        {
            bool retValue = false;
            Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
            objCrypto.Key = Constantes.ParametrosCifradoCs.key;
            objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.dbUser, this.dbPassword, this.dbServer, "UsuarioWebAppInt.UsuarioWebAppInt.cs ChangePassword()");

            oDatos.Paquete("GCK_API_CONSULTA_WEB.GCP_ACTUALIZA_PASSWORD");
            oDatos.Parametro("pv_user", this.appUser);
            oDatos.Parametro("pv_password_old", objCrypto.CifrarCadena(current_password));
            oDatos.Parametro("pv_password", objCrypto.CifrarCadena(new_password));
            oDatos.Parametro("pv_error", "V", 1000, "O");

            try
            {
                if (oDatos.Ejecutar("N"))//si no se retorno un error ORA
                {
                    string sError = oDatos.RetornarParametro("pv_Error").ToString();
                    if (sError == "S")
                    {
                        retValue = true;
                        this.trxMessage = "La contraseña ha sido modificada exitosamente";
                        this.trxError = "";
                    }
                    else
                    {
                        #region "Definir mensaje de error"
                        switch (sError)
                        {
                            case "1":
                                this.trxError = "El usuario no puede ser nulo";
                                break;
                            case "2":
                                this.trxError = "El password actual no puede ser nulo";
                                break;
                            case "3":
                                this.trxError = "El nuevo password no puede ser nulo";
                                break;
                            case "4":
                                this.trxError = "El usuario no existe o no está registrado";
                                break;
                            case "5":
                                this.trxError = "El password actual es incorrecto";
                                break;
                            case "6":
                                this.trxError = "El password nuevo es igual al actual";
                                break;
                            default:
                                this.trxError = "Error desconocido";
                                break;
                        }
                        //error 1 -> el usuario no puede ser nulo
                        //error 2 -> el password anterior no puede ser nulo
                        //error 3 -> el password nuevo no puede ser nulo
                        //error 4 -> el usuario no existe o no está registrado
                        //error 5 -> el password anterior no coincide con el registrado
                        //error 6 -> password nuevo es igual al anterior
                        #endregion
                    }
                }
                else// se retorno un error ORA
                {
                    this.trxError = oDatos.Mensaje;
                }
            }
            catch (Exception ex)
            {
                this.trxMessage = ex.Message;
            }
            finally
            {
                oDatos.Dispose();
            }

            return retValue;
        }


        #region "Propiedades"
        public string Username
        {
            get
            {
                return this.appUser;
            }
        }

        public ArrayList Roles
        {
            get
            {
                return this.rolesUsuario;
            }
        }

        public ArrayList Modulos
        {
            get
            {
                return this.modulosUsuario;
            }
        }
        
        public string Error
        {
            get
            {
                return this.trxError;
            }
        }

        public string Mensaje
        {
            get
            {
                return this.trxMessage;
            }
        }

        public bool UsuarioActivo
        {
            get
            {
                return this.activeUser;
            }
        }
        /*public object[] RolesUsuario
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
        }*/
        #endregion
    }
}
