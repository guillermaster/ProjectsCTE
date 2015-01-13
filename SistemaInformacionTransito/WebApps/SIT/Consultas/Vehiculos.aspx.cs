using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Consultas_DatosVehiculo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Master.Title = "Datos de Vehículo";
            Form.DefaultFocus = txtPlaca.ClientID;
            Form.DefaultButton = btnConsultar.UniqueID;
            CollapsiblePanelExtender2.Collapsed = false;
        }
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        gvVehicPorPropiet.Visible = false;
        TabContainer1.Visible = false;

        if (radChasis.Checked)
            ConsultarDatos(txtChasis.Text.Trim().ToUpper());
        else
            ConsultarDatos(txtPlaca.Text.Trim().ToUpper());
        pnlContent.Style["display"] = "block";
        pnlContent.Visible = true;
    }

    protected void ConsultarDatos(string identificacionVeh)
    {
        try
        {
            jwsVehiculos.VehiculosComunicacionControlService jwsVeh = new jwsVehiculos.VehiculosComunicacionControlService();
            jwsVehiculos.vehiculoUtil objVeh = jwsVeh.obtenerVehiculoPlacaChasis(identificacionVeh);
            if (string.IsNullOrWhiteSpace(objVeh.pv_error))
            {
                #region "características"
                txtChasisDat.Text = objVeh.pv_chasis;
                txtCilindraje.Text = objVeh.pv_cilindraje;
                txtServicio.Text = objVeh.pv_tipo_servicio;
                txtColor.Text = objVeh.pv_color;
                txtFechaCompra.Text = objVeh.pv_fecha_compra;
                txtCantonCirc.Text = objVeh.pv_canton_cir;
                txtMarca.Text = objVeh.pv_marca;
                txtModelo.Text = objVeh.pv_clase_tipo + " " + objVeh.pv_modelo_vehiculo;
                txtMotor.Text = objVeh.pv_num_motor;
                txtPaisOrigen.Text = objVeh.pv_pais;
                txtPlacaDat.Text = objVeh.pv_placa;
                txtTipo.Text = objVeh.pv_clase_servicio;
                txtTonelaje.Text = objVeh.pv_tonelaje;
                txtCAVMCPN.Text = objVeh.pv_camv;
                #endregion
                #region "matriculación"

                try
                {
                    if (objVeh.lsMatriculacion != null && objVeh.lsMatriculacion.Length > 0)
                    {
                        DataTable dtMatric = new DataTable("Matriculación");
                        dtMatric.Columns.Add("Año");
                        dtMatric.Columns.Add("Emisión");
                        dtMatric.Columns.Add("Caducidad");
                        dtMatric.Columns.Add("Tipo de matrícula");
                        dtMatric.Columns.Add("Tipo de cobro");
                        for (int i = 0; i < objVeh.lsMatriculacion.Length; i++)
                        {
                            DataRow drMat = dtMatric.NewRow();
                            drMat[0] = objVeh.lsMatriculacion[i].anioMatricula;
                            drMat[1] = objVeh.lsMatriculacion[i].fechaRegistro;
                            if (i == objVeh.lsMatriculacion.Length-1)
                                drMat[2] = objVeh.caducidadMatricula;
                            drMat[3] = objVeh.lsMatriculacion[i].tipoMatricula;
                            drMat[4] = objVeh.lsMatriculacion[i].tipoCobro;
                            dtMatric.Rows.Add(drMat);
                        }
                        gvHistMatriculac.DataSource = dtMatric;
                        gvHistMatriculac.DataBind();
                    }
                    else
                    {
                        gvHistMatriculac.EmptyDataText = "No registra historial de matriculación.";
                        gvHistMatriculac.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    gvHistMatriculac.EmptyDataText = ex.Message;
                    gvHistMatriculac.DataBind();
                }
                #endregion
                #region "Bloqueos"
                try
                {
                    if (objVeh.lsBloqueo != null && objVeh.lsBloqueo.Length > 0)
                    {
                        DataTable dtBloqueos = new DataTable("Bloqueos");
                        dtBloqueos.Columns.Add("Fecha de ingreso");
                        dtBloqueos.Columns.Add("Descripción");
                        for (int i = 0; i < objVeh.lsBloqueo.Length; i++)
                        {
                            DataRow drBloq = dtBloqueos.NewRow();
                            drBloq[0] = objVeh.lsBloqueo[i].fechaRegistro;
                            drBloq[1] = objVeh.lsBloqueo[i].descripcion;
                            dtBloqueos.Rows.Add(drBloq);
                        }
                        gvBloqueos.DataSource = dtBloqueos;
                        gvBloqueos.DataBind();
                    }
                    gvBloqueos.EmptyDataText = "No registra ningún bloqueo en la actualidad.";
                    gvBloqueos.DataBind();
                }
                catch (Exception ex)
                {
                    gvBloqueos.EmptyDataText = ex.Message;
                    gvBloqueos.DataBind();
                }
                #endregion
                if (string.IsNullOrWhiteSpace(txtServicio.Text) || txtServicio.Text == "PARTICULAR")
                    TabTransPub.Visible = false;
                TabContainer1.Visible = true;
            }
            else
                Master.Error = objVeh.pv_error;
        }
        catch (Exception ex)
        {
            Master.Error = ex.Message;
        }
    }


    protected void btnZoomImg_Click(object sender, EventArgs e)
    {
        imgFotoVehFull.ImageUrl = "fotoVehiculo.aspx?p=" + txtPlaca.Text + "&f=1";
        MPE.Show();
    }


    protected void grdVehiculos_SelectedIndexChanged(object sender, EventArgs e)
    {
        TabContainer1.ActiveTabIndex = 0;
        string placa = grdVehiculos.SelectedRow.Cells[0].Text;
        radPlaca.Checked = true;
        txtPlaca.Text = placa;
        ConsultarDatos(placa);
    }


    protected void gvVehicPorPropiet_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvVehicPorPropiet.Visible = false;
        TabContainer1.Visible = false;
        txtPlaca.Text = gvVehicPorPropiet.SelectedRow.Cells[1].Text.ToUpper();
        txtChasis.Text = string.Empty;
        radPlaca.Checked = true;
        ConsultarDatos(gvVehicPorPropiet.SelectedRow.Cells[1].Text.ToUpper());
        pnlContent.Style["display"] = "block";
        pnlContent.Visible = true;
    }

    protected void ConsultarVehiculosPorPersona(string numLicencia)
    {
        try
        {
            jwsVehiculos.VehiculosComunicacionControlService jwsVeh = new jwsVehiculos.VehiculosComunicacionControlService();
            jwsVehiculos.vehiculoUtil[] vehiculos = jwsVeh.obtenerVehiculoIdentificacion(numLicencia);
            DataTable dtVehiculos = new DataTable("Vehículos");
            dtVehiculos.Columns.Add("Placa");
            dtVehiculos.Columns.Add("Cantón de circulación");
            dtVehiculos.Columns.Add("Marca");
            dtVehiculos.Columns.Add("Modelo");
            dtVehiculos.Columns.Add("Tipo");
            dtVehiculos.Columns.Add("Color");
            dtVehiculos.Columns.Add("Año");
            dtVehiculos.Columns.Add("Servicio");
            for (int i = 0; i < vehiculos.Length; i++)
            {
                DataRow dr = dtVehiculos.NewRow();
                dr[0] = vehiculos[i].pv_placa;
                dr[1] = vehiculos[i].pv_canton_cir;
                dr[2] = vehiculos[i].pv_marca;
                dr[3] = vehiculos[i].pv_modelo_vehiculo;
                dr[4] = vehiculos[i].pv_clase_servicio + " " + vehiculos[i].pv_clase_tipo;
                dr[5] = vehiculos[i].pv_color;
                dr[6] = vehiculos[i].pv_anio;
                dr[7] = vehiculos[i].pv_tipo_servicio;
                dtVehiculos.Rows.Add(dr);
            }
            gvVehicPorPropiet.DataSource = dtVehiculos;
            gvVehicPorPropiet.DataBind();
            gvVehicPorPropiet.Visible = true;
            pnlContent.Visible = true;
        }
        catch (Exception ex)
        {
            Master.Error = ex.Message;
        }
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        TabContainer1.Visible = false;
        pnlContent.Visible = true;
        ConsultarVehiculosPorPersona(txtNumLicencia.Text.Trim());
    }
}