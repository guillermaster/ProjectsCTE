<%@ page language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Account_Login, App_Web_login.aspx.cdcab7d2" theme="efot" %>


<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Aspirantes a ingresar a la E.F.O.T.
    </h2>
    <p>
        Por favor ingrese su usuario (número de cédula) y contraseña.
        <a href="Registro.aspx">Registrese como aspirante</a> si usted aun no lo ha hecho.
    </p>
    <asp:Login ID="Login1" runat="server" DisplayRememberMe="False" 
        onauthenticate="Login1_Authenticate" />
</asp:Content>