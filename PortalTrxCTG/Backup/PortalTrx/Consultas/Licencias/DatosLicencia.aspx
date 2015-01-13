<%@ Page Title="" Language="C#" MasterPageFile="~/OneColumnV2.master" AutoEventWireup="true"
    CodeFile="DatosLicencia.aspx.cs" Inherits="Consultas_Licencias_DatosLicencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 145px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divContent" runat="server">
        <div class="title">
            Mi Licencia de Conducir
        </div>
        <div class="full" style="height: 170px">
            <div style="float: left; width: 132px">
                <asp:Image ID="imgFoto" runat="server" Height="165px" Width="132px" />
            </div>
            <div style="float: right;">
                <asp:DetailsView ID="dvDatosPersonales" runat="server" Height="50px" Width="720px"
                    EmptyDataText="---" EnableTheming="False" Font-Overline="False" GridLines="None">
                    <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" BorderStyle="None" Height="18px" />
                    <FieldHeaderStyle HorizontalAlign="Left" VerticalAlign="Top" BorderStyle="None" Font-Bold="True" />
                    <HeaderStyle Width="20px" BorderStyle="None" Font-Bold="True" Font-Size="Small" VerticalAlign="Top"
                        HorizontalAlign="Center" />
                </asp:DetailsView>
            </div>
        </div>
        <div class="full">
            <table>
                <tr>
                    <td>
                        <div class="titleLeft">
                            Licencias Emitidas
                        </div>
                        <div class="titleRight">
                            Restricciones
                        </div>
                        <div class="left" id="divLicencias" runat="server">
                            <asp:GridView ID="grdLicencias" runat="server" Width="100%" AllowPaging="True" PageSize="5" OnPageIndexChanging="GrdLicenciasPageIndexChanging" />
                        </div>
                        <div class="right" id="divRestricciones" runat="server">
                            <asp:GridView ID="grdRestricciones" runat="server" Width="100%"
                                AllowPaging="True" PageSize="5" OnPageIndexChanging="GrdRestriccionesPageIndexChanging" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="titleLeft">
                            Infracciones Graves
                        </div>
                        <div class="titleRight">
                            Bloqueos
                        </div>
                        <div class="left" id="divCitaciones" runat="server">
                            <asp:GridView ID="grdInfracciones" runat="server" Width="100%"
                                AllowPaging="True" PageSize="5" OnPageIndexChanging="GrdInfraccionesPageIndexChanging" />
                        </div>
                        <div class="right" id="divBloqueos" runat="server">
                            <asp:GridView ID="grdBloqueos" runat="server" Width="100%" AllowPaging="True"
                                PageSize="5" OnPageIndexChanging="GrdBloqueosPageIndexChanging" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
