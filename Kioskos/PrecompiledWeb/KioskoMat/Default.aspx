<%@ page title="Home Page" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="_Default, App_Web_default.aspx.cdcab7d2" theme="SkinFile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript">
        function clickButton(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 13) {
                    bt.click();
                    return false;
                }
            }
        }

        $(document).ready(function () {
            $addHandler(document, "keydown", OnKeyPress);
        });

        function OnKeyPress(args) {
            if (args.keyCode == Sys.UI.Key.esc) {
                $(".btnClose").click();
            } else if (args.keyCode == 84) {//if 't' key is pressed
                $('#<%= btnConsTramite.ClientID %>').click();
            } else if (args.keyCode == 67) {//if 'c' key is pressed
                $('#<%= btnConsCitaciones.ClientID %>').click();
            }
        }

        function setFocusBtnTramites() {
            $('#<%= btnConsTramite.ClientID %>').focus();
        }
        function setFocusBtnCitaciones() {
            $('#<%= btnConsCitaciones.ClientID %>').focus();
        }

    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <asp:Panel ID="pnlContent" runat="server">
        
        <asp:Button ID="ButtonMpeTramites" runat="server" Style="display: none" />
        <asp:Button ID="btnConsTramite" runat="server" Text="Consultar estado de trámite  [T]" SkinID="MainButton" TabIndex="1"
            onclick="btnConsTramite_Click" />
        <asp:Panel ID="pnlTramite" runat="server" CssClass="modalForeground" Style="display: none;" ScrollBars="None">
            <h2>Consulta de Trámite</h2>
            <div id="divConsTramite">
                <asp:Label ID="lblNumTramite" runat="server" Text="Número de Trámite:"></asp:Label>
                <asp:TextBox ID="txtNumTramite" ClientIDMode="Static" runat="server"></asp:TextBox>
                <asp:Button ID="btnEjConsTramite" runat="server" Text="Consultar"
                    OnClick="btnEjConsTramite_Click" />
                <div style="min-height: 30px">
                    <asp:Label ID="lblErrorConsTramite" ForeColor="Red" runat="server" Text=""></asp:Label>
                    <asp:Panel ID="pnlResConsTramite" runat="server" Visible="false">
                        <table>
                           <tr>
                                <th><b>Trámite:</b></th>
                                <th><asp:Label ID="lblNombreTramite" runat="server" Text=""></asp:Label></th>
                            </tr><tr>
                                <td><b>Num. Etapa:</b></td>
                                <td><asp:Label ID="lblNumEtapa" runat="server" Text=""></asp:Label></td>
                            </tr><tr>
                                <td><b>Descripción:</b></td>
                                <td><asp:Label ID="lblDescEtapa" runat="server" Text=""></asp:Label></td>
                            </tr><tr>
                                <td><b>Estado:</b></td>
                                <td><asp:Label ID="lblEstTramite" runat="server" Text=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td><b>Fecha de ejecución:</b></td>
                                <td><asp:Label ID="lblFechaEjTramite" runat="server" Text=""></asp:Label></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </div>
            <asp:ImageButton ID="btnCloseConsTramite" CssClass="btnClose" OnClientClick="javascript:setFocusBtnTramites();" ImageUrl="~/images/btnClose.png" runat="server" />
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeConsTramite" runat="server" TargetControlID="ButtonMpeTramites"
            PopupControlID="pnlTramite" BackgroundCssClass="modalBackground" DropShadow="false"
            CancelControlID="btnCloseConsTramite" />
        <br />
        
        <asp:Button ID="btnMpeCitac" runat="server" Style="display: none" />
        <asp:Button ID="btnConsCitaciones" runat="server" TabIndex="2" Text="Consultar citaciones pendientes de pago  [C]" SkinID="MainButton" 
            onclick="btnConsCitaciones_Click" />
        <asp:Panel ID="pnlCitaciones" runat="server" CssClass="modalForeground" Style="display: none;"  ScrollBars="None">
            <h2>Consulta de Citaciones Pendientes de Pago</h2>
            <div id="divConsCitac">
                <asp:Label ID="lblNumCedula" runat="server" Text="Número de Cédula:"></asp:Label>
                <asp:TextBox ID="txtNumCedula" ClientIDMode="Static" runat="server"></asp:TextBox>
                <asp:Button ID="btnEjConsCitac" runat="server" Text="Consultar"
                    OnClick="btnEjConsCitac_Click" />
                <br />
                <div style="min-height: 30px">
                    <asp:Label ID="lblErrorConsCitac" ForeColor="Red" runat="server" Text=""></asp:Label>
                    <asp:Panel ID="pnlGvCitacPend" runat="server" ScrollBars="Auto" style="max-height: 300px;">
                        <asp:GridView ID="gvCitacPend" runat="server" style="margin-top: -65px" AutoGenerateColumns="False" ShowFooter="true">
                            <Columns>
                                <asp:BoundField DataField="num_citacion" HeaderText="N.Citación" />
                                <asp:BoundField DataField="tipo_citacion" HeaderText="Tipo" />
                                <asp:BoundField DataField="licencia_infractor" HeaderText="Licencia" />
                                <asp:BoundField DataField="placa_vehiculo" HeaderText="Placa" />
                                <asp:BoundField DataField="fecha_citacion" HeaderText="Fecha" />
                                <asp:BoundField DataField="articulo" HeaderText="Artículo de ley" />
                                <asp:TemplateField>
                                    <HeaderTemplate>Puntos a perder</HeaderTemplate>
                                    <ItemTemplate><%# Eval("puntos_perdidos").ToString()%></ItemTemplate>
                                    <FooterTemplate><%# hdnTotPunto.Value%></FooterTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="contravencion"  HeaderText="Descripción de infracción" />
                                <asp:TemplateField>
                                    <HeaderTemplate>Valor ($)</HeaderTemplate>
                                    <ItemTemplate><%# Eval("valor_citacion").ToString() %></ItemTemplate>
                                    <FooterTemplate><%# hdnTotCitac.Value %></FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>Multa ($)</HeaderTemplate>
                                    <ItemTemplate><%# Eval("multa_citacion").ToString()%></ItemTemplate>
                                    <FooterTemplate><%# hdnTotMulta.Value%></FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>Total ($)</HeaderTemplate>
                                    <ItemTemplate><%# Eval("total_pagar").ToString()%></ItemTemplate>
                                    <FooterTemplate><%# hdnTotTotal.Value%></FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <asp:HiddenField ID="hdnTotPunto" runat="server" Value="111" />
                        <asp:HiddenField ID="hdnTotCitac" runat="server" Value="222" />
                        <asp:HiddenField ID="hdnTotMulta" runat="server" Value="333" />
                        <asp:HiddenField ID="hdnTotTotal" runat="server" Value="444" />
                    </asp:Panel>
                </div>
            </div>
            
            <asp:ImageButton ID="btnCloseConsCitac" OnClientClick="javascript:setFocusBtnCitaciones();" CssClass="btnClose" ImageUrl="~/images/btnClose.png" runat="server" />
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeConsCitac" runat="server" TargetControlID="btnMpeCitac"
            PopupControlID="pnlCitaciones" BackgroundCssClass="modalBackground" DropShadow="false"
            CancelControlID="btnCloseConsCitac" />
    </asp:Panel>
</asp:Content>
