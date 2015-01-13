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

public partial class NotificacionAudiencia : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Auxiliar.Helper.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Default.aspx");
            }
            Page.Form.DefaultFocus = this.txtNoCitacion.ID;
            Page.Form.DefaultButton = this.btnConsultar.ID;
            LoadHoras();
            LoadCiudad(User.Identity.Name.ToString());
        }
    }



    protected void LoadHoras()
    {
        for (int i = 0; i < 24; i++)
        {
            if(i < 10)
                this.ddlHoras.Items.Add("0"+i.ToString());
            else
                this.ddlHoras.Items.Add(i.ToString());
        }
        for (int i = 0; i < 60; i++)
        {
            if (i < 10)
                this.ddlMinutos.Items.Add("0"+i.ToString());
            else
                this.ddlMinutos.Items.Add(i.ToString());
        }
    }

    protected void LoadCiudad(string usuarioJuez)
    {
        Auxiliar.CitacionesJueces oCitacJueces = new Auxiliar.CitacionesJueces(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        this.txtCiudad.Text = oCitacJueces.GetCiudadJuzgado(usuarioJuez);
    }

    protected void LoadNumExpediente(string codCitacion)
    {
        Auxiliar.CitacionesJueces oCitacJueces = new Auxiliar.CitacionesJueces(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        string numExpediente = oCitacJueces.GetNumExpediente(codCitacion);
        if(numExpediente=="" || numExpediente==null)
        {
            this.lblError.Text = "Error al generar número de expediente. Por favor verifique el número de citación ingresado.";
            this.txtNoExpediente.Text = "";
            this.btnConsultar.Enabled = false;
        }
        else
        {
            this.txtNoExpediente.Text = numExpediente;
            this.lblError.Text = "";
            this.btnConsultar.Enabled = true;
        }
    }


    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Auxiliar.CitacionesJueces oCitacJueces = new Auxiliar.CitacionesJueces(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], Page.Request.Url.ToString());
        int dia = int.Parse(this.txtFechaDesde.Text.Substring(0,2));
        int mes = int.Parse(this.txtFechaDesde.Text.Substring(3,2));
        int anio = int.Parse(this.txtFechaDesde.Text.Substring(6,4));
        int horas = int.Parse(this.ddlHoras.SelectedValue);
        int minutos = int.Parse(this.ddlMinutos.SelectedValue);
        DateTime fechaNotif = new DateTime(anio, mes, dia, horas, minutos, 0);

        if (oCitacJueces.IngresarNotificacionAudiencia(User.Identity.Name.ToString(), this.txtNoCitacion.Text, this.txtNoExpediente.Text, fechaNotif))
        {
            this.lblError.Text = "";
            AlertJS("Se ha registrado la audiencia");
        }
        else
        {
            this.lblError.Text = "<b>Error:</b> " + oCitacJueces.Error;
        }
    }
    protected void txtNoCitacion_TextChanged(object sender, EventArgs e)
    {
        LoadNumExpediente(this.txtNoCitacion.Text);
    }
    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void AlertJS(string message)
    {
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" + message + "');", true);
    }
}
