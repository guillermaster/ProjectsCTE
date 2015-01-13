using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;


/// <summary>
/// Summary description for ParaHorasExtras
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class ParaHorasExtras : System.Web.Services.WebService
{
    private string dbServer;
    private string dbUser;
    private string dbPwd;


    public ParaHorasExtras()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
        this.dbUser = System.Configuration.ConfigurationManager.AppSettings["usuario"];
        this.dbPwd = System.Configuration.ConfigurationManager.AppSettings["clave"];
        this.dbServer = System.Configuration.ConfigurationManager.AppSettings["base"];
    }

    [WebMethod]
    public void ConsultaDatosEmpleadoHorariosMarcaciones(int codEmpleado, string fecha,
        out string horaEntradaMarcada, out string horaSalidaMarcada, out string horaEntradaHorario, out string horaSalidaHorario, 
        out double numHorasExtras, out string nombre, out int codDirArea, out string descDirArea, out int codDpto, 
        out string descDpto, out int codJefe, out string nombreJefe, out int codDirector, out string nombreDirector, 
        out int codTipoEmp, out string descTipoEmp, out string tipoHorario, out int maxHorasExt, out string userSpJefe, out string userSpDirector,
        out string error)
    {
        ConsultaHorariosMarcaciones(codEmpleado.ToString(), fecha, out horaEntradaMarcada, out horaSalidaMarcada, out horaEntradaHorario, out horaSalidaHorario, out numHorasExtras,
            out codTipoEmp, out descTipoEmp, out tipoHorario, out maxHorasExt, out error);

        ConsultaDatosEmpleado(codEmpleado.ToString(), fecha, out nombre, out codDirArea, out descDirArea, out codDpto, out descDpto, out codJefe, out nombreJefe, out codDirector,
            out nombreDirector, out userSpJefe, out userSpDirector);

        //ConsultaHorariosMarcaciones(codEmpleado.ToString(), fecha, out horaEntradaMarcada, out horaSalidaMarcada, out horaEntradaHorario, out horaSalidaHorario, out numHorasExtras,
        //     out error);

        //ConsultaDatosEmpleado(codEmpleado.ToString(), fecha, out nombre, out codDirArea, out descDirArea, out codDpto, out descDpto, out codJefe, out nombreJefe, out codDirector,
        //    out nombreDirector, out userSpJefe, out userSpDirector,
        //    out codTipoEmp, out descTipoEmp, out tipoHorario, out maxHorasExt);

        //if (tipoHorario == "B")
        if (tipoHorario == "E")
        {
            horaSalidaHorario = horaEntradaMarcada;
            //try
            //{
            //    TimeSpan ts;
            //    ts = Convert.ToDateTime(horaSalidaMarcada, new System.Globalization.CultureInfo("es-Es", false)) - Convert.ToDateTime(horaSalidaHorario, new System.Globalization.CultureInfo("es-Es", false));
            //    numHorasExtras = double.Parse(string.Format("{0:0.00}", ts.TotalHours));
            //}
            //catch (Exception ex)
            //{
            //    numHorasExtras = 0;
            //}
        }
    }

    [WebMethod]
    public void ConsultaDatosEmpleadoHorariosEspeciales(int codEmpleado, string fecha,
        out string horaEntradaHorario, out string horaSalidaHorario,
        out string nombre, out int codDirArea, out string descDirArea, out int codDpto,
        out string descDpto, out int codJefe, out string nombreJefe, out int codDirector, out string nombreDirector,
        out int codTipoEmp, out string descTipoEmp, out string tipoHorario, out int maxHorasExt, out string userSpJefe, out string userSpDirector,
        out string error)
    {
        horaEntradaHorario = string.Empty;
        horaSalidaHorario = string.Empty;
        codTipoEmp = -1;
        descTipoEmp = string.Empty;
        tipoHorario = string.Empty;
        maxHorasExt = -1;
        error = string.Empty;

        ConsultaDatosEmpleado(codEmpleado.ToString(), fecha, out nombre, out codDirArea, out descDirArea, out codDpto, out descDpto, out codJefe, out nombreJefe, out codDirector,
            out nombreDirector, out userSpJefe, out userSpDirector);

        #region horarios especiales
        Biometrico.Marcaciones objMarcaciones = new Biometrico.Marcaciones(codEmpleado.ToString(), this.dbUser, this.dbPwd, this.dbServer);
        if (objMarcaciones.LoadHorariosEspeciales(Convert.ToDateTime(fecha)))
        {
            if (objMarcaciones.HorarioEntrada.Length > 10)
                horaEntradaHorario = objMarcaciones.HorarioEntrada.Substring(11);
            if (objMarcaciones.HorarioSalida.Length > 10)
                horaSalidaHorario = objMarcaciones.HorarioSalida.Substring(11);
                        
            codTipoEmp = objMarcaciones.TipoEmpleadoCod;
            descTipoEmp = objMarcaciones.TipoEmpleadoDesc;
            tipoHorario = objMarcaciones.TipoHoraExtra;
            maxHorasExt = objMarcaciones.MaximoHorasExtras;
        }
        else
            error = objMarcaciones.Error;
        #endregion
    }

    [WebMethod]
    public void ConsultaHorariosMarcaciones(string codEmpleado, string fecha, 
        out string horaEntradaMarcada, out string horaSalidaMarcada, out string horaEntradaHorario, out string horaSalidaHorario,
        out double numHorasExtras, 
        out int codTipoEmp, out string descTipoEmp, out string codTipoHora, out int maxHorasExtras, ////
        out string error)
    {
        horaEntradaMarcada = string.Empty;
        horaSalidaMarcada = string.Empty;
        horaEntradaHorario = string.Empty;
        horaSalidaHorario = string.Empty;
        error = string.Empty;
        numHorasExtras = 0;
        codTipoEmp = 0;
        descTipoEmp = string.Empty;
        codTipoHora = string.Empty;
        maxHorasExtras = 0;
        //Biometrico.Marcaciones objMarcaciones = new Biometrico.Marcaciones(codEmpleado, System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
        Biometrico.Marcaciones objMarcaciones = new Biometrico.Marcaciones(codEmpleado, this.dbUser, this.dbPwd, this.dbServer);
        if (objMarcaciones.LoadHorariosMarcaciones(Convert.ToDateTime(fecha)))
        {
            if (objMarcaciones.HoraEntrada.Length > 10)
                horaEntradaMarcada = objMarcaciones.HoraEntrada.Substring(11);
            if (objMarcaciones.HorarioEntrada.Length > 10)
                horaEntradaHorario = objMarcaciones.HorarioEntrada.Substring(11);
            if (objMarcaciones.HoraSalida.Length > 10)
                horaSalidaMarcada = objMarcaciones.HoraSalida.Substring(11);
            if (objMarcaciones.HorarioSalida.Length > 10)
                horaSalidaHorario = objMarcaciones.HorarioSalida.Substring(11);
            
            numHorasExtras = objMarcaciones.HorasExtras;

            codTipoEmp = objMarcaciones.TipoEmpleadoCod;
            descTipoEmp = objMarcaciones.TipoEmpleadoDesc;
            codTipoHora = objMarcaciones.TipoHoraExtra;
            maxHorasExtras = objMarcaciones.MaximoHorasExtras;
        }
        else
            error = objMarcaciones.Error;
    }

    [WebMethod]
    public void ConsultaDatosEmpleado(string codEmpleado, string fecha, out string nombre, out int codDirArea, out string descDirArea, out int codDpto, out string descDpto,
        out int codJefe, out string nombreJefe, out int codDirector, out string nombreDirector, out string userSpJefe, out string userSpDirector)
    //public void ConsultaDatosEmpleado(string codEmpleado, string fecha, out string nombre, out int codDirArea, out string descDirArea, out int codDpto, out string descDpto,
    //out int codJefe, out string nombreJefe, out int codDirector, out string nombreDirector, out string userSpJefe, out string userSpDirector,
    //    out int codTipoEmp, out string descTipoEmp, out string tipoHorario, out int maxHorasExt)
    {
        nombre = string.Empty;
        codDirArea = 0;
        descDirArea = string.Empty;
        codDpto = 0;
        descDpto = string.Empty;
        codJefe = 0;
        nombreJefe = string.Empty;
        codDirector = 0;
        nombreDirector = string.Empty;
        
        userSpJefe = string.Empty;
        userSpDirector = string.Empty;

        //codTipoEmp = 0;
        //descTipoEmp = string.Empty;
        //tipoHorario = string.Empty;
        //maxHorasExt = 0;

        Biometrico.Empleado objEmp = new Biometrico.Empleado(int.Parse(codEmpleado), this.dbUser, this.dbPwd, this.dbServer);
        if (objEmp.LoadEmpleadoInfo(Convert.ToDateTime(fecha)))
        {
            nombre = objEmp.Nombre;
            codDirArea = objEmp.CodDireccion;
            descDirArea = objEmp.Direcccion;
            codDpto = objEmp.CodDepartamento;
            descDpto = objEmp.Departamento;
            codJefe = objEmp.CodJefeInmediato;
            nombreJefe = objEmp.JefeInmediato;
            codDirector = objEmp.CodJefeDirector;
            nombreDirector = objEmp.JefeDirector;            
            userSpJefe = objEmp.UserSharepointJefe;
            userSpDirector = objEmp.UserSharepointDirector;

            //codTipoEmp = objEmp.TipoEmpleadoCod;
            //descTipoEmp = objEmp.TipoEmpleadoDesc;
            //tipoHorario = objEmp.TipoHoraExtra;
            //maxHorasExt = objEmp.MaximoHorasExtras;
        }
    }

    [WebMethod]
    public void HorasExtrasPorMes(int codEmpleado, int mes, int anio, out int[] idHorasExtras, out string[] fechas,
        out string[] horasIngreso, out string[] horasSalida, out double[] horasExtras, out double[] horasExtrasAprob,
        out string[] tiposHorasExtras, out string[] idJefes, out string[] fechasAprobJefe, out string[] tareas,
        out string nombreEmp, out string descArea, out string descDpto, out string nombreJefe, out string nombreDir,
        out string userSpDirector, out int codDirector)
    {
        idHorasExtras = null;
        fechas = null;
        horasIngreso = null;
        horasSalida = null;
        horasExtras = null;
        horasExtrasAprob = null;
        tiposHorasExtras = null;
        idJefes = null;
        fechasAprobJefe = null;
        tareas = null;
        nombreEmp = string.Empty;
        descArea = string.Empty;
        descDpto = string.Empty;
        nombreJefe = string.Empty;
        nombreDir = string.Empty;
        userSpDirector = string.Empty;
        codDirector = 0;

        Biometrico.HorasExtrasMensual objHXMens = new Biometrico.HorasExtrasMensual(codEmpleado, this.dbUser, this.dbPwd, this.dbServer);
        //int nMonth = 12;
        //if (DateTime.Now.Month > 1)
        //    nMonth = DateTime.Now.Month -1;
        //int nYear = DateTime.Now.Year;
        //if (nMonth == 12)
        //    nYear--;
        //if (objHXMens.LoadHorasExtras(nMonth, nYear))

        //int nMonth = DateTime.ParseExact(mes, "MMMM", System.Globalization.CultureInfo.GetCultureInfo("es-Es")).Month;

        if (objHXMens.LoadHorasExtras(mes, anio))
        {
            idHorasExtras = objHXMens.ListaIdHoraExtra;
            fechas = objHXMens.ListaFecha;
            horasIngreso = objHXMens.ListaHoraIngreso;
            horasSalida = objHXMens.ListaHoraSalida;
            horasExtras = objHXMens.ListaHorasExtras;
            horasExtrasAprob = objHXMens.ListaHorasAprob;
            tiposHorasExtras = objHXMens.ListaTipoHoraExtra;
            idJefes = objHXMens.ListaIdJefe;
            fechasAprobJefe = objHXMens.ListaFechaAprobJefe;
            tareas = objHXMens.ListaTituloTarea;

            Biometrico.Empleado objEmp = new Biometrico.Empleado(codEmpleado, this.dbUser, this.dbPwd, this.dbServer);
            if (objEmp.LoadEmpleadoInfo(DateTime.Now))
            {
                nombreEmp = objEmp.Nombre;
                descArea = objEmp.Direcccion;
                descDpto = objEmp.Departamento;
                nombreJefe = objEmp.JefeInmediato;
                nombreDir = objEmp.JefeDirector;
                userSpDirector = objEmp.UserSharepointDirector;
                codDirector = objEmp.CodJefeDirector;
            }
        }
    }


    [WebMethod]
    public void GetDirecciones(out string[] nomDir, out string[] codDir)
    {
        Biometrico.Direcciones oDir = new Biometrico.Direcciones(this.dbUser, this.dbPwd, this.dbServer);
        DataTable dtRes = oDir.GetAll();
        ArrayList listCod = new ArrayList();
        ArrayList listNom = new ArrayList();

        foreach (DataRow dr in dtRes.Rows)
        {
            listCod.Add(dr[0]);
            listNom.Add(dr[1]);
        }

        codDir = listCod.ToArray(typeof(string)) as string[];
        nomDir = listNom.ToArray(typeof(string)) as string[];
    }

    [WebMethod]
    public void GetDepartamentos(int codDireccion, out string[] nomDir, out string[] codDir)
    {
        Biometrico.Direcciones oDir = new Biometrico.Direcciones( codDireccion, this.dbUser, this.dbPwd, this.dbServer);
        DataTable dtRes = oDir.GetDepartamentosHijos();
        ArrayList listCod = new ArrayList();
        ArrayList listNom = new ArrayList();

        foreach (DataRow dr in dtRes.Rows)
        {
            listCod.Add(dr[0]);
            listNom.Add(dr[1]);
        }

        codDir = listCod.ToArray(typeof(string)) as string[];
        nomDir = listNom.ToArray(typeof(string)) as string[];
    }


    [WebMethod]
    public void GetEmpleados(int codDepartamento, out string[] nomDir, out string[] codDir)
    {
        Biometrico.Direcciones oDir = new Biometrico.Direcciones(this.dbUser, this.dbPwd, this.dbServer);
        DataTable dtRes = oDir.GetAllEmployees(codDepartamento);
        ArrayList listCod = new ArrayList();
        ArrayList listNom = new ArrayList();

        foreach (DataRow dr in dtRes.Rows)
        {
            listCod.Add(dr[0]);
            listNom.Add(dr[1]);
        }

        codDir = listCod.ToArray(typeof(string)) as string[];
        nomDir = listNom.ToArray(typeof(string)) as string[];
    }


    [WebMethod]
    public string GetMarcacionesPorMesXML(int codEmpleado, string fechaInicio, string fechaFin)
    {
        Biometrico.Marcaciones objMarcac = new Biometrico.Marcaciones(codEmpleado.ToString(), this.dbUser, this.dbPwd, this.dbServer);
        DataTable dtMarcac = objMarcac.GetMarcaciones(fechaInicio, fechaFin);
        string xmlStr = "<Marcaciones>";

        foreach (DataRow dr in dtMarcac.Rows)
        {
            xmlStr += "<Marcacion>";
            for(int i=0; i<dtMarcac.Columns.Count; i++)
                xmlStr += "<" + dtMarcac.Columns[i].ColumnName + ">" + dr[i].ToString() + "</" + dtMarcac.Columns[i].ColumnName + ">";
            xmlStr += "</Marcacion>";
        }

        if (objMarcac.Error != string.Empty)
            xmlStr += "<Error>" + objMarcac.Error + "</Error>";

        xmlStr += "</Marcaciones>";

        return xmlStr;
    }


    //[WebMethod]
    //public void tmp(out string[] col1, out string[] col2)
    //{
    //    ArrayList c1 = new ArrayList();
    //    c1.Add("11");
    //    c1.Add("12");
    //    c1.Add("13");
    //    ArrayList c2 = new ArrayList();
    //    c2.Add("21");
    //    c2.Add("22");
    //    c2.Add("23");
    //    col1 = c1.ToArray(typeof(string)) as string[];
    //    col2 = c2.ToArray(typeof(string)) as string[];
    //}



}

