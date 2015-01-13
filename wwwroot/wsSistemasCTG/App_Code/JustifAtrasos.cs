using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;


/// <summary>
/// Summary description for JustifAtrasos
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class JustifAtrasos : System.Web.Services.WebService
{

    public JustifAtrasos()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void HoraEntradaMarcada(string codEmpleado, string fecha, out string horaEntradaMarc, out string nombreEmp, out string cargoEmp, out string nombreJefe, out int codJefe, out string spUserJefe, out string nombreDireccion, out string nombreDpto)
    {
        Biometrico.Marcaciones objMarcaciones = new Biometrico.Marcaciones(FormatCodEmpleado(codEmpleado), 
            System.Configuration.ConfigurationManager.AppSettings["usuarioBiom"], 
            System.Configuration.ConfigurationManager.AppSettings["claveBiom"], 
            System.Configuration.ConfigurationManager.AppSettings["baseBiom"]);
        //string horaEntrada = objMarcaciones.GetHoraEntrada(DateTime.Parse(fecha, new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false)));
        string horaEntrada = objMarcaciones.GetHoraEntrada(Convert.ToDateTime(fecha));
        //string horaEntrada = objMarcaciones.GetHoraEntrada(fecha);
        if (horaEntrada != null)
            horaEntradaMarc = horaEntrada;
        else
            horaEntradaMarc = objMarcaciones.Error;

        DatosContratos objDatCont = new DatosContratos();
        objDatCont.GetNombreAndCargoEmpleado(int.Parse(codEmpleado), out nombreEmp, out cargoEmp);

        int codDirArea, codDpto, codDirector;
        string nomDirector, spUserDirector;

        ParaHorasExtras objHorExt = new ParaHorasExtras();
        objHorExt.ConsultaDatosEmpleado(codEmpleado, fecha, out nombreEmp, out codDirArea, out nombreDireccion, out codDpto, out nombreDpto, out codJefe, out nombreJefe,
            out codDirector, out nomDirector, out spUserJefe, out spUserDirector);
    }

    [WebMethod]
    public string HoraEntrada(string codEmpleado, string fecha)
    {
        Biometrico.Marcaciones objMarcaciones = new Biometrico.Marcaciones(FormatCodEmpleado(codEmpleado),
            System.Configuration.ConfigurationManager.AppSettings["usuarioBiom"],
            System.Configuration.ConfigurationManager.AppSettings["claveBiom"],
            System.Configuration.ConfigurationManager.AppSettings["baseBiom"]);
        //string horaEntrada = objMarcaciones.GetHoraEntrada(DateTime.Parse(fecha, new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false)));
        string horaEntrada = objMarcaciones.GetHoraEntrada(Convert.ToDateTime(fecha));
        //string horaEntrada = objMarcaciones.GetHoraEntrada(fecha);
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

