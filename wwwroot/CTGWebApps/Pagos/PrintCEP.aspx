<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintCEP.aspx.cs" Inherits="Pagos_PrintCEP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_text.css" />
    <script type="text/javascript" src="../js/common.js"></script>
    <style>
        table.reciboCEP  {
	        border:#333333 thin;
        }
        td.reciboCEP_leftCol {
	        border-right: #333333 thin;
	        background-color:#FFFFFF;
	        font-size:11px;
	        font-weight:bold;
	        color: rgb(80,80,80);
	        height: 20px;
	        vertical-align:middle;
        }
    </style>
</head>
<body style="margin: 25px 5px 10px 5px;">
    <form id="form1" runat="server">
        <div>
            <table width="500" border="1" cellspacing="0" cellpadding="0" class="reciboCEP">
                <tr>
                    <td colspan="2" align="center" valign="middle" style="border-bottom: #333333 thin; padding-bottom: 5px; " class="h1">
                        <img src="../img/smallCTG.gif" style="margin: 5px;" /><br />
                        COMISI&Oacute;N DE TR&Aacute;NSITO DE LA PROVINCIA DEL GUAYAS<br />
                        COMPROBANTE ELECTR&Oacute;NICO DE PAGO
                    </td>
                </tr>
                <tr>
                    <td class="reciboCEP_leftCol">
                        &nbsp;C&oacute;digo de Pago (CEP):</td>
                    <td width="62%">
                        &nbsp;<asp:Label ID="lblCEP" runat="server" Text="Label" /></td>
                </tr>
                <tr>
                    <td class="reciboCEP_leftCol">
                        &nbsp;Tr&aacute;mite:</td>
                    <td>
                        &nbsp;<asp:Label ID="lblTramite" runat="server" Text="Label" /></td>
                </tr>
                <tr>
                    <td class="reciboCEP_leftCol">
                        &nbsp;Identificación de Usuario:</td>
                    <td>
                        &nbsp;<asp:Label ID="lblIdentificacion" runat="server" Text="Label" /></td>
                </tr>
                <tr>
                    <td class="reciboCEP_leftCol">
                        &nbsp;Nombre de Usuario:</td>
                    <td>
                        &nbsp;<asp:Label ID="lblUsuario" runat="server" Text="Label" /></td>
                </tr>
                <tr>
                    <td class="reciboCEP_leftCol">
                        &nbsp;Valor a pagar :</td>
                    <td>
                        &nbsp;<asp:Label ID="lblValor" runat="server" Text="Label" /></td>
                </tr>
                <tr>
                    <td class="reciboCEP_leftCol">
                        &nbsp;Fecha de solicitud:</td>
                    <td>
                        &nbsp;<asp:Label ID="lblFecha" runat="server" Text="Label" /></td>
                </tr>
            </table>
        </div>
        <br />
        <asp:Label ID="lblMensaje" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <table width="500">
            <tr><td colspan="2">
                Usted puede realizar el pago del trámite solicitado presentando/ingresando este Comprobante Electrónico de Pago a través de los siguientes bancos:
            </td></tr>
            <tr><td>&nbsp;</td></tr>
            <tr>
            <td align="center">
                <img src="../img/logo_BP.jpg" border="0" />
                <div class="small_dark_blue">Internet - Ventanilla</div>
			</td>
			<td align="center">
                <img src="../img/logo_BB.jpg" border="0" />
                <div class="small_dark_blue">Internet - Ventanilla</div>
			</td>
          </tr>
          <tr><td>&nbsp;</td></tr>
        </table>
    </form>
</body>
</html>
