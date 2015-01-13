using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Citaciones;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.DefaultFocus = btnConsTramite.ClientID;
            txtNumTramite.Attributes.Add("onkeypress", "return clickButton(event,'" + btnEjConsTramite.ClientID + "')");
            txtNumCedula.Attributes.Add("onkeypress", "return clickButton(event,'" + btnEjConsCitac.ClientID + "')");            
        }
        
    }

    protected void btnEjConsTramite_Click(object sender, EventArgs e)
    {
        pnlResConsTramite.Visible = false;
        pnlResTramites.Visible = false;
        lblErrorConsTramite.Text = string.Empty;
        mpeConsTramite.Show();
        if (radTram.Checked)
        {
            CargarDatosTramite(txtNumTramite.Text);
            Page.Form.DefaultFocus = txtNumTramite.ClientID;
        }
        else
        {
            CargarTramitesPorUsuario();
        }
    }

    protected void btnEjConsCitac_Click(object sender, EventArgs e)
    {
        lblErrorConsCitac.Text = string.Empty;
        ConsultarInfracciones(txtNumCedula.Text, "I");
        mpeConsCitac.Show();
        Page.Form.DefaultFocus = txtNumCedula.ClientID;
    }


    protected void ConsultarInfracciones(string id, string tipoId)
    {
        Infracciones oCitac = new Infracciones(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtCitac = oCitac.InfraccionesPendientes(id.Trim(), tipoId);
        GetTotalesInfracciones(dtCitac);
        gvCitacPend.DataSource = dtCitac;
        gvCitacPend.DataBind();

        if (gvCitacPend.Rows.Count == 0)
        {
            if (string.IsNullOrWhiteSpace(oCitac.Error))
                lblErrorConsCitac.Text = "Usted no registra ninguna citación pendiente de pago";
            else
                lblErrorConsCitac.Text = "Ha ocurrido un error: " + oCitac.Error;
        }
    }

    private void GetTotalesInfracciones(DataTable dtCitaciones)
    {
        float totPtos = 0, totCitac = 0, totMulta = 0, totTotal = 0;
        try
        {
            foreach (DataRow dr in dtCitaciones.Rows)
            {
                totPtos += float.Parse(dr["puntos_perdidos"].ToString());
                totCitac += float.Parse(dr["valor_citacion"].ToString());
                totMulta += float.Parse(dr["multa_citacion"].ToString());
                totTotal += float.Parse(dr["total_pagar"].ToString());
            }
        }
        catch (Exception ex)
        {
        }
        hdnTotPunto.Value = totPtos.ToString();
        hdnTotCitac.Value = totCitac.ToString();
        hdnTotMulta.Value = totMulta.ToString();
        hdnTotTotal.Value = totTotal.ToString();
    }

    protected void btnConsTramite_Click(object sender, EventArgs e)
    {
        txtNumTramite.Text = string.Empty;
        lblErrorConsTramite.Text = string.Empty;
        pnlResConsTramite.Visible = false;
        mpeConsTramite.Show();
        Page.Form.DefaultFocus = txtNumLicencia.ClientID;
        Page.Form.DefaultButton = btnEjConsTramite.UniqueID;
        pnlResTramites.Visible = false;
        pnlResConsTramite.Visible = false;
        radLic.Checked = true;
        txtNumLicencia.Text = string.Empty;
        txtNumTramite.Text = string.Empty;
        txtPlaca.Text = string.Empty;
    }

    protected void btnConsCitaciones_Click(object sender, EventArgs e)
    {
        txtNumCedula.Text = string.Empty;
        lblErrorConsCitac.Text = string.Empty;
        gvCitacPend.DataSource = new DataTable();
        gvCitacPend.DataBind();
        mpeConsCitac.Show();
        Page.Form.DefaultFocus = txtNumCedula.ClientID;
        Page.Form.DefaultButton = btnEjConsCitac.UniqueID;
        txtNumCedula.Text = string.Empty;
    }


    protected void CargarDatosTramite(string numTramite)
    {
        ComprobanteElectronicoPago.CEP oCEP = new ComprobanteElectronicoPago.CEP(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        DataTable dtTramite = oCEP.ConsultaDatosTramite(numTramite.Trim());
        
        if (dtTramite.Rows.Count == 0)
        {
            lblErrorConsTramite.Text = "No se encontró información del trámite número " + numTramite;
            pnlResConsTramite.Visible = false;
        }
        else
        {
            lblTramite.Text = dtTramite.Rows[0][1].ToString();
            gvEtapasTramite.DataSource = dtTramite;
            gvEtapasTramite.DataBind();
            /*lblNumEtapa.Text = dtTramite.Rows[dtTramite.Rows.Count - 1][0].ToString();
            lblDescEtapa.Text = dtTramite.Rows[dtTramite.Rows.Count - 1][1].ToString();
            lblEstTramite.Text = dtTramite.Rows[dtTramite.Rows.Count-1][2].ToString();
            lblFechaEjTramite.Text = dtTramite.Rows[dtTramite.Rows.Count-1][3].ToString();*/
            pnlResConsTramite.Visible = true;
        }
    }


    protected void CargarTramitesPorUsuario()
    {
        ComprobanteElectronicoPago.CEP oCEP = new ComprobanteElectronicoPago.CEP(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        string identificacion = string.Empty;
        string tipoIdentificacion = string.Empty;

        if (radLic.Checked)
        {
            identificacion = txtNumLicencia.Text.Trim();
            tipoIdentificacion = Constantes.TipoIdentificacion.Vehiculo.CEDRUCPAS.ToString();
        }
        else if (radPlaca.Checked)
        {
            identificacion = txtPlaca.Text.Trim();
            tipoIdentificacion = Constantes.TipoIdentificacion.Vehiculo.PLACA.ToString();
        }

        DataTable dtTramites = oCEP.ConsultaTramitesPorUsuario(identificacion, tipoIdentificacion);

        if (dtTramites.Rows.Count == 0)
        {
            lblErrorConsTramite.Text = "No se encontró ningun trámite activo";
            pnlResTramites.Visible = false;
        }
        else
        {
            gvTramites.DataSource = dtTramites;
            gvTramites.DataBind();
            /*lblNumEtapa.Text = dtTramite.Rows[dtTramite.Rows.Count - 1][0].ToString();
            lblDescEtapa.Text = dtTramite.Rows[dtTramite.Rows.Count - 1][1].ToString();
            lblEstTramite.Text = dtTramite.Rows[dtTramite.Rows.Count-1][2].ToString();
            lblFechaEjTramite.Text = dtTramite.Rows[dtTramite.Rows.Count-1][3].ToString();*/
            pnlResTramites.Visible = true;
        }
    }


    /*protected void btnCloseConsCitaciones_Click(object sender, EventArgs e)
    {
        Page.Form.DefaultFocus = btnCloseConsTramite.ClientID;
    }*/

    protected void gvTramites_SelectedIndexChanged(object sender, EventArgs e)
    {
        mpeConsTramite.Show();
        pnlResTramites.Visible = false;
        CargarDatosTramite(gvTramites.SelectedRow.Cells[1].Text);
    }
}
