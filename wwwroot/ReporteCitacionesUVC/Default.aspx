<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="Server">

    <style type="text/css">
        span.img-rollover{ width: 165px; height: 130px; overflow: hidden; display: block; position: relative; }
        span.img-rollover a:hover{ top: -130px; position: relative;}
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
            //alert(id);
	//select all the a tag with name equal to modal
	
		
		//Get the A tag
		//var id = $(this).attr('href');
	
		//Get the screen height and width
		var maskHeight = $(document).height();
		var maskWidth = $(window).width();
	
		//Set heigth and width to mask to fill up the whole screen
		$('#mask').css({'width':maskWidth,'height':maskHeight});
		
		//transition effect		
		//$('#mask').fadeIn(1000);	
		$('#mask').fadeTo("slow",0.8);	
	
		//Get the window height and width
		var winH = $(window).height();
		var winW = $(window).width();
              
		//Set the popup window to center
		$(id).css('top',  winH/2-$(id).height()/2);
		$(id).css('left', winW/2-$(id).width()/2);
	    
		//transition effect
		$(id).fadeIn(2000); 
	
	}
	
	//if close button is clicked
	/*$('.window .close').click(function (e) {
		//Cancel the link behavior
		e.preventDefault();
		
		$('#mask').hide();
		$('.window').hide();
	});		
	
	//if mask is clicked
	$('#mask').click(function () {
		$(this).hide();
		$('.window').hide();
	});		*/	
	
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

<style>


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

</style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderBody" runat="Server">
    <div style="padding: 20px;">
        <div style="width: 48%; float: left" align="center">
            <span class="img-rollover"><a onclick="javascript: ShowPopUp('#dlgCitac');">
                <img src="images/btnRepCitacUVC.png" alt="Citaciones Emitidas" /></a> </span>
        </div>
        <div style="width: 48%; float: left" align="center">
            <span class="img-rollover"><a onclick="javascript: ShowPopUp('#dlgConsultas');">
                <img src="images/btnRepConsUVC.png" alt="Consultas Realizadas" /></a> </span>
        </div>
        <div id="boxes">
            <div id="dlgCitac" class="window">
                <div id="title">Reporte de citaciones emitidas</div> 
                <div id="ctcForm">
                    <asp:Label ID="lblFechaDesde" runat="server" Text="Desde"></asp:Label>
                    <asp:TextBox ID="txtFechaDesde" CssClass="inputDate" runat="server"></asp:TextBox><br />
                    <asp:Label ID="lblFechaHasta" runat="server" Text="Hasta"></asp:Label>
                    <asp:TextBox ID="txtFechaHasta" runat="server"></asp:TextBox><br /><br />
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClientClick="javascript: showLoader('#ldrCitac', '#ctcForm');" OnClick="btnConsultar_Click" />
                    <input type="button" title="Cancelar" onclick="javascript: HidePopUp()" value="Cancelar" />
                </div>
                <br />
                <img id="ldrCitac" src="images/ajax-loader.gif" alt="Cargando" style="margin: 5px; display: none" />
            </div>
            <div id="dlgConsultas" class="window">
                <div id="titleCons">Reporte de consultas realizadas</div> 
                <div id="consForm">
                    <asp:Label ID="lblConsFechaDesde" runat="server" Text="Desde"></asp:Label>
                    <asp:TextBox ID="txtConsFechaDesde"  runat="server"></asp:TextBox><br />
                    <asp:Label ID="lblConsFechaHasta" runat="server" Text="Hasta"></asp:Label>
                    <asp:TextBox ID="txtConsFechaHasta" runat="server"></asp:TextBox><br /><br />
                    <asp:Button ID="btnConsultarCons" runat="server" Text="Consultar" OnClientClick="javascript: showLoader('#ldrConsultas', '#consForm');" OnClick="btnConsultarCons_Click" />
                    <input type="button" title="Cancelar" onclick="javascript: HidePopUp()" value="Cancelar" />
                </div>
                <br />
                <img id="ldrConsultas" src="images/ajax-loader.gif" alt="Cargando" style="margin: 5px; display: none" />
            </div>
        </div>
        
        <div id="mask" onclick="javascript: HidePopUp()">
        </div>
    </div>
</asp:Content>
