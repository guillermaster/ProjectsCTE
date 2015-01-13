using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using AccesoDatos;


/// <summary>
/// Summary description for RubrosContratos
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class RubrosContratos : System.Web.Services.WebService
{

    public RubrosContratos()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    //retorna el RMU del último sueldo recibido por un empleado
    [WebMethod]
    public double GetRMU(string numCedula)
    {
        double rmu = 0;

        string query = "select val_real from kactus.nm_acumu WHERE cod_conc='1' and cod_empl='" + numCedula + "' ORDER BY fec_fina DESC";
        ROracle oDatos = new ROracle(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            if (oDatos.EjecutarQuery(query))
            {
                if (oDatos.oDataReader.Read())
                {
                    rmu = double.Parse(oDatos.oDataReader.GetValue(0).ToString());
                }
            }
            else
                rmu = -1;
        }
        catch (Exception ex)
        {
            rmu = -1;
        }
        finally
        {
            oDatos.Dispose();
        }
        return rmu;
    }

    //retorna la sumatoria de descuentos en un determinado mes
    [WebMethod]
    public double GetDescuentos(string numCedula, int mes, int anio)
    {
        double desc = 0;
        string queryDateFormat = "ALTER SESSION SET NLS_DATE_FORMAT = 'DD/MM/YYYY'";
        string query = "SELECT SUM(a.val_real) as egresos FROM kactus.nm_acumu a, kactus.nm_conce c "
        + "WHERE c.tip_conc='E' AND c.cod_conc NOT IN (201, 209, 221) AND a.cod_conc=c.cod_conc " 
        + "AND a.cod_empl='" + numCedula + "' AND a.fec_inic='01/" + mes.ToString() + "/" + anio.ToString() + "'";
        ROracle oDatos = new ROracle(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            if (oDatos.EjecutarQuery(queryDateFormat))
            {
                if (oDatos.EjecutarQuery(query))
                {
                    if (oDatos.oDataReader.Read())
                    {
                        desc = double.Parse(oDatos.oDataReader.GetValue(0).ToString());
                    }
                }
                else
                    desc = 0;
            }
        }
        catch (Exception ex)
        {
            desc = 0;
        }
        finally
        {
            oDatos.Dispose();
        }
        return desc;
    }


    [WebMethod]
    public double GetAporteIESS(string numCedula)
    {
        double iess = 0;

        string query = "select val_real from kactus.nm_acumu WHERE cod_conc='201' and cod_empl='" + numCedula + "' ORDER BY fec_fina DESC";
        ROracle oDatos = new ROracle(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            if (oDatos.EjecutarQuery(query))
            {
                if (oDatos.oDataReader.Read())
                {
                    iess = double.Parse(oDatos.oDataReader.GetValue(0).ToString());
                }
            }
            else
                iess = -1;
        }
        catch (Exception ex)
        {
            iess = -1;
        }
        finally
        {
            oDatos.Dispose();
        }
        return iess;
    }

    [WebMethod]
    public double GetDietas(string numCedula, string fechaInicio, string fechaFin)
    {
        double dietas = 0;

        string query = "select nvl(sum(val_tota),0) from kactus.nm_acumu WHERE cod_conc='5' AND cod_empl='" + numCedula + "' AND fec_fina >= '" + fechaInicio + "' AND fec_fina<='" + fechaFin + "'";
        ROracle oDatos = new ROracle(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            if (oDatos.EjecutarQuery(query))
            {
                if (oDatos.oDataReader.Read())
                {
                    try
                    {
                        dietas = double.Parse(oDatos.oDataReader.GetValue(0).ToString());
                    }
                    catch (Exception ex)
                    {
                        dietas = 0;
                    }
                }
            }
            else
                dietas = -1;
        }
        catch (Exception ex)
        {
            dietas = -1;
        }
        finally
        {
            oDatos.Dispose();
        }
        return dietas;
    }

    [WebMethod]
    public void GetRmuIessDietas(string numCedula, string fechaInicioDietas, string fechaFinDietas,
        out double rmu, out double iess, out double dietas, out string nombreEmpleado)
    {
        rmu = GetRMU(numCedula);
        iess = GetAporteIESS(numCedula);
        //dietas = GetDietas(numCedula, fechaInicioDietas, fechaFinDietas);
        dietas = 0;
        DatosContratos wsDatCont = new DatosContratos();
        nombreEmpleado = wsDatCont.getNombreEmpleado(numCedula);
    }

    [WebMethod]
    public void GetRmuIessDietasParaDecImpRenta(string numCedula,
        out double rmu, out double iess, out double dietas, out string nombreEmpleado)
    {
        rmu = GetRMU(numCedula);
        iess = GetAporteIESS(numCedula);
        //dietas = GetDietas(numCedula, fechaInicioDietas, fechaFinDietas);
        dietas = 0;
        DatosContratos wsDatCont = new DatosContratos();
        nombreEmpleado = wsDatCont.getNombreEmpleado(numCedula);
    }

    [WebMethod]
    public double GetPorcNoExceder()
    {
        double porc = 0;

        string query = "SELECT por_medu FROM nm_param";
        ROracle oDatos = new ROracle(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            if (oDatos.EjecutarQuery(query))
            {
                if (oDatos.oDataReader.Read())
                {
                    porc = double.Parse(oDatos.oDataReader.GetValue(0).ToString());
                }
            }
            else
                porc = -1;
        }
        catch (Exception ex)
        {
            porc = -1;
        }
        finally
        {
            oDatos.Dispose();
        }
        return porc;
    }

    [WebMethod]
    public double GetMaximoGastosPersonales()
    {
        double val = 0;

        string query = "SELECT val_tope FROM nm_epard";
        ROracle oDatos = new ROracle(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            if (oDatos.EjecutarQuery(query))
            {
                if (oDatos.oDataReader.Read())
                {
                    val = double.Parse(oDatos.oDataReader.GetValue(0).ToString());
                }
            }
            else
                val = -1;
        }
        catch (Exception ex)
        {
            val = -1;
        }
        finally
        {
            oDatos.Dispose();
        }
        return val;
    }

    //public string formatDate(string date)
    //{
    //    DateTime dt = DateTime.Parse(date, new System.Globalization.CultureInfo(System.Globalization.CultureInfo.CurrentCulture.Name, false));
    //    return dt.ToShortDateString();
    //}
}

