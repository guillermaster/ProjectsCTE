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
using SecretariaGeneral;


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

            if (Request.QueryString.Count > 0)
            {
                this.txtLicencia.Text = Request.QueryString[0];
                Consultar();
                SetButtonVehURL();
            }
        }
        else
            SetButtonVehURL();
    }

    protected void SetButtonVehURL()
    {
        if (this.txtLicencia.Text.Length > 0)
        {
            this.btnVerVehiculos.PostBackUrl = "VehiculosPorPersona.aspx?Lic=" + this.txtLicencia.Text;
            this.btnVerVehiculos.Visible = true;
        }
        else
            this.btnVerVehiculos.Visible = false;
    }

    protected void btnConsultaByCedula_Click(object sender, EventArgs e)
    {
        Consultar();
    }

    protected void Consultar()
    {
        this.datosFull.Visible = false;
        this.lblError.Text = "";
        this.imgLicFoto.ImageUrl = "fotoLicencia.aspx?idPers=" + this.txtLicencia.Text.ToString();
        this.imgLicFoto.DataBind();
        PrintDatosLicencia();
        
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

        if (objDatosLic.Length > 2)
        {
            try
            {
                this.datosFull.Visible = true;
                this.lblNombre.Text = objDatosLic[1].ToString();
                this.lblFechaNacimiento.Text = objDatosLic[3].ToString();
                this.lblLugarNacimiento.Text = objDatosLic[4].ToString();
                this.lblProvNacimiento.Text = objDatosLic[5].ToString();
                this.lblPaisNacimiento.Text = objDatosLic[6].ToString();
                this.lblSexo.Text = objDatosLic[7].ToString();
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

                PrintDatosAdicionalesLicencia();
            }
            catch (Exception ex)
            {
            }
        }
        else
        {
            this.lblError.Text = objDatosLic[1].ToString();
            this.datosFull.Visible = false;
            PrintDatosContrato();

        }
    }

    protected void PrintDatosContrato()
    {
        Contratos oContratos = new Contratos(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if (oContratos.LoadDatosBasicosUsuarioById(this.txtLicencia.Text))
        {
            this.lblNombre2.Text = oContratos.Apellido1 + " " + oContratos.Apellido2 + " " + oContratos.Nombres;
            this.lblFechaNacimiento2.Text = oContratos.FechaNacimiento;
            this.lblLugarNacimiento2.Text = oContratos.Localidad;
            this.lblProvinciaNacimiento2.Text = oContratos.Provincia;
            this.lblPaisNacimiento2.Text = oContratos.Pais;
            this.lblSexo2.Text = oContratos.Sexo;
            this.lblTipoSangre2.Text = oContratos.Sangre;
            this.lblEstatura2.Text = oContratos.Estatura;
            this.lblEstadoCivil2.Text = oContratos.EstadoCivil;
            this.lblProfesion2.Text = oContratos.Profesion;
            this.lblDireccionRes2.Text = oContratos.Direccion;
            this.lblTelefonoRes2.Text = oContratos.Telefono;
            this.lblCelular2.Text = oContratos.Celular;
            this.lblCantonRes2.Text = oContratos.CantonRes;
            this.lblProvinciaRes2.Text = oContratos.ProvinciaRes;
            this.lblPaisRes2.Text = oContratos.PaisRes;
            this.datosBasicos.Visible = true;
        }
        else
        {
            this.lblError.Text = oContratos.Error;
            this.datosBasicos.Visible = false;
        }
    }
}
