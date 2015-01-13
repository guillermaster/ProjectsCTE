<%@ page language="C#" autoeventwireup="true" inherits="_Default, App_Web_vehiculosporpersona.aspx.cdcab7d2" theme="SkinFile" %>

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
    <div align="center" style="margin-top:10px;">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            CellPadding="4" ForeColor="#333333" GridLines="None">
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <Columns>
                <asp:HyperLinkField DataNavigateUrlFields="URL" DataTextField="Placa" 
                    HeaderText="Placa" />
                <asp:BoundField DataField="clase" HeaderText="Clase" />
                <asp:BoundField DataField="marca" HeaderText="Marca" />
                <asp:BoundField DataField="modelo" HeaderText="Modelo" />
                <asp:BoundField DataField="color" HeaderText="Color" />
            </Columns>
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#999999" />
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        </asp:GridView>
    </div>
    <div>
        <UserControls:footer ID="Footer" runat="server" />
    </div>
    </form>
</body>
</html>
