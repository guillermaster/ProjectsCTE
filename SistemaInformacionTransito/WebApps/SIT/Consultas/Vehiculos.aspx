<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Vehiculos.aspx.cs" Inherits="Consultas_DatosVehiculo" %>

<%@ MasterType VirtualPath="~/Site.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script LANGUAGE="JavaScript" SRC="../js/common.js">
    </script>
    <script language="javascript" type="text/javascript">
        function SetFocusOnPlaca() {
            document.getElementById('<%= txtPlaca.ClientID %>').focus();
        }
        function SetFocusOnChasis() {
            document.getElementById('<%= txtChasis.ClientID %>').focus();
        }
        function CheckPlaca() {
            document.getElementById('<%= radPlaca.ClientID %>').checked = true;
        }
        function CheckChasis() {
            document.getElementById('<%= radChasis.ClientID %>').checked = true;
        }
        function CollapseCP1() {
            $find("cpe1")._doClose();
        }
        function CollapseCP2() {
            $find("cpe2")._doClose();
        }
        function CollapseCPs() {

            $find("cpe1")._doClose();
            $find("cpe2")._doClose();
            document.getElementById("<%= pnlContent.ClientID %>").style.display = "none";
        }  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="QueryContent" runat="Server">
    <table>
        <tr>
            <td>
                <asp:Panel ID="pnlTitleExt" runat="server" CssClass="collapsePanelHeader">
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/btnconsultarSel.png" onClientClick="javascript: CollapseCP2();" />
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </asp:Panel>
            </td>
            <td>
                <asp:Panel ID="pnlTitleExt2" runat="server" CssClass="collapsePanelHeader">
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/images/btnconsultar.png" onClientClick="javascript: CollapseCP1();" />
                    <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                </asp:Panel>
            </td>
        </tr>
    </table>
    
     
    <asp:Panel ID="pnlQuery" runat="server" CssClass="collapsePanel">
        <div id="searchFormBigger" style="float: left">
        <table>
            <tr>
                <td>
                    <asp:RadioButton ID="radPlaca" GroupName="tipoBusqueda" onclick="javascrip: SetFocusOnPlaca();"
                        runat="server" Checked="true" />
                </td>
                <td>
                    Por placa
                </td>
                <td>
                    <asp:TextBox ID="txtPlaca" runat="server" onclick="javascript: CheckPlaca();" MaxLength="7"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="radChasis" GroupName="tipoBusqueda" onclick="javascrip: SetFocusOnChasis();"
                        runat="server" />
                </td>
                <td>
                    Por chasis
                </td>
                <td>
                    <asp:TextBox ID="txtChasis" runat="server" onclick="javascript: CheckChasis();"></asp:TextBox>
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
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" 
                        onclick="btnConsultar_Click" OnClientClick="javascript: CollapseCPs();" />
                </td>
            </tr>
        </table>
    </div>
    </asp:Panel>

    <asp:Panel ID="pnlQuery2" runat="server" CssClass="collapsePanel2">
        <div id="searchForm">
                Núm. de Licencia:
                <asp:TextBox ID="txtNumLicencia" runat="server" CausesValidation="True" ValidationGroup="ValGroupConsPorProp" Width="115px" />
                <asp:RequiredFieldValidator ID="reqFValNumLic" runat="server" ErrorMessage="*" ControlToValidate="txtNumLicencia" ValidationGroup="ValGroupConsPorProp" />
                <asp:Button ID="Button2" runat="server" Text="Consultar" 
                    ValidationGroup="ValGroupConsPorProp" onclick="Button2_Click" OnClientClick="javascript: CollapseCPs();" />
            </div>

    </asp:Panel>

    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender1" BehaviorID="cpe1" runat="server" 
    TargetControlID="pnlQuery" ExpandControlID="pnlTitleExt" CollapseControlID="pnlTitleExt" 
    TextLabelID="Label1" CollapsedText="" ExpandedText=""  ImageControlID="ImageButton1" 
     ExpandedImage="~/images/btnconsultarSel.png" CollapsedImage="~/images/btnconsultar.png" CollapsedSize="0" 
      ExpandedSize="140" Collapsed="true" SuppressPostBack="true">
    </cc1:CollapsiblePanelExtender>

    <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender2" BehaviorID="cpe2" runat="server" 
    TargetControlID="pnlQuery2" ExpandControlID="pnlTitleExt2" CollapseControlID="pnlTitleExt2" 
    TextLabelID="Label2" CollapsedText="" ExpandedText=""  ImageControlID="ImageButton2" 
     ExpandedImage="~/images/btnconsultarSel.png" CollapsedImage="~/images/btnconsultar.png" CollapsedSize="0" 
      ExpandedSize="140" Collapsed="false" SuppressPostBack="true" >
    </cc1:CollapsiblePanelExtender>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    
    <asp:Panel ID="pnlContent" runat="server" style="margin-top: 10px" visible="false">
        <cc1:tabcontainer id="TabContainer1" runat="server" activetabindex="0" cssclass="linkedin linkedin-blue"
        visible="false" width="100%">
        <cc1:TabPanel runat="server" HeaderText="Características" ID="TabCaracteristicas">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td style="width: 82px">
                            <asp:Label ID="lblPlaca" runat="server" Text="Placa"></asp:Label>
                        </td>
                        <td style="width: 175px">
                            <asp:TextBox ID="txtPlacaDat" runat="server" ReadOnly="True" CssClass="field"></asp:TextBox>
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
                            <asp:TextBox ID="txtChasisDat" runat="server" ReadOnly="True"></asp:TextBox>
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
                        <td>
                            <asp:TextBox ID="txtModelo" runat="server" ReadOnly="True" Columns="48"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblServicio" runat="server" Text="Servicio"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtServicio" runat="server" ReadOnly="true"></asp:TextBox>
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
                    <tr>
                        <td style="width: 82px">
                            <asp:Label ID="lblCantonCirc" runat="server" Text="Cantón de circulación"></asp:Label>
                        </td>
                        <td style="width: 175px">
                            <asp:TextBox ID="txtCantonCirc" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td style="width: 59px">
                            &nbsp;
                        </td>
                        <td style="width: 97px">
                            <asp:Label ID="lblFechaCompra" runat="server" Text="Fecha de compra"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFechaCompra" runat="server" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Transporte Público" ID="TabTransPub">
            <ContentTemplate>
                <table width="100%">
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
        <cc1:TabPanel runat="server" HeaderText="Foto" ID="TabFoto">
            <ContentTemplate>
                <div style="min-height: 290px">
                <div style="float:left;">
                    <asp:LinkButton ID="btnZoomImg" OnClick="btnZoomImg_Click" runat="server">
                        <asp:Image ID="imgFotoVeh" runat="server" />
                    </asp:LinkButton>
                </div>
                <div style="float:left; margin: 40px;">
                    <div class="info2">
                        Para visualizar la imagen en tamaño completo,<br /> haga clic sobre esta.
                    </div>
                </div>
                </div>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Matriculación" ID="TabMatriculacion">
            <ContentTemplate>
                <asp:GridView ID="gvHistMatriculac" runat="server" AutoGenerateColumns="true">
                </asp:GridView>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Bloqueos" ID="TabBloqueos">
            <ContentTemplate>
                <asp:GridView ID="gvBloqueos" runat="server" AutoGenerateColumns="true">
                </asp:GridView>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel runat="server" HeaderText="Propietario" ID="TabPropietario">
            <ContentTemplate>
                <asp:Panel ID="pnlDatosPropietario" runat="server" style="display: inline-block; clear: both;">
                    <div style="float: left">
                        <div style="float: left; width: 132px; padding: 2px 10px 3px 0px;">
                            <asp:Image ID="imgFoto" runat="server" Height="140px" Width="112px" />
                        </div>
                        <div style="float: left;">
                            <h3>
                                Datos Personales</h3>
                            <asp:FormView ID="frmViewDatosPers" runat="server">
                                <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" BorderStyle="None" Height="18px"
                                    Font-Size="Smaller" />
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <th>
                                                Nombre:
                                            </th>
                                            <td>
                                                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("nombre") %>' />
                                            </td>
                                            <th>
                                                Identificación:
                                            </th>
                                            <td>
                                                <asp:Label ID="lblIdent" runat="server" Text='<%# Eval("identificacion") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                Lugar de nacimiento:
                                            </th>
                                            <td>
                                                <asp:Label ID="lblLugNac" runat="server" Text='<%# Eval("lugar_nacimiento") %>' />
                                            </td>
                                            <th>
                                                Fecha de nacimiento:
                                            </th>
                                            <td>
                                                <asp:Label ID="lblFecNac" runat="server" Text='<%# Eval("fecha_nacimiento") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                Sexo:
                                            </th>
                                            <td>
                                                <asp:Label ID="lblSexo" runat="server" Text='<%# Eval("sexo") %>' />
                                            </td>
                                            <th>
                                                Estatura:
                                            </th>
                                            <td>
                                                <asp:Label ID="lblEstatura" runat="server" Text='<%# Eval("estatura") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                Profesión:
                                            </th>
                                            <td>
                                                <asp:Label ID="lblProfesion" runat="server" Text='<%# Eval("profesion") %>' />
                                            </td>
                                            <th>
                                                Estado civil:
                                            </th>
                                            <td>
                                                <asp:Label ID="lblEstCivil" runat="server" Text='<%# Eval("estado_civil") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                Lugar de residencia:
                                            </th>
                                            <td>
                                                <asp:Label ID="lblLugRes" runat="server" Text='<%# Eval("lugar_residencia") %>' />
                                            </td>
                                            <th>
                                                Teléfono:
                                            </th>
                                            <td>
                                                <asp:Label ID="lblTelRes" runat="server" Text='<%# Eval("telefono") %>' />
                                            </td>
                                        </tr>
                                        <tr>
                                            <th>
                                                Dirección:
                                            </th>
                                            <td>
                                                <asp:Label ID="lblDirRes" runat="server" Text='<%# Eval("direccion") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:FormView>
                        </div>
                        <div style="float: left">
                            <h3>
                                Vehículos de su Propiedad</h3>
                            <asp:GridView ID="grdVehiculos" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="grdVehiculos_SelectedIndexChanged"
                                Width="120px">
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
                        <div style="float: right">
                            <h3>
                                Licencias Emitidas</h3>
                            <asp:GridView ID="grdLicencias" runat="server" Width="270px" />
                            <asp:Label ID="lblMensajeLic" runat="server" Text="" ForeColor="Red" />
                        </div>
                    </div>
                    <div style="float: left;">
                        <table cellpadding="10">
                            <tr>
                                <td valign="top">
                                    <h3>
                                        Hist. Infracciones Graves</h3>
                                    <asp:GridView ID="grdInfracciones" runat="server" Width="400px" />
                                    <asp:Label ID="lblMensajeInfracc" runat="server" Text="" ForeColor="Red" />
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
                                        Bloqueos</h3>
                                    <asp:GridView ID="grdBloqueos" runat="server" Width="300px" />
                                    <asp:Label ID="lblMensajeBloq" runat="server" Text="" ForeColor="Red" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlMsgPropVeh" runat="server" CssClass="warning" Visible="false">
                    <asp:Label ID="lblMsgPropVeh" runat="server"></asp:Label></asp:Panel>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:tabcontainer>
    </asp:Panel>

    <asp:GridView ID="gvVehicPorPropiet" runat="server" AutoGenerateColumns="True" OnSelectedIndexChanged="gvVehicPorPropiet_SelectedIndexChanged" Visible="false">
        <Columns>
            <asp:CommandField ButtonType="Image" SelectImageUrl="~/images/btnsearch.gif" ShowSelectButton="True" />
        </Columns>
    </asp:GridView>

    <asp:Button ID="Button1" runat="server" Style="display: none" />
    <asp:Panel ID="pnlFotoVehFull" runat="server" Style="display: none; padding: 10px; text-align: center;" BackColor="AntiqueWhite" ScrollBars="None">
        <div>
            <asp:LinkButton ID="LinkButton1" runat="server">
                <asp:Image ID="imgFotoVehFull" runat="server" /></asp:LinkButton>
        </div>
        <asp:LinkButton ID="btnClosePopup" runat="server">Cerrar</asp:LinkButton>
    </asp:Panel>

    <cc1:modalpopupextender id="MPE" runat="server" targetcontrolid="Button1" popupcontrolid="pnlFotoVehFull"
        backgroundcssclass="modalBackground" dropshadow="false" cancelcontrolid="btnClosePopup" />
    <br />
</asp:Content>
