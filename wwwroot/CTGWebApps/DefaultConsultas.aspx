<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="DefaultConsultas.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_text.css" />
    <script type="text/javascript" src="js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%">
    <tr>
      <td valign="top">
      <table class="lista"  width="85%" cellspacing="10">
    <tr><td colspan="2"><img src="img/consTitlePersInfo.gif" /></td></tr>
    <tr><td class="lista" align="center" width="5%"><a href="Consultas/Licencias/index.aspx" onmouseover="window.status='';return true"><img class="center" border="0" src="img/icon_licencia.gif" /></a></td><td class="lista" width="85%"><a href="Consultas/Licencias/index.aspx" onmouseover="window.status='';return true">Datos de Licencia</a></td></tr>
    <tr><td class="lista" align="center" width="5%"><a href="Consultas/Citaciones/PuntosPorLicencia.aspx" onmouseover="window.status='';return true"><img class="center" border="0" src="img/icon_puntos.gif" /></a></td><td class="lista" width="85%"><a href="Consultas/Citaciones/PuntosPorLicencia.aspx" onmouseover="window.status='';return true">Puntos por Licencia</a></td></tr>
    <tr><td class="lista" align="center" width="5%"><a href="Consultas/Matriculas/index.aspx" onmouseover="window.status='';return true"><img class="center" border="0" src="img/icon_matricula.jpg" /></a></td><td class="lista" width="85%"><a href="Consultas/Matriculas/index.aspx" onmouseover="window.status='';return true">Datos de Vehículos</a></td></tr>
    <tr><td colspan="2"><img src="img/consTitleInfracciones.gif" /></td></tr>
    <tr><td class="lista" align="center" width="5%"><a href="Consultas/Citaciones/InfraccionesPendientes.aspx" onmouseover="window.status='';return true"><img class="center" border="0" src="img/icon_citaciones_pend.gif" /></a></td><td class="lista" width="85%"><a href="Consultas/Citaciones/InfraccionesPendientes.aspx" onmouseover="window.status='';return true">Infracciones Pendientes de Pago</a></td></tr>
    <tr><td class="lista" align="center" width="5%"><a href="Consultas/Citaciones/CitacionPorCodigo.aspx" onmouseover="window.status='';return true"><img class="center" border="0" src="img/ico_citaciones_numcitac.gif" /></a></td><td class="lista" width="85%"><a href="Consultas/Citaciones/CitacionPorCodigo.aspx" onmouseover="window.status='';return true">Infracciones por Número de Citación</a></td></tr>
    <tr><td colspan="2"><img src="img/consTitleCanchon.gif" /></td></tr>
    <tr><td class="lista" align="center" width="5%"><a href="Consultas/VehiculosDetenidos/index.aspx" onmouseover="window.status='';return true"><img class="center" border="0" src="img/iconCanchon.gif" /></a></td><td class="lista" width="85%"><a href="Consultas/VehiculosDetenidos/index.aspx" onmouseover="window.status='';return true">Vehículos Retenidos</a></td></tr>
    <!--<tr><td class="lista" align="center" width="5%"><a href="Consultas/VehiculosDetenidos/" target="_self"><img class="center" border="0" src="img/icon_canchon.png" /></a></td><td class="lista" width="85%"><a href="Consultas/VehiculosDetenidos/" target="_self">Vehículos Retenidos</a></td></tr>-->
    <!--<tr><td class="lista" align="center" width="5%"><img class="center" border="0" src="img/icon_tracking.png" /></td><td class="lista" width="85%">Seguimiento de tr&aacute;mites</td></tr>-->
    </table>
      </td>
      <td valign="top">
      <table class="lista"  width="85%" cellspacing="10">
    <tr><td colspan="2"><img src="img/consTitleExamPract.gif" /></td></tr>
    <tr><td class="lista" align="center" width="5%"><a href="Consultas/Licencias/turnoExamenPractico.aspx" target="_self"><img class="center" border="0" src="img/icon_examenpracticoTurno.gif" /></a></td><td class="lista" width="85%"><a href="Consultas/Licencias/turnoExamenPractico.aspx" target="_self">Turnos para Examen Práctico</a></td></tr>
    <tr><td class="lista" align="center" width="5%"><a href="Consultas/Licencias/califExamenPractico.aspx" target="_self"><img class="center" border="0" src="img/ico_examenpractico.gif" /></a></td><td class="lista" width="85%"><a href="Consultas/Licencias/califExamenPractico.aspx" target="_self">Calificaciones de Examen Práctico</a></td></tr>
    <tr><td colspan="2"><img src="img/consTitleTramites.gif" /></td></tr>
    <tr><td class="lista" align="center" width="5%"><a href="Consultas/Tramites/SeguimientoTramite.aspx" target="_self"><img class="center" border="0" src="img/icon_tracking.png" /></a></td><td class="lista" width="85%"><a href="Consultas/Tramites/SeguimientoTramite.aspx" target="_self">Seguimiento de tr&aacute;mites</a></td></tr>
    <tr><td colspan="2"><img src="img/consTitleOtras.gif" /></td></tr>
    <tr><td class="lista" align="center" width="5%"><a href="Consultas/Licencias/estadisticasTipoSangre.aspx" onmouseover="window.status='';return true"><img class="center" border="0" src="img/icon_tiposangre.gif" /></a></td><td class="lista" width="85%"><a href="Consultas/Licencias/estadisticasTipoSangre.aspx" onmouseover="window.status='';return true">Licencias por Tipo de Sangre</a></td></tr>
    <tr><td class="lista" align="center" width="5%"><a href="javascript:popup('https://declaraciones.sri.gov.ec/mat-vehicular-internet/reportes/general/valoresAPagar.jsp','1000','580','1')" onmouseover="window.status='';return true"><img class="center" border="0" src="img/icon_sri.gif" /></a></td><td class="lista" width="85%"><a href="javascript:popup('https://declaraciones.sri.gov.ec/mat-vehicular-internet/reportes/general/valoresAPagar.jsp','1000','580','1')" onmouseover="window.status='';return true">Matr&iacute;cula (SRI - Valor a Pagar)</a></td></tr>
    <tr><td class="lista" align="center" width="5%"><a href="Consultas/VehiculosDetenidos/VehRepRobados.aspx" onmouseover="window.status='';return true"><img class="center" border="0" src="img/ico_veh_robados.gif" /></a></td><td class="lista" width="85%"><a href="Consultas/VehiculosDetenidos/VehRepRobados.aspx" onmouseover="window.status='';return true">Veh&iacute;culos reportados como robados</a></td></tr>
    <!--<tr><td class="lista" align="center" width="5%"><a href="Consultas/VehiculosDetenidos/" target="_self"><img class="center" border="0" src="img/icon_canchon.png" /></a></td><td class="lista" width="85%"><a href="Consultas/VehiculosDetenidos/" target="_self">Vehículos Retenidos</a></td></tr>-->
    <!--<tr><td class="lista" align="center" width="5%"><img class="center" border="0" src="img/icon_tracking.png" /></td><td class="lista" width="85%">Seguimiento de tr&aacute;mites</td></tr>-->
    </table>
      </td>
    </tr>
    </table>
    
    </div>
    </form>
</body>
</html>
