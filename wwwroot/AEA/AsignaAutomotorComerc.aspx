<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AsignaAutomotorComerc.aspx.cs" Inherits="AsignaAutomotorComerc" %>

<%@ Register Src="UserControls/SaveButton.ascx" TagName="SaveButton" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main-content1" align="center">
        <form id="regForm">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
              <ContentTemplate>
<uc1:SaveButton id="btnSave" runat="server" Visible="false" OnLoad="btnSave_Load"></uc1:SaveButton> <asp:UpdateProgress id="UpdateProgress1" runat="server" DynamicLayout="False" DisplayAfter="5" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
             <div align="center">
                <img src="img/ajax-loader.gif" />
              </div>
        </ProgressTemplate>
        </asp:UpdateProgress> <DIV style="WIDTH: 779px" id="divBusqueda" class="registerform" runat="server"><asp:Label id="lblMatriz" runat="server" Text="Matriz de Comercializadora:"></asp:Label> <asp:DropDownList id="ddlMatriz" runat="server" OnSelectedIndexChanged="ddlMatriz_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label id="lblSucursal" runat="server" Text="Sucursal:"></asp:Label> <asp:DropDownList id="ddlSucursales" runat="server" OnSelectedIndexChanged="ddlSucursales_SelectedIndexChanged" AutoPostBack="True" Enabled="False"></asp:DropDownList><BR /><asp:Label id="lblAnioProd" runat="server" Text="Año de Producción:"></asp:Label> <asp:TextBox id="txtAnioProd" runat="server" AutoPostBack="True" Enabled="False" OnTextChanged="txtAnioProd_TextChanged" Width="59px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label id="lblMarca" runat="server" Text="Marca:"></asp:Label> <asp:DropDownList id="ddlMarca" runat="server" OnSelectedIndexChanged="ddlMarca_SelectedIndexChanged" AutoPostBack="True" Enabled="False"></asp:DropDownList> </DIV><BR /><BR /><asp:GridView id="gvModelos" runat="server" ForeColor="#333333" HorizontalAlign="Center" AutoGenerateColumns="False" CellPadding="4" GridLines="None" EmptyDataText="No existen automotores por asignar según los criterios seleccionados">
<RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
<Columns>
<asp:TemplateField><ItemTemplate>
            <asp:CheckBox ID="chkSeleccion" runat="server"></asp:CheckBox>
        <asp:HiddenField ID="hdnCodModelo" Value='<%# Bind("codigo_modelo") %>' runat="server"></asp:HiddenField>
        <asp:HiddenField ID="hdnCodColor" Value='<%# Bind("id_dominio_dato") %>' runat="server"></asp:HiddenField>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="descripcion" HeaderText="Modelo">
    <HeaderStyle HorizontalAlign="Left" />
    <ItemStyle HorizontalAlign="Left" Width="320px" />
</asp:BoundField>
<asp:BoundField DataField="valor" HeaderText="Color">
    <HeaderStyle HorizontalAlign="Left" />
    <ItemStyle HorizontalAlign="Left" Width="120px" />
</asp:BoundField>
</Columns>

<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>

<PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>

<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

<HeaderStyle HorizontalAlign="Center" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>

<EditRowStyle BackColor="#999999"></EditRowStyle>

<AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
</asp:GridView><BR /><BR /><asp:GridView id="gvErrores" runat="server" Visible="False" ForeColor="#333333" HorizontalAlign="Center" CellPadding="4" GridLines="None" Caption="Errores al asignar automotores" CaptionAlign="Top">
<RowStyle BackColor="#FFFBD6" ForeColor="#333333"></RowStyle>

<FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></FooterStyle>

<PagerStyle HorizontalAlign="Center" BackColor="#FFCC66" ForeColor="#333333"></PagerStyle>

<SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy"></SelectedRowStyle>

<HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White"></HeaderStyle>

<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
</asp:GridView> <BR /><DIV align=center><asp:LinkButton id="btnVerErrores" onclick="btnVerErrores_Click" runat="server" Visible="False">Ver errores</asp:LinkButton> <asp:LinkButton id="btnHideErrores" onclick="btnHideErrores_Click" runat="server" Visible="False">Ocultar errores</asp:LinkButton>&nbsp;<BR /><BR />  </DIV><DIV id="divError" runat="server" visible="false"><TABLE class="error2" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/error.gif" /></TD><TD><B>Error:</B><BR /><asp:Label id="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV><DIV id="divWarning" runat="server" visible="false"><TABLE class="warning" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/warning.gif" /></TD><TD><asp:Label id="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV>
</ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </div>
</asp:Content>
