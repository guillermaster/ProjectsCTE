using System;
using System.Collections.Generic;
using System.Text;

namespace Seguridad
{
    public class UsuarioVerificador
    {
        private string db_user;
        private string db_password;
        private string db_name;

        private string user;
        private string password;
        private string error;
        private bool loggedIn;

   

        public UsuarioVerificador(string sUsuario, string sClave, string sBaseDatos, string userw, string passwordw)
        {
            this.db_user = sUsuario;
            this.db_password = sClave;
            this.db_name = sBaseDatos;
            this.user = userw;
            this.password = passwordw;
            this.loggedIn = false;
        }

        public UsuarioVerificador(string sUsuario, string sClave, string sBaseDatos, string userw)
        {
            this.db_user = sUsuario;
            this.db_password = sClave;
            this.db_name = sBaseDatos;
            this.user = userw;
            this.loggedIn = false;
        }



        public void LogIn()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name, "Seguridad.UsuarioVerificador.cs LogIn()");

            oDatos.Paquete(Constantes.StoredProcedures.LoginUsuarioVerificador);
            oDatos.Parametro("pv_usuario", this.user);
            oDatos.Parametro("pv_contraseña", "V", 100, "O");
            oDatos.Parametro("pv_error", "V", 500, "O");
            string error = "";

            if (oDatos.Ejecutar("R"))
            {
                string password_from_db = "";
                password_from_db = oDatos.RetornarParametro("pv_contraseña").ToString();
                error = oDatos.RetornarParametro("pv_error").ToString();
                //ERROR 1 -> El usuario no existe
                //ERROR 2 -> Usuario inactivo
                //ERROR 3 -> Debe de cambiar contraseña
                //ERROR S -> NO HAY ERROR
                if (error == "S" || error=="3")
                {

                    CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
                    objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                    objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                    password_from_db = objCrypto.DescifrarCadena(password_from_db);
                    if (this.password == password_from_db)
                    {
                        this.loggedIn = true;
                        if (error == "3")
                            this.error = Constantes.UsuarioWeb.ErrorMsgUserMustChangePassword;
                    }
                    else
                    {
                        this.loggedIn = false;
                        this.error = Constantes.UsuarioWeb.ErrorMsgWrongPassword;
                    }
                }
                else
                {
                    this.loggedIn = false;
                    this.error = error;
                    switch (error)
                    {
                        case "1":
                            this.error = Constantes.UsuarioWeb.ErrorMsgUserDoesntExists;
                            break;
                        case "2":
                            this.error = Constantes.UsuarioWeb.ErrorMsgUserInactive;
                            break;
                    }
                }
            }
            else
            {
                password = null;
                this.error = "Error: No se pudo conectar a la Base de Datos.<br /><br />" + oDatos.Mensaje;
                loggedIn = false;
            }

            oDatos.Dispose();
        }


        public bool ChangePassword(string new_password)
        {
            bool result = false;
            CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
            objCrypto.Key = Constantes.ParametrosCifradoCs.key;
            objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.db_user, this.db_password, this.db_name, "Seguridad.UsuarioVerificador.cs ChangePassword()");
            oDatos.Paquete(Constantes.StoredProcedures.WebUserChangePassword);
            oDatos.Parametro("pv_usuario", this.user);
            oDatos.Parametro("pv_contraseña", objCrypto.CifrarCadena(new_password));
            oDatos.Parametro("pv_error", "V", 500, "O");

            if (oDatos.Ejecutar("N"))
            {
                if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                {
                    result = true;
                }
                else
                {
                    error = oDatos.RetornarParametro("pv_error").ToString();
                }
            }
            else
            {
                error = oDatos.Mensaje;
            }

            oDatos.Dispose();

            return result;
        }


        public bool isLoggedIn
        {
            get
            {
                return this.loggedIn;
            }
        }

       
        public string Username
        {
            get
            {
                return this.user;
            }
        }

        public string Error
        {
            get
            {
                return this.error;
            }
        }

    }
}
