<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ConsVeh.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <h1>
        Consulta de Datos de Vehículos</h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 0 0 20px 0;">
        Placa:<asp:TextBox ID="txtConsPlaca" CssClass="uppercase" runat="server" />
        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="button" OnClick="btnConsultar_Click" />
    </div>
    <asp:Panel ID="pnlWarning" runat="server" CssClass="warning" Visible="false">
        <asp:Label ID="lblWarning" runat="server"></asp:Label></asp:Panel>
    <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="linkedin linkedin-blue"
        Visible="false" Width="100%">
        <cc1:TabPanel runat="server" HeaderText="Características principales" ID="TabCaracteristicas">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td style="width: 82px">
                            <asp:Label ID="lblPlaca" runat="server" Text="Placa"></asp:Label>
                        </td>
                        <td style="width: 175px">
                            <asp:TextBox ID="txtPlaca" runat="server" ReadOnly="True" CssClass="field"></asp:TextBox>
                        </td>
                        <td style="width: 59px">
                            &nbsp;
                        </td>
                        <td style="width: 97px">
                            <asp:Label ID="lblColor" runat="server" Text="Color"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtColor" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 82px">
                            <asp:Label ID="lblChasis" runat="server" Text="Chasis"></asp:Label>
                        </td>
                        <td style="width: 175px">
                            <asp:TextBox ID="txtChasis" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 59px">
                            &nbsp;
                        </td>
                        <td style="width: 97px">
                            <asp:Label ID="lblMotor" runat="server" Text="Motor"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtMotor" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 82px">
                            <asp:Label ID="lblMarca" runat="server" Text="Marca"></asp:Label>
                        </td>
                        <td style="width: 175px">
                            <asp:TextBox ID="txtMarca" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 59px">
                            &nbsp;
                        </td>
                        <td style="width: 82px">
                            <asp:Label ID="lblTonelaje" runat="server" Text="Tonelaje"></asp:Label>
                        </td>
                        <td style="width: 175px">
                            <asp:TextBox ID="txtTonelaje" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 82px;">
                            <asp:Label ID="lblAno" runat="server" Text="Año"></asp:Label>
                        </td>
                        <td style="width: 175px;">
                            <asp:TextBox ID="txtAno" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 59px;">
                            &nbsp;
                        </td>
                        <td style="width: 97px;">
                            <asp:Label ID="lblPaisOrigen" runat="server" Text="País de Origen"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPaisOrigen" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 82px">
                            <asp:Label ID="lblClase" runat="server" Text="Clase"></asp:Label>
                        </td>
                        <td style="width: 175px">
                            <asp:TextBox ID="txtClase" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 59px">
                            &nbsp;
                        </td>
                        <td style="width: 97px">
                            <asp:Label ID="lblTipo" runat="server" Text="Tipo"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTipo" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 97px">
                            <asp:Label ID="lblModelo" runat="server" Text="Modelo"></asp:Label>
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtModelo" runat="server" ReadOnly="True" Columns="48"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 82px">
                            <asp:Label ID="lblCilindraje" runat="server" Text="Cilindraje"></asp:Label>
                        </td>
                        <td style="width: 175px">
                            <asp:TextBox ID="txtCilindraje" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 59px">
                            &nbsp;
                        </td>
                        <td style="width: 97px">
                            <asp:Label ID="lblCAVMCPN" runat="server" Text="CAMV/CPN"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCAVMCPN" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <!--
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
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Foto de revisión" ID="TabFoto">
            <ContentTemplate>
                <asp:Image ID="imgFotoVeh" runat="server" />
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Historial de matriculación" ID="TabMatriculacion">
            <ContentTemplate>
                <asp:GridView ID="gvHistMatriculac" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="anio_matricula" HeaderText="Año" />
                        <asp:BoundField DataField="emision" HeaderText="Fecha de Emisión" />
                        <asp:BoundField DataField="caducidad" HeaderText="Fecha de Caducidad" />
                        <asp:BoundField DataField="tipo_mat" HeaderText="Tipo de Matrícula" />
                        <asp:BoundField DataField="tipo_cobro" HeaderText="Tipo de Cobro" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Bloqueos" ID="TabBloqueos">
            <ContentTemplate>
                <asp:GridView ID="gvBloqueos" runat="server" AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="fecha_ingreso" HeaderText="Fecha de Ingreso" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Propietario" ID="TabPropietario">
            <ContentTemplate>
            <div style="display: inline-block; clear: both; ">
                <div style="float: left">
                                <div style="float: left; width: 132px; padding: 48px 10px 3px 0px;">
                                    <asp:Image ID="imgFoto" runat="server" Height="165px" Width="132px" />
                                </div>
                                <div style="float: left;">
                                    <h3>Datos Personales</h3>
                                    <asp:DetailsView ID="dvDatosPersonales" runat="server" Height="50px" Width="240px"
                                        EmptyDataText="---" EnableTheming="False" Font-Overline="False" GridLines="None">
                                        <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" BorderStyle="None" Height="18px" Font-Size="Smaller" />
                                        <FieldHeaderStyle HorizontalAlign="Left" VerticalAlign="Top" BorderStyle="None" Font-Bold="True" />
                                        <HeaderStyle Width="20px" BorderStyle="None" Font-Bold="True" Font-Size="Small" VerticalAlign="Top"
                                            HorizontalAlign="Center" />
                                    </asp:DetailsView>
                                </div>
                                <div style="float: right">
                                <h3>
                                    Vehículos de su Propiedad</h3>
                                <asp:GridView ID="grdVehiculos" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="grdVehiculos_SelectedIndexChanged"
                                    Width="100px">
                                    <Columns>
                                        <asp:BoundField DataField="placa" HeaderText="Placa" />
                                        <asp:BoundField DataField="clase" HeaderText="Clase" />
                                        <asp:BoundField DataField="marca" HeaderText="Marca" />
                                        <asp:BoundField DataField="modelo" HeaderText="Modelo" />
                                        <asp:BoundField DataField="color" HeaderText="Color" />
                                        <asp:BoundField DataField="bloqueos" HeaderText="Bloqueos" />
                                        <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/btnsearch.gif" ShowSelectButton="True" />
                                    </Columns>
                                </asp:GridView>
                                <asp:Label ID="lblMensajeVehiculos" runat="server" Text="" ForeColor="Red" />
                            </div>
                </div>
                <div style="float: left;">
                    <table cellpadding="10">
                        <tr>
                            <td valign="top">
                                <h3>
                                    Licencias Emitidas</h3>
                                <asp:GridView ID="grdLicencias" runat="server" Width="400px" />
                                <asp:Label ID="lblMensajeLic" runat="server" Text="" ForeColor="Red" />
                            </td>
                            <td valign="top">
                                <h3>
                                    Restricciones</h3>
                                <asp:GridView ID="grdRestricciones" runat="server" Width="300px" />
                                <asp:Label ID="lblMensajeRest" runat="server" Text="" ForeColor="Red" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <h3>
                                    Infracciones Graves</h3>
                                <asp:GridView ID="grdInfracciones" runat="server" Width="400px" />
                                <asp:Label ID="lblMensajeInfracc" runat="server" Text="" ForeColor="Red" />
                            </td>
                            <td valign="top">
                                <h3>
                                    Bloqueos</h3>
                                <asp:GridView ID="grdBloqueos" runat="server" Width="300px" />
                                <asp:Label ID="lblMensajeBloq" runat="server" Text="" ForeColor="Red" />
                            </td>
                        </tr>
                    </table>
                </div>
              </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Transporte Público" ID="TabTransPub">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td height="15" style="width: 82px">
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 22px; width: 82px;">
                            <asp:Label ID="lblNoPasajeros" runat="server" Text="No. Pasajeros"></asp:Label>
                        </td>
                        <td style="height: 22px; width: 175px;">
                            <asp:TextBox ID="txtNoPasajeros" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td style="height: 22px; width: 59px;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 82px">
                            <asp:Label ID="lblServicio" runat="server" Text="Servicio"></asp:Label>
                        </td>
                        <td style="width: 175px">
                            <asp:TextBox ID="txtServicio" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td style="width: 59px">
                            &nbsp;
                        </td>
                        <td style="width: 97px">
                            <asp:Label ID="lblClaseServicio" runat="server" Text="Clase Servicio"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtClaseServicio" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 82px">
                            <asp:Label ID="lblModalidad" runat="server" Text="Modalidad"></asp:Label>
                        </td>
                        <td style="width: 175px">
                            <asp:TextBox ID="txtModalidad" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td style="width: 59px">
                            &nbsp;
                        </td>
                        <td style="width: 97px">
                            <asp:Label ID="lblCooperativa" runat="server" Text="Cooperativa"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCooperativa" runat="server" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
</asp:Content>
