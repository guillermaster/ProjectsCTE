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
using ComprobanteElectronicoPago;

public partial class Consultas_Tramites_SeguimientoTramite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ConsultarCEPs(User.Identity.Name.ToString());
        }
        //this.divWarning.Visible = false;
    }
    protected void btnConsultar_Click(object sender, EventArgs e)
    {

    }

    protected void ConsultarCEPs(string identificacion)
    {
        CEP oCEP = new CEP(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        try
        {
            DataTable dtCEPs = oCEP.ConsultaCEPxUsuario(identificacion);

            if (dtCEPs.Rows.Count > 0)
            {
                this.lblMensaje.Text = "";
                //DataView dvSortedData;
                //dvSortedData = dtCEPs.DefaultView;
                //dvSortedData.Sort = string.Format("{0} {1}", Constantes.Tramites.colNameForFechaIngresoCEP, "ASC");
                dtCEPs.DefaultView.Sort = Constantes.Tramites.colNameForFechaIngresoCEP + " DESC";
                this.dgCEPs.DataSource = dtCEPs.DefaultView;
                this.dgCEPs.DataBind();
                this.divWarning.Visible = false;
            }
            else
            {
                //ClearControls();
                this.dgCEPs.Visible = false;
                if (oCEP.Error == null || oCEP.Error == "")
                    this.lblMensaje.Text = "Usted no tiene ninguna solicitud de trámite pendiente.";
                else
                {
                    this.lblMensaje.Text = oCEP.Error;
                }
                this.divWarning.Visible = true;
            }
        }
        catch (Exception ex)
        {
            this.lblMensaje.Text = "Error al consultar Comprobantes Electrónicos de Pago por usuario.";
            this.divWarning.Visible = true;
        }

    }
    protected void dgCEPs_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.divCEPs.Visible = false;
        this.divDetTramite.Visible = true;

        this.lblCEP.Text = this.dgCEPs.SelectedItem.Cells[0].Text;
        this.lblEntrega.Text = this.dgCEPs.SelectedItem.Cells[8].Text;
        string id_tramite = this.dgCEPs.SelectedItem.Cells[7].Text;
        this.lblIdTramite.Text = id_tramite;
        
        CEP oCEP = new CEP(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        
        DataTable dtTramite = oCEP.ConsultaDatosTramite(id_tramite);

        if (dtTramite.Rows.Count > 0)
        {
            this.dgDetTramite.DataSource = dtTramite;
            this.dgDetTramite.DataBind();
            this.dgDetTramite.Visible = true;
            this.divWarning.Visible = false;
        }
        else
        {
            if (oCEP.Error == "")
                this.lblMensaje.Text = "No registra datos de estado de trámite";
            else
                this.lblMensaje.Text = oCEP.Error;
            this.divWarning.Visible = true;
        }
    }

    protected void dgCEPs_PageIndexChanged(Object sender, DataGridPageChangedEventArgs e)
    {
        this.dgCEPs.CurrentPageIndex = e.NewPageIndex;
        ConsultarCEPs(User.Identity.Name.ToString());
    }
    protected void imgBtnCerrar_Click(object sender, ImageClickEventArgs e)
    {
        this.divDetTramite.Visible = false;
        this.divCEPs.Visible = true;
    }
}
