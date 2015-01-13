<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register TagPrefix="UserControls" TagName="header" Src="controls/header.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="footer" Src="controls/footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito del Ecuador</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <UserControls:header id="Header" runat="server" />
    </div>
    <div align="center" style="margin-top:10px;">
      <div>
	   <table width="700">
	     <tr>
	       <td valign="top" width="50%"><!-- izquierda -->
	            <div class="areaTitleCitaciones">
                    <a href="ConsultaCitaciones.aspx"><img src="img/titleAreaCitaciones.gif" /></a>
            </div>
        <table class="mainMenuSubTitle" style="width: 372px">
          <tr>
            <td style="width: 220px; height: 30px;" valign="middle">
                <img src="img/icoCedula.gif" /><a href="ConsultaCitaciones.aspx?critcons=ced" class="mainMenuSubTitleLinks">&nbsp;Cédula de Identidad</a></td>
            <td style="width: 220px; height: 30px;" valign="middle"><a href="ConsultaCitaciones.aspx?critcons=plac" class="mainMenuSubTitleLinks"><img src="img/icoPlaca.gif" />&nbsp;Número de Placa&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</a></td>
          </tr>
          <tr>
            <td style="width: 201px; height: 30px" valign="middle">
                <a href="ConsultaCitaciones.aspx?critcons=citac" class="mainMenuSubTitleLinks"><img src="img/icoCodCitac.gif" />&nbsp;Número de Citación</a></td>
            <td style="width: 201px; height: 30px" valign="middle">
                <a href="ConsultaCitaciones.aspx?critcons=fech" class="mainMenuSubTitleLinks"><img src="img/icoCalendar.gif" />&nbsp;Por Rango de Fecha</a></td>
          </tr>
          <tr>
            <td style="width: 201px; height: 30px" valign="middle">
                <a href="PuntosPorLicencia.aspx" class="mainMenuSubTitleLinks"><img src="img/icon_puntos.gif" />&nbsp;Puntos por licencia</a></td>
            <td valign="middle">
                <a href="ConsultaCitacionDigitalizada.aspx" class="mainMenuSubTitleLinks"><img src="img/icoImagenCitac.gif" />&nbsp;Citación digitalizada</a></td>
          </tr>
        </table>
        <div class="areaTitle">
          <a href="ConsultaActas.aspx">
              <img src="img/titleAreaActas.gif" /></a>
        </div>
        <table class="mainMenuSubTitle" style="width: 372px">
          <tr>
            <td style="height: 30px" valign="middle" align="center">
                <a href="ConsultaActa.aspx" class="mainMenuSubTitleLinks">Actas firmadas</a></td>
          </tr>
          <tr>
            <td style="height: 30px" valign="middle" align="center">
                <a href="ConsultaActas.aspx" class="mainMenuSubTitleLinks">Todas las actas</a></td>
          </tr>
        </table>
        <div class="areaTitle">
          <a href="Estadisticas.aspx">
              <img src="img/titleAreaEstadisticas.gif" /></a>
        </div>
	  </td>
	  
	  <td valign="top" width="100%"><!-- derecha -->
	  
	            <div class="areaTitle">
          <a href="ConsultaAudiencias.aspx">
              <img src="img/titleAreaAudiencias.gif" /></a>
        </div>
        <table class="mainMenuSubTitle">
          <tr>
            <td style="height: 30px" valign="middle" align="center">
                <a href="ConsultaAudiencias.aspx?critcons=citac" class="mainMenuSubTitleLinks"><img src="img/icoConsAudiencia.gif" />&nbsp;Consultar horarios de audiencias</a></td>
          </tr>
          <tr>
            <td style="height: 30px" valign="middle" align="center">
                <a href="NotificacionAudiencia.aspx" class="mainMenuSubTitleLinks"><img src="img/icoNewAudiencia.gif" />&nbsp;Registrar nueva audiencia</a></td>
          </tr>
        </table>
        <div class="areaTitle">
          <a href="ConsultaLicencias.aspx">
              <img src="img/titleAreaLicencias.gif" /></a>
        </div>
        <div class="areaTitle">
          <a href="ConsultaVehiculos.aspx">
              <img src="img/titleAreaVehiculos.gif" /></a>
        </div> 	       
	    </td>
	  </tr>
	</table>
	    
      
        
        
        
        
    </div>
    <div>
        <UserControls:footer ID="Footer" runat="server" />
    </div>
    </form>
</body>
</html>
