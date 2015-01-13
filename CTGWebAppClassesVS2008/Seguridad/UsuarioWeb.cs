using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using AccesoDatos;
using CifradoCs;
using System.Net;
using System.Net.Sockets;

namespace Seguridad
{
    public class UsuarioWeb
    {
        private string user;
        private string password;
        private string database;

        private string user_web;
        private string password_web;
        private string email_web;
        private string description;
        private string error;
        private bool loggedIn;
        private bool verified;

   

        public UsuarioWeb(string sUsuario, string sClave, string sBaseDatos, string userw, string passwordw)
        {
            this.user = sUsuario;
            this.password = sClave;
            this.database = sBaseDatos;
            this.user_web = userw;
            this.password_web = passwordw;
            this.loggedIn = false;
        }

        public UsuarioWeb(string sUsuario, string sClave, string sBaseDatos, string userw)
        {
            this.user = sUsuario;
            this.password = sClave;
            this.database = sBaseDatos;
            this.user_web = userw;
            this.loggedIn = false;
        }

        public void LogIn()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Seguridad.UsuarioWeb.cs LogIn()");

            oDatos.Paquete(Constantes.StoredProcedures.WebUserLogin);
            oDatos.Parametro("pv_identificacion", this.user_web);
            oDatos.Parametro("pv_clave", "V", 100, "O");
            oDatos.Parametro("pv_descripcion", "V", 80, "O");
            oDatos.Parametro("pv_estado_act", "V", 2, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");
            
            if (oDatos.Ejecutar("R"))
            {
                string password_from_db = "";
                password_from_db = oDatos.RetornarParametro("pv_clave").ToString();
                description = oDatos.RetornarParametro("pv_descripcion").ToString();
                if (oDatos.RetornarParametro("pv_estado_act").ToString() == Constantes.UsuarioWeb.EstadoVerificado)
                {
                    this.verified = true;
                }
                else
                {
                    this.verified = false;
                }
                error = oDatos.RetornarParametro("pv_error").ToString();
                //ERROR 1 ->  El usuario no puede ser nulo
                //ERROR 2 -> El usuario no existe o no está registrado
                //ERROR 3 -> error oracle
                //ERROR S -> NO HAY ERROR
                if (error == "S")
                {
                    Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                    objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                    objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                    password_from_db = objCrypto.DescifrarCadena(password_from_db);
                    if (password_web == password_from_db)
                        loggedIn = true;
                    else
                    {
                        loggedIn = false;
                        this.error = Constantes.UsuarioWeb.ErrorMsgWrongPassword;
                    }
                }
                else
                {
                    loggedIn = false;
                    if (error == "2")
                        this.error = Constantes.UsuarioWeb.ErrorMsgUserDoesntExists;
                    else
                        this.error = Constantes.UsuarioWeb.ErrorMsgLoginUnknown;
                }
            }
            else
            {
                password = null;
                description = null;
                error = "El servicio no está disponible en este momento, por mantenimiento.";
                loggedIn = false;
            }

            oDatos.Dispose();
        }


