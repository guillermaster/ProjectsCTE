using System;
using System.Data;
using System.Configuration;
using System.Web;
using Matriculacion;
using CifradoCs;

public partial class Consultas_Vehiculos_Vehiculos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ConsultarVehiculos(HttpContext.Current.User.Identity.Name);
    }


    protected void ConsultarVehiculos(string id)
    {
        MatriculacionVehicular oMatric = new MatriculacionVehicular(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtVehiculos = oMatric.VehiculosPorPersona(id);

        if (string.IsNullOrWhiteSpace(oMatric.Error))
        {
            HtmlWriter.Messages.HideMainContentError(Master, divMain);
            if (dtVehiculos.Rows.Count > 0)
            {
                gvVehiculos.DataSource = dtVehiculos;
                gvVehiculos.DataBind();
                gvVehiculos.Visible = true;
            }
            else
            {
                divContent.Controls.Add(HtmlWriter.Messages.PrintInfoMessage("Usted no posee ningún vehículo registrado a su nombre en la CTE"));
                gvVehiculos.Visible = false;
            }
        }
        else
            HtmlWriter.Messages.ShowMainContentError(Master, divMain, oMatric.Error);
    }

    protected void gvVehiculos_SelectedIndexChanged(object sender, EventArgs e)
    {
        Crypto objCrypto = new Crypto(Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        string placaEnc = Utilities.Utils.EncodeToBase64(objCrypto.CifrarCadena(gvVehiculos.SelectedRow.Cells[1].Text));
        Response.RedirectPermanent("VehiculoDetInfo.aspx?placaenc="+placaEnc);
    }
}