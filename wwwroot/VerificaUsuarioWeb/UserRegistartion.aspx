<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserRegistartion.aspx.cs" Inherits="LoginExtra_UserRegistartion" %>

<%@ Register Src="controls/sesionuser.ascx" TagName="sesionuser" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="css/layout4_text.css" />
</head>
<body>
  <div align="center" style="width: 700px;">
    <div>
        <img src="img/bg_head_middle_consulta.jpg" />
    </div>
    
    <form id="form1" runat="server">
    <uc1:sesionuser ID="Sesionuser1" runat="server" />
    <h2>
        
        REGISTRO DE USUARIO WEB</h2>
    <br /><br />
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>&nbsp;<br />
        <br />
        <asp:HyperLink ID="lnkContinuar" runat="server" NavigateUrl="Default.aspx" Visible="False">Continuar</asp:HyperLink>
        <div id="divForm" runat="server">
        <table class="contactform" id="tblForm">
        <tr>
            <td align="right" style="width: 194px"><asp:Label ID="lblCedula" runat="server" Text="Cédula/Pasaporte/RUC:"></asp:Label></td>
            <td><asp:TextBox ID="txtCedula" runat="server" CausesValidation="True" MaxLength="13"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNumCedula" runat="server" ErrorMessage="*" ControlToValidate="txtCedula"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="right" style="width: 194px"><asp:Label ID="lblNombres" runat="server" Text="Nombres:"></asp:Label></td>
            <td><asp:TextBox ID="txtNombres" runat="server" CausesValidation="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNombres" runat="server" ErrorMessage="*" ControlToValidate="txtNombres"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rgValNombres" runat="server" ControlToValidate="txtNombres"
                    ErrorMessage="*" ValidationExpression="[a-zA-Z áéíóúüñ]{3,}"></asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="right" style="width: 194px"><asp:Label ID="lblApellidos" runat="server" Text="Apellidos:"></asp:Label></td>
            <td><asp:TextBox ID="txtApellidos" runat="server" CausesValidation="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvApellidos" runat="server" ErrorMessage="*" ControlToValidate="txtApellidos"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="rgValApellidos" runat="server" ControlToValidate="txtApellidos"
                    ErrorMessage="*" ValidationExpression="[a-zA-Z áéíóúüñ]{3,}"></asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="right" style="width: 194px"></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align="right" style="height: 22px; width: 194px;"><asp:Label ID="lblEmail" runat="server" Text="Correo Electrónico:"></asp:Label></td>
            <td style="height: 22px"><asp:TextBox ID="txtEmail" runat="server" CausesValidation="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="right" style="height: 22px; width: 194px;"><asp:Label ID="lblConfEmail" runat="server" Text="Confirmar Correo Electrónico:"></asp:Label></td>
            <td style="height: 22px"><asp:TextBox ID="txtConfEmail" runat="server" CausesValidation="True"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td align="right" style="width: 194px"></td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="height: 26px">
                &nbsp;<asp:CompareValidator ID="cvEmailConfirmacion" runat="server" ErrorMessage="La correo electrónico y la confirmación de correo electrónico deben ser iguales." ControlToCompare="txtConfEmail" ControlToValidate="txtEmail" Width="459px"></asp:CompareValidator>
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                    ErrorMessage="El correo electrónico ingresado es inválido." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                <br />
                <asp:RegularExpressionValidator ID="rvCedula" runat="server" ErrorMessage="El número de Cedula/Pasaporte es inválido" ControlToValidate="txtCedula" ValidationExpression="[0-9A-Z]{5,}"></asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td align="right" style="width: 194px"><asp:Button ID="btnRegistrar" runat="server" Text="Crear usuario" CssClass="button" OnClick="btnRegistrar_Click" /></td>
            <td align="left"><asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" CausesValidation="False" OnClick="btnCancelar_Click" /></td>
        </tr>
        </table>
    </div>
    </form>
  </div>
</body>
</html>
