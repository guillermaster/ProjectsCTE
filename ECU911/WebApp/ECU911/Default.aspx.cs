using System;
using System.Web;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadNewsFeed();
        }
    }

    protected void LoadNewsFeed()
    {
        /*CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;*/
        Utilities.RSS oRSS = new Utilities.RSS();
        System.Xml.XmlDocument xmlFeed = oRSS.GetRssFeed("http://www.cte.gob.ec/category/noticia/feed/");/*,
            objCrypto.DescifrarCadena(ConfigurationManager.AppSettings["proxyusr"]),
            objCrypto.DescifrarCadena(ConfigurationManager.AppSettings["proxypwd"]));*/
        if (xmlFeed.FirstChild != null)
        {
            gvNoticias.DataSource = oRSS.GetCteNewsMostRecentLinks(xmlFeed);
            gvNoticias.DataBind();
        }
    }
}
