<%@ Control Language="C#" AutoEventWireup="true" CodeFile="header.ascx.cs" Inherits="user_controls_header" %>
<LINK href="../css/StyleSheet.css" type="text/css" rel="stylesheet">
<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" align="center">
    <tr>
	  <td colspan="2" bgcolor="#EAEEF9" style="height: 3px;"></td>
	</tr>
	<tr>
	  <td style="width: 194px">
          <!--<img src="./img/mainLogoCTG.jpg" />--></td>
      <td bgcolor="#EFEFEF" align="center" valign="middle">
        <h1>Comisión de Tránsito del Ecuador</h1>
        <h2>Juzgados de Tránsito</h2>
      </td>
	</tr>
	<tr>
	  <td colspan="2" bgcolor="#EAEEF9" style="height: 3px;" align="right" valign="middle">
          <asp:Label ID="lblUser" runat="server" Text=""></asp:Label>
          <asp:LinkButton ID="btnCloseSession" runat="server" CausesValidation="false" CssClass="logout" OnClick="btnCloseSession_Click" Visible="False"><IMG SRC="./img/logout_16x16.png" style="margin-left: 20px; margin-right:3px;" width="16" height="16" />Cerrar Sesión</asp:LinkButton>
      </td>
	</tr>
</TABLE>
<div style="height: 20px"></div>