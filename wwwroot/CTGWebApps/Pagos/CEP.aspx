<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CEP.aspx.cs" Inherits="Pagos_CEP" %>

<%@ Register Src="../controls/logoutPagos.ascx" TagName="logoutPagos" TagPrefix="uc1" %>

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
        <uc1:logoutPagos ID="Logout1" runat="server" />
        <asp:Label ID="lblPageTitle" CssClass="h1" runat="server">Comprobante Electrónico de Pago</asp:Label>
                
        <div id="divCepGenerado" runat="server">
                        
            <table align="center" width="580" border="0" cellspacing="3" cellpadding="3">
                <tr>
                    <td colspan="2">
                        <img src="../img/cep_title.jpg" />
                    </td>
                </tr>
                <tr>
                    <td width="28%">
                        <b>Código de Pago (CEP):</b></td>
                    <td width="72%">
                        &nbsp;<asp:Label ID="lblCEP" runat="server" Text="CEP" Font-Bold="True"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        Tr&aacute;mite:</td>
                    <td>
                        &nbsp;<asp:Label ID="lblTramite" runat="server" Text="Tramite"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        Identificación:</td>
                    <td>
                        &nbsp;<asp:Label ID="lblIdentificacion" runat="server" Text="Identificacion" /></td>
                </tr>
                <tr>
                    <td>
                        Usuario:</td>
                    <td>
                        &nbsp;<asp:Label ID="lblUsuario" runat="server" Text="Usuario" /></td>
                </tr>
                <tr>
                    <td>
                        Valor a pagar:</td>
                    <td>
                        &nbsp;<asp:Label ID="lblValorPago" runat="server" Text="Valor" /></td>
                </tr>
                <tr>
                    <td>
                        Fecha:</td>
                    <td>
                        &nbsp;<asp:Label ID="lblFecha" runat="server" Text="fecha" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" valign="middle">
                        <!--<asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="~/img/imprimir.gif" OnClientClick="javascript:window.print();" />-->
                        <a href="javascript:window.print();"><img src="../img/imprimir.gif" border="0" /></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:ImageButton ID="btnFinalizar" runat="server" ImageUrl="~/img/finalizar_pago.gif" PostBackUrl="~/DefaultPagos.aspx" /></td>&nbsp;&nbsp;&nbsp;
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                        <ul>
                        <li><asp:Label ID="lblMensaje1" runat="server" Text="Mensaje1" ForeColor="#C00000"></asp:Label></li>
                        <li><asp:Label ID="lblRequiredDocs" runat="server" Text="" /></li>
                        </ul></td>
                </tr>
            </table>
        </div>
                  
        
        <div id="divCepNoGenerado" align="center" runat="server" visible="false">
            <table class="error2" align="center" width="85%">
            <tr>
                <td style="width: 54px"><img src="../img/error.gif" /></td>
                <td><b>Datos Incorrectos:</b><br /><asp:Label ID="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></td>
            </tr>         
            </table>
            <asp:Button id="btnContinuarError" runat="server" CssClass="button2" Font-Bold="True" Text="Continuar" Width="93px" PostBackUrl="~/DefaultPagos.aspx"></asp:Button>
           </div>
       
        <table align="center" style="margin: 15px 5px 5px 0px;" cellspacing="10" id="linksBancos" runat="server" width="200">
          <tr>
            <td colspan="3" align="center"><img src="../img/msginfobankpayment.jpg" /></td>
          </tr>
          <tr><td>&nbsp;</td></tr>
          <tr>
            <td align="center">
                <a href="javascript:popup('https://www.bancodelpacifico.com/intermatico/intermatico/publico/default.asp','898','700','yes')"><img src="../img/logo_BP.jpg" border="0" /></a>
                <div class="small_dark_blue"><a href="javascript:popup('https://www.bancodelpacifico.com/intermatico/intermatico/publico/default.asp','898','700','yes')">Internet</a> - Ventanilla</div>
			</td>
			<td align="center">
                <a href="javascript:popup('https://www3.bolivariano.com/bancav/','860','600','yes')"><img src="../img/logo_BB.jpg" border="0" /></a>
                <div class="small_dark_blue"><a href="javascript:popup('https://www3.bolivariano.com/bancav/','860','600','yes')">Internet</a> - Ventanilla</div>
			</td>
			<td align="center">
                <a href="javascript:popup('https://iata.bankguay.com/BG.BancaVirtual.WebApp/FrameLogin.aspx','800','680','yes')"><img src="../img/logo_BG.jpg" border="0" /></a>
                <div class="small_dark_blue"><a href="javascript:popup('https://iata.bankguay.com/BG.BancaVirtual.WebApp/FrameLogin.aspx','800','680','yes')">Internet</a> - Ventanilla</div>
			</td>
          </tr>
          <tr>
            <td align="center">
                <a href="javascript:popup('https://www.produbanco.com/GFPNetSeguro/','898','700','yes')"><img src="../img/logo_PB.jpg" border="0" /></a>
                <div class="small_dark_blue"><a href="javascript:popup('https://www.produbanco.com/GFPNetSeguro/','898','700','yes')">Internet</a> - Ventanilla</div>
			</td>
			<td align="center">
                <img src="../img/logo_SP.jpg" border="0" />
                <div class="small_dark_blue">Ventanilla</div>
			</td>
          </tr>
        </table>
    </form>
</body>
</html>
