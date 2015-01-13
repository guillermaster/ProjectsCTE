<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ConsSolMatriculas.aspx.cs" Inherits="ConsComercializadoras" %>

<%@ Register Src="UserControls/PrintButton.ascx" TagName="PrintButton" TagPrefix="uc1" %>

<%@ Register Src="UserControls/NewButton.ascx" TagName="NewButton" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<div class="main-content1" align="center">
    <uc1:NewButton id="btnNew" runat="server"></uc1:NewButton>
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
        </asp:UpdateProgress> <DIV style="WIDTH: 779px" id="divBusqueda" class="registerform" runat="server"><asp:Label id="lblFechaIni" runat="server" Text="Desde:" CssClass="label"></asp:Label> <asp:RequiredFieldValidator id="reqValFechaIni" runat="server" __designer:wfdid="w1" ErrorMessage="*" ControlToValidate="txtFechaIni"></asp:RequiredFieldValidator> <asp:RegularExpressionValidator id="regExpFechaIni" runat="server" __designer:wfdid="w3" ErrorMessage="*" ControlToValidate="txtFechaIni" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator> <asp:TextBox id="txtFechaIni" runat="server" CssClass="field"></asp:TextBox>&nbsp; <cc1:CalendarExtender id="calFechaIni" runat="server" TargetControlID="txtFechaIni" Format="dd/MM/yyyy"></cc1:CalendarExtender> <asp:Label id="lblFechaFin" runat="server" Text="Hasta:" CssClass="label"></asp:Label> <asp:RequiredFieldValidator id="reqValFechaFin" runat="server" __designer:wfdid="w2" ErrorMessage="*" ControlToValidate="txtFechaFin"></asp:RequiredFieldValidator> <asp:RegularExpressionValidator id="reqExpFechaFin" runat="server" __designer:wfdid="w4" ErrorMessage="*" ControlToValidate="txtFechaFin" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"></asp:RegularExpressionValidator> <asp:TextBox id="txtFechaFin" runat="server" CssClass="field"></asp:TextBox> <cc1:CalendarExtender id="calFechaFin" runat="server" TargetControlID="txtFechaFin" Format="dd/MM/yyyy"></cc1:CalendarExtender> <asp:Button id="btnSearch" onclick="btnSearch_Click" runat="server" Text="Buscar" CssClass="button_horiz"></asp:Button> <BR /><asp:RadioButton id="rbGestor" runat="server" GroupName="criteriosBusq"></asp:RadioButton> <asp:Label id="lblGestor" runat="server" Text="Gestor:"></asp:Label> <asp:DropDownList id="ddlGestor" runat="server" CssClass="field"></asp:DropDownList><BR /><asp:RadioButton id="rbProv" runat="server" GroupName="criteriosBusq"></asp:RadioButton> <asp:Label id="lblProv" runat="server" Text="Provincia:"></asp:Label> <asp:DropDownList id="ddlProv" runat="server" CssClass="field"></asp:DropDownList> </DIV><BR /><BR /><asp:GridView id="gvSolMatriculas" runat="server" CssClass="mGrid" Width="648px" AutoGenerateColumns="False" EmptyDataText="No existe ninguna solicitud de matrícula activa con los criterios de búsqueda seleccionados" OnSelectedIndexChanged="gvSolMatriculas_SelectedIndexChanged" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
<EmptyDataRowStyle ForeColor="Maroon"></EmptyDataRowStyle>
<Columns>
<asp:BoundField DataField="id_solicitud"></asp:BoundField>
<asp:BoundField DataField="comercializadora" HeaderText="Comercializadora">
<ItemStyle Width="210px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="gestor" HeaderText="Gestor">
<ItemStyle Width="290px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="fecha_registro" HeaderText="Fecha Creaci&#243;n">
<ItemStyle Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="usuario_registra" HeaderText="Usuario Crea">
<ItemStyle Width="135px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="estado" HeaderText="Estado">
<ItemStyle Width="100px"></ItemStyle>
</asp:BoundField>
<asp:CommandField CausesValidation="False" SelectImageUrl="~/img/ico_details.gif" ShowSelectButton="True" ButtonType="Image"></asp:CommandField>
<asp:TemplateField><ItemTemplate>
                        <asp:ImageButton ID="btnDelete" runat="server" OnClientClick="return confirm('¿Está seguro que desea detener la solicitud?');" OnClick="gvSolMatricula_Delete" ImageUrl="~/img/ico_delete.gif" ToolTip="Detener Solicitud" />
                    
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:TemplateField>
</Columns>

<PagerStyle CssClass="pgr"></PagerStyle>

<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
</asp:GridView> <BR /><DIV id="divDesactiva" runat="server" visible="false"><asp:HiddenField id="hdnCodComercializadora" runat="server"></asp:HiddenField><BR />&nbsp; <BR /><asp:Label id="lblDesactivaObs" runat="server" Text="Observación:"></asp:Label> <asp:TextBox id="txtDesactivaObs" runat="server" Width="430px" Rows="3" TextMode="MultiLine"></asp:TextBox> <asp:RequiredFieldValidator id="reqFieldObserv" runat="server" ErrorMessage="Debe ingresar una observación" ControlToValidate="txtDesactivaObs"></asp:RequiredFieldValidator> <BR /><asp:Button id="btnDesacitvar" onclick="btnDesacitvar_Click" runat="server" Text="Desactivar" CssClass="button" Width="93px"></asp:Button> <asp:Button id="btnDesacCancelar" onclick="btnDesacCancelar_Click" runat="server" Text="Cancelar" CausesValidation="False" CssClass="button" Width="80px"></asp:Button> </DIV></DIV>
</contenttemplate>
    </asp:UpdatePanel>
 </div>
</asp:Content>
