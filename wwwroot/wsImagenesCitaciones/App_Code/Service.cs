using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public byte[] GetCitacImage(string codCitacion) {
        byte[] foto;
        AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuarioImg"], System.Configuration.ConfigurationManager.AppSettings["claveImg"], System.Configuration.ConfigurationManager.AppSettings["baseImg"], "web service wsImagenesCitaciones");
        try
        {
            oDatos.Consultar_Imagen("SELECT imagen FROM FA_CITACIONES_IMAGENES WHERE codigo = " + codCitacion);
            if (oDatos.oDataReader.Read())
            {
                Oracle.DataAccess.Types.OracleBlob blob = oDatos.oDataReader.GetOracleBlob(0);
                foto = blob.Value;
            }
            else
            {
                foto = null;
            }
        }
        catch (Exception ex)
        {
            foto = null;
        }
        finally
        {
            oDatos.Dispose();
        }
        return foto;
    }

    [WebMethod]
    public byte[] GetCitacImageHandHeld(string codCitacion)
    {
        byte[] foto;
        AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "web service wsImagenesCitaciones");
        try
        {
            oDatos.Consultar_Imagen("SELECT foto FROM axisctg.st_fotos_citaciones WHERE sec_libretines ='" + codCitacion + "' AND es_firma ='N'");
            if (oDatos.oDataReader.Read())
            {
                Oracle.DataAccess.Types.OracleBlob blob = oDatos.oDataReader.GetOracleBlob(0);
                foto = blob.Value;
            }
            else
            {
                foto = null;
            }
        }
        catch (Exception ex)
        {
            foto = null;
        }
        finally
        {
            oDatos.Dispose();
        }
        return foto;
    }

    [WebMethod]
    public byte[] GetCitacTipoImage(string codCitacion, string tipo)
    {
        byte[] foto;
        AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuarioImg"], 
            System.Configuration.ConfigurationManager.AppSettings["claveImg"], 
            System.Configuration.ConfigurationManager.AppSettings["baseImg"], "web service wsImagenesCitaciones");
        try
        {
            oDatos.Consultar_Imagen("SELECT imagen FROM FA_CITACIONES_IMAGENES WHERE codigo = " + codCitacion + "AND tipo='" + tipo + "'");
            if (oDatos.oDataReader.Read())
            {
                Oracle.DataAccess.Types.OracleBlob blob = oDatos.oDataReader.GetOracleBlob(0);
                foto = blob.Value;
            }
            else
            {
                foto = null;
            }
        }
        catch (Exception ex)
        {
            foto = null;
        }
        finally
        {
            oDatos.Dispose();
        }
        return foto;
    }

    [WebMethod]
    public byte[] GetLicenciaUserImage(string idPersona)
    {
        byte[] foto;
        AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "web service wsImagenesCitaciones GetLicenciaUserImage");
        try
        {
            oDatos.Consultar_Imagen("select imagen from CL_IMAGENES where id_persona = " + idPersona + " and id_imagen = 'FOT'");
            if (oDatos.oDataReader.Read())
            {
                Oracle.DataAccess.Types.OracleBlob blob = oDatos.oDataReader.GetOracleBlob(0);
                foto = blob.Value;
            }
            else
            {
                foto = null;
            }
        }
        catch (Exception ex)
        {
            foto = null;
        }
        finally
        {
            oDatos.Dispose();
        }
        return foto;
    }
    
}
