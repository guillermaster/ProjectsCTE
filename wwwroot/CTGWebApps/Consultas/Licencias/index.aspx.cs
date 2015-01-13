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
using System.Web.SessionState;
using Brevetacion;

public partial class Consultas_Licencias_index : System.Web.UI.Page
{
    private Int64 _iIdPersona = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Seguridad.UsuarioWeb.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/DefaultConsultas.aspx");
        }
        this.divError.Visible = false;
        this.divWarning.Visible = false;
        ConsultarDatosLicencia(User.Identity.Name.ToString());
    }
    protected void ConsultarDatosLicencia(string noLicencia)
    {
        this.imgFoto.ImageUrl = "../../img/noFoto.gif";
        this.lblMensaje.Text = "";
        try
        {
            Licencia oLicencia = new Licencia(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            object[] oDatos = oLicencia.ConsultarLicencia(noLicencia);
            //object[] oDatos = ((servicio.Service)Session["servicio"]).Consultar_Licencia(this.txtLicencia.Text.Trim());
            if (oDatos[0].ToString() == "N")
            {
                this.divWarning.Visible = true;
                this.lblMsgWarning.Text = "Número de Licencia " + noLicencia + ".<br /><b>" + oDatos[1].ToString() + "</b>";
                this.imgFoto.Visible = false;
            }
            else
            {
                this.imgFoto.Visible = true;
                #region Datos Personales
                DataTable oTabDatosPersonales = new DataTable();
                oTabDatosPersonales.Columns.Add("Nombre:");
                oTabDatosPersonales.Columns.Add("Fecha de Nacimiento:");
                oTabDatosPersonales.Columns.Add("Lugar de Nacimiento:");
                oTabDatosPersonales.Columns.Add("Sexo:");
                oTabDatosPersonales.Columns.Add("Estatura:");
                oTabDatosPersonales.Columns.Add("Estado Civil:");
                oTabDatosPersonales.Columns.Add("Profesión:");
                oTabDatosPersonales.Columns.Add("Lugar de Residencia:");
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
                this.dvDatosPersonales.DataSource = oTabDatosPersonales;
                this.dvDatosPersonales.DataBind();
                this.dvDatosPersonales.Visible = true;
                #endregion

                #region Rasgos
                if (oDatos[8].ToString() == "null")
                {
                    //this.txtTipoSangre.Text = "";
                }
                else
                {
                    //this.txtTipoSangre.Text = oDatos[8].ToString();
                }
                #endregion
                #region Personal
                //this.lblMensaje.Text = oDatos[1].ToString();

                //if (oDatos[3].ToString() == "null")
                //{
                //    this.txtFecNac.Text = "";
                //}
                //else
                //{
                //    this.txtFecNac.Text = General.Fecha(Convert.ToDateTime(oDatos[3].ToString()));
                //}

                //if (oDatos[4].ToString() == "null")
                //{
                //    this.txtLugar.Text = "";
                //}
                //else
                //{
                //    this.txtLugar.Text = oDatos[4].ToString();
                //}

                //if (oDatos[6].ToString() == "null")
                //{
                //    this.txtPais.Text = "";
                //}
                //else
                //{
                //    this.txtPais.Text = oDatos[6].ToString();
                //}

                //if (oDatos[5].ToString() == "null")
                //{
                //    this.txtProvincia.Text = "";
                //}
                //else
                //{
                //    this.txtProvincia.Text = oDatos[5].ToString();
                //}

                //#endregion

                //#region Residencia
                //if (oDatos[19].ToString() == "null")
                //{
                //    this.txtPaisRes.Text = "";
                //}
                //else
                //{
                //    this.txtPaisRes.Text = oDatos[19].ToString();
                //}

                //if (oDatos[18].ToString() == "null")
                //{
                //    this.txtProvinciaRes.Text = "";
                //}
                //else
                //{
                //    this.txtProvinciaRes.Text = oDatos[18].ToString();
                //}

                //if (oDatos[17].ToString() == "null")
                //{
                //    this.txtCanton.Text = "";
                //}
                //else
                //{
                //    this.txtCanton.Text = oDatos[17].ToString();
                //}

                //if (oDatos[15].ToString() == "null")
                //{
                //    this.txtDireccion.Text = "";
                //}
                //else
                //{
                //    this.txtDireccion.Text = oDatos[15].ToString();
                //}

                //if (oDatos[16].ToString() == "null")
                //{
                //    this.txtFono.Text = "";
                //}
                //else
                //{
                //    this.txtFono.Text = oDatos[16].ToString();
                //}

                //#endregion

                //#region Rasgos
                //if (oDatos[7].ToString() == "null")
                //{
                //    this.txtSexo.Text = "";
                //}
                //else
                //{
                //    this.txtSexo.Text = oDatos[7].ToString();
                //}

                //if (oDatos[12].ToString() == "null")
                //{
                //    this.txtEstatura.Text = "";
                //}
                //else
                //{
                //    this.txtEstatura.Text = oDatos[12].ToString();
                //}

                //if (oDatos[10].ToString() == "null")
                //{
                //    this.txtColorOjos.Text = "";
                //}
                //else
                //{
                //    this.txtColorOjos.Text = oDatos[10].ToString();
                //}

                //if (oDatos[11].ToString() == "null")
                //{
                //    this.txtRostro.Text = "";
                //}
                //else
                //{
                //    this.txtRostro.Text = oDatos[11].ToString();
                //}

                //if (oDatos[9].ToString() == "null")
                //{
                //    this.txtCabello.Text = "";
                //}
                //else
                //{
                //    this.txtCabello.Text = oDatos[9].ToString();
                //}

                //if (oDatos[8].ToString() == "null")
                //{
                //    this.txtTipoSangre.Text = "";
                //}
                //else
                //{
                //    this.txtTipoSangre.Text = oDatos[8].ToString();
                //}

                //if (oDatos[13].ToString() == "null")
                //{
                //    this.txtEstCivil.Text = "";
                //}
                //else
                //{
                //    this.txtEstCivil.Text = oDatos[13].ToString();
                //}

                //if (oDatos[14].ToString() == "null")
                //{
                //    this.txtProfesion.Text = "";
                //}
                //else
                //{
                //    this.txtProfesion.Text = oDatos[14].ToString();
                //}

                #endregion

                _iIdPersona = Convert.ToInt64(oDatos[20].ToString());
                Session["idPersona"] = _iIdPersona.ToString();
                this.imgFoto.ImageUrl = "fotoUsuario.aspx";
                this.imgFoto.DataBind();
                //this.imgFoto.Visible = true;

                #region Llenar Tablas
                DataRow oRegistros;
                DataTable oTabLic = new DataTable();
                oTabLic.Columns.Add("Categoría");
                oTabLic.Columns.Add("Fecha/Emisión");
                oTabLic.Columns.Add("Fecha/Caducidad");
                oTabLic.Columns.Add("Tipo de Sangre");

                DataTable oTabBloq = new DataTable();
                oTabBloq.Columns.Add("Fecha");
                oTabBloq.Columns.Add("Descripción");


                DataTable oTabRes = new DataTable();
                oTabRes.Columns.Add("Fecha");
                oTabRes.Columns.Add("Descripción");

                DataTable oTabInfr = new DataTable();
                oTabInfr.Columns.Add("Fecha/Infracción");
                oTabInfr.Columns.Add("Contravención");

                //////////////////////////////////////////////////////////
                #region Llenar tabla categoria de licencias
                DataTable oTabCatLicencias = new DataTable();
                oTabCatLicencias.Columns.Add("Cat.");
                oTabCatLicencias.Columns.Add("Autorización");
                DataRow oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "A";
                oRegCatLicencias[1] = "Para ciclomotores, motocicletas y triciclos motorizados";
                oTabCatLicencias.Rows.Add(oRegCatLicencias);
                oRegCatLicencias = oTabCatLicencias.NewRow();
                oRegCatLicencias[0] = "B";
                oRegCatLicencias[1] = "Para automóviles y camionetas con acoplados de hasta 1.750 kg. de carga útil o casas rodantes";
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
                this.grdCatLicencias.DataSource = oTabCatLicencias;
                this.grdCatLicencias.DataBind();
                #endregion
                /////////////////////////////////////////////////////////


                Int64 NumElem = oDatos.GetLength(0);
                

                for (Int64 iIndice = 21; iIndice < NumElem; iIndice++)
                {
                    switch (((object[])oDatos[iIndice])[0].ToString())
                    {
                        case "LIC":
                            oRegistros = oTabLic.NewRow();
                            oRegistros[0] = (((object[])oDatos[iIndice])[1].ToString());
                            oRegistros[1] = Convert.ToDateTime((((object[])oDatos[iIndice])[2].ToString()));
                            oRegistros[2] = Convert.ToDateTime((((object[])oDatos[iIndice])[3].ToString()));
                            oRegistros[3] = oDatos[8];
                            oTabLic.Rows.Add(oRegistros);
                            break;
                        case "BLO":
                            oRegistros = oTabBloq.NewRow();
                            oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                            oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                            oTabBloq.Rows.Add(oRegistros);
                            break;
                        case "RES":
                            oRegistros = oTabRes.NewRow();
                            oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                            oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                            oTabRes.Rows.Add(oRegistros);
                            break;
                        case "INF":
                            oRegistros = oTabInfr.NewRow();
                            oRegistros[0] = Convert.ToDateTime((((object[])oDatos[iIndice])[1].ToString()));
                            oRegistros[1] = (((object[])oDatos[iIndice])[2].ToString());
                            oTabInfr.Rows.Add(oRegistros);
                            break;
                    }
                }
                this.grdLicencias.DataSource = oTabLic;
                this.grdLicencias.DataBind();
                this.grdBloqueos.DataSource = oTabBloq;
                this.grdBloqueos.DataBind();
                this.grdRestricciones.DataSource = oTabRes;
                this.grdRestricciones.DataBind();
                this.grdInfracciones.DataSource = oTabInfr;
                this.grdInfracciones.DataBind();
                //ShowControls();
                oDatos = null;
                
                #endregion

            }
        }
        catch (Exception ex)
        {
            this.lblMsgError.Text = "No es posible consultar datos de la licencia " + noLicencia;
            this.divError.Visible = true;
        }
    }

    public void HideControls()
    {
        this.grdBloqueos.Visible = false;
        this.grdCatLicencias.Visible = false;
        this.grdInfracciones.Visible = false;
        this.grdLicencias.Visible = false;
        this.grdRestricciones.Visible = false;
        this.imgFoto.Visible = false;
        this.dvDatosPersonales.Visible = false;
    }

    public void ShowControls()
    {
        this.grdBloqueos.Visible = true;
        this.grdCatLicencias.Visible = true;
        this.grdInfracciones.Visible = true;
        this.grdLicencias.Visible = true;
        this.grdRestricciones.Visible = true;
        this.imgFoto.Visible = true;
        this.dvDatosPersonales.Visible = true;
    }

}
