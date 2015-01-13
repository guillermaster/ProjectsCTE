<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ConciliaReversoCNTTTSV.aspx.cs" Inherits="ReversarFact" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="/js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="js/alertbox.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="js/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Conciliar reversos de CEPs (CNTTTSV)
    </h2>
    <div style="margin-bottom: 5px">
        <asp:Label ID="Label1" runat="server" Text="CEP"></asp:Label>
        <asp:TextBox ID="txtCEP" runat="server"></asp:TextBox>
    </div>
    <asp:Button ID="Button1" runat="server" Text="OK" Width="64" 
    onclick="Button1_Click" />
</asp:Content>
