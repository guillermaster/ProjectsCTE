using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Data;

namespace wsSIS911
{
    /// <summary>
    /// Summary description for ControlTransito
    /// </summary>
    [WebService(Namespace = "https://secure.cte.gob.ec/wsSIS911/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ControlTransito : System.Web.Services.WebService
    {

        [WebMethod]
        public string ConsDatVeh(string placa, string user, string password)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<DatosVehiculo></DatosVehiculo>");
            XmlNode rootNode = xmlDoc.SelectSingleNode("DatosVehiculo");
            string errorValUser = string.Empty;

            if (ValidateUserAndPassword(user, password, out errorValUser))
            {
                try
                {
                    Matriculacion.MatriculacionVehicular oMatric = new Matriculacion.MatriculacionVehicular(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                        ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                        ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
                    string[] datosVehiculo = oMatric.DatosAvanzadosVehiculo(placa.ToUpper());

                    if (datosVehiculo.Length > 2 && rootNode != null)
                    {
                        XmlNode nodeDatosBasicos = xmlDoc.CreateNode(XmlNodeType.Element, "DatosBasicos", null);
                        #region Agregar Datos Basicos
                        XmlNode nodePlaca = xmlDoc.CreateNode(XmlNodeType.Element, "Placa", null);
                        nodePlaca.InnerText = placa;
                        nodeDatosBasicos.AppendChild(nodePlaca);
                        XmlNode nodeChasis = xmlDoc.CreateNode(XmlNodeType.Element, "Chasis", null);
                        nodeChasis.InnerText = datosVehiculo[0];
                        nodeDatosBasicos.AppendChild(nodeChasis);
                        XmlNode nodeCAMV = xmlDoc.CreateNode(XmlNodeType.Element, "CAMV", null);
                        nodeCAMV.InnerText = datosVehiculo[3];
                        nodeDatosBasicos.AppendChild(nodeCAMV);
                        XmlNode nodeAnioProd = xmlDoc.CreateNode(XmlNodeType.Element, "AnioProducc", null);
                        nodeAnioProd.InnerText = datosVehiculo[1];
                        nodeDatosBasicos.AppendChild(nodeAnioProd);
                        XmlNode nodePaisOrig = xmlDoc.CreateNode(XmlNodeType.Element, "PaisOrigen", null);
                        nodePaisOrig.InnerText = datosVehiculo[5];
                        nodeDatosBasicos.AppendChild(nodePaisOrig);
                        XmlNode nodeMotor = xmlDoc.CreateNode(XmlNodeType.Element, "Motor", null);
                        nodeMotor.InnerText = datosVehiculo[4];
                        nodeDatosBasicos.AppendChild(nodeMotor);
                        XmlNode nodeCilindraje = xmlDoc.CreateNode(XmlNodeType.Element, "Cilindraje", null);
                        nodeCilindraje.InnerText = datosVehiculo[2];
                        nodeDatosBasicos.AppendChild(nodeCilindraje);
                        XmlNode nodeMarca = xmlDoc.CreateNode(XmlNodeType.Element, "Marca", null);
                        nodeMarca.InnerText = datosVehiculo[14];
                        nodeDatosBasicos.AppendChild(nodeMarca);
                        XmlNode nodeModelo = xmlDoc.CreateNode(XmlNodeType.Element, "Modelo", null);
                        nodeModelo.InnerText = datosVehiculo[15];
                        nodeDatosBasicos.AppendChild(nodeModelo);
                        XmlNode nodeColor = xmlDoc.CreateNode(XmlNodeType.Element, "Color", null);
                        nodeColor.InnerText = datosVehiculo[16];
                        nodeDatosBasicos.AppendChild(nodeColor);
                        XmlNode nodeTipoServ = xmlDoc.CreateNode(XmlNodeType.Element, "TipoServic", null);
                        nodeTipoServ.InnerText = datosVehiculo[8];
                        nodeDatosBasicos.AppendChild(nodeTipoServ);
                        XmlNode nodeNumPasaj = xmlDoc.CreateNode(XmlNodeType.Element, "NumPasajer", null);
                        nodeNumPasaj.InnerText = datosVehiculo[9];
                        nodeDatosBasicos.AppendChild(nodeNumPasaj);
                        XmlNode nodeCoop = xmlDoc.CreateNode(XmlNodeType.Element, "Cooperativa", null);
                        nodeCoop.InnerText = datosVehiculo[11];
                        nodeDatosBasicos.AppendChild(nodeCoop);
                        XmlNode nodeClaseServ = xmlDoc.CreateNode(XmlNodeType.Element, "ClaseServic", null);
                        nodeClaseServ.InnerText = datosVehiculo[12];
                        nodeDatosBasicos.AppendChild(nodeClaseServ);
                        XmlNode nodeClase = xmlDoc.CreateNode(XmlNodeType.Element, "Clase", null);
                        nodeClase.InnerText = datosVehiculo[13];
                        nodeDatosBasicos.AppendChild(nodeClase);
                        #endregion
                        rootNode.AppendChild(nodeDatosBasicos);


                        XmlNode nodeHistMat = xmlDoc.CreateNode(XmlNodeType.Element, "HistMatriculacion", null);
                        #region Agregar Datos de Matriculacion
                        DataTable dtHistMat = oMatric.HistorialMatriculacion(placa);
                        foreach (DataRow dr in dtHistMat.Rows)
                        {
                            XmlNode nodeMatricula = xmlDoc.CreateNode(XmlNodeType.Element, "Matricula", null);
                            XmlNode nodeMatricAnio = xmlDoc.CreateNode(XmlNodeType.Element, "Anio", null);
                            nodeMatricAnio.InnerText = dr[0].ToString();
                            nodeMatricula.AppendChild(nodeMatricAnio);
                            XmlNode nodeMatriFecEmis = xmlDoc.CreateNode(XmlNodeType.Element, "FechaEmision", null);
                            nodeMatriFecEmis.InnerText = dr[1].ToString();
                            nodeMatricula.AppendChild(nodeMatriFecEmis);
                            XmlNode nodeMatriFecCaduc = xmlDoc.CreateNode(XmlNodeType.Element, "FechaCaducidad", null);
                            nodeMatriFecCaduc.InnerText = dr[2].ToString();
                            nodeMatricula.AppendChild(nodeMatriFecCaduc);
                            XmlNode nodeMatriTipo = xmlDoc.CreateNode(XmlNodeType.Element, "Tipo", null);
                            nodeMatriTipo.InnerText = dr[3].ToString();
                            nodeMatricula.AppendChild(nodeMatriTipo);
                            nodeHistMat.AppendChild(nodeMatricula);
                        }
                        #endregion
                        rootNode.AppendChild(nodeHistMat);

                        XmlNode nodeBloqueos = xmlDoc.CreateNode(XmlNodeType.Element, "Bloqueos", null);
                        #region Agregar Datos de Bloqueos
                        DataTable dtBloqueos = oMatric.BloqueosPorVehiculo(placa);
                        foreach (DataRow dr in dtBloqueos.Rows)
                        {
                            XmlNode nodeBloqueo = xmlDoc.CreateNode(XmlNodeType.Element, "Bloqueo", null);
                            XmlNode nodeFecIng = xmlDoc.CreateNode(XmlNodeType.Element, "FechaIngreso", null);
                            nodeFecIng.InnerText = dr[0].ToString();
                            nodeBloqueo.AppendChild(nodeFecIng);
                            XmlNode nodeDescr = xmlDoc.CreateNode(XmlNodeType.Element, "Descripcion", null);
                            nodeDescr.InnerText = dr[1].ToString();
                            nodeBloqueo.AppendChild(nodeDescr);
                            nodeBloqueos.AppendChild(nodeBloqueo);
                        }
                        #endregion
                        rootNode.AppendChild(nodeBloqueos);
                    }
                    else
                    {
                        rootNode.AppendChild(ErrorNode(xmlDoc, datosVehiculo[1]));
                    }
                }
                catch (Exception ex)
                {
                    rootNode.AppendChild(ErrorNode(xmlDoc, ex.Message));
                }
            }
            else
            {
                rootNode.AppendChild(ErrorNode(xmlDoc, "Login Error, Please contact gpincay@cte.gob.ec - " + errorValUser));
            }

            return xmlDoc.InnerXml;
        }


        [WebMethod]
        public string ConsDatPropVeh(string placa, string user, string password)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<DatosPersona></DatosPersona>");
            XmlNode rootNode = xmlDoc.SelectSingleNode("DatosPersona");

            string errorValUser = string.Empty;

            if (ValidateUserAndPassword(user, password, out errorValUser))
            {
                try
                {
                    Brevetacion.Licencia oLicencia = new Brevetacion.Licencia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                        ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                        ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
                    Matriculacion.MatriculacionVehicular oMatric = new Matriculacion.MatriculacionVehicular(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                        ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                        ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
                    string numIdentificacion = oMatric.GetCedulaPropietario(placa.ToUpper());
                    object[] oDatos = oLicencia.ConsultarLicencia(numIdentificacion);

                    if (oDatos[0].ToString() != "N")
                    {
                        XmlNode nodeDatosBasicos = xmlDoc.CreateNode(XmlNodeType.Element, "DatosBasicos", null);
                        #region Datos Basicos
                        XmlNode nodeIdent = xmlDoc.CreateNode(XmlNodeType.Element, "Identificacion", null);
                        nodeIdent.InnerText = numIdentificacion;
                        nodeDatosBasicos.AppendChild(nodeIdent);
                        XmlNode nodeTipoIdent = xmlDoc.CreateNode(XmlNodeType.Element, "TipoIdentificacion", null);
                        nodeTipoIdent.InnerText = oDatos[2].ToString();
                        nodeDatosBasicos.AppendChild(nodeTipoIdent);
                        XmlNode nodeNombre = xmlDoc.CreateNode(XmlNodeType.Element, "Nombre", null);
                        nodeNombre.InnerText = oDatos[1].ToString();
                        nodeDatosBasicos.AppendChild(nodeNombre);
                        XmlNode nodeFecNac = xmlDoc.CreateNode(XmlNodeType.Element, "FechaNacimiento", null);
                        nodeFecNac.InnerText = oDatos[3].ToString();
                        nodeDatosBasicos.AppendChild(nodeFecNac);
                        XmlNode nodeLugarNac = xmlDoc.CreateNode(XmlNodeType.Element, "LugarNacimiento", null);
                        nodeLugarNac.InnerText = oDatos[4].ToString() + " - " + oDatos[5].ToString() + " - " + oDatos[6].ToString();
                        nodeDatosBasicos.AppendChild(nodeLugarNac);
                        XmlNode nodeSexo = xmlDoc.CreateNode(XmlNodeType.Element, "Sexo", null);
                        nodeSexo.InnerText = oDatos[7].ToString();
                        nodeDatosBasicos.AppendChild(nodeSexo);

                        XmlNode nodeSangre = xmlDoc.CreateNode(XmlNodeType.Element, "TipoSangre", null);
                        nodeSangre.InnerText = oDatos[8].ToString();
                        nodeDatosBasicos.AppendChild(nodeSangre);
                        XmlNode nodeCabello = xmlDoc.CreateNode(XmlNodeType.Element, "ColorCabello", null);
                        nodeCabello.InnerText = oDatos[9].ToString();
                        nodeDatosBasicos.AppendChild(nodeCabello);
                        XmlNode nodeOjo = xmlDoc.CreateNode(XmlNodeType.Element, "ColorOjo", null);
                        nodeOjo.InnerText = oDatos[10].ToString();
                        nodeDatosBasicos.AppendChild(nodeOjo);
                        XmlNode nodePiel = xmlDoc.CreateNode(XmlNodeType.Element, "ColorPiel", null);
                        nodePiel.InnerText = oDatos[11].ToString();
                        nodeDatosBasicos.AppendChild(nodePiel);

                        XmlNode nodeEstatura = xmlDoc.CreateNode(XmlNodeType.Element, "Estatura", null);
                        nodeEstatura.InnerText = oDatos[12].ToString();
                        nodeDatosBasicos.AppendChild(nodeEstatura);
                        XmlNode nodeEstCivil = xmlDoc.CreateNode(XmlNodeType.Element, "EstadoCivil", null);
                        nodeEstCivil.InnerText = oDatos[13].ToString();
                        nodeDatosBasicos.AppendChild(nodeEstCivil);
                        XmlNode nodeProf = xmlDoc.CreateNode(XmlNodeType.Element, "Profesion", null);
                        nodeProf.InnerText = oDatos[14].ToString();
                        nodeDatosBasicos.AppendChild(nodeProf);
                        XmlNode nodeResidencia = xmlDoc.CreateNode(XmlNodeType.Element, "LugarResidencia", null);
                        nodeResidencia.InnerText = oDatos[17].ToString() + " - " + oDatos[18].ToString() + " - " + oDatos[19].ToString();
                        nodeDatosBasicos.AppendChild(nodeResidencia);
                        XmlNode nodeDireccion = xmlDoc.CreateNode(XmlNodeType.Element, "DireccionResidencia", null);
                        nodeDireccion.InnerText = oDatos[15].ToString();
                        nodeDatosBasicos.AppendChild(nodeDireccion);
                        XmlNode nodeTelefono = xmlDoc.CreateNode(XmlNodeType.Element, "TelefonoResidencia", null);
                        nodeTelefono.InnerText = oDatos[16].ToString();
                        nodeDatosBasicos.AppendChild(nodeTelefono);
                        #endregion
                        rootNode.AppendChild(nodeDatosBasicos);


                        DataRow oRegistros;
                        DataTable _oTabLic = new DataTable();
                        _oTabLic.Columns.Add("Categoria");
                        _oTabLic.Columns.Add("FechaEmision");
                        _oTabLic.Columns.Add("FechaCaducidad");

                        DataTable _oTabRes = new DataTable();
                        _oTabRes.Columns.Add("FechaRestriccion");
                        _oTabRes.Columns.Add("Descripcion");

                        DataTable _oTabBloq = new DataTable();
                        _oTabBloq.Columns.Add("FechaBloqueo");
                        _oTabBloq.Columns.Add("Descripcion");

                        DataTable _oTabInfr = new DataTable();
                        _oTabInfr.Columns.Add("FechaInfracc");
                        _oTabInfr.Columns.Add("Descripcion");



                        Int64 numElem = oDatos.GetLength(0);

                        #region Llenar Tablas
                        for (Int64 iIndice = 21; iIndice < numElem; iIndice++)
                        {
                            switch (((object[])oDatos[iIndice])[0].ToString())
                            {
                                case "LIC":
                                    oRegistros = _oTabLic.NewRow();
                                    oRegistros[0] = (((object[])oDatos[iIndice])[1].ToString());
                                    oRegistros[1] = Convert.ToDateTime((((object[])oDatos[iIndice])[2].ToString()));
                                    oRegistros[2] = Convert.ToDateTime((((object[])oDatos[iIndice])[3].ToString()));
                                    _oTabLic.Rows.Add(oRegistros);
                                    break;
                                case "BLO":
                                    oRegistros = _oTabBloq.NewRow();
                                    oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                                    oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                                    _oTabBloq.Rows.Add(oRegistros);
                                    break;
                                case "RES":
                                    oRegistros = _oTabRes.NewRow();
                                    oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                                    oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                                    _oTabRes.Rows.Add(oRegistros);
                                    break;
                                case "INF":
                                    oRegistros = _oTabInfr.NewRow();
                                    oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                                    oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                                    _oTabInfr.Rows.Add(oRegistros);
                                    break;
                            }
                        }



                        #endregion

                        XmlNode nodeHistLic = xmlDoc.CreateNode(XmlNodeType.Element, "HistLicencias", null);

                        foreach (DataRow dr in _oTabLic.Rows)
                        {
                            XmlNode nodeLic = xmlDoc.CreateNode(XmlNodeType.Element, "Licencia", null);
                            XmlNode nodeLicCat = xmlDoc.CreateNode(XmlNodeType.Element, "Categoria", null);
                            nodeLicCat.InnerText = dr[0].ToString();
                            nodeLic.AppendChild(nodeLicCat);
                            XmlNode nodeLicFecEmi = xmlDoc.CreateNode(XmlNodeType.Element, "FechaEmision", null);
                            nodeLicFecEmi.InnerText = dr[1].ToString();
                            nodeLic.AppendChild(nodeLicFecEmi);
                            XmlNode nodeLicFecCad = xmlDoc.CreateNode(XmlNodeType.Element, "FechaCaducidad", null);
                            nodeLicFecCad.InnerText = dr[2].ToString();
                            nodeLic.AppendChild(nodeLicFecCad);
                            nodeHistLic.AppendChild(nodeLic);
                        }
                        rootNode.AppendChild(nodeHistLic);


                        XmlNode nodeRestricciones = xmlDoc.CreateNode(XmlNodeType.Element, "Restricciones", null);

                        foreach (DataRow dr in _oTabRes.Rows)
                        {
                            XmlNode nodeRestricc = xmlDoc.CreateNode(XmlNodeType.Element, "Restriccion", null);
                            XmlNode nodeRestFec = xmlDoc.CreateNode(XmlNodeType.Element, "FechaRestriccion", null);
                            nodeRestFec.InnerText = dr[0].ToString();
                            nodeRestricc.AppendChild(nodeRestFec);
                            XmlNode nodeRestDesc = xmlDoc.CreateNode(XmlNodeType.Element, "Descripcion", null);
                            nodeRestDesc.InnerText = dr[1].ToString();
                            nodeRestricc.AppendChild(nodeRestDesc);
                            nodeRestricciones.AppendChild(nodeRestricc);
                        }
                        rootNode.AppendChild(nodeRestricciones);


                        XmlNode nodeBloqueos = xmlDoc.CreateNode(XmlNodeType.Element, "Bloqueos", null);

                        foreach (DataRow dr in _oTabBloq.Rows)
                        {
                            XmlNode nodeBloqueo = xmlDoc.CreateNode(XmlNodeType.Element, "Bloqueo", null);
                            XmlNode nodeBloqFec = xmlDoc.CreateNode(XmlNodeType.Element, "FechaBloqueo", null);
                            nodeBloqFec.InnerText = dr[0].ToString();
                            nodeBloqueo.AppendChild(nodeBloqFec);
                            XmlNode nodeBloqDesc = xmlDoc.CreateNode(XmlNodeType.Element, "Descripcion", null);
                            nodeBloqDesc.InnerText = dr[1].ToString();
                            nodeBloqueo.AppendChild(nodeBloqDesc);
                            nodeBloqueos.AppendChild(nodeBloqueo);
                        }
                        rootNode.AppendChild(nodeBloqueos);


                        XmlNode nodeInfGraves = xmlDoc.CreateNode(XmlNodeType.Element, "InfraccGraves", null);

                        foreach (DataRow dr in _oTabInfr.Rows)
                        {
                            XmlNode nodeInfGrave = xmlDoc.CreateNode(XmlNodeType.Element, "InfraccGrave", null);
                            XmlNode nodeInfFec = xmlDoc.CreateNode(XmlNodeType.Element, "FechaInfracc", null);
                            nodeInfFec.InnerText = dr[0].ToString();
                            nodeInfGrave.AppendChild(nodeInfFec);
                            XmlNode nodeInfDesc = xmlDoc.CreateNode(XmlNodeType.Element, "Descripcion", null);
                            nodeInfDesc.InnerText = dr[1].ToString();
                            nodeInfGrave.AppendChild(nodeInfDesc);
                            nodeInfGraves.AppendChild(nodeInfGrave);
                        }
                        rootNode.AppendChild(nodeInfGraves);

                    }
                    else
                    {
                        rootNode.AppendChild(ErrorNode(xmlDoc, oDatos[1].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    rootNode.AppendChild(ErrorNode(xmlDoc, ex.Message));
                }
            }
            else
            {
                rootNode.AppendChild(ErrorNode(xmlDoc, "Login Error, Please contact gpincay@cte.gob.ec - " + errorValUser));
            }

            return xmlDoc.InnerXml;
        }

        [WebMethod]
        public string ConsDatPersona(string identificacion, string user, string password)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<DatosPersona></DatosPersona>");
            XmlNode rootNode = xmlDoc.SelectSingleNode("DatosPersona");

            string errorValUser = string.Empty;

            if (ValidateUserAndPassword(user, password, out errorValUser))
            {
                try
                {
                    Brevetacion.Licencia oLicencia = new Brevetacion.Licencia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                        ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                        ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
                    object[] oDatos = oLicencia.ConsultarLicencia(identificacion);

                    if (oDatos[0].ToString() != "N")
                    {
                        XmlNode nodeDatosBasicos = xmlDoc.CreateNode(XmlNodeType.Element, "DatosBasicos", null);
                        #region Datos Basicos
                        XmlNode nodeIdent = xmlDoc.CreateNode(XmlNodeType.Element, "Identificacion", null);
                        nodeIdent.InnerText = identificacion;
                        nodeDatosBasicos.AppendChild(nodeIdent);
                        XmlNode nodeTipoIdent = xmlDoc.CreateNode(XmlNodeType.Element, "TipoIdentificacion", null);
                        nodeTipoIdent.InnerText = oDatos[2].ToString();
                        nodeDatosBasicos.AppendChild(nodeTipoIdent);
                        XmlNode nodeNombre = xmlDoc.CreateNode(XmlNodeType.Element, "Nombre", null);
                        nodeNombre.InnerText = oDatos[1].ToString();
                        nodeDatosBasicos.AppendChild(nodeNombre);
                        XmlNode nodeFecNac = xmlDoc.CreateNode(XmlNodeType.Element, "FechaNacimiento", null);
                        nodeFecNac.InnerText = oDatos[3].ToString();
                        nodeDatosBasicos.AppendChild(nodeFecNac);
                        XmlNode nodeLugarNac = xmlDoc.CreateNode(XmlNodeType.Element, "LugarNacimiento", null);
                        nodeLugarNac.InnerText = oDatos[4].ToString() + " - " + oDatos[5].ToString() + " - " + oDatos[6].ToString();
                        nodeDatosBasicos.AppendChild(nodeLugarNac);
                        XmlNode nodeSexo = xmlDoc.CreateNode(XmlNodeType.Element, "Sexo", null);
                        nodeSexo.InnerText = oDatos[7].ToString();
                        nodeDatosBasicos.AppendChild(nodeSexo);
                        XmlNode nodeEstatura = xmlDoc.CreateNode(XmlNodeType.Element, "Estatura", null);
                        nodeEstatura.InnerText = oDatos[12].ToString();
                        nodeDatosBasicos.AppendChild(nodeEstatura);
                        XmlNode nodeEstCivil = xmlDoc.CreateNode(XmlNodeType.Element, "EstadoCivil", null);
                        nodeEstCivil.InnerText = oDatos[13].ToString();
                        nodeDatosBasicos.AppendChild(nodeEstCivil);
                        XmlNode nodeProf = xmlDoc.CreateNode(XmlNodeType.Element, "Profesion", null);
                        nodeProf.InnerText = oDatos[14].ToString();
                        nodeDatosBasicos.AppendChild(nodeProf);
                        XmlNode nodeResidencia = xmlDoc.CreateNode(XmlNodeType.Element, "LugarResidencia", null);
                        nodeResidencia.InnerText = oDatos[17].ToString() + " - " + oDatos[18].ToString() + " - " + oDatos[19].ToString();
                        nodeDatosBasicos.AppendChild(nodeResidencia);
                        XmlNode nodeDireccion = xmlDoc.CreateNode(XmlNodeType.Element, "DireccionResidencia", null);
                        nodeDireccion.InnerText = oDatos[15].ToString();
                        nodeDatosBasicos.AppendChild(nodeDireccion);
                        XmlNode nodeTelefono = xmlDoc.CreateNode(XmlNodeType.Element, "TelefonoResidencia", null);
                        nodeTelefono.InnerText = oDatos[16].ToString();
                        nodeDatosBasicos.AppendChild(nodeTelefono);
                        #endregion
                        rootNode.AppendChild(nodeDatosBasicos);

                        DataRow oRegistros;
                        DataTable _oTabLic = new DataTable();
                        _oTabLic.Columns.Add("Categoria");
                        _oTabLic.Columns.Add("FechaEmision");
                        _oTabLic.Columns.Add("FechaCaducidad");

                        DataTable _oTabRes = new DataTable();
                        _oTabRes.Columns.Add("FechaRestriccion");
                        _oTabRes.Columns.Add("Descripcion");

                        DataTable _oTabBloq = new DataTable();
                        _oTabBloq.Columns.Add("FechaBloqueo");
                        _oTabBloq.Columns.Add("Descripcion");

                        DataTable _oTabInfr = new DataTable();
                        _oTabInfr.Columns.Add("FechaInfracc");
                        _oTabInfr.Columns.Add("Descripcion");



                        Int64 numElem = oDatos.GetLength(0);

                        #region Llenar Tablas
                        for (Int64 iIndice = 21; iIndice < numElem; iIndice++)
                        {
                            switch (((object[])oDatos[iIndice])[0].ToString())
                            {
                                case "LIC":
                                    oRegistros = _oTabLic.NewRow();
                                    oRegistros[0] = (((object[])oDatos[iIndice])[1].ToString());
                                    oRegistros[1] = Convert.ToDateTime((((object[])oDatos[iIndice])[2].ToString()));
                                    oRegistros[2] = Convert.ToDateTime((((object[])oDatos[iIndice])[3].ToString()));
                                    _oTabLic.Rows.Add(oRegistros);
                                    break;
                                case "BLO":
                                    oRegistros = _oTabBloq.NewRow();
                                    oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                                    oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                                    _oTabBloq.Rows.Add(oRegistros);
                                    break;
                                case "RES":
                                    oRegistros = _oTabRes.NewRow();
                                    oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                                    oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                                    _oTabRes.Rows.Add(oRegistros);
                                    break;
                                case "INF":
                                    oRegistros = _oTabInfr.NewRow();
                                    oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                                    oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                                    _oTabInfr.Rows.Add(oRegistros);
                                    break;
                            }
                        }



                        #endregion

                        XmlNode nodeHistLic = xmlDoc.CreateNode(XmlNodeType.Element, "HistLicencias", null);
                        #region Crear XML de Historia de Licencias
                        foreach (DataRow dr in _oTabLic.Rows)
                        {
                            XmlNode nodeLic = xmlDoc.CreateNode(XmlNodeType.Element, "Licencia", null);
                            XmlNode nodeLicCat = xmlDoc.CreateNode(XmlNodeType.Element, "Categoria", null);
                            nodeLicCat.InnerText = dr[0].ToString();
                            nodeLic.AppendChild(nodeLicCat);
                            XmlNode nodeLicFecEmi = xmlDoc.CreateNode(XmlNodeType.Element, "FechaEmision", null);
                            nodeLicFecEmi.InnerText = dr[1].ToString();
                            nodeLic.AppendChild(nodeLicFecEmi);
                            XmlNode nodeLicFecCad = xmlDoc.CreateNode(XmlNodeType.Element, "FechaCaducidad", null);
                            nodeLicFecCad.InnerText = dr[2].ToString();
                            nodeLic.AppendChild(nodeLicFecCad);
                            nodeHistLic.AppendChild(nodeLic);
                        }
                        rootNode.AppendChild(nodeHistLic);
                        #endregion

                        XmlNode nodeRestricciones = xmlDoc.CreateNode(XmlNodeType.Element, "Restricciones", null);
                        #region Crear XML de Restricciones
                        foreach (DataRow dr in _oTabRes.Rows)
                        {
                            XmlNode nodeRestricc = xmlDoc.CreateNode(XmlNodeType.Element, "Restriccion", null);
                            XmlNode nodeRestFec = xmlDoc.CreateNode(XmlNodeType.Element, "FechaRestriccion", null);
                            nodeRestFec.InnerText = dr[0].ToString();
                            nodeRestricc.AppendChild(nodeRestFec);
                            XmlNode nodeRestDesc = xmlDoc.CreateNode(XmlNodeType.Element, "Descripcion", null);
                            nodeRestDesc.InnerText = dr[1].ToString();
                            nodeRestricc.AppendChild(nodeRestDesc);
                            nodeRestricciones.AppendChild(nodeRestricc);
                        }
                        rootNode.AppendChild(nodeRestricciones);
                        #endregion

                        XmlNode nodeBloqueos = xmlDoc.CreateNode(XmlNodeType.Element, "Bloqueos", null);
                        #region Crear XML de Bloqueos
                        foreach (DataRow dr in _oTabBloq.Rows)
                        {
                            XmlNode nodeBloqueo = xmlDoc.CreateNode(XmlNodeType.Element, "Bloqueo", null);
                            XmlNode nodeBloqFec = xmlDoc.CreateNode(XmlNodeType.Element, "FechaBloqueo", null);
                            nodeBloqFec.InnerText = dr[0].ToString();
                            nodeBloqueo.AppendChild(nodeBloqFec);
                            XmlNode nodeBloqDesc = xmlDoc.CreateNode(XmlNodeType.Element, "Descripcion", null);
                            nodeBloqDesc.InnerText = dr[1].ToString();
                            nodeBloqueo.AppendChild(nodeBloqDesc);
                            nodeBloqueos.AppendChild(nodeBloqueo);
                        }
                        rootNode.AppendChild(nodeBloqueos);
                        #endregion

                        XmlNode nodeInfGraves = xmlDoc.CreateNode(XmlNodeType.Element, "InfraccGraves", null);
                        #region Crear XML de Infracciones Graves
                        foreach (DataRow dr in _oTabInfr.Rows)
                        {
                            XmlNode nodeInfGrave = xmlDoc.CreateNode(XmlNodeType.Element, "InfraccGrave", null);
                            XmlNode nodeInfFec = xmlDoc.CreateNode(XmlNodeType.Element, "FechaInfracc", null);
                            nodeInfFec.InnerText = dr[0].ToString();
                            nodeInfGrave.AppendChild(nodeInfFec);
                            XmlNode nodeInfDesc = xmlDoc.CreateNode(XmlNodeType.Element, "Descripcion", null);
                            nodeInfDesc.InnerText = dr[1].ToString();
                            nodeInfGrave.AppendChild(nodeInfDesc);
                            nodeInfGraves.AppendChild(nodeInfGrave);
                        }
                        rootNode.AppendChild(nodeInfGraves);
                        #endregion

                        Matriculacion.MatriculacionVehicular oMatric = new Matriculacion.MatriculacionVehicular(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                                                            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                                                            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
                        DataTable _dtVehiculos = oMatric.VehiculosPorPersona(identificacion);
                        XmlNode nodeVehiculos = xmlDoc.CreateNode(XmlNodeType.Element, "Vehiculos", null);
                        #region Crear XML de Vehiculos que posee la persona
                        foreach (DataRow dr in _dtVehiculos.Rows)
                        {
                            XmlNode nodeVehiculo = xmlDoc.CreateNode(XmlNodeType.Element, "Vehiculo", null);
                            XmlNode nodePlaca = xmlDoc.CreateNode(XmlNodeType.Element, "Placa", null);
                            nodePlaca.InnerText = dr[0].ToString();
                            nodeVehiculo.AppendChild(nodePlaca);
                            XmlNode nodeClase = xmlDoc.CreateNode(XmlNodeType.Element, "Clase", null);
                            nodeClase.InnerText = dr[1].ToString();
                            nodeVehiculo.AppendChild(nodeClase);
                            XmlNode nodeMarca = xmlDoc.CreateNode(XmlNodeType.Element, "Marca", null);
                            nodeMarca.InnerText = dr[2].ToString();
                            nodeVehiculo.AppendChild(nodeMarca);
                            XmlNode nodeModelo = xmlDoc.CreateNode(XmlNodeType.Element, "Modelo", null);
                            nodeModelo.InnerText = dr[3].ToString();
                            nodeVehiculo.AppendChild(nodeModelo);
                            XmlNode nodeColor = xmlDoc.CreateNode(XmlNodeType.Element, "Color", null);
                            nodeColor.InnerText = dr[4].ToString();
                            nodeVehiculo.AppendChild(nodeColor);
                            nodeVehiculos.AppendChild(nodeVehiculo);
                        }
                        rootNode.AppendChild(nodeVehiculos);
                        #endregion
                    }
                    else
                    {
                        rootNode.AppendChild(ErrorNode(xmlDoc, oDatos[1].ToString()));
                    }
                }
                catch (Exception ex)
                {
                    rootNode.AppendChild(ErrorNode(xmlDoc, ex.Message));
                }
            }
            else
            {
                rootNode.AppendChild(ErrorNode(xmlDoc, "Login Error, Please contact gpincay@cte.gob.ec - " + errorValUser));
            }

            return xmlDoc.InnerXml;
        }


        private bool ValidateUserAndPassword(string user, string password, out string error)
        {
            CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
            objCrypto.Key = Constantes.ParametrosCifradoCs.key;
            objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
            SeguridadWebAppInt.UsuarioWebAppInt oUsuario = new SeguridadWebAppInt.UsuarioWebAppInt(user, objCrypto, ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                    ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                    ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
            if (oUsuario.LogIn(password))
            {
                error = string.Empty;
                return true;
            }
            else
            {
                error = oUsuario.Error;
                return false;
            }
        }

        private XmlNode ErrorNode(XmlDocument xmlD, string errorMessage)
        {
            XmlNode nodeError = xmlD.CreateNode(XmlNodeType.Element, "Error", null);
            nodeError.InnerText = errorMessage;
            return nodeError;
        }
    }
}