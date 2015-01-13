using System;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;

public partial class OrdenCuerpo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ListDocumentLibrary();
    }

    protected void ListDocumentLibrary()
    {
        intranetLists.Lists wsLists = new intranetLists.Lists();
        wsLists.Credentials = new System.Net.NetworkCredential("webmaster", "123456", "CTG");

        XmlDocument xmlDocQry = new XmlDocument();
        xmlDocQry.LoadXml("<Query><Where><Lt><FieldRef Name=\"ID\" /><Value Type=\"Counter\">3</Value></Lt></Where></Query>");
        XmlNode xmlQry = xmlDocQry.FirstChild;
        
        XmlDocument xmlDocViewFields = new XmlDocument();
        xmlDocViewFields.LoadXml("<ViewFields><FieldRef Name=\"ID\" /><FieldRef Name=\"Title\" /></ViewFields>");
        XmlNode xmlViewFields = xmlDocViewFields.FirstChild;

        XmlDocument xmlDocQryOpt = new XmlDocument();
        xmlDocQryOpt.LoadXml("<queryOptions xmlns:SOAPSDK9=\"http://schemas.microsoft.com/sharepoint/soap/\" ><QueryOptions/></queryOptions>");
        XmlNode xmlQryOpt = xmlDocQryOpt.FirstChild;
        
        XmlNode listItems = wsLists.GetListItems("{80ae192e-cfbe-4c84-98eb-f23a5ad7c980}", "{ec42fb80-5fd7-47d3-8426-17f18c1ac789}", null, null, null, null, null);//,
        string preUrlLatestFile = (((listItems.ChildNodes[1]).ChildNodes[1])).Attributes[16].Value;
        string urlLatestFile = "http://intranet/" + preUrlLatestFile.Substring(preUrlLatestFile.IndexOf('#') + 1);

        DownloadFile(urlLatestFile);
    }

    private void DownloadFile(string url)
    {
        byte[] content;
        WebClient webclient = new WebClient();
        webclient.Credentials = new System.Net.NetworkCredential("webmaster", "123456", "CTG");
        content = webclient.DownloadData(url);

        Response.Clear();
        //Response.ContentType = "application/octet-stream";
        Response.ContentType = "application/pdf";
        //Response.AddHeader("content-disposition", "attachment;filename=orderdelcuerpo.pdf");
        Response.BinaryWrite(content);
        /*Response.Flush();
        Response.Close();*/
    }
    

}