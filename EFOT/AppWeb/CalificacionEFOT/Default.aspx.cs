using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session[EFOTclass.Parametros.Session.Roles.InstructorCalificador] != null)
            {
                try
                {
                    if (Convert.ToBoolean(Session[EFOTclass.Parametros.Session.Roles.InstructorCalificador]))
                        lnkReporte.Visible = true;
                    else
                        lnkReporte.Visible = false;
                }
                catch (Exception) { lnkReporte.Visible = false; }
            }
            else
                lnkReporte.Visible = false;
        }
    }
}
