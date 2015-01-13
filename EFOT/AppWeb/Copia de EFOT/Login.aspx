<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

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