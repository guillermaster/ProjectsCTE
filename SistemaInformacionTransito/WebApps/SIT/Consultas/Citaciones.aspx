<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Citaciones.aspx.cs" Inherits="Consultas_Citaciones" %>

<%@ MasterType VirtualPath="~/Site.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script language="javascript" type="text/javascript">
        function SetFocusOnNumLic() {
            document.getElementById('<%= txtNumLic.ClientID %>').focus();
        }
        function SetFocusOnNumCitac() {
            document.getElementById('<%= txtNumCitac.ClientID %>').focus();
        }
        function CollapseCP1() {
            $find("cpe1")._doClose();
        }
        function CollapseCP2() {
            $find("cpe2")._doClose();
        }
        function CollapseCPs() {

            $find("cpe1")._doClose();
            $find("cpe2")._doClose();
            document.getElementById("<%= pnlContent.ClientID %>").style.display = "none";
        }  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="QueryContent" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Panel ID="pnlTitleExt" runat="server" CssClass="collapsePanelHeader">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/btnconsultarSel.png"
                        OnClientClick="javascript: CollapseCP2();" />
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </asp:Panel>
            </td>
            <td>
                <asp:Panel ID="pnlTitleExt2" runat="server" CssClass="collapsePanelHeader">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/btnconsultar.png"
                        OnClientClick="javascript: CollapseCP1();" />
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlQuery" runat="server" CssClass="collapsePanel">
        <div id="searchFormBigger">
            Núm. de Licencia:
            <asp:TextBox ID="txtNumLic" runat="server" CausesValidation="True" ValidationGroup="ValGroupConsPorLic"
                Width="115px" />
            <asp:RequiredFieldValidator ID="reqFValNumLic" runat="server" ErrorMessage="*" ControlToValidate="txtNumLic"
                ValidationGroup="ValGroupConsPorLic" /><br />
                <asp:RadioButtonList ID="rblEstado" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="N" Selected="True">No pagadas</asp:ListItem>
                    <asp:ListItem Value="S">Pagadas</asp:ListItem>
                </asp:RadioButtonList>
            <asp:Button ID="btnConsultarPorLic" runat="server" Text="Consultar" ValidationGroup="ValGroupConsPorLic"
                OnClick="btnConsultarPorLic_Click" OnClientClick="javascript: CollapseCPs();" />
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlQuery2" runat="server" CssClass="collapsePanel2">
        <div id="searchForm">
            Núm. de Citación:
            <asp:TextBox ID="txtNumCitac" runat="server" CausesValidation="True" ValidationGroup="ValGroupConsPorCod"
                Width="115px" />
            <asp:RequiredFieldValidator ID="reqFValNumCit" runat="server" ErrorMessage="*" ControlToValidate="txtNumCitac"
                ValidationGroup="ValGroupConsPorCod" />
            <asp:Button ID="btnConsultarPorCod" runat="server" Text="Consultar" ValidationGroup="ValGroupConsPorCod"
                OnClick="btnConsultarPorCod_Click" OnClientClick="javascript: CollapseCPs();" />
        </div>
    </asp:Panel>
    <cc1:collapsiblepanelextender id="CollapsiblePanelExtender1" behaviorid="cpe1" runat="server"
        targetcontrolid="pnlQuery" expandcontrolid="pnlTitleExt" collapsecontrolid="pnlTitleExt"
        textlabelid="Label1" collapsedtext="" expandedtext="" imagecontrolid="ImageButton1"
        expandedimage="~/images/btnconsultarSel.png" collapsedimage="~/images/btnconsultar.png"
        collapsedsize="0" expandedsize="140" collapsed="true" suppresspostback="true">
    </cc1:collapsiblepanelextender>
    <cc1:collapsiblepanelextender id="CollapsiblePanelExtender2" behaviorid="cpe2" runat="server"
        targetcontrolid="pnlQuery2" expandcontrolid="pnlTitleExt2" collapsecontrolid="pnlTitleExt2"
        textlabelid="Label2" collapsedtext="" expandedtext="" imagecontrolid="ImageButton2"
        expandedimage="~/images/btnconsultarSel.png" collapsedimage="~/images/btnconsultar.png"
        collapsedsize="0" expandedsize="140" collapsed="false" suppresspostback="true">
    </cc1:collapsiblepanelextender>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Panel ID="pnlContent" runat="server" style="margin-top: 10px" visible="false">
        <asp:GridView ID="gvCitaciones" runat="server" OnSelectedIndexChanged="gvCitaciones_SelectedIndexChanged"
                OnRowDataBound="gvCitaciones_RowDataBound" AutoGenerateColumns="False" ShowFooter="True" Visible="false">
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
                <asp:BoundField DataField="num_citacion" HeaderText="Código" />
                <asp:BoundField DataField="tipo_citacion" HeaderText="Tipo" />
                <asp:BoundField DataField="licencia_infractor" HeaderText="Licencia del infractor" />
                <asp:BoundField DataField="placa_vehiculo" HeaderText="Placa del vehículo" />
                <asp:BoundField DataField="fecha_citacion" HeaderText="Fecha de citación" />
                <asp:BoundField DataField="estado_pagada" HeaderText="Pagada" />
                <asp:BoundField DataField="valor_citacion" HeaderText="Valor ($)" />
                <asp:BoundField DataField="multa_citacion" HeaderText="Multa ($)" />
                <asp:BoundField DataField="total_pagar" HeaderText="Total ($)" />
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
            <cc1:ModalPopupExtender ID="MPE" runat="server" TargetControlID="Button1" PopupControlID="pnlCitac"
                BackgroundCssClass="modalBackground" DropShadow="false" CancelControlID="btnClosePopup" />
        </asp:Panel>
    </asp:Panel>
</asp:Content>
