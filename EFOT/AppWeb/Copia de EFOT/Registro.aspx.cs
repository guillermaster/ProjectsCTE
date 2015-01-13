using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Utilities;
using Constantes;
using CifradoCs;
using EFOTclass;
using System.Net.Mail;
using System.Data;
using Brevetacion;

public partial class Registro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!base.IsPostBack)
        {
            this.LoadSexoDDL();
            this.LoadEstadoCivil();
            this.LoadEstaturaDDL();
            this.LoadTiposSangre();
            this.LoadTallas();
            this.LoadPaises(this.ddlPaisNac);
            this.LoadProvincias(this.ddlProvinciaRes, "ECU");
            this.LoadCiudades(this.ddlLocalidadRes, this.ddlProvinciaRes.SelectedValue);
        }
    }

    protected void btnNextStep1_Click(object sender, EventArgs e)
    {
        if (base.Page.IsValid)
        {
            DateTime dateTime = this.ConvertToDateTime(this.txtFecNac.Text);
            DateTime now = DateTime.Now;
            int year = now.Year - dateTime.Year;
            if (year >= 18 && year <= 25)
            {
                this.pnlForm1.Visible = false;
                this.pnlForm2.Visible = true;
            }
            else
                this.ShowErrorMessage("Debe tener de 18 a 25 años de edad para poder aspirar a ingresar a la EFOT.");
        }
        try
        {
        }
        catch (Exception exception)
        {
        }
    }
    protected void btnNextStep2_Click(object sender, EventArgs e)
    {
        if (base.Page.IsValid || Convert.ToInt16(this.ddlEstatura.SelectedValue) >= 170)
        {
            this.pnlForm2.Visible = false;
            this.pnlForm3.Visible = true;
            return;
        }
        this.ShowErrorMessage("Lo sentimos, usted no es apto para ingresar a la E.F.O.T., debe de tener al menos 170 cm de estatura.");
    }
    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        if (base.Page.IsValid)
        {
            DateTime dateTime = this.ConvertToDateTime(this.txtFecNac.Text);
            DateTime now = DateTime.Now;
            int year = now.Year - dateTime.Year;
            if (year >= 18 && year > 25 || Convert.ToInt16(this.ddlEstatura.SelectedValue) >= 170)
            {
                EFOT eFOT = new EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
                string str1 = Utils.CreateRandomPassword(8);
                Crypto crypto = new Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
                crypto.Key = ParametrosCifradoCs.key;
                crypto.IV = ParametrosCifradoCs.iv;
                string str2 = crypto.CifrarCadena(str1);
                if (eFOT.RegistrarAspirante(this.txtNombres.Text.Trim(), this.txtApellidos.Text.Trim(), this.txtCedula.Text.Trim(), this.txtDireccion.Text, this.txtEmail.Text, dateTime, this.ddlEstatura.SelectedValue, this.ddlSexo.SelectedValue, this.ddlEstadoCivil.SelectedValue, this.txtCargasFam.Text, this.txtIdeDactilar.Text, this.ddlPaisNac.SelectedValue, this.ddlProvNac.SelectedValue, this.ddlCiudadNac.SelectedValue, this.ddlTipoSangre.SelectedValue, int.Parse(this.txtPeso.Text), int.Parse(this.ddlTallaCalzado.SelectedValue), int.Parse(this.ddlTallaPantalon.SelectedValue), int.Parse(this.ddlTallaCamisa.SelectedValue), int.Parse(this.ddlTallaGorra.SelectedValue), str2))
                {
                    string empty = string.Empty;
                    if (!this.RegistrarDireccionResidencia(eFOT.DatosAspirante.CodigoAspirante))
                    {
                        empty = "Sus datos personales fueron registrados pero su dirección de residencia no pudo ser registrada, complemente sus datos ingresando con su número de cédula y contraseña asignada.";
                    }
                    if (this.SendNotificationEmail(this.txtEmail.Text, this.txtNombres.Text, this.txtCedula.Text, str1))
                    {
                        this.ShowSuccessMessage(string.Concat("Sus datos han sido registrados exitosamente, pronto recibirá un mensaje de confirmación en su correo electrónico ", this.txtEmail.Text));
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(empty))
                        {
                            this.ShowErrorMessage(string.Concat("Sus datos han sido registrados correctamente, no se pudo enviar el mensaje de confirmación a su correo electrónico ", this.txtEmail.Text));
                        }
                        else
                        {
                            this.ShowErrorMessage(string.Concat(empty, " No se pudo enviar el mensaje de confirmación a su correo electrónico ", this.txtEmail.Text));
                            this.ShowErrorMessage(eFOT.Error);
                        }
                    }
                }
            }
            else
                this.ShowErrorMessage("Debe de tener entre 18 y 25 años de edad, y medir más de 168cms para ingresar a la EFOT.");
        }
        try
        {
        }
        catch (Exception exception)
        {
        }
    }

    protected void btnTryAgain_Click(object sender, EventArgs e)
    {
        this.divError.Visible = false;
        this.divError.Visible = false;
        this.pnlForm1.Visible = true;
    }

    protected void ddlPaisNac_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadProvincias(this.ddlProvNac, this.ddlPaisNac.SelectedValue);
    }
    protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCiudades(this.ddlLocalidadRes, this.ddlProvinciaRes.SelectedValue);
    }
    protected void ddlProvNac_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.LoadCiudades(this.ddlCiudadNac, this.ddlProvNac.SelectedValue);
    }


    protected void HideFormPanels()
    {
        this.pnlForm1.Visible = false;
        this.pnlForm2.Visible = false;
        this.pnlForm3.Visible = false;
    }
    protected void LoadCiudades(DropDownList ddlCiudades, string codProvincia)
    {
        Geografia geografium = new Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView defaultView = geografium.GetCiudadesConCodigos(codProvincia).DefaultView;
        defaultView.Sort = string.Format("{0} {1}", defaultView.Table.Columns[1].ColumnName, "ASC");
        ddlCiudades.DataSource = defaultView;
        ddlCiudades.DataValueField = defaultView.Table.Columns[0].ColumnName;
        ddlCiudades.DataTextField = defaultView.Table.Columns[1].ColumnName;
        ddlCiudades.DataBind();
    }
    protected void LoadEstadoCivil()
    {
        this.ddlEstadoCivil.Items.Add(new ListItem("Soltero"));
        this.ddlEstadoCivil.Items.Add(new ListItem("Casado"));
        this.ddlEstadoCivil.Items.Add(new ListItem("Divorciado"));
        this.ddlEstadoCivil.Items.Add(new ListItem("Viudo"));
    }
    protected void LoadEstaturaDDL()
    {
        for (int i = 150; i <= 210; i++)
        {
            string str = i.ToString();
            ListItem listItem = new ListItem(str, str);
            if (i == 170)
            {
                listItem.Selected = true;
            }
            this.ddlEstatura.Items.Add(listItem);
        }
    }
    protected void LoadPaises(DropDownList ddlPaises)
    {
        Geografia geografium = new Geografia(ConfigurationManager.AppSettings[1.ToString()], ConfigurationManager.AppSettings[2.ToString()], ConfigurationManager.AppSettings[0.ToString()]);
        DataView defaultView = geografium.GetPaisesConCodigosConfContinente().DefaultView;
        defaultView.Sort = string.Format("{0} {1}", defaultView.Table.Columns[1].ColumnName, "ASC");
        ddlPaises.DataSource = defaultView;
        ddlPaises.DataValueField = defaultView.Table.Columns[0].ColumnName;
        ddlPaises.DataTextField = defaultView.Table.Columns[1].ColumnName;
        ddlPaises.DataBind();
    }
    protected void LoadProvincias(DropDownList ddlProvincias, string codPais)
    {
        Geografia geografium = new Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView defaultView = geografium.GetProvinciasConCodigos(codPais).DefaultView;
        defaultView.Sort = string.Format("{0} {1}", defaultView.Table.Columns[1].ColumnName, "ASC");
        ddlProvincias.DataSource = defaultView;
        ddlProvincias.DataValueField = defaultView.Table.Columns[0].ColumnName;
        ddlProvincias.DataTextField = defaultView.Table.Columns[1].ColumnName;
        ddlProvincias.DataBind();
    }
    protected void LoadSexoDDL()
    {
        this.ddlSexo.Items.Add(new ListItem("Masculino", "M"));
        this.ddlSexo.Items.Add(new ListItem("Femenino", "F"));
    }
    protected void LoadTallas()
    {
        Tallas talla = new Tallas(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dataTable1 = talla.TallasCalzado();
        this.ddlTallaCalzado.DataSource = dataTable1;
        this.ddlTallaCalzado.DataTextField = dataTable1.Columns[0].ColumnName;
        this.ddlTallaCalzado.DataValueField = dataTable1.Columns[0].ColumnName;
        this.ddlTallaCalzado.DataBind();
        DataTable dataTable2 = talla.TallasPantalon();
        this.ddlTallaPantalon.DataSource = dataTable2;
        this.ddlTallaPantalon.DataTextField = dataTable2.Columns[0].ColumnName;
        this.ddlTallaPantalon.DataValueField = dataTable2.Columns[0].ColumnName;
        this.ddlTallaPantalon.DataBind();
        DataTable dataTable3 = talla.TallasCamisa();
        this.ddlTallaCamisa.DataSource = dataTable3;
        this.ddlTallaCamisa.DataTextField = dataTable3.Columns[0].ColumnName;
        this.ddlTallaCamisa.DataValueField = dataTable3.Columns[0].ColumnName;
        this.ddlTallaCamisa.DataBind();
        DataTable dataTable4 = talla.TallasGorra();
        this.ddlTallaGorra.DataSource = dataTable4;
        this.ddlTallaGorra.DataTextField = dataTable4.Columns[0].ColumnName;
        this.ddlTallaGorra.DataValueField = dataTable4.Columns[0].ColumnName;
        this.ddlTallaGorra.DataBind();
    }
    protected void LoadTiposSangre()
    {
        Licencia licencium = new Licencia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dataTable = licencium.ObtenerTiposSangre();
        this.ddlTipoSangre.DataSource = dataTable;
        this.ddlTipoSangre.DataValueField = dataTable.Columns[0].ColumnName;
        this.ddlTipoSangre.DataTextField = dataTable.Columns[1].ColumnName;
        this.ddlTipoSangre.DataBind();
    }

    protected DateTime ConvertToDateTime(string mmddyyyy)
    {
        int num1 = mmddyyyy.IndexOf('/');
        int num2 = mmddyyyy.LastIndexOf('/');
        int num3 = int.Parse(mmddyyyy.Substring(0, num1));
        int num4 = int.Parse(mmddyyyy.Substring(num1 + 1, num2 - 1 - num1));
        int num5 = int.Parse(mmddyyyy.Substring(num2 + 1, mmddyyyy.Length - num2 + 1));
        DateTime dateTime = new DateTime(num5, num3, num4);
        return dateTime;
    }

    protected bool RegistrarDireccionResidencia(int codAspirante)
    {
        EFOTclass.EFOT.Aspirante aspirante = new EFOTclass.EFOT.Aspirante(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        if (aspirante.InsertDireccionOnDB(codAspirante, "DOM", this.ddlProvinciaRes.SelectedValue, this.ddlLocalidadRes.SelectedValue, this.txtDireccion.Text, this.txtTelConv.Text, this.txtTelCel.Text, this.txtReferenciaUbicac.Text))
        {
            return true;
        }
        return false;
    }

    protected bool SendNotificationEmail(string emailTo, string nombres, string numCedula, string password)
    {
        try
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(ParametrosEnvioEmail.mailAddress);
            mailMessage.To.Add(emailTo);
            mailMessage.Subject = "Registro de aspirante a la EFOT";
            string str1 = string.Concat("Saludos ", nombres, ",<br /><br />\n\nEste mensaje fue generado automáticamente para confirmar su solicitud de registro como aspirante a ingresar a la Escuela de Formación de Oficiales y Tropa de la Comisión de Tránsito del Ecuador.");
            str1 = string.Concat(str1, "<br /><br />\n\nPara terminar el proceso de registro debe ingresar a www.cte.gob.ec/EFOT, ingresar sus datos académicos, referencias personales y agregue direcciones de contacto en caso de ser necesario. <br /><br />\n\n");
            string str2 = str1;
            string[] strArrays = new string[5];
            strArrays[0] = str2;
            strArrays[1] = "Debe ingresar su usuario: ";
            strArrays[2] = numCedula;
            strArrays[3] = " y contraseña: ";
            strArrays[4] = password;
            str1 = string.Concat(strArrays);
            mailMessage.Body = str1;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.High;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = ParametrosEnvioEmail.smtpHost;
            smtpClient.Credentials = new System.Net.NetworkCredential(ParametrosEnvioEmail.smtpUser, ParametrosEnvioEmail.smptPassword);
            smtpClient.Send(mailMessage);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected void ShowErrorMessage(string message)
    {
        this.lblError.Text = message;
        this.divError.Style.Value = "visibility: visible; background-color: #fbbcc8";
        this.divSuccess.Style.Value = "visibility: hidden";
        this.HideFormPanels();
        ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.UpdatePanel2.GetType(), "click", "Rounded('div#divError', '#FFFFFF', '#fbbcc8');", true);
    }
    protected void ShowSuccessMessage(string message)
    {
        this.lblSuccess.Text = message;
        this.divError.Style.Value = "visibility: hidden";
        this.divSuccess.Style.Value = "visibility: visible; background-color: #cfe7f2";
        this.HideFormPanels();
        ScriptManager.RegisterStartupScript(this.UpdatePanel2, this.UpdatePanel2.GetType(), "click", "Rounded('div#divSuccess', '#FFFFFF', '#cfe7f2');", true);
    }

}