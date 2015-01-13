<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Details.aspx.cs" Inherits="Consultas_Matriculas_index" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Src="../controls/logout.ascx" TagName="logout" TagPrefix="uc1" %>

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
    <h2>Datos Avanzados de Vehículo</h2>  
    <uc1:logout ID="Logout1" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="height:40px;">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5">
            <ProgressTemplate>
            <div>
                <img src="../img/ajax-loader.gif" /> 
                Procesando datos...
            </div>
            </ProgressTemplate>
            </asp:UpdateProgress>
        </div>  
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
          <div align="left">
        <asp:Menu
            ID="Menu1"
            Width="109px"
            runat="server"
            Orientation="Horizontal"
            StaticEnableDefaultPopOutImage="False"
            OnMenuItemClick="Menu1_MenuItemClick">
            <Items>
                <asp:MenuItem ImageUrl="../img/tab0_on.gif" Text=" " Value="0"></asp:MenuItem>
                <asp:MenuItem ImageUrl="../img/tab1_off.gif" Text=" " Value="1"></asp:MenuItem>
                <asp:MenuItem ImageUrl="../img/tab2_off.gif"  Text=" " Value="2"></asp:MenuItem>
                <asp:MenuItem ImageUrl="../img/tab3_off.gif"  Text=" " Value="3"></asp:MenuItem>
            </Items>
        </asp:Menu>
            <asp:MultiView 
            ID="MultiView1"
            runat="server"
            ActiveViewIndex="0"  >
           <asp:View ID="Tab1" runat="server"  >
                <table width="600" height="200" cellpadding=0 cellspacing=0>
                    <tr valign="top">
                        <td class="TabArea" style="width: 600px; height: 200px;">
                            <table width="100%">
                            <tr><td height="15" style="width: 82px"></td></tr>
                             <tr>
                              <td style="width: 82px">
                                  <asp:Label ID="lblPlaca" runat="server" Text="Placa"></asp:Label></td>
                              <td style="width: 175px">
                                  <asp:TextBox ID="txtPlaca" runat="server" ReadOnly="True" CssClass="field"></asp:TextBox></td>
                              <td style="width: 59px">&nbsp;</td>
                              <td style="width: 97px">
                                  <asp:Label ID="lblColor" runat="server" Text="Color"></asp:Label></td>
                              <td>
                                  <asp:TextBox ID="txtColor" runat="server" ReadOnly="True"></asp:TextBox></td>
                             </tr>
                             <tr>
                              <td style="width: 82px">
                                  <asp:Label ID="lblChasis" runat="server" Text="Chasis"></asp:Label></td>
                              <td style="width: 175px">
                                  <asp:TextBox ID="txtChasis" runat="server" ReadOnly="True"></asp:TextBox></td>
                              <td style="width: 59px">&nbsp;</td>
                              <td style="width: 97px">
                                  <asp:Label ID="lblMotor" runat="server" Text="Motor"></asp:Label></td>
                              <td>
                                  <asp:TextBox ID="txtMotor" runat="server" ReadOnly="True"></asp:TextBox></td>
                             </tr>
                             <tr>
                              <td style="width: 82px">
                                  <asp:Label ID="lblMarca" runat="server" Text="Marca"></asp:Label></td>
                              <td style="width: 175px">
                                  <asp:TextBox ID="txtMarca" runat="server" ReadOnly="True"></asp:TextBox></td>
                              <td style="width: 59px">&nbsp;</td>
                              <td style="width: 82px">
                                  <asp:Label ID="lblTonelaje" runat="server" Text="Tonelaje"></asp:Label></td>
                              <td style="width: 175px">
                                  <asp:TextBox ID="txtTonelaje" runat="server" ReadOnly="true"></asp:TextBox></td>
                             </tr>
                             <tr>
                              <td style="width: 82px; height: 22px;">
                                  <asp:Label ID="lblAno" runat="server" Text="Año"></asp:Label></td>
                              <td style="width: 175px; height: 22px;">
                                  <asp:TextBox ID="txtAno" runat="server" ReadOnly="True"></asp:TextBox></td>
                              <td style="width: 59px; height: 22px;">&nbsp;</td>
                              <td style="width: 97px; height: 22px;">
                                  <asp:Label ID="lblPaisOrigen" runat="server" Text="País de Origen"></asp:Label></td>
                              <td style="height: 22px">
                                  <asp:TextBox ID="txtPaisOrigen" runat="server" ReadOnly="True"></asp:TextBox></td>
                             </tr>
                             <tr>
                              <td style="width: 82px">
                                  <asp:Label ID="lblClase" runat="server" Text="Clase"></asp:Label></td>
                              <td style="width: 175px">
                                  <asp:TextBox ID="txtClase" runat="server" ReadOnly="True"></asp:TextBox></td>
                              <td style="width: 59px">&nbsp;</td>
                              <td style="width: 97px">
                                  <asp:Label ID="lblTipo" runat="server" Text="Tipo"></asp:Label></td>
                              <td>
                                  <asp:TextBox ID="txtTipo" runat="server" ReadOnly="True"></asp:TextBox></td>
                             </tr>
                             <tr>
                               <td style="width: 97px">
                                  <asp:Label ID="lblModelo" runat="server" Text="Modelo"></asp:Label></td>
                              <td colspan="4">
                                  <asp:TextBox ID="txtModelo" runat="server" ReadOnly="True" Columns="48"></asp:TextBox></td>
                             </tr>
                             <tr>
                              <td style="width: 82px">
                                  <asp:Label ID="lblCilindraje" runat="server" Text="Cilindraje" Visible="False"></asp:Label></td>
                              <td style="width: 175px">
                                  <asp:TextBox ID="txtCilindraje" runat="server" ReadOnly="true" Visible="False"></asp:TextBox></td>
                              <td style="width: 59px">&nbsp;</td>
                              <td style="width: 97px">
                                  <asp:Label ID="lblCAVMCPN" runat="server" Text="CAMV/CPN" Visible="False"></asp:Label></td>
                              <td>
                                  <asp:TextBox ID="txtCAVMCPN" runat="server" ReadOnly="True" Visible="False"></asp:TextBox></td>
                             </tr><!--
                             <tr>
                              <td style="width: 82px">
                                  <asp:Label ID="lblNumSOAT" runat="server" Text="Número SOAT"></asp:Label></td>
                              <td style="width: 175px">
                                  <asp:TextBox ID="txtNumSOAT" runat="server" ReadOnly="true"></asp:TextBox></td>
                              <td style="width: 59px">&nbsp;</td>
                              <td style="width: 97px">
                                  <asp:Label ID="lblFechaIniSOAT" runat="server" Text="Inicio SOAT"></asp:Label></td>
                              <td>
                                  <asp:TextBox ID="txtFechaIniSOAT" runat="server" ReadOnly="True"></asp:TextBox></td>
                             </tr>
                             <tr>
                              <td style="width: 82px">
                                  <asp:Label ID="lblEmpSOAT" runat="server" Text="Emp. SOAT"></asp:Label></td>
                              <td style="width: 175px">
                                  <asp:TextBox ID="txtEmpSOAT" runat="server" ReadOnly="True"></asp:TextBox></td>
                              <td style="width: 59px">&nbsp;</td>
                              <td style="width: 97px">
                                  <asp:Label ID="lblFechaFinSOAT" runat="server" Text="Caducidad SOAT"></asp:Label></td>
                              <td>
                                  <asp:TextBox ID="txtFechaFinSOAT" runat="server" ReadOnly="True"></asp:TextBox></td>
                             </tr>-->
                            </table>
                        </td>
                    </tr>
                </table>
             </asp:View>
            <asp:View ID="Tab2" runat="server">
                <table width="600px" height="200px" cellpadding=0 cellspacing=0>
                    <tr valign="top">
                        <td class="TabArea" style="width: 600px">
                            <table width="100%">
                            <tr><td height="15" style="width: 82px"></td></tr>
                             <tr>
                               <td style="height: 22px; width: 82px;">
                                  <asp:Label ID="lblNoPasajeros" runat="server" Text="No. Pasajeros"></asp:Label></td>
                              <td style="height: 22px; width: 175px;">
                                  <asp:TextBox ID="txtNoPasajeros" runat="server" ReadOnly="true"></asp:TextBox></td>
                              <td style="height: 22px; width: 59px;">&nbsp;</td>
                             </tr>
                             <tr>
                              <td style="width: 82px">
                                  <asp:Label ID="lblServicio" runat="server" Text="Servicio"></asp:Label></td>
                              <td style="width: 175px">
                                  <asp:TextBox ID="txtServicio" runat="server" ReadOnly="true"></asp:TextBox></td>
                              <td style="width: 59px">&nbsp;</td>
                              <td style="width: 97px">
                                  <asp:Label ID="lblClaseServicio" runat="server" Text="Clase Servicio"></asp:Label></td>
                              <td>
                                  <asp:TextBox ID="txtClaseServicio" runat="server" ReadOnly="true"></asp:TextBox></td>
                             </tr>
                             <tr>
                              <td style="width: 82px">
                                  <asp:Label ID="lblModalidad" runat="server" Text="Modalidad"></asp:Label></td>
                              <td style="width: 175px">
                                  <asp:TextBox ID="txtModalidad" runat="server" ReadOnly="true"></asp:TextBox></td>
                              <td style="width: 59px">&nbsp;</td>
                              <td style="width: 97px">
                                  <asp:Label ID="lblCooperativa" runat="server" Text="Cooperativa"></asp:Label></td>
                              <td>
                                  <asp:TextBox ID="txtCooperativa" runat="server" ReadOnly="true"></asp:TextBox></td>
                             </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="Tab3" runat="server">
                <table width="600px" height="200px" cellpadding=0 cellspacing=0>
                    <tr valign="top">
                        <td class="TabArea" style="width: 600px">
                          <table width="100%" style="margin: 10px 0px 10px 0px">
                            <tr><td>
                                <asp:Label ID="lblMensajeMatriculas" runat="server"></asp:Label></td></tr>
                            <tr><td>
                            <asp:GridView ID="gvHistMatricula" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None" Width="550px">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="anio_matricula" HeaderText="A&#241;o" ReadOnly="True" SortExpression="anio_matricula">
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="emision" HeaderText="Fecha de Emisi&#243;n" SortExpression="emision">
                                        <ItemStyle Width="130px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="caducidad" HeaderText="Fecha de Caducidad" ReadOnly="True"
                                        SortExpression="caducidad" >
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo_mat" HeaderText="Tipo de Matr&#237;cula" ReadOnly="True"
                                        SortExpression="tipo_mat">
                                        <ItemStyle Width="100px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="tipo_cobro" HeaderText="Tipo de Cobro" ReadOnly="True"
                                        SortExpression="tipo_cobro">
                                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            </td></tr>
                           </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="Tab4" runat="server">
                <table width="600" height="200" cellpadding=0 cellspacing=0>
                    <tr valign="top">
                        <td class="TabArea" style="width: 600px">
                          <table width="100%" style="margin: 10px 0px 10px 0px">
                            <tr><td>
                                <asp:Label ID="lblMensajeBloqueos" runat="server"></asp:Label></td></tr>
                            <tr><td>
                            <asp:GridView ID="gvBloqueos" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                ForeColor="#333333" GridLines="None" Width="550px">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <Columns>
                                    <asp:BoundField DataField="fecha_ingreso" HeaderText="Fecha" ReadOnly="True" SortExpression="fecha_ingreso">
                                        <ItemStyle Width="210px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripci&#243;n" ReadOnly="True"
                                        SortExpression="descripcion" />
                                </Columns>
                                <RowStyle BackColor="#EFF3FB" />
                                <EditRowStyle BackColor="#2461BF" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            </td></tr>
                           </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
        </div>
          </ContentTemplate>
        </asp:UpdatePanel>
        <br /><br />
        <div class="column1-unit">
		    <a href="index.aspx"><img src="../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
        </div> 
    
    </form>
    <script type="text/javascript" language="javascript">
    var oldMenu_HideItems = Menu_HideItems;
	
if(oldMenu_HideItems)
{
	Menu_HideItems = function(items)

         {
	     if (!items || ((typeof(items.tagName) == "undefined") && (items instanceof Event)))

              { 

                items = __rootMenuItem;

              }
 	     if(items && items.rows && items.rows.length == 0)

               { 

               items.insertRow(0);

               }
	     return oldMenu_HideItems(items);
	} 
}

    </script>
</body>
</html>
