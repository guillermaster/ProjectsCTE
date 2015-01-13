<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ConciliaFechaPagoTramite.aspx.cs" Inherits="ConciliaPago" %>
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
        Conciliar Fecha en Pagos de Trámites
    </h2>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="CEP"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCEP" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="reqValCEP" ControlToValidate="txtCEP" runat="server" ErrorMessage="Debe ingresar el CEP" />
            </td>
        </tr>
         <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Fecha"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFecha">
                </asp:CalendarExtender>
                <asp:RequiredFieldValidator ID="reqValFecha" ControlToValidate="txtFecha" runat="server" ErrorMessage="Debe ingresar la fecha" />
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
