<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ConciliaPagoTramite.aspx.cs" Inherits="ConciliaPago" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="/js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="js/alertbox.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="js/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Conciliar Pagos de Trámites
    </h2>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="CEP"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCEP" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" Text="OK" Width="60px" 
                    onclick="Button1_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
