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

public partial class fotoLicencia : System.Web.UI.Page
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
        }
    }
}
