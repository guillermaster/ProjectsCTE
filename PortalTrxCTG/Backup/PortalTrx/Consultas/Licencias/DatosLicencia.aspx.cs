using System;
using System.Web;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Brevetacion;

public partial class Consultas_Licencias_DatosLicencia : System.Web.UI.Page
{
    private static DataTable _oTabLic;
    private static DataTable _oTabBloq;
    private static DataTable _oTabRes;
    private static DataTable _oTabInfr;


    protected void Page_Load(object sender, EventArgs e)
    {
        ConsultarDatosLicencia(HttpContext.Current.User.Identity.Name);
    }

    protected void ConsultarDatosLicencia(string noLicencia)
    {
        try
        {
            Licencia oLicencia = new Licencia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
            object[] oDatos = oLicencia.ConsultarLicencia(noLicencia);
            
            if (oDatos[0].ToString() == "N")
                HtmlWriter.Messages.ShowMainContentError(Master, divContent, "No existe licencia número " + noLicencia + " o se produjo un error al consultar la información");
            else
            {
                HtmlWriter.Messages.HideMainContentError(Master, divContent);
                #region Datos Personales
                DataTable oTabDatosPersonales = new DataTable();
                oTabDatosPersonales.Columns.Add("Nombre:");
                oTabDatosPersonales.Columns.Add("Fecha de nacimiento:");
                oTabDatosPersonales.Columns.Add("Lugar de nacimiento:");
                oTabDatosPersonales.Columns.Add("Sexo:");
                oTabDatosPersonales.Columns.Add("Estatura:");
                oTabDatosPersonales.Columns.Add("Estado civil:");
                oTabDatosPersonales.Columns.Add("Profesión:");
                oTabDatosPersonales.Columns.Add("Lugar de residencia:");
                oTabDatosPersonales.Columns.Add("Dirección:");
                oTabDatosPersonales.Columns.Add("Teléfono:");

                DataRow rowDatosPersonales = oTabDatosPersonales.NewRow();
                rowDatosPersonales[0] = oDatos[1].ToString();
                rowDatosPersonales[1] = oDatos[3].ToString();
                rowDatosPersonales[2] = oDatos[4].ToString() + " - " + oDatos[5].ToString() + " - " + oDatos[6].ToString();
                rowDatosPersonales[3] = oDatos[7].ToString();
                rowDatosPersonales[4] = oDatos[12].ToString();
                rowDatosPersonales[5] = oDatos[13].ToString();
                rowDatosPersonales[6] = oDatos[14].ToString();
                rowDatosPersonales[7] = oDatos[17].ToString() + " - " + oDatos[18].ToString() + " - " + oDatos[19].ToString();
                rowDatosPersonales[8] = oDatos[15].ToString();
                rowDatosPersonales[9] = oDatos[16].ToString();
                oTabDatosPersonales.Rows.Add(rowDatosPersonales);
                dvDatosPersonales.DataSource = oTabDatosPersonales;
                dvDatosPersonales.DataBind();
                dvDatosPersonales.Visible = true;
                #endregion

                #region Rasgos
                if (oDatos[8].ToString() == "null")
                {
                    //this.txtTipoSangre.Text = "";
                }
                #endregion

                imgFoto.ImageUrl = "fotoUsuarioLic.aspx?id=" + oDatos[20].ToString();
                imgFoto.DataBind();

                #region Llenar Tablas
                DataRow oRegistros;
                _oTabLic = new DataTable();
                _oTabLic.Columns.Add("Categoría");
                _oTabLic.Columns.Add("Fecha/Emisión");
                _oTabLic.Columns.Add("Fecha/Caducidad");
                _oTabLic.Columns.Add("Tipo de Sangre");

                _oTabBloq = new DataTable();
                _oTabBloq.Columns.Add("Fecha");
                _oTabBloq.Columns.Add("Descripción");


                _oTabRes = new DataTable();
                _oTabRes.Columns.Add("Fecha");
                _oTabRes.Columns.Add("Descripción");

                _oTabInfr = new DataTable();
                _oTabInfr.Columns.Add("Fecha/Infracción");
                _oTabInfr.Columns.Add("Contravención");

                //////////////////////////////////////////////////////////
                #region Llenar tabla categoria de licencias
                /*DataTable oTabCatLicencias = new DataTable();
                oTabCatLicencias.Columns.Add("Cat.");
                oTabCatLicencias.Columns.Add("Autorización");
                DataRow oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "A";
                oRegCatLicencias[1] = "Para ciclomotores, motocicletas y triciclos motorizados";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "B";
                oRegCatLicencias[1] = "Para automóviles y camionetas con acoplados de hasta 1.75 KG de carga o casas rodantes";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "C";
                oRegCatLicencias[1] = "Para camiones sin acoplados y los comprendidos en la clase B";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "D";
                oRegCatLicencias[1] = "Para los destinados al servicio de transporte de pasajeros y los de la clase B o C según el caso";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "E";
                oRegCatLicencias[1] = "Para camiones articulados o con acoplados, maquinaria especial no agrícola y los de la clase C y D";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "F";
                oRegCatLicencias[1] = "Para automotores especiales adaptados para discapacitados";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "G";
                oRegCatLicencias[1] = "Para tractores y maquinaria especial agrícola";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                grdCatLicencias.DataSource = oTabCatLicencias;
                grdCatLicencias.DataBind();*/
                #endregion
                /////////////////////////////////////////////////////////


                Int64 numElem = oDatos.GetLength(0);


                for (Int64 iIndice = 21; iIndice < numElem; iIndice++)
                {
                    switch (((object[])oDatos[iIndice])[0].ToString())
                    {
                        case "LIC":
                            oRegistros = _oTabLic.NewRow();
                            oRegistros[0] = (((object[])oDatos[iIndice])[1].ToString());
                            oRegistros[1] = Convert.ToDateTime((((object[])oDatos[iIndice])[2].ToString()));
                            oRegistros[2] = Convert.ToDateTime((((object[])oDatos[iIndice])[3].ToString()));
                            oRegistros[3] = oDatos[8];
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
                grdLicencias.DataSource = _oTabLic;
                grdLicencias.DataBind();
                if(_oTabLic.Rows.Count == 0)
                    divLicencias.Controls.Add(HtmlWriter.Messages.PrintInfoMessage("No se le ha emitido ninguna licencia"));

                grdBloqueos.DataSource = _oTabBloq;
                grdBloqueos.DataBind();
                if(_oTabBloq.Rows.Count == 0)
                    divBloqueos.Controls.Add(HtmlWriter.Messages.PrintInfoMessage("No registra ningún bloqueo"));

                grdRestricciones.DataSource = _oTabRes;
                grdRestricciones.DataBind();
                if (_oTabRes.Rows.Count == 0)
                    divRestricciones.Controls.Add(HtmlWriter.Messages.PrintInfoMessage("No tiene ninguna restricción"));

                grdInfracciones.DataSource = _oTabInfr;
                grdInfracciones.DataBind();
                if(_oTabInfr.Rows.Count == 0)
                    divCitaciones.Controls.Add(HtmlWriter.Messages.PrintInfoMessage("No ha cometido ninguna infracción grave"));
                //ShowControls();

                #endregion

            }
        }
        catch
        {
            HtmlWriter.Messages.ShowMainContentError(Master, divContent, "Se produjo un error al consultar la información");
        }


    }

    protected void GrdLicenciasPageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        grdLicencias.PageIndex = e.NewPageIndex;
        grdLicencias.DataSource = _oTabLic;
        grdLicencias.DataBind();
    }

    protected void GrdRestriccionesPageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        grdRestricciones.PageIndex = e.NewPageIndex;
        grdRestricciones.DataSource = _oTabRes;
        grdRestricciones.DataBind();
    }

    protected void GrdBloqueosPageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        grdBloqueos.PageIndex = e.NewPageIndex;
        grdBloqueos.DataSource = _oTabBloq;
        grdBloqueos.DataBind();
    }

    protected void GrdInfraccionesPageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        grdInfracciones.PageIndex = e.NewPageIndex;
        grdInfracciones.DataSource = _oTabInfr;
        grdInfracciones.DataBind();
    }
}