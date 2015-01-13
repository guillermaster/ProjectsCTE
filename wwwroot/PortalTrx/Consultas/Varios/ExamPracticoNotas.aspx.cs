using System;
using System.Configuration;
using System.Web;

public partial class Consultas_Varios_ExamPracticoCalif : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ConsultarCalificaciones(HttpContext.Current.User.Identity.Name);
    }


    protected void ConsultarCalificaciones(string identificacion)
    {
        Brevetacion.ExamenPractico oExamenPractico = new Brevetacion.ExamenPractico(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()], 
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        
        gvNotasExamenes.DataSource = oExamenPractico.GetCalificacionesExamenByIdUser(identificacion);
        gvNotasExamenes.DataBind();

        if (gvNotasExamenes.Rows.Count == 0)
        {
            if (string.IsNullOrWhiteSpace(oExamenPractico.Error))
                divContent.Controls.Add(HtmlWriter.Messages.PrintInfoMessage("Usted no ha realizado ningún examen práctico"));
            else
                HtmlWriter.Messages.ShowMainContentError(Master, divMain, oExamenPractico.Error);
        }
    }
}