<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LostPassword.aspx.cs" Inherits="LoginExtra_LostPassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_text.css" />
    <script type="text/javascript" src="../js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <h2>Recuperación de contraseña</h2>
    <asp:Label ID="lblMensaje" runat="server" Text="Ingrese su nombre de usuario (cédula) para que recibe su contraseña."></asp:Label>&nbsp;
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" Visible="False">Continuar</asp:LinkButton><div id="divForm" runat="server">
        <table class="contactform" id="tblForm">
        <tr>
            <td align="right"><asp:Label ID="lblCedula" runat="server" Text="Número de Cédula/Pasaporte/RUC:"></asp:Label></td>
            <td><asp:TextBox ID="txtCedula" runat="server" CausesValidation="True" MaxLength="18"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                &nbsp;
                </td>
        </tr>
        <tr>
            <td align="right"><asp:Button ID="btnContinuar" runat="server" Text="Continuar" CssClass="button" OnClick="btnContinuar_Click" /></td>
            <td align="left"><asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="button" CausesValidation="False" OnClick="btnCancelar_Click" /></td>
        </tr>
        </table>
    </div>
    </form>
</body>
</html>
