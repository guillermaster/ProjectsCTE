using System;

public partial class Consultas_Licencias_fotoUsuarioLic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] foto = null;

        try
        {

            wsImagenesCitaciones.Service webServImg = new wsImagenesCitaciones.Service();
            foto = webServImg.GetLicenciaUserImage(Request.QueryString[0]);

            System.IO.MemoryStream ms = new System.IO.MemoryStream(foto, 0, foto.Length);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            //Response.BinaryWrite(foto);
        }
        catch (Exception ex)
        {
            foto = System.IO.File.ReadAllBytes(Server.MapPath("~\\images/silueta.gif"));
            System.IO.MemoryStream ms = new System.IO.MemoryStream(foto, 0, foto.Length);
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}