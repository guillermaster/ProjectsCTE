<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="./controls/logout.ascx" TagName="logout" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_text.css" />
    <script type="text/javascript" src="js/common.js"></script>
</head>
<body>
  <div class="page-container">
   <div class="main-content1">
    <form id="form1" runat="server">
    <uc1:logout ID="Logout1" runat="server" />
    <h1>Consultas CTG</h1><br /><br />
    
        <ul>
          <li>&raquo;&nbsp;<a href="Citaciones/InfraccionesPendientes.aspx">Citaciones Pendientes de Pago</a></li>
          <li>&raquo;&nbsp;<a href="Citaciones/PuntosPorLicencia.aspx">Puntos por Licencia</a></li>
          <li>&raquo;&nbsp;<a href="Licencias/index.aspx">Datos de Licencia</a></li>
          <li>&raquo;&nbsp;<a href="Vehiculos/index.aspx">Datos de Vehículos</a></li>
          <li>&raquo;&nbsp;<a href="javascript:popup('https://declaraciones.sri.gov.ec/mat-vehicular-internet/reportes/general/valoresAPagar.jsp','1000','580','1')" onmouseover="window.status='';return true">Matr&iacute;cula (SRI - Valor a Pagar)</a></li>
        </ul>
    </form>
   </div>
  </div>
</body>
</html>
