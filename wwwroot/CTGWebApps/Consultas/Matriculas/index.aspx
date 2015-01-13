<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Consultas_Matriculas_index" %>

<%@ Register Src="../../controls/logout.ascx" TagName="logout" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../../css/layout4_text.css" />
    <script type="text/javascript" src="../../js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <h2>
     Vehículos
        </h2> 
        <uc1:logout ID="Logout1" runat="server" />
        <div style="height:50px;">
            &nbsp;</div> 
    <div>
        <div><asp:Label ID="lblMensaje" runat="server"></asp:Label></div>
        <asp:GridView ID="gvVehiculos" runat="server" CellPadding="0" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Width="610px" OnSelectedIndexChanged="gvVehiculos_SelectedIndexChanged" CellSpacing="2">
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="placa" HeaderText="Placa" ReadOnly="True" SortExpression="placa" >
                    <ItemStyle Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="clase" HeaderText="Clase" SortExpression="clase" >
                    <ItemStyle Width="90px" />
                </asp:BoundField>
                <asp:BoundField DataField="marca" HeaderText="Marca" ReadOnly="True" SortExpression="marca" >
                    <ItemStyle Width="105px" />
                </asp:BoundField>
                <asp:BoundField DataField="modelo" HeaderText="Modelo" ReadOnly="True" SortExpression="modelo" >
                    <ItemStyle Width="280px" />
                </asp:BoundField>
                <asp:BoundField DataField="color" HeaderText="Color" ReadOnly="True" SortExpression="color" >
                    <ItemStyle Width="70px" />
                </asp:BoundField>
                <asp:CommandField HeaderText="M&#225;s datos" SelectText="Ver" ShowHeader="True"
                    ShowSelectButton="True">
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:CommandField>
            </Columns>
        </asp:GridView>
    
    </div>
    <br /><br />
        <div class="column1-unit">
		    <a href="../../DefaultConsultas.aspx"><img src="../../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
        </div> 
    </form>
</body>
</html>
