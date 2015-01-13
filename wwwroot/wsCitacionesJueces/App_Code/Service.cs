using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Xml;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{
    private string decriptedKey;
    private static string errorBadPassword = "Error de conexión a webservice";

    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent();
        this.decriptedKey = "+g&+w8Yk@V3aAsA";
    }

    public bool VerifyAccess(string key)
    {
        CifradoCs.Crypto crypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        crypto.Key = "webCTGespPROYsept2007";
        crypto.IV = "deptoInfoCTG19092007";
        if (crypto.DescifrarCadena(key) == this.decriptedKey)
            return true;
        else
            return false;
    }

    [WebMethod]
    public DataSet ActasJuzgamientoCitacNoImpug(string usuarioJuez, string fechaDesde, string fechaHasta, out string error, string key) 
    {
        DataSet ds = new DataSet();

        if (VerifyAccess(key))
        {
            Auxiliar.CitacionesJueces oCitacJueces = new Auxiliar.CitacionesJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
            Auxiliar.ActasJueces oActas = new Auxiliar.ActasJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
            ds.Tables.Add(oActas.CitacNoImpugParaActaJuzgam(oCitacJueces.GetJuzgado(usuarioJuez), fechaDesde, fechaHasta));
            error = oActas.Error;
        }
        else
        {
            error = errorBadPassword;
        }
        return ds;
    }

    [WebMethod]
    public DataSet ActasNotificacionCitacNoImpug(string usuarioJuez, string fechaDesde, string fechaHasta, out string error, string key)
    {
        DataSet ds = new DataSet();

        if (VerifyAccess(key))
        {
            Auxiliar.CitacionesJueces oCitacJueces = new Auxiliar.CitacionesJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
            Auxiliar.ActasJueces oActas = new Auxiliar.ActasJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
            ds.Tables.Add(oActas.CitacNoImpugParaActaNotificac(oCitacJueces.GetJuzgado(usuarioJuez), fechaDesde.ToString(), fechaHasta.ToString()));
            error = oActas.Error;
        }
        else
        {
            error = errorBadPassword;
        }
        return ds;
    }

    [WebMethod]
    public DataSet ActasNotificacionNoFirmPorSecret(string usuarioSec, out string error, string key)
    {
        DataSet ds = new DataSet();

        if (VerifyAccess(key))
        {
            Auxiliar.CitacionesJueces oCitacJueces = new Auxiliar.CitacionesJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
            Auxiliar.ActasJueces oActas = new Auxiliar.ActasJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
            ds.Tables.Add(oActas.ActasPendientesFirmaSecretario(usuarioSec, "N"));
            error = oActas.Error;
        }
        else
        {
            error = errorBadPassword;
        }
        return ds;
    }

    [WebMethod]
    public DataSet ActasJuzgamientoNoFirmPorSecret(string usuarioSec, out string error, string key)
    {
        DataSet ds = new DataSet();

        if (VerifyAccess(key))
        {
            Auxiliar.CitacionesJueces oCitacJueces = new Auxiliar.CitacionesJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
            Auxiliar.ActasJueces oActas = new Auxiliar.ActasJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
            ds.Tables.Add(oActas.ActasPendientesFirmaSecretario(usuarioSec, "A"));
            error = oActas.Error;
        }
        else
        {
            error = errorBadPassword;
        }
        return ds;
    }

    
    [WebMethod]
    public DataSet ReturnActaJuzgamData(string codCitac, string key)
    {
        if (VerifyAccess(key))
        {
            Auxiliar.ActasJueces oActasJueces = new Auxiliar.ActasJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
            return oActasJueces.GetDataForActaJuzgamiento(codCitac);
        }
        else
        {
            return new DataSet();
        }
    }


    [WebMethod]
    public DataSet ReturnActaNotificacData(string codCitac, string key)
    {
        if (VerifyAccess(key))
        {
            Auxiliar.ActasJueces oActasJueces = new Auxiliar.ActasJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
            return oActasJueces.GetDataForActaNotificacion(codCitac);
        }
        else
        {
            return new DataSet();
        }
    }


    [WebMethod]
    public bool SaveFileActaToDB(byte[] file, string numExpediente, string licencia, string juez, string tipoDoc, bool firmaJuez, bool firmaSec, out string error, string key)
    {
        if (VerifyAccess(key))
        {
            Auxiliar.ActasJueces oActasJueces = new Auxiliar.ActasJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
            if (oActasJueces.GuardarActaEnBD(file, numExpediente, licencia, juez, tipoDoc, firmaJuez, firmaSec))
            {
                error = "";
                return true;
            }
            else
            {
                error = oActasJueces.Error;
                return false;
            }
        }
        else
        {
            error = errorBadPassword;
            return false;
        }
    }


    [WebMethod]
    public byte[] DownloadActa(string numExpediente, string tipoDocumento, string licencia, string usuario, string key)
    {
        if (VerifyAccess(key))
        {
            Auxiliar.ActasJueces oActasJueces = new Auxiliar.ActasJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
            return oActasJueces.ObtenerActa(numExpediente, tipoDocumento, licencia, usuario);
        }
        else
        {
            return null;
        }
    }


    [WebMethod]
    public bool Login(string user, string password, string key)
    {
        if (VerifyAccess(key))
        {
            Seguridad.UsuarioCertificadoLicencia db_user = new Seguridad.UsuarioCertificadoLicencia(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], user);

            if (db_user.LogInSinRoles())//si no existe error al hacer login
            {
                CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
                objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                if (password == objCrypto.DescifrarCadena(db_user.Password))//verificar contraseña
                {
                    //Auxiliar.CitacionesJueces oCitacJueces = new Auxiliar.CitacionesJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
                    //position = oCitacJueces.GetCargo(user);
                    return true;
                }
                else//contraseña incorrecta
                {
                    //position = "";
                    return false;
                }
            }
            else//existe error
            {
                //position = "";
                return false;
            }
        }
        else
        {
            //position = null;
            return false;
        }
    }

    [WebMethod]
    public string GetCargo(string user)
    {
        Auxiliar.CitacionesJueces oCitacJueces = new Auxiliar.CitacionesJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "wsCitacionesJueces");
        string cargo = oCitacJueces.GetCargo(user);
        if (oCitacJueces.Error != "")
        {
            SendErrorAlert(oCitacJueces.Error);
        }
        return cargo;
    }

    private void SendErrorAlert(string errorMsg)
    {
        System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
        correo.From = new System.Net.Mail.MailAddress("webmaster@ctg.gov.ec");
        correo.To.Add("gpincay@ctg.gov.ec");
        correo.Subject = "Error Web Service de Jueces";
        string body = "Error en web service para aplicaciones de jueces de tránsito. <br /><br />" +
            "<b>Error:</b>  " + errorMsg +
            "<br /><b>Base de Datos:</b> " + System.Configuration.ConfigurationManager.AppSettings["base"];
        correo.Body = body;
        correo.IsBodyHtml = true;
        correo.Priority = System.Net.Mail.MailPriority.High;
        //
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        //---------------------------------------------
        // Estos datos debes rellanarlos correctamente
        //---------------------------------------------
        smtp.Host = "201.218.0.228";
        smtp.Credentials = new System.Net.NetworkCredential("webmaster", "123456");
        //smtp.EnableSsl = false;
        try
        {
            smtp.Send(correo);
        }
        catch (Exception ex)
        {
        }
    }
    
}
