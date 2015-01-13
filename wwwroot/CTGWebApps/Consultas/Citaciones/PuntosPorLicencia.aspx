<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PuntosPorLicencia.aspx.cs" Inherits="Consultas_Citaciones_PuntosPorLicencia" %>

<%@ Register Src="../../controls/logout.ascx" TagName="logout" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../../css/layout4_text.css" />
    <script type="text/javascript" src="../../js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <h2>Puntos por Licencia</h2>  
    <uc1:logout ID="Logout1" runat="server" />
    <div>
        <h3>Detalle de puntos por licencia:</h3>
        <table style="font-size:small; width: 380px; height:94px; margin-left:20px; margin-top:5px; margin-bottom:20px; background-repeat:no-repeat;" background="../../img/bgTablePointsV2.gif">
          <tr>
            <td style="width: 107px; padding-left:10px; color:White; font-weight:bold;">No. Licencia:</td>
            <td style="width: 77px; color:White;"><asp:Label ID="lblLicencia" runat="server" Text=""></asp:Label></td>
          </tr>
          <tr>
            <td style="width: 107px; padding-left:10px"><b>Puntos Referenciales:</b></td>
            <td style="width: 77px" align="center"><asp:Label ID="lblPuntosPerdidos" runat="server" Width="16px">0</asp:Label></td>
            <td style="width: 85px; padding-left:10px"><b>Puntos Perdidos:</b></td>
            <td style="width: 128px" align="center"><asp:Label ID="lblPuntosPerdidosJuzgados" runat="server" Text="0"></asp:Label></td>
          </tr>
          <tr>
            <td style="width: 107px; padding-left:10px"></td>
            <td style="width: 77px"></td>
            <td style="width: 85px; padding-left:10px"><b>Total de Puntos:</b></td>
            <td style="width: 128px" align="center"><asp:Label ID="lblTotalPuntos" runat="server" Font-Bold="True">30</asp:Label></td>
          </tr>
        </table>
        <h3>Tabla de puntos por contravenciones:</h3>
        <img src="../../img/Infracciones_puntos.jpg" style="margin-left:20px; margin-top:5px; margin-bottom:15px;" />
        <div class="column1-unit">
		    <a href="../../DefaultConsultas.aspx"><img src="../../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
        </div>  
    </div>
    </form>
</body>
</html>
