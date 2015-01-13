<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sinPermiso.aspx.cs" Inherits="sinPermiso" %>

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
    <div class="main-content" style="width: 540px">
<table class="error" align="center">
<tr>
<td style="width: 54px"><img src="img/error.gif" /></td>
<td>
    <br />
    Usted no tiene los permisos necesarios para realizar la acción solicitada.
    <br />
    <br />
    Para poder acceder a este servicio, debe solicitar la activación de su usuario en la CTG.
    <br />
    <br />
</td>
</tr>
</table><br />
 <span style="color:DarkRed; font-weight:bold;">
    Para activar su usuario y utilizar este
     servicio, debe de presentar sus documentos personales (Cédula de Identidad, Pasaporte o RUC) y actualizar sus datos en:
    <ul>
      <li>&middot;&nbsp;<i>Atención al Usuario (frente al Terminal Terrestre de Guayaquil)</i></li>
    </ul>
 </span>       
<!--<div class="column1-unit">
<a href="javascript:history.back();">
    <img src="img/bullet_back.gif" border="0" /> Regresar</a>
</div>-->
</div>

    </form>
</body>
</html>
