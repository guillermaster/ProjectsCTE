<%@ Page Title="" Language="C#" MasterPageFile="~/MainV2.master" AutoEventWireup="true"
    CodeFile="SolTramitesNoPagadas.aspx.cs" Inherits="Consultas_Tramites_SolTramitesNoPagadas" %>

<%@ Register Src="../../UserControls/LinkTramitesTracking.ascx" TagName="LinkTramitesTracking"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/LinkTramitesFinalizados.ascx" TagName="LinkTramitesFinalizados"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/LinkCitacionesPagadasBanca.ascx" TagName="LinkCitacionesPagadasBanca"
    TagPrefix="uc1" %>
<%@ Register Src="../../UserControls/LinkSolTramitesNoPag.ascx" TagName="LinkSolTramitesNoPag"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightColumnContent" runat="Server">
    <div class="full">
        <h2>
            Transacciones en línea</h2>
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
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divMain" runat="server">
        <asp:Panel runat="server" ID="pnlContent" class="full">
            <h2>
                Solicitudes de Trámites no pagadas</h2>
            <asp:GridView ID="gvCepNoPagados" runat="server" AutoGenerateColumns="false" Width="100%"
                AllowPaging="true" OnPageIndexChanging="GridViewPageIndexChanging">
                <Columns>
                    <asp:BoundField DataField="cep" HeaderText="CEP" />
                    <asp:BoundField DataField="proceso" HeaderText="Trámite" />
                    <asp:BoundField DataField="fecha_ingreso" HeaderText="Fecha de creación" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>
</asp:Content>
