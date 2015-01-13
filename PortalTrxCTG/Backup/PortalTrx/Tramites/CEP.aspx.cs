using System;
using System.Configuration;
using System.Web;
using System.Collections.Generic;
using System.Data;
using ComprobanteElectronicoPago;


public partial class Tramites_CEP : System.Web.UI.Page
{
    private string _numSolicitud;
    private string _valorPago;
    private string _codModoEntrega;
    private string _valorPagoCde;
    private string _codTramiteProceso;
    private string _codOficina;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ReadNumSolicitudEnc())
            {
                if (!string.IsNullOrWhiteSpace(_numSolicitud))
                {
                    if (!GeneraCEP(_numSolicitud))
                        ConsultaCEP(_numSolicitud);
                    btnPrint.OnClientClick = "javascript:launchPopUp('" + pnlCEP.ClientID + "');";
                }
                else
                    HtmlWriter.Messages.ShowMainContentError(Master, divContent, "La solicitud del trámite es incorrecta, intente solicitando el trámite nuevamente por favor.");
            }
            else
                HtmlWriter.Messages.ShowMainContentError(Master, divContent, "Error al leer solicitud, por favor realice nuevamente la solicitud.");
        }
    }


    private bool ReadNumSolicitudEnc()
    {
        try
        {
            CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
            objCrypto.Key = Constantes.ParametrosCifradoCs.key;
            objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
            _numSolicitud = objCrypto.DescifrarCadena(Utilities.Utils.DecodeFrom64(Session[Constantes.WebApp.DatosSesion.CodigoSolicitudTramite.ToString()].ToString()));
            _valorPago = objCrypto.DescifrarCadena(Utilities.Utils.DecodeFrom64(Session[Constantes.WebApp.DatosSesion.ValorTramite.ToString()].ToString()));
            _codModoEntrega = objCrypto.DescifrarCadena(Utilities.Utils.DecodeFrom64(Session[Constantes.WebApp.DatosSesion.ModoEntregaTramite.ToString()].ToString()));
            _valorPagoCde = objCrypto.DescifrarCadena(Utilities.Utils.DecodeFrom64(Session[Constantes.WebApp.DatosSesion.CostoCorreosDelEcuador.ToString()].ToString()));
            _codTramiteProceso = objCrypto.DescifrarCadena(Utilities.Utils.DecodeFrom64(Session[Constantes.WebApp.DatosSesion.CodigoProceso.ToString()].ToString()));
            _codOficina = objCrypto.DescifrarCadena(Utilities.Utils.DecodeFrom64(Session[Constantes.WebApp.DatosSesion.CodigoOficina.ToString()].ToString()));
            return true;
        }
        catch
        {
            _numSolicitud = string.Empty;
            _valorPago = string.Empty;
            return false;
        }
    }


    private void ConsultaCEP(string numSolicitud)
    {
        CEP objCEP = new CEP(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        if (objCEP.ConsultaCEP(numSolicitud, HttpContext.Current.User.Identity.Name))
            PrintCEP(objCEP);
        else //error al generar cep
        {
            pnlCEP.Visible = false;
            HtmlWriter.Messages.ShowMainContentError(Master, divContent, objCEP.Error);
        }
    }


    private List<string> GetDocumentosPresentar(string codTramiteProceso)
    {
        Constantes.Tramites oTramite = new Constantes.Tramites(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        return oTramite.GetDocumentacionPresentarPorTramite(codTramiteProceso);
    }

    private void PrintInfoMessages(string mensaje, List<string> requisitos)
    {
        DataTable dtMsg = new DataTable();
        dtMsg.Columns.Add("Mensaje");
        if (!string.IsNullOrWhiteSpace(mensaje))
        {
            lblMensaje1.Text = mensaje;
        }
        foreach (string doc in requisitos)
        {
            DataRow dr = dtMsg.NewRow();
            dr[0] = doc;
            dtMsg.Rows.Add(dr);
        }
        if (dtMsg.Rows.Count > 0)
            repMessages.Visible = true;
        else
            repMessages.Visible = false;
        repMessages.DataSource = dtMsg;
        repMessages.DataBind();
    }




    private bool GeneraCEP(string numSolicitud)
    {
        CEP objCEP = new CEP(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        if (objCEP.GeneraCEP(numSolicitud, HttpContext.Current.User.Identity.Name, _codModoEntrega, _valorPagoCde, _codOficina))
        {
            PrintCEP(objCEP);
            return true;
        }
        else //error al generar cep
            return false;
    }


    private void PrintCEP(CEP objCEP)
    {

        pnlCEP.Visible = true;
        lblCEP.Text = objCEP.codigoCEP;
        lblTramite.Text = objCEP.Tramite;
        lblUsuario.Text = objCEP.NombreUsuario;
        lblFecha.Text = DateTime.Now.ToString();
        lblIdentificacion.Text = objCEP.IdentificacionUsuario;
        double numValorPago = 0;
        double numValorPagoCDE = 0;
        try { numValorPago = Convert.ToDouble(_valorPago.Replace(',', '.')); }//servidor web de producción utiliza . como separador decimal
        catch { }
        try { numValorPagoCDE = Convert.ToDouble(_valorPagoCde.Replace(',', '.')); }//servidor web de producción utiliza . como separador decimal
        catch { }
        double valorTotPago = numValorPago + numValorPagoCDE;
        lblValorPago.Text = "$ " + Utilities.Utils.FormatStringNumber(valorTotPago.ToString(), '.'/*','*/, Constantes.Tramites.DecimalLong).Replace('.',',');
        PrintInfoMessages(objCEP.Mensaje, GetDocumentosPresentar(_codTramiteProceso));

    }

}