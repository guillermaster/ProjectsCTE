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

public partial class LoginExtra_getCaptchaImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        CaptchaImage.CaptchaImage ci = new CaptchaImage.CaptchaImage(Session["CaptchaImageText"].ToString(), 200, 50, "Century Schoolbook");
        Response.Clear();
        Response.ContentType = "image/jpeg";
        ci.Image.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        ci.Dispose();
    }
}
