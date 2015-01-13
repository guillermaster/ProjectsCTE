using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Security;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Linq;

/// <summary>
/// Summary description for SrvMatriculacion
/// </summary>
[WebService(Namespace = "http://10.30.1.7/wsMatricVehQuito/")]
//[WebService(Namespace = "https://secure.ctg.gov.ec/wsMatricVehQuito/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class SrvMatriculacion : WebService
{

    public SrvMatriculacion()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    /*** ConsVehiculo ***/
    [WebMethod]
    public string QryVhc(string placa, string chasis, string camv)
    {
        MatriculacionCNTTTSV.VehiculoCNTTTSV objVehiculoCNTTSV = new MatriculacionCNTTTSV.VehiculoCNTTTSV(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"],
                                                                                                          placa.Trim(), chasis.Trim(), camv.Trim());

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<DatosVehiculoCNTTTSV></DatosVehiculoCNTTTSV>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("DatosVehiculoCNTTTSV");

        if (objVehiculoCNTTSV.LoadDatosVehiculo())
        {
            DataTable dtDatosVeh = objVehiculoCNTTSV.DatosVehiculo;

            for (int i = 0; i < dtDatosVeh.Columns.Count; i++)
            {
                XmlNode newProperty = xmlDoc.CreateNode(XmlNodeType.Element, dtDatosVeh.Columns[i].ColumnName, null);
                newProperty.InnerText = SecurityElement.Escape(dtDatosVeh.Rows[0][i].ToString());
                if (rootNode != null) rootNode.AppendChild(newProperty);
            }

            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = "0";
            rootNode.AppendChild(nodeError);

            XmlNode nodeMensaje = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
            nodeMensaje.InnerText = objVehiculoCNTTSV.Error;
            rootNode.AppendChild(nodeMensaje);
        }
        else
        {
            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = "1";
            rootNode.AppendChild(nodeError);

            XmlNode nodeMensaje = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
            nodeMensaje.InnerText = objVehiculoCNTTSV.Error;
            rootNode.AppendChild(nodeMensaje);
        }

        return xmlDoc.InnerXml;
    }

    /*** ConsultaBloqueosVehiculo ***/
    [WebMethod]
    public string QryBlqVhc(string placa, string chasis, string camv)
    {
        MatriculacionCNTTTSV.VehiculoCNTTTSV objVehiculoCNTTSV = new MatriculacionCNTTTSV.VehiculoCNTTTSV(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"],
            placa.Trim(), chasis.Trim(), camv.Trim());

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
                    XmlNode nodeBloqueo = xmlDoc.CreateNode(XmlNodeType.Element, "Bloqueo", null);
                    for (int i = 0; i < dtBloqVeh.Columns.Count; i++)
                    {
                        XmlNode newProperty = xmlDoc.CreateNode(XmlNodeType.Element, dtBloqVeh.Columns[i].ColumnName, null);
                        newProperty.InnerText = SecurityElement.Escape(row[i].ToString());
                        if (nodeBloqueo != null) nodeBloqueo.AppendChild(newProperty);
                    }
                    if (rootNode != null) rootNode.AppendChild(nodeBloqueo);
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


    /*** ConsCitaciones ***/
    [WebMethod]
    public string QryCtc(string placa, string chasis, string camv)
    {
        CitacionesCNTTTSV.Citaciones objCitacionesCNTTSV = new CitacionesCNTTTSV.Citaciones(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<CitacionesCNTTTSV></CitacionesCNTTTSV>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("CitacionesCNTTTSV");

        DataTable dtCitac = objCitacionesCNTTSV.CitacionesPorVehiculo(placa, chasis, camv);

        if (dtCitac.Rows.Count > 0)
        {
            foreach (DataRow row in dtCitac.Rows)
            {
                XmlNode nodeCitacion = xmlDoc.CreateNode(XmlNodeType.Element, "Citacion", null);
                for (int i = 0; i < dtCitac.Columns.Count; i++)
                {
                    XmlNode newProperty = xmlDoc.CreateNode(XmlNodeType.Element, dtCitac.Columns[i].ColumnName, null);
                    newProperty.InnerText = SecurityElement.Escape(row[i].ToString());
                    if (nodeCitacion != null) nodeCitacion.AppendChild(newProperty);
                }
                if (rootNode != null) rootNode.AppendChild(nodeCitacion);
            }
        }
        else
        {
            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            if (string.IsNullOrEmpty(objCitacionesCNTTSV.Error))
                nodeError.InnerText = "Vehículo no tiene citaciones pendientes de pago";
            else
                nodeError.InnerText = objCitacionesCNTTSV.Error;
            if (rootNode != null) rootNode.AppendChild(nodeError);
        }

        return xmlDoc.InnerXml;
    }

    /*** ConsContrCompraVenta ***/
    [WebMethod]
    public string QryCntCmpVnt(string placa_camv_cpn, string chasis)
    {
        MatriculacionCNTTTSV.VehiculoCNTTTSV objVehiculoCNTTSV = new MatriculacionCNTTTSV.VehiculoCNTTTSV(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"],
                                                                                                          placa_camv_cpn, chasis, placa_camv_cpn);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<ContratoCompraVentaCNTTTSV></ContratoCompraVentaCNTTTSV>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("ContratoCompraVentaCNTTTSV");

        if (objVehiculoCNTTSV.LoadDataContratoCompraVenta())
        {
            DataTable dtContrato = objVehiculoCNTTSV.ContratoCompraVenta;

            for (int j = 0; j < dtContrato.Rows.Count; j++)
            {
                for (int i = 0; i < dtContrato.Columns.Count; i++)
                {
                    XmlNode newProperty = xmlDoc.CreateNode(XmlNodeType.Element, dtContrato.Columns[i].ColumnName, null);
                    newProperty.InnerText = dtContrato.Rows[j][i].ToString();
                    if (rootNode != null) rootNode.AppendChild(newProperty);
                }
            }
        }
        else
        {
            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = objVehiculoCNTTSV.Error;
            if (rootNode != null) rootNode.AppendChild(nodeError);
        }

        return xmlDoc.InnerXml;
    }

    /*** ConsDatosPropietario ***/
    [WebMethod]
    public string QryDtsPrp(string placa_camv_cpn, string chasis)
    {
        MatriculacionCNTTTSV.VehiculoCNTTTSV objVehiculoCNTTSV = new MatriculacionCNTTTSV.VehiculoCNTTTSV(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"],
                                                                                                          placa_camv_cpn, chasis, placa_camv_cpn);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<DatosPropietarioVehCNTTTSV></DatosPropietarioVehCNTTTSV>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("DatosPropietarioVehCNTTTSV");

        if (objVehiculoCNTTSV.LoadDataPropietario())
        {
            DataTable dtPropietario = objVehiculoCNTTSV.Propietario;

            for (int i = 0; i < dtPropietario.Columns.Count; i++)
            {
                XmlNode newProperty = xmlDoc.CreateNode(XmlNodeType.Element, dtPropietario.Columns[i].ColumnName, null);
                newProperty.InnerText = dtPropietario.Rows[0][i].ToString();
                if (rootNode != null) rootNode.AppendChild(newProperty);
            }
        }
        else
        {
            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = objVehiculoCNTTSV.Error;
            if (rootNode != null) rootNode.AppendChild(nodeError);
        }

        return xmlDoc.InnerXml;
    }


    /*** ConsPagoMatricula ***/
    /*[WebMethod]
    public string QryPgMtr(string placa, string chasis, string camv)
    {
        MatriculacionCNTTTSV.MatriculaCNTTTSV oMatriculacion = new MatriculacionCNTTTSV.MatriculaCNTTTSV(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        string anioMat, valorPagadoMat;

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<UltimoPagoMatricula></UltimoPagoMatricula>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("UltimoPagoMatricula");

        if (oMatriculacion.UltimaMatricula(placa, chasis, camv, out anioMat, out valorPagadoMat))
        {
            XmlNode nodeAnio = xmlDoc.CreateNode(XmlNodeType.Element, "AnioMatricula", null);
            nodeAnio.InnerText = anioMat;
            rootNode.AppendChild(nodeAnio);
            XmlNode nodeValor = xmlDoc.CreateNode(XmlNodeType.Element, "ValorPagado", null);
            nodeValor.InnerText = valorPagadoMat;
            rootNode.AppendChild(nodeValor);
        }
        else
        {
            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "ValorPagado", null);
            nodeError.InnerText = oMatriculacion.Error;
            rootNode.AppendChild(nodeError);
        }

        return xmlDoc.InnerXml;
    }*/


    /*** ConsMatricula ***/
    [WebMethod]
    public string QryMtr(string placa, string chasis, string camv, string claseTipo, string ejes, string cantonCirc)
    {
        MatriculacionCNTTTSV.MatriculaCNTTTSV oMatriculacion = new MatriculacionCNTTTSV.MatriculaCNTTTSV(ConfigurationManager.AppSettings["usuarioConsMat"], ConfigurationManager.AppSettings["claveConsMat"], ConfigurationManager.AppSettings["baseConsMat"]);

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<DatosMatricula></DatosMatricula>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("DatosMatricula");

        if (oMatriculacion.LoadDatosMatricula2(placa.Trim(), chasis.Trim(), camv.Trim(), claseTipo, ejes, cantonCirc))
        {
            XmlNode nodePlaca = xmlDoc.CreateNode(XmlNodeType.Element, "Placa", null);
            nodePlaca.InnerText = oMatriculacion.DatosDeMatricula.Placa;
            rootNode.AppendChild(nodePlaca);

            XmlNode nodeChasis = xmlDoc.CreateNode(XmlNodeType.Element, "Chasis", null);
            nodeChasis.InnerText = oMatriculacion.DatosDeMatricula.Chasis;
            rootNode.AppendChild(nodeChasis);

            XmlNode nodeCAMV = xmlDoc.CreateNode(XmlNodeType.Element, "CAMV", null);
            nodeCAMV.InnerText = oMatriculacion.DatosDeMatricula.Camv;
            rootNode.AppendChild(nodeCAMV);

            XmlNode nodeValorMat = xmlDoc.CreateNode(XmlNodeType.Element, "ValorMatricula", null);
            nodeValorMat.InnerText = oMatriculacion.DatosDeMatricula.ValorMat;
            rootNode.AppendChild(nodeValorMat);

            XmlNode nodeAnioProd = xmlDoc.CreateNode(XmlNodeType.Element, "AnioProduccion", null);
            nodeAnioProd.InnerText = oMatriculacion.DatosDeMatricula.AnioProd;
            rootNode.AppendChild(nodeAnioProd);

            XmlNode nodeAsientos = xmlDoc.CreateNode(XmlNodeType.Element, "Asientos", null);
            nodeAsientos.InnerText = oMatriculacion.DatosDeMatricula.Asientos;
            rootNode.AppendChild(nodeAsientos);

            XmlNode nodeAvaluo = xmlDoc.CreateNode(XmlNodeType.Element, "Avaluo", null);
            nodeAvaluo.InnerText = oMatriculacion.DatosDeMatricula.Avaluo;
            rootNode.AppendChild(nodeAvaluo);

            XmlNode nodeCarroceria = xmlDoc.CreateNode(XmlNodeType.Element, "Carroceria", null);
            nodeCarroceria.InnerText = oMatriculacion.DatosDeMatricula.Carroceria;
            rootNode.AppendChild(nodeCarroceria);

            XmlNode nodeCilindraje = xmlDoc.CreateNode(XmlNodeType.Element, "Cilindraje", null);
            nodeCilindraje.InnerText = oMatriculacion.DatosDeMatricula.Cilindraje;
            rootNode.AppendChild(nodeCilindraje);

            XmlNode nodeClase = xmlDoc.CreateNode(XmlNodeType.Element, "Clase", null);
            nodeClase.InnerText = oMatriculacion.DatosDeMatricula.Clase;
            rootNode.AppendChild(nodeClase);

            XmlNode nodeTipo = xmlDoc.CreateNode(XmlNodeType.Element, "Tipo", null);
            nodeTipo.InnerText = oMatriculacion.DatosDeMatricula.Tipo;
            rootNode.AppendChild(nodeTipo);

            XmlNode nodeColor = xmlDoc.CreateNode(XmlNodeType.Element, "Color", null);
            nodeColor.InnerText = oMatriculacion.DatosDeMatricula.Color;
            rootNode.AppendChild(nodeColor);

            XmlNode nodeColor2 = xmlDoc.CreateNode(XmlNodeType.Element, "Color2", null);
            nodeColor2.InnerText = oMatriculacion.DatosDeMatricula.Color2;
            rootNode.AppendChild(nodeColor2);

            XmlNode nodeCombustible = xmlDoc.CreateNode(XmlNodeType.Element, "Combustible", null);
            nodeCombustible.InnerText = oMatriculacion.DatosDeMatricula.Combustible;
            rootNode.AppendChild(nodeCombustible);

            XmlNode nodeCooperativa = xmlDoc.CreateNode(XmlNodeType.Element, "Cooperativa", null);
            nodeCooperativa.InnerText = oMatriculacion.DatosDeMatricula.Cooperativa;
            rootNode.AppendChild(nodeCooperativa);

            XmlNode nodeDisco = xmlDoc.CreateNode(XmlNodeType.Element, "Disco", null);
            nodeDisco.InnerText = oMatriculacion.DatosDeMatricula.Disco;
            rootNode.AppendChild(nodeDisco);

            XmlNode nodeMarca = xmlDoc.CreateNode(XmlNodeType.Element, "Marca", null);
            nodeMarca.InnerText = oMatriculacion.DatosDeMatricula.Marca;
            rootNode.AppendChild(nodeMarca);

            XmlNode nodeModelo = xmlDoc.CreateNode(XmlNodeType.Element, "Modelo", null);
            nodeModelo.InnerText = oMatriculacion.DatosDeMatricula.Modelo;
            rootNode.AppendChild(nodeModelo);

            XmlNode nodeMotor = xmlDoc.CreateNode(XmlNodeType.Element, "Motor", null);
            nodeMotor.InnerText = oMatriculacion.DatosDeMatricula.Motor;
            rootNode.AppendChild(nodeMotor);

            XmlNode nodePaisOrigen = xmlDoc.CreateNode(XmlNodeType.Element, "PaisOrigen", null);
            nodePaisOrigen.InnerText = oMatriculacion.DatosDeMatricula.PaisOrigen;
            rootNode.AppendChild(nodePaisOrigen);

            XmlNode nodePlacaAnterior = xmlDoc.CreateNode(XmlNodeType.Element, "PlacaAnterior", null);
            nodePlacaAnterior.InnerText = oMatriculacion.DatosDeMatricula.PlacaAnt;
            rootNode.AppendChild(nodePlacaAnterior);

            XmlNode nodeServicio = xmlDoc.CreateNode(XmlNodeType.Element, "Servicio", null);
            nodeServicio.InnerText = oMatriculacion.DatosDeMatricula.Servicio;
            rootNode.AppendChild(nodeServicio);

            XmlNode nodeTonelaje = xmlDoc.CreateNode(XmlNodeType.Element, "Tonelaje", null);
            nodeTonelaje.InnerText = oMatriculacion.DatosDeMatricula.Tonelaje;
            rootNode.AppendChild(nodeTonelaje);

            XmlNode nodeMatAnterior = xmlDoc.CreateNode(XmlNodeType.Element, "MatriculaAnterior", null);
            nodeMatAnterior.InnerText = oMatriculacion.DatosDeMatricula.MatricAnterior;
            rootNode.AppendChild(nodeMatAnterior);

            XmlNode nodeGravamen = xmlDoc.CreateNode(XmlNodeType.Element, "Gravamen", null);
            nodeGravamen.InnerText = oMatriculacion.DatosDeMatricula.Gravamen;
            rootNode.AppendChild(nodeGravamen);

            XmlNode nodeCedula = xmlDoc.CreateNode(XmlNodeType.Element, "Cedula", null);
            nodeCedula.InnerText = oMatriculacion.DatosDeMatricula.Cedula;
            rootNode.AppendChild(nodeCedula);

            XmlNode nodePropietario = xmlDoc.CreateNode(XmlNodeType.Element, "Propietario", null);
            nodePropietario.InnerText = oMatriculacion.DatosDeMatricula.Propietario;
            rootNode.AppendChild(nodePropietario);

            XmlNode nodeDomicilio = xmlDoc.CreateNode(XmlNodeType.Element, "Domicilio", null);
            nodeDomicilio.InnerText = oMatriculacion.DatosDeMatricula.Domicilio;
            rootNode.AppendChild(nodeDomicilio);

            XmlNode nodeTelefono = xmlDoc.CreateNode(XmlNodeType.Element, "Telefono", null);
            nodeTelefono.InnerText = oMatriculacion.DatosDeMatricula.Telefono;
            rootNode.AppendChild(nodeTelefono);

            XmlNode nodeValEspecie = xmlDoc.CreateNode(XmlNodeType.Element, "ValorEspecieMat", null);
            nodeValEspecie.InnerText = oMatriculacion.DatosDeMatricula.ValorEspecie;
            rootNode.AppendChild(nodeValEspecie);

            XmlNode nodeValMatAnioAnt = xmlDoc.CreateNode(XmlNodeType.Element, "ValorMatriculaAnioAnterior", null);
            nodeValMatAnioAnt.InnerText = oMatriculacion.DatosDeMatricula.MatAnioAnterior1;
            rootNode.AppendChild(nodeValMatAnioAnt);

            XmlNode nodeRecargosAniosAnt = xmlDoc.CreateNode(XmlNodeType.Element, "RecargosAnteriores", null);
            nodeRecargosAniosAnt.InnerText = oMatriculacion.DatosDeMatricula.RecargoAnioAnt;
            rootNode.AppendChild(nodeRecargosAniosAnt);

            XmlNode nodeValPlacas = xmlDoc.CreateNode(XmlNodeType.Element, "Placas", null);
            nodeValPlacas.InnerText = oMatriculacion.DatosDeMatricula.Placas;
            rootNode.AppendChild(nodeValPlacas);

            XmlNode nodeOtrosVal1 = xmlDoc.CreateNode(XmlNodeType.Element, "OtroValor1", null);
            nodeOtrosVal1.InnerText = oMatriculacion.DatosDeMatricula.Otros1;
            rootNode.AppendChild(nodeOtrosVal1);

            XmlNode nodeOtrosVal2 = xmlDoc.CreateNode(XmlNodeType.Element, "OtroValor2", null);
            nodeOtrosVal2.InnerText = oMatriculacion.DatosDeMatricula.Otros2;
            rootNode.AppendChild(nodeOtrosVal2);

            XmlNode nodeOtrosVal3 = xmlDoc.CreateNode(XmlNodeType.Element, "OtroValor3", null);
            nodeOtrosVal3.InnerText = oMatriculacion.DatosDeMatricula.Otros3;
            rootNode.AppendChild(nodeOtrosVal3);

            XmlNode nodeTotalCNTTTSV = xmlDoc.CreateNode(XmlNodeType.Element, "TotalCNTTTSV", null);
            nodeTotalCNTTTSV.InnerText = oMatriculacion.DatosDeMatricula.TotalCntttsv;
            rootNode.AppendChild(nodeTotalCNTTTSV);

            XmlNode nodeNumCompSRI = xmlDoc.CreateNode(XmlNodeType.Element, "NumeroCompSRI", null);
            nodeNumCompSRI.InnerText = oMatriculacion.DatosDeMatricula.NumCompSRI;
            rootNode.AppendChild(nodeNumCompSRI);

            XmlNode nodeNumTraspasos = xmlDoc.CreateNode(XmlNodeType.Element, "NumTraspasos", null);
            nodeNumTraspasos.InnerText = oMatriculacion.DatosDeMatricula.NumTraspasos;
            rootNode.AppendChild(nodeNumTraspasos);

            XmlNode nodeCedPropietAnt = xmlDoc.CreateNode(XmlNodeType.Element, "CedPropietAnt", null);
            nodeCedPropietAnt.InnerText = oMatriculacion.DatosDeMatricula.CedulaPropAnt;
            rootNode.AppendChild(nodeCedPropietAnt);

            XmlNode nodeNomPropietAnt = xmlDoc.CreateNode(XmlNodeType.Element, "NomPropietAnt", null);
            nodeNomPropietAnt.InnerText = oMatriculacion.DatosDeMatricula.NombrePropAnt;
            rootNode.AppendChild(nodeNomPropietAnt);

            XmlNode nodeNumFacturaCom = xmlDoc.CreateNode(XmlNodeType.Element, "NumFacturaCom", null);
            nodeNumFacturaCom.InnerText = oMatriculacion.DatosDeMatricula.FacturaCom;
            rootNode.AppendChild(nodeNumFacturaCom);

            XmlNode nodeCasaComerc = xmlDoc.CreateNode(XmlNodeType.Element, "CasaComerc", null);
            nodeCasaComerc.InnerText = oMatriculacion.DatosDeMatricula.CasaCom;
            rootNode.AppendChild(nodeCasaComerc);

            XmlNode nodeCantonProp = xmlDoc.CreateNode(XmlNodeType.Element, "CantonProp", null);
            nodeCantonProp.InnerText = oMatriculacion.DatosDeMatricula.CantonProp;
            rootNode.AppendChild(nodeCantonProp);

            XmlNode nodeSaldo = xmlDoc.CreateNode(XmlNodeType.Element, "Saldo", null);
            nodeSaldo.InnerText = oMatriculacion.DatosDeMatricula.Saldo;
            rootNode.AppendChild(nodeSaldo);

            XmlNode nodeDescModelo = xmlDoc.CreateNode(XmlNodeType.Element, "DescModelo", null);
            nodeDescModelo.InnerText = oMatriculacion.DatosDeMatricula.DescModelo;
            rootNode.AppendChild(nodeDescModelo);

            XmlNode nodeDescMarca = xmlDoc.CreateNode(XmlNodeType.Element, "DescMarca", null);
            nodeDescMarca.InnerText = oMatriculacion.DatosDeMatricula.DescMarca;
            rootNode.AppendChild(nodeDescMarca);

            XmlNode nodeDescPais = xmlDoc.CreateNode(XmlNodeType.Element, "DescPais", null);
            nodeDescPais.InnerText = oMatriculacion.DatosDeMatricula.DescPais;
            rootNode.AppendChild(nodeDescPais);

            #region "Bloqueos"
            MatriculacionCNTTTSV.VehiculoCNTTTSV objVehiculoCNTTSV = new MatriculacionCNTTTSV.VehiculoCNTTTSV(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"],
                placa.Trim(), chasis.Trim(), camv.Trim());
            string valBloqueos = string.Empty;
            if (objVehiculoCNTTSV.LoadBloqueosVehiculo())
            {
                if (objVehiculoCNTTSV.BloqueosVehiculo.Rows.Count > 0)
                    valBloqueos = "SI";
                else
                    valBloqueos = "NO";
            }
            else
                valBloqueos = "E";
            XmlNode nodeBloqueos = xmlDoc.CreateNode(XmlNodeType.Element, "Bloqueos", null);
            nodeBloqueos.InnerText = valBloqueos;
            rootNode.AppendChild(nodeBloqueos);
            #endregion

            #region "Citaciones"
            CitacionesCNTTTSV.Citaciones objCitacionesCNTTSV = new CitacionesCNTTTSV.Citaciones(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            string valCitac = string.Empty;
            if (objCitacionesCNTTSV.CitacionesPorVehiculo(placa, chasis, camv).Rows.Count > 0)
                valCitac = "SI";
            else if (string.IsNullOrEmpty(objCitacionesCNTTSV.Error))
                valCitac = "NO";
            else
                valCitac = "E";
            XmlNode nodeCitac = xmlDoc.CreateNode(XmlNodeType.Element, "Citaciones", null);
            nodeCitac.InnerText = valCitac;
            rootNode.AppendChild(nodeCitac);
            #endregion


            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = oMatriculacion.DatosDeMatricula.HayError;
            rootNode.AppendChild(nodeError);

            XmlNode nodeMensaje = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
            nodeMensaje.InnerText = oMatriculacion.DatosDeMatricula.Mensaje;
            rootNode.AppendChild(nodeMensaje);
        }
        else
        {
            XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = "1";
            rootNode.AppendChild(nodeError);

            XmlNode nodeMensaje = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
            nodeMensaje.InnerText = oMatriculacion.Error;
            rootNode.AppendChild(nodeMensaje);
        }

        return xmlDoc.InnerXml;
    }


    /*** RegistrarPropietario ***/
    /*[WebMethod]
    public string RgsPrp(string identificacion, string tipoIdentificacion, string nombres, string fechaNacimiento, string codCantonNacimiento, string codProvinciaNacimiento,
        string codPaisNacimiento, string sexo, string tipoSangre, string telefono, string telefonoMovil, string direccion, string codCantonResidencia, string codProvinciaResidencia, string codPaisResidencia)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<RegistrarPropietarioVehiculoCNTTTSV></RegistrarPropietarioVehiculoCNTTTSV>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("RegistrarPropietarioVehiculoCNTTTSV");

        if (rootNode != null) rootNode.AppendChild(ErrorMessageNode(xmlDoc, "COOO", "No implementado"));

        return xmlDoc.InnerXml;
    }*/



    
    [WebMethod]
    public string RgsMtrAnl(string placa, string chasis, string identificacion, string direccion, string telefono, string codCanton, string numEspecieAnt, string numEspecieNva,
         string codBanco, string fechaPago, string valorPagado, string numDocumento, string codAgencia, string usuario, string observacion, string fechaCaducidad, string tipoCobro)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml("<Matricula></Matricula>");
        XmlNode rootNode = xmlDoc.SelectSingleNode("Matricula");

        MatriculacionCNTTTSV.MatriculaCNTTTSV oMatriculacion = new MatriculacionCNTTTSV.MatriculaCNTTTSV(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        string outPlaca, outChasis, especieAnt, especieNueva, tramite, error;

        if (oMatriculacion.RegistrarMatriculaNueva(placa, chasis, identificacion, direccion, telefono, codCanton, numEspecieAnt, numEspecieNva, codBanco, fechaPago,
            valorPagado, numDocumento, codAgencia, usuario, observacion, fechaCaducidad, tipoCobro, out especieAnt, out especieNueva, out tramite, out outChasis, out outPlaca, out error))
        {

            XmlNode nodeEspecieAnt = xmlDoc.CreateNode(XmlNodeType.Element, "EspecieAnt", null);
            nodeEspecieAnt.InnerText = especieAnt;
            rootNode.AppendChild(nodeEspecieAnt);
            XmlNode nodeEspecieNueva = xmlDoc.CreateNode(XmlNodeType.Element, "EspecieNueva", null);
            nodeEspecieNueva.InnerText = especieNueva;
            rootNode.AppendChild(nodeEspecieNueva);
            XmlNode nodeIdTramite = xmlDoc.CreateNode(XmlNodeType.Element, "IdTramite", null);
            nodeIdTramite.InnerText = tramite;
            rootNode.AppendChild(nodeIdTramite);
            XmlNode nodePlaca = xmlDoc.CreateNode(XmlNodeType.Element, "Placa", null);
            nodePlaca.InnerText = outPlaca;
            rootNode.AppendChild(nodePlaca);
            XmlNode nodeChasis = xmlDoc.CreateNode(XmlNodeType.Element, "Chasis", null);
            nodeChasis.InnerText = outChasis;
            rootNode.AppendChild(nodeChasis);
        }
        XmlNode nodeError = xmlDoc.CreateNode(XmlNodeType.Element, "Error", null);
        nodeError.InnerText = error;
        rootNode.AppendChild(nodeError);
        XmlNode nodeMsjError = xmlDoc.CreateNode(XmlNodeType.Element, "Mensaje", null);
        nodeMsjError.InnerText = oMatriculacion.Error;
        rootNode.AppendChild(nodeMsjError);

        return xmlDoc.InnerXml;
    }

    [WebMethod]
    public string ActEsp(int numEspecie, string chasis, string nvaEsp)
    {
        MatriculacionCNTTTSV.MatriculaCNTTTSV oMatric = new MatriculacionCNTTTSV.MatriculaCNTTTSV(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        string hayError = oMatric.ActualizarEspecie(numEspecie, chasis, nvaEsp) ? "0" : "1";
        return "<ActEsp><Error>" + hayError + "</Error><Mensaje>" + oMatric.Error + "</Mensaje></ActEsp>";
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

