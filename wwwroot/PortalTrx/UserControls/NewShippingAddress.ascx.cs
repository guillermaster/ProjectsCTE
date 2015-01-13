using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

public partial class UserControls_NewShippingAddress : System.Web.UI.UserControl
{
    public event EventHandler Evento;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarPaises();
        }
        ddlProvincia.Attributes.Add("onChange", "DisplayDropDownListEditText('" + txtProvincia.ID +"', '" + ddlProvincia.ID +"')");
        ddlCiudad.Attributes.Add("onChange", "DisplayDropDownListEditText('" + txtCiudad.ID + "', '" + ddlCiudad.ID + "')");
        Page.Validate();
    }

    protected void CargarPaises()
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView dvPaises = geog.GetPaisesConCodigosConfContinente().DefaultView;
        dvPaises.Sort = string.Format("{0} {1}", dvPaises.Table.Columns[1].ColumnName, "ASC");

        DataRow nullrow = dvPaises.Table.NewRow();
        nullrow[0] = null;
        nullrow[1] = null;

        dvPaises.Table.Rows.InsertAt(nullrow, 0);

        ddlPais.DataSource = dvPaises;
        ddlPais.DataValueField = dvPaises.Table.Columns[0].ColumnName;
        ddlPais.DataTextField = dvPaises.Table.Columns[1].ColumnName;
        ddlPais.DataBind();
        
        /*this.ddlPais.Items.Add(new ListItem("-- Seleccione --", null));
        this.ddlPais.Items.Add(new ListItem("ECUADOR", "ECU"));*/
        ddlPais.SelectedValue = hdnCodEcuador.Value;
        CargarProvincias();
        SetValorPagar();
    }


    public void CargarProvincias()
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        DataView dvProvincias = geog.GetProvinciasConCodigos(ddlPais.SelectedValue).DefaultView;
        dvProvincias.Sort = string.Format("{0} {1}", dvProvincias.Table.Columns[1].ColumnName, "ASC");

        DataRow nullrow = dvProvincias.Table.NewRow();
        nullrow[0] = null;
        nullrow[1] = null;

        dvProvincias.Table.Rows.InsertAt(nullrow, 0);

        ddlProvincia.DataSource = dvProvincias;
        ddlProvincia.DataValueField = dvProvincias.Table.Columns[0].ColumnName;
        ddlProvincia.DataTextField = dvProvincias.Table.Columns[1].ColumnName;
        ddlProvincia.DataBind();

    }

    protected void CargarCiudades()
    {
        Constantes.Geografia geog = new Constantes.Geografia(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        string codProvincia = string.Empty;

        if (ddlProvincia.Items.Count>1 && (txtProvincia.Text.ToUpper() == ddlProvincia.SelectedItem.Text))
            codProvincia = ddlProvincia.SelectedValue;
        else
        {
            foreach (ListItem listItem in
                    ddlProvincia.Items.Cast<ListItem>().Where(listItem => txtProvincia.Text.ToUpper() == listItem.Text))
            {
                codProvincia = listItem.Value;
                break;
            }
        }

        if (!string.IsNullOrWhiteSpace(codProvincia))
        {
            DataView dvCiudades = geog.GetCiudadesConCodigos(ddlProvincia.SelectedValue).DefaultView;
            dvCiudades.Sort = string.Format("{0} {1}", dvCiudades.Table.Columns[1].ColumnName, "ASC");

            DataRow nullrow = dvCiudades.Table.NewRow();
            nullrow[0] = null;
            nullrow[1] = null;

            dvCiudades.Table.Rows.InsertAt(nullrow, 0);
            ddlCiudad.DataSource = dvCiudades;
            ddlCiudad.DataValueField = dvCiudades.Table.Columns[0].ColumnName;
            ddlCiudad.DataTextField = dvCiudades.Table.Columns[1].ColumnName;
            ddlCiudad.DataBind();
        }
        else
        {
            reqValCiudad.ControlToValidate = txtCiudad.ID;
            RemoveListaCiudadesItems();
        }
    }


    protected void ddlPais_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Evento != null)
            Evento(this, EventArgs.Empty);
        CargarProvincias();
        txtProvincia.Text = string.Empty;
        CargarCiudades();
        txtCiudad.Text = string.Empty;
        ClearValorPagar();
        SetValorPagar();
    }
    protected void ddlProvincia_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Evento != null)
            Evento(this, EventArgs.Empty);
        CargarCiudades();
        txtCiudad.Text = string.Empty;
        ClearValorPagar();
        SetValorPagar();
    }

    protected void ddlProvincia_DataBound(object sender, EventArgs e)
    {
        reqValProv.ControlToValidate = ddlProvincia.Items.Count == 1 ? txtProvincia.ID : ddlProvincia.ID;
    }

    protected void ddlCiudad_DataBound(object sender, EventArgs e)
    {
        reqValCiudad.ControlToValidate = ddlCiudad.Items.Count <= 1 ? txtCiudad.ID : ddlCiudad.ID;
    }

    protected void txtProvincia_TextChanged(object sender, EventArgs e)
    {
        reqValProv.ControlToValidate = txtProvincia.ID;
        txtCiudad.Text = string.Empty;
        CargarCiudades();
    }

    protected void RemoveListaCiudadesItems()
    {
        DataTable dt = new DataTable();
        ddlCiudad.DataSource = dt;
        ddlCiudad.DataBind();
    }

    protected void txtCiudad_TextChanged(object sender, EventArgs e)
    {
        reqValCiudad.ControlToValidate = txtCiudad.ID;
    }

    public string CostoTramite
    {
        get { return lblCostoTramite.Text; }
        set { lblCostoTramite.Text = value; }
    }

    protected void ClearValorPagar()
    {
        txtCosto.Text = "--";
    }

    protected void SetValorPagar()
    {
        CorreosDelEcuador.Shipping oShipping = new CorreosDelEcuador.Shipping(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        string value = oShipping.GetPaymentValue(ddlPais.SelectedValue, CodProvincia);
        txtCosto.Text = !string.IsNullOrWhiteSpace(value) ? value : "--";
    }
    


    public string NombrePersona1
    {
        get { return txtNombreResponsable1.Text.ToUpper(); }
    }

    public string NombrePersona2
    {
        get { return txtNombreResponsable2.Text.ToUpper(); }
    }

    public string Direccion
    {
        get { return txtDireccion.Text.ToUpper(); }
    }

    public string DireccionReferencia
    {
        get { return txtDirRef.Text.ToUpper(); }
    }

    public string CodPais
    {
        get { return ddlPais.SelectedValue; }
    }

    public string CodProvincia
    {
        get
        {
            if (ddlProvincia.Items.Count > 0)
            {
                if (txtProvincia.Text.ToUpper() == ddlProvincia.SelectedItem.Text)
                    return ddlProvincia.SelectedValue;
                foreach (ListItem listItem in
                    ddlProvincia.Items.Cast<ListItem>().Where(listItem => txtProvincia.Text.ToUpper() == listItem.Text))
                {
                    return listItem.Value;
                }
            }
            return string.Empty;
        }
    }

    public string CodCiudad
    {
        get
        {
            if (ddlCiudad.Items.Count > 0)
            {
                if (txtCiudad.Text.ToUpper() == ddlCiudad.SelectedItem.Text)
                    return ddlCiudad.SelectedValue;
                foreach (ListItem listItem in
                    ddlCiudad.Items.Cast<ListItem>().Where(listItem => txtCiudad.Text.ToUpper() == listItem.Text))
                {
                    return listItem.Value;
                }
            }
            return string.Empty;
        }
    }

    public string TelefonoConv
    {
        get { return txtTelefono.Text; }
    }

    public string TelefonoMovil
    {
        get { return txtTelefonoMovil.Text; }
    }

    public string CostoFlete
    {
        get { return txtCosto.Text; }
    }

    public string Ciudad
    {
        get { return txtCiudad.Text.ToUpper(); }
    }

    public string Provincia
    {
        get { return txtProvincia.Text.ToUpper(); }
    }
}