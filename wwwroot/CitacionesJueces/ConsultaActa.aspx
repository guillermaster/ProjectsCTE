<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaActa.aspx.cs" Inherits="ConsultaActa" %>
    
<%@ Register TagPrefix="UserControls" TagName="header" Src="controls/header.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="footer" Src="controls/footer.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="backButton" Src="controls/backToHome.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comisión de Tránsito del Ecuador</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/StyleSheet.css" />
    <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divHeader" runat="server">
        <UserControls:header id="Header" runat="server" />
        <UserControls:backButton ID="btnBack" runat="server" />
    </div>
    <div>
        <div id="divConsPorFecha" align="center" runat="server">
               <h3>Actas firmadas</h3>
                 <table width="350" align="center" class="tableConsulta2">
                   <tr>
                     <td>Número de Expediente:</td>
                     <td>
                         <asp:TextBox ID="txtNumExpediente" runat="server"></asp:TextBox>
                     </td>
                   </tr>
                   <tr>
                     <td>Número de Licencia:</td>
                     <td>
                         <asp:TextBox ID="txtNumLicencia" runat="server"></asp:TextBox>
                     </td>
                   </tr>
                   <tr>
                     <td>Tipo de Documento:</td>
                     <td>
                         <asp:DropDownList ID="ddlTipoDocumento" runat="server">
                             <asp:ListItem Value="A">Acta de Juzgamiento</asp:ListItem>
                             <asp:ListItem Value="N">Acta de Notificaci&#243;n</asp:ListItem>
                         </asp:DropDownList>
                     </td>
                   </tr>
                   <tr>
                     <td colspan="2"><asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="button" OnClick="btnConsultar_Click" /><br />
                     </td>
                   </tr>
                 </table>
              </div>
              
    </div>
    </form>
</body>
</html>
