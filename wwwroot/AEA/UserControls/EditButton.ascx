<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditButton.ascx.cs" Inherits="UserControls_EditButton" %>
<asp:ImageButton ID="btnEdit" runat="server" AlternateText="Modificar" CausesValidation="True"
    ImageUrl="~/img/btnModificar.gif" OnClick="btnEdit_Click" />
<asp:HiddenField ID="hdnTargetURL" runat="server" />