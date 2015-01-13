<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ MasterType VirtualPath="~/Site.master" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="/js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="js/alertbox.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="js/style.css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Regularizar Pagos en Web Transaccional CTG
    </h2>
    <p>
        <a href="ConciliaPago.aspx">Conciliar Pago de Citaciones</a>
    </p>
    <p>
        <a href="ConciliaReverso.aspx">Conciliar Reverso de Citaciones</a>
    </p>
    <p>
        <a href="ConciliaReversoCNTTTSV.aspx">Conciliar Reverso de Citaciones CNTTTSV</a>
    </p>
    <p>
        <asp:LinkButton ID="btnReversoAXIS" runat="server" 
            onclick="btnReversoAXIS_Click">Conciliar reversos en AXIS</asp:LinkButton>
    </p>
    <p>
        <a href="ConciliaPagoTramite.aspx">Conciliar Pago de Trámite</a>
    </p>
    <p>
        <a href="ConciliaFechaPagoTramite.aspx">Conciliar Fecha de Pago de Trámite</a>
    </p>
    <p>
        <asp:HyperLink ID="linkPagosEncCitac" runat="server">Consulta Pagos de Citaciones Encolados</asp:HyperLink>
    </p>
    <p>
        <asp:HyperLink ID="linkPagosEncTram" runat="server">Consulta Pagos de Trámites Encolados</asp:HyperLink>
    </p>
</asp:Content>
