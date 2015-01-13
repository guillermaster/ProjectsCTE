<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="ConsultaCitaciones.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
<%@ Register TagPrefix="UserControls" TagName="header" Src="controls/header.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="footer" Src="controls/footer.ascx" %>
<%@ Register TagPrefix="UserControls" TagName="backButton" Src="controls/backToHome.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comisión de Tránsito del Ecuador</title>
    <link rel="stylesheet" type="text/css" media="screen,projection,print" href="./css/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="divHeader" runat="server">
        <UserControls:header id="Header" runat="server" />
        <UserControls:backButton ID="btnBack" runat="server" />
    </div>
    <div>
        &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackErrorMessage="Ocurrió un error en la consulta al servidor CTG" AsyncPostBackTimeout="360">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
          <ProgressTemplate>
            <div>
                <img src="./img/ajax-loader.gif" />&nbsp;Cargando...
            </div>
          </ProgressTemplate>
        </asp:UpdateProgress>
        &nbsp;&nbsp;
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate><asp:HiddenField ID="hdnCriterioConsulta" runat="server" />
              <div id="divConsPorFecha" visible="false" runat="server">
               <h3>Consulta de Citaciones por Rango Fechas</h3>
                 <table width="350" align="center" class="tableConsulta2">
                   <tr>
                     <td>Desde:</td>
                     <td>
                         <asp:TextBox ID="txtFechaDesde" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqValFechDesde" runat="server" ControlToValidate="txtFechaDesde"
                             ErrorMessage="&nbsp;*" ForeColor="Maroon"></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="regexValFechaIni" runat="server" ControlToValidate="txtFechaDesde"
                             ErrorMessage="&nbsp;&nbsp;*"
                             ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" ForeColor="Maroon"></asp:RegularExpressionValidator>
                         <cc1:CalendarExtender ID="calExtDesde" runat="server" TargetControlID="txtFechaDesde" Format="dd/MM/yyyy">
                         </cc1:CalendarExtender>
                     </td>
                   </tr>
                   <tr>
                     <td>Hasta:</td>
                     <td>
                         <asp:TextBox ID="txtFechaHasta" runat="server"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="reqValFechaHasta" runat="server" ControlToValidate="txtFechaHasta"
                             ErrorMessage="&nbsp;*" ForeColor="Maroon"></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="regexValFechaFin" runat="server" ControlToValidate="txtFechaHasta"
                             ErrorMessage="&nbsp;*"
                             ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)[0-9]{2}" ForeColor="Maroon"></asp:RegularExpressionValidator>
                         <cc1:CalendarExtender ID="calExtHasta" runat="server" TargetControlID="txtFechaHasta" Format="dd/MM/yyyy">
                         </cc1:CalendarExtender>
                         <!--<asp:CompareValidator ID="cmpValFechas" runat="server" ErrorMessage="Fecha desde debe ser menor a fecha hasta" ControlToCompare="txtFechaHasta" ControlToValidate="txtFechaDesde" Operator="LessThanEqual" Type="Date" CssClass="error" ForeColor="Maroon"></asp:CompareValidator>-->
                     </td>
                   </tr>
                 </table>
              </div>
              <div id="divConsPorCedula" visible="false" runat="server">
                <h3>Consulta de Citaciones por Licencia de Conductor Infractor</h3>
                <table width="350" align="center" class="tableConsulta1">
                  <tr>
                    <td>Cédula:</td>
                    <td style="height: 34px">
                        <asp:TextBox ID="txtCedula" runat="server" MaxLength="18"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqValCedula" runat="server" ControlToValidate="txtCedula"
                            ErrorMessage="&nbsp;*" ForeColor="Maroon"></asp:RequiredFieldValidator></td>
                  </tr>
                </table>
              </div>
              <div id="divConsPorPlaca" visible="false" runat="server">
                <h3>Consulta de Citaciones por Placa de Vehículo Infractor</h3>
                <table width="350" align="center" class="tableConsulta1">
                  <tr>
                    <td>Placa:</td>
                    <td style="height: 34px">
                        <asp:TextBox ID="txtPlaca" runat="server" MaxLength="7" CssClass="uppercase"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqValPlaca" runat="server" ControlToValidate="txtPlaca"
                            ErrorMessage="&nbsp;*" ForeColor="Maroon"></asp:RequiredFieldValidator></td>
                  </tr>
                  <tr>
                </table>
              </div>
              <div id="divConsPorCodCitac" visible="false" runat="server">
                <h3>Consulta de Citaciones por Número de Citación</h3>
                <table width="350" align="center" class="tableConsulta1">
                  <tr>
                    <td>Número de Citación:</td>
                    <td style="height: 34px">
                        <asp:TextBox ID="txtNumCitacion" runat="server" MaxLength="18"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqValNumCitac" runat="server" ControlToValidate="txtNumCitacion"
                            ErrorMessage="&nbsp;*" ForeColor="Maroon"></asp:RequiredFieldValidator></td>
                  </tr>
                </table>
              </div>
              <div align="center">
                 <table width="350" align="center">
                   <tr>
                     <td align="left">
                      <asp:Label ID="lblAccion" runat="server" Text="Acción:"></asp:Label>
                      <asp:DropDownList ID="ddlAccion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAccion_SelectedIndexChanged">
                        <asp:ListItem Selected="True" Value=""> -- Ninguna --</asp:ListItem>
                          <asp:ListItem Value="A">Absolver</asp:ListItem>
                          <asp:ListItem Value="C">Condenar</asp:ListItem>
                        <asp:ListItem Value="I">Impugnar</asp:ListItem>
                        <asp:ListItem Value="V">Verificar</asp:ListItem>
                     </asp:DropDownList>
                     <asp:CheckBox ID="chkIncludePaid" runat="server" Text="Incluir citaciones pagadas" />
                    </td>
                   </tr>
                   <tr>
                    <td align="right">
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="button" OnClick="btnConsultar_Click" /></td>
                   </tr>
                 </table>
                  <br />
                  <asp:Label ID="lblError" runat="server" CssClass="error"></asp:Label><br />
                  <div>
                  <asp:ImageButton ID="btnImprimir" runat="server" ImageUrl="./img/imprimir.gif" Visible="false" OnClientClick="javascript:window.print();" />
                  </div>
                  <div>
                  <asp:ImageButton ID="imgBtnCitacion" runat="server" Visible="false" OnClick="imgBtnCitacion_Click" Width="780px" />
                      <br />
                      </div>
                  <asp:GridView ID="dgCitaciones" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" OnSelectedIndexChanged="dgCitaciones_SelectedIndexChanged" OnRowEditing="dgCitaciones_EditIndexChanged" AllowPaging="True" PageSize="20" OnPageIndexChanging="CitacGridView_PageIndexChanging">
                      <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                      <Columns>
                          <asp:TemplateField><ItemTemplate>
                              <asp:CheckBox ID="CheckBox1" runat="server" /></ItemTemplate></asp:TemplateField>
                          <asp:BoundField HeaderText="Factura" DataField="factura" />
                          <asp:BoundField HeaderText="Num. Citaci&#243;n" DataField="num_infraccion" />
                          <asp:BoundField HeaderText="Fecha" DataField="fec_infraccion" DataFormatString="{0:d}" HtmlEncode="False" />
                          <asp:BoundField HeaderText="Nombre sancionado" DataField="nombres" />
                          <asp:BoundField HeaderText="CI sancionado" DataField="identificacion" />
                          <asp:BoundField HeaderText="Nombre Uniformado" DataField="uniformado" />
                          <asp:BoundField HeaderText="Cod. Uniformado" DataField="id_persona_vig" />
                          <asp:BoundField HeaderText="Placa" DataField="placa" />
                          <asp:BoundField HeaderText="Motivo" DataField="contravencion" />
                          <asp:BoundField HeaderText="Art&#237;culo" DataField="articulo" />
                          <asp:BoundField DataField="PUNTOS" HeaderText="Puntos" />
                          <asp:BoundField HeaderText="Valor ($)" DataField="val_contrav" />
                          <asp:BoundField DataField="PAGADA" HeaderText="Pagada (S/N)" />
                          <asp:BoundField DataField="reincidencias" HeaderText="Reincidencias" />
                          <asp:CommandField HeaderText="Imagen" SelectText="Ver" ShowSelectButton="True" />
                          <asp:CommandField HeaderText="Acci&#243;n" ShowEditButton="True" EditText="Ejecutar" />
                      </Columns>
                      <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                      <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                      <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                      <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                      <AlternatingRowStyle BackColor="Gainsboro" />
                      <PagerSettings PageButtonCount="20" Position="TopAndBottom" />
                  </asp:GridView>
                  <div id="divImpugnacion" runat="server" visible="false">
                    <table>
                      <tr>
                        <td>Citación:</td>
                        <td align="left">
                            <asp:TextBox ID="txtCodCitacionModif" ReadOnly="true" runat="server"></asp:TextBox>
                            <asp:HiddenField ID="hdnFactura" runat="server" />
                        </td>
                      </tr>
                      <tr>
                        <td align="left">Observación:</td>
                        <td><asp:TextBox ID="txtObservacion" runat="server" Height="68px" TextMode="MultiLine" Width="440px"></asp:TextBox></td>
                      </tr>
                      <tr>
                        <td colspan="2" align="right">
                            <asp:Button ID="btnImpugnar" runat="server" Text="Impugnar" CssClass="button" OnClick="btnImpugnar_Click" />
                            <asp:Button ID="btnCancelarImpugnar" runat="server" Text="Cancelar" CssClass="button" OnClick="btnCancelarImpugnar_Click" />
                        </td>
                      </tr>
                    </table>
                  </div>
              </div>
          </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
    <div>
        <UserControls:footer ID="Footer" runat="server" />
    </div>
    </form>
</body>
</html>
