<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Logon" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
        <h1>Comisión de Tránsito del Ecuador</h1>
    </div>
    <div align="center" style="margin: 60px">
        <asp:Login ID="Login1" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" 
            BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" 
            DisplayRememberMe="False" Font-Names="Verdana" Font-Size="0.8em" 
            ForeColor="#333333" LoginButtonText="Ingresar" PasswordLabelText="Contraseña:" 
            PasswordRequiredErrorMessage="Debe ingresar su contraseña" 
            TitleText="Ingrese sus datos de acceso" UserNameLabelText="Usuario:" 
            UserNameRequiredErrorMessage="Debe ingresar su nombre de usuario" 
            Width="241px" onauthenticate="Login1_Authenticate">
            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
            <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
                BorderWidth="1px" Font-Names="Verdana" Font-Size="1.0em" ForeColor="#284775" />
            <TextBoxStyle Font-Size="0.8em" />
            <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="1.1em" 
                ForeColor="White" />
        </asp:Login>
    </div>
    </form>
</body>
</html>
