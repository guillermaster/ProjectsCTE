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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Auxiliar.Helper.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Default.aspx");
            }
        }
    }


    ///********* PERMITE BAJAR UN ACTA FIRMADA QUE SE ENCUENTRA EN DB ******//
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    Auxiliar.ActasJueces oActasJueces = new Auxiliar.ActasJueces(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"]);
    //    byte[] data = oActasJueces.ObtenerActa("01-3006069-2008", "A", "2323", "vruales");
    //    Response.AddHeader("ContentType", "application/pdf");
    //    //Response.AddHeader ("content-disposition","attachment; filename=fname.ext")
    //    Response.BinaryWrite(data);
    //    Response.End();
    //}
}
