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
        /*Bitmap bmp = Utilities.Utils.CreateTextImage(Request.QueryString[0]);
        Session[Constantes.WebApp.DatosSesion.ImgCitacWidth.ToString()] = bmp.Width;
        Session[Constantes.WebApp.DatosSesion.ImgCitacHeight.ToString()] = bmp.Height;
        bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        bmp.Dispose();*/

        byte[] foto = null;
        string codCitac = Request.QueryString[0];

        try
        {
            wsImagenesCitaciones.Service webServImg = new wsImagenesCitaciones.Service();
            foto = webServImg.GetCitacImage(codCitac);
            if (foto != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(foto, 0, foto.Length);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                Session[Constantes.WebApp.DatosSesion.ImgCitacWidth.ToString()] = img.Width;
                Session[Constantes.WebApp.DatosSesion.ImgCitacHeight.ToString()] = img.Height;
                img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                img.Dispose();
                ms.Dispose();
                ms.Close();
            }
            else
            {
                foto = webServImg.GetCitacImageHandHeld(codCitac);
                //foto = webServImg.GetCitacImageHandHeld("1327000704");
                if (foto != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(foto, 0, foto.Length);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    Session[Constantes.WebApp.DatosSesion.ImgCitacWidth.ToString()] = img.Width;
                    Session[Constantes.WebApp.DatosSesion.ImgCitacHeight.ToString()] = img.Height;
                    img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                    img.Dispose();
                    ms.Dispose();
                    ms.Close();
                }
                else
                {
                    Bitmap bmp = Utilities.Utils.CreateTextImage("No existe la foto");
                    Session[Constantes.WebApp.DatosSesion.ImgCitacWidth.ToString()] = bmp.Width;
                    Session[Constantes.WebApp.DatosSesion.ImgCitacHeight.ToString()] = bmp.Height;
                    bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                    bmp.Dispose();
                }
            }
        }
        catch (Exception ex)
        {
            Bitmap bmp = Utilities.Utils.CreateTextImage(ex.Message);
            Session[Constantes.WebApp.DatosSesion.ImgCitacWidth.ToString()] = bmp.Width;
            Session[Constantes.WebApp.DatosSesion.ImgCitacHeight.ToString()] = bmp.Height;
            bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
            bmp.Dispose();
        }

        
    }
}