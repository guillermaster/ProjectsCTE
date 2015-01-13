using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Data;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //[WebMethod]
    //public DataSet Test()
    //{
    //    Vehiculos.Vehiculo oVehiculo = new Vehiculos.Vehiculo(user, password, System.Configuration.ConfigurationManager.AppSettings["base"]);
    //    return oVehiculo.Test();
    //}

    public XmlDocument ErrorXML(string descError)
    {
        XmlDocument xmlError = new XmlDocument();
        //XmlElement xmlNodo = xmlError.CreateElement("Error");
        //xmlNodo.InnerText = descError;
        //xmlError.AppendChild(xmlNodo);
        string xmlContent = "<xmlds><Permisos><ErrorConexion>"+descError+"</ErrorConexion><Login>false</Login><Mensajes>"
            + "<Mensaje><Descripcion>" + descError + "</Descripcion><Validar>TRUE</Validar></Mensaje></Mensajes></Permisos></xmlds>";
        xmlError.LoadXml(xmlContent);
        return xmlError;
    }

    [WebMethod]
    public XmlDocument PermisosUsuarios(string xmlFileContent, string user, string password)
    {
        Seguridad.Seguridades oSeguridades = new Seguridad.Seguridades(user, password, System.Configuration.ConfigurationManager.AppSettings["base"]);
        string xmlText = oSeguridades.PermisosPorUsuario(xmlFileContent);
        XmlDocument xmlDoc = new XmlDocument();
        if (xmlText != null)
            xmlDoc.LoadXml(xmlText);
        else
            xmlDoc = ErrorXML(oSeguridades.Error);
        return xmlDoc;
    }


    [WebMethod]
    public XmlDocument CambiaClave(string xmlFileContent, string user, string password)
    {
        Seguridad.Seguridades oSeguridades = new Seguridad.Seguridades(user, password, System.Configuration.ConfigurationManager.AppSettings["base"]);
        string xmlText = oSeguridades.CambiaClave(xmlFileContent);
        XmlDocument xmlDoc = new XmlDocument();
        if (xmlText != null)
            xmlDoc.LoadXml(xmlText);
        else
            xmlDoc = ErrorXML(oSeguridades.Error);
        return xmlDoc;
    }


    [WebMethod]
    public XmlDocument ActualizarBase(string emei, string user, string password)
    {
        Seguridad.Seguridades oSeguridades = new Seguridad.Seguridades(user, password, System.Configuration.ConfigurationManager.AppSettings["base"]);
        string xmlText = oSeguridades.ActualizarBase(emei);
        XmlDocument xmlDoc = new XmlDocument();
        if (xmlText != null)
            xmlDoc.LoadXml(xmlText);
        else
            xmlDoc = ErrorXML(oSeguridades.Error);
        return xmlDoc;
    }


    [WebMethod]
    public string DatosVehiculo(string xmlRevision, string user, string password)
    {        
        Vehiculos.Vehiculo oVehiculo = new Vehiculos.Vehiculo(user, password, System.Configuration.ConfigurationManager.AppSettings["base"]);
        string xmlReturnContent = oVehiculo.DatosVehiculo(xmlRevision);
        //XmlDocument xmlDoc = new XmlDocument();
        //if (xmlReturnContent != null)
        //    xmlDoc.LoadXml(xmlReturnContent);
        //return xmlDoc;
        return xmlReturnContent;
    }

    [WebMethod]
    public XmlDocument ResultadoRevision(string xmlRevision, string user, string password)
    {
        Vehiculos.Vehiculo oVehiculo = new Vehiculos.Vehiculo(user, password, System.Configuration.ConfigurationManager.AppSettings["base"]);
        string xmlReturnContent = oVehiculo.ResultadoRevision(xmlRevision);
        XmlDocument xmlDoc = new XmlDocument();
        if (xmlReturnContent != null)
            xmlDoc.LoadXml(xmlReturnContent);
        else
            xmlDoc = ErrorXML(oVehiculo.Error);
        return xmlDoc;
    }


    [WebMethod]
    public XmlDocument TestDatosVehiculo(string xmlFileContent, string user, string password)
    {

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlFileContent);

        Vehiculos.Vehiculo oVehiculo = new Vehiculos.Vehiculo(user, password, System.Configuration.ConfigurationManager.AppSettings["base"]);
        string placa = xmlDoc.LastChild.LastChild.InnerText;
        DataTable dtDatosVehiculo = oVehiculo.DatosAvanzadosVehiculo(placa);

        XmlDocument resXML = new XmlDocument();
        XmlElement xmlroot = resXML.DocumentElement;

        XmlElement xmlAutomotor = resXML.CreateElement("Automotor");
        xmlAutomotor.SetAttribute("vehiculo", "ABC123");

        XmlElement xmlRevision = resXML.CreateElement("Revision");

        XmlElement xmlVehiculo = resXML.CreateElement("Vehiculo");

        for (int i = 0; i < dtDatosVehiculo.Columns.Count; i++)
        {
            xmlVehiculo.AppendChild(resXML.CreateElement(dtDatosVehiculo.Columns[i].ColumnName));
            xmlVehiculo.LastChild.AppendChild(resXML.CreateTextNode(dtDatosVehiculo.Rows[0][i].ToString()));
        }

        xmlRevision.AppendChild(xmlVehiculo);
        xmlAutomotor.AppendChild(xmlRevision);
        resXML.AppendChild(xmlAutomotor);

        
        //return resXML.InnerXml;
        return resXML;
    }


    [WebMethod]
    public XmlDocument DatosAutomotor(string xmlCriterioConsulta, string user, string password)
    {
        XmlDocument xmlInDoc = new XmlDocument();
        xmlInDoc.LoadXml(xmlCriterioConsulta);
        string placa = xmlInDoc.LastChild.LastChild.InnerText;


        XmlDocument xmlAutomotor = new XmlDocument();
        XmlElement xmlElem_Automotor = xmlAutomotor.CreateElement("Automotor");

        ////
        #region "Bloqueos"
        XmlElement xmlElem_Bloqueos = xmlAutomotor.CreateElement("Bloqueos");
        xmlElem_Automotor.AppendChild(xmlElem_Bloqueos);
        #endregion
        ////
        #region "Novedades"
        XmlElement xmlElem_Novedades = xmlAutomotor.CreateElement("Novedades");
        xmlElem_Automotor.AppendChild(xmlElem_Novedades);
        #endregion
        ////
        #region "Valores"
        XmlElement xmlElem_Valores = xmlAutomotor.CreateElement("Valores");
        xmlElem_Automotor.AppendChild(xmlElem_Valores);
        #endregion

        ////**************
        #region "Revision"
        XmlElement xmlElem_Revision = xmlAutomotor.CreateElement("Revision");
        //////

        #region "Propietario"
        XmlElement xmlElem_RevisionPropietario = xmlAutomotor.CreateElement("Propietario");
        // agregar datos de propietario aquí
        xmlElem_Revision.AppendChild(xmlElem_RevisionPropietario);
        #endregion
        ///////
        #region "Datos Vehículos (Vehiculos)"
        XmlElement xmlElem_RevisionVehiculos = xmlAutomotor.CreateElement("Vehiculos");
        // agregar datos de vehículo aquí
        Vehiculos.Vehiculo oVehiculo = new Vehiculos.Vehiculo(user, password, System.Configuration.ConfigurationManager.AppSettings["base"]);
        DataTable dtDatosVehiculo = oVehiculo.DatosAvanzadosVehiculo(placa);
        for (int i = 0; i < dtDatosVehiculo.Columns.Count; i++)
        {
            if (dtDatosVehiculo.Columns[i].ColumnName != "Mensaje")
            {
                XmlElement vehiculo = xmlAutomotor.CreateElement("vehiculo");
                XmlElement dato1 = xmlAutomotor.CreateElement("dato1");
                dato1.AppendChild(xmlAutomotor.CreateTextNode(dtDatosVehiculo.Columns[i].ColumnName));
                vehiculo.AppendChild(dato1);
                XmlElement dato2 = xmlAutomotor.CreateElement("dato2");
                if (dtDatosVehiculo.Rows.Count > 0)
                    dato2.AppendChild(xmlAutomotor.CreateTextNode(dtDatosVehiculo.Rows[0][i].ToString()));///
                vehiculo.AppendChild(dato2);
                XmlElement dato3 = xmlAutomotor.CreateElement("dato3");
                vehiculo.AppendChild(dato3);
                xmlElem_RevisionVehiculos.AppendChild(vehiculo);
            }
            else
            {
                XmlElement error = xmlAutomotor.CreateElement("Error");
                XmlElement dato1 = xmlAutomotor.CreateElement("dato1");
                dato1.AppendChild(xmlAutomotor.CreateTextNode(dtDatosVehiculo.Columns[i].ColumnName));
                error.AppendChild(dato1);
                XmlElement dato2 = xmlAutomotor.CreateElement("dato2");
                if (dtDatosVehiculo.Rows.Count > 0)
                    dato2.AppendChild(xmlAutomotor.CreateTextNode(dtDatosVehiculo.Rows[0][i].ToString()));///
                error.AppendChild(dato2);
                XmlElement dato3 = xmlAutomotor.CreateElement("dato3");
                error.AppendChild(dato3);
                xmlElem_RevisionVehiculos.AppendChild(error);
            }
        }
        xmlElem_Revision.AppendChild(xmlElem_RevisionVehiculos);
        #endregion
        //////
        #region "Características Ley"
        XmlElement xmlElem_RevisionCaracLey = xmlAutomotor.CreateElement("CaracteristicasLey");
        // agregar datos de vehículo aquí
        xmlElem_Revision.AppendChild(xmlElem_RevisionCaracLey);
        #endregion
        //////
        xmlElem_Automotor.AppendChild(xmlElem_Revision);
        #endregion
        ////**************

        xmlAutomotor.AppendChild(xmlElem_Automotor);
        return xmlAutomotor;
    }

}
