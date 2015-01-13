<%@ Page Title="" Language="C#" MasterPageFile="~/OneColumnV2.master" AutoEventWireup="true" CodeFile="ModEntrega.aspx.cs" Inherits="Tramites_ModEntrega" %>
<%@ MasterType VirtualPath="~/OneColumnV2.master" %>

<%@ Register src="../UserControls/NewShippingAddress.ascx" tagname="NewShippingAddress" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<!--<script type="text/javascript" src="../js/formElements.js"></script>-->
<script src="../js/common.js" type="text/javascript"></script>
<style type="text/css">
 table { 
    text-align:center;
    width: 80%;
    border: 2;
    border-style:dashed;
    margin-left: 100px;
  }
  table th {
    text-align: right;
    padding: 10px;
    vertical-align:middle;
  }
  table td {
    text-align: left;
    padding: 10px;
  }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div id="divContent" runat="server">
        <div class="title">
            Trámites en Línea<br />
            <br />
            <asp:Label Text="Seleccione la forma de entrega deseada:" runat="server" ID="lblSubtitulo" />
        </div>
        <div class="full">
            <div style="width: 50%; margin: 20px 0 0 190px;">
                <asp:RadioButtonList ID="rbtnModoEntrega" runat="server" 
                    onselectedindexchanged="rbtnModoEntrega_SelectedIndexChanged" AutoPostBack="true">
                </asp:RadioButtonList>
            </div>
            <div>
                <asp:HiddenField ID="hdnTipoTramite" runat="server" />
                <asp:HiddenField ID="hdnNumSolicitud" runat="server" />
                <asp:HiddenField ID="hdnValorPago" runat="server" />
                <asp:HiddenField ID="hdnCodProcesoTramite" runat="server" />
                <uc1:NewShippingAddress ID="NewShippingAddress1" onEvento="NewShippingAddress1_Evento" Visible="false" runat="server" />

                <asp:Panel ID="pnlOficinasCTG" Visible="false" runat="server" style="width:80%; margin:15px 0 0 100px; text-align:center">
                    <asp:Label ID="lblOficCtgTitle" runat="server" Text="Seleccione el lugar donde desea retirar su trámite" />
                    <asp:Label ID="lblCdeEntregaDocs" runat="server" Text="Este trámite requiere entrega de documentación, seleccione el lugar donde usted realizará la entrega de documentos:" Visible="false" />
                    <div style="margin: 5px"><asp:DropDownList ID="ddlProvinciasOficinas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlProvinciasOficinas_SelectedIndexChanged" /></div>
                    <div style="margin: 5px"><asp:DropDownList ID="ddlOficinas" runat="server" Visible="false" /></div>
                    <h6><span style="color:#505f76"><asp:Label ID="lblOficCtgNota" runat="server" Text="Si el trámite requiere entrega de documentación, lo deberá hacer en el lugar que haya seleccionado." /></span></h6>
                </asp:Panel>

            </div>
            <div style="width: 50%; margin: 20px 0 10px 380px;">
                <asp:Button ID="btnContinuar" runat="server" Text="Continuar »" onclick="btnContinuar_Click" Visible="false" CausesValidation="true" />
            </div>
        </div>
    </div>
</asp:Content>

