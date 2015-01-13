using System;

namespace Seguridad
{
    public class UsuarioVerificador
    {
        private string db_user;
        private string db_password;
        private string db_name;

        private string user;
        private string _password;
        private string _error;
        private bool _loggedIn;

   

        public UsuarioVerificador(string sUsuario, string sClave, string sBaseDatos, string userw, string passwordw)
        {
            db_user = sUsuario;
            db_password = sClave;
            db_name = sBaseDatos;
            user = userw;
            _password = passwordw;
            _loggedIn = false;
        }

        public UsuarioVerificador(string sUsuario, string sClave, string sBaseDatos, string userw)
        {
            db_user = sUsuario;
            db_password = sClave;
            db_name = sBaseDatos;
            user = userw;
            _loggedIn = false;
        }



        public void LogIn()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name, "Seguridad.UsuarioVerificador.cs LogIn()");

            oDatos.Paquete(Constantes.StoredProcedures.LoginUsuarioVerificador);
            oDatos.Parametro("pv_usuario", user);
            oDatos.Parametro("pv_contraseña", "V", 100, "O");
            oDatos.Parametro("pv_error", "V", 500, "O");
            string error;

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string passwordFromDb;
                    passwordFromDb = oDatos.RetornarParametro("pv_contraseña").ToString();
                    error = oDatos.RetornarParametro("pv_error").ToString();
                    //ERROR 1 -> El usuario no existe
                    //ERROR 2 -> Usuario inactivo
                    //ERROR 3 -> Debe de cambiar contraseña
                    //ERROR S -> NO HAY ERROR
                    if (error == "S" || error == "3")
                    {

                        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
                        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                        passwordFromDb = objCrypto.DescifrarCadena(passwordFromDb);
                        if (_password == passwordFromDb)
                        {
                            _loggedIn = true;
                            if (error == "3")
                                _error = Constantes.UsuarioWeb.ErrorMsgUserMustChangePassword;
                        }
                        else
                        {
                            _loggedIn = false;
                            _error = Constantes.UsuarioWeb.ErrorMsgWrongPassword;
                        }
                    }
                    else
                    {
                        _loggedIn = false;
                        _error = error;
                        switch (error)
                        {
                            case "1":
                                _error = Constantes.UsuarioWeb.ErrorMsgUserDoesntExists;
                                break;
                            case "2":
                                _error = Constantes.UsuarioWeb.ErrorMsgUserInactive;
                                break;
                        }
                    }
                }
                else
                {
                    _password = null;
                    _error = "Error: No se pudo conectar a la Base de Datos.<br /><br />" + oDatos.Mensaje;
                    _loggedIn = false;
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
        }


        public bool ChangePassword(string newPassword)
        {
            bool result = false;
            CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
            objCrypto.Key = Constantes.ParametrosCifradoCs.key;
            objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(db_user, db_password, db_name, "Seguridad.UsuarioVerificador.cs ChangePassword()");
            oDatos.Paquete(Constantes.StoredProcedures.WebUserChangePassword);
            oDatos.Parametro("pv_usuario", user);
            oDatos.Parametro("pv_contraseña", objCrypto.CifrarCadena(newPassword));
            oDatos.Parametro("pv_error", "V", 500, "O");

            if (oDatos.Ejecutar("N"))
            {
                if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                {
                    result = true;
                }
                else
                {
                    _error = oDatos.RetornarParametro("pv_error").ToString();
                }
            }
            else
            {
                _error = oDatos.Mensaje;
            }

            oDatos.Dispose();

            return result;
        }


        public bool isLoggedIn
        {
            get
            {
                return _loggedIn;
            }
        }

       
        public string Username
        {
            get
            {
                return user;
            }
        }

        public string Error
        {
            get
            {
                return _error;
            }
        }

    }
}
