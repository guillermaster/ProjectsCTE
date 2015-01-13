<%@ Control Language="C#" AutoEventWireup="true" CodeFile="sesionuser.ascx.cs" Inherits="controls_logout" %>
<asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/img/cerrar-sesion.gif" ImageAlign="Right" OnClick="ImageButton1_Click" CausesValidation="False" />
<asp:ImageButton ID="btnChangePwd" runat="server" ImageAlign="Right" ImageUrl="~/img/cambiar-contrasena.gif" CausesValidation="False" PostBackUrl="~/ChangePassword.aspx" />
