<%@ Page Title="" Language="C#" MasterPageFile="~/OneColumnV2.master" AutoEventWireup="true"
    CodeFile="VehiculoDetInfo.aspx.cs" Inherits="Consultas_Vehiculos_VehiculoDetInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../../CSS/AjaxTabsCss.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        table td { height: 25px; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField runat="server" ID="hdnPlaca" />
    <div id="divMain" runat="server">
        <div class="title">
            Mis Vehículos
        </div>
        <div class="full" id="divContent" runat="server">
            <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" CssClass="linkedin linkedin-blue">
                <asp:TabPanel runat="server" HeaderText="Características principales" ID="TabCaracteristicas">
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
                                    <asp:TextBox ID="txtTonelaje" runat="server" ReadOnly="true"></asp:TextBox>
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
                                    <asp:Label ID="lblCilindraje" runat="server" Text="Cilindraje" Visible="True"></asp:Label>
                                </td>
                                <td style="width: 175px">
                                    <asp:TextBox ID="txtCilindraje" runat="server" ReadOnly="true" Visible="True"></asp:TextBox>
                                </td>
                                <td style="width: 59px">
                                    &nbsp;
                                </td>
                                <td style="width: 97px">
                                    <asp:Label ID="lblCAVMCPN" runat="server" Text="CAMV/CPN" Visible="True"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCAVMCPN" runat="server" ReadOnly="True" Visible="True"></asp:TextBox>
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
                </asp:TabPanel>
                <asp:TabPanel runat="server" HeaderText="Datos de transportación pública" ID="TabTransPub">
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
                </asp:TabPanel>
                <asp:TabPanel runat="server" HeaderText="Historial de matriculación" ID="TabMatriculacion">
                    <ContentTemplate>
                        <asp:GridView ID="gvHistMatriculac" runat="server" />
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel runat="server" HeaderText="Bloqueos" ID="TabBloqueos">
                    <ContentTemplate>
                        <asp:GridView ID="gvBloqueos" runat="server" />
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </div>
    </div>
</asp:Content>
