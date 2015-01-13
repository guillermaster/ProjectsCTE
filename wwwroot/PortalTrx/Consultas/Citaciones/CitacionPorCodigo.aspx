<%@ Page Title="" Language="C#" MasterPageFile="~/OneColumnV2.master" AutoEventWireup="true"
    CodeFile="CitacionPorCodigo.aspx.cs" Inherits="Consultas_Citaciones_CitacionPorCodigo" %>
<%@ MasterType VirtualPath="~/OneColumnV2.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../../js/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../../js/alertbox.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../../js/style.css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="title">
            Citación por Código
        </div>
    <div class="full" id="divContent" runat="server" style="float: left; width:100%;">
        <div>
            <span style="margin: 0 5px 0 0;">Código de citación: </span>
            <asp:TextBox ID="txtCodCitacion" runat="server" CausesValidation="True" />
            <asp:RequiredFieldValidator ID="reqFValCodCitac" runat="server" ErrorMessage="*" ControlToValidate="txtCodCitacion" />
            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
        </div>
        <div>
            <asp:RegularExpressionValidator ID="regExpValCodCitac" runat="server" 
                ErrorMessage="El código de citación solo debe incluir caracteres numéricos" 
                ControlToValidate="txtCodCitacion" ValidationExpression="\d+"></asp:RegularExpressionValidator>
        </div>
        <asp:DetailsView ID="detViewDetCitacion" Width="640" runat="server" />
    </div>
</asp:Content>
