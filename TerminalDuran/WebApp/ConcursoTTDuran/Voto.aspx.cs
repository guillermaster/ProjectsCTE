using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TTDuran;
using System.Configuration;


public partial class _Default : System.Web.UI.Page
{
    private static string[] nombresSugeridos;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetNombresSugeridos();
        }
    }

    private void GetNombresSugeridos()
    {
        //string[] tmp = { "GUILLERMO PINCAY", "GUILLERMO SEBASTIÁN", "GUILLERMO PINCAY", "JUAN PUEBLO", "JUAN JUAN" };
        TerminalDuran oTTDuran = new TerminalDuran(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()], ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);
        nombresSugeridos = oTTDuran.GetNombresPropuestos();
    }


    protected void step1NextBtn_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, typeof(Page), "click", "goStep2()", true);
    }


    protected void step2NextBtn_Click(object sender, EventArgs e)
    {
        TerminalDuran oTTDuran = new TerminalDuran(ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.usuario.ToString()],
            ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.clave.ToString()], ConfigurationManager.AppSettings[Constantes.WebApp.BaseDatosKeys.tns.ToString()]);

        if(oTTDuran.InsertNameProposal(txtNombreTT.Text, txtMotivoNombreTT.Text, txtCedula.Text, txtNombre.Text, txtTelefono.Text, txtEmail.Text))
            pnlMessages.Controls.Add(HtmlWriter.Messages.PrintInfoMessage("Su sugerencia ha sido enviada."));
        else
            pnlMessages.Controls.Add(HtmlWriter.Messages.PrintErrorMessage("No se pudo enviar su sugerencia. " + oTTDuran.Error));

        ScriptManager.RegisterStartupScript(this, typeof(Page), "click", "goStep3()", true);
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static string[] GetCompletionList(string prefixText, int count, string contextKey)
    {
        var query = from s in nombresSugeridos where s.Contains(prefixText.ToUpper()) select s;
        ArrayList result = new ArrayList();
        foreach (var val in query)
        {
            result.Add(val);
        }
        return ((string[])result.ToArray(typeof(string)));
    }
}
