<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Inicio
    </h2>
    <ul>
        <li style="margin: 0 0 20px 0;"><a href="AspirantesRegistrados.aspx">Consultar aspirantes registrados</a></li>
        <li>
            <asp:HyperLink ID="lnkReporte" NavigateUrl="~/RepActInstructores.aspx" runat="server">Reporte de actividades por instructor</asp:HyperLink></li>
    </ul>
</asp:Content>
