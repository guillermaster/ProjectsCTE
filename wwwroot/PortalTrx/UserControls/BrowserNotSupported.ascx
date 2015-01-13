<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BrowserNotSupported.ascx.cs" Inherits="UserControls_BrowserNotSupported" %>
<div class="full" style="background-color: #efb4b4;">
        El navegador web que usted tiene es obsoleto, para acceder a nuestro portal transaccional es altamente recomendable utilizar alguno de los siguientes navegadores:
        <div style="text-align:center; margin: 30px;">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/chrome.png" ToolTip="Google Chrome 2.0 o superior" />
            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/firefox.png" ToolTip="Mozilla Firefox 1.5 o superior" />
            <asp:Image ID="Image3" runat="server" ImageUrl="~/images/internet_explorer.png" ToolTip="Internet Explorer 7.0 o superior" />
            <asp:Image ID="Image4" runat="server" ImageUrl="~/images/opera.png" ToolTip="Opera 9.0 o superior" />
            <asp:Image ID="Image5" runat="server" ImageUrl="~/images/safari.png" ToolTip="Safari 3.0 o superior" />
        </div>
    </div>