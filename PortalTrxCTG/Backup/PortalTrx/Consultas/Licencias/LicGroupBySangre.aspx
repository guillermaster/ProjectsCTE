<%@ Page Title="" Language="C#" MasterPageFile="~/OneColumnV2.master" AutoEventWireup="true"
    CodeFile="LicGroupBySangre.aspx.cs" Inherits="Consultas_Licencias_LicGroupBySangre" %>

<%@ MasterType VirtualPath="~/OneColumnV2.master" %>
<%@ Register Src="../../UserControls/AZfilter.ascx" TagName="AZfilter" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../../js/jquery.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../../js/alertbox.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../../js/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divMain" runat="server">
        <div class="title">
            Licencias por Tipo de Sangre
        </div>
        <div class="full">
            <div>
                <span style="margin: 0 5px 0 0;">Rango de edad: </span>
                <asp:DropDownList ID="ddlRangoEdad" runat="server" />
                <span style="margin: 0 5px 0 15px;">Tipo de sangre: </span>
                <asp:DropDownList ID="ddlTipoSangre" runat="server" />
                <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="BtnConsultarClick" />
            </div>
            <div style="margin: 15px 0 10px 0">
                <uc1:AZfilter ID="AZfilter1" runat="server" OnFilterClicked="AZfilter1FilterClicked"
                    Visible="false" />
            </div>
            <div align="center">
            <asp:GridView ID="gvResLicencias" runat="server" AutoGenerateColumns="False" 
                OnPageIndexChanging="ResLicGridViewPageIndexChanging" AllowPaging="true" 
                Width="755px">
                <Columns>
                    <asp:BoundField DataField="apellidos" HeaderText="Apellidos">
                        <ItemStyle Width="220px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="nombres" HeaderText="Nombres">
                        <ItemStyle Width="220px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="telefono" HeaderText="Tel&#233;fono">
                        <ItemStyle Width="75px" HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField DataField="edad" HeaderText="Edad">
                        <ItemStyle Width="25px" HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
