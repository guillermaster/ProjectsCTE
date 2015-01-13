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
using CifradoCs;
using Seguridad;

public partial class ValidateRegistration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ValidarUsuario(Request.QueryString[Constantes.UsuarioWeb.URLParamEncIdent], Request.QueryString[Constantes.UsuarioWeb.URLParamIdent]))
            {
                this.lblMensaje.Text = "Su usuario ha sido registrado y activado exitósamente.";
            }
            else
            {
                this.lblMensaje.Text = "No se pudo activar su cuenta.";
                this.lblMensaje.ForeColor = System.Drawing.Color.Red;
            }
        }
    }

    protected bool ValidarUsuario(string encIdent, string Ident)
    {
        Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        try
        {
            if (Ident == objCrypto.DescifrarCadena(Utilities.Utils.DecodeFrom64(encIdent)))
            {
                UsuarioWeb newuser = new UsuarioWeb(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], Ident);
                if (newuser.ActualizaDato(Constantes.UsuarioWeb.DBFieldNameEstadoRegistro, Constantes.UsuarioWeb.EstadoActivo))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