        public bool Register(string nombres, string apellidos, string email)
        {
            //if (CheckEmailExistence(email))
            //{
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Seguridad.UsuarioWeb.cs Register()");

                oDatos.Paquete(Constantes.StoredProcedures.WebUserRegister);
                oDatos.Parametro("pv_identificacion", this.user_web);
                oDatos.Parametro("pv_clave", this.password_web);
                oDatos.Parametro("pv_nombres", nombres);
                oDatos.Parametro("pv_apellidos", apellidos);
                oDatos.Parametro("pv_email", email);
                oDatos.Parametro("pv_error", "V", 1000, "O");

                if (oDatos.Ejecutar("R"))
                {
                    error = oDatos.RetornarParametro("pv_error").ToString();
                    //ERROR 1 ->  El usuario no existe o no está registrado
                    //ERROR S -> NO HAY ERROR
                    if (error != "S")
                    {
                        if (error == "1")
                            this.error = Constantes.UsuarioWeb.ErrorMsgUserAlreadyExists;
                        else
                            this.error = Constantes.UsuarioWeb.ErrorMsgLoginUnknown;
                        return false;
                    }
                }
                else
                {
                    error = "No se pudo conectar a la Base de Datos, intente nuevamente por favor.";
                    return false;
                }

                Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                string identificacion_enc = Utilities.Utils.EncodeToBase64(objCrypto.CifrarCadena(this.user_web));

                System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                correo.From = new System.Net.Mail.MailAddress(Constantes.ParametrosEnvioEmail.mailAddress);
                correo.To.Add(email);
                correo.Subject = "Registro de usuario en www.ctg.gov.ec";
                //correo.Body = "Saludos " + nombres + ",\n\nEste mensaje fue generado automáticamente por www.ctg.gov.ec para confirmar su solicitud de registro.\n\nPara ingresar deberá de ingresar su número de cédula/pasaporte " + this.user_web + " y su contraseña.\n\nAl registrarse en nuestra página web tiene acceso a realizar exámenes de pruebas, y próximamente realizar pagos de infracciones, matrículas y más.\n";
                string urlValidate = "https://secure.ctg.gov.ec/CTGWebApps/ValidateRegistration.aspx?" + Constantes.UsuarioWeb.URLParamEncIdent + "=" + identificacion_enc + "&" + Constantes.UsuarioWeb.URLParamIdent + "=" + this.user_web;
                string body = "Saludos " + nombres + ",<br /><br />\n\nEste mensaje fue generado automáticamente por www.ctg.gov.ec para confirmar su solicitud de registro.<br /><br />\n\nPara terminar el proceso de registro debe dar clic en el siguiente enlace (o copie y pegue la URL en la barra de direcciones de su navegador web) <br /><br />\n\n <a href=\"" + urlValidate + "\">" + urlValidate + "</a> <br /><br />\n\n";
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
                smtp.Credentials = new System.Net.NetworkCredential(Constantes.ParametrosEnvioEmail.smtpUser, Constantes.ParametrosEnvioEmail.smptPassword);
                //smtp.EnableSsl = false;
                try
                {
                    smtp.Send(correo);
                    error = "Se le ha enviado e-mail de bienvenida.";
                }
                catch (Exception ex)
                {
                    //error = "ERROR: " + ex.Message;
                    error = "El e-mail de activación de sus cuenta no pudo ser enviado, por favor comuníquese con la CTG desde el formulario de Contacto de nuestra página web.";
                    return false;
                }

                oDatos.Dispose();

                return true;
            //}
            //else
            //{
            //    error = "El email ingresado no existe o es inaccesible.";
            //    return false;
            //}
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
                         "webmaster@ctg.gov.ec"));
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
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public string GetEncPassword()
        {
            string enc_password = "";
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Seguridad.UsuarioWeb.cs GetEncPassword()");

            oDatos.Paquete(Constantes.StoredProcedures.WebUserGetPassword);
            oDatos.Parametro("pv_identificacion", this.user_web);
            oDatos.Parametro("pv_email", "V", 120, "O");
            oDatos.Parametro("pv_clave", "V", 120, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");

            if (oDatos.Ejecutar("R"))
            {
                error = oDatos.RetornarParametro("pv_error").ToString();
                //ERROR 1 ->  El usuario no puede ser nulo
                //ERROR 2 -> El usuario no existe o no está registrado
                //ERROR 3 -> Debe modificar su clave, es la primera vez que entra al sistema
                //ERROR 4 -> el password no puede ser nulo
                //ERROR S -> NO HAY ERROR
                if (error == "S")
                {
                    enc_password = oDatos.RetornarParametro("pv_clave").ToString();
                }
            }

            oDatos.Dispose();

            return enc_password;
        }

        public bool SendLostPassword()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Seguridad.UsuarioWeb.cs SendLostPassword()");

            oDatos.Paquete(Constantes.StoredProcedures.WebUserGetPassword);
            oDatos.Parametro("pv_identificacion", this.user_web);
            oDatos.Parametro("pv_email", "V", 120, "O");
            oDatos.Parametro("pv_clave", "V", 120, "O");
            oDatos.Parametro("pv_error", "V", 1000, "O");

            if (oDatos.Ejecutar("R"))
            {
                error = oDatos.RetornarParametro("pv_error").ToString();
                //ERROR 1 ->  El usuario no puede ser nulo
                //ERROR 2 -> El usuario no existe o no está registrado
                //ERROR 3 -> Debe modificar su clave, es la primera vez que entra al sistema
                //ERROR 4 -> el password no puede ser nulo
                //ERROR S -> NO HAY ERROR
                if (error == "S")
                {
                    Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                    objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                    objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                    string nombres = oDatos.RetornarParametro("pv_names").ToString();
                    string email = oDatos.RetornarParametro("pv_email").ToString();
                    string contrasena = objCrypto.DescifrarCadena(oDatos.RetornarParametro("pv_clave").ToString());
                    this.email_web = email;
                    oDatos.Dispose();

                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                    correo.From = new System.Net.Mail.MailAddress(Constantes.ParametrosEnvioEmail.mailAddress);
                    correo.To.Add(email);
                    correo.Subject = "Recuperación de contraseña en www.ctg.gov.ec";
                    correo.Body = "Saludos " + nombres + ",\n\nEste mensaje fue generado automáticamente por www.ctg.gov.ec al recibir su solicitud de contraseña por pérdida de la misma.\n\nSu contraseña de acceso es: " + contrasena + "\n";
                    correo.IsBodyHtml = false;
                    correo.Priority = System.Net.Mail.MailPriority.Normal;
                    //
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    //---------------------------------------------
                    // Estos datos debes rellanarlos correctamente
                    //---------------------------------------------
                    smtp.Host = Constantes.ParametrosEnvioEmail.smtpHost;
                    smtp.Credentials = new System.Net.NetworkCredential(Constantes.ParametrosEnvioEmail.smtpUser, Constantes.ParametrosEnvioEmail.smptPassword);
                    //smtp.EnableSsl = false;
                    try
                    {
                        smtp.Send(correo);
                    }
                    catch (Exception ex)
                    {
                        this.error = "Su contraseña no pudo ser enviada a " + email;
                        return false;
                    }
                }
                else
                {
                    if (error == "1")
                    {
                        string[] dataUsrWeb = ObtenerDatosUsuarioWeb();
                        if (dataUsrWeb.Length > 2 && dataUsrWeb[3] == Constantes.UsuarioWeb.EstadoInactivo)
                        {
                            this.error = "Su usuario se encuentra inactivo.<br />Debe seguir las instrucciones que se indican en el e-mail de activación, enviado cuando registró su cuenta.";
                        }
                        else
                        {
                            this.error = Constantes.UsuarioWeb.ErrorMsgUserDoesntExists;
                        }
                    }
                    return false;
                }
            }
            else
            {
                this.error = "No se pudo conectar a la Base de Datos";
                return false;
            }

            oDatos.Dispose();

            return true;
        }

       
        public bool ReSendActivationEmail()
        {
            string[] dataUsrWeb = ObtenerDatosUsuarioWeb();

            if (dataUsrWeb.Length > 2)
            {
                if (dataUsrWeb[3] == Constantes.UsuarioWeb.EstadoInactivo)
                {
                    string nombres = dataUsrWeb[0];
                    this.email_web = dataUsrWeb[2];

                    Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                    objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                    objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                    string identificacion_enc = Utilities.Utils.EncodeToBase64(objCrypto.CifrarCadena(this.user_web));
                    string urlValidate = "https://secure.ctg.gov.ec/CTGWebApps/ValidateRegistration.aspx?" + Constantes.UsuarioWeb.URLParamEncIdent + "=" + identificacion_enc + "&" + Constantes.UsuarioWeb.URLParamIdent + "=" + this.user_web;

                    System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
                    correo.From = new System.Net.Mail.MailAddress(Constantes.ParametrosEnvioEmail.mailAddress);
                    correo.To.Add(this.email_web);
                    correo.Subject = "Registro de usuario en www.ctg.gov.ec";
                    string body = "Saludos " + nombres + ",<br /><br />\n\nEste mensaje fue generado automáticamente por www.ctg.gov.ec para confirmar su solicitud de registro.<br /><br />\n\nPara terminar el proceso de registro debe dar clic en el siguiente enlace (o copie y pegue la URL en la barra de direcciones de su navegador web) <br /><br />\n\n <a href=\"" + urlValidate + "\">" + urlValidate + "</a> <br /><br />\n\n";
                    correo.Body = body;
                    correo.IsBodyHtml = true;
                    correo.Priority = System.Net.Mail.MailPriority.High;
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.Host = Constantes.ParametrosEnvioEmail.smtpHost;
                    smtp.Credentials = new System.Net.NetworkCredential(Constantes.ParametrosEnvioEmail.smtpUser, Constantes.ParametrosEnvioEmail.smptPassword);
                    try
                    {
                        smtp.Send(correo);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        this.error = "No se pudo enviar el e-mail de activación a " + this.email_web;
                        return false;
                    }
                }
                else
                {
                    this.error = "Su usuario se encuentra activo, usted no necesita que se le envíe nuevamente el correo electrónico de activación";
                    return false;
                }
            }
            else
            {
                this.error = dataUsrWeb[1];
                return false;
            }

        }

