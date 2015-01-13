<%@ Page Title="" Language="C#" MasterPageFile="~/MainV2.master" AutoEventWireup="true" CodeFile="VehiculosCanchon.aspx.cs" Inherits="Consultas_Vehiculos_VehiculosCanchon" %>
<%@ MasterType VirtualPath="~/MainV2.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../../js/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../../js/alertbox.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../../js/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightColumnContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="title">
        Vehículos Retenidos
    </div>
    <div class="full" id="divContent" runat="server">
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" 
                DataFile="~/XML/TiposIdentificacion.xml" />
        <div>
            <table>
                <tr>
                    <td>Identificación:</td>
                    <td><asp:TextBox ID="txtIdentificacion" runat="server" CausesValidation="True" />
                        <asp:RequiredFieldValidator ID="reqFValIdent" runat="server" ErrorMessage="*" ControlToValidate="txtIdentificacion" /></td>
                </tr>
                <tr>
                    <td>Tipo de identificación:</td>
                    <td><asp:DropDownList ID="ddlTipoIdent" runat="server" 
                            DataSourceID="XmlDataSource1" DataTextField="descripcion" 
                            DataValueField="codigo" /> 
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" />
                </td>
                </tr>
            </table>
        </div>
        <asp:DetailsView ID="detViewVehCanchon" Width="640" runat="server" />
    </div>
</asp:Content>

