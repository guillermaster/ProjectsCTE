<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_text.css" />
    <script type="text/javascript" src="js/common.js"></script>
</head>
<body>
    <form id="formLogin" runat="server">
    <div>
        <asp:Login ID="Login1" runat="server" CssClass="loginform" FailureText="Login fallido. Por favor, intente nuevamente." LoginButtonText="Iniciar Sesión" PasswordLabelText="Contraseña:" RememberMeText="Guardar contraseña" TitleText="" UserNameLabelText="Usuario:" Width="266px" BorderStyle="None" PasswordRequiredErrorMessage="Debe ingresar la contraseña." UserNameRequiredErrorMessage="Debe ingresar el nombre de usuario." DisplayRememberMe="False" OnAuthenticate="Login1_Authenticate">
            <CheckBoxStyle CssClass="checkbox" />
            <LoginButtonStyle CssClass="button" Width="90px" />
        </asp:Login>
    </div>
    </form>
</body>
</html>
