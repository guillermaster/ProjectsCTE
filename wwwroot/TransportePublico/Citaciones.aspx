<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Citaciones.aspx.cs" Inherits="Citaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table cellpadding="5" cellspacing="10">
        <tr>
            <td><asp:Label ID="lblGremio" runat="server" Text="Gremio:" SkinID="etiqueta" /></td>
            <td><asp:DropDownList ID="ddlGremios" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td><asp:Label ID="lblCooperativa" runat="server" Text="Cooperativa:" SkinID="etiqueta" /></td>
            <td><asp:DropDownList ID="ddlCooperativas" runat="server"></asp:DropDownList></td>
        </tr>
    </table>
    <asp:GridView ID="gvCitaciones" runat="server">
    </asp:GridView>
</asp:Content>

