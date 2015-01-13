<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsGestores.aspx.cs" Inherits="ConsComercializadoras" %>

<%@ Register Src="UserControls/PrintButton.ascx" TagName="PrintButton" TagPrefix="uc1" %>

<%@ Register Src="UserControls/NewButton.ascx" TagName="NewButton" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-content1" align="center">
    <uc1:NewButton id="btnNew" runat="server"></uc1:NewButton>
    <uc1:PrintButton ID="btnPrint" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
<DIV id="divContent"><asp:UpdateProgress id="UpdateProgress1" runat="server" DynamicLayout="False" DisplayAfter="5" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
             <div align="center">
                <img src="img/ajax-loader.gif" />
              </div>
        </ProgressTemplate>
        </asp:UpdateProgress> <asp:GridView id="gvGestores" runat="server" EmptyDataText="No existe ningún gestor activo" AutoGenerateColumns="False" OnSelectedIndexChanged="gvGestores_SelectedIndexChanged" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
<EmptyDataRowStyle ForeColor="Maroon"></EmptyDataRowStyle>
<Columns>
<asp:TemplateField><ItemTemplate>
        <asp:HiddenField ID="hdnCodGestor" Value='<%# Bind("id_gestor") %>' runat="server"></asp:HiddenField>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="identificacion" HeaderText="C&#233;dula">
<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="nombres" HeaderText="Nombres">
<ItemStyle Width="280px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="provincia" HeaderText="Provincia">
<ItemStyle Width="120px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="email" HeaderText="E-Mail">
<ItemStyle Width="250px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="telefono" HeaderText="Tel&#233;fono">
<ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
</asp:BoundField>
<asp:CommandField SelectImageUrl="~/img/ico_details.gif" SelectText="" ShowSelectButton="True" ButtonType="Image"></asp:CommandField>
<asp:TemplateField><ItemTemplate>
                        <asp:ImageButton ID="btnDelete" runat="server" OnClientClick="return confirm('¿Está seguro que desea desactivar el gestor?');" OnClick="gvGestores_Delete" ImageUrl="~/img/ico_delete.gif" ToolTip="Desactivar gestor" />
                    
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:TemplateField>
</Columns>
</asp:GridView> <BR /><DIV id="divDesactiva" runat="server" visible="false"><asp:Label id="lblDesactivaCedula" runat="server" Text="Cédula:" Width="80px"></asp:Label> <asp:TextBox id="txtDesactivaCedula" runat="server" CssClass="field" Width="150px" ReadOnly="true"></asp:TextBox>&nbsp; <asp:HiddenField id="hdnIdGestor" runat="server"></asp:HiddenField> <BR /><asp:Label id="lblDesactivaNombre" runat="server" Text="Nombre:" Width="80px"></asp:Label> <asp:TextBox id="txtDesactivaNombre" runat="server" CssClass="field" Width="342px" ReadOnly="true"></asp:TextBox> <BR /><asp:Label id="lblDesactivaObs" runat="server" Text="Observación:"></asp:Label> <asp:TextBox id="txtDesactivaObs" runat="server" Width="430px" TextMode="MultiLine" Rows="3"></asp:TextBox> <asp:RequiredFieldValidator id="reqFieldObserv" runat="server" ControlToValidate="txtDesactivaObs" ErrorMessage="Debe ingresar una observación"></asp:RequiredFieldValidator> <BR /><asp:Button id="btnDesactivar" onclick="btnDesactivar_Click" runat="server" Text="Desactivar" CssClass="button" Width="93px"></asp:Button> <asp:Button id="btnDesacCancelar" onclick="btnDesacCancelar_Click1" runat="server" Text="Cancelar" CausesValidation="False" CssClass="button" Width="80px"></asp:Button> </DIV></DIV>
</ContentTemplate>
    </asp:UpdatePanel>
   </div>
  </asp:Content>