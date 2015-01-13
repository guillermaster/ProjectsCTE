<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Consultas_Licencias_index" %>

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
    <form id="form1" runat="server" onsubmit="javascript:clearLabel('lblMensaje'); clearLabel('results'); return true;"> 
        <h2>Licencias</h2>  
        <uc1:logout ID="Logout1" runat="server" />
        &nbsp;
        <div style="height:10px;">
            &nbsp;</div>   
        <div align="left">     
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
                          
                <div id="results">
                    <div>
                        <asp:Label ID="lblMensaje" runat="server"></asp:Label>
                        <br />
                        <table width="100%">
                          <tr>
                            <td valign="bottom"><asp:Image ID="imgFoto" runat="server" Height="165px" Width="132px" /></td>
                            <td valign="top"><asp:DetailsView ID="dvDatosPersonales" runat="server" Height="50px" Width="437px" EmptyDataText="---" EnableTheming="False" HeaderText="Datos Personales" CellPadding="4" CellSpacing="4" Font-Overline="False" GridLines="None">
                            <RowStyle HorizontalAlign="Left" VerticalAlign="Top" BorderStyle="None" Font-Bold="False" />
                            <FieldHeaderStyle HorizontalAlign="Left" VerticalAlign="Top" BorderStyle="None" Font-Bold="True" />
                            <HeaderStyle Width="20px" BorderStyle="None" Font-Bold="True" Font-Size="Small" VerticalAlign="Top" HorizontalAlign="Center" />
                        </asp:DetailsView></td>
                          </tr>
                        </table>
                        <br />
                    </div>
                    <div>
	                    <asp:GridView ID="grdLicencias" runat="server" Caption="Licencias"
                                CellPadding="4" Font-Names="Verdana" Font-Size="10pt" GridLines="None"
                                Width="591px" Height="20px" Font-Bold="True" ForeColor="#333333">
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <RowStyle BackColor="#EFF3FB" Font-Size="X-Small" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                                <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            </asp:GridView>
                        <br />
                    </div>

                    <div>
	                    <asp:GridView ID="grdBloqueos" runat="server" Caption="Bloqueos" CaptionAlign="Top"
                                CellPadding="4" Font-Names="Verdana" Font-Size="10pt" GridLines="None"
                                Width="591px" Height="20px" Font-Bold="True" ForeColor="#333333">
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <RowStyle BackColor="#EFF3FB" Font-Size="X-Small" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                                <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            </asp:GridView>
                        <br />
                    </div>

                    <div>
	                    <asp:GridView ID="grdRestricciones" runat="server" Caption="Restricciones" CaptionAlign="Top"
                                CellPadding="4" Font-Names="Verdana" Font-Size="10pt" GridLines="None"
                                Width="591px" Height="20px" Font-Bold="True" ForeColor="#333333">
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <RowStyle BackColor="#EFF3FB" Font-Size="X-Small" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                                <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            </asp:GridView>
                        <br />
                    </div>

                    <div>
	                    <asp:GridView ID="grdInfracciones" runat="server" Caption="Contravenciones" CaptionAlign="Top"
                                CellPadding="4" Font-Names="Verdana" Font-Size="10pt" GridLines="None"
                                Width="591px" Height="20px" Font-Bold="True" ForeColor="#333333">
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <RowStyle BackColor="#EFF3FB" Font-Size="X-Small" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                                <AlternatingRowStyle BackColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            </asp:GridView>
                    </div>
                    <br />
                    <br />
                    <div>
	                    <asp:GridView ID="grdCatLicencias" runat="server" Caption="Categorías de Licencias" CellPadding="4"
                                GridLines="None" Height="20px" Width="591px" Font-Bold="True" Font-Names="Verdana" Font-Size="X-Small" ForeColor="#333333" Font-Overline="False">
                                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                                <RowStyle BackColor="#EFF3FB" Font-Size="X-Small" HorizontalAlign="Left" Font-Bold="False" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="X-Small" />
                                <AlternatingRowStyle BackColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                            </asp:GridView>
                            <br /><br />
                    </div>
                  </div>
        </div>  
        <div class="column1-unit">
		    <a href="../../DefaultConsultas.aspx"><img src="../../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
        </div>      
    </form>
</body>
</html>
