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


public partial class Consultas_VehiculosDetenidos_fotoVehiculo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] foto = null;

        if (Request.QueryString.Count > 0)
        {
            wsImagenesVehiculos.Vehiculos webServImg = new wsImagenesVehiculos.Vehiculos();
            foto = webServImg.GetFotoRevision(Request.QueryString[0]);
            
            if (foto != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(foto, 0, foto.Length);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else
            {
                Bitmap bmp = Utilities.Utils.CreateTextImage("No existe foto de revisión para el vehículo con placa " + Request.QueryString[0]);
                bmp.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
            }
        }
    }
}
