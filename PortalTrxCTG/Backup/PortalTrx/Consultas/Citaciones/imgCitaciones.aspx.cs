using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Consultas_Citaciones_imgCitaciones : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] foto = null;

        try
        {

            wsImagenesCitaciones.Service webServImg = new wsImagenesCitaciones.Service();
            foto = webServImg.GetCitacImage(Request.QueryString[0]);

            System.IO.MemoryStream ms = new System.IO.MemoryStream(foto, 0, foto.Length);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            Session[Constantes.WebApp.DatosSesion.ImgCitacWidth.ToString()] = img.Width;
            Session[Constantes.WebApp.DatosSesion.ImgCitacHeight.ToString()] = img.Height;
            img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //Response.BinaryWrite(foto);
        }
        catch (Exception ex)
        {
            Bitmap bmp = Utilities.Utils.CreateTextImage(ex.Message);
            bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        }
    }
}