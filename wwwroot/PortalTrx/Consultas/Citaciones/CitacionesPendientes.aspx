<%@ Page Title="" Language="C#" MasterPageFile="~/OneColumnV2.master" AutoEventWireup="true"
    CodeFile="CitacionesPendientes.aspx.cs" Inherits="Consultas_Citaciones_CitacionesPendientes" %>
<%@ MasterType VirtualPath="~/OneColumnV2.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../../js/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../../js/alertbox.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../../js/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divMain" runat="server">
        <div class="title">
            Citaciones Pendientes
        </div>

        <asp:Button ID="Button1" runat="server" Style="display: none" />
        <asp:Panel runat="server" CssClass="full" ID="pnlContent">
            <asp:Panel ID="pnlNumCedula" Visible="false" runat="server">
                <span style="margin: 0 5px 0 0;">Número de Licencia: </span>
                <asp:TextBox ID="txtNumLicencia" runat="server" CausesValidation="True" />
                <asp:RequiredFieldValidator ID="reqFValNumLic" runat="server" ErrorMessage="*" ControlToValidate="txtNumLicencia" />
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" 
                    onclick="btnConsultar_Click" />
            </asp:Panel>
            <asp:GridView ID="gvCitacPend" runat="server" OnSelectedIndexChanged="gvCitacPend_SelectedIndexChanged"
                OnRowDataBound="gvCitacPend_RowDataBound" AutoGenerateColumns="False" ShowFooter="True">
                <Columns>
                    <asp:CommandField CausesValidation="False" HeaderText="Detalles" SelectText="Ver"
                        ShowSelectButton="True">
                        <ItemStyle Font-Size="X-Small" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnContravencion" runat="server" Value='<%# Eval("contravencion") %>' />
                            <asp:HiddenField ID="hdnArticulo" runat="server" Value='<%# Eval("articulo") %>' />
                            <asp:HiddenField ID="hdnPuntos" runat="server" Value='<%# Eval("puntos_perdidos") %>' />
                            <asp:HiddenField ID="hdnNumCitacion" runat="server" Value='<%# Eval("num_citacion") %>' />
                            <asp:HiddenField ID="hdnFechaCitacion" runat="server" Value='<%# Eval("fecha_citacion") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="num_citacion" HeaderText="Código de citación" />
                    <asp:BoundField DataField="tipo_citacion" HeaderText="Tipo" />
                    <asp:BoundField DataField="licencia_infractor" HeaderText="Licencia del infractor" />
                    <asp:BoundField DataField="placa_vehiculo" HeaderText="Placa de vehículo" />
                    <asp:BoundField DataField="fecha_citacion" HeaderText="Fecha de citación" />
                    <asp:BoundField DataField="valor_citacion" HeaderText="Valor de citación ($)" />
                    <asp:BoundField DataField="multa_citacion" HeaderText="Multa ($)" />
                    <asp:BoundField DataField="total_pagar" HeaderText="Total a pagar ($)" />
                </Columns>
            </asp:GridView>
            <asp:Panel ID="pnlCitac" runat="server" Style="display: none; padding: 10px; text-align: center;"
                BackColor="AntiqueWhite" ScrollBars="None">
                <div>
                    <asp:DetailsView ID="dvCitacPend" runat="server" />
                    <asp:Image ID="imgCitacion" Visible="false" runat="server" Width="614" Height="539" /> 
                </div>
                <div style="margin: 10px 0 20px;">
                    <asp:ImageButton ID="btnVerCitacion" ImageUrl="~/images/btnPhotoCitac.png" runat="server" onclick="btnVerCitacion_Click" />
                    <asp:ImageButton ID="btnVolverDetCitac" ImageUrl="~/images/btnVolverDetCitac.png" runat="server" onclick="btnVolverDetCitacion_Click" Visible="false" />
                </div>
                <asp:LinkButton ID="btnClosePopup" runat="server">Cerrar</asp:LinkButton>
            </asp:Panel>
            <asp:ModalPopupExtender ID="MPE" runat="server" TargetControlID="Button1" PopupControlID="pnlCitac"
                BackgroundCssClass="modalBackground" DropShadow="false" CancelControlID="btnClosePopup" />
        </asp:Panel>
    </div>
</asp:Content>
