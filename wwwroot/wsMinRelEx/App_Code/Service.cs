using System;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Configuration;
using Brevetacion;

//[WebService(Namespace = "http://tempuri.org/")]
[WebService(Namespace = "http://10.30.1.7/wsMinRelEx/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Service : System.Web.Services.WebService
{
    public Service()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
/*
    [WebMethod]
    public string ConsultaDatosLicencia(string numLicencia) {
        Licencia oLicencia = new Licencia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataSet dsDatosLicencia = oLicencia.ConsultarDatosAdicionalesLicencia(numLicencia);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<DatosLicencia></DatosLicencia>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("DatosLicencia");

        foreach(DataTable table in dsDatosLicencia.Tables)
        {
            XmlNode grandfatherNode = xmlDoc.CreateNode(XmlNodeType.Element, table.TableName, null);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                XmlNode fatherNode = xmlDoc.CreateNode(XmlNodeType.Element, table.TableName + i.ToString(), null);
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    XmlNode newNode = xmlDoc.CreateNode(XmlNodeType.Element, table.Columns[j].ColumnName, null);
                    newNode.InnerText = table.Rows[i][j].ToString();
                    if (table.TableName == "DatosBasicos")
                        grandfatherNode.AppendChild(newNode);
                    else
                        fatherNode.AppendChild(newNode);
                }
            }
            rootNode.AppendChild(grandfatherNode);
        }

        return rootNode.InnerXml;
    }
    */
    [WebMethod]
    public string RegistraAlcoholimetro(string codPrueba, string codAlcohol, string codPersona, string licencia, string fechaPrueba,
                          string aprobado, string resultado, string observacion, string estado)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<Alcoholimetro></Alcoholimetro>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("Alcoholimetro");

        Alcoholimetros.ControlAlcoholimetro oAlcoholimetro = new Alcoholimetros.ControlAlcoholimetro(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        string mensaje, error;

        if (oAlcoholimetro.Registrar(codPrueba, codAlcohol, codPersona, licencia, fechaPrueba,
            aprobado, resultado, observacion, estado,
            out mensaje, out error))
        {
            if (error == "N")
            {
                /*XmlNode nodeMsjOk = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
                nodeMsjOk.InnerText = mensaje;
                rootNode.AppendChild(nodeMsjOk);*/
                XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
                nodeError.InnerText = error;
                rootNode.AppendChild(nodeError);
            }
            else
            {
                XmlNode nodeMsjOk = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
                nodeMsjOk.InnerText = mensaje;
                rootNode.AppendChild(nodeMsjOk);
                XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
                nodeError.InnerText = error;
                rootNode.AppendChild(nodeError);
            }
        }
        else
        {
            XmlNode nodeMsjError = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
            nodeMsjError.InnerText = oAlcoholimetro.Error;
            rootNode.AppendChild(nodeMsjError);
            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = error;
            rootNode.AppendChild(nodeError);
        }

        return xmlDoc.InnerXml;
    }

   
   
    [WebMethod]
    public string ConsultaAlcoholimetro(string licencia)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<Alcoholimetro></Alcoholimetro>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("Alcoholimetro");

        Alcoholimetros.ControlAlcoholimetro oAlcoholimetro = new Alcoholimetros.ControlAlcoholimetro(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        string codPersona, nombres, bloqueo, tipoLicencia, descBloqueo, fechaInicio, fechaCaducidad, tipoIdentificacion, estado, puntos, mensaje, error;

        if (oAlcoholimetro.Consulta(licencia, out codPersona, out nombres, out bloqueo, out tipoLicencia, out descBloqueo, out fechaInicio, out fechaCaducidad, out tipoIdentificacion, out estado, out puntos, 
            out mensaje, out error))
        {
            if (error == "N")
            {
                XmlNode nodePersona = xmlDoc.CreateNode(XmlNodeType.Element, "Persona", null);
                nodePersona.InnerText = codPersona;
                rootNode.AppendChild(nodePersona);
                XmlNode nodeNombres = xmlDoc.CreateNode(XmlNodeType.Element, "Nombres", null);
                nodeNombres.InnerText = nombres;
                rootNode.AppendChild(nodeNombres);
                XmlNode nodeBloqueo = xmlDoc.CreateNode(XmlNodeType.Element, "Bloqueo", null);
                nodeBloqueo.InnerText = bloqueo;
                rootNode.AppendChild(nodeBloqueo);
                XmlNode nodeTipoLicencia = xmlDoc.CreateNode(XmlNodeType.Element, "TipoLicencia", null);
                nodeTipoLicencia.InnerText = tipoLicencia;
                rootNode.AppendChild(nodeTipoLicencia);
                XmlNode nodeDescBloqueo = xmlDoc.CreateNode(XmlNodeType.Element, "DescripcionBloqueo", null);
                nodeDescBloqueo.InnerText = descBloqueo;
                rootNode.AppendChild(nodeDescBloqueo);
                XmlNode nodeFechaInicio = xmlDoc.CreateNode(XmlNodeType.Element, "FechaInicio", null);
                nodeFechaInicio.InnerText = fechaInicio;
                rootNode.AppendChild(nodeFechaInicio);
                XmlNode nodeFechaCaducidad = xmlDoc.CreateNode(XmlNodeType.Element, "FechaCaducidad", null);
                nodeFechaCaducidad.InnerText = fechaCaducidad;
                rootNode.AppendChild(nodeFechaCaducidad);
                XmlNode nodeTipoIdentificacion = xmlDoc.CreateNode(XmlNodeType.Element, "TipoIdentificacion", null);
                nodeTipoIdentificacion.InnerText = tipoIdentificacion;
                rootNode.AppendChild(nodeTipoIdentificacion);
                XmlNode nodeEstado = xmlDoc.CreateNode(XmlNodeType.Element, "Estado", null);
                nodeEstado.InnerText = estado;
                rootNode.AppendChild(nodeEstado);
                XmlNode nodePuntos = xmlDoc.CreateNode(XmlNodeType.Element, "Puntos", null);
                nodePuntos.InnerText = puntos;
                rootNode.AppendChild(nodePuntos);
                /*XmlNode nodeMsjOk = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
                nodeMsjOk.InnerText = mensaje;
                rootNode.AppendChild(nodeMsjOk);*/
                XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
                nodeError.InnerText = error;
                rootNode.AppendChild(nodeError);
            }
            else
            {
                XmlNode nodeMsjOk = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
                nodeMsjOk.InnerText = mensaje;
                rootNode.AppendChild(nodeMsjOk);
                XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
                nodeError.InnerText = error;
                rootNode.AppendChild(nodeError);
            }
        }
        else
        {
            XmlNode nodeMsjError = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
            nodeMsjError.InnerText = oAlcoholimetro.Error;
            rootNode.AppendChild(nodeMsjError);
            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = error;
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
