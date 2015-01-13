<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaCitacionDigitalizada.aspx.cs" Inherits="ConsultaCitacionDigitalizada" %>

<%@ Register TagPrefix="UserControls" TagName="header" Src="controls/header.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="footer" Src="controls/footer.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="backButton" Src="controls/backToHome.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
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
        <asp:Panel id="divCons" style="text-align:center" runat="server">
               <h3>Actas firmadas</h3>
                 <table width="350" align="center" class="tableConsulta2">
                   <tr>
                     <td>Número de Citación:</td>
                     <td>
                         <asp:TextBox ID="txtNumCitacion" runat="server"></asp:TextBox>
                     </td>
                   </tr>
                   <tr>
                     <td>Tipo:</td>
                     <td>
                         <asp:DropDownList ID="ddlTipo" runat="server">
                             <asp:ListItem Value="02">Adhesiva</asp:ListItem>
                             <asp:ListItem Value="01" Selected="True">Citación</asp:ListItem>
                         </asp:DropDownList>
                     </td>
                   </tr>
                   <tr>
                     <td colspan="2"><asp:Button ID="btnConsultar" Text="Consultar" CssClass="button" runat="server" OnClick="btnConsultar_Click" /><br />
                     </td>
                   </tr>
                 </table>
                 
                  
         </asp:Panel>
         <asp:Panel ID="pnlImagen" style="text-align:center" Visible="false" runat="server">
            <asp:Label ID="lblError" runat="server" CssClass="error"></asp:Label><br />
                  <div>
                  <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="./img/imprimir.gif" OnClientClick="javascript:window.print();" />
                  </div>
                  <div><asp:ImageButton ID="imgBtnCitacion" runat="server" OnClick="imgBtnCitacion_Click" Width="780px" /></div>
         </asp:Panel>     
    </div>
    </form>
</body>
</html>
