<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BackButton.ascx.cs" Inherits="UserControls_BackButton" %>
<asp:ImageButton ID="btnBack" runat="server" AlternateText="Regresar" CausesValidation="False"
    ImageUrl="~/img/btnBack.gif" OnClick="btnBack_Click" ToolTip="Regresar a la página anterior" />
<asp:HiddenField ID="hdnTargetURL" runat="server" />
