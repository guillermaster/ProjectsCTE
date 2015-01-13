<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AsignaGestorSolMatricula.aspx.cs" Inherits="AsignaGestorSolMatricula" %>

<%@ Register Src="UserControls/SaveButton.ascx" TagName="SaveButton" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="main-content1" align="center">
  <form id="regForm">
     <asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
<uc1:SaveButton id="btnSave" runat="server" Visible="false" OnLoad="btnSave_Load"></uc1:SaveButton> <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5" DynamicLayout="False">
               <ProgressTemplate>
                  <div align="center">
                     <img src="img/ajax-loader.gif" />
                   </div>
               </ProgressTemplate>
           </asp:UpdateProgress>
           <div style="width: 779px;">
           <asp:HiddenField id="hdnCodComerc" runat="server"></asp:HiddenField>
            <!--<DIV style="WIDTH: 779px" id="divBusqueda" class="registerform" runat="server"><asp:Label id="lblComercSolPend" runat="server" Text="Comercializadoras con solicitudes pendientes:"></asp:Label> <asp:DropDownList id="ddlComercSolPend" runat="server" OnSelectedIndexChanged="ddlComercSolPend_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList> </DIV>
           <BR /><BR />-->
           <asp:GridView id="gvSolPendientes" runat="server" ForeColor="#333333" EmptyDataText="No existen solicitudes pendientes de asignar gestor para la comercializadora/sucursal seleccionada" HorizontalAlign="Center" GridLines="None" CellPadding="4" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" AutoGenerateColumns="False">
<RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
<Columns>
<asp:BoundField DataField="id_solicitud" HeaderText="C&#243;digo de Solicitud" ReadOnly="True">
<ItemStyle HorizontalAlign="Center" Width="50px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="cantidad" HeaderText="Automotores en Solicitud" ReadOnly="True">
<ItemStyle HorizontalAlign="Center" Width="90px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="fecha_registro" HeaderText="Fecha de Creaci&#243;n" ReadOnly="True">
<ItemStyle HorizontalAlign="Center" Width="220px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="usuario_registra" HeaderText="Usuario Crea" ReadOnly="True">
<ItemStyle HorizontalAlign="Left" Width="120px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="Gestor Asignado"><EditItemTemplate>
                <asp:DropDownList ID="ddlGestor" runat="server"></asp:DropDownList>           
</EditItemTemplate>
<ItemTemplate>
             <asp:Label ID="lblGestor" runat="server" Text='<%# Bind("gestor") %>'></asp:Label>
             <asp:HiddenField ID="hdnCodGestor" Value='<%# Bind("id_gestor") %>' runat="server"></asp:HiddenField>
            
</ItemTemplate>

<ItemStyle HorizontalAlign="Left" Width="240px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField><EditItemTemplate>
               <asp:Button Text="Asignar" CommandName="Update" CausesValidation="true" runat="server" ID="btUpdate" />&nbsp;
               <asp:Button Text="Cancelar" CommandName="Cancel" CausesValidation="false" runat="server" ID="btCancel" />
            
</EditItemTemplate>
<ItemTemplate>
               <asp:Button Text="Editar" CommandName="Edit" CausesValidation="false" runat="server" ID="btEdit" />&nbsp;
            
</ItemTemplate>
</asp:TemplateField>
</Columns>

<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

<PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

<HeaderStyle HorizontalAlign="Center" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#999999"></EditRowStyle>

<AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
</asp:GridView> <BR />

<asp:GridView id="gvErrores" runat="server" Visible="False" ForeColor="#333333" HorizontalAlign="Center" GridLines="None" CellPadding="4" Caption="Errores al asignar automotores" CaptionAlign="Top">
<RowStyle BackColor="#FFFBD6" ForeColor="#333333"></RowStyle>
<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></FooterStyle>
<PagerStyle HorizontalAlign="Center" BackColor="#FFCC66" ForeColor="#333333"></PagerStyle>
<SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy"></SelectedRowStyle>
<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></HeaderStyle>
<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
</asp:GridView> <BR />
<DIV align=center>
<asp:LinkButton id="btnVerErrores" onclick="btnVerErrores_Click" runat="server" Visible="False">Ver errores</asp:LinkButton> 
<asp:LinkButton id="btnHideErrores" onclick="btnHideErrores_Click" runat="server" Visible="False">Ocultar errores</asp:LinkButton>&nbsp;<BR /><BR />
&nbsp; </DIV>
<DIV id="divError" runat="server" visible="false">
<TABLE class="error2" width=480 align=center>
<TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/error.gif" /></TD><TD><B>Error:</B><BR /><asp:Label id="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR>
</TBODY></TABLE></DIV>
<DIV id="divWarning" runat="server" visible="false">
<TABLE class="warning" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/warning.gif" /></TD><TD><asp:Label id="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR>
</TBODY></TABLE></DIV>
</div>
</ContentTemplate>
     </asp:UpdatePanel>
   </form>   
 </div>
</asp:Content>

