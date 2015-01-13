<%@ Page Title="" Language="C#" MasterPageFile="~/MainV2.master" AutoEventWireup="true"
    CodeFile="VehiculosRobados.aspx.cs" Inherits="Consultas_Vehiculos_VehiculosRobados" %>

<%@ MasterType VirtualPath="~/Main.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../../js/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../../js/alertbox.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../../js/style.css" />
    <script type="text/javascript" language="javascript">
        function hidePlaca() {
            document.getElementById("txtPlaca").style.display = "none";
            document.getElementById("txtChasis").style.display = "";
        }
        function hideChasis() {
            document.getElementById("txtPlaca").style.display = "";
            document.getElementById("txtChasis").style.display = "none";
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="RightColumnContent">
    <div class="full">
        <h2>Buscar vehículos robados</h2>
        <div style="margin-bottom: 5px;">
            Tipo de identificación:</div>
        <div>
            <asp:RadioButton ID="rbtnPlaca" GroupName="TipoIdent" Text="&nbsp;Placa" Checked="true"
                runat="server" onclick="javascript:hideChasis()" Style="margin: 15px;" />
            <asp:RadioButton ID="rbtnChasis" GroupName="TipoIdent" Text="&nbsp;Chasis" Checked="false"
                runat="server" onclick="javascript:hidePlaca()" Style="margin: 15px;" />
        </div>
        <div style="margin: 5px;">
            <asp:TextBox ID="txtPlaca" ClientIDMode="Static" MaxLength="7" Width="125" runat="server" />
            <asp:TextBox ID="txtChasis" ClientIDMode="Static" MaxLength="27" Width="125" Style="display: none"
                runat="server" />
            <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
            <asp:DetailsView ID="detViewVehRobado" runat="server" SkinID="detViewSmall">
                <Fields>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnImgPhoto2" runat="server" ImageUrl="~/images/icoPhoto.png"
                                ImageAlign="Left" AlternateText="Ver foto" CommandName="Select" Style="padding-right: 15px"
                                OnClick="btnImgPhoto2_Click" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                </Fields>
            </asp:DetailsView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="title">
        Vehículos Robados
    </div>
    <div class="full" id="divContent" runat="server">
        <asp:GridView ID="gvRecientes" runat="server" CellPadding="4" OnPageIndexChanging="VehRobGridView_PageIndexChanging"
            Width="580px" AllowPaging="true">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:ImageButton ID="btnImgPhoto" runat="server" ImageUrl="~/images/icoPhoto.png"
                            ImageAlign="Left" AlternateText="Ver foto" CommandName="Select" Style="padding-right: 15px"
                            OnClick="btnImgPhoto_Click" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="Button1" runat="server" Style="display: none" />
        <asp:Panel ID="pnlPhoto" runat="server" Style="display: none; padding: 10px; text-align: center;"
            BackColor="AntiqueWhite" ScrollBars="None">
            <div align="center" style="padding: 10px">
                <i>Haga clic en la imagen/mensaje para cerrar</i><br />
                <br />
                <asp:ImageButton ID="imgFotoVehiculo" ImageUrl="fotoVehiculo.aspx" AlternateText="Haga clic en la imagen para cerrar"
                    runat="server" />
                <br />
                <br />
                <i>Haga clic en la imagen/mensaje para cerrar</i>
            </div>
        </asp:Panel>
        <asp:ModalPopupExtender ID="MPE" runat="server" TargetControlID="Button1" PopupControlID="pnlPhoto"
            BackgroundCssClass="modalBackground" DropShadow="false" CancelControlID="imgFotoVehiculo" />
    </div>
</asp:Content>
