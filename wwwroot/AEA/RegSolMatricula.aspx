<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RegSolMatricula.aspx.cs" Inherits="RegistroComercializadora" %>

<%@ Register Src="UserControls/SaveButton.ascx" TagName="SaveButton" TagPrefix="uc1" %>

<%@ Register Src="UserControls/BackButton.ascx" TagName="BackButton" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="main-content1" align="center">
  
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
    <uc1:BackButton ID="btnBack" Visible="false" runat="server" />
      <uc1:SaveButton ID="btnSave" Visible="false" runat="server" OnLoad="btnSave_Load" />
      <br /><br />
<!--
<asp:Label id="lblGestores" runat="server" Text="Gestor:" CssClass="label"></asp:Label> <BR />
<asp:DropDownList id="ddlGestores" runat="server" CssClass="field" Width="280px">
        </asp:DropDownList>
        
        --><asp:Label id="lblSubtitle" runat="server" Text="Ingrese los datos solicitados:" CssClass="h2"></asp:Label> <asp:UpdateProgress id="UpdateProgress1" runat="server" DynamicLayout="False" DisplayAfter="5" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
             <div align="center">
                <img src="img/ajax-loader.gif" />
              </div>
        </ProgressTemplate>
        </asp:UpdateProgress> <DIV style="WIDTH: 593px" id="divForms" class="registerform" runat="server"><asp:HiddenField id="hdnGridViewEditIndex" runat="server"></asp:HiddenField> <asp:GridView id="GridView1" runat="server" ForeColor="#333333" HorizontalAlign="Center" Width="579px" GridLines="None" CellPadding="4" AutoGenerateColumns="False" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowDeleting="GridView1_RowDeleting" OnRowCommand="GridView1_RowCommand1" ShowFooter="True">
<RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
<Columns>
<asp:TemplateField><EditItemTemplate>
               <asp:ImageButton CommandName="Update" CausesValidation="true" runat="server" ID="btUpdate" ImageURL="~/img/ico_det_update.gif"></asp:ImageButton>
               <asp:ImageButton CommandName="Cancel" CausesValidation="false" runat="server" ID="btCancel" ImageURL="~/img/ico_det_cancel.gif"></asp:ImageButton>
</EditItemTemplate>
<FooterTemplate>
               <asp:ImageButton CommandName="Insert" CausesValidation="true" runat="server" ID="btInsert" Visible="false" ImageURL="~/img/ico_det_insertar.gif"></asp:ImageButton>
</FooterTemplate>
<ItemTemplate>
               <asp:ImageButton CommandName="Edit" CausesValidation="false" runat="server" ID="btEdit" ImageURL="~/img/ico_edit.gif"></asp:ImageButton>
               <asp:ImageButton CommandName="Delete" CausesValidation="false" runat="server" ID="btDelete" ImageURL="~/img/ico_delete.gif"></asp:ImageButton>
            
</ItemTemplate>

<FooterStyle HorizontalAlign="Center" VerticalAlign="Middle"></FooterStyle>

<ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="RAMV"><EditItemTemplate>
                  <asp:TextBox ID="tbRAMV" runat="server" Text='<%# Bind("RAMV") %>' AutoPostBack="True" OnTextChanged="txtEditRAMV_TextChanged"></asp:TextBox>
            
</EditItemTemplate>
<FooterTemplate>
                  <asp:TextBox ID="txtInsertRAMV" runat="server" Text=""  AutoPostBack="True" OnTextChanged="txtInsertRAMV_TextChanged"></asp:TextBox>
            
</FooterTemplate>
<ItemTemplate>
               <asp:Label ID="lblRAMV" Text='<%# Bind("RAMV") %>' runat="server"></asp:Label>
            
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Automotor"><EditItemTemplate>
                  <asp:Label ID="lblAutomotorEdit" Text='<%# Eval("Automotor") %>' runat="server"></asp:Label>
            
</EditItemTemplate>
<FooterTemplate>
                  <asp:Label ID="lblAutomotorInsert" runat="server"></asp:Label>
            
</FooterTemplate>
<ItemTemplate>
               <asp:Label ID="lblAutomotor" Text='<%# Eval("Automotor") %>' runat="server"></asp:Label>
            
</ItemTemplate>

<ItemStyle Width="420px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField><EditItemTemplate>
                  <asp:HiddenField ID="hdnAutomotorAnioEdit" Value='<%# Eval("Anio") %>' runat="server"></asp:HiddenField>
            
</EditItemTemplate>
<FooterTemplate>
                  <asp:HiddenField ID="hdnAutomotorAnioInsert" Value="" runat="server"></asp:HiddenField>
            
</FooterTemplate>
<ItemTemplate>
               <asp:HiddenField ID="hdnAutomotorAnio" Value='<%# Eval("Anio") %>' runat="server"></asp:HiddenField>
            
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField><EditItemTemplate>
                  <asp:HiddenField ID="hdnAutomotorMarcaEdit" Value='<%# Eval("Marca") %>' runat="server"></asp:HiddenField>
            
</EditItemTemplate>
<FooterTemplate>
                  <asp:HiddenField ID="hdnAutomotorMarcaInsert" Value="" runat="server"></asp:HiddenField>
            
