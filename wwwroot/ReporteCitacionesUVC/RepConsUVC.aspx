<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="RepConsUVC.aspx.cs" Inherits="RepCitacUVC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" Runat="Server">
    <style type="text/css">
        span.img-rollover{ width: 165px; height: 130px; overflow: hidden; display: block; position: relative; }
        span.img-rollover a:hover{ top: -130px; position: relative;}
        iframe { border: none; }
    </style>
    
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

    function ShowPopUp(id) {
		//transition effect
		$('#boxes').children().hide();
		$('#'+id).fadeIn(2000); 
	    document.getElementById(id).contentWindow.location.reload();

	}
	
	
    function HidePopUp()
    {
        $('#mask').fadeOut(1000);
        $('.window').hide();
    }
    
    function showLoader(idLdr, idFrm)
    {
        $(idFrm).fadeOut(200);
        $(idLdr).fadeIn(200);
    }

</script>

<style type="text/css">


#mask {
  position:absolute;
  left:0;
  top:0;
  z-index:9000;
  background-color:#f0f0f0;
  display:none;
}
  
#boxes .window {
  position:absolute;
  left:0;
  top:0;
  width:440px;
  height:200px;
  display:none;
  z-index:9999;
  padding:20px;
  background-color:#ffffff;
  border: thin solid #000000;
}
input.dp-applied {
	width: 140px;
	float: left;
}

a.dp-choose-date {
	float: left;
	width: 16px;
	height: 16px;
	padding: 0;
	margin: 5px 3px 0;
	display: block;
	text-indent: -2000px;
	overflow: hidden;
	background: url(../images/calendar.png) no-repeat; 
}
a.dp-choose-date.dp-disabled {
	background-position: 0 -20px;
	cursor: default;
</style>
    <script type="text/javascript">
        function ToggleGrid(gridID, titleID)
        {
            $("#<%= lblTabTitle.ClientID %>").text($("#ctl00_ContentPlaceHolderBody_"+titleID).text());
            $(".mGrid").hide();
            $("#"+gridID).toggle();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" Runat="Server">
    <h1>Reporte de Consultas</h1>
    <div id="block-m-gray">
        Fechas de consulta: 
        <asp:Label ID="lblFechaDesde" runat="server" Text="Desde"></asp:Label>
        <asp:TextBox ID="txtFechaDesde" CssClass="inputDate" runat="server"></asp:TextBox>
        <asp:Label ID="lblFechaHasta" runat="server" Text="Hasta"></asp:Label>
        <asp:TextBox ID="txtFechaHasta" CssClass="inputDate" runat="server"></asp:TextBox>
        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
    </div>
    <div style="width: 100%; float:left;">
        <div style="width: 33%; float: left" align="center">
            <div class="viewPieChartBtnMdm">
                <a onclick="javascript: ShowPopUp('frmConsPlacas');">Consultas de Placas</a>
            </div>
        </div>
        <div style="width: 33%; float: left" align="center">
            <div class="viewPieChartBtnMdm">
                <a onclick="javascript: ShowPopUp('frmConsLicenc');">Consultas de Licencias</a>
            </div>
        </div>
        <div style="width: 33%; float: left" align="center">
            <div class="viewPieChartBtnMdm">
                <a onclick="javascript: ShowPopUp('frmConsGral');">Consultas Generales</a>
            </div>
        </div>
        <!--<div style="width: 33%; float: left" align="center">
            <div class="viewPieChartBtnMdm">
                <a onclick="javascript: ShowPopUp('frmConsCitac');">Consultas de Citaciones</a>
            </div>
        </div>
        <div style="width: 33%; float: left" align="center">
            <div class="viewPieChartBtnMdm">
                <a onclick="javascript: ShowPopUp('frmConsVehic');">Consultas de Vehículos</a>
            </div>
        </div>-->
    </div>
    <div id="boxes">
        <iframe id="frmConsPlacas" src="ChartConsPlacas.aspx" style="display: none;" width="1100" height="670" scrolling="auto"></iframe>
        <iframe id="frmConsLicenc" src="ChartConsLicencias.aspx" style="display: none;" width="1100" height="670" scrolling="auto"></iframe>
        <iframe id="frmConsGral" src="ChartConsGeneral.aspx" style="display: none;" width="1100" height="670" scrolling="auto"></iframe>
        <!--<iframe id="frmConsCitac" src="ChartConsCitaciones.aspx" style="display: none;" width="1000" height="670" scrolling="auto"></iframe>
        <iframe id="frmConsVehic" src="ChartConsVehiculos.aspx" style="display: none;" width="1000" height="670" scrolling="auto"></iframe>-->
    </div>
    <div id="mask" onclick="javascript: HidePopUp()">
        </div>
    
    <asp:Panel ID="pnlContent" runat="server">
        <div style="text-align:center; margin: 20px 0 5px 0;"><h2>Datos Absolutos por Delegaciones (Licencias/Placas)</h2></div>
        <asp:Panel ID="pnlTabs" runat="server">
        </asp:Panel>
        <asp:Panel ID="pnlTabsContent" runat="server">
            <h3><asp:Label ID="lblTabTitle" runat="server" Text=""></asp:Label></h3>
        </asp:Panel>
    </asp:Panel>
</asp:Content>

