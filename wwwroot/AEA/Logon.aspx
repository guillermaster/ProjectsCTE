<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Logon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="css/layout4_text.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="main-content1" align="center">
    <asp:Login ID="Login1" runat="server" CssClass="loginform" FailureText="Inicio de sesión fallido. Por favor, intente nuevamente." LoginButtonText="Ingresar" PasswordLabelText="Contraseña:" RememberMeText="Guardar contraseña" TitleText="Inicio de sesión" UserNameLabelText="Usuario:" Width="266px" BorderStyle="None" PasswordRequiredErrorMessage="Debe ingresar la contraseña." UserNameRequiredErrorMessage="Debe ingresar el nombre de usuario." DisplayRememberMe="False" OnAuthenticate="Login1_Authenticate">
            <LoginButtonStyle CssClass="button" />
            <FailureTextStyle Font-Bold="True" ForeColor="Maroon" />
            <TitleTextStyle CssClass="h1" />
        <TextBoxStyle Width="150px" BackColor="White" />
        <LabelStyle HorizontalAlign="Left" />
        </asp:Login>
    </div>
    </form>
</body>
</html>
