<%@ Page Language="C#" MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="ConsComercializadoras.aspx.cs" Inherits="ConsComercializadoras" %>

<%@ Register Src="UserControls/PrintButton.ascx" TagName="PrintButton" TagPrefix="uc1" %>

<%@ Register Src="UserControls/NewButton.ascx" TagName="NewButton" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div class="main-content1">
    <uc1:NewButton ID="btnNew" runat="server" />
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
        </asp:UpdateProgress>
         <DIV style="WIDTH: 481px" class="registerform"><asp:Label id="lblCodAutomotor" runat="server" Text="Criterio de búsqueda:"></asp:Label> <asp:DropDownList id="ddlBusqueda" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBusqueda_SelectedIndexChanged">
        </asp:DropDownList> <asp:DropDownList id="ddlBusqProvincia" runat="server" Visible="False" CssClass="field" Width="179px"></asp:DropDownList> <asp:TextBox id="txtBusqUsuario" runat="server" Visible="False"></asp:TextBox> <asp:TextBox id="txtBusqFecha" runat="server" Visible="False"></asp:TextBox> <cc1:CalendarExtender id="calExt" runat="server" Format="dd/MM/yyyy" TargetControlID="txtBusqFecha"></cc1:CalendarExtender> <asp:Button id="btnSearch" onclick="btnSearch_Click" runat="server" Text="Buscar" CssClass="button_horiz"></asp:Button> </DIV><BR /><BR /><BR />
        <asp:GridView id="gvComercializadoras" runat="server" CssClass="mGrid" OnSelectedIndexChanged="gvComercializadoras_SelectedIndexChanged" AutoGenerateColumns="False" EmptyDataText="No existe ninguna comercializadora activa con el criterio de búsqueda seleccionado">
<EmptyDataRowStyle ForeColor="Maroon"></EmptyDataRowStyle>
<Columns>
<asp:TemplateField><ItemTemplate>
        <asp:HiddenField ID="hdnCodComerc" Value='<%# Bind("id_det_comercializadora") %>' runat="server"></asp:HiddenField>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="razon_social" HeaderText="Raz&#243;n Social">
<ItemStyle Width="280px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="nombre_comercial" HeaderText="Nombre Comercial">
<ItemStyle Width="250px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="representante_legal" HeaderText="Representante Legal">
<ItemStyle Width="310px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="nombre_contacto" HeaderText="Nombre del Contacto">
<ItemStyle Width="310px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="telefono1" HeaderText="Tel&#233;fono">
<ItemStyle Width="80px"></ItemStyle>
</asp:BoundField>
<asp:CommandField SelectImageUrl="~/img/ico_details.gif" SelectText="" ShowSelectButton="True" ButtonType="Image"></asp:CommandField>
<asp:TemplateField><ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" OnClick="gvComercializadoras_Edit" ImageUrl="~/img/ico_edit.gif" ToolTip="Modificar comercializadora" />
                    
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField><ItemTemplate>
                        <asp:ImageButton ID="btnDelete" runat="server" OnClientClick="return confirm('¿Está seguro que desea desactivar la comercializadora?');" OnClick="gvComercializadoras_Delete" ImageUrl="~/img/ico_delete.gif" ToolTip="Desactivar comercializadora" />
                    
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:TemplateField>
</Columns>

<PagerStyle CssClass="pgr"></PagerStyle>

<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
</asp:GridView> <BR /><DIV id="divDesactiva" runat="server" visible="false"><asp:Label id="lblDesactivaRUC" runat="server" Text="RUC:" Width="80px"></asp:Label> <asp:TextBox id="txtDesactivaRUC" runat="server" CssClass="field" Width="150px" ReadOnly="true"></asp:TextBox> <asp:HiddenField id="hdnCodComercializadora" runat="server"></asp:HiddenField> <BR /><asp:Label id="lblDesactivaRazonSocial" runat="server" Text="Razón Social:" Width="80px"></asp:Label> <asp:TextBox id="txtDesactivaRazonSocial" runat="server" CssClass="field" Width="342px" ReadOnly="true"></asp:TextBox> <BR /><asp:Label id="lblDesactivaObs" runat="server" Text="Observación:"></asp:Label> <asp:TextBox id="txtDesactivaObs" runat="server" Width="430px" Rows="3" TextMode="MultiLine"></asp:TextBox> <asp:RequiredFieldValidator id="reqFieldObserv" runat="server" ErrorMessage="Debe ingresar una observación" ControlToValidate="txtDesactivaObs"></asp:RequiredFieldValidator> <BR /><asp:Button id="btnDesacitvar" onclick="btnDesacitvar_Click" runat="server" Text="Desactivar" CssClass="button" Width="93px"></asp:Button> <asp:Button id="btnDesacCancelar" onclick="btnDesacCancelar_Click" runat="server" Text="Cancelar" CausesValidation="False" CssClass="button" Width="80px"></asp:Button> </DIV></DIV>
</ContentTemplate>
    </asp:UpdatePanel>
  </div>
    </asp:Content>