<%@ Register TagPrefix="uc1" TagName="cabecera" Src="cabecera.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Pie_pagina" Src="Pie_pagina.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codefile="Infracciones_Pendientes.aspx.vb" Inherits="Infracciones_Pendientes" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Consulta de Infracciones Pendientes por Identificación</title>
		<meta content="False" name="vs_showGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Scripts/style.css" type="text/css" rel="stylesheet">
        <link href="Scripts/style.css" rel="stylesheet" type="text/css" />
	</HEAD>
	<body leftMargin="0" topMargin="0" rightMargin="0">
	<form id="Form1" method="post" runat="server">
		<TABLE id="Table2a" style="WIDTH: 721px; HEIGHT: 8px" cellSpacing="0" cellPadding="0" width="721"
			align="center" border="0">
			<TR>
				<TD style="WIDTH: 109px" colSpan="3"><uc1:cabecera id="Cabecera1" runat="server"></uc1:cabecera></TD>
			</TR>
		</TABLE>
		<br /><br /><br />
			<DIV align="center">
				<table cellSpacing="0" cellPadding="0" width="410" align="center" border="0">
					<!--DWLayoutTable-->
					<TBODY>
						<tr>
							<td vAlign="top" colSpan="3" height="17" style="WIDTH: 404px; HEIGHT: 17px"><IMG src="images/consultas_r1_c1.gif"></td>
						</tr>
						<tr>
							<td vAlign="top" width="15" rowSpan="2" style="WIDTH: 15px; HEIGHT: 172px"><IMG src="images/consultas_r2_c1.gif"></td>
							<td style="WIDTH: 389px; HEIGHT: 156px" vAlign="top" width="389" height="156"><!--DWLayoutEmptyCell-->
								<CENTER>
									<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="302" border="0" style="WIDTH: 302px; HEIGHT: 88px">
										<TR>
											<TD style="WIDTH: 363px; HEIGHT: 14px" colSpan="3"><P align="center">
                                                <asp:Label ID="Label1" runat="server" CssClass="blacktitle3" Text="Consulta de Infracciones Pendientes"></asp:Label>&nbsp;</P>
											</TD>
										</TR>
										<TR>
											<TD style="WIDTH: 121px; HEIGHT: 58px" colSpan="3">
												<asp:RadioButtonList id="Rad_Consulta" runat="server" CssClass="blacktitle2" Width="290px" Height="40px">
													<asp:ListItem Value="P" Selected="True">Placa</asp:ListItem>
													<asp:ListItem Value="I">Identificaci&#243;n(C&#233;dula, RUC, Pasaporte)</asp:ListItem>
												</asp:RadioButtonList></TD>
										</TR>
										<TR>
											<TD style="WIDTH: 121px; HEIGHT: 23px">&nbsp;</TD>
											<TD style="WIDTH: 189px; HEIGHT: 23px">
												<P align="center"><asp:textbox id="TxtDato" runat="server" Width="133px" CssClass="blacktext2" MaxLength="14"></asp:textbox></P>
											</TD>
											<TD style="WIDTH: 110px; HEIGHT: 23px"><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" Width="1px" CssClass="whiteTitle" ForeColor="Highlight"
													ControlToValidate="TxtDato" ErrorMessage="Ingrese Dato a Consultar">*</asp:requiredfieldvalidator></TD>
										</TR>
									</TABLE>
									<TABLE id="Table2" cellSpacing="0" cellPadding="0" width="302" border="0" style="WIDTH: 302px"
										height="38">
										<TR>
											<TD>
												<p align="right"><asp:imagebutton id="consultarValido1" runat="server" ImageUrl="images/ConsultaValidoboton.gif"></asp:imagebutton></p>
											</TD>
											<td>
												<p align="left"><IMG onclick="self.close();" alt="" src="images/Cerrarvalido1.gif"></p>
											</td>
										</TR>
									</TABLE>
								</CENTER>
							</td>
							<td vAlign="top" width="4" rowSpan="2" style="WIDTH: 4px; HEIGHT: 172px"><IMG src="images/consultas_r2_c3.gif" height="181"></td>
						</tr>
						<tr>
							<td style="WIDTH: 389px; HEIGHT: 2px" vAlign="top" height="2"><IMG src="images/consultas_r3_c2.gif"></td>
						</tr>
						<tr>
							<td height="2" style="WIDTH: 15px"></td>
							<td style="WIDTH: 389px">
								<P align="center"><asp:validationsummary id="ValidationSummary1" runat="server" Width="200px" ForeColor="Highlight" Height="24px"
										Font-Size="Smaller" Font-Names="Arial"></asp:validationsummary></P>
							</td>
							<td style="WIDTH: 4px"></td>
						</tr>
					</TBODY>
				</table>
			</DIV>
		<DIV>
			<TABLE id="Table3" style="WIDTH: 721px; HEIGHT: 8px" cellSpacing="0" cellPadding="0" width="721"
				align="center" border="0">
				<TR>
					<TD style="WIDTH: 109px" colSpan="3">
						<uc1:Pie_pagina id="Pie_pagina1" runat="server"></uc1:Pie_pagina><FONT face="Times New Roman" color="#0000ff" size="3"></FONT></TD>
				</TR>
			</TABLE>
		</DIV>
		</FORM>
	</body>
</HTML>