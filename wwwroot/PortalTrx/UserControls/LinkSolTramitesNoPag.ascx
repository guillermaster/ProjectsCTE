<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LinkSolTramitesNoPag.ascx.cs" Inherits="UserControls_LinkSolTramitesNoPag" %>
<div align="center" style="padding: 0 0 10px 0">
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Consultas/Tramites/SolTramitesNoPagadas.aspx">
        <asp:Image ID="Image1" ImageUrl="~/images/icoConsSolTramitesNoPaid.png" runat="server" />
        <br />
        Solicitudes de trámites no pagadas
    </asp:HyperLink>
</div>