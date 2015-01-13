<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SaveButton.ascx.cs" Inherits="UserControls_SaveButton" %>
<asp:ImageButton ID="btnSave" ImageUrl="~/img/btnGuardar.gif" runat="server" OnClick="btnSave_Click" />
<asp:HiddenField ID="hdnTipoGuardar" runat="server" />
