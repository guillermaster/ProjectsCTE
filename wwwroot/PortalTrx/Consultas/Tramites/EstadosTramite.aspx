<%@ Page Title="" Language="C#" MasterPageFile="~/MainV2.master" AutoEventWireup="true"
    CodeFile="EstadosTramite.aspx.cs" Inherits="Consultas_Tramites_Tracking" %>

<%@ Register Src="../../UserControls/LinkTramitesTracking.ascx" TagName="LinkTramitesTracking"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/LinkTramitesFinalizados.ascx" TagName="LinkTramitesFinalizados"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/LinkCitacionesPagadasBanca.ascx" TagName="LinkCitacionesPagadasBanca"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/LinkSolTramitesNoPag.ascx" TagName="LinkSolTramitesNoPag"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../../js/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../../js/alertbox.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../../js/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divMain" runat="server">
        <asp:Panel runat="server" ID="pnlContent" class="full">
            <h2>Consulta de Estado de Trámite</h2>
            <asp:Panel ID="pnlNumCedula" runat="server" style="margin-bottom: 15px">
                <span style="margin: 0 5px 0 0;">Número de Trámite: </span>
                <asp:TextBox ID="txtNumTramite" runat="server" CausesValidation="True" />
                <asp:RequiredFieldValidator ID="reqFValNumTram" runat="server" ErrorMessage="*" ControlToValidate="txtNumTramite" />
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
            </asp:Panel>
            <asp:GridView ID="gvEtapasTramite" runat="server" AutoGenerateColumns="false" Width="97%"
                Style="margin: 0 0 5px 10px">
                <Columns>
                    <asp:BoundField DataField="paso" HeaderText="Paso" />
                                <asp:BoundField DataField="descripcion" HeaderText="Etapa" />
                                <asp:BoundField DataField="estado" HeaderText="Estado" />
                                <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha de ejecución" />
                                <asp:BoundField DataField="id_usuario" HeaderText="Usuario" />
                                <asp:BoundField DataField="lugar" HeaderText="Lugar" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="RightColumnContent">
    <div class="full">
        <h2>Transacciones en línea</h2>
        <div class="left" style="text-align: center">
            <uc1:LinkTramitesTracking ID="LinkTramitesTracking1" runat="server" />
        </div>
        <div class="right" style="text-align: center">
            <uc1:LinkTramitesFinalizados ID="LinkTramitesFinalizados1" runat="server" />
        </div>
        <div class="left" style="text-align: center">
            <uc1:LinkSolTramitesNoPag ID="LinkSolTramitesNoPag1" runat="server" />
        </div>
        <div class="right" style="text-align: center">
            <uc1:LinkCitacionesPagadasBanca ID="LinkCitacionesPagadasBanca1" runat="server" />
        </div>
    </div>
</asp:Content>
