<%@ page title="Log In" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Account_Login, App_Web_login.aspx.dae9cef9" theme="efot" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Control de Acceso
    </h2>
    <p>
        Por favor ingrese su nombre de usuario y contraseña.
    </p>
    <asp:Login ID="Login1" runat="server" DisplayRememberMe="False" 
        onauthenticate="Login1_Authenticate" />
</asp:Content>