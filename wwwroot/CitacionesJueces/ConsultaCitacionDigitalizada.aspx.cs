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

public partial class ConsultaCitacionDigitalizada : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        imgBtnCitacion.ImageUrl = "imgCitacion.aspx?codCitacion=" + txtNumCitacion.Text + "&tipo=" + ddlTipo.SelectedValue;
        imgBtnCitacion.DataBind();
        HideControlsButImgCitacion();
        lblError.Text = "HAGA CLIC EN LA IMAGEN PARA CERRARLA";
    }

    protected void imgBtnCitacion_Click(object sender, ImageClickEventArgs e)
    {
        ShowControlsButImgCitacion();
        lblError.Text = string.Empty;
    }

    protected void ShowControlsButImgCitacion()
    {
        divCons.Visible = true;
        pnlImagen.Visible = false;
    }

    protected void HideControlsButImgCitacion()
    {
        divCons.Visible = false;
        pnlImagen.Visible = true;
    }
}
