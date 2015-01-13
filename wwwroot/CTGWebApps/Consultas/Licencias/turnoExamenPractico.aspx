<%@ Page Language="C#" AutoEventWireup="true" CodeFile="turnoExamenPractico.aspx.cs" Inherits="Consultas_Licencias_turnoExamenPractico" %>

<%@ Register Src="../../controls/logout.ascx" TagName="logout" TagPrefix="uc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito de la Provincia del Guayas</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../../css/layout4_setup.css" />
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="../../css/layout4_text.css" />
    <script type="text/javascript" src="../../js/common.js"></script>
</head>
<body>
    <form id="form1" runat="server">
       <h2>Turnos para Examen Práctico</h2>  
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
                <div id="divError" runat="server" visible="false">
                <table class="error2" align="center" width="480">
                <tr>
                    <td style="width: 54px"><img src="../../img/error.gif" /></td>
                    <td><b>Error:</b><br /><asp:Label ID="lblMsgError" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></td>
                </tr>         
                </table>
                </div>
                <div id="divWarning" runat="server" visible="false">
                <table class="warning" align="center" width="480">
                <tr>
                    <td style="width: 54px"><img src="../../img/warning.gif" /></td>
                    <td><asp:Label ID="lblMsgWarning" runat="server" Font-Size="Small" Font-Bold="False"></asp:Label></td>
                </tr>         
                </table>
                </div>
                <div>
                <asp:GridView ID="gvTurnosExamen" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Width="520px">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <EditRowStyle BackColor="#2461BF" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="nombre_escuela" HeaderText="Escuela"  >
                            <HeaderStyle Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="nombre_persona" HeaderText="Usuario" >
                            <HeaderStyle Width="40%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="fecha_examen" HeaderText="Fecha Examen">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="numero_examen" HeaderText="No. Examen">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
                </div>
                <div class="column1-unit" style="margin-top:20px;">
		        <a href="../../DefaultConsultas.aspx"><img src="../../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
                </div>      
            </ContentTemplate>
           </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
