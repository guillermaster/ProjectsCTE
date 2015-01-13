<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>


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