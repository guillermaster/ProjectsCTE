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

public partial class FotoGestor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["codGestor"] != null)
        {
            byte[] foto = null;
            AccesoDatos.ROracle oDatos = new AccesoDatos.ROracle(System.Configuration.ConfigurationManager.AppSettings["usuario"], System.Configuration.ConfigurationManager.AppSettings["clave"], System.Configuration.ConfigurationManager.AppSettings["base"], "web service wsImagenesCitaciones GetLicenciaUserImage");

            try
            {
                oDatos.Consultar_Imagen("select foto from transito.web_gestores where id_gestor = " +
                    Request.QueryString["codGestor"]);
                if (oDatos.oDataReader.Read())
                {
                    Oracle.DataAccess.Types.OracleBlob blob = oDatos.oDataReader.GetOracleBlob(0);
                    foto = blob.Value;
                }
                else
                {
                    foto = null;
                }

                System.IO.MemoryStream ms = new System.IO.MemoryStream(foto, 0, foto.Length);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                oDatos.Dispose();
            }
        }
    }
}
