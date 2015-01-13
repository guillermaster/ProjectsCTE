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

public partial class Pagos_PrintCEP : System.Web.UI.Page
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
                this.lblCEP.Text = objCrypto.DescifrarCadena(Session["cepCEP"].ToString());
                this.lblTramite.Text = objCrypto.DescifrarCadena(Session["cepTramite"].ToString());
                this.lblUsuario.Text = objCrypto.DescifrarCadena(Session["cepUsuario"].ToString());
                this.lblFecha.Text = objCrypto.DescifrarCadena(Session["cepFecha"].ToString());
                this.lblIdentificacion.Text = objCrypto.DescifrarCadena(Session["cepIdUsuario"].ToString());
                this.lblValor.Text = "$ " + objCrypto.DescifrarCadena(Session["valorCEP"].ToString());
                this.lblMensaje.Text = Constantes.Tramites.msgExpiracionCEP;
                Page.RegisterClientScriptBlock("MyScript",
                    "<script language=javascript>" +
                    "window.print();" +
                    "</script>");
            }
            else
            {
                Response.Redirect("Requisitos.aspx");
            }

        }
    }
}
