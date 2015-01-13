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
using ComprobanteElectronicoPago;


public partial class Pagos_CEP : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Seguridad.UsuarioWeb.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/DefaultPagos.aspx");
            }

            //if (Session[Constantes.UsuarioWeb.SessionVarNameFullAccess].ToString() == Constantes.UsuarioWeb.SessionLimitedAccess)
            //{
            //    Response.Redirect("~/sinPermiso.aspx");
            //}

            if (Session["tramaSolicTramite"] != null)
            {
                CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
                objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                string no_solicitud = objCrypto.DescifrarCadena(Session["tramaSolicTramite"].ToString());
                this.lblValorPago.Text = "$ " + objCrypto.DescifrarCadena(Session["valorCEP"].ToString());
                GeneraCEP(no_solicitud);
            }
            else
            {
                Response.Redirect("Requisitos.aspx");
            }
            
        }
    }

    protected void GeneraCEP(string NumSolicitud)
    {
        CEP objCEP = new CEP(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        if (objCEP.GeneraCEP(NumSolicitud, User.Identity.Name.ToString()))
        {
            this.divCepNoGenerado.Visible = false;
            this.lblCEP.Text = objCEP.codigoCEP;
            this.lblTramite.Text = objCEP.Tramite;
            this.lblUsuario.Text = objCEP.NombreUsuario;
            this.lblFecha.Text = DateTime.Now.ToString();
            this.lblIdentificacion.Text = objCEP.IdentificacionUsuario;
            this.lblMensaje1.Text = Constantes.Tramites.msgExpiracionCEP;
            this.divCepGenerado.Visible = true;
            this.linksBancos.Visible = true;
            PrintRequiredDocuments(Session["codTramite"].ToString());
        }
        else //error al generar cep
        {
            this.divCepGenerado.Visible = false;
            this.linksBancos.Visible = false;
            this.divCepNoGenerado.Visible = true;
            this.lblMsgError.Text = "Error al generar código de pago. " + objCEP.Error;
        }
    }

    protected void btnImprimir_Click(object sender, ImageClickEventArgs e)
    {
        //CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        //objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        //objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        //Session["cepCEP"] = objCrypto.CifrarCadena(this.lblCEP.Text);
        //Session["cepTramite"] = objCrypto.CifrarCadena(this.lblTramite.Text);
        //Session["cepUsuario"] = objCrypto.CifrarCadena(this.lblUsuario.Text);
        //Session["cepValor"] = objCrypto.CifrarCadena(this.lblValorPago.Text);
        //Session["cepFecha"] = objCrypto.CifrarCadena(this.lblFecha.Text);
        //Session["cepIdUsuario"] = objCrypto.CifrarCadena(this.lblIdentificacion.Text);

        //Page.RegisterClientScriptBlock("MyScript",
        //   "<script language=javascript>" +
        //   "popup('PrintCEP.aspx','515','490'); " +
        //   "</script>");
        Page.RegisterClientScriptBlock("MyScript",
                    "<script language=javascript>" +
                    "window.print();" +
                    "</script>");

    }

    protected void PrintRequiredDocuments(string codTramite)
    {
        string pre_requisitos = "<br />Cuando se acerque a la CTG a retirar el documento solicitado, además de su recibo de pago, deberá presentar lo siguiente:";
        switch (codTramite)
        {
            case "ATU_GRAPS1":
                this.lblRequiredDocs.Text = pre_requisitos + Constantes.Tramites.RequiredDocsForCertGravamen;
                break;
            case "ATU_PPCPS1":
                this.lblRequiredDocs.Text = pre_requisitos + Constantes.Tramites.RequiredDocsForPermCirculacion;
                break;
            case "ATU_NPVPS1":
                this.lblRequiredDocs.Text = pre_requisitos + Constantes.Tramites.RequiredDocsForCertPosVehiculos;
                break;
            case "BRE_RLPS1":
                this.lblRequiredDocs.Text = pre_requisitos + Constantes.Tramites.RequiredDocsForRenovLicencia;
                break;
            case "BRE_DLPS1":
                this.lblRequiredDocs.Text = pre_requisitos + Constantes.Tramites.RequiredDocsForDupLicencia;
                break;
            case "JPG_DCIPS1":
                this.lblRequiredDocs.Text = pre_requisitos + Constantes.Tramites.RequiredDocsForDupCitacion;
                break;
            case "JPG_CCCPS1":
                this.lblRequiredDocs.Text = pre_requisitos + Constantes.Tramites.RequiredDocsForCambColor;
                break;
            case "JPG_DUMPS1":
                this.lblRequiredDocs.Text = pre_requisitos + Constantes.Tramites.RequiredDocsForDupMatricula;
                break;
            default:
                this.lblRequiredDocs.Text = "";
                break;
        }
    }
}
