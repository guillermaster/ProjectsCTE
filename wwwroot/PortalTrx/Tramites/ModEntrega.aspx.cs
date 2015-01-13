using System;
using System.Data;
using System.Configuration;
using System.Web;

public partial class Tramites_ModEntrega : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ReadNumSolicitudEnc())
            {
                SetTipoTramite(hdnCodProcesoTramite.Value);
                if (SetModosEntregas())
                    NewShippingAddress1.CostoTramite = hdnValorPago.Value;
                else
                    HtmlWriter.Messages.ShowMainContentError(Master, divContent, "Error al cargar modos de entrega, intente cargar nuevamente esta página");
            }
            else
                HtmlWriter.Messages.ShowMainContentError(Master, divContent, "Error al leer solicitud, por favor realice nuevamente la solicitud.");
        }
    }


    protected void SetTipoTramite(string codTramite)
    {
        Constantes.Tramites oTramite = new Constantes.Tramites(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        hdnTipoTramite.Value = oTramite.GetTipoTramite(codTramite);
    }

    protected bool SetModosEntregas()
    {
        ComprobanteElectronicoPago.CEP oCEP = new ComprobanteElectronicoPago.CEP(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;

        DataTable dtModosEnt = oCEP.ModosEntregaDisponibles(objCrypto.DescifrarCadena(Utilities.Utils.DecodeFrom64(Session[Constantes.WebApp.DatosSesion.CodigoProceso.ToString()].ToString())));
        if (dtModosEnt.Rows.Count > 0)
        {
            rbtnModoEntrega.DataSource = dtModosEnt;
            rbtnModoEntrega.DataValueField = dtModosEnt.Columns[0].ColumnName;
            rbtnModoEntrega.DataTextField = dtModosEnt.Columns[1].ColumnName;
            rbtnModoEntrega.DataBind();
            return true;
        }
        else
            return false;
    }



    protected void CargarProvinciasOficinasCTG(string codModoEntrega)
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        if (codModoEntrega == ComprobanteElectronicoPago.CEP.ModosEntrega.CDE.ToString())
        {
            lblCdeEntregaDocs.Visible = true;
            lblOficCtgNota.Visible = false;
            lblOficCtgTitle.Visible = false;
        }
        else if (codModoEntrega == ComprobanteElectronicoPago.CEP.ModosEntrega.CTG.ToString())
        {
            lblCdeEntregaDocs.Visible = false;
            lblOficCtgNota.Visible = true;
            lblOficCtgTitle.Visible = true;
        }

        DataView dvProvincias = geog.GetProvinciasConCodigos(Constantes.Geografia.CodigoEcuador).DefaultView;
       dvProvincias.Sort = string.Format("{0} {1}", dvProvincias.Table.Columns[1].ColumnName, "ASC");

        DataRow nullrow = dvProvincias.Table.NewRow();
        nullrow[0] = null;
        nullrow[1] = null;

        dvProvincias.Table.Rows.InsertAt(nullrow, 0);

        ddlProvinciasOficinas.DataSource = dvProvincias;
        ddlProvinciasOficinas.DataValueField = dvProvincias.Table.Columns[0].ColumnName;
        ddlProvinciasOficinas.DataTextField = dvProvincias.Table.Columns[1].ColumnName;
        ddlProvinciasOficinas.DataBind();
    }



    protected void CargarOficinasCTE(string codModoEntrega, string codDptoCTG, string codProvincia)
    {
        Constantes.Tramites oTramite = new Constantes.Tramites(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        /*if (codModoEntrega == ComprobanteElectronicoPago.CEP.ModosEntrega.CDE.ToString())
        {
                lblCdeEntregaDocs.Visible = true;
                lblOficCtgNota.Visible = false;
                lblOficCtgTitle.Visible = false;
        }else if(codModoEntrega == ComprobanteElectronicoPago.CEP.ModosEntrega.CTG.ToString())
        {
                lblCdeEntregaDocs.Visible = false;
                lblOficCtgNota.Visible = true;
                lblOficCtgTitle.Visible = true;
        }*/

        DataTable dtOficinasATX = oTramite.GetOficinasCTG(codDptoCTG, codProvincia);
        ddlOficinas.DataSource = dtOficinasATX;
        ddlOficinas.DataValueField = dtOficinasATX.Columns[0].ColumnName;
        ddlOficinas.DataTextField = dtOficinasATX.Columns[1].ColumnName;
        ddlOficinas.DataBind();
        ddlOficinas.Visible = true;
        //HtmlWriter.Messages.ShowMainContentError(Master, divContent, oTramite.Error);
        lblOficCtgTitle.Text = oTramite.Error;
    }


    protected void btnContinuar_Click(object sender, EventArgs e)
    {
        Page.Validate();
        if (Page.IsValid)
        {
            if (rbtnModoEntrega.SelectedValue == ComprobanteElectronicoPago.CEP.ModosEntrega.CDE.ToString())
            {
                if (SaveAddressCorreosDelEcuador(hdnNumSolicitud.Value, hdnCodProcesoTramite.Value, HttpContext.Current.User.Identity.Name))
                    GenerarCEP();
                else
                    HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), "Error al registrar dirección de envío, por favor intente nuevamente.");
            }
            else
            {
                GenerarCEP();
            }
        }
    }

    private bool SaveAddressCorreosDelEcuador(string numSolicitud, string idProceso, string numIdentificacion)
    {
        CorreosDelEcuador.Shipping oShipping = new CorreosDelEcuador.Shipping(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        string direccion = string.Empty;

        if (string.IsNullOrWhiteSpace(NewShippingAddress1.CodCiudad))
            direccion = NewShippingAddress1.Ciudad;
        if (string.IsNullOrWhiteSpace(NewShippingAddress1.CodProvincia))
            direccion = NewShippingAddress1.Provincia + " " + direccion;

        if (string.IsNullOrWhiteSpace(direccion))
            direccion = NewShippingAddress1.Direccion;
        else
            direccion += " " + NewShippingAddress1.Direccion;

        if (oShipping.SaveAddress(numSolicitud, idProceso, numIdentificacion, NewShippingAddress1.NombrePersona1, NewShippingAddress1.NombrePersona2, direccion,
            NewShippingAddress1.DireccionReferencia, NewShippingAddress1.TelefonoConv, NewShippingAddress1.TelefonoMovil, NewShippingAddress1.CodCiudad,
            NewShippingAddress1.CodProvincia, NewShippingAddress1.CodPais, NewShippingAddress1.DireccionReferencia))
            return true;
        else
            return false;
    }

    private bool SaveModEntregaEnc(string codModentrega, string valorPagarCDE, string idOficina)
    {
        try
        {
            CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
            objCrypto.Key = Constantes.ParametrosCifradoCs.key;
            objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
            Session[Constantes.WebApp.DatosSesion.ModoEntregaTramite.ToString()] = Utilities.Utils.EncodeToBase64(objCrypto.CifrarCadena(codModentrega));
            Session[Constantes.WebApp.DatosSesion.CostoCorreosDelEcuador.ToString()] = Utilities.Utils.EncodeToBase64(objCrypto.CifrarCadena(valorPagarCDE));
            Session[Constantes.WebApp.DatosSesion.CodigoOficina.ToString()] = Utilities.Utils.EncodeToBase64(objCrypto.CifrarCadena(idOficina));
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void GenerarCEP()
    {
        if (SaveModEntregaEnc(rbtnModoEntrega.SelectedValue, NewShippingAddress1.CostoFlete, ddlOficinas.SelectedValue))
            Response.Redirect("CEP.aspx", true);
        else
            HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), "Se ha generado un error, por favor intente nuevamente.");
    }


    private bool ReadNumSolicitudEnc()
    {
        try
        {
            CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
            objCrypto.Key = Constantes.ParametrosCifradoCs.key;
            objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
            hdnNumSolicitud.Value = objCrypto.DescifrarCadena(Utilities.Utils.DecodeFrom64(Session[Constantes.WebApp.DatosSesion.CodigoSolicitudTramite.ToString()].ToString()));
            hdnValorPago.Value = objCrypto.DescifrarCadena(Utilities.Utils.DecodeFrom64(Session[Constantes.WebApp.DatosSesion.ValorTramite.ToString()].ToString()));
            hdnCodProcesoTramite.Value = objCrypto.DescifrarCadena(Utilities.Utils.DecodeFrom64(Session[Constantes.WebApp.DatosSesion.CodigoProceso.ToString()].ToString()));
            return true;
        }
        catch
        {
            hdnNumSolicitud.Value = string.Empty;
            hdnValorPago.Value = string.Empty;
            hdnCodProcesoTramite.Value = string.Empty;
            return false;
        }
    }

    private bool TramiteRequiereUsuarioEntregeDocs(string codTramite)
    {
        Constantes.Tramites oTramite = new Constantes.Tramites(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        return oTramite.TramiteRequiereEntregaDocumentos(codTramite);
    }


    protected void NewShippingAddress1_Evento(object sender, EventArgs e)
    {
    }


    protected void rbtnModoEntrega_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnContinuar.Visible = true;

        //HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), rbtnModoEntrega.SelectedValue);

        if (rbtnModoEntrega.SelectedValue == ComprobanteElectronicoPago.CEP.ModosEntrega.CDE.ToString())
        {
            NewShippingAddress1.Visible = true;
            lblOficCtgTitle.Text = "Seleccione el lugar donde va a entregar la documentación requerida";
            if (TramiteRequiereUsuarioEntregeDocs(hdnCodProcesoTramite.Value))
            {
                pnlOficinasCTG.Visible = true;
                //CargarOficinasCTE(rbtnModoEntrega.SelectedValue, hdnTipoTramite.Value);
                CargarProvinciasOficinasCTG(rbtnModoEntrega.SelectedValue);
            }
        }
        else if(rbtnModoEntrega.SelectedValue == ComprobanteElectronicoPago.CEP.ModosEntrega.CTG.ToString())
        {
            NewShippingAddress1.Visible = false;
            pnlOficinasCTG.Visible = true;
            lblOficCtgTitle.Text = "Seleccione el lugar donde desea retirar su trámite";
            //CargarOficinasCTE(rbtnModoEntrega.SelectedValue, hdnTipoTramite.Value);
            CargarProvinciasOficinasCTG(rbtnModoEntrega.SelectedValue);
        }
    }

    protected void ddlProvinciasOficinas_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarOficinasCTE(rbtnModoEntrega.SelectedValue, hdnCodProcesoTramite.Value, ddlProvinciasOficinas.SelectedValue);
    }
}