using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// Summary description for Vehiculos
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Vehiculos : System.Web.Services.WebService
{

    public Vehiculos()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public byte[] GetFotoRevision(string placa)
    {
        byte[] foto = null;
        AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "web service wsImagenesCitaciones");
        AccesoDatos.ROracle oDatosImg = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuarioImg"], System.Configuration.ConfigurationManager.AppSettings["claveImg"], System.Configuration.ConfigurationManager.AppSettings["baseImg"], "web service wsImagenesCitaciones");

        try
        {
            string idRevision = string.Empty;

            if (oDatos.Consultar_Imagen("SELECT id_revision FROM st_revision_veh WHERE placa='" + placa.ToUpper() + "' AND fecha_revision = (SELECT MAX(fecha_revision) FROM st_revision_veh WHERE placa='" + placa.ToUpper() + "')"))
            {
                if (oDatos.oDataReader.Read())
                {
                    idRevision = oDatos.oDataReader.GetValue(0).ToString();
                }

                if (idRevision != string.Empty)
                {
                    if (oDatosImg.Consultar_Imagen("SELECT foto FROM st_imagen_revision WHERE id_revision='" + idRevision + "'"))
                    {
                        if (oDatosImg.oDataReader.Read())
                        {
                            Oracle.DataAccess.Types.OracleBlob blob = oDatosImg.oDataReader.GetOracleBlob(0);
                            foto = blob.Value;
                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
        }
        finally
        {
            oDatos.Dispose();
            oDatosImg.Dispose();
        }
        return foto;
    }

}

