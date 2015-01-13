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
using Citaciones;

public partial class Consultas_Citaciones_CitacionPorCodigo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.DefaultFocus = this.txtCodCitacion.ID;
            Page.Form.DefaultButton = this.btnSearch.ID;
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Infracciones oCitac = new Infracciones(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        this.detViewDetCitacion.DataSource = oCitac.CitacionesPorCodigoCitac(this.txtCodCitacion.Text);
        this.detViewDetCitacion.DataBind();
        if (this.detViewDetCitacion.Rows.Count == 0)
        {
            ShowWarning(oCitac.Error);
        }
        else
        {
            this.divError.Visible = false;
            this.divWarning.Visible = false;
        }
    }

    public void ShowError(string message)
    {
        this.divError.Visible = true;
        this.lblMsgError.Text = message;
    }

    public void ShowWarning(string message)
    {
        this.divWarning.Visible = true;
        this.lblMsgWarning.Text = message;
    }
}
