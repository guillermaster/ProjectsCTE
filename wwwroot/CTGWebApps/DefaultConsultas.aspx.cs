using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //HttpCookie cogeCookie = Request.Cookies.Get("webtransctg");
        //if (cogeCookie == null)
        //{
        //    HttpCookie addCookie = new HttpCookie("webtransctg");
        //    addCookie.Expires = DateTime.Today.AddDays(1).AddSeconds(-1);
        //    addCookie.Value = Session.SessionID;
        //    Response.Cookies.Add(addCookie);
        //}
        //else
        //{
        //    string[] values = Request.Url.ToString().Split('/');
        //    string url = values[0] + "//" + values[2] + "/" + values[3] + "/(S(" + cogeCookie.Value + "))/DefaultConsultas.aspx";
        //    Response.Redirect(url,true);
        //}
    }
}
