using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Security;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Linq;


/// <summary>
/// Summary description for WsAlch
/// </summary>
[WebService(Namespace = "http://10.30.1.7/wsAlch/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class WsAlch : System.Web.Services.WebService {

    public WsAlch () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public string RgsAlch(string codPrueba, string codAlcohol, string codPersona, string fechaPrueba,
                          string aprobado, string resultado, string observacion, string estado)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<Alcoholimetro></Alcoholimetro>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("Alcoholimetro");

        Alcoholimetros.ControlAlcoholimetro oAlcoholimetro = new Alcoholimetros.ControlAlcoholimetro(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        string mensaje, error;

        if (oAlcoholimetro.Registrar(codPrueba, codAlcohol, codPersona, fechaPrueba,
            aprobado, resultado, observacion, estado,
            out mensaje, out error))
        {

            XmlNode nodeMsjOk = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
            nodeMsjOk.InnerText = "Registro exitoso";
            rootNode.AppendChild(nodeMsjOk);
            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = error;
            rootNode.AppendChild(nodeError);
        }
        else
        {
            XmlNode nodeMsjError = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
            nodeMsjError.InnerText = oAlcoholimetro.Error;
            rootNode.AppendChild(nodeMsjError);
            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = "1";
            rootNode.AppendChild(nodeError);
        }

        return xmlDoc.InnerXml;
    }


    [WebMethod]
    public string QryAlch(string licencia)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<DatosAlcoholimetro></DatosAlcoholimetro>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("DatosAlcoholimetro");

        Alcoholimetros.ControlAlcoholimetro oAlcoholimetro = new Alcoholimetros.ControlAlcoholimetro(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        string codPersona, nombres, bloqueo, tipoLicencia, descBloqueo, fechaInicio, fechaCaducidad, tipoIdentificacion, estado, mensaje, hayError;


        if (oAlcoholimetro.Consulta(licencia, out codPersona, out nombres, out bloqueo, out tipoLicencia, out descBloqueo, out fechaInicio, out fechaCaducidad, out tipoIdentificacion, out estado, out mensaje, out hayError))
        {

            XmlNode nodeCodPersona = xmlDoc.CreateNode(XmlNodeType.Element, "CodPersona", null);
            nodeCodPersona.InnerText = codPersona;
            rootNode.AppendChild(nodeCodPersona);

            XmlNode nodeNombres = xmlDoc.CreateNode(XmlNodeType.Element, "Nombres", null);
            nodeNombres.InnerText = nombres;
            rootNode.AppendChild(nodeNombres);

            XmlNode nodeBloqueo = xmlDoc.CreateNode(XmlNodeType.Element, "Bloqueo", null);
            nodeBloqueo.InnerText = bloqueo;
            rootNode.AppendChild(nodeBloqueo);

            XmlNode nodeTipoLicencia = xmlDoc.CreateNode(XmlNodeType.Element, "TipoLicencia", null);
            nodeTipoLicencia.InnerText = tipoLicencia;
            rootNode.AppendChild(nodeTipoLicencia);

            XmlNode nodeDescBloqueo = xmlDoc.CreateNode(XmlNodeType.Element, "DescBloqueo", null);
            nodeDescBloqueo.InnerText = descBloqueo;
            rootNode.AppendChild(nodeDescBloqueo);

            XmlNode nodeFechaInicio = xmlDoc.CreateNode(XmlNodeType.Element, "fechaInicio", null);
            nodeFechaInicio.InnerText = fechaInicio;
            rootNode.AppendChild(nodeFechaInicio);

            XmlNode nodeFechaCaducidad = xmlDoc.CreateNode(XmlNodeType.Element, "fechaCaducidad", null);
            nodeFechaCaducidad.InnerText = fechaCaducidad;
            rootNode.AppendChild(nodeFechaCaducidad);

            XmlNode nodeTipoIdentificacion = xmlDoc.CreateNode(XmlNodeType.Element, "TipoIdentificacion", null);
            nodeTipoIdentificacion.InnerText = tipoIdentificacion;
            rootNode.AppendChild(nodeTipoIdentificacion);


            XmlNode nodeEstado = xmlDoc.CreateNode(XmlNodeType.Element, "Estado", null);
            nodeEstado.InnerText = estado;
            rootNode.AppendChild(nodeEstado);

        }
        else
        {
            XmlNode nodeMensaje = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
            nodeMensaje.InnerText = oAlcoholimetro.Error;
            rootNode.AppendChild(nodeMensaje);

            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = "1";
            rootNode.AppendChild(nodeError);


        }

        return xmlDoc.InnerXml;
    }

    private static XmlNode ErrorMessageNode(XmlDocument xmlDocument, string codMsg, string msg)
    {
        XmlNode nodeError = xmlDocument.CreateNode(XmlNodeType.Element, "Mensaje", null);
        XmlAttribute attribute = xmlDocument.CreateAttribute("codigo");
        attribute.Value = codMsg;
        if (nodeError.Attributes != null) nodeError.Attributes.Append(attribute);
        nodeError.InnerText = msg;
        return nodeError;
    }
    
}

