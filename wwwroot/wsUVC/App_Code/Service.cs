using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public DataTable GetUVCsLocation()
    {
        DatosUVC.DatosRepUVC oDatUVC = new DatosUVC.DatosRepUVC(ConfigurationManager.AppSettings["usuario"],
            ConfigurationManager.AppSettings["clave"],
            ConfigurationManager.AppSettings["tns"]);
        DataTable dtDelegac = oDatUVC.UbicacionesUVC();
        return dtDelegac;
    }
    
}