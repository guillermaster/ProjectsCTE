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

public partial class UserControls_SaveButton : System.Web.UI.UserControl
{
    public event EventHandler ButtonClickDemo;


    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //    this.hdnTipoGuardar.Value = ((int)AEA.Parametros.Acciones.Guardar.Nuevo).ToString();
    }

    public AEA.Parametros.Acciones.Guardar Tipo
    {
        get
        {
            return (AEA.Parametros.Acciones.Guardar)int.Parse(this.hdnTipoGuardar.Value);
        }
        set
        {
            this.hdnTipoGuardar.Value = ((int)value).ToString();
            switch (value)
            {
                case AEA.Parametros.Acciones.Guardar.Nuevo:
                    this.btnSave.ImageUrl = "~/img/btnGuardar.gif";
                    break;
                case AEA.Parametros.Acciones.Guardar.Actualizar:
                    this.btnSave.ImageUrl = "~/img/btnGuardarAct.gif";
                    break;
            }
        }
    }
    protected void btnSave_Click(object sender, ImageClickEventArgs e)
    {
        ButtonClickDemo(sender, e);
    }
}
