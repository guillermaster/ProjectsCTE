using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace HtmlWriter
{
    public class Controls
    {
        public static bool AddTableToControlOnMasterPage(MasterPage masterPage, string controlId, HtmlTable table)
        {
            try
            {
                Control webCtrl = masterPage.FindControl(controlId);
                webCtrl.Controls.Add(table);
                webCtrl.Visible = true;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool HideControlOnMasterPage(MasterPage masterPage, string controlId)
        {
            try
            {
                masterPage.FindControl(controlId).Visible = false;
                return true;
            }
            catch
            {
                return false;
            }
        }                
    }
}
