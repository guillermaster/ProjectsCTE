using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Informativo : System.Web.UI.Page
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