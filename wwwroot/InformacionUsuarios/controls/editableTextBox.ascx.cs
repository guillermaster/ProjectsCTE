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
using Usuario;

public partial class controls_editableTextBox : System.Web.UI.UserControl
{
    private System.Drawing.Color defaultBorderColor;
    private System.Drawing.Color defaultBackgroundColor;
    private System.Web.UI.WebControls.BorderStyle defaultBorder;

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


    protected void btnEditar_Click(object sender, EventArgs e)
    {
        ClearMessages();
        this.hdnPreviousValue.Value = this.TextBox1.Text;
        this.TextBox1.ReadOnly = false;
        defaultBorderColor = this.TextBox1.BorderColor;
        defaultBackgroundColor = this.TextBox1.BackColor;
        defaultBorder = this.TextBox1.BorderStyle;
        this.TextBox1.BackColor = System.Drawing.Color.LightBlue;
        this.TextBox1.BorderColor = defaultBorderColor;
        this.TextBox1.BorderStyle = defaultBorder;

        this.btnEditar.Visible = false;
        this.btnGuardar.Visible = true;
        this.btnCancelar.Visible = true;
    }


    public void setValue(string value)
    {
        this.TextBox1.Text = value;
    }

    public void setMaxLength(int value)
    {
        this.TextBox1.MaxLength = value;
    }

    public void setCampo(string value)
    {
        this.hdnCampo.Value = value;
    }

    public void setIdentificacion(string value)
    {
        this.hdnIdentificacion.Value = value;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        this.btnGuardar.Visible = false;
        this.btnCancelar.Visible = false;
        this.btnEditar.Visible = true;
        this.TextBox1.ReadOnly = true;
        this.TextBox1.BackColor = defaultBackgroundColor;
        this.TextBox1.BorderColor = defaultBorderColor;
        this.TextBox1.BorderStyle = defaultBorder;

        DatosUsuario oDatosUsuario = new DatosUsuario(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"]);
        if(oDatosUsuario.ActualizaDato(this.hdnIdentificacion.Value, this.hdnCampo.Value, this.TextBox1.Text, this.hdnPreviousValue.Value))
        {
            this.hdnPreviousValue.Value = this.TextBox1.Text;
            this.lblSuccess.Text = Constantes.MensajesUsuarios.actDatosSuccesful;
        }else{
            this.TextBox1.Text = this.hdnPreviousValue.Value;
            this.lblMensaje.Text = "Error: " + oDatosUsuario.Error; ;
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        ClearMessages();
        this.btnGuardar.Visible = false;
        this.btnCancelar.Visible = false;
        this.btnEditar.Visible = true;
        this.TextBox1.ReadOnly = true;
        this.TextBox1.BackColor = defaultBackgroundColor;
        this.TextBox1.BorderColor = defaultBorderColor;
        this.TextBox1.BorderStyle = defaultBorder;
        this.TextBox1.Text = this.hdnPreviousValue.Value;
    }

    public void ClearMessages()
    {
        this.lblSuccess.Text = "";
        this.lblMensaje.Text = "";
    }

}