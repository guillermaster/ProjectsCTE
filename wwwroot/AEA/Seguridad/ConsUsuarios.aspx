<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="ConsUsuarios.aspx.cs" Inherits="Seguridad_ConsUsuario" %>

<%@ Register Src="../UserControls/BackButton.ascx" TagName="BackButton" TagPrefix="uc1" %>

<%@ Register Src="../UserControls/NewButton.ascx" TagName="NewButton" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-content1" align="center">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <contenttemplate>
<uc1:BackButton id="btnBack" runat="server" Visible="false"></uc1:BackButton> <uc1:NewButton id="btnNew" runat="server" TargetURL="RegUsuario.aspx"></uc1:NewButton> <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5" DynamicLayout="False">
            <ProgressTemplate>
                <img src="../img/ajax-loader.gif" />
            </ProgressTemplate>
        </asp:UpdateProgress> <DIV style="MARGIN: 0px 0px 20px; WIDTH: 779px" id="divConsulta" class="registerform" runat="server"><asp:Label id="lblAEA" runat="server" Text="Matriculación:" visible="false"></asp:Label> <asp:RequiredFieldValidator id="reqValAEA" runat="server" ControlToValidate="ddlAEA" ErrorMessage="*"></asp:RequiredFieldValidator> <asp:DropDownList id="ddlAEA" runat="server" CssClass="field" visible="false" Width="220px"></asp:DropDownList> <asp:Label id="lblComercializadora" runat="server" Text="Comercializadora:"></asp:Label> <asp:RequiredFieldValidator id="reqValComercializadora" runat="server" ControlToValidate="ddlComercializadora" ErrorMessage="*"></asp:RequiredFieldValidator> <asp:DropDownList id="ddlComercializadora" runat="server" CssClass="field" Width="240px" AutoPostBack="True" OnSelectedIndexChanged="ddlComercializadora_SelectedIndexChanged"></asp:DropDownList> <asp:Button id="btnConsultar" onclick="btnConsultar_Click" runat="server" Text="Buscar" CssClass="button_horiz"></asp:Button> <BR /><asp:Label id="lblSucursal" runat="server" Text="Sucursal:"></asp:Label> <asp:RequiredFieldValidator id="reqValSucursal" runat="server" ControlToValidate="ddlSucursal" ErrorMessage="*"></asp:RequiredFieldValidator> <asp:DropDownList id="ddlSucursal" runat="server" CssClass="field" Width="240px"></asp:DropDownList> <asp:HiddenField id="hdnTipoEmpresa" runat="server" __designer:wfdid="w21"></asp:HiddenField></DIV><asp:GridView id="gvUsuarios" runat="server" CssClass="mGrid" OnSelectedIndexChanged="gvUsuarios_SelectedIndexChanged" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" AutoGenerateColumns="False" EmptyDataText="No existen usuarios para la empresa seleccionada"><Columns>
<asp:BoundField DataField="id_usuario" HeaderText="Usuario">
<ItemStyle Width="120px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="nombres" HeaderText="Nombre">
<ItemStyle Width="260px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="descripcion" HeaderText="Tipo de Usuario">
<ItemStyle Width="170px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="correo" HeaderText="E-Mail">
<ItemStyle Width="220px"></ItemStyle>
</asp:BoundField>
<asp:CommandField SelectImageUrl="~/img/ico_details.gif" ShowSelectButton="True" ButtonType="Image"></asp:CommandField>
<asp:TemplateField><ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" OnClick="gvUsuarios_Edit" ImageUrl="~/img/ico_edit.gif" ToolTip="Modificar usuario" />
                    
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField><ItemTemplate>
                        <asp:ImageButton ID="btnDelete" runat="server" OnClientClick="return confirm('¿Está seguro que desea desactivar este usuario?');" OnClick="gvUsuarios_Delete" ImageUrl="~/img/ico_delete.gif" ToolTip="Eliminar usuario" />
                    
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:TemplateField>

<asp:TemplateField><ItemTemplate>
                        <asp:ImageButton ID="btnSendPwd" runat="server" OnClick="gvUsuarios_SendPwd" OnClientClick="return confirm('Usted va a resetear la contraseña del usuario, la nueva contraseña se enviará por mail a este usuario. ¿Desea continuar?');" ImageUrl="~/img/ico_sendpwd.gif" ToolTip="Enviar contraseña" />
                    
</ItemTemplate>

<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
</asp:TemplateField>
</Columns>

<PagerStyle CssClass="pgr"></PagerStyle>

<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
</asp:GridView> <BR /><DIV id="divDesactiva" runat="server" visible="false"><asp:Label id="lblDesactivaUsuario" runat="server" Text="Usuario:" Width="80px"></asp:Label> <asp:TextBox id="txtDesactivaUsuario" runat="server" CssClass="field" Width="150px" ReadOnly="true"></asp:TextBox>&nbsp; <asp:HiddenField id="hdnDesacNombre" runat="server" __designer:wfdid="w2"></asp:HiddenField> <asp:HiddenField id="hdnDesacTipoUsuario" runat="server" __designer:wfdid="w3"></asp:HiddenField> <asp:HiddenField id="hdnDesacEmail" runat="server" __designer:wfdid="w4"></asp:HiddenField> <asp:HiddenField id="hdnDesacIdentificacion" runat="server" __designer:wfdid="w5"></asp:HiddenField><BR /><asp:Label id="lblDesactivaEmpresa" runat="server" Text="Comercializadora:" Width="80px"></asp:Label> <asp:TextBox id="txtDesactivaEmpresa" runat="server" CssClass="field" Width="313px" ReadOnly="true"></asp:TextBox> <BR /><asp:Label id="lblDesactivaObs" runat="server" Text="Observación:"></asp:Label> <asp:TextBox id="txtDesactivaObs" runat="server" Width="430px" TextMode="MultiLine" Rows="3"></asp:TextBox> <asp:RequiredFieldValidator id="reqFieldObserv" runat="server" ControlToValidate="txtDesactivaObs" ErrorMessage="Debe ingresar una observación"></asp:RequiredFieldValidator> <BR /><asp:Button id="btnDesacitvar" onclick="btnDesacitvar_Click" runat="server" Text="Desactivar" CssClass="button" Width="93px"></asp:Button> <asp:Button id="btnDesacCancelar" onclick="btnDesacCancelar_Click" runat="server" Text="Cancelar" CausesValidation="False" CssClass="button" Width="80px"></asp:Button> </DIV><DIV id="divError" runat="server" visible="false"><TABLE class="error2" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="../img/error.gif" /></TD><TD><B>Error:</B><BR /><asp:Label id="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV><DIV id="divWarning" runat="server" visible="false"><TABLE class="warning" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="../img/warning.gif" /></TD><TD><asp:Label id="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV>
</contenttemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

