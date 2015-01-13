<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login"  ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register TagPrefix="UserControls" TagName="header" Src="controls/header.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="footer" Src="controls/footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CTG - CNTTTSV</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/StyleSheet.css" />
    <link href="../css/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <UserControls:header id="Header" runat="server" />
    </div>
    <div>
    <div align="center">
        <h3>Consultas de datos de Tránsito</h3>
        <asp:Login ID="Login1" runat="server" CssClass="loginform" FailureText="Login fallido. Por favor, intente nuevamente." LoginButtonText="Iniciar Sesión" PasswordLabelText="Contraseña:" RememberMeText="Guardar contraseña" TitleText="Ingrese su usuario y contraseña" UserNameLabelText="Usuario:" Width="266px" BorderStyle="None" PasswordRequiredErrorMessage="Debe ingresar la contraseña." UserNameRequiredErrorMessage="Debe ingresar el nombre de usuario." DisplayRememberMe="False" OnAuthenticate="Login1_Authenticate">
            <CheckBoxStyle CssClass="checkbox" />
            <LoginButtonStyle CssClass="button" />
            <FailureTextStyle Font-Bold="True" ForeColor="Maroon" />
            <TitleTextStyle CssClass="titleLogin" />
        </asp:Login>
        <table style="width: 463px" id="tblChangePwd" runat="server">
            <tr><td colspan="3" class="error">Esta aplicación es de uso exclusivo para personal 
                de la Comisión de Tránsito del Ecuador. <br /><br /> <b>Esta es la primera vez que va a ingresar a la aplicación, por lo cual deberá cambiar la contraseña asignada por una que usted desee.</b></td></tr>
            <tr><td style="width: 145px"><asp:label ID="lblContrasena" runat="server" CssClass="blackTitle2">Contrase&ntilde;a Actual:</asp:label></td><td style="width: 162px"><asp:TextBox runat="server" ID="txtCurrPwd" MaxLength="12" TextMode="Password" CssClass="blackTitle2" /></td><td style="width: 31px">
                <asp:RequiredFieldValidator ID="reqFieldVal1" runat="server" ControlToValidate="txtCurrPwd"
                    ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator>
                <asp:HiddenField ID="hdUsername" runat="server" />
            </td></tr>
            <tr><td style="width: 145px"><asp:label ID="lblNuevaContrasena" runat="server" CssClass="blackTitle2">Nueva contrase&ntilde;a:</asp:label></td><td style="width: 162px"><asp:TextBox runat="server" ID="txtNewPwd" MaxLength="12" TextMode="Password" CssClass="blackTitle2" /></td><td style="width: 31px">
                <asp:RequiredFieldValidator ID="reqFieldVal2" runat="server" ControlToValidate="txtNewPwd"
                    ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator></td></tr>
            <tr><td style="width: 145px; height: 40px;"><asp:label ID="lblConfContrasena" runat="server" CssClass="blackTitle2">Confirme contrase&ntilde;a:</asp:label></td><td style="width: 162px; height: 40px;"><asp:TextBox runat="server" ID="txtNewPwdConf" MaxLength="12" TextMode="Password" CssClass="blackTitle2" /></td><td style="width: 31px; height: 40px;">
                <asp:RequiredFieldValidator ID="reqFieldVal3" runat="server" ControlToValidate="txtNewPwdConf"
                    ErrorMessage="*" Display="Dynamic"></asp:RequiredFieldValidator></td></tr>
            <tr><td colspan="3" align="right">
             <asp:Button ID="btnModificar" runat="server" Text="Modificar Contraseña" OnClick="btnModificar_Click" CssClass="button" /></td></tr>
            <tr><td colspan="3">
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtNewPwd"
                    ErrorMessage="La nueva contraseña debe ser de al menos 8 caracteres de longitud, y tener al menos 1 caracter numérico"
                    ValidationExpression="(?=.{8,})[a-z]+[^a-z]+|[^a-z]+[a-z]+" Display="Dynamic" CssClass="blackTitle2"></asp:RegularExpressionValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtNewPwd"
                    ControlToValidate="txtNewPwdConf" ErrorMessage="La confirmación de la nueva contraseña no coincide" Display="Dynamic" CssClass="blackTitle2"></asp:CompareValidator><br />
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtNewPwd"
                    ControlToValidate="txtCurrPwd" CssClass="blackTitle2" Display="Dynamic" ErrorMessage="La nueva contraseña debe ser distinta a la actual"
                    Operator="NotEqual"></asp:CompareValidator>
                <asp:Label ID="lblError" runat="server" CssClass="error"></asp:Label></td></tr>
        </table>
    </div>
    <table align="center" width="135" border="0" cellpadding="2" cellspacing="0" title="Clic para verificar - Este sitio ha escogido VeriSign SSL para comercio electrónico seguro y comunicación confidencial.">
         <tr><td><br /><br /></td></tr>
        <tr>
        <td width="135" align="center" valign="top"><script src=https://seal.verisign.com/getseal?host_name=secure.ctg.gov.ec&size=S&use_flash=YES&use_transparent=YES&lang=es></script></td>
        </tr>
    </table>
    </div>
    <div>
        <UserControls:footer ID="Footer" runat="server" />
    </div> 
    </form>
</body>
</html>
