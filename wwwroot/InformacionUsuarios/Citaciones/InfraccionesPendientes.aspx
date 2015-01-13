<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InfraccionesPendientes.aspx.cs" Inherits="Consultas_Citaciones_InfraccionesPendientes" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Src="../controls/logout.ascx" TagName="logout" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_text.css" />
    <script type="text/javascript" src="../../js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server" onsubmit="javascript:clearLabel('lblMensaje'); return true;">
        <h2>
            Infracciones Pendientes
        </h2>  
        <uc1:logout ID="Logout1" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="height:10px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5">
        <ProgressTemplate>
            <div>
                <img src="../img/ajax-loader.gif" /> 
                Procesando datos...
            </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        </div>
        <div style="height:10px;">
            &nbsp;</div>
        
        <div>
                
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <div class="contactform">
                <p>
                  <table style="width: 414px">
                    <tr>
                     <td style="width: 213px"><asp:Label ID="lblIdentificacion" runat="server" Text="Ingrese Cédula/RUC/Pasaporte: "></asp:Label></td>
                     <td style="width: 179px"><asp:TextBox ID="txtIdentificacion" runat="server" MaxLength="20"></asp:TextBox></td>
                    </tr>
                    <tr>
                     <td colspan="2" style="height: 34px"><asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="button" OnClick="btnConsultar_Click" Font-Bold="True" /></td>
                    </tr>
                  </table>
                </p>
                </div>
                <br />
                <div><asp:Label ID="lblMensaje" runat="server"></asp:Label></div>
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
                <div><asp:Label ID="Label2" runat="server" Text="Total a Pagar:" Visible="false" /> <asp:Label ID="lblTotPagar" runat="server" Visible="false" /></div>
            </ContentTemplate>
            </asp:UpdatePanel>
            
                <br />
            
            <br />
        </div>
        <div class="column1-unit">
		    <a href="../Default.aspx"><img src="../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
        </div>  
    </form>
</body>
</html>
