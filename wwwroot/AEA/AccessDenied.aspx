<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AccessDenied.aspx.cs" Inherits="AccessDenied" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-content1" align="center">
        <DIV id="divError"><TABLE class="error2" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/error.gif" /></TD><TD><asp:Label id="lblMsgError" runat="server" Font-Size="Small" Text="Usted no tiene privilegios suficientes para acceder a la página solicitada." Font-Bold="True"></asp:Label></TD></TR></TBODY></TABLE></DIV>
    </div>
</asp:Content>

