<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VehRepRobados.aspx.cs" EnableEventValidation="false"
    Inherits="Consultas_VehiculosDetenidos_VehRepRobados" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../../controls/logout.ascx" TagName="logout" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../../css/layout4_text.css" />

    <script type="text/javascript" src="../../js/common.js"></script>

    <style>
        .accordionCabecera
        {
        border: 1px solid black;        background-color: #30608d;
        font-family: Arial, Sans-Serif;        font-size: 14px; color:#ffffff;
        font-weight: bold;        padding: 4px;
        margin-top: 4px;        cursor: pointer;
        }

        .accordionContenido
        {
        font-family: Sans-Serif;       background-color: #bdcbd8;
        border: 1px solid black;        border-top: none;
        font-size: 12px;        padding: 7px;
        
        }
        .rowStyle
        {
        padding: 3px 5px 3px 5px;
        }
        .modalBackground {
        background-color:Gray;
        filter:alpha(opacity=70);
        opacity:0.7;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>
            Vehículos Reportados como Robados</h2>
        <uc1:logout ID="Logout1" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <script type= "text/javascript">
            if (Sys.Browser.agent == Sys.Browser.InternetExplorer){
                Sys.UI.DomElement.getLocation=function(a){
                if(a.self||a.nodeType===9)
                    return new Sys.UI.Point(0,0);
                var b=a.getBoundingClientRect();
                if(!b)
                    return new Sys.UI.Point(0,0);
                var c=a.document.documentElement,d=b.left-2+c.scrollLeft,e=b.top-2+c.scrollTop;
                try{
                    var g=a.ownerDocument.parentWindow.frameElement||null;
                    if(g){
                        var f=2-(g.frameBorder||1)*2;
                    d+=f;e+=f
                    }
               }catch(h){}
               return new Sys.UI.Point(d,e)};
        }
        </script>
        <div style="height: 10px;">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1"
                DisplayAfter="5">
                <ProgressTemplate>
                    <div>
                        <img src="../../img/ajax-loader.gif" />
                        Procesando datos...
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="Button1" runat="server" Style="display: none" />
                <asp:Panel ID="pnlPhoto" runat="server" Style="display: none" BackColor="AntiqueWhite"
                    ScrollBars="None">
                    <div align="center">
                        <br />
                        <i>Haga clic en la imagen/mensaje para cerrar</i><br />
                        <br />
                        <asp:ImageButton ID="imgFotoVehiculo" ImageUrl="fotoVehiculo.aspx" AlternateText="Haga clic en la imagen para cerrar"
                            runat="server" />
                        <br />
                        <br />
                        <i>Haga clic en la imagen/mensaje para cerrar</i><br />
                        <br />
                </asp:Panel>
                <cc1:ModalPopupExtender ID="MPE" runat="server" TargetControlID="Button1" PopupControlID="pnlPhoto"
                    BackgroundCssClass="modalBackground" DropShadow="false" CancelControlID="imgFotoVehiculo" />
                </div>
                <div style="margin-top: 20px;">
                    <cc1:Accordion ID="MyAccordion"
    runat="Server"
    SelectedIndex="0"
    HeaderCssClass="accordionCabecera"
    ContentCssClass="accordionContenido"
    AutoSize="None"
    FadeTransitions="true"
    TransitionDuration="250"
    FramesPerSecond="40"
    RequireOpenedPane="false"
    SuppressHeaderPostbacks="true">
                        <Panes>
                            <cc1:AccordionPane ID="AccordionPane1" runat="server">
                                <Header>
                                    Vehículos recientemente reportados como robados</Header>
                                <Content>
                                    <div align="center">
                                        <div id="divError1" runat="server" visible="false">
                                            <table class="error2" align="center" width="480">
                                                <tr>
                                                    <td style="width: 54px">
                                                        <img src="../../img/error.gif" /></td>
                                                    <td>
                                                        <b>Error:</b><br />
                                                        <asp:Label ID="lblMsgError1" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </div>
                                        <asp:GridView ID="gvRecientes" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                            ForeColor="#333333" GridLines="None" AllowPaging="True" PageSize="10" OnPageIndexChanging="VehRobGridView_PageIndexChanging">
                                            <RowStyle BackColor="#EFF3FB" />
                                            <Columns>
                                                <asp:BoundField DataField="placa" HeaderText="Placa de veh&#237;culo">
                                                    <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="fecha" HeaderText="Fecha de denuncia">
                                                    <ItemStyle Width="200px" HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Ver foto">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnImgPhoto" runat="server" ImageUrl="~/img/icon_photoCam.gif"
                                                            ImageAlign="Left" AlternateText="Ver foto" CommandName="Select" Style="padding-right: 15px"
                                                            OnClick="btnImgPhoto_Click" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="6" />
                                        </asp:GridView>
                                    </div>
                                </Content>
                            </cc1:AccordionPane>
                            <cc1:AccordionPane ID="AccordionPane2" runat="server">
                                <Header>
                                    Buscar por placa</Header>
                                <Content>
                                    <table>
                                        <tr>
                                            <td width="200" valign="middle">
                                                Placa:
                                                <asp:TextBox ID="txtPlaca" MaxLength="7" CssClass="uppercase" runat="server"></asp:TextBox></td>
                                            <td width="280" valign="middle">
                                                <asp:Button ID="btnBuscarPorPlaca" runat="server" Text="Buscar" CssClass="button"
                                                    OnClick="btnBuscarPorPlaca_Click" /></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" align="center">
                                                <div id="divError2" runat="server" visible="false">
                                                    <table class="error2" align="center" width="480">
                                                        <tr>
                                                            <td style="width: 54px">
                                                                <img src="../../img/error.gif" /></td>
                                                            <td>
                                                                <b>Error:</b><br />
                                                                <asp:Label ID="lblMsgError2" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div id="divWarning" runat="server" visible="false">
                                                    <table class="warning" align="center" width="480">
                                                        <tr>
                                                            <td style="width: 54px">
                                                                <img src="../../img/warning.gif" /></td>
                                                            <td>
                                                                <asp:Label ID="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <asp:GridView ID="gvPlacaVehRob" Visible="false" runat="server" AutoGenerateColumns="False"
                                                    CellPadding="4" ForeColor="#333333" GridLines="None">
                                                    <RowStyle BackColor="#EFF3FB" />
                                                    <Columns>
                                                        <asp:BoundField DataField="placa" HeaderText="Placa de veh&#237;culo">
                                                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="fecha" HeaderText="Fecha de denuncia">
                                                            <ItemStyle Width="200px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="Ver foto">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnImgPhotoPlacaRob" runat="server" ImageUrl="~/img/icon_photoCam.gif"
                                                                    ImageAlign="Left" AlternateText="Ver foto" CommandName="Select" Style="padding-right: 15px"
                                                                    OnClick="btnImgPhoto_Click" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#2461BF" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </Content>
                            </cc1:AccordionPane>
                        </Panes>
                    </cc1:Accordion>
                </div>
                <br />
                <br />
                <br />
                <div class="column1-unit">
                    <a href="../../DefaultConsultas.aspx">
                        <img src="../../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
