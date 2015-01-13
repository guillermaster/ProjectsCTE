<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultPagos.aspx.cs" Inherits="DefaultPagos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_text.css" />
    <script type="text/javascript" src="js/common.js"></script>
    <style>
        td.seccion{
            cursor:pointer;
        }
        tr.departamento {
	        font-size:12px;
	        font-weight:bold;
	        color: #ffffff;
	        height: 20px;
	        vertical-align:middle;
        }
        td.descripcion {
            padding: 0px 10px 0px 10px;
            color: #414141;
            font-weight:normal;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <br />
    <table cellpadding="5" cellspacing="15">
     <tr>
        
        <td class="seccion" onclick="location='Pagos/Requisitos.aspx?codCatTramite=ATU';">
            
            <table background="img/bgBlueAreaRoundCorn.gif" width="282" height="70">
              <tr class="departamento"><td width="15%"><img class="center" src="img/icon_atusuario.gif" border="0" /></td><td align="left">Atención al Usuario</td></tr>
              <tr><td colspan="2" class="descripcion">En este departamento puede adquirir documentación(certificados) acerca de licencias y vehículos registrados en la CTG.</td></tr>
            </table>
            
        </td>
        
        <td class="seccion" onclick="location='Pagos/Requisitos.aspx?codCatTramite=BRP';">
            <table background="img/bgBlueAreaRoundCorn.gif" width="282" height="70">
              <tr class="departamento"><td width="15%" align="center"><img class="center" src="img/icon_brevetacion.gif" border="0" /></td><td align="left">&nbsp;Brevetación</td></tr>
              <tr><td colspan="2"class="descripcion">En este departamento se emiten las licencias de conducir, aquí puede adquirir, renovar y obtener duplicados de licencias.</td></tr>
            </table>
        </td>
     </tr>
     <tr>
        <td class="seccion" onclick="location='Pagos/Requisitos.aspx?codCatTramite=JPG';">
            <table background="img/bgBlueAreaRoundCorn.gif" width="282" height="70">
              <tr class="departamento"><td width="15%" align="center"><img class="center" src="img/icon_citaciones.gif" border="0" /></td><td align="left">Citaciones</td></tr>
              <tr><td colspan="2" class="descripcion">En este departamento se realizan trámites relacionados con citaciones, tales como el duplicado de citación.</td></tr>
            </table>
        </td>
        <td class="seccion" onclick="location='Pagos/Requisitos.aspx?codCatTramite=MAT';">
            <table background="img/bgBlueAreaRoundCorn.gif" width="282" height="70">
              <tr class="departamento"><td width="15%" align="center"><img class="center" src="img/icon_matricula.gif" border="0" /></td><td align="left">&nbsp;Matriculación</td></tr>
              <tr><td colspan="2" class="descripcion">En este departamento se realizan todo tipo de trámites correspondientes a la matriculación vehícular.</td></tr>
            </table>
        </td>
     </tr>
    </table>
    </div>
    </form>
</body>
</html>