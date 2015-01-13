using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;

public partial class FotoVehiculo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] foto = null;

        try
        {

            if (Request.QueryString.Count > 0)
            {
                /*wsImagenesVehiculos.Vehiculos webServImg = new wsImagenesVehiculos.Vehiculos();
                foto = webServImg.GetFotoRevision(Request.QueryString[0]);

                if (foto != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(foto, 0, foto.Length);
                    System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                    if (Request.QueryString.Count == 1)
                    {
                        img = GetResizedImage(img, 220, 293);
                    }                    
                    img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    Bitmap bmp = Utilities.Utils.CreateTextImage("No existe foto de revisión para el vehículo con placa " + Request.QueryString[0]);
                    bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                }*/
            }
        }
        catch (Exception ex)
        {
            /*Bitmap bmp = Utilities.Utils.CreateTextImage("Error al visualizar imagen. " + ex.Message);
            bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);*/
        }
    }


    private System.Drawing.Image GetResizedImage(System.Drawing.Image FullsizeImage, int NewWidth, int MaxHeight)
    {
        // Prevent using images internal thumbnail
        FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);
        FullsizeImage.RotateFlip(System.Drawing.RotateFlipType.Rotate180FlipNone);

         if (FullsizeImage.Width <= NewWidth)
            {
                NewWidth = FullsizeImage.Width;
            }

        int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
        if (NewHeight > MaxHeight)
        {
            // Resize with height instead
            NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
            NewHeight = MaxHeight;
        }

        System.Drawing.Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);

        // Clear handle to original file so that we can overwrite it if necessary
        FullsizeImage.Dispose();
        return NewImage;
    }
}
