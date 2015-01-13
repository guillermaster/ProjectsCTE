<%@ Page Title="" Language="C#" MasterPageFile="~/MainV2.master" AutoEventWireup="true"
    CodeFile="Tracking.aspx.cs" Inherits="Consultas_Tramites_Tracking" %>

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
            <h2>
                Trámites en Ejecución</h2>
            <asp:GridView ID="gvTrackTramites" runat="server" AutoGenerateColumns="False" Width="100%"
                AllowPaging="true" OnPageIndexChanging="GridViewPageIndexChanging" OnSelectedIndexChanged="gvTrackTramites_SelectedIndexChanged">
                <Columns>
                    <asp:CommandField CausesValidation="False" HeaderText="Detalles" SelectText="Ver"
                        ShowSelectButton="True">
                        <ItemStyle Font-Size="X-Small" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnIdTramite" runat="server" Value='<%# Eval("id_tramite") %>' />
                            <asp:HiddenField ID="hdnEstadoCEP" runat="server" Value='<%# Eval("pagada") %>' />
                            <asp:HiddenField ID="hdnValor" runat="server" Value='<%# Eval("valor") %>' />
                            <asp:HiddenField ID="hdnFechaSolicitud" runat="server" Value='<%# Eval("fecha_ingreso") %>' />
                            <asp:HiddenField ID="hdnFechaPagoReverso" runat="server" Value='<%# Eval("fecha_pago_reverso") %>' />
                            <asp:HiddenField ID="hdnCanalPago" runat="server" Value='<%# Eval("canal") %>' />
                            <asp:HiddenField ID="hdnFechaEjecTramite" runat="server" Value='<%# Eval("fecha_ejecucion") %>' />
                            <asp:HiddenField ID="hdnEstadoTramite" runat="server" Value='<%# Eval("terminada") %>' />
                            <asp:HiddenField ID="hdnEntregaTramite" runat="server" Value='<%# Eval("entrega") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="cep" HeaderText="CEP" />
                    <asp:BoundField DataField="nombre_tramite" HeaderText="Nombre de Trámite" />
                    <asp:BoundField DataField="pagada" HeaderText="Estado" />
                </Columns>
            </asp:GridView>
            <!--<asp:BoundField DataField="nombre_tramite" HeaderText="Trámite" />   agregar esta columna al gridview-->
            <asp:Button ID="Button1" runat="server" Style="display: none" />
            <asp:Panel ID="pnlDetails" runat="server" Style="display: none; padding: 10px; text-align: center;"
                BackColor="AntiqueWhite" ScrollBars="None">
                <asp:DetailsView ID="dvDetTramite" runat="server" />
                <h2>
                    Proceso de trámite</h2>
                <asp:GridView ID="gvEtapasTramite" runat="server" AutoGenerateColumns="false" Width="97%"
                    Style="margin: 0 0 5px 10px">
                    <Columns>
                        <asp:BoundField DataField="paso" HeaderText="Paso" />
                        <asp:BoundField DataField="descripcion" HeaderText="Etapa" />
                        <asp:BoundField DataField="estado" HeaderText="Estado" />
                        <asp:BoundField DataField="fecha_inicio" HeaderText="Fecha de ejecución" />
                    </Columns>
                </asp:GridView>
                <asp:LinkButton ID="btnClosePopup" runat="server">Cerrar</asp:LinkButton>
            </asp:Panel>
            <asp:ModalPopupExtender ID="MPE" runat="server" TargetControlID="Button1" PopupControlID="pnlDetails"
                BackgroundCssClass="modalBackground" DropShadow="false" CancelControlID="btnClosePopup" />
        </asp:Panel>
    </div>
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="RightColumnContent">
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
