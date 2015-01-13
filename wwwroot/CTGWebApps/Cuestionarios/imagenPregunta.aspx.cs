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
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using AccesoDatos;

public partial class Cuestionarios_imagenPregunta : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] foto = null;
        try
        {
            ROracle oDatos = new ROracle(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            string sSql = "SELECT IMAGEN FROM lc_preguntas WHERE COD_PREGUNTA=" + Request["cod"];
            oDatos.Consultar_Imagen(sSql);
            if (oDatos.oDataReader.Read())
            {
                foto = (byte[])oDatos.oDataReader["imagen"];
            }
            oDatos.Dispose();
            
            MemoryStream _stream = new MemoryStream();
            _stream.Write(foto, 0, foto.Length);

            Bitmap _bmp = new Bitmap(_stream);
            // Here is the trick
            Context.Response.ContentType = "image/gif";
            _bmp.Save(Context.Response.OutputStream, ImageFormat.Jpeg);
            _bmp.Dispose();
            _stream.Close();
            
        }
        catch (Exception ex)
        {
        }
    }
}
