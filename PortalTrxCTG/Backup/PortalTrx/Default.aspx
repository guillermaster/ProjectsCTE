<%@ Page Language="C#" MasterPageFile="~/MainV2.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="_Default" %>

<%@ MasterType VirtualPath="~/MainV2.master" %>
<%@ Register Src="UserControls/UserLoggedInBasic.ascx" TagName="UserLoggedInBasic"
    TagPrefix="myControls" %>
<%@ Register Src="UserControls/VerisignLogo.ascx" TagName="VerisignLogo" TagPrefix="uc1" %>
<%@ Register src="UserControls/BrowserNotSupported.ascx" tagname="BrowserNotSupported" tagprefix="myControls" %>
<%@ Register TagPrefix="ddlb" Assembly="OptionDropDownList" Namespace="OptionDropDownList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <a href="Consultas/Default.aspx">
            <div class="mainIcon">
                    <img title="Consultas" src="images/iconConsultas.png" alt="Consultas" />
                    <h3>Consultas</h3>
                    Consultas de citaciones, trámites, datos de su licencia, vehículos y más.
            </div>
        </a>
    </div>
    <div>
        <a href="Tramites/Default.aspx">
            <div class="mainIcon">
                    <img title="Trámites" src="images/iconTramites.png" alt="Trámites" />
                    <h3>Trámites</h3>
                    Solicite sus trámites en línea.
            </div>
        </a>
    </div>
    <myControls:BrowserNotSupported ID="BrowserNotSupported1" runat="server" Visible="false" />
</asp:Content>
<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="RightColumnContent">
    <asp:Panel ID="pnlUserData" runat="server">
        <div class="full">
            <myControls:UserLoggedInBasic ID="UserLoggedInBasic1" runat="server" />
        </div>
        <div class="titleLeft">
            <img src="images/icoDateTime.png" style="margin-right: 4px;" />Último acceso</div>
        <div class="titleRight">
            <img src="images/icoStatus.png" />Estado de cuenta</div>
        <div class="left" style="height: 22px;">
            <div style="padding-top: 5px; text-align: center;">
                <asp:Label ID="lblLastLoggedInDate" runat="server" />
            </div>
        </div>
        <div class="right" style="height: 22px;">
            <asp:Image ID="imgIcoEstado" Width="16" Height="16" runat="server" />
            <acronym id="acrEstado" runat="server">
                <asp:Label ID="lblEstado" runat="server" />
            </acronym>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlLogin" Visible="false" runat="server">
        <!--<div class="titleMessage">
            Acceda a su cuenta
        </div>  -->
        <!--<div class="full">-->
            <asp:Login ID="Login1" runat="server" SkinID="SmallLogin" OnAuthenticate="Login1_Authenticate" DisplayRememberMe="False" />


            <div style="margin: 20px 0 0 0;">
                <uc1:verisignlogo id="VerisignLogo1" runat="server" />
            </div>
            <ul style="margin-left: 85px; font-size: 11px; list-style-image: url('images/bulletDark.gif');">
                <li><a href="LoginExtra/UserRegistartion.aspx">Registre una nueva cuenta</a></li>
                <li><a href="LoginExtra/LostPassword.aspx">Recuerde su contraseña</a></li>
                <li><a href="LoginExtra/ResendActivationEmail.aspx">Solicite el reenvío e-mail de activación
                    de cuenta</a></li>
            </ul>
        <!--</div>-->
    </asp:Panel>
</asp:Content>
