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

public partial class Estadisticas : System.Web.UI.Page
{
    private const string codOpcImpugnacionesRecibidas = "IR";
    private static readonly string txtOpcImpugnacionesRecibidas = "Impugnaciones recibidas";
    private const string codOpcJuzgamientoImpugnaciones = "JI";
    private static readonly string txtOpcJuzgamientoImpugnaciones = "Juzgamiento de impugnaciones";
    private const string codOpcContravencionesMayorNumeroImpgunaciones = "CMNI";
    private static readonly string txtOpcContravencionesMayorNumeroImpgunaciones = "Contravenciones más impugnadas";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Auxiliar.Helper.ValidateIPDNSAgent(Session[Constantes.UsuarioWeb.SessionIP], Session[Constantes.UsuarioWeb.SessionDNS], Session[Constantes.UsuarioWeb.SessionAgent], Request.UserHostAddress, Request.UserHostName, Request.UserAgent))
            {
                FormsAuthentication.SignOut();
                Response.Redirect("~/Default.aspx");
            }
            PopulateListCriterios();
        }
    }

    protected void PopulateListCriterios()
    {
        this.ddlCriterios.Items.Add(new ListItem(" -- Seleccione -- ", null));
        this.ddlCriterios.Items.Add(new ListItem(txtOpcContravencionesMayorNumeroImpgunaciones, codOpcContravencionesMayorNumeroImpgunaciones));
        this.ddlCriterios.Items.Add(new ListItem(txtOpcImpugnacionesRecibidas, codOpcImpugnacionesRecibidas));
        this.ddlCriterios.Items.Add(new ListItem(txtOpcJuzgamientoImpugnaciones, codOpcJuzgamientoImpugnaciones));
    }

    protected void btnConsultar_Click(object sender, EventArgs e)
    {
        Auxiliar.EstadisticasImpugnaciones oEstadImpug = new Auxiliar.EstadisticasImpugnaciones(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);

        switch (this.ddlCriterios.SelectedValue)
        {
            case codOpcImpugnacionesRecibidas:
                this.gvEstadisticasImpug.DataSource = oEstadImpug.EstadImpugnacRecibidas(this.txtFechaDesde.Text, this.txtFechaHasta.Text);
                this.gvEstadisticasImpug.DataBind();
                this.gvEstadisticasImpug.Visible = true;
                break;
            case codOpcJuzgamientoImpugnaciones:
                this.gvEstadisticasImpug.DataSource = oEstadImpug.EstadJuzgamientoImpugnac(this.txtFechaDesde.Text, this.txtFechaHasta.Text);
                this.gvEstadisticasImpug.DataBind();
                this.gvEstadisticasImpug.Visible = true;
                break;
            case codOpcContravencionesMayorNumeroImpgunaciones:
                this.gvEstadisticasImpug.DataSource = oEstadImpug.EstadContravencMasImpug(this.txtFechaDesde.Text, this.txtFechaHasta.Text);
                this.gvEstadisticasImpug.DataBind();
                this.gvEstadisticasImpug.Visible = true;
                break;
        }
    }
}
