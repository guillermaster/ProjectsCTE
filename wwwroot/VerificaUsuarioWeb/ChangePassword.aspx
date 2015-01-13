<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="LoginExtra_UserRegistartion" %>
<%@ Register Src="controls/sesionuser.ascx" TagName="sesionuser" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="css/layout4_text.css" />
</head>
<body>
  <div align="center" style="width: 700px;" >
    <div>
        <img src="img/bg_head_middle_consulta.jpg" />
    </div>
    <form id="form1" runat="server">
    <uc2:sesionuser ID="Sesionuser1" runat="server" />
    <h2>VERIFICACIÓN DE USUARIOS WEB</h2>
    <h3>Cambio de Contraseña</h3>
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>&nbsp;<br />
        <br />
        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" 
            Visible="False" Font-Size="Small" PostBackUrl="~/Default.aspx">Continuar</asp:LinkButton><div id="divForm" runat="server">
        <div id="tableDiv" runat="server">
        <table class="contactform" id="tblForm">
        <tr>
            <td align="right" style="width: 194px"><asp:Label ID="lblContrasena" runat="server" Text="Contraseña:"></asp:Label></td>
            <td><asp:TextBox ID="txtContrasena" runat="server" CausesValidation="True" MaxLength="12" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvContrasena" runat="server" ErrorMessage="*" ControlToValidate="txtContrasena"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="right" style="width: 194px"><asp:Label ID="lblConfContrasena" runat="server" Text="Confirmar Contraseña:"></asp:Label></td>
            <td><asp:TextBox ID="txtConfContrasena" runat="server" CausesValidation="True" MaxLength="12" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvConfContrasena" runat="server" ErrorMessage="*" ControlToValidate="txtConfContrasena"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="height: 26px">
                <asp:CompareValidator ID="cvContrasenaConfirmacion" runat="server" ErrorMessage="La contraseña y la confirmación de contraseña deben ser iguales." ControlToCompare="txtConfContrasena" ControlToValidate="txtContrasena"></asp:CompareValidator>&nbsp;<br />
                <br />
                </td>
        </tr>
        <tr>
            <td align="right" style="width: 194px">
                <asp:Button ID="btnChangePwd" runat="server" CssClass="button" Font-Bold="True" OnClick="btnChangePwd_Click"
                    Text="Cambiar contraseña" Width="140px" /></td>
            <td align="left"><asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" CausesValidation="False"  PostBackUrl="~/Default.aspx" /></td>
        </tr>
        </table>
        </div>
    </div>
    </form>
   </div>
</body>
</html>
