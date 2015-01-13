using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Configuration;
using AccesoDatos;


/// <summary>
/// Summary description for DatosContratos
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class DatosContratos : System.Web.Services.WebService
{

    public DatosContratos()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public void GetDatosBasicosContratos(string numCedula, out string nombreEmpleado,
        out string regimen, out string codRegimen, out string lugarTrabajo, 
        out string fechaInicio, out string fechaFin, out string tiempoServicio,
        out double rmu, out double descuentos, out int maxPlazo)
    {

        nombreEmpleado = getNombreEmpleado(numCedula);
        codRegimen = string.Empty;
        regimen = string.Empty;
        lugarTrabajo = string.Empty;
        fechaInicio = string.Empty;
        fechaFin = string.Empty;
        tiempoServicio = string.Empty;
        rmu = 0;
        descuentos = 0;
        maxPlazo = 18;

        string query = "SELECT g.cod_gpro, g.nom_gpro, l.nom_cenp, c.fec_ingr, c.fec_venc FROM kactus.nm_contr c, kactus.nm_centp l, kactus.nm_gprot g "
            + "where l.cod_cenp=c.cod_cenp and g.cod_gpro=c.cod_gpro and c.cod_empr='1' and c.cod_empl='"+ numCedula +"'";
        ROracle oDatos = new ROracle(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            if (oDatos.EjecutarQuery(query))
            {
                if (oDatos.oDataReader.Read())
                {
                    codRegimen = oDatos.oDataReader.GetOracleValue(0).ToString();
                    regimen = oDatos.oDataReader.GetString(1);
                    lugarTrabajo = oDatos.oDataReader.GetString(2);
                    fechaInicio = oDatos.oDataReader.GetOracleValue(3).ToString();
                    fechaFin = oDatos.oDataReader.GetOracleValue(4).ToString();
                    tiempoServicio = RestarFechas(DateTime.Now, Convert.ToDateTime(fechaInicio));                    
                    RubrosContratos wsRubros = new RubrosContratos();
                    rmu = wsRubros.GetRMU(numCedula);
                    descuentos = wsRubros.GetDescuentos(numCedula, DateTime.Now.Month - 1, DateTime.Now.Year);
                    if (codRegimen == "4")
                    {
                        int nCurrMonth = DateTime.Now.Month;
                        maxPlazo = 12 - nCurrMonth;
                    }
                }
            }
            else
            {                
                regimen = oDatos.Mensaje;
            }
        }
        catch (Exception ex)
        {
            regimen = ex.Message;
        }
        finally
        {
            oDatos.Dispose();
        }
    }

    public string RestarFechas(DateTime fin, DateTime ini)
    {
        TimeSpan ts = fin - ini;
        int nDays = ts.Days;
        int nYears = nDays / 365;
        int nMonths = nDays / 30;
        return nYears.ToString() + " años  " + nMonths.ToString() + " meses";
    }

    public string getNombreEmpleado(string numCedula)
    {
        string nombre = string.Empty;
        string query = "select nom_empl, ape_empl from kactus.bi_emple where cod_empr='1' and cod_empl='" + numCedula + "'";
        ROracle oDatos = new ROracle(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            if (oDatos.EjecutarQuery(query))
            {
                if (oDatos.oDataReader.Read())
                {
                    nombre = oDatos.oDataReader.GetString(1) + " " + oDatos.oDataReader.GetString(0);
                }
            }
            else
                nombre = oDatos.Mensaje;
        }
        catch (Exception ex)
        {
            nombre = ex.Message;
        }
        finally
        {
            oDatos.Dispose();
        }
        return nombre;
    }

    public string getCargoEmpleado(string numCedula)
    {
        string cargo = string.Empty;
        string query = "select b.nom_carg from kactus.nm_contr a, kactus.bi_cargo b where a.ind_acti='A' AND a.cod_empl='" + numCedula + "' AND a.cod_carg=b.cod_carg";
        ROracle oDatos = new ROracle(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            if (oDatos.EjecutarQuery(query))
            {
                if (oDatos.oDataReader.Read())
                {
                    cargo = oDatos.oDataReader.GetString(0);
                }
            }
            else
                cargo = oDatos.Mensaje;
        }
        catch (Exception ex)
        {
            cargo = ex.Message;
        }
        finally
        {
            oDatos.Dispose();
        }
        return cargo;
    }

    

    [WebMethod]
    public void GetNombreAndRegimenEmp(string numCedula, out string nombre, out string codRegimen, out string regimen)
    {
        nombre = getNombreEmpleado(numCedula);
        codRegimen = string.Empty;
        regimen = string.Empty;

        string query = "SELECT g.cod_gpro, g.nom_gpro FROM kactus.nm_contr c, kactus.nm_gprot g "
            + "where g.cod_gpro=c.cod_gpro and c.cod_empr='1' and c.cod_empl='" + numCedula + "'";
        ROracle oDatos = new ROracle(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            if (oDatos.EjecutarQuery(query))
            {
                if (oDatos.oDataReader.Read())
                {
                    codRegimen = oDatos.oDataReader.GetOracleValue(0).ToString();
                    regimen = oDatos.oDataReader.GetString(1);
                }
            }
            else
            {
                regimen = oDatos.Mensaje;
            }
        }
        catch (Exception ex)
        {
            regimen = ex.Message;
        }
        finally
        {
            oDatos.Dispose();
        }
    }

    [WebMethod]
    public string GetNombreEmpleado(int codEmpleado)
    {
        return getNombreEmpleado(GetNumCedulaEmpleado(codEmpleado));
    }

    [WebMethod]
    public void GetNombreAndCargoEmpleado(int codEmpleado, out string nomEmp, out string cargoEmp)
    {
        nomEmp = getNombreEmpleado(GetNumCedulaEmpleado(codEmpleado));
        cargoEmp = getCargoEmpleado(GetNumCedulaEmpleado(codEmpleado));
    }


    [WebMethod]
    public void GetEmpleadoNombreCargoDptoJefe(int codEmpleado, out string nomEmp, out string cargoEmp, 
        out string nomJefe, out int codJefe, out string spUserJefe, out string nomDir, out string nomDpto)
    {
        nomEmp = getNombreEmpleado(GetNumCedulaEmpleado(codEmpleado));
        cargoEmp = getCargoEmpleado(GetNumCedulaEmpleado(codEmpleado));
        ParaHorasExtras objHorExt = new ParaHorasExtras();
        int codDirArea, codDpto, codDirector;
        string nomDirector, spUserDirector;
        objHorExt.ConsultaDatosEmpleado(codEmpleado.ToString(), "01/01/2010", out nomEmp, out codDirArea, out nomDir, out codDpto, out nomDpto, out codJefe, out nomJefe,
            out codDirector, out nomDirector, out spUserJefe, out spUserDirector);
    }

    
    public string GetNumCedulaEmpleado(int codEmpleado)
    {
        string numCedula = string.Empty;
        string query = "select cod_empl from bi_emple where act_esta<>'I' AND cod_inte ='" + codEmpleado + "'";
        ROracle oDatos = new ROracle(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            if (oDatos.EjecutarQuery(query))
            {
                if (oDatos.oDataReader.Read())
                {
                    numCedula = oDatos.oDataReader.GetValue(0).ToString();
                }
            }
            else
                numCedula = oDatos.Mensaje;
        }
        catch (Exception ex)
        {
            numCedula = ex.Message;
        }
        finally
        {
            oDatos.Dispose();
        }
        return numCedula;
    }

}

