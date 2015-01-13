<%@ Page Title="" Language="C#" MasterPageFile="~/OneColumnV2.master" AutoEventWireup="true"
    CodeFile="DataUserCTG.aspx.cs" Inherits="DataUserCTG" %>
<%@ MasterType VirtualPath="~/OneColumnV2.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="js/alertbox.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="js/style.css" />
    <style type="text/css">
        table
        {
            width: 100%;
            margin: 0;
        }
        table th
        {
            text-align: left;
            width: 80px;
        }
        table td
        {
            text-align: left;
            padding: 2px 15px 2px 0px;
            width: 420px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divMain" runat="server">
        <div class="title">
            Mis Datos Personales en CTE
        </div>
        <asp:Panel runat="server" ID="pnlContent" class="full" style="width: 90%">
            <table>
                <tr>
                    <th>
                        <asp:Label ID="lblIdentificacion" runat="server" Text="Identificación" />
                    </th>
                    <td>
                        <asp:TextBox ID="txtIdentificacion" ReadOnly="true" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        Nombres
                    </th>
                    <td>
                        <asp:TextBox ID="txtNombres" ReadOnly="true" runat="server" Width="280" />
                    </td>
                    <th>
                        Apellidos
                    </th>
                    <td>
                        <asp:TextBox ID="txtApellidos" ReadOnly="true" runat="server" Width="280" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <h2>
                            Datos de Nacimiento</h2>
                    </td>
                </tr>
                <tr>
                    <th>
                        Fecha
                    </th>
                    <td>
                        <asp:TextBox ID="txtFechaNac" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        Cantón
                    </th>
                    <td>
                        <asp:TextBox ID="txtCantonNac" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        Provincia
                    </th>
                    <td>
                        <asp:TextBox ID="txtProvinciaNac" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        País
                    </th>
                    <td>
                        <asp:TextBox ID="txtPaisNac" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <h2>
                            Rasgos Físicos y Actividades</h2>
                    </td>
                </tr>
                <tr>
                    <th>
                        Sexo
                    </th>
                    <td>
                        <asp:TextBox ID="txtSexo" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        Estatura
                    </th>
                    <td>
                        <asp:TextBox ID="txtEstatura" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        Tipo de sangre
                    </th>
                    <td>
                        <asp:TextBox ID="txtTipoSangre" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        Estado civil
                    </th>
                    <td>
                        <asp:TextBox ID="txtEstadoCivil" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        Profesión
                    </th>
                    <td>
                        <asp:TextBox ID="txtProfesion" ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <h2>
                            Datos de contacto y residencia</h2>
                    </td>
                </tr>
                <tr>
                    <th>
                        Teléfono
                    </th>
                    <td>
                        <asp:TextBox ID="txtTelefono" ReadOnly="true" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="hdnTelefono" runat="server" />
                        <asp:ImageButton ID="btnEditTelefono" ImageUrl="~/images/icoXsEditField.png" 
                            runat="server" onclick="btnEditTelefono_Click" />
                        <asp:ImageButton ID="btnSaveTelefono" ImageUrl="~/images/btnSmGuardar.png" 
                            runat="server" Visible="false" onclick="btnSaveTelefono_Click" />
                        <asp:ImageButton ID="btnCancelTelefono" ImageUrl="~/images/btnSmCancelar.png" 
                            runat="server" Visible="false" onclick="btnCancelTelefono_Click" />
                    </td>
                    <th>
                        Teléfono móvil
                    </th>
                    <td>
                        <asp:TextBox ID="txtCelular" ReadOnly="true" runat="server"></asp:TextBox>
                        <asp:HiddenField ID="hdnTelefonoMovil" runat="server" />
                        <asp:ImageButton ID="btnEditTelMov" ImageUrl="~/images/icoXsEditField.png" 
                            runat="server" onclick="btnEditTelMov_Click1" />
                        <asp:ImageButton ID="btnSaveTelMov" ImageUrl="~/images/btnSmGuardar.png" 
                            runat="server" Visible="false" onclick="btnSaveTelMov_Click1" />
                        <asp:ImageButton ID="btnCancelTelmov" ImageUrl="~/images/btnSmCancelar.png" 
                            runat="server" Visible="false" onclick="btnCancelTelmov_Click" />
                    </td>
                </tr>
                <tr>
                    <th>
                        País
                    </th>
                    <td>
                        <asp:TextBox ID="txtPaisRes" runat="server" ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <th>
                        Provincia
                    </th>
                    <td>
                        <asp:TextBox ID="txtProvRes" runat="server" ReadOnly="true" />
                    </td>
                    <th>
                        Cantón
                    </th>
                    <td>
                        <asp:TextBox ID="txtCantonRes" runat="server" ReadOnly="true" />
                    </td>
                </tr>
                <tr>
                    <th>
                        Dirección
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="txtDireccion" ReadOnly="true" runat="server" Width="75%" />
                        <asp:ImageButton ID="btnEditDireccion" ImageUrl="~/images/icoXsEditField.png" 
                            runat="server" onclick="btnEditDireccion_Click" />
                    </td>
                </tr>
            </table>

            <asp:Panel ID="pnlEditDireccion" runat="server" BackColor="AntiqueWhite" Style=" padding: 10px;
                            filter: alpha(opacity=95); opacity: 0.95; text-align: center;"
                            ScrollBars="None" DefaultButton="btnSaveChangeAddress">
                            <table style="width: 640px; padding-bottom:10px;">
                                <tr>
                                    <td>
                                        País:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlPais" runat="server" Width="250" AutoPostBack="True" 
                                            onselectedindexchanged="ddlPais_SelectedIndexChanged" />
                                    </td>
                                    <td>
                                        Provincia:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlProvincia" runat="server" Width="250" 
                                            AutoPostBack="True" 
                                            onselectedindexchanged="ddlProvincia_SelectedIndexChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Cantón:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCanton" runat="server" Width="250" AutoPostBack="True" 
                                            onselectedindexchanged="ddlCanton_SelectedIndexChanged" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Ciudadela:
                                    </td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="ddlCiudadela" runat="server" Width="620" />
                                        <asp:HiddenField ID="hdnCiudadela" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Calle 1:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCalle1" Width="250" runat="server" />
                                        <asp:HiddenField ID="hdnCalle1" runat="server" />
                                    </td>
                                    <td>
                                        Calle 2:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCalle2" Width="250" runat="server" />
                                        <asp:HiddenField ID="hdnCalle2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Número o Villa:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNumVilla" Width="70" runat="server" />
                                        <asp:HiddenField ID="hdnNumVilla" runat="server" />
                                    </td>
                                    <td>
                                        Manzana:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtManzana" Width="70" runat="server" />
                                        <asp:HiddenField ID="hdnManzana" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        Piso:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPiso" Width="70" runat="server" />
                                        <asp:HiddenField ID="hdnPiso" runat="server" />
                                    </td>
                                    <td>
                                        Departamento:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDpto" Width="70" runat="server" />
                                        <asp:HiddenField ID="hdnDpto" runat="server" />
                                    </td>
                                </tr>
                            </table>
                            <asp:ImageButton ID="btnSaveChangeAddress" ImageUrl="~/images/btnSmGuardar.png" 
                                runat="server" onclick="btnSaveChangeAddress_Click" />
                            <asp:ImageButton ID="btnCancelChangAddress" 
                                ImageUrl="~/images/btnSmCancelar.png" runat="server" 
                                onclick="btnCancelChangAddress_Click" />
                            <asp:Button ID="Button2" runat="server" Style="display: none" Text="Button" />
                        </asp:Panel>
        </asp:Panel>
        <asp:Button ID="Button1" runat="server" Style="display: none" />
        <asp:ModalPopupExtender ID="MPE" runat="server" TargetControlID="Button1" PopupControlID="pnlEditDireccion"
            BackgroundCssClass="modalBackground" DropShadow="false" CancelControlID="Button2" />
    </div>
</asp:Content>
