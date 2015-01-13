<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Logon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas - Transporte Público</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="css/layout4_text.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Login ID="Login1" runat="server" 
            FailureText="Su intento fue fallido. Por favor intente nuevamente." 
            LoginButtonText="Iniciar sesión" PasswordLabelText="Contraseña:" 
            PasswordRequiredErrorMessage="Debe ingresar la contraseña" 
            RememberMeText="Remember me" TitleText="Inicio de Sesión" 
            UserNameLabelText="Usuario:" 
            UserNameRequiredErrorMessage="Debe ingresar el nombre de usuario" 
            CssClass="loginform"
            DisplayRememberMe="false" onauthenticate="Login1_Authenticate">
            <TextBoxStyle BackColor="White" Width="150px" />
            <LoginButtonStyle CssClass="button" />
            <TitleTextStyle CssClass="h1" />
        </asp:Login>
        <br />
        <table id="tblChangePwd" runat="server" style="width: 463px" visible="false">
            <tr>
                <td class="error" colspan="3">
                    Esta aplicación es de uso exclusivo para personal de la Comisión de Tránsito de 
                    la provincia del Guayas.
                    <br />
                    <br />
                    <b>Esta es la primera vez que va a ingresar a la aplicación, por lo cual deberá 
                    cambiar la contraseña asignada por una que usted desee.</b></td>
            </tr>
            <tr>
                <td style="width: 145px">
                    <asp:Label ID="lblContrasena" runat="server" CssClass="blackTitle2">Contraseña 
                    Actual:</asp:Label>
                </td>
                <td style="width: 162px">
                    <asp:TextBox ID="txtCurrPwd" runat="server" CssClass="blackTitle2" 
                        MaxLength="12" TextMode="Password" BackColor="White" />
                </td>
                <td style="width: 31px">
                    <asp:RequiredFieldValidator ID="reqFieldVal1" runat="server" 
                        ControlToValidate="txtCurrPwd" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:HiddenField ID="hdnUsername" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 145px">
                    <asp:Label ID="lblNuevaContrasena" runat="server" CssClass="blackTitle2">Nueva 
                    contraseña:</asp:Label>
                </td>
                <td style="width: 162px">
                    <asp:TextBox ID="txtNewPwd" runat="server" CssClass="blackTitle2" 
                        MaxLength="12" TextMode="Password" BackColor="White" />
                </td>
                <td style="width: 31px">
                    <asp:RequiredFieldValidator ID="reqFieldVal2" runat="server" 
                        ControlToValidate="txtNewPwd" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <asp:HiddenField ID="hdnCurrPassword" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 145px; height: 40px;">
                    <asp:Label ID="lblConfContrasena" runat="server" CssClass="blackTitle2">Confirme 
                    contraseña:</asp:Label>
                </td>
                <td style="width: 162px; height: 40px;">
                    <asp:TextBox ID="txtNewPwdConf" runat="server" CssClass="blackTitle2" 
                        MaxLength="12" TextMode="Password" BackColor="White" />
                </td>
                <td style="width: 31px; height: 40px;">
                    <asp:RequiredFieldValidator ID="reqFieldVal3" runat="server" 
                        ControlToValidate="txtNewPwdConf" Display="Dynamic" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="right" colspan="3">
                    <asp:Button ID="btnModificar" runat="server" CssClass="button" 
                        OnClick="btnModificar_Click" Text="Modificar Contraseña" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtNewPwd" CssClass="blackTitle2" Display="Dynamic" 
                        ErrorMessage="La nueva contraseña debe ser de al menos 8 caracteres de longitud, y tener al menos 1 caracter numérico" 
                        ValidationExpression="(?=.{8,})[a-z]+[^a-z]+|[^a-z]+[a-z]+"></asp:RegularExpressionValidator>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                        ControlToCompare="txtNewPwd" ControlToValidate="txtNewPwdConf" 
                        CssClass="blackTitle2" Display="Dynamic" 
                        ErrorMessage="La confirmación de la nueva contraseña no coincide"></asp:CompareValidator>
                    <br />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                        ControlToCompare="txtNewPwd" ControlToValidate="txtCurrPwd" 
                        CssClass="blackTitle2" Display="Dynamic" 
                        ErrorMessage="La nueva contraseña debe ser distinta a la actual" 
                        Operator="NotEqual"></asp:CompareValidator>
                    <asp:Label ID="lblError" runat="server" CssClass="error" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
