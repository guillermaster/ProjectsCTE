using System;
using System.Configuration;
using System.Web;
using System.Web.UI;

public partial class DataUserAccount : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetFieldsValues(HttpContext.Current.User.Identity.Name);
        }
    }

    protected void SetFieldsValues(string identificacion)
    {
        txtIdentificacion.Text = identificacion;

        Seguridad.UsuarioWeb oUsuarioWeb = new Seguridad.UsuarioWeb(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()], identificacion);
        string[] datos_usuarioweb = new string[5];
        datos_usuarioweb = oUsuarioWeb.ObtenerDatosUsuarioWeb();

        if (datos_usuarioweb[0] != "error")
        {
            txtNombres.Text = datos_usuarioweb[0];
            txtApellidos.Text = datos_usuarioweb[1];
            txtEmail.Text = datos_usuarioweb[2];
            txtEstado.Text = DescEstado(datos_usuarioweb[3]);
            hdnPassword.Value = oUsuarioWeb.GetEncPassword();
        }
    }

    protected string DescEstado(string estado)
    {
        return estado == "A" ? "Verificado" : "No Verificado";
    }

    #region "Nombres"
    protected void btnEditNombres_Click(object sender, ImageClickEventArgs e)
    {
        hdnNombres.Value = txtNombres.Text;
        btnEditNombres.Visible = false;
        btnSaveNombres.Visible = true;
        btnCancelNombres.Visible = true;
        txtNombres.ReadOnly = false;
        txtNombres.BackColor = System.Drawing.Color.LightBlue;
    }
    protected void ReadViewNombres()
    {
        btnSaveNombres.Visible = false;
        btnCancelNombres.Visible = false;
        btnEditNombres.Visible = true;
        txtNombres.ReadOnly = true;
        txtNombres.BackColor = System.Drawing.Color.FromArgb(255, 254, 225);
    }
    protected void btnSaveNombres_Click(object sender, ImageClickEventArgs e)
    {
        ReadViewNombres();
        if (UpdateUserData(txtIdentificacion.Text, Constantes.UsuarioWeb.DBFieldNameNombres, txtNombres.Text.ToUpper()))
            HtmlWriter.Messages.ShowModalInfoMessage(Master.MasterUpdatePanel, Master.GetType(), "Se han modificado correctamente sus nombres");
        else
            HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), "Ha ocurrido un error y no se pudieron modificar sus nombres, intente nuevamente por favor");
    }
    protected void btnCancelNombres_Click(object sender, ImageClickEventArgs e)
    {
        txtNombres.Text = hdnNombres.Value;
        ReadViewNombres();
    }
    #endregion
    #region "Apellidos"
    protected void btnEditApellidos_Click(object sender, ImageClickEventArgs e)
    {
        hdnApellidos.Value = txtApellidos.Text;
        btnEditApellidos.Visible = false;
        btnSaveApellidos.Visible = true;
        btnCancelApellidos.Visible = true;
        txtApellidos.ReadOnly = false;
        txtApellidos.BackColor = System.Drawing.Color.LightBlue;
    }
    protected void ReadViewApellidos()
    {
        btnSaveApellidos.Visible = false;
        btnCancelApellidos.Visible = false;
        btnEditApellidos.Visible = true;
        txtApellidos.ReadOnly = true;
        txtApellidos.BackColor = System.Drawing.Color.FromArgb(255, 254, 225);
    }
    protected void btnSaveApellidos_Click(object sender, ImageClickEventArgs e)
    {
        ReadViewApellidos();
        if (UpdateUserData(txtIdentificacion.Text, Constantes.UsuarioWeb.DBFieldNameApellidos, txtApellidos.Text.ToUpper()))
            HtmlWriter.Messages.ShowModalInfoMessage(Master.MasterUpdatePanel, Master.GetType(), "Se han modificado correctamente sus apellidos");
        else
            HtmlWriter.Messages.ShowModalAlertMessage(Master.MasterUpdatePanel, Master.GetType(), "Ha ocurrido un error y no se pudieron modificar sus apellidos, intente nuevamente por favor");
    }
    protected void btnCancelApellidos_Click(object sender, ImageClickEventArgs e)
    {
        txtApellidos.Text = hdnApellidos.Value;
        ReadViewApellidos();
    }
    #endregion
    #region "Email"
    protected void btnEditEmail_Click(object sender, ImageClickEventArgs e)
    {
        hdnEmail.Value = txtEmail.Text;
        btnEditEmail.Visible = false;
        btnSaveEmail.Visible = true;
        btnCancelEmail.Visible = true;
        txtEmail.ReadOnly = false;
        txtEmail.BackColor = System.Drawing.Color.LightBlue;
    }
    protected void ReadViewEmails()
    {
        btnSaveEmail.Visible = false;
        btnCancelEmail.Visible = false;
        btnEditEmail.Visible = true;
        txtApellidos.ReadOnly = true;
        txtApellidos.BackColor = System.Drawing.Color.FromArgb(255, 254, 225);
    }
    protected void btnSaveEmail_Click(object sender, ImageClickEventArgs e)
    {
        ReadViewEmails();
        if (UpdateUserData(txtIdentificacion.Text, Constantes.UsuarioWeb.DBFieldNameEmail, txtEmail.Text))
            HtmlWriter.Messages.ShowModalInfoMessage(Master.MasterUpdatePanel, Master.GetType(), "Se ha modificado correctamente su correo electrónico");
        else
            HtmlWriter.Messages.ShowModalAlertMessage(Master.MasterUpdatePanel, Master.GetType(), "Ha ocurrido un error y no se pudo modificar su correo electrónico, intente nuevamente por favor");
    }
    protected void btnCancelEmail_Click(object sender, ImageClickEventArgs e)
    {
        txtEmail.Text = hdnEmail.Value;
        ReadViewEmails();
    }
    #endregion
    #region "Password"
    protected void btnSaveNewPwd_Click(object sender, ImageClickEventArgs e)
    {
        ClearTextboxes4Password();

        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
        string curr_pwd = objCrypto.DescifrarCadena(hdnPassword.Value);

        if (txtPwdCurr.Text == curr_pwd)
        {
            if (txtPwdNew.Text == txtPwdNewConf.Text)
            {
                Seguridad.UsuarioWeb oUsuarioWeb = new Seguridad.UsuarioWeb(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                        ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                        ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()], this.txtIdentificacion.Text);
                if (oUsuarioWeb.ActualizaDato(Constantes.UsuarioWeb.DBFieldNameContrasena, objCrypto.CifrarCadena(txtPwdNew.Text)))
                {
                    hdnPassword.Value = objCrypto.CifrarCadena(txtPwdNew.Text);
                    HtmlWriter.Messages.ShowModalInfoMessage(Master.MasterUpdatePanel, Master.GetType(), "Se han modificado su contraseña correctamente.");
                }
                else
                    HtmlWriter.Messages.ShowModalFailureMessage(Master.MasterUpdatePanel, Master.GetType(), "Ha ocurrido un error, su contraseña no pudo ser modificada. Por favor intente nuevamente.");
            }
            else
                HtmlWriter.Messages.ShowModalAlertMessage(Master.MasterUpdatePanel, Master.GetType(), "No se modificó la contraseña, la nueva contraseña y su confirmación no coinciden.");
        }
        else
            HtmlWriter.Messages.ShowModalAlertMessage(Master.MasterUpdatePanel, Master.GetType(), "No se pudo modificar la contraseña, la contraseña actual es incorrecta.");
    }
    protected void btnCancelNewPwd_Click(object sender, ImageClickEventArgs e)
    {
        ClearTextboxes4Password();
    }
    protected void ClearTextboxes4Password()
    {
        txtNewPwd.Text = string.Empty;
        txtNewPwdConf.Text = string.Empty;
        txtCurrPwd.Text = string.Empty;
    }
    #endregion

    public bool UpdateUserData(string identificacion, string campo, string newValue)
    {
        Seguridad.UsuarioWeb oUsuarioWeb = new Seguridad.UsuarioWeb(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                        ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                        ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()], this.txtIdentificacion.Text);
        if (oUsuarioWeb.ActualizaDato(campo, newValue))
            return true;
        else
            return false;
    }


    protected void btnchangepwd_Click(object sender, EventArgs e)
    {
        MPE.Show();
    }
}