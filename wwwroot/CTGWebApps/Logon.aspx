<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Login" ValidateRequest="false" EnableEventValidation="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_text.css" />
    <script type="text/javascript" src="js/common.js"></script>
    <style type="text/css">
        .left{
            float: left;
            left: 0.00%;
            width: 50.00%;
            vertical-align:top;
        }
        .right{
            float: right;
            right: 0.00%;
            width: 50.00%;
            vertical-align:top;
        }
    </style>
</head>
<body>
    <form id="formLogin" runat="server">
    <div>
      <div class="left">
        <asp:Login ID="Login1" runat="server" CssClass="loginform" FailureText="Login fallido. Por favor, intente nuevamente." LoginButtonText="Iniciar Sesión" PasswordLabelText="Contraseña:" PasswordRecoveryUrl="~/LoginExtra/LostPassword.aspx" RememberMeText="Guardar contraseña" TitleText="" UserNameLabelText="Cédula/Pasaporte/RUC:" Width="266px" BorderStyle="None" PasswordRequiredErrorMessage="Debe ingresar la contraseña." UserNameRequiredErrorMessage="Debe ingresar el nombre de usuario." DisplayRememberMe="False" OnAuthenticate="Login1_Authenticate">
            <CheckBoxStyle CssClass="checkbox" />
            <LoginButtonStyle CssClass="button" Width="90px" />
            <FailureTextStyle Font-Bold="True" />
        </asp:Login>
        <table width="135" border="0" cellpadding="2" cellspacing="0" title="Clic para verificar - Este sitio ha escogido VeriSign SSL para comercio electrónico seguro y comunicación confidencial.">
         <tr><td><br /><br /></td></tr>
        <tr>
        <td width="135" align="center" valign="top"></td>
        </tr>
    </table>
      </div>
      <div class="right">
        <table>
          <tr>
            <td valign="top"><img src="img/password.gif" /></td>
            <td valign="top">
                <h3>¿No posee contraseña de acceso o no puede acceder?</h3>
                <ul style="margin-left:15px;list-style-image:url('img/bullet.gif');">
                  <li><a href="LoginExtra/UserRegistartion.aspx">Registre una nueva cuenta</a></li>
                  <li><a href="LoginExtra/LostPassword.aspx">Recuerde su contraseña</a></li>
                  <li><a href="LoginExtra/ResendActivationEmail.aspx">Solicite el reenvío e-mail de activación de cuenta</a></li>
                </ul>
            </td>
          </tr>
        </table>
      </div>
    </div>
    <div>
    <script src=https://seal.verisign.com/getseal?host_name=secure.ctg.gov.ec&size=S&use_flash=YES&use_transparent=YES&lang=es></script>
    </div>
    </form>
</body>
</html>
