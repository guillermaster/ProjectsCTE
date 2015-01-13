<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ConsSolMatriculasCTG.aspx.cs" Inherits="ConsComercializadoras" %>

<%@ Register Src="UserControls/PrintButton.ascx" TagName="PrintButton" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content1" align="center">
    <uc1:PrintButton ID="btnPrint" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<DIV id="divContent"><asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5" DynamicLayout="False">
            <ProgressTemplate>
             <div align="center">
                <img src="img/ajax-loader.gif" />
              </div>
        </ProgressTemplate>
        </asp:UpdateProgress> <DIV style="WIDTH: 629px" id="divBusqueda" class="registerform" runat="server"><asp:Label id="lblFechaIni" runat="server" Text="Desde:"></asp:Label> <asp:TextBox id="txtFechaIni" runat="server"></asp:TextBox> <cc1:CalendarExtender id="calFechaIni" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaIni"></cc1:CalendarExtender> &nbsp;&nbsp;&nbsp; <asp:Label id="lblFechaFin" runat="server" Text="Hasta:"></asp:Label> <asp:TextBox id="txtFechaFin" runat="server"></asp:TextBox> <cc1:CalendarExtender id="calFechaFin" runat="server" Format="dd/MM/yyyy" TargetControlID="txtFechaFin"></cc1:CalendarExtender> <asp:Button id="btnSearch" onclick="btnSearch_Click" runat="server" Text="Buscar" CssClass="button_horiz"></asp:Button> <BR /><BR /><asp:RadioButton id="rbGestor" runat="server" GroupName="criteriosBusq"></asp:RadioButton> <asp:Label id="lblGestor" runat="server" Text="Gestor:"></asp:Label> <asp:DropDownList id="ddlGestor" runat="server" CssClass="field"></asp:DropDownList> <BR /><asp:RadioButton id="rbProv" runat="server" GroupName="criteriosBusq"></asp:RadioButton> <asp:Label id="lblProv" runat="server" Text="Provincia:"></asp:Label> <asp:DropDownList id="ddlProv" runat="server" CssClass="field"></asp:DropDownList> <asp:RadioButton id="rbUsuario" runat="server" GroupName="criteriosBusq"></asp:RadioButton> <asp:Label id="lblUsuario" runat="server" Text="Usuario CTG:"></asp:Label> <asp:DropDownList id="ddlUsuario" runat="server" CssClass="field"></asp:DropDownList> </DIV><BR /><asp:GridView id="gvSolMatriculas" runat="server" OnSelectedIndexChanged="gvSolMatriculas_SelectedIndexChanged" Width="648px" AutoGenerateColumns="False" EmptyDataText="No existe ninguna solicitud de matrícula activa" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
<EmptyDataRowStyle ForeColor="Maroon"></EmptyDataRowStyle>
<Columns>
<asp:BoundField DataField="id_solicitud"></asp:BoundField>
<asp:BoundField DataField="comercializadora" HeaderText="Comercializadora">
<ItemStyle Width="280px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="gestor" HeaderText="Gestor">
<ItemStyle Width="60px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="fecha_registro" HeaderText="Fecha Creaci&#243;n">
<ItemStyle Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="usuario_registra" HeaderText="Usuario Crea"></asp:BoundField>
<asp:BoundField DataField="estado" HeaderText="Estado"></asp:BoundField>
<asp:CommandField CausesValidation="False" SelectImageUrl="~/img/ico_details.gif" ShowSelectButton="True" ButtonType="Image"></asp:CommandField>
</Columns>
</asp:GridView> <BR /><DIV id="divDesactiva" runat="server" visible="false"><BR />&nbsp;&nbsp;<BR />&nbsp; <BR /></DIV></DIV>
</ContentTemplate>
    </asp:UpdatePanel>
    </div>
</asp:Content>