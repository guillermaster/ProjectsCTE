<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaAudiencias.aspx.cs" Inherits="ConsultaAudiencias" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
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
               <h3>Consulta de Audiencias</h3>
                 <table width="350" align="center" class="tableConsulta2">
                   <tr>
                     <td>Fecha:</td>
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
                     <td colspan="2" style="height: 43px"><asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="button" OnClick="btnConsultar_Click" /><br />
                     </td>
                   </tr>
                 </table>
              </div>
              <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" EmptyDataText="No existen audiencias programadas para la fecha seleccionada." HorizontalAlign="Center">
                  <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                  <Columns>
                      <asp:BoundField HeaderText="Hora" DataField="hora" />
                      <asp:BoundField HeaderText="Expediente No." DataField="expediente" />
                      <asp:BoundField HeaderText="Infractor" DataField="infractor" />
                      <asp:BoundField HeaderText="Agente de Tr&#225;nsito" DataField="agente" />
                  </Columns>
                  <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                  <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                  <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                  <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                  <AlternatingRowStyle BackColor="Gainsboro" />
              </asp:GridView>
           </ContentTemplate>
         </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
