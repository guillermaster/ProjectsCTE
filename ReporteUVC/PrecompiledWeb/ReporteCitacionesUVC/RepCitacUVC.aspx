<%@ page language="C#" masterpagefile="~/MasterPage.master" autoeventwireup="true" inherits="RepCitacUVC, App_Web_repcitacuvc.aspx.cdcab7d2" theme="SkinFile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="Server">
    

    <script type="text/javascript" src="js/piechart.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
	        $('#<%= txtFechaDesde.ClientID %>').DatePicker({
		        format:'d/m/Y',
		        date: $('#<%= txtFechaDesde.ClientID %>').val(),
		        current: $('#<%= txtFechaDesde.ClientID %>').val(),
		        starts: 1,
		        position: 'bottom',
		        onBeforeShow: function(){
    			    $('#<%= txtFechaDesde.ClientID %>').DatePickerSetDate($('#<%= txtFechaDesde.ClientID %>').val(), true);
		        },
		        onChange: function(formated, dates){
			        $('#<%= txtFechaDesde.ClientID %>').val(formated);
			        $('#<%= txtFechaDesde.ClientID %>').DatePickerHide();
		        }
	        });
	    });
	    $(document).ready(function () {
	        $('#<%= txtFechaHasta.ClientID %>').DatePicker({
		        format:'d/m/Y',
		        date: $('#<%= txtFechaHasta.ClientID %>').val(),
		        current: $('#<%= txtFechaHasta.ClientID %>').val(),
		        starts: 1,
		        position: 'bottom',
		        onBeforeShow: function(){
    			    $('#<%= txtFechaHasta.ClientID %>').DatePickerSetDate($('#<%= txtFechaHasta.ClientID %>').val(), true);
		        },
		        onChange: function(formated, dates){
			        $('#<%= txtFechaHasta.ClientID %>').val(formated);
			        $('#<%= txtFechaHasta.ClientID %>').DatePickerHide();
		        }
	        });
	    });
        function ToggleGrid(gridID, titleID)
        {
            $("#<%= lblTabTitle.ClientID %>").text($("#ctl00_ContentPlaceHolderBody_"+titleID).text());
            $(".mGrid").hide();
            $("#"+gridID).toggle();
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <h1>
        Reporte de Citaciones</h1>
    <div id="block-m-gray">
        Fechas de consulta:
        <asp:Label ID="lblFechaDesde" runat="server" Text="Desde"></asp:Label>
        <asp:TextBox ID="txtFechaDesde"  CssClass="inputDate" runat="server"></asp:TextBox>
        <asp:Label ID="lblFechaHasta" runat="server" Text="Hasta"></asp:Label>
        <asp:TextBox ID="txtFechaHasta" CssClass="inputDate" runat="server"></asp:TextBox>
        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
    </div>
    
    <div style="width: 100%; float: left;">
        <div id="aux">
        </div>
        <canvas id="chart" width="600" height="500"></canvas>
        <asp:GridView ID="gvChartData" CssClass="chartData" OnRowDataBound="GridView_RowDataBound"
            runat="server" ShowFooter="true" ShowHeader="true" AutoGenerateColumns="false">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Delegación
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("Delegación").ToString() %>
                    </ItemTemplate>
                    <FooterTemplate>
                        TOTAL:
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        No.Citaciones
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("Citaciones").ToString()%>
                    </ItemTemplate>
                    <FooterTemplate>
                        <%# TotalCitaciones%>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        %
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("%").ToString() %>
                    </ItemTemplate>
                    <FooterTemplate>
                        100
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        No.UVC
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%# Eval("Vehículos").ToString()%>
                    </ItemTemplate>
                    <FooterTemplate>
                        <%# TotalUVC%>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    
    <asp:TextBox ID="hdnTotRegistros" style="display: none;" CssClass="numTotReg" runat="server" />
    
    <asp:Panel ID="pnlContent" runat="server">
        <div style="text-align: center; margin: 30px 0 5px 0;">
            <h2>Datos Absolutos por Delegaciones</h2>
        </div>
        <asp:Panel ID="pnlTabs" runat="server">
        </asp:Panel>
        <asp:Panel ID="pnlTabsContent" runat="server">
            <h3><asp:Label ID="lblTabTitle" runat="server" Text=""></asp:Label></h3>
        </asp:Panel>
    </asp:Panel>
    
    
</asp:Content>
