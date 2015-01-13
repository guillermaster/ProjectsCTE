using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Data;
using CifradoCs;

[WebService(Namespace = "http://sicottt.cntttsv.gov.ec/wsCorpaire/")]
//[WebService(Namespace = "http://10.30.1.7/wsCorpaire/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string ConsultaVehiculo(string placa, string chasis, string camv) {
        MatriculacionCNTTTSV.VehiculoCNTTTSV objVehiculoCNTTSV = new MatriculacionCNTTTSV.VehiculoCNTTTSV(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"],
            placa, chasis, camv);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<DatosVehiculoCNTTTSV></DatosVehiculoCNTTTSV>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("DatosVehiculoCNTTTSV");

        if (objVehiculoCNTTSV.LoadDatosVehiculo())
        {
            DataTable dtDatosVeh = objVehiculoCNTTSV.DatosVehiculo;
            
            for (int i = 0; i < dtDatosVeh.Columns.Count; i++)
            {
                XmlNode newProperty = xmlDoc.CreateNode(XmlNodeType.Element, dtDatosVeh.Columns[i].ColumnName, null);
                newProperty.InnerText = dtDatosVeh.Rows[0][i].ToString();
                rootNode.AppendChild(newProperty);
            }
        }
        else
        {
            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = objVehiculoCNTTSV.Error;
            rootNode.AppendChild(nodeError);
        }

        return xmlDoc.InnerXml;
    }


    [WebMethod]
    public string ConsultaCitaciones(string placa, string chasis, string camv)
    {
        CitacionesCNTTTSV.Citaciones objCitacionesCNTTSV = new CitacionesCNTTTSV.Citaciones(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<CitacionesCNTTTSV></CitacionesCNTTTSV>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("CitacionesCNTTTSV");

        DataTable dtCitac = objCitacionesCNTTSV.CitacionesPorVehiculo(placa, chasis, camv);

        if (dtCitac.Rows.Count > 0)
        {
            
                for (int i = 0; i < dtCitac.Columns.Count; i++)
                {
                    XmlNode newProperty = xmlDoc.CreateNode(XmlNodeType.Element, dtCitac.Columns[i].ColumnName, null);
                    newProperty.InnerText = dtCitac.Rows[0].ToString();
                    rootNode.AppendChild(newProperty);
                }
        }
        else
        {
            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = objCitacionesCNTTSV.Error;
            rootNode.AppendChild(nodeError);
        }

        return xmlDoc.InnerXml;
    }


    [WebMethod]
    public string ConsultaBloqueosVehiculo(string placa, string chasis, string camv)
    {
        MatriculacionCNTTTSV.VehiculoCNTTTSV objVehiculoCNTTSV = new MatriculacionCNTTTSV.VehiculoCNTTTSV(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"],
            placa, chasis, camv);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<BloqueosVehiculoCNTTTSV></BloqueosVehiculoCNTTTSV>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("BloqueosVehiculoCNTTTSV");

        if (objVehiculoCNTTSV.LoadBloqueosVehiculo())
        {
            DataTable dtBloqVeh = objVehiculoCNTTSV.BloqueosVehiculo;

            if (dtBloqVeh.Rows.Count > 0)
            {
                foreach (DataRow row in dtBloqVeh.Rows)
                {
                    for (int i = 0; i < dtBloqVeh.Columns.Count; i++)
                    {
                        XmlNode newProperty = xmlDoc.CreateNode(XmlNodeType.Element, dtBloqVeh.Columns[i].ColumnName, null);
                        newProperty.InnerText = row[i].ToString();
                        rootNode.AppendChild(newProperty);
                    }
                }
            }
            else
            {
                XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
                nodeError.InnerText = "Vehículo no tiene bloqueos";
                rootNode.AppendChild(nodeError);
            }
        }
        else
        {
            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = objVehiculoCNTTSV.Error;
            rootNode.AppendChild(nodeError);
        }

        return xmlDoc.InnerXml;
    }


    [WebMethod]
    public string IngresaRevision(int codigo_revision, int numero_revision, string fecha_revision, string fecha_vigencia,
        string placa, string chasis, string ramv, string usuario_ingresa, string fecha_ingresa, string estado, 
        int cod_zona, int cod_agencia, int numero_certificado, int numero_adhesiva, string corpaire_user, string corpaire_password)
    {
        string retValue;
        Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        SeguridadWebAppInt.UsuarioWebAppInt oUser = new SeguridadWebAppInt.UsuarioWebAppInt(corpaire_user, objCrypto, System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);

        if (oUser.LogIn(corpaire_password))
        {

            MatriculacionCNTTTSV.RevisionCorpaire oRevision = new MatriculacionCNTTTSV.RevisionCorpaire(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);

            try
            {
                if (oRevision.IngresarRevision(codigo_revision, numero_revision, Convert.ToDateTime(fecha_revision), Convert.ToDateTime(fecha_vigencia), placa, chasis, ramv, usuario_ingresa, Convert.ToDateTime(fecha_ingresa),
                    Convert.ToChar(estado), cod_zona, cod_agencia, numero_certificado, numero_adhesiva))
                {
                    retValue = oRevision.CodigoErrorEvento;
                }
                else
                {
                    if (oRevision.CodigoErrorEvento == string.Empty)
                        retValue = oRevision.Error;
                    else
                        retValue = oRevision.CodigoErrorEvento;
                }
            }
            catch (FormatException ex1)
            {
                retValue = "El formato de fecha y/o estado es inválido. " + ex1.Message;
            }
            catch (ArgumentNullException ex2)
            {
                retValue = "El estado no puede ser nulo. " + ex2.Message;
            }
        }
        else
        {
            retValue = "Usuario y/o contraseña incorrecto.";
        }

        return retValue;
    }


    [WebMethod]
    public string ReversaRevision(int codigo_revision, int numero_revision, string usuario_reversa, string fecha_reversa,
        /*out string codigo_error,*/ string corpaire_user, string corpaire_password)
    {
        string retValue;
        Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        SeguridadWebAppInt.UsuarioWebAppInt oUser = new SeguridadWebAppInt.UsuarioWebAppInt(corpaire_user, objCrypto, System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);

        if (oUser.LogIn(corpaire_password))
        {
            MatriculacionCNTTTSV.RevisionCorpaire oRevision = new MatriculacionCNTTTSV.RevisionCorpaire(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);

            try
            {
                if (oRevision.ReversarRevision(codigo_revision, numero_revision, usuario_reversa, Convert.ToDateTime(fecha_reversa)))
                {
                    retValue = oRevision.CodigoErrorEvento;
                }
                else
                {
                    if (oRevision.CodigoErrorEvento == string.Empty)
                        retValue = oRevision.Error;
                    else
                        retValue = oRevision.CodigoErrorEvento;
                }
            }
            catch (FormatException ex)
            {
                retValue = "El formato de la fecha del reverso es inválido. " + ex.Message;
            }
        }
        else
        {
            retValue = "Usuario y/o contraseña incorrecto.";
        }
        
        return retValue;
    }


    [WebMethod]
    public string ModificaRevision(int codigo_revision, int numero_revision, int numero_certificado, int numero_adhesiva, 
        string placa, string dui, string usuario_modifica, string fecha_modifica, int cod_zona, int cod_agencia,
        string fecha_revision, string estado_revision, string chasis,
        string corpaire_user, string corpaire_password)
    {
        string retValue;

        Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        SeguridadWebAppInt.UsuarioWebAppInt oUser = new SeguridadWebAppInt.UsuarioWebAppInt(corpaire_user, objCrypto, System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);

        if (oUser.LogIn(corpaire_password))
        {
            MatriculacionCNTTTSV.RevisionCorpaire oRevision = new MatriculacionCNTTTSV.RevisionCorpaire(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);

            try
            {
                if (oRevision.ActualizarRevision(codigo_revision, numero_revision, numero_certificado, numero_adhesiva, placa, dui, usuario_modifica, Convert.ToDateTime(fecha_modifica), cod_zona, cod_agencia, Convert.ToDateTime(fecha_revision), estado_revision, chasis))
                {
                    retValue = oRevision.CodigoErrorEvento;
                }
                else
                {
                    if (oRevision.CodigoErrorEvento == string.Empty)
                        retValue = oRevision.Error;
                    else
                        retValue = oRevision.CodigoErrorEvento;
                }
            }
            catch (FormatException ex)
            {
                retValue = "El formato de la fecha de modificación es inválido. " + ex.Message;
            }
        }
        else
        {
            retValue = "Usuario y/o contraseña incorrecto.";
        }

        return retValue;
    }


    [WebMethod]
    public string ConsultaRevision(int codigo_revision, int numero_revision,
        /*out string codigo_error,*/ string corpaire_user, string corpaire_password)
    {
        string retValue;
        Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        SeguridadWebAppInt.UsuarioWebAppInt oUser = new SeguridadWebAppInt.UsuarioWebAppInt(corpaire_user, objCrypto, System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);

        if (oUser.LogIn(corpaire_password))
        {

            MatriculacionCNTTTSV.RevisionCorpaire oRevision = new MatriculacionCNTTTSV.RevisionCorpaire(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<RevisionCorpaire></RevisionCorpaire>");
            XmlNode rootNode = xmlDoc.SelectSingleNode("RevisionCorpaire");
            DataTable dtRevisionCorp = oRevision.ConsultaRevision(codigo_revision, numero_revision);

            if (dtRevisionCorp.Rows.Count > 0)
            {
                XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
                nodeError.InnerText = oRevision.CodigoErrorEvento;
                rootNode.AppendChild(nodeError);

                for (int i = 0; i < dtRevisionCorp.Columns.Count; i++)
                {
                    XmlNode newProperty = xmlDoc.CreateNode(XmlNodeType.Element, dtRevisionCorp.Columns[i].ColumnName, null);
                    newProperty.InnerText = dtRevisionCorp.Rows[0][i].ToString();
                    rootNode.AppendChild(newProperty);
                }
            }
            else
            {
                XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
                if (oRevision.CodigoErrorEvento == string.Empty)
                    nodeError.InnerText = oRevision.Error;
                else
                    nodeError.InnerText = oRevision.CodigoErrorEvento;
                rootNode.AppendChild(nodeError);
            }
            retValue = xmlDoc.InnerXml;
        }
        else
        {
            retValue = "Usuario y/o contraseña incorrecto.";
        }

        return retValue;
    }
}
