using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Reporte;


public partial class ConsultaLicencias : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Auxiliar.Helper.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Default.aspx");
            }
            Page.Form.DefaultButton = this.btnConsultaByCedula.ID;
            Page.Form.DefaultFocus = this.txtLicencia.ID;
        }
    }
    protected void btnConsultaByCedula_Click(object sender, EventArgs e)
    {
        this.datosFull.Visible = false;
        this.lblError.Text = "";
        PrintDatosLicencia();
        PrintDatosAdicionalesLicencia();
        this.datosFull.Visible = true;
    }


    protected void PrintDatosAdicionalesLicencia()
    {
        Brevetacion.Licencia oLicencia = new Brevetacion.Licencia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        Object[] objDatosAdicLic = oLicencia.ConsultarDatosAdicionalesLicencia(this.txtLicencia.Text.ToString(), Session["Username"].ToString());
        this.lblFechaObtencLic.Text = objDatosAdicLic[0].ToString();
        this.lblProvinciaObtencLic.Text = objDatosAdicLic[1].ToString();
        this.lblCategoriaObtencLic.Text = objDatosAdicLic[2].ToString();
        this.lblDocumento.Text = objDatosAdicLic[3].ToString();
        this.lblNumero.Text = objDatosAdicLic[4].ToString();
        this.lblCatLicencia.Text = objDatosAdicLic[5].ToString();
        this.lblDescCatLicencia.Text = objDatosAdicLic[6].ToString();
        this.lblProvinciaOrigen.Text = objDatosAdicLic[7].ToString();
        this.lblFechaOrigen.Text = objDatosAdicLic[8].ToString();
        this.lblFechaEmision.Text = objDatosAdicLic[9].ToString();
        this.lblFechaCaducidad.Text = objDatosAdicLic[10].ToString();


        #region "Renovación y Duplicación de Licencia"

        DataTable dtRenovLic = new DataTable("RENOVACION_CANJE_LICENCIA");
        dtRenovLic.Columns.Add(new DataColumn("FECHA_EXPEDICION"));
        dtRenovLic.Columns.Add(new DataColumn("FECHA_EXPIRACION"));
        dtRenovLic.Columns.Add(new DataColumn("CATEGORIA"));

        DataTable dtDupLic = new DataTable("DUPLICACION_LICENCIA");
        dtDupLic.Columns.Add(new DataColumn("FECHA_EXPEDICION"));
        dtDupLic.Columns.Add(new DataColumn("FECHA_EXPIRACION"));
        dtDupLic.Columns.Add(new DataColumn("CATEGORIA"));

        DataRow dr;
        object[] row_datos;
        for (int i = 11; i < objDatosAdicLic.Length; i++)
        {
            row_datos = (object[])objDatosAdicLic[i];
            switch (row_datos[0].ToString())
            {
                case "REN":
                    dr = dtRenovLic.NewRow();
                    if (row_datos[2] != null)
                    {
                        dr["FECHA_EXPEDICION"] = row_datos[1].ToString();
                        dr["FECHA_EXPIRACION"] = row_datos[2].ToString();
                        dr["CATEGORIA"] = row_datos[3].ToString();
                        dtRenovLic.Rows.Add(dr);
                    }

                    break;
                case "DUP":
                    dr = dtDupLic.NewRow();
                    if (row_datos[2] != null)
                    {
                        dr["FECHA_EXPEDICION"] = row_datos[1].ToString();
                        dr["FECHA_EXPIRACION"] = row_datos[2].ToString();
                        dr["CATEGORIA"] = row_datos[3].ToString();
                        dtDupLic.Rows.Add(dr);
                    }
                    break;
            }
        }

        this.gvRenovacionLicencia.DataSource = dtRenovLic;
        this.gvRenovacionLicencia.DataBind();

        this.gvDuplicados.DataSource = dtDupLic;
        this.gvDuplicados.DataBind();
        

        #endregion

    }

    protected void PrintDatosLicencia()
    {
        Brevetacion.Licencia oLicencia = new Brevetacion.Licencia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        Object[] objDatosLic = oLicencia.ConsultarDatosLicencia(this.txtLicencia.Text.ToString());

        this.lblNombre.Text = objDatosLic[1].ToString();
        this.lblFechaNacimiento.Text = objDatosLic[3].ToString();
        this.lblLugarNacimiento.Text = objDatosLic[4].ToString();
        this.lblProvNacimiento.Text = objDatosLic[5].ToString();
        this.lblPaisNacimiento.Text = objDatosLic[6].ToString();
        this.lblSexo2.Text = objDatosLic[7].ToString();
        this.lblTipoSangre.Text = objDatosLic[8].ToString();
        this.lblCabello.Text = objDatosLic[9].ToString();
        this.lblColorOjos.Text = objDatosLic[10].ToString();
        this.lblRostro.Text = objDatosLic[11].ToString();
        this.lblEstatura.Text = objDatosLic[12].ToString();
        this.lblEstadoCivil.Text = objDatosLic[13].ToString();
        this.lblProfesion.Text = objDatosLic[14].ToString();
        this.lblDireccion.Text = objDatosLic[15].ToString();
        this.lblTelefono.Text = objDatosLic[16].ToString();
        this.lblCantonRes.Text = objDatosLic[17].ToString();
        this.lblProvinciaRes.Text = objDatosLic[18].ToString();
        this.lblPaisRes.Text = objDatosLic[19].ToString();
        this.imgLicFoto.ImageUrl = "fotoLicencia.aspx?idPers=" + objDatosLic[20].ToString();
        this.imgLicFoto.DataBind();

        #region "Consultar Bloqueos, Restricciones e Infracciones"

        DataTable dtBloqueosLic = new DataTable("BLOQUEOS_LICENCIA");
        dtBloqueosLic.Columns.Add(new DataColumn("FECHA"));
        dtBloqueosLic.Columns.Add(new DataColumn("DESCRIPCION"));

        DataTable dtRestriccLic = new DataTable("RESTRICCIONES_CONDUCTOR");
        dtRestriccLic.Columns.Add(new DataColumn("FECHA"));
        dtRestriccLic.Columns.Add(new DataColumn("DESCRIPCION"));

        DataTable dtInfraccGraves = new DataTable("INFRACCIONES_GRAVES_CONDUCTOR");
        dtInfraccGraves.Columns.Add(new DataColumn("FECHA"));
        dtInfraccGraves.Columns.Add(new DataColumn("DESCRIPCION"));

        DataRow dr;

        object[] row_datos;
        for (int i = 22; i < objDatosLic.Length; i++)
        {
            row_datos = (object[])objDatosLic[i];
            switch (row_datos[0].ToString())
            {
                case "BLO":
                    dr = dtBloqueosLic.NewRow();
                    if (row_datos[1] != null)
                    {
                        dr["FECHA"] = row_datos[1].ToString();
                        dr["DESCRIPCION"] = row_datos[2].ToString();
                        dtBloqueosLic.Rows.Add(dr);
                    }
                    break;
                case "RES":
                    dr = dtRestriccLic.NewRow();
                    if (row_datos[1] != null)
                    {
                        dr["FECHA"] = row_datos[1].ToString();
                        dr["DESCRIPCION"] = row_datos[2].ToString();
                        dtRestriccLic.Rows.Add(dr);
                    }

                    break;
                case "INF":
                    dr = dtInfraccGraves.NewRow();
                    if (row_datos[1] != null)
                    {
                        dr["FECHA"] = row_datos[1].ToString();
                        dr["DESCRIPCION"] = row_datos[2].ToString();
                        dtInfraccGraves.Rows.Add(dr);
                    }

                    break;
            }
        }

        this.gvBloqueos.DataSource = dtBloqueosLic;
        this.gvBloqueos.DataBind();

        this.gvRestricciones.DataSource = dtRestriccLic;
        this.gvRestricciones.DataBind();

        this.gvInfraccGraves.DataSource = dtInfraccGraves;
        this.gvInfraccGraves.DataBind();

        #endregion

    }
}
