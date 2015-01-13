using System;
using System.Xml;
using System.Net;
using System.Data;

namespace Utilities
{
    public class RSS
    {
        string error;

        public RSS()
        {
        }

        public XmlDocument GetRssFeed(string rssURL)//, string proxyuser, string proxypassword)
        {
            XmlDocument rssDoc = new System.Xml.XmlDocument();
            try
            {
                error = string.Empty;
                WebRequest myRequest = WebRequest.Create(rssURL);
                /*WebProxy webProxy = new WebProxy("10.10.1.15:8080");
                webProxy.Credentials = new System.Net.NetworkCredential(proxyuser, proxypassword);
                myRequest.Proxy = webProxy;*/
                WebResponse myResponse = myRequest.GetResponse();
                System.IO.Stream rssStream = myResponse.GetResponseStream();                
                rssDoc.Load(rssStream);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            return rssDoc;
        }

        public DataTable GetCteNewsMostRecentLinks(XmlDocument xmlDocFeed)
        {
            XmlNodeList rssItems = xmlDocFeed.SelectNodes("rss/channel/item");
            DataTable dtLinks = new DataTable();
            dtLinks.Columns.Add("title");
            dtLinks.Columns.Add("url");
            for (int i = 0; i < 3; i++)
            {
                DataRow dr = dtLinks.NewRow();
                dr[0] = rssItems.Item(i).SelectSingleNode("title").InnerText;
                dr[1] = rssItems.Item(i).SelectSingleNode("link").InnerText;
                dtLinks.Rows.Add(dr);
            }
            return dtLinks;
        }
    }
}
