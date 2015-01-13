using System;
using System.Web.UI;

public partial class _NotLoggedOn : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.UrlReferrer != null)
            {
                /*string referrerURL = Request.UrlReferrer.ToString();
                if (referrerURL.Contains("ctg.gob.ec"))
                    lblInstitucion.Text = "Agencia Nacional de Tránsito";*/
            }
        }
    }

    public UpdatePanel MasterUpdatePanel
    {
        get { return UpdatePanel1; }
    }

    public UpdatePanel RightColumnUpdatePanel
    {
        get { return UpdatePanel2; }
    }
}
