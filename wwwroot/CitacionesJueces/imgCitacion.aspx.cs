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


public partial class imgCitacion : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] foto = null;
              
        try
        {

            wsImagCitac.Service oWsImgCitac = new wsImagCitac.Service();
            if(string.IsNullOrEmpty(Request.QueryString["tipo"]))
                foto = oWsImgCitac.GetCitacImage(Request.QueryString["codCitacion"]);
            else
                foto = oWsImgCitac.GetCitacTipoImage(Request.QueryString["codCitacion"], Request.QueryString["tipo"]);

            if (foto != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(foto, 0, foto.Length);
                System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
                Session["imgCitacWidth"] = img.Width;
                Session["imgCitacHeight"] = img.Height;
                img.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            //if (existeImagen)
            //{
            //    Response.BinaryWrite(foto);
            //}
            else
            {
                System.Drawing.Image imgError = System.Drawing.Image.FromFile(Server.MapPath("./img/imgCitacionError.gif"));
                imgError.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
            }
        }
        catch (Exception ex)
        {
            System.Drawing.Image imgError = System.Drawing.Image.FromFile(Server.MapPath("./img/imgCitacionError.gif"));
            imgError.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        }
    }
}
