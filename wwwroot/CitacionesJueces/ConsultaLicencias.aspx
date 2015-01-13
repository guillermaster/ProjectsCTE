<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConsultaLicencias.aspx.cs" Inherits="ConsultaLicencias" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

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
    <div>
        <UserControls:header id="Header" runat="server" />
        <UserControls:backButton ID="btnBack" runat="server" />
    </div>
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
          <ProgressTemplate>
            <div>
                <img src="./img/ajax-loader.gif" />&nbsp;Cargando...
            </div>
          </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
           <div runat="server">
           <h3>Reporte de Datos de Licencia</h3>
           
                <table>
                  <tr>
                    <td style="height: 34px">
                        No. Licencia:</td>
                    <td style="height: 34px">
                        <asp:TextBox ID="txtLicencia" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td colspan="2" align="right">
                        <asp:Button ID="btnConsultaByCedula" runat="server" Text="Consultar" OnClick="btnConsultaByCedula_Click" CssClass="button" /></td>
                  </tr>
                </table>
              </div>
         <asp:Label ID="lblError" runat="server" CssClass="error"></asp:Label>
         
        <div id="datosFull" visible="false" runat="server">
          <table style="width: 89%">
            <tr>
              <td width="50%" valign="top">
                  <asp:Image ID="imgLicFoto" runat="server" Height="188px" Width="150px" />
               <table>
                <tr><th colspan="2">Datos Personales</th></tr>
                <tr><td>Nombre:</td><td><asp:Label ID="lblNombre" runat="server" Text=""></asp:Label></td></tr>
                <tr><td>Fecha de Nacimiento:</td><td><asp:Label ID="lblFechaNacimiento" runat="server" Text=""></asp:Label></td></tr>
                <tr><td>Lugar de Nacimiento:</td><td><asp:Label ID="lblLugarNacimiento" runat="server" Text=""></asp:Label></td></tr>
                <tr><td>Provincia de Nacimiento:</td><td><asp:Label ID="lblProvNacimiento" runat="server" Text=""></asp:Label></td></tr>
                <tr><td>País de Nacimiento:</td><td><asp:Label ID="lblPaisNacimiento" runat="server" Text=""></asp:Label></td></tr>
               </table>
              </td>
              <td width="50%" valign="top">
               <table>
                <tr><th colspan="2">Datos de Licencia</th></tr>
                <tr><td>Documento:</td><td><asp:Label ID="lblDocumento" runat="server" Text=""></asp:Label></td></tr>
                <tr><td>Provincia de Orígen:</td><td><asp:Label ID="lblProvinciaOrigen" runat="server" Text=""></asp:Label></td></tr>
                <tr><td>Fecha de Orígen:</td><td><asp:Label ID="lblFechaOrigen" runat="server" Text=""></asp:Label></td></tr>
                <tr><td>Fecha de Emisión:</td><td><asp:Label ID="lblFechaEmision" runat="server" Text=""></asp:Label></td></tr>
                <tr><td>Fecha de Caducidad:</td><td><asp:Label ID="lblFechaCaducidad" runat="server" Text=""></asp:Label></td></tr>
                <tr><td>Número:</td><td><asp:Label ID="lblNumero" runat="server" Text=""></asp:Label></td></tr>
                <tr><td>Cat. Licencia:</td><td><asp:Label ID="lblCatLicencia" runat="server" Text=""></asp:Label></td></tr>
                <tr><td>Desc. Cat. Lic.:</td><td><asp:Label ID="lblDescCatLicencia" runat="server" Text="" /></td></tr>
               </table>
              </td>
            </tr>
            <tr>
             <td width="50%" valign="top">
             <table>
              <tr><th colspan="2">Rasgos Físicos y Actividades</th></tr>
              <tr><td>Sexo:</td><td><asp:Label ID="lblSexo2" runat="server" Text=""></asp:Label></td></tr>
              <tr><td>Estatura:</td><td><asp:Label ID="lblEstatura" runat="server" Text=""></asp:Label></td></tr>
              <tr><td>Color de Ojos:</td><td><asp:Label ID="lblColorOjos" runat="server" Text=""></asp:Label></td></tr>
              <tr><td>Rostro:</td><td><asp:Label ID="lblRostro" runat="server" Text=""></asp:Label></td></tr>
              <tr><td>Cabello:</td><td><asp:Label ID="lblCabello" runat="server" Text=""></asp:Label></td></tr>
              <tr><td>Tipo de Sangre:</td><td><asp:Label ID="lblTipoSangre" runat="server" Text=""></asp:Label></td></tr>
              <tr><td>Estado Civil:</td><td><asp:Label ID="lblEstadoCivil" runat="server" Text=""></asp:Label></td></tr>
              <tr><td>Profesión:</td><td><asp:Label ID="lblProfesion" runat="server" Text=""></asp:Label></td></tr>
            </table>
             </td>
             <td width="50%" valign="top">
            <table>
              <tr><th colspan="2">Datos de Residencia</th></tr>
              <tr><td>País:</td><td><asp:Label ID="lblPaisRes" runat="server" Text=""></asp:Label></td></tr>
              <tr><td>Provincia:</td><td><asp:Label ID="lblProvinciaRes" runat="server" Text=""></asp:Label></td></tr>
              <tr><td>Cantón:</td><td><asp:Label ID="lblCantonRes" runat="server" Text=""></asp:Label></td></tr>
              <tr><td>Dirección:</td><td><asp:Label ID="lblDireccion" runat="server" Text=""></asp:Label></td></tr>
              <tr><td>Teléfono:</td><td><asp:Label ID="lblTelefono" runat="server" Text=""></asp:Label></td></tr>
            </table>
            <table>
              <tr><th colspan="2">Datos de Obtención de Licencia</th></tr>
              <tr><td>Fecha:</td><td><asp:Label ID="lblFechaObtencLic" runat="server" Text=""></asp:Label></td></tr>
              <tr><td>Provincia:</td><td><asp:Label ID="lblProvinciaObtencLic" runat="server" Text=""></asp:Label></td></tr>
              <tr><td>Categoría:</td><td><asp:Label ID="lblCategoriaObtencLic" runat="server" Text=""></asp:Label></td></tr>
            </table>
             </td>
            </tr>
            <tr>
             <td width="50%" valign="top">
                 HISTORIAL DE INFRACCIONES GRAVES
                 &nbsp;<asp:GridView ID="gvInfraccGraves" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" EmptyDataText="No registra infracciones graves">
                     <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                     <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                     <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                     <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                     <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                     <AlternatingRowStyle BackColor="White" />
                     <Columns>
                         <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                         <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n" />
                     </Columns>
                 </asp:GridView>
             </td>
             <td width="50%" valign="top">
                 &nbsp;HISTORIAL DE RENOVACIÓN DE LICENCIA<asp:GridView ID="gvRenovacionLicencia" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" EmptyDataText="No registra renovaciones de licencia">
                     <FooterStyle BackColor="#CCCC99" />
                     <RowStyle BackColor="#F7F7DE" />
                     <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                     <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                     <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                     <AlternatingRowStyle BackColor="White" />
                     <Columns>
                         <asp:BoundField DataField="FECHA_EXPEDICION" HeaderText="Fecha de Expedici&#243;n" />
                         <asp:BoundField DataField="FECHA_EXPIRACION" HeaderText="Fecha de Expiraci&#243;n" />
                         <asp:BoundField DataField="CATEGORIA" HeaderText="Categor&#237;a" />
                     </Columns>
                 </asp:GridView>
             </td>
            </tr>
            <tr>
             <td width="50%" valign="top">
             RESTRICCIONES
                 <asp:GridView ID="gvRestricciones" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" EmptyDataText="No registra restricciones">
                     <FooterStyle BackColor="#CCCC99" />
                     <RowStyle BackColor="#F7F7DE" />
                     <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                     <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                     <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                     <AlternatingRowStyle BackColor="White" />
                     <Columns>
                         <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                         <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n" />
                     </Columns>
                 </asp:GridView>
             </td>
             <td width="50%">
                HISTORIAL DE DUPLICADOS DE LICENCIA
                 <asp:GridView ID="gvDuplicados" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" EmptyDataText="No registra duplicados de licencia">
                     <FooterStyle BackColor="#CCCC99" />
                     <RowStyle BackColor="#F7F7DE" />
                     <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                     <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                     <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                     <AlternatingRowStyle BackColor="White" />
                     <Columns>
                         <asp:BoundField DataField="FECHA_EXPEDICION" HeaderText="Fecha de Expedici&#243;n" />
                         <asp:BoundField DataField="FECHA_EXPIRACION" HeaderText="Fecha de Expiraci&#243;n" />
                         <asp:BoundField DataField="CATEGORIA" HeaderText="Categor&#237;a" />
                     </Columns>
                 </asp:GridView>
             </td>
            </tr>
            <tr>
             <td width="50%" valign="top">
                BLOQUEOS
                 <asp:GridView ID="gvBloqueos" runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" AutoGenerateColumns="False" EmptyDataText="No registra bloqueos">
                     <FooterStyle BackColor="#CCCC99" />
                     <RowStyle BackColor="#F7F7DE" />
                     <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                     <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                     <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                     <AlternatingRowStyle BackColor="White" />
                     <Columns>
                         <asp:BoundField DataField="FECHA" HeaderText="Fecha" />
                         <asp:BoundField DataField="DESCRIPCION" HeaderText="Descripci&#243;n" />
                     </Columns>
                 </asp:GridView>
             </td>
             <td width="50%" valign="top">
                 &nbsp;</td>
            </tr>
          </table>
            
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
    <div>
        <UserControls:footer ID="Footer" runat="server" />
    </div>    
    </form>
</body>
</html>
