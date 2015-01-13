<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaLicenciasPorNombre.aspx.cs" Inherits="ConsultaLicencias" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register TagPrefix="UserControls" TagName="header" Src="controls/header.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="footer" Src="controls/footer.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="backButton" Src="controls/backToHome.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
           <div runat="server">
           <h3>Búsqueda de Personas</h3>
           
                <table style="margin:0 0 20px 0;">
                  <tr>
                    <td style="height: 34px">
                        Nombre:</td>
                    <td style="height: 34px">
                        <asp:TextBox ID="txtNombre" runat="server" Width="284px">PIGUAVE PEREZ JUAN JOSE</asp:TextBox></td>
                    <td style="height: 34px">
                        <span class="helpText">(Ingrese apellidos y/o nombres)</span></td>
                  </tr>
                  <tr>
                    <td colspan="2" align="right">
                        <asp:Button ID="btnConsultaByCedula" runat="server" Text="Consultar" OnClick="btnConsultaByCedula_Click" CssClass="button" /></td>
                    <td align="right">
                        &nbsp;</td>
                  </tr>
                </table>
              </div>
         <asp:Label ID="lblError" runat="server" CssClass="error"></asp:Label>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" ForeColor="#333333" GridLines="None">
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="URL" DataTextField="IDENTIFICACION" 
                        HeaderText="Identificación" />
                    <asp:BoundField DataField="NOMBRE_COMPLETO" HeaderText="Nombre" />
                    <asp:BoundField DataField="DIRECCION" HeaderText="Dirección" />
                    <asp:BoundField DataField="TELEFONO1" HeaderText="Teléfono" />
                    <asp:BoundField DataField="CELULAR" HeaderText="Teléfono Móvil" />
                    <asp:BoundField DataField="PROVINCIA" HeaderText="Provincia" />
                    <asp:BoundField DataField="LOCALIDAD" HeaderText="Cantón" />
                </Columns>
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            </asp:GridView>

    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <UserControls:footer ID="Footer" runat="server" />
    </div>    
    </form>
</body>
</html>
