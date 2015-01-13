using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// Summary description for HoraEntrada
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class HoraEntrada : System.Web.Services.WebService
{

    //public HoraEntrada()
    //{

    //    //Uncomment the following line if using designed components 
    //    //InitializeComponent(); 
    //}

    [WebMethod]
    public string HoraEntradaMarcada(string codEmpleado, string fecha)
    {
        Biometrico.Marcaciones objMarcaciones = new Biometrico.Marcaciones(FormatCodEmpleado(codEmpleado), System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
        string horaEntrada = objMarcaciones.GetHoraEntrada(DateTime.Parse(fecha, new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false)));
        if (horaEntrada != null)
            return horaEntrada;
        else
            return objMarcaciones.Error;
    }

    public string FormatCodEmpleado(string codEmpleado)
    {
        string formatedCode;
        int length = 5;
        if ((int.Parse(codEmpleado)).ToString().Length < 5)
        {
            formatedCode = "";
            for (int i = 0; i < (length - codEmpleado.Length); i++)
                formatedCode += "0";
            formatedCode += codEmpleado;
        }
        else
            formatedCode = codEmpleado;

        return formatedCode;
    }

}

