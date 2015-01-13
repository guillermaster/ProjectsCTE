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


public partial class controls_editableTextBox : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }


    protected void btnEditar_Click(object sender, EventArgs e)
    {
        ClearMessages();
        this.hdnPreviousValue.Value = this.TextBox1.Text;
        this.TextBox1.Visible = false;
        this.TextBox2.Visible = true;
        this.TextBox2.Text = this.TextBox1.Text;

        this.btnEditar.Visible = false;
        this.btnGuardar.Visible = true;
        this.btnCancelar.Visible = true;
    }

    public string Value
    {
        get{
            return this.TextBox1.Text;
        }
        set{
            this.TextBox1.Text = value;
            this.hdnPreviousValue.Value = value;
        }
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

        Seguridad.UsuarioWeb oUsuarioWeb = new Seguridad.UsuarioWeb(ConfigurationManager.AppSettings["usuario"], ConfigurationManager.AppSettings["clave"], ConfigurationManager.AppSettings["base"], this.hdnIdentificacion.Value);
        if (oUsuarioWeb.ActualizaDato(this.hdnCampo.Value, this.TextBox2.Text))
        {
            this.hdnPreviousValue.Value = this.TextBox2.Text;
            this.TextBox1.Text = this.TextBox2.Text;
            this.lblSuccess.Text = "Los cambios han sido guardados";
            this.TextBox1.Visible = true;
            this.TextBox2.Visible = false;
        }
        else
        {
            this.TextBox1.Text = this.hdnPreviousValue.Value;
            this.TextBox2.Text = this.hdnPreviousValue.Value;
            this.lblMensaje.Text = "Error: " + oUsuarioWeb.Error;
        }
    }


    protected void btnCancelar_Click(object sender, EventArgs e)
    {
        ClearMessages();
        this.btnGuardar.Visible = false;
        this.btnCancelar.Visible = false;
        this.btnEditar.Visible = true;
        this.TextBox1.Visible = true;
        this.TextBox2.Visible = false;
    }

    public void ClearMessages()
    {
        this.lblSuccess.Text = "";
        this.lblMensaje.Text = "";
    }

    public void Init()
    {
        this.btnEditar.Visible = true;
        this.btnGuardar.Visible = false;
        this.btnCancelar.Visible = false;
        this.TextBox1.Visible = true;
        this.TextBox2.Visible = false;
        ClearMessages();
    }

}