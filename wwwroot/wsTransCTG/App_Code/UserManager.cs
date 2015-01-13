using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for UserManager
/// </summary>
public class UserManager
{
    private string motivo;
    public UserManager()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public bool Authenticate(string username, string password)
    {
        bool retValue = false;
        AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "wsTransCTG UserManager.cs Authenticate");

        oDatos.Paquete(Constantes.StoredProcedures.LoginBanco);
        oDatos.Parametro("PV_COD_BANCO", username);
        oDatos.Parametro("PV_PASSWORD", password);//se envia desencriptado, en la base se encuentra guardado con encriptacion MD5
        oDatos.Parametro("PN_ERROR", "V", 1, "O");// retorna 1 para login exitoso, retorna 0 para password incorrecto
        oDatos.Parametro("PV_MENSAJE", "V", 200, "O");

        try
        {
            if (oDatos.Ejecutar("R"))
            {
                motivo = oDatos.RetornarParametro("PV_MENSAJE").ToString();
                if (oDatos.RetornarParametro("PN_ERROR").ToString() == "1")
                    retValue = true;
            }
            else
            {
                motivo = Constantes.WebService.ErrorConexionDB;
            }
        }
        catch(Exception ex)
        {
            motivo = ex.Message;
        }
        finally
        {
            oDatos.Dispose();
        }
        return retValue;
        //if (username == "26" && password == "BcoPacificoCTGtransWS")
        //    return true;
        //else
        //    return false;
    }
}
