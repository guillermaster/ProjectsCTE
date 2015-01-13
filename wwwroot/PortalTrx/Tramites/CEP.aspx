<%@ Page Title="" Language="C#" MasterPageFile="~/MainV2.master" AutoEventWireup="true"
    CodeFile="CEP.aspx.cs" Inherits="Tramites_CEP" %>

<%@ MasterType VirtualPath="~/MainV2.master" %>
<%@ Register Src="../UserControls/BcoBolivariano.ascx" TagName="BcoBolivariano" TagPrefix="ucBancos" %>
<%@ Register Src="../UserControls/BcoPacifico.ascx" TagName="BcoPacifico" TagPrefix="ucBancos" %>
<%@ Register Src="../UserControls/BcoGuayaquil.ascx" TagName="BcoGuayaquil" TagPrefix="ucBancos" %>
<%@ Register Src="../UserControls/BcoProdubanco.ascx" TagName="BcoProdubanco" TagPrefix="ucBancos" %>
<%@ Register Src="../UserControls/BcoServipagos.ascx" TagName="BcoServipagos" TagPrefix="ucBancos" %>
<%@ Register src="../UserControls/BcoWesternUnion.ascx" tagname="BcoWesternUnion" tagprefix="ucBancos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="../js/common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function launchPopUp(panelID) {
            var pnl = document.getElementById(panelID);
            var inHtml = pnl.innerHTML;
            popupAndWriteHtml('630', '420', '0', 'popupCTG', inHtml);
            //window.print();
        }
    </script>
    <style type="text/css">
        .printer
        {
            display: block;
            width: 183px;
            height: 22px;
            background-image: url(../images/buttonUpPrintPreview.gif);
            text-indent: -999em;
        }
        
        .printer:hover
        {
            background-image: url(../images/buttonDownPrintPreview.gif);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightColumnContent" runat="Server">
    <div class="full">
        <h2>Pague por Internet su trámite a través de:</h2>
        <ucBancos:BcoBolivariano ID="BcoBolivariano1" runat="server" />
        <ucBancos:BcoGuayaquil ID="BcoGuayaquil1" runat="server" />
        <ucBancos:BcoPacifico ID="BcoPacifico1" runat="server" />
        <ucBancos:BcoProdubanco ID="BcoProdubanco1" runat="server" />
        <h6>También puede realizar los pagos en las agencias de los bancos mencionados.</h6>
    </div>
    <div class="full">
        <h2>o en las agencias de:</h2>
        <ucbancos:bcoservipagos ID="BcoServipagos1" runat="server" />
        <ucBancos:BcoWesternUnion ID="BcoWesternUnion1" runat="server" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divContent" runat="server">
        <div class="full">
            <h2>Código Electrónico de Pago</h2>
            <asp:Panel runat="server" ID="pnlCEP">
                <table align="center" width="580" border="0" cellspacing="3" cellpadding="3">
                    <tr>
                        <td colspan="2">
                            <img src="../images/cep_title.png" alt="" />
                        </td>
                    </tr>
                    <tr>
                        <td width="28%">
                            <b>Código de Pago (CEP):</b>
                        </td>
                        <td width="72%">
                            &nbsp;<asp:Label ID="lblCEP" runat="server" Text="CEP" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Tr&aacute;mite:
                        </td>
                        <td>
                            &nbsp;<asp:Label ID="lblTramite" runat="server" Text="Tramite"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Identificación:
                        </td>
                        <td>
                            &nbsp;<asp:Label ID="lblIdentificacion" runat="server" Text="Identificacion" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Usuario:
                        </td>
                        <td>
                            &nbsp;<asp:Label ID="lblUsuario" runat="server" Text="Usuario" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Valor a pagar:
                        </td>
                        <td>
                            &nbsp;<asp:Label ID="lblValorPago" runat="server" Text="Valor" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Fecha:
                        </td>
                        <td>
                            &nbsp;<asp:Label ID="lblFecha" runat="server" Text="fecha" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="padding: 6px; margin: 6px;">
                            <asp:Label ID="lblMensaje1" runat="server" Text="" ForeColor="#C00000" style="margin-top:10px;" />
                            <div style="color: #000000; font-size: 90%; margin: 10px;">
                                <asp:Repeater ID="repMessages" runat="server">
                                    <HeaderTemplate>
                                        <b>Debe acercarse a la CTE para presentar los siguientes documentos:</b>
                                        <ul>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <li>
                                            <%# Eval("Mensaje") %></li>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </ul>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <div>
                <table align="center" width="580" border="0" cellspacing="3" cellpadding="3">
                    <tr>
                        <td colspan="2" align="center" valign="middle">
                            <div align="center" style="margin: 20px">
                                
                                <asp:Button ID="btnPrint" runat="server" Text="Imprimir" SkinID="PrintPreview" />
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
