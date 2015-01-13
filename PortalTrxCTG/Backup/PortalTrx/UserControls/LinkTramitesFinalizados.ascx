<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LinkTramitesFinalizados.ascx.cs"
    Inherits="UserControls_LinkTramitesFinalizados" %>
<div align="center">
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Consultas/Tramites/Finalizados.aspx">
        <asp:Image ID="Image1" ImageUrl="~/images/icoConsSegTramiteFin.png" runat="server" />
        <br />
        Trámites<br />finalizados
    </asp:HyperLink>
</div>
