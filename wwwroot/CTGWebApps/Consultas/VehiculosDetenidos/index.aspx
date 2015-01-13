<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="VehiculosDetenidos" %>

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
    <h2>Vehículos Retenidos</h2><uc1:logout ID="Logout1" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="height:10px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5">
            <ProgressTemplate>
            <div>
                <img src="../../img/ajax-loader.gif" /> 
                Procesando datos...
            </div>
        </ProgressTemplate>
        </asp:UpdateProgress>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="contactform">
                <p>
                  <table style="width: 414px">
                    <tr>
                     <td style="width: 213px"><asp:Label ID="lblIdentificacion" runat="server" Text="Ingrese identificación: "></asp:Label></td>
                     <td style="width: 179px"><asp:TextBox ID="txtIdentificacion" runat="server" MaxLength="20"></asp:TextBox></td>
                    </tr>
                    <tr>
                     <td style="width: 213px; height: 81px"><asp:Label ID="lblTipoIdentificacion" runat="server" Text="Seleccione el tipo de identificación: "></asp:Label></td>
                     <td style="height: 81px; width: 179px;">
                         <asp:RadioButtonList ID="rbTipoIdentificacion" runat="server">
                             <asp:ListItem Selected="True" Value="placa">Placa</asp:ListItem>
                             <asp:ListItem Value="ramv">RAMV</asp:ListItem>
                             <asp:ListItem Value="licencia">C&#233;dula/RUC/Pasaporte</asp:ListItem>
                         </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                     <td colspan="2" style="height: 34px"><asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="button" OnClick="btnConsultar_Click" Font-Bold="True" /></td>
                    </tr>
                  </table>
                </p>
                </div>
                <br /><br />
                <asp:Label ID="lblEstado" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                <br /><br />
                <asp:GridView ID="gvVehicRet" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="602px">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="fecha_hora" HeaderText="Ingreso" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="100px" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="dias" HeaderText="D&#237;as retenido" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="canchon" HeaderText="&#191;D&#243;nde est&#225;?" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="180px" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="placa" HeaderText="Placa" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50px" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="marca" HeaderText="Marca" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="90px" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="modelo" HeaderText="Modelo" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="135px" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="color" HeaderText="Color" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="60px" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                    </Columns>
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <br /><br />
                <asp:HyperLink ID="hypInfoCanchon1" runat="server" NavigateUrl="javascript:popup('infoCanchones.htm', 480, 150, 1);" Visible="false">Ver información de canchón</asp:HyperLink>
                <br /><br /><br />
                <div class="column1-unit">
		            <a href="../../DefaultConsultas.aspx"><img src="../../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
                </div> 
            </ContentTemplate>
        </asp:UpdatePanel>
     
    </form>
</body>
</html>
