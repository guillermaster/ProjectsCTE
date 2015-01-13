<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register TagPrefix="UserControls" TagName="header" Src="controls/header.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="footer" Src="controls/footer.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
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
        <div class="areaTitle">
            <a href="ConsultaLicencias.aspx"><img src="img/titleAreaPersonas.gif" /></a>
        </div>
        <div>
            <ul class="listWithoutStyles">
                <li><a href="ConsultaLicencias.aspx">Por identificación</a></li>
                <li><a href="ConsultaLicenciasPorNombre.aspx">Por nombre</a></li>
            </ul>
        </div>
	  </td>
	  
	  <td valign="top" width="100%"><!-- derecha -->
	  
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
