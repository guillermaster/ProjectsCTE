using System;
using System.Web.UI.HtmlControls;
using System.Web.UI;


namespace HtmlWriter
{
    public class Messages
    {
        public static HtmlTable PrintAlertMessage(string title, string message, string iconRefUrl, string cssClass)
        {
            HtmlTable table = new HtmlTable();
            table.Attributes["class"] = cssClass;
            HtmlTableRow tr = new HtmlTableRow();
            HtmlTableCell tdLeft = new HtmlTableCell();
            tdLeft.Width = "54";
            HtmlImage img = new HtmlImage();
            img.Src = iconRefUrl;
            tdLeft.Controls.Add(img);
            tr.Cells.Add(tdLeft);
            HtmlTableCell tdRight = new HtmlTableCell();
            string htmlMessage = "<b>" + title + "</b><br />" + message;
            tdRight.InnerHtml = htmlMessage;
            tr.Cells.Add(tdRight);
            table.Rows.Add(tr);
            return table;
        }

        public static HtmlTable PrintInfoMessage(string message)
        {
            HtmlTable table = new HtmlTable();
            table.Attributes["class"] = "info";
            table.Align = "center";
            HtmlTableRow tr = new HtmlTableRow();
            HtmlTableCell td = new HtmlTableCell();
            td.Attributes["style"] = "padding: 20px 5px 5px 70px;";
            td.InnerHtml = message;
            tr.Controls.Add(td);
            table.Rows.Add(tr);

            return table;
        }

        public static HtmlTable PrintErrorMessage(string message)
        {
            HtmlTable table = new HtmlTable();
            table.Attributes["class"] = "error";
            table.Align = "center";
            HtmlTableRow tr = new HtmlTableRow();
            HtmlTableCell td = new HtmlTableCell();
            td.Attributes["style"] = "padding: 35px 5px 5px 70px;";
            td.InnerHtml = message;
            tr.Controls.Add(td);
            table.Rows.Add(tr);

            return table;
        }

        public static void ShowMainContentInfo(MasterPage masterPage, HtmlGenericControl divContentToHide, string message)
        {
            Controls.AddTableToControlOnMasterPage(masterPage,
                Constantes.WebApp.MasterPageControls.divMainContentError.ToString(), PrintInfoMessage(message));
            divContentToHide.Visible = false;
        }

        public static void ShowMainContentError(MasterPage masterPage, HtmlGenericControl divContentToHide, string message)
        {
            Controls.AddTableToControlOnMasterPage(masterPage,
                Constantes.WebApp.MasterPageControls.divMainContentError.ToString(), PrintErrorMessage(message));
            divContentToHide.Visible = false;
        }

        public static void HideMainContentError(MasterPage masterPage, HtmlGenericControl divContentToShow)
        {
            Controls.HideControlOnMasterPage(masterPage,
                Constantes.WebApp.MasterPageControls.divMainContentError.ToString());
            divContentToShow.Visible = true;
        }

        public static void ShowModalFailureMessage(UpdatePanel updatePanel, Type tipo, string message)
        {
            if(updatePanel!=null && tipo!=null)
                ScriptManager.RegisterStartupScript(updatePanel, tipo, "click", "csscody.error('" + message + "')", true);
        }

        public static void ShowModalInfoMessage(UpdatePanel updatePanel, Type tipo, string message)
        {
            if (updatePanel != null && tipo != null)
                ScriptManager.RegisterStartupScript(updatePanel, tipo, "click", "csscody.info('" + message + "')", true);
        }

        public static void ShowModalAlertMessage(UpdatePanel updatePanel, Type tipo, string message)
        {
            if (updatePanel != null && tipo != null)
                ScriptManager.RegisterStartupScript(updatePanel, tipo, "click", "csscody.alert('" + message + "')", true);
        }
    }
}
