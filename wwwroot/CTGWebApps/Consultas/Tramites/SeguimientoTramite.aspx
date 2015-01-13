<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeguimientoTramite.aspx.cs" Inherits="Consultas_Tramites_SeguimientoTramite" %>

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
    <h2>Seguimiento de Trámites</h2>  
        <uc1:logout ID="Logout1" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="height:10px;">
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="5">
        <ProgressTemplate>
        
            <div>
                <img src="../../img/ajax-loader.gif" /> 
                Procesando datos...
            </div>
        
        </ProgressTemplate>
        </asp:UpdateProgress>
        </div>   
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <!--<div class="contactform">
	                <p>
		            <table style="width: 414px">
                    <tr>
                     <td style="width: 204px"><asp:Label ID="lblIdentificacion" runat="server" Text="Ingrese identificación: "></asp:Label></td>
                     <td style="width: 179px"><asp:TextBox ID="txtIdentificacion" runat="server" MaxLength="20"></asp:TextBox></td>
                    </tr>
                    <tr>
                     <td style="width: 204px; height: 81px"><asp:Label ID="lblTipoIdentificacion" runat="server" Text="Seleccione el tipo de identificación: "></asp:Label></td>
                     <td style="height: 81px; width: 179px;">
                         <asp:RadioButtonList ID="rbTipoIdentificacion" runat="server">
                             <asp:ListItem Selected="True" Value="placa">N&uacute;mero de Tr&aacute;mite</asp:ListItem>
                             <asp:ListItem Value="licencia">C&#233;dula/RUC/Pasaporte</asp:ListItem>
                         </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                     <td colspan="2" style="height: 34px"><asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="button" OnClick="btnConsultar_Click" Font-Bold="True" /></td>
                    </tr>
                  </table>
	                </p>
                </div>-->
                <div>
                    <div id="divWarning" runat="server">
                        <table class="warning" align="center" width="75%">
                        <tr>
                        <td style="width: 54px"><img src="../../img/warning.gif" /></td>
                        <td><asp:Label ID="lblMensaje" runat="server"></asp:Label></td>
                        </tr>         
                        </table>
                    </div>
                    
                    <div id="divCEPs" runat="server" visible="true">
                    <br /><br /><br />
                    <asp:DataGrid ID="dgCEPs" runat="server" Width="620px"
										AutoGenerateColumns="False" AllowSorting="True" GridLines="None" CellPadding="3" ForeColor="#333333" CellSpacing="3" OnSelectedIndexChanged="dgCEPs_SelectedIndexChanged" AllowPaging="True" OnPageIndexChanged="dgCEPs_PageIndexChanged">
				        <FooterStyle ForeColor="White" BackColor="#507CD1" Font-Bold="True"></FooterStyle>
										<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
										<ItemStyle BackColor="#EFF3FB" Font-Size="X-Small" VerticalAlign="Top"></ItemStyle>
										<HeaderStyle Font-Size="XX-Small" Font-Bold="True" ForeColor="White" BackColor="#507CD1" HorizontalAlign="Center"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn HeaderText="C&#243;digo de Pago (CEP)" DataField="cep">
                                <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Valor ($)" DataField="valor">
                                <ItemStyle HorizontalAlign="Right" Width="45px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Tipo" DataField="tipo">
                                <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Fecha de Creaci&#243;n" DataField="fecha_ingreso">
                                <ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Fecha de Pago/Rerverso" DataField="fecha_pago_reverso">
                                <ItemStyle HorizontalAlign="Center" Width="110px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Estado" DataField="pagada">
                                <ItemStyle HorizontalAlign="Center" Width="70px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:ButtonColumn CommandName="Select" DataTextField="ver_tramite">
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                            </asp:ButtonColumn>
                            <asp:BoundColumn DataField="id_tramite" HeaderText="IdTramite" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="entrega" HeaderText="Entrega" Visible="False"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle NextPageText="Anterior" PrevPageText="Siguiente" HorizontalAlign="Center" ForeColor="White"
											BackColor="#2461BF" Mode="NumericPages"></PagerStyle>
                        <EditItemStyle BackColor="#2461BF" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <br /><a href="../../DefaultConsultas.aspx"><img src="../../img/bullet_back.gif" border="0" class="readmore" />&nbsp;Regresar</a>
                    </div>
                   <div id="divDetTramite" runat="server" visible="false">
                    <div class="contactform" style="width: 480px">
                        <b>CEP:</b>
                        <asp:Label ID="lblCEP" runat="server"></asp:Label>
                        <br />
                        <b>Código de Trámite: </b>
                        <asp:Label ID="lblIdTramite" runat="server"></asp:Label>
                        <br />
                        <b>Entrega:</b>
                        <asp:Label ID="lblEntrega" runat="server"></asp:Label>
                        <br />&nbsp;
                    </div>
                    <br />
                    <asp:DataGrid ID="dgDetTramite" AutoGenerateColumns="False" AllowSorting="True" GridLines="None" CellPadding="3" ForeColor="#333333" CellSpacing="3" runat="server">
                        <FooterStyle ForeColor="White" BackColor="#507CD1" Font-Bold="True"></FooterStyle>
						<AlternatingItemStyle BackColor="White"></AlternatingItemStyle>
						<ItemStyle BackColor="#EFF3FB" Font-Size="X-Small" VerticalAlign="Top"></ItemStyle>
						<HeaderStyle Font-Size="XX-Small" Font-Bold="True" ForeColor="White" BackColor="#507CD1" HorizontalAlign="Center"></HeaderStyle>
                        <Columns>
                            <asp:BoundColumn DataField="descripcion" HeaderText="Etapa">
                                <ItemStyle Width="210px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="estado" HeaderText="Estado">
                                <ItemStyle Width="140px"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="fecha_inicio" HeaderText="Fecha de Inicio">
                                <ItemStyle Width="140px"></ItemStyle>
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                    <br /><br />
                       <asp:ImageButton ID="imgBtnCerrar" runat="server" Height="13px" ImageUrl="~/img/close.gif" OnClick="imgBtnCerrar_Click" Width="60px" />
                    </div>
                  </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        
    </form>
</body>
</html>
