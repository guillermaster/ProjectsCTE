using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;


public partial class Registro : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSexoDDL();
            LoadEstadoCivil();
            LoadEstaturaDDL();
            LoadTiposSangre();
            LoadTallas();
            LoadPaises(ddlPaisNac);
            LoadProvincias(ddlProvinciaRes, "ECU");
            LoadCiudades(ddlLocalidadRes, ddlProvinciaRes.SelectedValue);
        }
    }


    protected void btnRegistrar_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                DateTime dtFecNac = ConvertToDateTime(txtFecNac.Text);
                int edad = DateTime.Now.Year - dtFecNac.Year;

                if (edad >= 18 && edad <= 25)
                {
                    if (Convert.ToInt16(ddlEstatura.SelectedValue) >= 168)
                    {
                        EFOTclass.EFOT oEFOT = new EFOTclass.EFOT(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
                        string password = Utilities.Utils.CreateRandomPassword(8);

                        CifradoCs.Crypto objCrypto = new CifradoCs.Crypto(CifradoCs.Crypto.CryptoProvider.TripleDES);
                        objCrypto.Key = Constantes.ParametrosCifradoCs.key;
                        objCrypto.IV = Constantes.ParametrosCifradoCs.iv;
                        string encPassword = objCrypto.CifrarCadena(password);

                        if (oEFOT.RegistrarAspirante(txtNombres.Text.Trim(), txtApellidos.Text.Trim(), txtCedula.Text.Trim(),
                            txtDireccion.Text, txtEmail.Text, dtFecNac, ddlEstatura.SelectedValue, ddlSexo.SelectedValue, ddlEstadoCivil.SelectedValue,
                            txtCargasFam.Text, txtIdeDactilar.Text, ddlPaisNac.SelectedValue, ddlProvNac.SelectedValue, ddlCiudadNac.SelectedValue,
                            ddlTipoSangre.SelectedValue, int.Parse(txtPeso.Text), int.Parse(ddlTallaCalzado.SelectedValue), int.Parse(ddlTallaPantalon.SelectedValue),
                            int.Parse(ddlTallaCamisa.SelectedValue), int.Parse(ddlTallaGorra.SelectedValue), encPassword))
                        {
                            string error = string.Empty;
                            if (!RegistrarDireccionResidencia(oEFOT.DatosAspirante.CodigoAspirante))
                                ShowSuccessMessage("Sus datos personales fueron registrados pero su dirección de residencia no pudo ser registrada, complemente sus datos ingresando con su número de cédula y contraseña asignada.");
                            if (SendNotificationEmail(txtEmail.Text, txtNombres.Text, txtCedula.Text, password))
                                ShowSuccessMessage("Sus datos han sido registrados exitosamente, pronto recibirá un mensaje de confirmación en su correo electrónico " + txtEmail.Text);
                            else
                            {
                                if (string.IsNullOrWhiteSpace(error))
                                    ShowErrorMessage("Sus datos han sido registrados correctamente, no se pudo enviar el mensaje de confirmación a su correo electrónico " + txtEmail.Text);
                                else
                                    ShowErrorMessage(error + " No se pudo enviar el mensaje de confirmación a su correo electrónico " + txtEmail.Text);
                            }
                        }
                        else
                        {
                            ShowErrorMessage(oEFOT.Error);
                        }
                    }
                    else
                        ShowErrorMessage("Los aspirantes deben de medir más de 168 cms. de estatura");
                }
                else
                    ShowErrorMessage("Los aspirantes deben ser de 18 a 25 años de edad.");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }
        else
            ShowErrorMessage("La información ingresada es incorrecta, verifíquela.");
    }

    protected bool RegistrarDireccionResidencia(int codAspirante)
    {
        EFOTclass.EFOT.Aspirante oAspirante = new EFOTclass.EFOT.Aspirante(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        if (oAspirante.InsertDireccionOnDB(codAspirante, EFOTclass.Parametros.CodTipoUbicacionDomicilio, ddlProvinciaRes.SelectedValue, ddlLocalidadRes.SelectedValue,
            txtDireccion.Text, txtTelConv.Text, txtTelCel.Text, txtReferenciaUbicac.Text))
            return true;
        else
            return false;
    }

    protected bool SendNotificationEmail(string emailTo, string nombres, string numCedula, string password)
    {
        try
        {
            System.Net.Mail.MailMessage correo = new System.Net.Mail.MailMessage();
            correo.From = new System.Net.Mail.MailAddress(Constantes.ParametrosEnvioEmail.mailAddress);
            correo.To.Add(emailTo);
            correo.Subject = "Registro de aspirante a la EFOT";
            string body = "Saludos " + nombres + ",<br /><br />\n\nEste mensaje fue generado automáticamente para confirmar su solicitud de registro como aspirante a ingresar a la Escuela de Formación de Oficiales y Tropa de la Comisión de Tránsito del Ecuador.";
            body += "<br /><br />\n\nPara terminar el proceso de registro debe ingresar a www.cte.gob.ec/EFOT, ingresar sus datos académicos, referencias personales y agregue direcciones de contacto en caso de ser necesario. <br /><br />\n\n";
            body += "Debe ingresar su usuario: " + numCedula + " y contraseña: " + password;
            correo.Body = body;
            correo.IsBodyHtml = true;
            correo.Priority = System.Net.Mail.MailPriority.High;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = Constantes.ParametrosEnvioEmail.smtpHost;
            smtp.Credentials = new System.Net.NetworkCredential(Constantes.ParametrosEnvioEmail.smtpUser, Constantes.ParametrosEnvioEmail.smptPassword);

            smtp.Send(correo);
            return true;
        }
        catch
        {
            return false;
        }
    }

    protected DateTime ConvertToDateTime(string mmddyyyy)
    {
        int d, m, y, idxFirst, idxLast;
        idxFirst = mmddyyyy.IndexOf('/');
        idxLast = mmddyyyy.LastIndexOf('/');
        m = int.Parse(mmddyyyy.Substring(0, idxFirst));
        d = int.Parse(mmddyyyy.Substring(idxFirst + 1, (idxLast - 1) - idxFirst));
        y = int.Parse(mmddyyyy.Substring(idxLast + 1, mmddyyyy.Length - (idxLast + 1)));
        DateTime dt = new DateTime(y, m, d);
        return dt;
    }

    protected void LoadSexoDDL()
    {
        ddlSexo.Items.Add(new ListItem("Masculino", "M"));
        ddlSexo.Items.Add(new ListItem("Femenino", "F"));
    }

    protected void LoadEstadoCivil()
    {
        ddlEstadoCivil.Items.Add(new ListItem("Soltero"));
        ddlEstadoCivil.Items.Add(new ListItem("Casado"));
        ddlEstadoCivil.Items.Add(new ListItem("Divorciado"));
        ddlEstadoCivil.Items.Add(new ListItem("Viudo"));
    }

    protected void LoadEstaturaDDL()
    {
        for (int i = 150; i <= 210; i++)
        {
            string si = i.ToString();
            ListItem li = new ListItem(si, si);
            if (i == 170)
                li.Selected = true;
            ddlEstatura.Items.Add(li);
        }
    }

    protected void LoadTiposSangre()
    {
        Brevetacion.Licencia oLicencia = new Brevetacion.Licencia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
                                ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtTiposSangre = oLicencia.ObtenerTiposSangre();
        ddlTipoSangre.DataSource = dtTiposSangre;
        ddlTipoSangre.DataValueField = dtTiposSangre.Columns[0].ColumnName;
        ddlTipoSangre.DataTextField = dtTiposSangre.Columns[1].ColumnName;
        ddlTipoSangre.DataBind();
    }
    protected void LoadPaises(DropDownList ddlPaises)
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView dvPaises = geog.GetPaisesConCodigosConfContinente().DefaultView;
        dvPaises.Sort = string.Format("{0} {1}", dvPaises.Table.Columns[1].ColumnName, "ASC");
        ddlPaises.DataSource = dvPaises;
        ddlPaises.DataValueField = dvPaises.Table.Columns[0].ColumnName;
        ddlPaises.DataTextField = dvPaises.Table.Columns[1].ColumnName;
        ddlPaises.DataBind();
    }
    protected void LoadProvincias(DropDownList ddlProvincias, string codPais)
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView dvProvincias = geog.GetProvinciasConCodigos(codPais).DefaultView;
        dvProvincias.Sort = string.Format("{0} {1}", dvProvincias.Table.Columns[1].ColumnName, "ASC");
        ddlProvincias.DataSource = dvProvincias;
        ddlProvincias.DataValueField = dvProvincias.Table.Columns[0].ColumnName;
        ddlProvincias.DataTextField = dvProvincias.Table.Columns[1].ColumnName;
        ddlProvincias.DataBind();
    }

    protected void LoadCiudades(DropDownList ddlCiudades, string codProvincia)
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView dvCiudades = geog.GetCiudadesConCodigos(codProvincia).DefaultView;
        dvCiudades.Sort = string.Format("{0} {1}", dvCiudades.Table.Columns[1].ColumnName, "ASC");
        ddlCiudades.DataSource = dvCiudades;
        ddlCiudades.DataValueField = dvCiudades.Table.Columns[0].ColumnName;
        ddlCiudades.DataTextField = dvCiudades.Table.Columns[1].ColumnName;
        ddlCiudades.DataBind();
    }

    protected void LoadTallas()
    {
        EFOTclass.Tallas oTallas = new EFOTclass.Tallas(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataTable dtTallasCalzado = oTallas.TallasCalzado();
        ddlTallaCalzado.DataSource = dtTallasCalzado;
        ddlTallaCalzado.DataTextField = dtTallasCalzado.Columns[0].ColumnName;
        ddlTallaCalzado.DataValueField = dtTallasCalzado.Columns[0].ColumnName;
        ddlTallaCalzado.DataBind();
        DataTable dtTallasPantalon = oTallas.TallasPantalon();
        ddlTallaPantalon.DataSource = dtTallasPantalon;
        ddlTallaPantalon.DataTextField = dtTallasPantalon.Columns[0].ColumnName;
        ddlTallaPantalon.DataValueField = dtTallasPantalon.Columns[0].ColumnName;
        ddlTallaPantalon.DataBind();
        DataTable dtTallasCamisa = oTallas.TallasCamisa();
        ddlTallaCamisa.DataSource = dtTallasCamisa;
        ddlTallaCamisa.DataTextField = dtTallasCamisa.Columns[0].ColumnName;
        ddlTallaCamisa.DataValueField = dtTallasCamisa.Columns[0].ColumnName;
        ddlTallaCamisa.DataBind();
        DataTable dtTallasGorra = oTallas.TallasGorra();
        ddlTallaGorra.DataSource = dtTallasGorra;
        ddlTallaGorra.DataTextField = dtTallasGorra.Columns[0].ColumnName;
        ddlTallaGorra.DataValueField = dtTallasGorra.Columns[0].ColumnName;
        ddlTallaGorra.DataBind();
    }

    protected void ShowErrorMessage(string message)
    {
        lblError.Text = message;
        divError.Style.Value = "visibility: visible; background-color: #fbbcc8";
        divSuccess.Style.Value = "visibility: hidden";
        HideFormPanels();
        ScriptManager.RegisterStartupScript(UpdatePanel2, UpdatePanel2.GetType(), "click", "Rounded('div#divError', '#FFFFFF', '#fbbcc8');", true);
    }

    protected void ShowSuccessMessage(string message)
    {
        lblSuccess.Text = message;
        divError.Style.Value = "visibility: hidden";
        divSuccess.Style.Value = "visibility: visible; background-color: #cfe7f2";
        HideFormPanels();
        ScriptManager.RegisterStartupScript(UpdatePanel2, UpdatePanel2.GetType(), "click", "Rounded('div#divSuccess', '#FFFFFF', '#cfe7f2');", true);
    }


    protected void btnNextStep1_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            try
            {
                DateTime dtFecNac = ConvertToDateTime(txtFecNac.Text);
                int edad = DateTime.Now.Year - dtFecNac.Year;

                if (edad >= 18 && edad <= 25)
                {
                    pnlForm1.Visible = false;
                    pnlForm2.Visible = true;
                }
                else
                    ShowErrorMessage("Lo sentimos, sólo estamos admitiendo a personas que tenga de 18 a 25 años de edad.");
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }
    }
    protected void btnNextStep2_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            if (Convert.ToInt16(ddlEstatura.SelectedValue) >= 168)
            {
                pnlForm2.Visible = false;
                pnlForm3.Visible = true;
            }
            else
                ShowErrorMessage("Lo sentimos, usted no es apto para ingresar a la E.F.O.T., debe de tener al menos 168 cm de estatura.");
        }
    }
    protected void HideFormPanels()
    {
        pnlForm1.Visible = false;
        pnlForm2.Visible = false;
        pnlForm3.Visible = false;
    }
    protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCiudades(ddlLocalidadRes, ddlProvinciaRes.SelectedValue);
    }
    protected void ddlPaisNac_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProvincias(ddlProvNac, ddlPaisNac.SelectedValue);
    }
    protected void ddlProvNac_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCiudades(ddlCiudadNac, ddlProvNac.SelectedValue);
    }
    protected void btnTryAgain_Click(object sender, EventArgs e)
    {
        divError.Visible = false;
        divError.Visible = false;
        pnlForm1.Visible = true;
    }
}