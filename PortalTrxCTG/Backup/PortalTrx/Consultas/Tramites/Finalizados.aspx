<%@ Page Title="" Language="C#" MasterPageFile="~/MainV2.master" AutoEventWireup="true"
    CodeFile="Finalizados.aspx.cs" Inherits="Consultas_Tramites_Finalizados" %>

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
                Trámites Finalizados</h2>
            <asp:GridView ID="gvTrackTramites" runat="server" AutoGenerateColumns="False" Width="100%"
                OnSelectedIndexChanged="gvTrackTramites_SelectedIndexChanged" AllowPaging="true"
                OnPageIndexChanging="GridViewPageIndexChanging">
                <Columns>
                    <asp:CommandField CausesValidation="False" HeaderText="Detalles" SelectText="Ver"
                        ShowSelectButton="True">
                        <ItemStyle Font-Size="X-Small" />
                    </asp:CommandField>
                    <asp:BoundField DataField="cep" HeaderText="Código de Pago (CEP)" />
                    <asp:BoundField DataField="creado" HeaderText="Fecha de creación" />
                    <asp:BoundField DataField="nombre_tramite" HeaderText="Trámite" />
                </Columns>
            </asp:GridView>
            <asp:Button ID="Button1" runat="server" Style="display: none" />
            <asp:Panel ID="pnlDetails" runat="server" Style="display: none; padding: 10px; text-align: center;"
                BackColor="AntiqueWhite" ScrollBars="None">
                <asp:DetailsView ID="dvDetTramite" runat="server" />
                <asp:LinkButton ID="btnClosePopup" runat="server">Cerrar</asp:LinkButton>
            </asp:Panel>
            <asp:ModalPopupExtender ID="MPE" runat="server" TargetControlID="Button1" PopupControlID="pnlDetails"
                BackgroundCssClass="modalBackground" DropShadow="false" CancelControlID="btnClosePopup" />
        </asp:Panel>
    </div>
</asp:Content>
