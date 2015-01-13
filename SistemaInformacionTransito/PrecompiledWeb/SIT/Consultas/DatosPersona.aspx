<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Consultas_DatosLicencia, App_Web_datospersona.aspx.a1f19d04" theme="SkinFileSIT" %>

<%@ MasterType VirtualPath="~/Site.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <script language="javascript" type="text/javascript">
        function SetFocusOnNombre() {
            document.getElementById('<%= txtNombre.ClientID %>').focus();
            document.getElementById('hlpMsgNameSrch').style.display = 'block';
        }
        function SetFocusOnCedula() {
            document.getElementById('<%= txtNumLicencia.ClientID %>').focus();
            document.getElementById('hlpMsgNameSrch').style.display = 'none';
        }
        function CheckNombre() {
            document.getElementById('<%= radNom.ClientID %>').checked = true;
            document.getElementById('hlpMsgNameSrch').style.display = 'block';
        }
        function CheckCedula() {
            document.getElementById('<%= radLic.ClientID %>').checked = true;
            document.getElementById('hlpMsgNameSrch').style.display = 'none';
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="QueryContent" Runat="Server">
    <div id="searchFormBigger" style="float: left">
        <table>
            <tr>
                <td>
                    <asp:RadioButton ID="radLic" GroupName="tipoBusqueda" onclick="javascrip: SetFocusOnCedula();" runat="server" Checked="true" />
                </td>
                <td>
                    Por no. de licencia
                </td>
                <td>
                    <asp:TextBox ID="txtNumLicencia" runat="server" onclick="javascript: CheckCedula();"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="radNom" GroupName="tipoBusqueda" onclick="javascrip: SetFocusOnNombre();" runat="server" />
                </td>
                <td>
                    Por nombre
                </td>
                <td>
                    <asp:TextBox ID="txtNombre" runat="server" onclick="javascript: CheckNombre();"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td align="right">
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="gvPersonas" runat="server" Visible="false" 
        AutoGenerateColumns="False" 
        onselectedindexchanged="gvPersonas_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="IDENTIFICACION" HeaderText="Identificación" />
            <asp:BoundField DataField="NOMBRE_COMPLETO" HeaderText="Nombre" />
            <asp:BoundField DataField="DIRECCION" HeaderText="Dirección" />
            <asp:BoundField DataField="TELEFONO1" HeaderText="Teléfono" />
            <asp:BoundField DataField="CELULAR" HeaderText="Teléfono Móvil" />
            <asp:BoundField DataField="PROVINCIA" HeaderText="Provincia" />
            <asp:BoundField DataField="LOCALIDAD" HeaderText="Cantón" />
            <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/btnsearch.gif" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <asp:Panel ID="pnlDatosLicencia" Visible="false" runat="server" Style="display: inline-block;
        clear: both;">
        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="linkedin linkedin-blue" Width="100%">
            <asp:TabPanel runat="server" HeaderText="Datos personales" ID="TabDatosPers">
                <ContentTemplate>
                <div style="height: 170px">
                    <div style="float: left; width: 132px; padding: 5px 10px 3px 0px;">
                        <asp:Image ID="imgFoto" runat="server" Height="165px" Width="132px" />
                    </div>
                    <div style="float:left;">
                        <asp:FormView ID="frmViewDatosPers" runat="server">
                            <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" BorderStyle="None" Height="18px"
                                Font-Size="Smaller" />
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <th>Nombre:</th>
                                        <td><asp:Label ID="lblNombre" runat="server" Text='<%# Eval("nombre") %>' /></td>
                                        <th>Identificación:</th>
                                        <td><asp:Label ID="lblIdent" runat="server" Text='<%# Eval("identificacion") %>' /></td>                                        
                                    </tr>
                                    <tr>
                                        <th>Lugar de nacimiento:</th>
                                        <td><asp:Label ID="lblLugNac" runat="server" Text='<%# Eval("lugar_nacimiento") %>' /></td>
                                        <th>Fecha de nacimiento:</th>
                                        <td><asp:Label ID="lblFecNac" runat="server" Text='<%# Eval("fecha_nacimiento") %>' /></td>                                        
                                    </tr>
                                    <tr>
                                        <th>Sexo:</th>
                                        <td><asp:Label ID="lblSexo" runat="server" Text='<%# Eval("sexo") %>' /></td>
                                        
                                    </tr>
                                    <tr>
                                        <th>Estatura:</th>
                                        <td><asp:Label ID="lblEstatura" runat="server" Text='<%# Eval("estatura") %>' /></td>
                                    </tr>
                                    <tr>
                                        <th>Estado civil:</th>
                                        <td><asp:Label ID="lblEstCivil" runat="server" Text='<%# Eval("estado_civil") %>' /></td>
                                    </tr>
                                    <tr>
                                        <th>Profesión:</th>
                                        <td><asp:Label ID="lblProfesion" runat="server" Text='<%# Eval("profesion") %>' /></td>
                                    </tr>
                                    <tr>
                                        <th>Lugar de residencia:</th>
                                        <td><asp:Label ID="lblLugRes" runat="server" Text='<%# Eval("lugar_residencia") %>' /></td>
                                    </tr>
                                    <tr>
                                        <th>Dirección:</th>
                                        <td><asp:Label ID="lblDirRes" runat="server" Text='<%# Eval("direccion") %>' /></td>
                                        <th>Teléfono:</th>
                                        <td><asp:Label ID="lblTelRes" runat="server" Text='<%# Eval("telefono") %>' /></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:FormView>
                    </div>
                </div>
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" HeaderText="Licencias" ID="TabLicEmitidas">
                <ContentTemplate>
                    <asp:GridView ID="grdLicencias" runat="server" Width="100%" />
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" HeaderText="Restricciones" ID="TabRestric">
                <ContentTemplate>
                    <asp:GridView ID="grdRestricciones" runat="server" Width="100%" />
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" HeaderText="Infracciones graves" ID="TabInfGrav">
                <ContentTemplate>
                    <asp:GridView ID="grdInfracciones" runat="server" Width="100%" />
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" HeaderText="Bloqueos" ID="TabBloqueos">
                <ContentTemplate>
                    <asp:GridView ID="grdBloqueos" runat="server" Width="100%" />
                </ContentTemplate>
            </asp:TabPanel>
            <asp:TabPanel runat="server" HeaderText="Vehículos" ID="TabVeh">
                <ContentTemplate>
                    <asp:GridView ID="grdVehiculos" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="grdVehiculos_SelectedIndexChanged"
                    Width="100%">
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
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    </asp:Panel>
</asp:Content>

