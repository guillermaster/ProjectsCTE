<%@ control language="C#" autoeventwireup="true" inherits="user_controls_header, App_Web_header.ascx.6bb32623" %>
<LINK href="../css/StyleSheet.css" type="text/css" rel="stylesheet">
<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" align="center">
    <tr>
	  <td colspan="3" bgcolor="#EAEEF9" style="height: 3px;"></td>
	</tr>
	<tr>
	  <td style="width: 194px">
          <!--<img src="./img/mainLogoCTG.jpg" />--></td>
      <td bgcolor="#EFEFEF" align="center" valign="middle">
        <h1>CTE - ANT - Policía Nacional</h1>
        <h2>Datos de Tránsito</h2>
      </td>
      <td style="width: 194px">
          <img src="./img/mainLogoPolicia.jpg" /></td>
	</tr>
	<tr>
	  <td colspan="3" bgcolor="#EAEEF9" style="height: 3px;" align="right" valign="middle">
          <asp:Label ID="lblUser" runat="server" Text=""></asp:Label>
          <asp:LinkButton ID="btnCloseSession" runat="server" CssClass="logout" 
              OnClick="btnCloseSession_Click" Visible="False" CausesValidation="False"><IMG SRC="./img/logout_16x16.png" style="margin-left: 20px; margin-right:3px;" width="16" height="16" />Cerrar Sesión</asp:LinkButton>
      </td>
	</tr>
</TABLE>
<div style="height: 20px"></div>