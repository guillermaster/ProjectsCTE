<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewButton.ascx.cs" Inherits="UserControls_NewButton" %>
<asp:ImageButton ID="btnNew" runat="server" AlternateText="Nuevo" CausesValidation="False"
    ImageUrl="~/img/btnNuevo.gif" OnClick="btnNew_Click" />
<asp:HiddenField ID="hdnTargetURL" runat="server" />