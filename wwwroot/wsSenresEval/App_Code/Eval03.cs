using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;


/// <summary>
/// Summary description for Eval03
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Eval03 : System.Web.Services.WebService
{
    private string dbServer;
    private string dbUser;
    private string dbPwd;

    public Eval03()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        this.dbUser = System.Configuration.ConfigurationManager.AppSettings["usuario"];
        this.dbPwd = System.Configuration.ConfigurationManager.AppSettings["clave"];
        this.dbServer = System.Configuration.ConfigurationManager.AppSettings["base"];
    }

    [WebMethod]
    public void GetResultadosEvaluaciones(DateTime fechaIni, DateTime fechaFin, out string[] apellidos, out string[] nombres, out string[] cedulas,
        out string[] puestos, out string[] unidades, out string[] ciudades, out double[] calif_jefe, out double[] calif_ciud, out double[] calif_tot, out string[] calif_escala, out string error, out int aux)
    {
        #region init out vars
        apellidos = null;
        nombres = null;
        cedulas = null;
        puestos = null;
        unidades = null;
        ciudades = null;
        calif_jefe = null;
        calif_ciud = null;
        calif_tot = null;
        calif_escala = null;
        error = null;
        aux = 1;
        #endregion

        EvalSENRES.Senres oSenres = new EvalSENRES.Senres(this.dbUser, this.dbPwd, this.dbServer);

        if (oSenres.ConsolidadoEvaluaciones(fechaIni.ToString("dd/MM/yyyy"), fechaFin.ToString("dd/MM/yyyy")))
        {
            apellidos = oSenres.Apellidos;
            nombres = oSenres.Nombres;
            cedulas = oSenres.Cedulas;
            puestos = oSenres.Cargos;
            unidades = oSenres.Unidades;
            ciudades = oSenres.Ciudades;
            calif_jefe = oSenres.CalificacionesJefe;
            calif_ciud = oSenres.CalificacionesCiudadanos;
            calif_tot = oSenres.CalificacionesTotales;
            calif_escala = oSenres.CalificacionesEscalas;
        }
        else
            error = oSenres.Error;
    }

}

