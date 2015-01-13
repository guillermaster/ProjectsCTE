using System;
using System.Drawing;

public partial class Consultas_Vehiculos_FotoVehiculo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] foto = null;

        if (Request.QueryString.Count > 0)
        {
            wsImagenesVehiculos.Vehiculos webServImg = new wsImagenesVehiculos.Vehiculos();
            foto = webServImg.GetFotoRevision(Request.QueryString[0]);

            try
            {

                if (foto != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(foto, 0, foto.Length);
                    Image img = Image.FromStream(ms);
                    img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                else
                {
                    Bitmap bmp = Utilities.Utils.CreateTextImage("No existe foto de revisión para el vehículo con placa " + Request.QueryString[0]);
                    bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
                }
            }
            catch
            {
                Bitmap bmp = Utilities.Utils.CreateTextImage("Error al consultar foto del vehículo con placa " + Request.QueryString[0]);
                bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
            }
        }
    }
}