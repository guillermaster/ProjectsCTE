using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;


/// <summary>
/// Summary description for ActivosFijos
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ActivosFijos : System.Web.Services.WebService
{
    private string dbServer;
    private string dbUser;
    private string dbPwd;

    public ActivosFijos()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        this.dbUser = System.Configuration.ConfigurationManager.AppSettings["usuario"];
        this.dbPwd = System.Configuration.ConfigurationManager.AppSettings["clave"];
        this.dbServer = System.Configuration.ConfigurationManager.AppSettings["base"];
    }

    [WebMethod]
    public string ActivosPorEmpleadoXML(string cedula, string codJurisdiccion)
    {
        OpenSide.ActivosFijos objActivos = new OpenSide.ActivosFijos(this.dbUser, this.dbPwd, this.dbServer);
        DataTable dtActivos = objActivos.GetActivosFijosPorEmpleado(cedula, codJurisdiccion);
        string xmlStr = "<ActivosFijos>";

        foreach (DataRow dr in dtActivos.Rows)
        {
            xmlStr += "<Activos>";
            for (int i = 0; i < dtActivos.Columns.Count; i++)
                xmlStr += "<" + dtActivos.Columns[i].ColumnName + ">" + dr[i].ToString().Replace("&","&amp;") + "</" + dtActivos.Columns[i].ColumnName + ">";
            xmlStr += "</Activos>";
        }

        if (objActivos.Error != string.Empty)
            xmlStr += "<Error>" + objActivos.Error + "</Error>";

        xmlStr += "</ActivosFijos>";
        return xmlStr;
    }

    [WebMethod]
    public DataTable TiposJurisdicciones()
    {
        OpenSide.ActivosFijos objActivos = new OpenSide.ActivosFijos(this.dbUser, this.dbPwd, this.dbServer);
        DataTable dtJur = objActivos.GetTiposJurisdicciones();
        /*string xmlStr = "<Jurisdicciones>";
        foreach (DataRow dr in dtJur.Rows)
        {
            xmlStr += "<Jurisdiccion>";
            for (int i = 0; i < dtJur.Columns.Count; i++)
                xmlStr += "<" + dtJur.Columns[i].ColumnName + ">" + dr[i].ToString() + "</" + dtJur.Columns[i].ColumnName + ">";
            xmlStr += "</Jurisdiccion>";
        }
        if (objActivos.Error != string.Empty)
            xmlStr += "<Error>" + objActivos.Error + "</Error>";
        xmlStr += "</Jurisdicciones>";
        return xmlStr;*/
        return dtJur;
    }

}

