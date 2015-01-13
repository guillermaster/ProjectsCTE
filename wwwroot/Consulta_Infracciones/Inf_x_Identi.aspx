<%@ Register TagPrefix="uc1" TagName="cabecera" Src="cabecera.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codefile="Inf_x_Identi.aspx.vb" Inherits="Inf_x_Identi" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Infracciones Pendientes</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Scripts/style.css" type="text/css" rel="stylesheet">
        <link href="Scripts/style.css" rel="stylesheet" type="text/css" />
	</HEAD>
	<body>
		<script language="javascript">
	function Cerrar() {
	  var newconsulta = window.close('infplaca');
	}
	function Imprimir() {
	  window.print();
	}
	
		</script>
		<form id="Form1" method="post" runat="server">		
		<DIV align="center">
		  <TABLE id="Table3" style="WIDTH: 687px; HEIGHT: 232px" cellSpacing="0" cellPadding="0"
					border="0">
					<TR>
						<TD colSpan="2">
							<P align="center"><uc1:cabecera id="Cabecera1" runat="server"></uc1:cabecera></P>
						</TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 6px" colSpan="2">
							<P align="center"><IMG src="images/ConsInfraccPendientexIdentificacion.gif"></P>
						</TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 5px" colSpan="2">&nbsp;
						</TD>
					</TR>
					<TR>
						<TD>&nbsp;</TD>
						<TD style="HEIGHT: 5px">
							<P align="left"><asp:label id="lblPlaca" runat="server" CssClass="redtitle4 ç">IDENTIFICACIÓN:</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;
								<asp:label id="lbltitulo" runat="server" CssClass="blacktitle3"></asp:label></P>
						</TD>
					</TR>
					<TR>
						<TD style="HEIGHT: 116px" colSpan="2">
							<CENTER>
								<CENTER>
									<P align="center"><asp:datagrid id="GrdInfrac" runat="server" CssClass="blacktext1" BorderStyle="None" AllowSorting="True"
											AutoGenerateColumns="False" Width="696px" Height="64px" PageSize="5" AllowPaging="True" BorderColor="#999999"
											BorderWidth="1px" BackColor="White" CellPadding="3" GridLines="Vertical">
											<FooterStyle ForeColor="Black" BackColor="#CCCCCC"></FooterStyle>
											<AlternatingItemStyle BackColor="Gainsboro"></AlternatingItemStyle>
											<ItemStyle ForeColor="Black" BackColor="#EEEEEE"></ItemStyle>
											<HeaderStyle Font-Size="8pt" Font-Bold="True" ForeColor="White" BackColor="#000084"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="num_infraccion" SortExpression="num_infraccion" HeaderText="No.">
													<HeaderStyle Font-Size="9pt" Font-Bold="True"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="identificacion" SortExpression="identificacion" HeaderText="Identificaci&#243;n">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="fec_infraccion" SortExpression="fec_infraccion" HeaderText="Fec/Infracci&#243;n">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="placa" SortExpression="placa" HeaderText="Placa"></asp:BoundColumn>
												<asp:BoundColumn DataField="contravencion" SortExpression="contravencion" HeaderText="Contravenci&#243;n">
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="val_contrav" HeaderText="Val/Contrav" DataFormatString="{0:C}">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="mul_contrav" HeaderText=" Val/Multa" DataFormatString="{0:C}">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="total" SortExpression="total" HeaderText="Total" DataFormatString="{0:C}">
													<ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                    <HeaderStyle Width="75px" />
												</asp:BoundColumn>
											</Columns>
											<PagerStyle NextPageText="Anterior" PrevPageText="Siguiente" HorizontalAlign="Center" ForeColor="Black"
												BackColor="#999999" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></P>
								</CENTER>
							</CENTER>
						</TD>
					</TR>
					<tr>
						<td style="HEIGHT: 18px" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label
                                ID="Label4" runat="server" CssClass="whitetitle1">Cantidad:</asp:Label><asp:Label
                                    ID="lblCantidad" runat="server" BackColor="Black" CssClass="whitetitle1" ForeColor="White"></asp:Label>&nbsp;&nbsp;&nbsp;
							<asp:label id="lblMonto" runat="server" CssClass="whitetitle1">Valor Total: $</asp:label>&nbsp;&nbsp;&nbsp;
							<asp:label id="lbltotal" runat="server" CssClass="whitetitle1" BackColor="Black" ForeColor="White"></asp:label></td>
					
					</tr>
					<TR>
						<TD style="HEIGHT: 22px" colSpan="2">
							<P align="right">
									<TABLE id="Table2" style="WIDTH: 132px; HEIGHT: 27px" cellSpacing="1" cellPadding="1" width="132"
										border="0">
										<TR>
											<TD><asp:button id="btn_regresar" runat="server" CssClass="blacktitle1" Text="Regresar"></asp:button></TD>
											<TD>
												<P align="right"><INPUT class="blacktitle1" onclick="Cerrar()" type="button" value="Cerrar"></P>
											</TD>
										</TR>
									</TABLE>
							</P>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="Table1" border="0">
					<TR>
						<TD style="width: 7px">&nbsp;</TD>
						<TD style="width: 8px">&nbsp;</TD>
					</TR>
				</TABLE>
				</DIV>
		</form>
	</body>
</HTML>
