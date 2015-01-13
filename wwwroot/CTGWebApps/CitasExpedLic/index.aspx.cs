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

public partial class CitasExpLic : System.Web.UI.Page
{
    string currentStep = "0";
    string TYPE = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack)
        {
            
        }
        else
        {
            if(Request.QueryString["type"]=="REN" || Request.QueryString["type"]=="DUP")
                TYPE = Request.QueryString["type"];
            if(TYPE=="")
                this.divStep0.Visible = false;
            else
                this.divStep0.Visible = true;
            this.divStep1.Visible = false;
        }
    }
    protected void btnIniciar_Click(object sender, EventArgs e)
    {
        currentStep = "1";
        this.divStep0.Visible = false;
        this.divStep1.Visible = true;
    }
}
