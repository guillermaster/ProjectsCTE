<%@ Page Title="" Language="C#" MasterPageFile="~/MainV2.master" AutoEventWireup="true"
    CodeFile="PuntosLicencia.aspx.cs" Inherits="Consultas_Licencias_PuntosLicencia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        table th
        {
            padding: 0 25px 0 10px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightColumnContent" runat="Server">
    <div class="full">
        <h2>Equivalencia</h2>
        <asp:GridView ID="gvLeyendaPuntos" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1"
            Width="263px" HorizontalAlign="Center" style="margin: 10px 10px 0 20px">
            <Columns>
                <asp:BoundField DataField="Tipo" HeaderText="Tipo" SortExpression="Tipo" />
                <asp:BoundField DataField="Puntos" HeaderText="Puntos" SortExpression="Puntos" />
            </Columns>
        </asp:GridView>
    </div>
    <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/Consultas/Licencias/LeyendaPuntos.xml">
    </asp:XmlDataSource>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <!--<div id="divError" class="left" style="width:623px" runat="server" visible="false">
    </div>-->
    <div id="divContent" runat="server">
        <div class="full">
            <h2>Puntos en Mi Licencia</h2>
            <asp:DetailsView ID="dvPuntos" runat="server" FieldHeaderStyle-Width="270px" style="margin-bottom:93px" />
        </div>
    </div>
</asp:Content>
