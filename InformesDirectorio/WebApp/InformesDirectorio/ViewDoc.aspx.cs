using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewDoc : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 2)
            {
                LoadDocument(Convert.ToInt32(Request.QueryString[0]), Request.QueryString[1], Request.QueryString[2]);
            }
        }
    }

    protected void LoadDocument(int idDocumento, string fileExtension, string filename)
    {
        InformesDirectorioExtra.Document objDoc = new InformesDirectorioExtra.Document(HttpContext.Current.User.Identity.Name,
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        byte[] data = objDoc.LoadDocumentFile(idDocumento);
        if (data != null)
        {
            Response.AddHeader("ContentType", "application/" + fileExtension);
            Response.AddHeader("content-disposition", "attachment; filename=" + filename.Replace(' ', '_'));
            Response.BinaryWrite(data);
            Response.End();
        }
        /*else
        {
            AlertJS("Usted no ha firmado ningún acta con los criterios de búsqueda seleccionados.");
        }*/
    }
}