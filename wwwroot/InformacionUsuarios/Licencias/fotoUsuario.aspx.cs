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
using AccesoDatos;

public partial class Consultas_Licencias_fotoUsuario : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] foto = null;
        
        try
        {
            ROracle oDatos = new ROracle(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
            string sSql = "SELECT imagen FROM CL_IMAGENES WHERE id_persona = " + Session["idPersona"].ToString();
            sSql += " AND id_imagen = 'FOT'";

            oDatos.Consultar_Imagen(sSql);
            if (oDatos.oDataReader.Read())
            {
                //foto = (byte[])oDatos.oDataReader["imagen"];
                Oracle.DataAccess.Types.OracleBlob blob = oDatos.oDataReader.GetOracleBlob(0);
                foto = blob.Value;
            }
            oDatos.Dispose();
            System.IO.MemoryStream ms = new System.IO.MemoryStream(foto, 0, foto.Length);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //Response.BinaryWrite(foto);
        }
        catch (Exception ex)
        {
        }
    }
}
