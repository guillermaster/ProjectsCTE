<%@ Page Title="" Language="C#" MasterPageFile="~/OneColumnV2.master" AutoEventWireup="true"
    CodeFile="Vehiculos.aspx.cs" Inherits="Consultas_Vehiculos_Vehiculos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divMain" runat="server">
        <div class="title">
            Mis Vehículos
        </div>
        <div class="full" id="divContent" runat="server">
            <asp:GridView ID="gvVehiculos" runat="server" 
                onselectedindexchanged="gvVehiculos_SelectedIndexChanged" >
                <Columns>
                    <asp:CommandField SelectText="Ver más datos" ShowSelectButton="True">
                    <ItemStyle Font-Size="X-Small" />
                    </asp:CommandField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
