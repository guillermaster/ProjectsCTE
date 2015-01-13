<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LinkTramitesTracking.ascx.cs" Inherits="UserControls_LinkTramitesTracking" %>
<div align="center">
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Consultas/Tramites/Tracking.aspx">
        <asp:Image ID="Image1" ImageUrl="~/images/icoConsSegTramite.png" runat="server" />
        <br />
        Tramites<br />en ejecución
    </asp:HyperLink>
</div>