<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserLoggedIn.ascx.cs" Inherits="userControls_UserLoggedIn" %>
<asp:Image ID="imgUser" runat="server" ImageUrl="~/img/ico_user.gif" />&nbsp;
<asp:HyperLink ID="hypUsuario" NavigateUrl="~/UserInfo.aspx" runat="server" ForeColor="White"></asp:HyperLink>
<br /> 
<asp:Label ID="lblEmpresa" runat="server" Text="" />