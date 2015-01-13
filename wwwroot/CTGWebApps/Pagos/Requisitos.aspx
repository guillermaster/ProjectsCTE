<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Requisitos.aspx.cs" Inherits="Pagos_Requisitos" Culture="auto" UICulture="auto"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Src="../controls/logoutPagos.ascx" TagName="logoutPagos" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../css/layout4_text.css" />
    <script type="text/javascript" src="../js/common.js"></script>
    <style type="text/css">
        .uppercase
        {
            text-transform: uppercase;
        } 
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblPageTitle" CssClass="h2" runat="server"></asp:Label>
        <uc1:logoutPagos ID="Logout1" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="180" EnableScriptGlobalization="true" EnableScriptLocalization="true">
        </asp:ScriptManager>
        <div style="height:15px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5">
            <ProgressTemplate>
            <div>
                <img src="../img/ajax-loader.gif" /> 
                Procesando datos...
            </div>
        </ProgressTemplate>
        </asp:UpdateProgress>
        </div> 
    <div>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
       <div id="divDdlTramite" class="contactform" runat="server">
        Seleccione el trámite deseado: 
        <asp:DropDownList ID="ddlTramite" runat="server" CssClass="combo3" AutoPostBack="True" OnSelectedIndexChanged="ddlTramite_SelectedIndexChanged">
              </asp:DropDownList>
        <br /><br />
       </div>
       <div id="contenido" runat="server">
           <asp:Label ID="lblMessage2" runat="server" Font-Size="Medium"></asp:Label><br />
           <asp:Label ID="lblMessage" runat="server" Font-Size="Small" ForeColor="Red"></asp:Label>
           <div id="divError" runat="server" visible="false">
            <table class="error2" align="center" width="85%">
            <tr>
                <td style="width: 54px"><img src="../img/error.gif" /></td>
                <td><b>Datos Incorrectos:</b><br /><asp:Label ID="lblError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></td>
            </tr>         
            </table>
           </div>
        <div style="width:600px; margin-top:20px;" id="divReqControls" runat="server">
         <table id="tblDynamic" runat="server"></table>
         <div>&nbsp;</div>
         <asp:Label ID="lblLocationMsg" runat="server" ForeColor="#C00000" />
        </div>
    </div>
        <div align="center">
            <asp:Button ID="btnAceptar" runat="server" CssClass="button2" Text="  Si  " Font-Bold="True" Visible="False" OnClick="btnAceptar_Click"  />
            <asp:Button ID="btnCancelar" runat="server" CssClass="button2" Text="  No  " UseSubmitBehavior="False" OnClick="btnCancelar_Click" Visible="False" />
            <br />
            <asp:Button ID="btnCorregir" runat="server" CssClass="button2" Font-Bold="True" OnClick="btnCorregir_Click"
               Text="Corregir Datos" Visible="False" Width="103px" />
        </div>
        <div align="center">
          <asp:ImageButton ID="btnGenerarCEP" runat="server" ImageUrl="~/img/generar-cep.gif" Visible="False" OnClick="btnGenerarCEP_Click" />
            <br />
            <br />
        </div>
        <div class="column1-unit" id="divBack" runat="server">
		    <a href="../DefaultPagos.aspx"><img src="../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
        </div> 
       </ContentTemplate>
      </asp:UpdatePanel>
     </div>
    </form>
</body>
</html>