</FooterTemplate>
<ItemTemplate>
               <asp:HiddenField ID="hdnAutomotorMarca" Value='<%# Eval("Marca") %>' runat="server"></asp:HiddenField>
            
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField><EditItemTemplate>
                  <asp:HiddenField ID="hdnAutomotorModeloEdit" Value='<%# Eval("Modelo") %>' runat="server"></asp:HiddenField>
            
</EditItemTemplate>
<FooterTemplate>
                  <asp:HiddenField ID="hdnAutomotorModeloInsert" Value="" runat="server"></asp:HiddenField>
            
</FooterTemplate>
<ItemTemplate>
               <asp:HiddenField ID="hdnAutomotorModelo" Value='<%# Eval("Modelo") %>' runat="server"></asp:HiddenField>
            
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Color"><EditItemTemplate>
                  <asp:DropDownList ID="ddlAutomotorColorEdit" runat="server"></asp:DropDownList>
            
</EditItemTemplate>
<FooterTemplate>
                  <asp:DropDownList ID="ddlAutomotorColorInsert" visible="false" runat="server"></asp:DropDownList>
            
</FooterTemplate>
<ItemTemplate>
                  <asp:Label ID="lblAutomotorDescColor" Text='<%# Eval("DescColor") %>' runat="server"></asp:Label>
                  <asp:HiddenField ID="hdnAutomotorCodColor" Value='<%# Eval("CodColor") %>' runat="server"></asp:HiddenField>
            
</ItemTemplate>

<ItemStyle Width="180px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Gravamen"><EditItemTemplate>
                <asp:DropDownList ID="ddlGravamen" runat="server">
                </asp:DropDownList>
            
</EditItemTemplate>
<FooterTemplate>
                  <asp:DropDownList ID="ddlInsertAutomotor" visible="false" runat="server">
                </asp:DropDownList>
            
</FooterTemplate>
<ItemTemplate>
               <asp:Label ID="lblGravamen" Text='<%# Eval("Gravamen") %>' runat="server"></asp:Label>
            
</ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField><EditItemTemplate>
                  <asp:HiddenField ID="hdnCodComercEdit" Value='<%# Eval("Comercializadora") %>' runat="server"></asp:HiddenField>
            
</EditItemTemplate>
<FooterTemplate>
                  <asp:HiddenField ID="hdnCodComercInsert" Value="" runat="server"></asp:HiddenField>
            
</FooterTemplate>
<ItemTemplate>
               <asp:HiddenField ID="hdnCodComerc" Value='<%# Eval("Comercializadora") %>' runat="server"></asp:HiddenField>
            
</ItemTemplate>
</asp:TemplateField>
</Columns>

<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

<PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>
<EmptyDataTemplate>
           <table>
             <tr>
               <th>RAMV</th>
               <th><asp:Label ID="lblEmptyModelo" runat="server" Text="Modelo" Visible="False"></asp:Label></th>
               <th><asp:Label ID="lblEmptyColor" runat="server" Text="Color" Visible="False" /></th>
               <th><asp:Label ID="lblEmptyGravamen" runat="server" Text="Gravamen" Visible="False" /></th>
               <th>&nbsp;</th>
             </tr>
             <tr>
               <td><asp:TextBox ID="txtEmptyInsertRAMV" AutoPostBack="True" OnTextChanged="txtEmptyInsertRAMV_TextChanged" runat="server"></asp:TextBox></td>
               <td><asp:Label ID="lblEmptyDescModelo" runat="server" Text="" Visible="False"></asp:Label>
               <asp:HiddenField ID="hdnEmptyCodMarca" runat="server" />
               <asp:HiddenField ID="hdnEmptyCodModelo" runat="server" />
               <asp:HiddenField ID="hdnEmptyAnio" runat="server" />
               <asp:HiddenField ID="hdnEmptyCodComerc" runat="server" /></td>
               <td><asp:DropDownList ID="ddlEmptyCodColor" runat="server" Visible="False">
                </asp:DropDownList></td>
               <td><asp:DropDownList ID="ddlEmptyInsertAutomotor" Visible="False" runat="server">
                </asp:DropDownList></td>
               <td>
               <asp:ImageButton CommandName="EmptyInsert" CausesValidation="true" Visible="False" runat="server" ID="btSend" ImageURL="~/img/ico_det_insertar.gif"></asp:ImageButton></td>
             </tr>
           </table>               
            
</EmptyDataTemplate>

<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#999999"></EditRowStyle>

<AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
</asp:GridView> <BR /><BR /><DIV align=center>&nbsp;&nbsp;</DIV></DIV><DIV id="divError" runat="server" visible="false"><TABLE class="error2" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/error.gif" /></TD><TD><B>Error:</B><BR /><asp:Label id="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV><DIV id="divWarning" runat="server" visible="false"><TABLE class="warning" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/warning.gif" /></TD><TD><asp:Label id="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV><asp:GridView id="gvDetallesErrores" runat="server" Visible="False" ForeColor="#333333" HorizontalAlign="Center" GridLines="None" CellPadding="4">
<RowStyle BackColor="#FFFBD6" ForeColor="#333333"></RowStyle>

<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></FooterStyle>

<PagerStyle HorizontalAlign="Center" BackColor="#FFCC66" ForeColor="#333333"></PagerStyle>

<SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy"></SelectedRowStyle>

<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></HeaderStyle>

<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
</asp:GridView> 
</ContentTemplate>
   </asp:UpdatePanel>
  </div>
</asp:Content>