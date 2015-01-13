<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfraccionesPendientes.aspx.cs" Inherits="Consultas_Citaciones_InfraccionesPendientes" %>

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
    <form id="form1" runat="server" >
        <h2>
            Infracciones Pendientes de Pago
        </h2>  
        <uc1:logout ID="Logout1" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="height:10px; margin-bottom:20px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5">
        <ProgressTemplate>
            <div>
                <img src="../../img/ajax-loader.gif" /> 
                Procesando datos...
            </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        </div>
        
        
        <div>
        
                
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <asp:Menu
                ID="Menu1"
                Width="109px"
                runat="server"
                Orientation="Horizontal"
                StaticEnableDefaultPopOutImage="False"
                OnMenuItemClick="Menu1_MenuItemClick">
                <Items>
                    <asp:MenuItem ImageUrl="../../img/tabConsCitac0_on.gif" Text=" " Value="0"></asp:MenuItem>
                    <asp:MenuItem ImageUrl="../../img/tabConsCitac1_off.gif" Text=" " Value="1"></asp:MenuItem>
                </Items>
            </asp:Menu>
            <asp:MultiView 
            ID="MultiView1"
            runat="server"
            ActiveViewIndex="0"  >
           <asp:View ID="Tab1" runat="server"  >
                <table width="600" height="60" class="contactform" style="margin-top:0; padding-top:0px; margin-bottom:20px;" cellpadding=0 cellspacing=0>
                    <tr valign="top">
                        <td class="TabArea" style="width: 600px; height: 60px; border:0;">
                            <table width="100%">
                            <tr><td height="15" style="width: 58px"></td></tr>
                             <tr>
                              <td style="width: 58px">
                                  <asp:Label ID="lblIdentificacion" runat="server" Text="Identificación (Cédula/RUC/Pasaporte):" Width="231px"></asp:Label></td>
                              <td style="width: 291px">
                                  <asp:TextBox ID="txtIdentificacion" runat="server" ReadOnly="True" CssClass="field" Width="168px"></asp:TextBox></td>
                             </tr>
                            </table>
                        </td>
                    </tr>
                </table>
             </asp:View>
            <asp:View ID="Tab2" runat="server">
                <table width="600" height="60" class="contactform" style="margin-top:0; padding-top:0px;margin-bottom:20px;" cellpadding=0 cellspacing=0>
                    <tr valign="top">
                        <td class="TabArea" style="width: 600px; height: 60px; border:0;">
                            <table width="100%">
                            <tr><td height="15" style="width: 56px"></td></tr>
                             <tr>
                              <td style="width: 56px">
                                  <asp:Label ID="lblPlaca" runat="server" Text="Placa del vehículo:" Width="107px"></asp:Label></td>
                              <td style="width: 226px">
                                  <asp:TextBox ID="txtPlaca" runat="server" CssClass="field" Width="120px" MaxLength="7"></asp:TextBox>
                                  &nbsp;&nbsp;&nbsp; &nbsp;<asp:Button ID="btnConsultar" runat="server" CssClass="button2" Font-Bold="True"
                                      OnClick="btnConsultar_Click" Text="Consultar" Width="69px" /></td>
                             </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
            <div id="divError" runat="server" visible="false">
                <table class="error2" align="center" width="480">
                <tr>
                    <td style="width: 54px"><img src="../../img/error.gif" /></td>
                    <td><b>Error:</b><br /><asp:Label ID="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></td>
                </tr>         
                </table>
                </div>
                <div id="divWarning" runat="server" visible="false">
                <table class="warning" align="center" width="480">
                <tr>
                    <td style="width: 54px"><img src="../../img/warning.gif" /></td>
                    <td><asp:Label ID="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></td>
                </tr>         
                </table>
                </div>
                <br />
                <div>
                    <asp:datagrid id="GrdInfrac" runat="server" Width="618px"
										AutoGenerateColumns="False" AllowSorting="True" GridLines="None" CellPadding="3" ForeColor="#333333" CellSpacing="3" OnSelectedIndexChanged="GrdInfrac_SelectedIndexChanged">
										<FooterStyle ForeColor="White" BackColor="#507CD1" Font-Bold="True"></FooterStyle>
										<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
										<ItemStyle BackColor="#EFF3FB" Font-Size="X-Small" VerticalAlign="Top"></ItemStyle>
										<HeaderStyle Font-Size="X-Small" Font-Bold="True" ForeColor="White" BackColor="#507CD1" HorizontalAlign="Center"></HeaderStyle>
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
                                            <asp:BoundColumn DataField="puntos" HeaderText="Puntos">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundColumn>
											<asp:BoundColumn DataField="contravencion" SortExpression="contravencion" HeaderText="Contravenci&#243;n" Visible="False">
												<ItemStyle HorizontalAlign="Left"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="val_contrav" HeaderText="Valor($)">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="mul_contrav" HeaderText="Multa($)">
												<ItemStyle HorizontalAlign="Right"></ItemStyle>
											</asp:BoundColumn>
											<asp:BoundColumn DataField="total" SortExpression="total" HeaderText="Total($)">
												<ItemStyle HorizontalAlign="Right" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                                                <HeaderStyle Width="50px" />
											</asp:BoundColumn>
                                            <asp:ButtonColumn CommandName="Select" HeaderText="Detalles" Text="Ver"></asp:ButtonColumn>
										</Columns>
										<PagerStyle NextPageText="Anterior" PrevPageText="Siguiente" HorizontalAlign="Center" ForeColor="White"
											BackColor="#2461BF"></PagerStyle>
                        <EditItemStyle BackColor="#2461BF" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
									</asp:datagrid>
                </div>
                <br />
                <div align="right" style="width: 437px;">
                    &nbsp;<asp:ImageButton ID="btnCloseDetails" CausesValidation="false" Visible="false" runat="server" Height="9px" ImageUrl="~/img/icon_close.gif" OnClick="btnCloseDetails_Click" Width="10px" />
                    </div>
                <asp:DetailsView ID="dvDetalleCitacion" runat="server" Height="50px" Width="437px" EmptyDataText="---" EnableTheming="False" HeaderText="Detalles de Citación" CellPadding="4" CellSpacing="4" Font-Overline="False" GridLines="None" BackColor="#D1DDF1" Visible="False">
                            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" BorderStyle="None" Font-Bold="False" />
                            <FieldHeaderStyle HorizontalAlign="Left" VerticalAlign="Top" BorderStyle="None" Font-Bold="True" />
                            <HeaderStyle Width="20px" BorderStyle="None" Font-Bold="True" Font-Size="Small" VerticalAlign="Top" HorizontalAlign="Left" Font-Underline="True" />
                        </asp:DetailsView>
                <br />
                <div><asp:Label ID="Label1" runat="server" Text="Infracciones Pendientes:" Visible="false" /> <asp:Label ID="lblTotPendientes" runat="server" Visible="false" /></div>
                <div><asp:Label ID="Label2" runat="server" Text="Total a Pagar:" Visible="False" Font-Bold="True" /> <asp:Label ID="lblTotPagar" runat="server" Visible="false" /></div>
                <div>
                <asp:HyperLink ID="lnkVerPtos" NavigateUrl="PuntosPorLicencia.aspx" runat="server">Ver detalle de puntos</asp:HyperLink>
                </div>
            </ContentTemplate>
            </asp:UpdatePanel>
            
                <br />
            
            <br />
        </div>
        <div class="column1-unit">
		    <a href="../../DefaultConsultas.aspx"><img src="../../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
        </div>  
    </form>
</body>
</html>
