<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NotificacionAudiencia.aspx.cs" Inherits="NotificacionAudiencia" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register TagPrefix="UserControls" TagName="header" Src="controls/header.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="footer" Src="controls/footer.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="backButton" Src="controls/backToHome.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comisión de Tránsito del Ecuador</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/StyleSheet.css" />
    <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divHeader" runat="server">
        <UserControls:header id="Header" runat="server" />
        <UserControls:backButton ID="btnBack" runat="server" />
    </div>
    <div>
      <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
          <ProgressTemplate>
            <div>
                <img src="./img/ajax-loader.gif" />&nbsp;Cargando...
            </div>
          </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
        <div id="divConsPorFecha" align="center" runat="server">
               <h3>Notificación de Audiencia</h3>
               <asp:Label ID="lblError" runat="server" CssClass="error"></asp:Label>
               <br /><br />
                 <table width="350" align="center" class="tableConsulta2">
                   <tr>
                     <td>No. Citación:</td>
                     <td>
                         <asp:TextBox ID="txtNoCitacion" runat="server" OnTextChanged="txtNoCitacion_TextChanged" AutoPostBack="true"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqValNoCitacion" runat="server" ErrorMessage="*" ControlToValidate="txtNoCitacion" ForeColor="Maroon"></asp:RequiredFieldValidator></td>
                   </tr>
                   <tr>
                     <td>No. Expediente:</td>
                     <td>
                         <asp:TextBox ID="txtNoExpediente" runat="server" ReadOnly="True"></asp:TextBox></td>
                   </tr>
                   <tr>
                     <td>Ciudad:</td>
                     <td>
                         <asp:TextBox ID="txtCiudad" runat="server" ReadOnly="True"></asp:TextBox></td>
                   </tr>
                   <tr>
                     <td>Día:</td>
                     <td>
                         <asp:TextBox ID="txtFechaDesde" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqValFechDesde" runat="server" ControlToValidate="txtFechaDesde"
                             ErrorMessage="&nbsp;*" ForeColor="Maroon"></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="regexValFechaIni" runat="server" ControlToValidate="txtFechaDesde"
                             ErrorMessage="&nbsp;&nbsp;*"
                             ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" ForeColor="Maroon"></asp:RegularExpressionValidator>
                         <cc1:CalendarExtender ID="calExtDesde" runat="server" TargetControlID="txtFechaDesde" Format="dd/MM/yyyy">
                         </cc1:CalendarExtender>
                     </td>
                   </tr>
                   <tr>
                     <td>Hora:</td>
                     <td>
                         <asp:DropDownList ID="ddlHoras" runat="server">
                         </asp:DropDownList>:<asp:DropDownList ID="ddlMinutos" runat="server">
                         </asp:DropDownList></td>
                   </tr>
                   <tr>
                     <td colspan="2" style="height: 43px"><asp:Button ID="btnConsultar" runat="server" Text="Ingresar Notificación" CssClass="button" OnClick="btnConsultar_Click" />
                     <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelar_Click"  /><br />
                     </td>
                   </tr>
                 </table>
              </div>
           </ContentTemplate>
         </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
