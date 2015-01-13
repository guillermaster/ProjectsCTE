<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PuntosPorLicencia.aspx.cs" Inherits="Consultas_Citaciones_PuntosPorLicencia" %>

<%@ Register Src="../controls/logout.ascx" TagName="logout" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_text.css" />
    <script type="text/javascript" src="../js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <h2>Puntos por Licencia</h2>  
    <uc1:logout ID="Logout1" runat="server" />
    <div>
        <div class="contactform">
                <p>
                  <table style="width: 414px">
                    <tr>
                     <td style="width: 213px"><asp:Label ID="lblIdentificacion" runat="server" Text="Ingrese Cédula/RUC/Pasaporte: "></asp:Label></td>
                     <td style="width: 179px"><asp:TextBox ID="txtIdentificacion" runat="server" MaxLength="20"></asp:TextBox></td>
                    </tr>
                    <tr>
                     <td colspan="2" style="height: 34px"><asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="button" OnClick="btnConsultar_Click" Font-Bold="True" /></td>
                    </tr>
                  </table>
                </p>
                </div>
                <br />
                <asp:Panel ID="pnlData" runat="server" Visible="false">
        <h3>Detalle de puntos por licencia:</h3>
        <table style="font-size:small; width: 380px; height:94px; margin-left:20px; margin-top:5px; margin-bottom:20px; background-repeat:no-repeat;" background="../../img/bgTablePointsV2.gif">
          <tr>
            <td style="width: 107px; padding-left:10px; font-weight:bold;">No. Licencia:</td>
            <td style="width: 77px; "><asp:Label ID="lblLicencia" runat="server" Text=""></asp:Label></td>
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
        <img src="../img/Infracciones_puntos.jpg" style="margin-left:20px; margin-top:5px; margin-bottom:15px;" />
        </asp:Panel>
        <div class="column1-unit">
		    <a href="../Default.aspx"><img src="../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
        </div>  
    </div>
    </form>
</body>
</html>
