<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_text.css" />
    <script language="javascript">
     function clear_licencia(){
        document.getElementById("txtLicencia").value = '';
     }
    </script>
</head>
<body>
 <div align="center">
    <div>
        <img src="img/bg_head_middle_consulta.jpg" />
    </div>
    <h2>CONSULTAR ESTADO DE LICENCIA</h2>
    <form id="form1" runat="server">
    <div align="center" style="height: 10px">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <img src="img/ajax-loader.gif" width="70" height="10" />
            </ProgressTemplate>
        </asp:UpdateProgress>
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div id="divForm" align="center">
        <table class="contactform" id="tblForm">
        <tr>
            <td align="right"><asp:Label ID="lblLicencia" runat="server" Text="Número de Licencia:" Font-Size="Small"></asp:Label></td>
            <td><asp:TextBox ID="txtLicencia" runat="server" CausesValidation="True" MaxLength="18" Font-Size="Medium" AutoPostBack="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLicencia"
                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                &nbsp;
                </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="button" OnClick="btnConsultar_Click" Font-Bold="True" Font-Size="Small" /></td>
            <td align="left"><asp:Button ID="btnCancelar" runat="server" Text="Limpiar" CssClass="button" CausesValidation="False" UseSubmitBehavior="False" OnClick="btnCancelar_Click" OnClientClick="clear_licencia();" Font-Bold="True" Font-Size="Small" /></td>
        </tr>
        </table>
    </div>  
    <br /><br />
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red" Font-Size="Medium"></asp:Label>
    <div id="resultados" runat="server" style="width: 700px;" align="center">
        <table align="left" style="width: 591px">
         <tr><th align="left" style="width: 45px; height: 16px; font-size:small">Licencia:</th><td align="left" style="width: 297px; height: 16px"><asp:Label ID="lblResLicencia" runat="server" Font-Size="Small"></asp:Label></td></tr>
         <tr><th align="left" style="width: 45px; height: 16px; font-size:small">Nombre:</th><td align="left" style="width: 297px; height: 16px;"><asp:Label ID="lblResNombre" runat="server" Font-Size="Small"></asp:Label></td></tr>
        </table>
        <br />
        <br />
        <br />
        <br />
        <asp:GridView ID="grdLicencias" runat="server" Caption="Licencias"
                                CellPadding="4" Font-Names="Verdana" Font-Size="Medium" GridLines="None"
                                Width="691px" Height="20px" Font-Bold="True" ForeColor="#333333">
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <RowStyle BackColor="#EFF3FB" Font-Size="Small" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="Small" />
                                <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            </asp:GridView>
        <br />
        
        <asp:Label ID="lblTitInfPend" runat="server" Text="Infracciones Pendientes" Font-Bold="True" Font-Size="Medium"></asp:Label>
        <asp:datagrid id="GrdInfrac" runat="server" Width="690px"
										AutoGenerateColumns="False" AllowSorting="True" GridLines="None" CellPadding="0" ForeColor="#333333" Font-Bold="False">
										<FooterStyle ForeColor="White" BackColor="#507CD1" Font-Bold="True"></FooterStyle>
										<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
										<ItemStyle BackColor="#EFF3FB" Font-Size="Small" VerticalAlign="Top"></ItemStyle>
										<HeaderStyle Font-Size="Small" Font-Bold="True" ForeColor="White" BackColor="#507CD1" HorizontalAlign="Center"></HeaderStyle>
										<Columns>
											<asp:BoundColumn DataField="num_infraccion" SortExpression="num_infraccion" HeaderText="Citaci&#243;n">
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="fec_infraccion" SortExpression="fec_infraccion" HeaderText="Fecha">
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="identificacion" SortExpression="identificacion" HeaderText="Licencia">
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
                                            <asp:BoundColumn DataField="tipo" HeaderText="Tipo"></asp:BoundColumn>
											<asp:BoundColumn DataField="placa" SortExpression="placa" HeaderText="Placa"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="cod_contravencion" HeaderText="Art&#237;culo-Literal"></asp:BoundColumn>
											<asp:BoundColumn DataField="contravencion" SortExpression="contravencion" HeaderText="Contravenci&#243;n" Visible="False">
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="val_contrav" HeaderText="Valor ($)">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="mul_contrav" HeaderText="Multa ($)">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="total" SortExpression="total" HeaderText="Total ($)">
												<ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                                <HeaderStyle Width="75px" />
											</asp:BoundColumn>
										</Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
									</asp:datagrid>&nbsp;
		<table style="width: 231px">
		<th style="width: 96px">
            <asp:Label ID="lblEtiqTotPagar" runat="server" Text="Total a Pagar:" Width="105px" Font-Size="Small"></asp:Label></th><td style="width: 36px"><asp:Label ID="lblTotPagar" runat="server" Width="120px" Font-Size="Small"></asp:Label></td>
		</table>
        <br />
        
        <asp:GridView ID="grdBloqueos" runat="server" Caption="Bloqueos" CaptionAlign="Top"
                                CellPadding="4" Font-Names="Verdana" Font-Size="Medium" GridLines="None"
                                Width="591px" Font-Bold="True" ForeColor="#333333">
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <RowStyle BackColor="#EFF3FB" Font-Size="Small" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="Small" />
                                <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            </asp:GridView>
        <br />
        
        <asp:GridView ID="grdRestricciones" runat="server" Caption="Restricciones" CaptionAlign="Top"
                                CellPadding="4" Font-Names="Verdana" Font-Size="Medium" GridLines="None"
                                Width="591px" Font-Bold="True" ForeColor="#333333">
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <RowStyle BackColor="#EFF3FB" Font-Size="Small" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="Small" />
                                <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            </asp:GridView>
        <br />
        
        <asp:Label ID="lblInfraccionesPendientes" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="RoyalBlue"></asp:Label><br />
        <asp:Label ID="lblBloqueos" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="RoyalBlue"></asp:Label><br />
        <asp:Label ID="lblRestricciones" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="RoyalBlue"></asp:Label>
        <br />
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>  
    </form>
  </div>
</body>
</html>
