<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ConsultaPagosEncolados.aspx.cs" Inherits="ConsultaPagosEncolados" %>
<%@ MasterType VirtualPath="~/Site.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="pnlContent" runat="server">
        <asp:GridView ID="gvPagosEncolados" runat="server" 
            onselectedindexchanged="gvPagosEncolados_SelectedIndexChanged">
            <Columns>
                <asp:CommandField SelectText="Ver citaciones" ShowSelectButton="true" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="Button1" runat="server" Style="display: none" />
        <asp:Panel ID="pnlCitacPagadas" runat="server" Style="display: none; padding: 10px; text-align: center;"
                BackColor="AntiqueWhite" ScrollBars="None">
                <asp:GridView ID="gvCitacPagadas" OnRowDataBound="gvCitacPagadas_RowDataBound" ShowFooter="true" runat="server" />
                <asp:LinkButton ID="btnClosePopup" runat="server">Cerrar</asp:LinkButton>
            </asp:Panel>
            <asp:ModalPopupExtender ID="MPE" runat="server" TargetControlID="Button1" PopupControlID="pnlCitacPagadas"
                BackgroundCssClass="modalBackground" DropShadow="false" CancelControlID="btnClosePopup" />
    </asp:Panel>    
</asp:Content>

