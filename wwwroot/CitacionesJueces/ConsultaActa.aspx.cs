using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class ConsultaActa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Auxiliar.ActasJueces oActasJueces = new Auxiliar.ActasJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
        byte[] data = oActasJueces.ObtenerActa(this.txtNumExpediente.Text, this.ddlTipoDocumento.SelectedValue, this.txtNumLicencia.Text, User.Identity.Name.ToString());
        if (data != null)
        {
            Response.AddHeader("ContentType", "application/pdf");
            //Response.AddHeader ("content-disposition","attachment; filename=fname.ext")
            Response.BinaryWrite(data);
            Response.End();
        }
        else
        {
            AlertJS("Usted no ha firmado ningún acta con los criterios de búsqueda seleccionados.");
        }
    }

    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }
}
