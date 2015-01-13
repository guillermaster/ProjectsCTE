<%@ page title="" language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="ConsCitaciones, App_Web_conscitaciones.aspx.cdcab7d2" theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <h2>Consulta de Citaciones</h2>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button ID="Button1" runat="server" Style="display: none" />
        <asp:Panel runat="server" CssClass="full" ID="pnlContent">
            <div id="searchForm">
                Núm. de Licencia:
                <asp:TextBox ID="txtNumLicencia" runat="server" CausesValidation="True" Width="115px" />
                <asp:RequiredFieldValidator ID="reqFValNumLic" runat="server" ErrorMessage="*" ControlToValidate="txtNumLicencia" /><br />
                Estado: 
                <asp:RadioButtonList ID="rblEstado" runat="server">
                    <asp:ListItem Value="N" Selected="True">Pendientes de pago</asp:ListItem>
                    <asp:ListItem Value="S">Pagadas</asp:ListItem>
                </asp:RadioButtonList>
                <br />
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" onclick="btnConsultar_Click" />
            </div>
            <asp:Panel ID="pnlWarning" runat="server" CssClass="warning" Visible="false">
                <asp:Label ID="lblWarning" runat="server"></asp:Label>
            </asp:Panel>
            <asp:GridView ID="gvCitacPend" runat="server" OnSelectedIndexChanged="gvCitacPend_SelectedIndexChanged"
                OnRowDataBound="gvCitacPend_RowDataBound" AutoGenerateColumns="False" ShowFooter="True" Visible="false">
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
            <asp:GridView ID="gvCitacPag" runat="server" OnSelectedIndexChanged="gvCitacPag_SelectedIndexChanged"
                OnRowDataBound="gvCitacPag_RowDataBound" AutoGenerateColumns="False" ShowFooter="True" Visible="false">
                <Columns>
                    <asp:CommandField CausesValidation="False" HeaderText="Detalles" SelectText="Ver"
                        ShowSelectButton="True">
                        <ItemStyle Font-Size="X-Small" />
                    </asp:CommandField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:HiddenField ID="hdnContravencion" runat="server" Value='<%# Eval("contravencion") %>' />
                            <asp:HiddenField ID="hdnArticulo" runat="server" Value='<%# Eval("articulo") %>' />
                            <asp:HiddenField ID="hdnPuntos" runat="server" Value='<%# Eval("puntos") %>' />
                            <asp:HiddenField ID="hdnNumCitacion" runat="server" Value='<%# Eval("num_infraccion") %>' />
                            <asp:HiddenField ID="hdnFechaCitacion" runat="server" Value='<%# Eval("fec_infraccion") %>' />
                            <asp:HiddenField ID="hdnUniformado" runat="server" Value='<%# Eval("uniformado") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="num_infraccion" HeaderText="Código de citación" />
                    <asp:BoundField DataField="identificacion" HeaderText="Licencia del infractor" />
                    <asp:BoundField DataField="placa" HeaderText="Placa de vehículo" />
                    <asp:BoundField DataField="fecha_citacion" HeaderText="Fecha de citación" />
                    <asp:BoundField DataField="reincidencias" HeaderText="Reincidencias" />
                    <asp:BoundField DataField="val_contrav" HeaderText="Valor pagado ($)" />
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
</asp:Content>

