<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsModeloAutomotorLista.aspx.cs" Inherits="ConsModeloAutomotorLista" %>

<%@ Register Src="UserControls/NewButton.ascx" TagName="NewButton" TagPrefix="uc1" %>
<%@ Register Src="UserControls/PrintButton.ascx" TagName="PrintButton" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript">
    // Let's use a lowercase function name to keep with JavaScript conventions
    function selectAll() {
        // Since ASP.NET checkboxes are really HTML input elements
        //  let's get all the inputs 
        var inputElements = document.getElementsByTagName('input');
        var btnAprob = document.getElementById('ctl00_ContentPlaceHolder1_btnAprobar');
        btnAprob.style.visibility = "hidden";
        var btnReprob = document.getElementById('ctl00_ContentPlaceHolder1_btnReprobar');
        btnReprob.style.visibility = "hidden";
 
        for (var i = 0 ; i < inputElements.length ; i++) {
            var myElement = inputElements[i];
            // Filter through the input types looking for checkboxes
            if (myElement.type === "checkbox") {
 
               // Use the invoker (our calling element) as the reference 
               //  for our checkbox status
                if(myElement.checked)
                {
                    btnAprob.style.visibility = "visible";
                    btnReprob.style.visibility = "visible";
                    break;
                }
            }
        }
    } 
</script>
<div class="main-content1" align="center">
<uc1:NewButton ID="btnNew" runat="server" />
    <uc1:PrintButton ID="btnPrint" runat="server" />
    
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
   <ContentTemplate>
