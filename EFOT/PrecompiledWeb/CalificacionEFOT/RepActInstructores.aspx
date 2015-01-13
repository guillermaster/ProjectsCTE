<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="RepActInstructores, App_Web_repactinstructores.aspx.cdcab7d2" theme="efot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Panel ID="pnlForm" runat="server">
    <asp:Label ID="lblTipoActividad" runat="server" Text="Tipo de Actividad:"></asp:Label>
    <asp:DropDownList ID="ddlTipoActividad" runat="server" AutoPostBack="true" 
            onselectedindexchanged="ddlTipoActividad_SelectedIndexChanged">
    </asp:DropDownList>
    <br />
    <asp:Label ID="lblActividad" runat="server" Text="Actividad:" SkinID="Small" />
    <asp:DropDownList ID="ddlActividad" runat="server" />
    <asp:Label ID="lblEstado" runat="server" Text="Estado:" SkinID="Small" />
    <asp:DropDownList ID="ddlEstado" runat="server">
        <asp:ListItem Selected="True" Value="S">Idóneo</asp:ListItem>
        <asp:ListItem Value="N">No Idóneo</asp:ListItem>
    </asp:DropDownList>
    <br />
    <asp:Label ID="lblFecDesde" runat="server" Text="Desde:" SkinID="Small"></asp:Label>
    <asp:TextBox ID="txtFecDesde" runat="server" SkinID="skinMedium"></asp:TextBox>
    <asp:CalendarExtender ID="txtFecDesde_CalendarExtender" runat="server" TargetControlID="txtFecDesde">
                </asp:CalendarExtender>
    <asp:RequiredFieldValidator ID="reqValFecDesde" runat="server" ControlToValidate="txtFecDesde" ErrorMessage="*"></asp:RequiredFieldValidator>

    <asp:Label ID="lblFecHasta" runat="server" Text="Hasta:" SkinID="Small"></asp:Label>
    <asp:TextBox ID="txtFecHasta" runat="server" SkinID="skinMedium"></asp:TextBox>
    <asp:CalendarExtender ID="txtFecHasta_CalendarExtender" runat="server" TargetControlID="txtFecHasta">
                </asp:CalendarExtender>
    <asp:RequiredFieldValidator ID="reqValFecHasta" runat="server" ControlToValidate="txtFecHasta" ErrorMessage="*"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="lblInstructor" runat="server" Text="Usuario de instructor:"></asp:Label>
    <asp:DropDownList ID="ddlInstructores" runat="server">
    </asp:DropDownList><br />
    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" 
        onclick="btnConsultar_Click" />
    <br /><br />
    <asp:Label ID="lblTotal" runat="server" Text="" SkinID="Large" />
    <asp:GridView ID="gvActividades" runat="server" CellPadding="4" ForeColor="#333333" 
            GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
</asp:Panel>
<asp:Label ID="lblError" runat="server" Text="" Font-Size="Medium" ForeColor="Red" />
</asp:Content>

