<%@ Page Language="C#" MasterPageFile="~/MainV2.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/MainV2.master" %>

<%@ Register TagPrefix="ddlb" Assembly="OptionDropDownList" Namespace="OptionDropDownList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../js/jquery.min.js"></script>
    <script type="text/javascript" src="../js/jquery.easing.1.3.js"></script>
    <script type="text/javascript" src="../js/alertbox.js"></script>
    <link rel="stylesheet" type="text/css" media="all" href="../js/style.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="full">
        <h2>Trámites en Línea</h2>
        Seleccione el trámite deseado e ingrese los datos requeridos
        <asp:Accordion ID="Accordion1" runat="server">
            <Panes>
                <asp:AccordionPane ID="AccordionPane1" runat="server">
                    <Header>
                        Atención al Usuario</Header>
                    <Content>
                        <ddlb:optiongroupselect id="ddlAccPane1" runat="server" appenddatabounditems="True"
                            autopostback="True" enableviewstate="True" onvaluechanged="ddlTramite_SelectedIndexChanged" />
                        
                        <asp:Panel ID="panel1" runat="server" style="margin: 10px">
                        </asp:Panel>
                        <asp:Button ID="btnContinuar1" runat="server" Text="Continuar »" OnClick="btnContinuar_Click"
                            Visible="false" CausesValidation="true" />
                    </Content>
                </asp:AccordionPane>
                <asp:AccordionPane ID="AccordionPane2" runat="server">
                    <Header>
                        Brevetación</Header>
                    <Content>
                        <ddlb:optiongroupselect id="ddlAccPane2" runat="server" appenddatabounditems="True"
                            autopostback="True" enableviewstate="True" onvaluechanged="ddlTramite_SelectedIndexChanged" />
                        
                        <asp:Panel ID="panel2" runat="server" style="margin: 10px">
                        </asp:Panel>
                        <asp:Button ID="btnContinuar2" runat="server" Text="Continuar »" OnClick="btnContinuar_Click"
                            Visible="false" />
                    </Content>
                </asp:AccordionPane>
                <asp:AccordionPane ID="AccordionPane3" runat="server">
                    <Header>
                        Citaciones y Partes</Header>
                    <Content>
                        <ddlb:optiongroupselect id="ddlAccPane3" runat="server" appenddatabounditems="True"
                            autopostback="True" enableviewstate="True" onvaluechanged="ddlTramite_SelectedIndexChanged" />
                        
                        <asp:Panel ID="panel3" runat="server" style="margin: 10px">
                        </asp:Panel>
                        <asp:Button ID="btnContinuar3" runat="server" Text="Continuar »" OnClick="btnContinuar_Click"
                            Visible="false" />
                    </Content>
                </asp:AccordionPane>
                <asp:AccordionPane ID="AccordionPane4" runat="server">
                    <Header>
                        Matriculación</Header>
                    <Content>
                        <ddlb:optiongroupselect id="ddlAccPane4" runat="server" appenddatabounditems="True"
                            autopostback="True" enableviewstate="True" onvaluechanged="ddlTramite_SelectedIndexChanged" />
                        
                        <asp:Panel ID="panel4" runat="server" style="margin: 10px">
                        </asp:Panel>
                        <asp:Button ID="btnContinuar4" runat="server" Text="Continuar »" OnClick="btnContinuar_Click"
                            Visible="false" />
                    </Content>
                </asp:AccordionPane>
            </Panes>
        </asp:Accordion>
    </div>
    <asp:HiddenField ID="hdnHiddenAbv" Value="hdn" runat="server" />
    <asp:HiddenField ID="hdnDropDownListAbv" Value="ddl" runat="server" />
    <asp:HiddenField ID="hdnPanelAbv" Value="pnl" runat="server" />
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="RightColumnContent">
    <asp:Panel class="full" runat="server" ID="pnlSidebar" Visible="false">
        <h2 style="margin-bottom:10px"><asp:Label ID="lblSelectedDropDownListText" runat="server" Text="" /></h2>
        <h3 style="margin: 0 15px">¿Desea continuar y solicitar este trámite?</h3>
        <asp:Panel runat="server" ID="pnlConfirmar" style="margin: 0 15px">
        </asp:Panel>
        <div align="center" style="margin: 15px 15px 0 15px">
            <asp:Button ID="btnSi" runat="server" Text="Sí" Visible="false" 
                onclick="btnSi_Click" />
            <asp:Button ID="btnNo" runat="server" Text="No" Visible="false" 
                onclick="btnNo_Click" />
        </div>
    </asp:Panel>
</asp:Content>
