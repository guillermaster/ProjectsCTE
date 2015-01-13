<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Logout.ascx.cs" Inherits="userControls_Logout" %>
<asp:ImageButton ID="imgBtnClose" runat="server" AlternateText="Cerrar sesión" 
    ImageAlign="Right" ImageUrl="~/img/cerrar-sesion.gif" 
    ToolTip="Cerrar sesión" CausesValidation="False" onclick="imgBtnClose_Click" 
    PostBackUrl="~/Default.aspx" />