<%@ Page Language="C#" AutoEventWireup="true" CodeFile="estadisticasTipoSangre.aspx.cs" Inherits="Consultas_Licencias_estadisticas" %>

<%@ Register Src="../../controls/logout.ascx" TagName="logout" TagPrefix="uc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

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
    <h2>Licencias por Tipo de Sangre</h2>  
        <uc1:logout ID="Logout1" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="180">
        </asp:ScriptManager>
        <div style="height:15px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5">
            <ProgressTemplate>
            <div>
                <img src="../../img/ajax-loader.gif" /> 
                Procesando datos... (puede tomar varios minutos)
            </div>
        </ProgressTemplate>
        </asp:UpdateProgress>
        </div> 
    <div>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
       <ContentTemplate>
        <div class="contactform">
          <p>
          </p>
        <div>
            <asp:Label ID="lblRangoEdad" runat="server" Text="Rango de Edad:" />
            <asp:DropDownList ID="ddlRangoEdad" runat="server" CssClass="combo2" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblTipoSangre" runat="server" Text="Tipo de Sangre:" />
            <asp:DropDownList ID="ddlTipoSangre" runat="server" CssClass="combo2" />
              <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="button" Font-Bold="True" OnClick="btnConsultar_Click" /></div>
        </div>
        <div>
        <br />
            <asp:Label ID="lblNumRegistros" runat="server"></asp:Label><br />
            <br />
           <div align="center" style="width: 476px;">
    <asp:LinkButton ID="lnkA" runat="server" OnClick="lnkA_Click" Visible="False">A</asp:LinkButton>
    <asp:LinkButton ID="lnkB" runat="server" OnClick="lnkB_Click" Visible="False">B</asp:LinkButton>
    <asp:LinkButton ID="lnkC" runat="server" OnClick="lnkC_Click" Visible="False">C</asp:LinkButton>
    <asp:LinkButton ID="lnkD" runat="server" OnClick="lnkD_Click" Visible="False">D</asp:LinkButton>
    <asp:LinkButton ID="lnkE" runat="server" OnClick="lnkE_Click" Visible="False">E</asp:LinkButton>
    <asp:LinkButton ID="lnkF" runat="server" OnClick="lnkF_Click" Visible="False">F</asp:LinkButton>
    <asp:LinkButton ID="lnkG" runat="server" OnClick="lnkG_Click" Visible="False">G</asp:LinkButton>
    <asp:LinkButton ID="lnkH" runat="server" OnClick="lnkH_Click" Visible="False">H</asp:LinkButton>
    <asp:LinkButton ID="lnkI" runat="server" OnClick="lnkI_Click" Visible="False">I</asp:LinkButton>
    <asp:LinkButton ID="lnkJ" runat="server" OnClick="lnkJ_Click" Visible="False">J</asp:LinkButton>
    <asp:LinkButton ID="lnkK" runat="server" OnClick="lnkK_Click" Visible="False">K</asp:LinkButton>
    <asp:LinkButton ID="lnkL" runat="server" OnClick="lnkL_Click" Visible="False">L</asp:LinkButton>
    <asp:LinkButton ID="lnkM" runat="server" OnClick="lnkM_Click" Visible="False">M</asp:LinkButton>
    <asp:LinkButton ID="lnkN" runat="server" OnClick="lnkN_Click" Visible="False">N</asp:LinkButton>
    <asp:LinkButton ID="lnkO" runat="server" OnClick="lnkO_Click" Visible="False">O</asp:LinkButton>
    <asp:LinkButton ID="lnkP" runat="server" OnClick="lnkP_Click" Visible="False">P</asp:LinkButton>
    <asp:LinkButton ID="lnkQ" runat="server" OnClick="lnkQ_Click" Visible="False">Q</asp:LinkButton>
    <asp:LinkButton ID="lnkR" runat="server" OnClick="lnkR_Click" Visible="False">R</asp:LinkButton>
    <asp:LinkButton ID="lnkS" runat="server" OnClick="lnkS_Click" Visible="False">S</asp:LinkButton>
    <asp:LinkButton ID="lnkT" runat="server" OnClick="lnkT_Click" Visible="False">T</asp:LinkButton>
    <asp:LinkButton ID="lnkU" runat="server" OnClick="lnkU_Click" Visible="False">U</asp:LinkButton>
    <asp:LinkButton ID="lnkV" runat="server" OnClick="lnkV_Click" Visible="False">V</asp:LinkButton>
    <asp:LinkButton ID="lnkW" runat="server" OnClick="lnkW_Click" Visible="False">W</asp:LinkButton>
    <asp:LinkButton ID="lnkX" runat="server" OnClick="lnkX_Click" Visible="False">X</asp:LinkButton>
    <asp:LinkButton ID="lnkY" runat="server" OnClick="lnkY_Click" Visible="False">Y</asp:LinkButton>
    <asp:LinkButton ID="lnkZ" runat="server" OnClick="lnkZ_Click" Visible="False">Z</asp:LinkButton>
</div>
           <asp:GridView ID="gvResLicencias" runat="server" AutoGenerateColumns="False" CellPadding="4"
               ForeColor="#333333" GridLines="None" AllowPaging="True" PageSize="100" Width="476px" OnPageIndexChanging="ResLicGridView_PageIndexChanging">
               <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <Columns>
                   <asp:BoundField DataField="apellidos" HeaderText="Apellidos" >
                       <ItemStyle Width="180px" />
                   </asp:BoundField>
                   <asp:BoundField DataField="nombres" HeaderText="Nombres" >
                       <ItemStyle Width="180px" />
                   </asp:BoundField>
                   <asp:BoundField DataField="telefono" HeaderText="Tel&#233;fono" >
                       <ItemStyle Width="70px" HorizontalAlign="Center" />
                   </asp:BoundField>
                   <asp:BoundField DataField="edad" HeaderText="Edad" >
                       <ItemStyle Width="20px" HorizontalAlign="Center" />
                   </asp:BoundField>
               </Columns>
               <RowStyle BackColor="#EFF3FB" />
               <EditRowStyle BackColor="#2461BF" />
               <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
               <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
               <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
               <AlternatingRowStyle BackColor="White" />
               <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" PageButtonCount="20" />
           </asp:GridView>
         </div>
       </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    <br />
    <div class="column1-unit">
		    <a href="../../DefaultConsultas.aspx"><img src="../../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
        </div>   
    </form>
          
</body>
</html>