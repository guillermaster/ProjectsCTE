<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="controls/sesionuser.ascx" TagName="sesionuser" TagPrefix="uc2" %>

<%@ Register Src="controls/editableTextBox.ascx" TagName="editableTextBox" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/layout4_text.css" />
    <script language="javascript">
     function clear_licencia(){
        document.getElementById("txtIdentificacion").value = '';
     }
    </script>
</head>
<body>
 <div align="center" style="width: 700px;" >
    <div>
        <img src="img/bg_head_middle_consulta.jpg" />
    </div>
    
    <form id="form1" runat="server">
    <uc2:sesionuser ID="Sesionuser1" runat="server" />
    <h2>
        
        VERIFICACIÓN DE USUARIOS WEB</h2>
    <div align="center" style="height: 10px">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <img src="img/ajax-loader.gif" width="70" height="10" />
            </ProgressTemplate>
        </asp:UpdateProgress>
        
    </div>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <div id="divForm" align="center">
        <table class="contactform" id="tblForm">
        <tr>
            <td align="right" style="height: 24px"><asp:Label ID="lblCedPasRUC" runat="server" Text="Cédula/Pasaporte/RUC:" Font-Size="Small"></asp:Label></td>
            <td style="height: 24px"><asp:TextBox ID="txtIdentificacion" runat="server" CausesValidation="True" MaxLength="18" Font-Size="Medium"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtIdentificacion"
                    ErrorMessage="*"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                &nbsp;
                </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="button" OnClick="btnConsultar_Click" Font-Bold="True" Font-Size="Small" /></td>
            <td align="left"><asp:Button ID="btnCancelar" runat="server" Text="Limpiar" CssClass="button" CausesValidation="False" UseSubmitBehavior="False" OnClick="btnCancelar_Click" OnClientClick="clear_licencia();" Font-Bold="True" Font-Size="Small" /></td>
        </tr>
        </table>
    </div>  
    <br /><br />
    <asp:Label ID="lblMensaje" runat="server" Font-Bold="True" ForeColor="Red" Font-Size="Medium"></asp:Label>
        <br />
        <asp:LinkButton ID="btnRetrySendPwd" runat="server" OnClick="btnRetrySendPwd_Click"
            Visible="False">Reintentar envío de contraseña</asp:LinkButton>
        <br />
        <asp:HyperLink ID="lnkRegistrar" runat="server" NavigateUrl="~/UserRegistartion.aspx"
            Visible="False">Registrar Usuario</asp:HyperLink><br />
    <div id="resultados" runat="server" style="width: 700px;" align="center">
        <table align="left" style="width: 591px">
         <tr><th align="left" style="width: 109px; height: 16px; font-size:small">
             Identificación:</th><td align="left" style="width: 297px; height: 16px"><asp:Label ID="lblIdentificacion" runat="server" Font-Size="Small"></asp:Label></td></tr>
         <tr><th align="left" style="width: 109px; height: 16px; font-size:small">
             Nombres:</th><td align="left" style="width: 297px; height: 16px;">
                 <uc1:editableTextBox ID="editNombres" runat="server" />
             </td></tr>
         <tr><th align="left" style="width: 109px; height: 16px; font-size:small">
             Apellidos:</th><td align="left" style="width: 297px; height: 16px;">
                 <uc1:editableTextBox ID="editApellidos" runat="server" />
             </td></tr>
         <tr><th align="left" style="width: 109px; height: 16px; font-size:small">
             E-mail:</th><td align="left" style="width: 297px; height: 16px;">
                 <uc1:editableTextBox ID="editEmail" runat="server" />
                 </td></tr>
         <tr><td style="width: 109px">
             <asp:HiddenField ID="hdnNewPwd" runat="server" Visible="False" />
             &nbsp;</td></tr>
         <tr><th align="left" style="width: 109px; height: 16px; font-size:small">
             Estado:</th><td align="left" style="width: 297px; height: 16px;"><asp:Label ID="lblEstado" runat="server" Font-Size="Small"></asp:Label>
                 </td></tr>
         <tr><td style="width: 109px">&nbsp;</td></tr>
         <tr><th style="width: 109px">&nbsp;</th><td align="left">
             <asp:Label ID="lblMensajeCambioEstado" runat="server"></asp:Label>
             <asp:ImageButton ID="btnChangeStateToVerified" runat="server" ImageUrl="~/img/btn_cambiar_estado_verifica.gif" OnClick="btnChangeStateToVerified_Click" Visible="False" /></td></tr>
        </table>
        <br />
        <br />
        <asp:Label ID="lblInfoMessage" runat="server" Text="Los usuarios, si desean, pueden cambiar su contraseña en www.ctg.gov.ec en la sección de Actualización de Datos" Visible="False" ForeColor="Blue"></asp:Label>
        <br />
        <br />
        <br />
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>  
    </form>
  </div>
</body>
</html>
