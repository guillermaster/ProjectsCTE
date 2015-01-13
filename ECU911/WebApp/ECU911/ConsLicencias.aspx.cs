using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;
using Brevetacion;

public partial class ConsLicencias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            radLic.Checked = true;
            Page.Form.DefaultFocus = txtNumLicencia.ClientID;
            Page.Form.DefaultButton = btnConsultar.UniqueID;
        }
    }

    private void SetError(string errorMsg)
    {
        lblWarning.Text = errorMsg;
        pnlWarning.Visible = true;
    }


    protected bool ConsultarDatosLicencia(string noLicencia)
    {
        bool retValue = false;
        try
        {
            Licencia oLicencia = new Licencia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            object[] oDatos = oLicencia.ConsultarLicencia(noLicencia);

            if (string.IsNullOrWhiteSpace(oLicencia.Error))
            {
                if (oDatos[0].ToString() == "N")
                {
                    SetError("No existe licencia con identificación " + noLicencia);
                }
                else
                {
                    pnlDatosLicencia.Visible = true;
                    #region Datos Personales
                    DataTable oTabDatosPersonales = new DataTable();
                    oTabDatosPersonales.Columns.Add("identificacion");
                    oTabDatosPersonales.Columns.Add("nombre");
                    oTabDatosPersonales.Columns.Add("fecha_nacimiento");
                    oTabDatosPersonales.Columns.Add("lugar_nacimiento");
                    oTabDatosPersonales.Columns.Add("sexo");
                    oTabDatosPersonales.Columns.Add("estatura");
                    oTabDatosPersonales.Columns.Add("estado_civil");
                    oTabDatosPersonales.Columns.Add("profesion");
                    oTabDatosPersonales.Columns.Add("lugar_residencia");
                    oTabDatosPersonales.Columns.Add("direccion");
                    oTabDatosPersonales.Columns.Add("telefono");

                    DataRow rowDatosPersonales = oTabDatosPersonales.NewRow();
                    rowDatosPersonales[0] = noLicencia;
                    rowDatosPersonales[1] = oDatos[1].ToString();
                    rowDatosPersonales[2] = oDatos[3].ToString();
                    rowDatosPersonales[3] = oDatos[4].ToString() + "-" + oDatos[5].ToString() + "-" + oDatos[6].ToString();
                    rowDatosPersonales[4] = oDatos[7].ToString();
                    rowDatosPersonales[5] = oDatos[12].ToString();
                    rowDatosPersonales[6] = oDatos[13].ToString();
                    rowDatosPersonales[7] = oDatos[14].ToString();
                    rowDatosPersonales[8] = oDatos[17].ToString() + "-" + oDatos[18].ToString() + "-" + oDatos[19].ToString();
                    rowDatosPersonales[9] = oDatos[15].ToString();
                    rowDatosPersonales[10] = oDatos[16].ToString();
                    oTabDatosPersonales.Rows.Add(rowDatosPersonales);
                    frmViewDatosPers.DataSource = oTabDatosPersonales;
                    frmViewDatosPers.DataBind();
                    #endregion

                    #region Rasgos
                    if (oDatos[8].ToString() == "null")
                    {
                        //this.txtTipoSangre.Text = "";
                    }
                    #endregion

                    imgFoto.ImageUrl = "fotoLicencia.aspx?id=" + oDatos[20].ToString();
                    imgFoto.DataBind();

                    #region Llenar Tablas
                    DataRow oRegistros;
                    DataTable _oTabLic = new DataTable();
                    _oTabLic.Columns.Add("Categoría");
                    _oTabLic.Columns.Add("Fecha de emisión");
                    _oTabLic.Columns.Add("Fecha de caducidad");
                    _oTabLic.Columns.Add("Tipo de Sangre");

                    DataTable _oTabBloq = new DataTable();
                    _oTabBloq.Columns.Add("Fecha");
                    _oTabBloq.Columns.Add("Descripción");


                    DataTable _oTabRes = new DataTable();
                    _oTabRes.Columns.Add("Fecha");
                    _oTabRes.Columns.Add("Descripción");

                    DataTable _oTabInfr = new DataTable();
                    _oTabInfr.Columns.Add("Fecha");
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
                    if (_oTabLic.Rows.Count == 0)
                        grdLicencias.EmptyDataText = "No se le ha emitido ninguna licencia";
                    grdLicencias.DataBind();
                    

                    grdBloqueos.DataSource = _oTabBloq;
                    if (_oTabBloq.Rows.Count == 0)
                        grdBloqueos.EmptyDataText = "No registra ningún bloqueo";
                    grdBloqueos.DataBind();
                    

                    grdRestricciones.DataSource = _oTabRes;
                    if (_oTabRes.Rows.Count == 0)
                        grdRestricciones.EmptyDataText = "No tiene ninguna restricción";
                    grdRestricciones.DataBind();
                    

                    grdInfracciones.DataSource = _oTabInfr;
                    if (_oTabInfr.Rows.Count == 0)
                        grdInfracciones.EmptyDataText = "No ha cometido ninguna infracción grave";
                    grdInfracciones.DataBind();
                    
                    //ShowControls();

                    #endregion

                    retValue = true;
                }
            }
            else
            {
                SetError(oLicencia.Error);
            }
        }
        catch(Exception ex)
        {
            SetError("Error: " + ex.Message);
        }

        return retValue;
    }

    protected void ConsultarPersonas(string nombre)
    {
        SecretariaGeneral.Contratos oContratos = new SecretariaGeneral.Contratos(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtPersonas = oContratos.ConsultarPersonas(nombre.ToUpper(), null);
        gvPersonas.DataSource = dtPersonas;
        gvPersonas.DataBind();
        gvPersonas.Visible = true;

        if (gvPersonas.Rows.Count == 0)
        {
            if (string.IsNullOrWhiteSpace(oContratos.Error))
                SetError("No se encontraron licencias con el nombre ingresado");
            else
                SetError("Error: " + oContratos.Error);
        }
    }


    public void ConsultarVehiculosPorPersona(string identificacion)
    {
        Matriculacion.MatriculacionVehicular oMatric = new Matriculacion.MatriculacionVehicular(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        DataTable dtVehiculos = new DataTable();

        dtVehiculos = oMatric.VehiculosPorPersonaDatosBloqueos(identificacion);
        grdVehiculos.DataSource = dtVehiculos;


        if (dtVehiculos.Rows.Count == 0)
        {
            if (oMatric.Error == string.Empty)
                this.grdVehiculos.EmptyDataText = "No registra ningún vehículo a su nombre";
            else
                this.grdVehiculos.EmptyDataText = oMatric.Error;
        }

        grdVehiculos.DataBind();
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        pnlDatosLicencia.Visible = false;
        gvPersonas.Visible = false;

        if (radLic.Checked)
        {
            if (ConsultarDatosLicencia(txtNumLicencia.Text))
                ConsultarVehiculosPorPersona(txtNumLicencia.Text);
        }
        else
            ConsultarPersonas(txtNombre.Text);
    }

    protected void grdVehiculos_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("ConsVehiculo.aspx?placa=" + grdVehiculos.SelectedRow.Cells[0].Text);
    }
    protected void gvPersonas_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPersonas.Visible = false;
        ConsultarDatosLicencia(gvPersonas.SelectedRow.Cells[0].Text);
        ConsultarVehiculosPorPersona(gvPersonas.SelectedRow.Cells[0].Text);
    }
}