<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PuntosPorLicencia.aspx.cs" Inherits="PuntosPorLicencia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divHeader" runat="server">
        <UserControls:header id="Header" runat="server" />
        <UserControls:backButton ID="btnBack" runat="server" />
    </div>
    <div>
        &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server">
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
              <div id="divConsPorCedula" >
                <h3>Consulta de Citaciones por Licencia de Conductor Infractor</h3>
                <table width="350" align="center" class="tableConsulta1">
                  <tr>
                    <td>Cédula:</td>
                    <td style="height: 34px">
                        <asp:TextBox ID="txtCedula" runat="server" MaxLength="18"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqValCedula" runat="server" ControlToValidate="txtCedula"
                            ErrorMessage="&nbsp;*" ForeColor="Maroon"></asp:RequiredFieldValidator></td>
                  </tr>
                  <tr><td colspan="2" align="center"><asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="button" OnClick="btnConsultar_Click" /></td></tr>
                </table>
              </div>
              <asp:Label ID="lblError" runat="server" CssClass="error"></asp:Label><br />
              <div id="divPuntos" runat="server" visible="false">
                <table width="350" align="center">
                  <tr>
                    <th>Puntos perdidos:</th>
                    <td><asp:Label ID="lblPuntosPerdidos" runat="server" Font-Size="Medium"></asp:Label></td>
                  </tr>
                  <tr>
                    <th>Total de puntos:</th>
                    <td><asp:Label ID="lblTotalPuntos" runat="server" Font-Size="Medium"></asp:Label></td>
                  </tr>
                </table>
                  
              </div>
              
          </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
    <div>
        <UserControls:footer ID="Footer" runat="server" />
    </div>
    </form>
</body>
</html>