        public string PersonaExisteEnAxis(string identificacion)
        {
            string result;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Seguridad.UsuarioWeb.cs PersonaExisteEnAxis()");
            
            oDatos.Paquete(Constantes.StoredProcedures.WebUserExisteAxis);
            oDatos.Parametro("pv_identificacion", this.user_web);
            oDatos.Parametro("pv_error", "V", 1, "O");

            if (oDatos.Ejecutar("R"))
            {
                string error = oDatos.RetornarParametro("pv_error").ToString();
                switch (error)
                {
                    case "1"://no existe
                        result = "Usuario con identificación " + identificacion + " no registra contrato con la CTG.";
                        break;
                    case "2"://inactivo
                        result = "Usuario con identificación " + identificacion + " tiene contrato con la CTG con estado inactivo";
                        break;
                    case "3"://si registra contrato
                        result = "";
                        break;
                    default://error en DB
                        result = "No se pudo verificar el contrato del usuario con la CTG.";
                        break;
                }
            }
            else
            {
                result = "No se pudo verificar el contrato del usuario con la CTG.";
            }

            oDatos.Dispose();

            return result;
        }


        public string[] ObtenerDatosUsuarioWeb()
        {
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(user, password, database, "Seguridad.UsuarioWeb.cs ObtenerDatosUsuarioWeb()");
            string[] datos_web = new string[5];

            oDatos.Paquete(Constantes.StoredProcedures.WebUserGetData);
            oDatos.Parametro("pv_identificacion", this.user_web);
            oDatos.Parametro("pv_nombres", "V", 120, "O");
            oDatos.Parametro("pv_apellidos", "V", 120, "O");
            oDatos.Parametro("pv_email", "V", 120, "O");
            oDatos.Parametro("pv_estado", "V", 2, "O");
            oDatos.Parametro("pv_error", "V", 120, "O");

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
                    datos_web[0] = oDatos.RetornarParametro("pv_nombres").ToString();
                    datos_web[1] = oDatos.RetornarParametro("pv_apellidos").ToString();
                    datos_web[2] = oDatos.RetornarParametro("pv_email").ToString();
                    datos_web[3] = oDatos.RetornarParametro("pv_estado").ToString();
                    datos_web[4] = oDatos.RetornarParametro("pv_error").ToString();
                }
                else
                {
                    datos_web[0] = "error";
                    datos_web[1] = error;
                }
            }
            else
            {
                datos_web[0] = "error";
                datos_web[1] = "Error: No se pudo conectar a la Base de Datos";
            }

            oDatos.Dispose();

            return datos_web;
        }


        public bool ActualizaDato(string campo, string value)
        {
            bool result = false;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(this.user, this.password, this.database, "Seguridad.UsuarioWeb.cs ActualizaDato()");
            oDatos.Paquete(Constantes.StoredProcedures.WebUserUpdateData);
            oDatos.Parametro("pv_identificacion", user_web);
            oDatos.Parametro("pv_campo", campo);
            oDatos.Parametro("pv_valor", value);
            oDatos.Parametro("pv_error", "V", 1000, "O");

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


        public static bool ValidateIPDNSAgent(object encStartIP, object encStartDNS, object encStartAgent, string currentIP, string currentDNS, string currentAgent)
        {
            if (encStartIP != null && encStartDNS != null && encStartAgent != null)
            {
                Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
                objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

                if ((objCrypto.DescifrarCadena(encStartIP.ToString()) == currentIP) && (objCrypto.DescifrarCadena(encStartDNS.ToString()) == currentDNS) && (objCrypto.DescifrarCadena(encStartAgent.ToString()) == currentAgent))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
       
        
        #region "Propiedades"
        public bool isLoggedIn
        {
            get
            {
                return this.loggedIn;
            }
        }

        public bool isVerified
        {
            get
            {
                return this.verified;
            }
        }
        
        public string Username
        {
            get
            {
                return this.user_web;
            }
        }

        public string UserEmail
        {
            get
            {
                return this.email_web;
            }
        }

        public string Error
        {
            get
            {
                return this.error;
            }
        }

        
        #endregion
        
    }
}
