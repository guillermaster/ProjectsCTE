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
using System.Drawing;

public partial class fotoLicencia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] foto = null;

        try
        {
            if (foto != null)
            {
                wsImagenesCitaciones.Service webServImg = new wsImagenesCitaciones.Service();
                foto = webServImg.GetLicenciaUserImage(Request.QueryString[0]);
                System.IO.MemoryStream ms = new System.IO.MemoryStream(foto, 0, foto.Length);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                img.Dispose();
                ms.Dispose();
                ms.Close();
            }
            else
            {
                Bitmap bmp = Utilities.Utils.CreateTextImage("No existe foto");
                bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                bmp.Dispose();
            }
        }
        catch (Exception ex)
        {
            Bitmap bmp = Utilities.Utils.CreateTextImage(ex.Message);
            bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
            bmp.Dispose();
        }
    }
}
