using System;
using System.Text;
using CifradoCs;
using System.Net;
using System.Net.Sockets;

namespace Seguridad
{
    public class UsuarioWeb
    {
        private string user;
        private string _password;
        private string database;

        private string user_web;
        private string password_web;
        private string _emailWEB;
        private string _error;
        private bool _loggedIn;
        private bool _verified;
        private string _fechaUltimoAcceso;
   

        public UsuarioWeb(string sUsuario, string sClave, string sBaseDatos, string userw, string passwordw)
        {
            user = sUsuario;
            _password = sClave;
            database = sBaseDatos;
            user_web = userw;
            password_web = passwordw;
            _loggedIn = false;
        }

        public UsuarioWeb(string sUsuario, string sClave, string sBaseDatos, string userw)
        {
            user = sUsuario;
            _password = sClave;
            database = sBaseDatos;
            user_web = userw;
            _loggedIn = false;
        }

        public void LogIn()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, _password, database, "Seguridad.UsuarioWeb.cs LogIn()");

            oDatos.Paquete(Constantes.StoredProcedures.WebUserLogin);
            oDatos.Parametro("pv_identificacion", user_web);
            oDatos.Parametro("pv_clave", "V", 100, "O");
            oDatos.Parametro("pv_descripcion", "V", 80, "O");
            oDatos.Parametro("pv_estado_act", "V", 2, "O");
            oDatos.Parametro("pd_fecha_ultimo_acceso", "D", "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string passwordFromDb;
                    passwordFromDb = oDatos.RetornarParametro("pv_clave").ToString();
                    oDatos.RetornarParametro("pv_descripcion").ToString();
                    _verified = oDatos.RetornarParametro("pv_estado_act").ToString() == Constantes.UsuarioWeb.EstadoVerificado;
                    _error = oDatos.RetornarParametro("pv_error").ToString();
                    _fechaUltimoAcceso = oDatos.RetornarParametro("pd_fecha_ultimo_acceso").ToString();
                    //ERROR 1 ->  El usuario no puede ser nulo
                    //ERROR 2 -> El usuario no existe o no está registrado
                    //ERROR 3 -> error oracle
                    //ERROR S -> NO HAY ERROR
                    if (_error == "S")
                    {
                        Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                        passwordFromDb = objCrypto.DescifrarCadena(passwordFromDb);
                        if (password_web == passwordFromDb)
                            _loggedIn = true;
                        else
                        {
                            _loggedIn = false;
                            _error = Constantes.UsuarioWeb.ErrorMsgWrongPassword;
                        }
                    }
                    else
                    {
                        _loggedIn = false;
                        _error = _error == "2" ? Constantes.UsuarioWeb.ErrorMsgUserDoesntExists : Constantes.UsuarioWeb.ErrorMsgLoginUnknown;
                    }
                }
                else
                {
                    _password = null;
                    _error = "El servicio no está disponible en este momento por mantenimiento, intente más tarde por favor.";
                    _loggedIn = false;
                }
            }
            catch 
            {
                _error = "Error al acceder a su cuenta, intente nuevamente por favor.";
            }
            finally
            {
                oDatos.Dispose();
            }
            
        }


        public bool Register(string nombres, string apellidos, string email, string ip, string fechaCreacion)
        {
            bool retValue = false;
            if (Utilities.Utils.IsValidEmail(email))
            {
            
                AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, _password, database, "Seguridad.UsuarioWeb.cs Register()");

                oDatos.Paquete(Constantes.StoredProcedures.WebUserRegister);
                #region agregar parámetros a stored procedure
                oDatos.Parametro("pv_identificacion", user_web);
                oDatos.Parametro("pv_clave", password_web);
                oDatos.Parametro("pv_nombres", nombres);
                oDatos.Parametro("pv_apellidos", apellidos);
                oDatos.Parametro("pv_email", email);
                oDatos.Parametro("pv_ip", ip);
                oDatos.Parametro("pv_fecha", fechaCreacion);
                oDatos.Parametro("pv_error", "V", 1000, "O");
                #endregion
                try
                {
                    if (oDatos.Ejecutar("R"))
                    {
                        _error = oDatos.RetornarParametro("pv_error").ToString();
                        //ERROR 1 ->  El usuario no existe o no está registrado
                        //ERROR S -> NO HAY ERROR
                        if (_error != "S")
                        {
                            switch (_error)
                            {
                                case "1":
                                    _error = Constantes.UsuarioWeb.ErrorMsgUserAlreadyExists;
                                    break;
                                case "2":
                                    _error = Constantes.UsuarioWeb.ErrorMsgEmailAlreadyExists;
                                    break;
                                default:
                                    _error = Constantes.UsuarioWeb.ErrorMsgLoginUnknown;
                                    break;
                            }
                        }
                        else
                        {
                            retValue = true;

                            #region envío de email de activación de cuenta
                            Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                            objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                            objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                            string identificacionEnc = Utilities.Utils.EncodeToBase64(objCrypto.CifrarCadena(this.user_web));

                            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                            correo.From = new System.Net.Mail.MailAddress("Comisión de Tránsito del Ecuador <" + Constantes.ParametrosEnvioEmail.mailAddress + ">");
                            correo.ReplyTo = new System.Net.Mail.MailAddress(Constantes.ParametrosEnvioEmail.replyTo);
                            correo.To.Add(email);
                            correo.Subject = "Registro de usuario en www.cte.gob.ec";
                            //correo.Body = "Saludos " + nombres + ",\n\nEste mensaje fue generado automáticamente por www.ctg.gov.ec para confirmar su solicitud de registro.\n\nPara ingresar deberá de ingresar su número de cédula/pasaporte " + this.user_web + " y su contraseña.\n\nAl registrarse en nuestra página web tiene acceso a realizar exámenes de pruebas, y próximamente realizar pagos de infracciones, matrículas y más.\n";
                            string urlValidate = "https://secure.cte.gob.ec/CTGWebApps/ValidateRegistration.aspx?" + Constantes.UsuarioWeb.URLParamEncIdent + "=" + identificacionEnc + "&" + Constantes.UsuarioWeb.URLParamIdent + "=" + user_web;
                            string body = "Saludos " + nombres + ",<br /><br />\n\nEste mensaje fue generado automáticamente por www.cte.gob.ec para confirmar su solicitud de registro.<br /><br />\n\nPara terminar el proceso de registro debe dar clic en el siguiente enlace (o copie y pegue la URL en la barra de direcciones de su navegador web) <br /><br />\n\n <a href=\"" + urlValidate + "\">" + urlValidate + "</a> <br /><br />\n\n";
                            //body += "<b><i>Si usted no realiza esto, su cuenta no podrá ser activada. Usted tiene un lapso de 7 días desde que recibió este e-mail para activar su cuenta. Si no lo hace después de los 7 días, su cuenta será eliminada y tendrá que solicitar su registro nuevamente.</i></b>";
                            correo.Body = body;
                            correo.IsBodyHtml = true;
                            correo.Priority = System.Net.Mail.MailPriority.Normal;
                            //
                            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                            //---------------------------------------------
                            // Estos datos debes rellanarlos correctamente
                            //---------------------------------------------
                            smtp.Host = Constantes.ParametrosEnvioEmail.smtpHost;
                            smtp.Credentials = new NetworkCredential(Constantes.ParametrosEnvioEmail.smtpUser, Constantes.ParametrosEnvioEmail.smptPassword);
                            //smtp.EnableSsl = false;
                            try
                            {
                                smtp.Send(correo);
                                _error = "Se le ha enviado a su correo electrónico el mensaje de activación de su usuario.";
                            }
                            catch
                            {
                                _error = "El mensaje de activación de sus cuenta no pudo ser enviado a su correo electrónico, por favor comuníquese con la CTE desde el formulario de Contacto de nuestra página web.";
                            }
                            #endregion
                        }
                    }
                    else
                        _error = oDatos.Mensaje;
                        //_error = "El servicio no está disponible en este momento por mantenimiento, intente más tarde por favor.";

                    
                }
                catch (Exception)
                {
                    _error = "El servicio no está disponible en este momento por mantenimiento, intente más tarde por favor.";
                }
                finally
                {
                    oDatos.Dispose();
                }
            }
            else
                _error = "El formato del correo electrónico ingresado es erróneo.";

            return retValue;
        }

        private static void Senddata(Socket s, string msg)
        {
            byte[] _msg = Encoding.ASCII.GetBytes(msg);
            s.Send(_msg, 0, _msg.Length, SocketFlags.None);
        }


        public bool CheckEmailExistence(string email)
        {
            string[] host = (email.Split('@'));
            string hostname = host[1];

            if (hostname == "ctg.gov.ec")
            {
                return true;
            }
            else
            {
                try
                {
                    IPHostEntry IPhst = Dns.Resolve(hostname);
                    IPEndPoint endPt = new IPEndPoint(IPhst.AddressList[0], 25);
                    Socket s = new Socket(endPt.AddressFamily,
                                 SocketType.Stream, ProtocolType.Tcp);
                    s.Connect(endPt);

                    //Attempting to connect

                    if (!s.Connected)
                    {
                        s.Close();
                        return false;
                    }

                    //HELO server

                    Senddata(s, string.Format("HELO {0}\r\n", Dns.GetHostName()));
                    if (!s.Connected)
                    {
                        return false;
                    }

                    //Identify yourself

                    //Servers may resolve your domain and check whether 

                    //you are listed in BlackLists etc.

                    Senddata(s, string.Format("MAIL From: {0}\r\n",
                         Constantes.ParametrosEnvioEmail.mailAddress));
                    if (!s.Connected)
                    {
                        s.Close();
                        return false;
                    }


                    //Attempt Delivery (I can use VRFY, but most 

                    //SMTP servers only disable it for security reasons)

                    Senddata(s, email);
                    if (!s.Connected)
                    {
                        s.Close();
                        return false;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public string GetEncPassword()
        {
            string encPassword = "";
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, _password, database, "Seguridad.UsuarioWeb.cs GetEncPassword()");

            oDatos.Paquete(Constantes.StoredProcedures.WebUserGetPassword);
            oDatos.Parametro("pv_identificacion", user_web);
            oDatos.Parametro("pv_email", "V", 120, "O");
            oDatos.Parametro("pv_clave", "V", 120, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    _error = oDatos.RetornarParametro("pv_error").ToString();
                    //ERROR 1 ->  El usuario no puede ser nulo
                    //ERROR 2 -> El usuario no existe o no está registrado
                    //ERROR 3 -> Debe modificar su clave, es la primera vez que entra al sistema
                    //ERROR 4 -> el password no puede ser nulo
                    //ERROR S -> NO HAY ERROR
                    if (_error == "S")
                    {
                        encPassword = oDatos.RetornarParametro("pv_clave").ToString();
                    }
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                oDatos.Dispose();
            }

            return encPassword;
        }

        public bool SendLostPassword()
        {
            bool retValue = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, _password, database, "Seguridad.UsuarioWeb.cs SendLostPassword()");

            oDatos.Paquete(Constantes.StoredProcedures.WebUserGetPassword);
            oDatos.Parametro("pv_identificacion", user_web);
            oDatos.Parametro("pv_email", "V", 120, "O");
            oDatos.Parametro("pv_clave", "V", 120, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");
            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    _error = oDatos.RetornarParametro("pv_error").ToString();
                    //ERROR 1 ->  El usuario no puede ser nulo
                    //ERROR 2 -> El usuario no existe o no está registrado
                    //ERROR 3 -> Debe modificar su clave, es la primera vez que entra al sistema
                    //ERROR 4 -> el password no puede ser nulo
                    //ERROR S -> NO HAY ERROR
                    if (_error == "S")
                    {
                        Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                        string nombres = oDatos.RetornarParametro("pv_names").ToString();
                        string email = oDatos.RetornarParametro("pv_email").ToString();
                        string contrasena = objCrypto.DescifrarCadena(oDatos.RetornarParametro("pv_clave").ToString());
                        this._emailWEB = email;
                        #region envío de email
                        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                        correo.From = new System.Net.Mail.MailAddress("Comisión de Tránsito del Ecuador <" + Constantes.ParametrosEnvioEmail.mailAddress + ">");
                        correo.ReplyTo = new System.Net.Mail.MailAddress(Constantes.ParametrosEnvioEmail.replyTo);
                        correo.To.Add(email);
                        correo.Subject = "Recuperación de contraseña en www.cte.gob.ec";
                        correo.Body = "Saludos " + nombres + ",\n\nEste mensaje fue generado automáticamente por www.cte.gob.ec al recibir su solicitud de contraseña por pérdida de la misma.\n\nSu contraseña de acceso es: " + contrasena + "\n";
                        correo.IsBodyHtml = false;
                        correo.Priority = System.Net.Mail.MailPriority.Normal;
                        //
                        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        //---------------------------------------------
                        // Estos datos debes rellanarlos correctamente
                        //---------------------------------------------
                        smtp.Host = Constantes.ParametrosEnvioEmail.smtpHost;
                        smtp.Credentials = new NetworkCredential(Constantes.ParametrosEnvioEmail.smtpUser, Constantes.ParametrosEnvioEmail.smptPassword);
                        //smtp.EnableSsl = false;
                        try
                        {
                            smtp.Send(correo);
                            retValue = true;
                        }
                        catch(Exception ex)
                        {
                            this._error = "Su contraseña no pudo ser enviada a " + email;
                        }
                        #endregion
                    }
                    else
                    {
                        if (_error == "1")
                        {
                            string[] dataUsrWeb = ObtenerDatosUsuarioWeb();
                            _error = dataUsrWeb.Length > 2 && dataUsrWeb[3] == Constantes.UsuarioWeb.EstadoInactivo
                                         ? "Su usuario se encuentra inactivo.<br />Debe seguir las instrucciones que se indican en el e-mail de activación, enviado cuando registró su cuenta."
                                         : Constantes.UsuarioWeb.ErrorMsgUserDoesntExists;
                        }
                    }
                }
                else
                {
                    _error = "El servicio no está disponible en este momento por mantenimiento, intente más tarde por favor.";
                    return false;
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                oDatos.Dispose();
            }

            return retValue;
        }

       
        public bool ReSendActivationEmail()
        {
            bool retValue = false;
            string[] dataUsrWeb = ObtenerDatosUsuarioWeb();

            try
            {
                if (dataUsrWeb.Length > 2)
                {
                    if (dataUsrWeb[3] == Constantes.UsuarioWeb.EstadoInactivo)
                    {
                        string nombres = dataUsrWeb[0];
                        this._emailWEB = dataUsrWeb[2];

                        Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                        string identificacionEnc = Utilities.Utils.EncodeToBase64(objCrypto.CifrarCadena(user_web));
                        string urlValidate = "https://secure.cte.gob.ec/CTGWebApps/ValidateRegistration.aspx?" + Constantes.UsuarioWeb.URLParamEncIdent + "=" + identificacionEnc + "&" + Constantes.UsuarioWeb.URLParamIdent + "=" + this.user_web;

                        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                        correo.From = new System.Net.Mail.MailAddress("Comisión de Tránsito del Ecuador <" + Constantes.ParametrosEnvioEmail.mailAddress + ">");
                        correo.ReplyTo = new System.Net.Mail.MailAddress(Constantes.ParametrosEnvioEmail.replyTo);
                        correo.To.Add(_emailWEB);
                        correo.Subject = "Activación de usuario en www.cte.gob.ec";
                        string body = "Saludos " + nombres + ",<br /><br />\n\nEste mensaje fue generado automáticamente por www.cte.gob.ec para confirmar su solicitud de registro.<br /><br />\n\nPara terminar el proceso de registro debe dar clic en el siguiente enlace (o copie y pegue la URL en la barra de direcciones de su navegador web) <br /><br />\n\n <a href=\"" + urlValidate + "\">" + urlValidate + "</a> <br /><br />\n\n";
                        correo.Body = body;
                        correo.IsBodyHtml = true;
                        correo.Priority = System.Net.Mail.MailPriority.High;
                        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                        smtp.Host = Constantes.ParametrosEnvioEmail.smtpHost;
                        smtp.Credentials = new NetworkCredential(Constantes.ParametrosEnvioEmail.smtpUser, Constantes.ParametrosEnvioEmail.smptPassword);
                        try
                        {
                            smtp.Send(correo);
                            retValue = true;
                        }
                        catch
                        {
                            _error = "No se pudo enviar el e-mail de activación a " + _emailWEB;
                        }
                    }
                    else
                        _error = "Su usuario se encuentra activo, usted no necesita que se le envíe nuevamente el correo electrónico de activación";
                }
                else
                    _error = dataUsrWeb[1];
            }
            catch (Exception)
            {
            }

            return retValue;
        }

        public string PersonaExisteEnAxis(string identificacion)
        {
            string result;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, _password, database, "Seguridad.UsuarioWeb.cs PersonaExisteEnAxis()");
            
            oDatos.Paquete(Constantes.StoredProcedures.WebUserExisteAxis);
            oDatos.Parametro("pv_identificacion", user_web);
            oDatos.Parametro("pv_error", "V", 1, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    switch (error)
                    {
                        case "1"://no existe
                            result = "Usuario con identificación " + identificacion + " no registra contrato con la CTE.";
                            break;
                        case "2"://inactivo
                            result = "Usuario con identificación " + identificacion + " tiene contrato con la CTE con estado inactivo";
                            break;
                        case "3"://si registra contrato
                            result = "";
                            break;
                        default://error en DB
                            result = "No se pudo verificar el contrato del usuario con la CTE.";
                            break;
                    }
                }
                else
                {
                    result = "No se pudo verificar el contrato del usuario con la CTE.";
                }
            }
            catch
            {
                result = "No se pudo verificar el contrato del usuario con la CTE.";
            }
            finally
            {
                oDatos.Dispose();
            }

            return result;
        }


        public string[] ObtenerDatosUsuarioWeb()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, _password, database, "Seguridad.UsuarioWeb.cs ObtenerDatosUsuarioWeb()");
            string[] datosWEB = new string[5];

            oDatos.Paquete(Constantes.StoredProcedures.WebUserGetData);
            oDatos.Parametro("pv_identificacion", user_web);
            oDatos.Parametro("pv_nombres", "V", 120, "O");
            oDatos.Parametro("pv_apellidos", "V", 120, "O");
            oDatos.Parametro("pv_email", "V", 120, "O");
            oDatos.Parametro("pv_estado", "V", 2, "O");
            oDatos.Parametro("pv_error", "V", 120, "O");

            try
            {
                if (oDatos.Ejecutar("R"))
                {
                    string error = oDatos.RetornarParametro("pv_error").ToString();
                    //ERROR 1 ->  El usuario no puede ser nulo
                    //ERROR 2 -> El usuario no existe o no está registrado
                    //ERROR 3 -> Debe modificar su clave, es la primera vez que entra al sistema
                    //ERROR 4 -> el password no puede ser nulo
                    //ERROR S -> NO HAY ERROR
                    if (error == "")
                    {
                        datosWEB[0] = oDatos.RetornarParametro("pv_nombres").ToString();
                        datosWEB[1] = oDatos.RetornarParametro("pv_apellidos").ToString();
                        datosWEB[2] = oDatos.RetornarParametro("pv_email").ToString();
                        datosWEB[3] = oDatos.RetornarParametro("pv_estado").ToString();
                        datosWEB[4] = oDatos.RetornarParametro("pv_error").ToString();
                    }
                    else
                    {
                        datosWEB[0] = "error";
                        datosWEB[1] = error;
                    }
                }
                else
                {
                    datosWEB[0] = "error";
                    datosWEB[1] = "Error: No se pudo conectar a la Base de Datos";
                }

            }
            catch
            {
                datosWEB[0] = "error";
                datosWEB[1] = "Error: No se pudo conectar a la Base de Datos";
            }
            finally
            {
                oDatos.Dispose();
            }

            return datosWEB;
        }


        public bool ActualizaDato(string campo, string value)
        {
            bool result = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, _password, database, "Seguridad.UsuarioWeb.cs ActualizaDato()");
            oDatos.Paquete(Constantes.StoredProcedures.WebUserUpdateData);
            oDatos.Parametro("pv_identificacion", user_web);
            oDatos.Parametro("pv_campo", campo);
            oDatos.Parametro("pv_valor", value);
            oDatos.Parametro("pv_error", "V", 1000, "O");

            try
            {
                if (oDatos.Ejecutar("N"))
                {
                    if (oDatos.RetornarParametro("pv_error").ToString() == "S")
                        result = true;
                    else
                        _error = oDatos.RetornarParametro("pv_error").ToString();
                }
                else
                    _error = oDatos.Mensaje;
            }
            catch
            {
                _error = "Error: No se pudo conectar a la Base de Datos";
            }
            finally
            {
                oDatos.Dispose();
            }

            return result;
        }


        public static bool ValidateIPDNSAgent(object encStartIP, object encStartDNS, object encStartAgent, string currentIP, string currentDNS, string currentAgent)
        {
            if (encStartIP != null && encStartDNS != null && encStartAgent != null)
            {
                Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

                return (objCrypto.DescifrarCadena(encStartIP.ToString()) == currentIP) && (objCrypto.DescifrarCadena(encStartDNS.ToString()) == currentDNS) && (objCrypto.DescifrarCadena(encStartAgent.ToString()) == currentAgent);
            }
            else
                return false;
        }
       
        
        #region "Propiedades"
        public bool isLoggedIn
        {
            get
            {
                return _loggedIn;
            }
        }

        public bool isVerified
        {
            get
            {
                return _verified;
            }
        }
        
        public string Username
        {
            get
            {
                return user_web;
            }
        }

        public string UserEmail
        {
            get
            {
                return _emailWEB;
            }
        }

        public string Error
        {
            get
            {
                return _error;
            }
        }

        public string FechaUltimoAcceso
        {
            get
            {
                return _fechaUltimoAcceso;
            }
        }
        
        #endregion
        
    }
}
