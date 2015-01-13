<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ConsSolMatricula.aspx.cs" Inherits="ConsSolMatricula" Title="Comisión de Tránsito de la Provincia del Guayas" %>

<%@ Register Src="UserControls/PrintButton.ascx" TagName="PrintButton" TagPrefix="uc1" %>

<%@ Register Src="UserControls/BackButton.ascx" TagName="BackButton" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main-content1">
        <uc1:BackButton ID="btnBack" Visible="false" runat="server" />
        <uc1:PrintButton ID="btnPrint" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <contenttemplate>
<asp:HiddenField id="hdnSelDet" runat="server"></asp:HiddenField> <asp:HiddenField id="hdnCodSolicitud" runat="server" __designer:wfdid="w16"></asp:HiddenField> <DIV id="divContent"><BR /><asp:Label id="lblPageTitle" runat="server" Text="" CssClass="h2" Font-Bold="true"></asp:Label><BR /><br /><asp:Label id="lblPageSubtitle" runat="server" Text="" CssClass="h3"></asp:Label><BR /><br /><TABLE width=580><TBODY><TR><TD width="10%"><B>Comercializadora:</B></TD><TD width="40%"><asp:Label id="lblComerc" runat="server" Text=""></asp:Label></TD><TD width="10%"><B>Sucursal:</B></TD><TD width="40%"><asp:Label id="lblSucursal" runat="server" Text=""></asp:Label></TD></TR><TR><TD><B>Gestor:</B></TD><TD colSpan=3><asp:Label id="lblGestor" runat="server" Text=""></asp:Label></TD></TR></TBODY></TABLE><BR /><BR /><asp:GridView id="gvDetallesSolicitud" runat="server" CssClass="mGrid" AlternatingRowStyle-CssClass="alt" PagerStyle-CssClass="pgr" onrowdatabound="gvDetallesSolicitudGridView_RowDataBound" OnSelectedIndexChanged="gvDetallesSolicitud_SelectedIndexChanged" AutoGenerateColumns="False" Width="652px"><Columns>
<asp:BoundField DataField="ramv" HeaderText="RAMV">
<ItemStyle Width="160px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="fecha_creacion" HeaderText="Fecha de Creaci&#243;n">
<ItemStyle Width="150px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="gravamen" HeaderText="Gravamen">
<HeaderStyle HorizontalAlign="Center"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="estado" HeaderText="Estado">
<ItemStyle Width="100px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField><ItemTemplate>
        <asp:HiddenField ID="hdnIdDetalleSol" Value='<%# Bind("id_det_solicitud") %>' runat="server" />
           <asp:HiddenField ID="hdnFechaProceso" Value='<%# Bind("fecha_proceso") %>' runat="server" />
           <asp:HiddenField ID="hdnObservacion" Value='<%# Bind("observacion") %>' runat="server" />
           <asp:HiddenField ID="hdnIdTramite" Value='<%# Bind("id_tramite") %>' runat="server" />             
</ItemTemplate>

<ItemStyle Width="1px"></ItemStyle>
</asp:TemplateField>
<asp:CommandField CausesValidation="False" SelectImageUrl="~/img/ico_details.gif" ShowSelectButton="True" ButtonType="Image">
<ItemStyle Width="18px"></ItemStyle>
</asp:CommandField>
<asp:TemplateField><ItemTemplate>
        <asp:ImageButton ID="btnDelete" runat="server" OnClientClick="return confirm('¿Está seguro que desea eliminar el automotor de la solicitud?');" OnClick="gvDetalles_Delete" ImageUrl="~/img/ico_delete.gif" ToolTip="Elimina automotor de esta solicitud" />            
    
</ItemTemplate>

<ItemStyle Width="18px"></ItemStyle>
</asp:TemplateField>
</Columns>

<PagerStyle CssClass="pgr"></PagerStyle>

<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
</asp:GridView> <asp:Panel style="PADDING-RIGHT: 4px; PADDING-LEFT: 4px; LEFT: 700px; PADDING-BOTTOM: 4px; PADDING-TOP: 4px" id="Panel1" runat="server" Visible="false" BackColor="Beige" Width="330px" BorderWidth="1px" BorderStyle="Solid" BorderColor="#FF8000">
                    <div align="center">
                        <asp:DetailsView ID="detviewSolicitud" runat="server" Height="50px" CellPadding="4" Width="320px" ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <CommandRowStyle BackColor="#C5BBAF" Font-Bold="True" />
                            <RowStyle BackColor="#E3EAEB" HorizontalAlign="left" Width="180px" />
                            <FieldHeaderStyle BackColor="#D0D0D0" Font-Bold="True" HorizontalAlign="left" Width="140px" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#7C6F57" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:DetailsView>
                        <asp:ImageButton ID="btnCloseDet" runat="server" ImageUrl="~/img/ico_close.gif" OnClick="btnCloseDet_Click" />
                    </div>
                </asp:Panel> <cc1:AlwaysVisibleControlExtender id="AlwaysVisibleControlExtender1" runat="server" HorizontalSide="Center" VerticalSide="Middle" TargetControlID="Panel1">
                </cc1:AlwaysVisibleControlExtender> <DIV id="divDesactiva" runat="server" visible="false"><B>RAMV:</B>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Label id="lblRAMV" runat="server" Text=""></asp:Label> <BR /><BR /><B>Observación:</B> <asp:TextBox id="txtObservacion" runat="server" Width="318px" TextMode="MultiLine" Rows="3"></asp:TextBox><BR /><BR /><asp:Button id="btnDesacitvar" onclick="btnDesacitvar_Click" runat="server" Text="Desactivar" CssClass="button" Width="93px"></asp:Button> <asp:Button id="btnDesacCancelar" onclick="btnDesacCancelar_Click" runat="server" Text="Cancelar" CausesValidation="False" CssClass="button" Width="80px"></asp:Button> </DIV><DIV id="divError" runat="server" visible="false"><TABLE class="error2" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/error.gif" /></TD><TD><B>Error:</B><BR /><asp:Label id="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV><DIV id="divWarning" runat="server" visible="false"><TABLE class="warning" width=480 align=center><TBODY><TR><TD style="WIDTH: 54px"><IMG src="img/warning.gif" /></TD><TD><asp:Label id="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></TD></TR></TBODY></TABLE></DIV></DIV>
</contenttemplate>
       </asp:UpdatePanel>
    </div>
</asp:Content>