<DIV style="MARGIN: -34px 0px 2px 250px; height: 32px;"><asp:ImageButton style="VISIBILITY: hidden" id="btnAprobar" onclick="btnAprobar_Click" runat="server" ImageUrl="~/img/btnAprobar.gif"></asp:ImageButton> <asp:ImageButton style="VISIBILITY: hidden" id="btnReprobar" onclick="btnReprobar_Click" runat="server" ImageUrl="~/img/btnReprobar.gif"></asp:ImageButton> </DIV><DIV id="divContent"><asp:UpdateProgress id="UpdateProgress1" runat="server" DynamicLayout="False" DisplayAfter="5" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
             <div align="center">
                <img src="img/ajax-loader.gif" />
              </div>
        </ProgressTemplate>
        </asp:UpdateProgress> <DIV style="WIDTH: 779px" class="registerform"><asp:Label id="lblCriterio" runat="server" Text="Criterio de Búsqueda:"></asp:Label> <asp:DropDownList id="ddlCriterio" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCriterio_SelectedIndexChanged">
        <asp:ListItem Selected="True">-- Seleccione --</asp:ListItem>
        <asp:ListItem Value="TODOS_ORD">Todos (ordenados alfab&#233;ticamente)</asp:ListItem>
        <asp:ListItem Value="TODOS_ORD_COD">Todos (ordenados por c&#243;digo)</asp:ListItem>
        <asp:ListItem Value="ESTADO">Estado</asp:ListItem>
        <asp:ListItem Value="COLOR">Color</asp:ListItem>
        <asp:ListItem Value="MARCA">Marca</asp:ListItem>
        <asp:ListItem Value="MODELO">Modelo</asp:ListItem>
    </asp:DropDownList> &nbsp;&nbsp;<asp:Button id="btnConsultar" onclick="btnConsultar_Click1" runat="server" Text="Buscar" CssClass="button_horiz"></asp:Button> <BR /><BR /><asp:Label id="lblEstado" runat="server" Text="Estado:" Visible="False"></asp:Label> <asp:RequiredFieldValidator id="reqValEstado" runat="server" ErrorMessage="*" __designer:wfdid="w6" ControlToValidate="ddlEstado"></asp:RequiredFieldValidator> <asp:DropDownList id="ddlEstado" runat="server" Visible="False"></asp:DropDownList> <asp:Label id="lblAnio" runat="server" Text="Año:" Visible="False"></asp:Label> <asp:RegularExpressionValidator id="regExpValAnio" runat="server" ErrorMessage="*" __designer:wfdid="w10" ControlToValidate="txtAnio" ValidationExpression="(19|20)\d\d"></asp:RegularExpressionValidator> <asp:TextBox id="txtAnio" runat="server" Visible="False" AutoPostBack="True" MaxLength="4" Width="40px" OnTextChanged="txtAnio_TextChanged"></asp:TextBox> <asp:Label id="lblMarca" runat="server" Text="Marca:" Visible="False"></asp:Label> <asp:RequiredFieldValidator id="reqValMarca" runat="server" ErrorMessage="*" __designer:wfdid="w7" ControlToValidate="ddlMarca"></asp:RequiredFieldValidator> <asp:DropDownList id="ddlMarca" runat="server" Visible="False" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged"></asp:DropDownList> <asp:Label id="lblModelo" runat="server" Text="Modelo:" Visible="False"></asp:Label> <asp:RequiredFieldValidator id="reqValModelo" runat="server" ErrorMessage="*" __designer:wfdid="w8" ControlToValidate="ddlModelo"></asp:RequiredFieldValidator> <asp:DropDownList id="ddlModelo" runat="server" Visible="False"></asp:DropDownList> <asp:Label id="lblColor" runat="server" Text="Color:" Visible="False"></asp:Label> <asp:RequiredFieldValidator id="reqValColor" runat="server" ErrorMessage="*" __designer:wfdid="w9" ControlToValidate="ddlColor"></asp:RequiredFieldValidator> <asp:DropDownList id="ddlColor" runat="server" Visible="False"></asp:DropDownList> </DIV><BR /><asp:GridView id="gvModelosAutomotores" runat="server" CssClass="mGrid" OnSelectedIndexChanged="gvModelosAutomotores_SelectedIndexChanged" Width="597px" EmptyDataText="No existen automotores correspondientes al criterio de búsqueda ingresado" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" AutoGenerateColumns="False">
<RowStyle HorizontalAlign="Center"></RowStyle>
<Columns>
<asp:BoundField DataField="ID" HeaderText="C&#243;digo">
<ItemStyle Width="40px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Marca" HeaderText="Marca">
<ItemStyle HorizontalAlign="Left" Width="520px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="Color" HeaderText="Color">
<ItemStyle HorizontalAlign="Left" Width="125px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="A&#241;o de Producci&#243;n" HeaderText="A&#241;o de producci&#243;n">
<ItemStyle HorizontalAlign="Center" Width="180px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="estado" HeaderText="Estado">
<ItemStyle HorizontalAlign="Left" Width="140px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="">
 <ItemTemplate>
    <asp:ImageButton id="btnVerFoto" onclick="btnVerFoto_Click" runat="server" ImageUrl="~/img/iPhoto.png" />
    <cc1:ModalPopupExtender id="mdlPopup" runat="server" TargetControlID="btnVerFoto" PopupControlID="pnlFotoModeloAutomotor" CancelControlID="imgFotoModeloAutomotor" BackgroundCssClass="modalBackground" /> 
    <asp:Panel ID="pnlFotoModeloAutomotor" runat="server" Style="display: none;">
                <asp:UpdatePanel ID="upFotoModeloAutomotor" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div align="center">
                         <asp:ImageButton ID="imgFotoModeloAutomotor" ImageUrl='<%# this.GetFotoUrl( Eval("ID") as string ) %>' runat="server" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:Panel>
 </ItemTemplate>
</asp:TemplateField>
<asp:CommandField SelectImageUrl="~/img/ico_details.gif" SelectText="" ShowSelectButton="True" ButtonType="Image"></asp:CommandField>
<asp:TemplateField>
  <ItemTemplate>
    <asp:CheckBox ID="chkAprobac" runat="server" OnClick="selectAll()"  visible='<%# this.CheckboxVisibility( Eval("estado") as string ) %>' />
  </ItemTemplate>
</asp:TemplateField>
<asp:TemplateField>
   <HeaderTemplate>
    <asp:Label ID="lblTitleObs" runat="server"  visible='<%# this.CheckboxVisibility( Eval("estado") as string ) %>' Text="Observación" />
   </HeaderTemplate>
   <ItemTemplate>
     <asp:TextBox ID="txtObservacRechazo" maxlength="500" style='width:180px;' runat="server" visible='<%# this.CheckboxVisibility( Eval("estado") as string ) %>'></asp:TextBox>
   </ItemTemplate>
</asp:TemplateField>
</Columns>

<PagerStyle CssClass="pgr"></PagerStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
</asp:GridView> 
<DIV id="divError" runat="server" visible="false"><TABLE class="error2" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/error.gif" /></TD><TD><B>Error:</B><BR /><asp:Label id="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR><TR><TD style="HEIGHT: 13px">&nbsp;</TD><TD style="HEIGHT: 13px"><asp:LinkButton id="btnVerErrores" runat="server" Visible="False" OnClick="btnVerErrores_Click">Ver errores</asp:LinkButton> <asp:LinkButton id="btnHideErrores" runat="server" Visible="False" OnClick="btnHideErrores_Click">Ocultar errores</asp:LinkButton></TD></TR></TBODY></TABLE></DIV><DIV id="divWarning" runat="server" visible="false"><TABLE class="warning" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/warning.gif" /></TD><TD><asp:Label id="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV>
<asp:GridView id="gvErrores" runat="server" Visible="False" ForeColor="#333333" CaptionAlign="Top" Caption="Errores al aprobar automotores" CellPadding="4" GridLines="None" HorizontalAlign="Center">
<RowStyle BackColor="#FFFBD6" ForeColor="#333333"></RowStyle>
<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></FooterStyle>
<PagerStyle HorizontalAlign="Center" BackColor="#FFCC66" ForeColor="#333333"></PagerStyle>
<SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy"></SelectedRowStyle>
<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></HeaderStyle>
<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
</asp:GridView>
</DIV>
</ContentTemplate>
  </asp:UpdatePanel>
 </div>
</asp:Content>

